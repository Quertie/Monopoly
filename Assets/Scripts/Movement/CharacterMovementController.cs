using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Squares;
using UnityEngine;

namespace Movement
{
    internal class CharacterMovementController : ICharacterMovementController, ICharacterMovementSubscriber
    {
        private readonly IGameBoard _gameBoard;
        private bool _coroutineFinished;
        private TaskCompletionSource<bool> _coroutineFinishedTaskCompletionSource = new TaskCompletionSource<bool>();
        private readonly int _playerNumber;
    
        private Square CurrentSquare => _gameBoard.CurrentSquare[_playerNumber];

        public event EventHandler PlayerMovesOneSquare; 

        public CharacterMovementController(IGameBoard gameBoard, int playerNumber)
        {
            _gameBoard = gameBoard;
            _playerNumber = playerNumber;
        }

        public async Task UpdatePosition()
        {
            await MoveToSquare(CurrentSquare);
        }


        public async Task MoveSquares(int numberOfSquares)
        {
            var originSquare = _gameBoard.CurrentSquare[_playerNumber];
            var destinationSquare = _gameBoard.GetLandingSquare(originSquare, numberOfSquares);
            await MoveWithWaypointToSquare(destinationSquare);
        }
    
        private async Task MoveWithWaypointToSquare(Square square)
        {
            var waypoint = _gameBoard.GetWaypoint(_gameBoard.CurrentSquare[_playerNumber], square);
            foreach (Square step in waypoint)
            {
                _gameBoard.CurrentSquare[_playerNumber] = step;
                PlayerMovesOneSquare?.Invoke(this, EventArgs.Empty);
                await MoveToSquare(step);
            }
        }

        private async Task MoveToSquare(Square square)
        {
            var squareIndex = _gameBoard.GetSquareIndex(square);
            var squareGameObjectName = string.Format(Constants.GameObjectNames.Square, squareIndex);

            var tokenPositionMarkerGameObjectName = GetDestinationTokenPositionMarkerName(square);

            _coroutineFinishedTaskCompletionSource = new TaskCompletionSource<bool>();
            await UnityMainThreadDispatcher.Instance().EnqueueAsync(dispatcher =>
                MoveAction(dispatcher, tokenPositionMarkerGameObjectName, squareGameObjectName));
            await _coroutineFinishedTaskCompletionSource.Task;
        }

        private string GetDestinationTokenPositionMarkerName(Square destinationSquare)
        {
            var playersOnLandingSquareIncludingCurrent =
                _gameBoard.CurrentSquare.Select((square, index) => square.Name == destinationSquare.Name ? index : -1).Where(i => i >= 0).ToList();

            var positionOnSquare = playersOnLandingSquareIncludingCurrent.IndexOf(_playerNumber);

            return string.Format(Constants.GameObjectNames.TokenPosition, playersOnLandingSquareIncludingCurrent.Count(), positionOnSquare);
        }

        private void MoveAction(MonoBehaviour dispatcher, string positionMarkerGameObjectName, string destinationSquareGameObjectName)
        {
            var destinationSquareTransform = GetGameBoardGameObject().GetComponentsInChildren<Transform>().Single(c => c.gameObject.name == destinationSquareGameObjectName);
            var destinationPosition = destinationSquareTransform
                .GetComponentsInChildren<Transform>().Single(c => c.gameObject.name == positionMarkerGameObjectName).position;
            dispatcher.StartCoroutine(ProgressivelyMoveTowards(destinationPosition, 5f));
        }

        private IEnumerator ProgressivelyMoveTowards(Vector3 destinationPosition, float speed)
        {
            var playerPosition = GetPlayerTokenGameObject().transform.position;
            while (playerPosition != destinationPosition)
            {
                playerPosition = IsPlayerPositionAlmostDestination(destinationPosition, playerPosition)
                    ? destinationPosition
                    : Vector3.MoveTowards(playerPosition, destinationPosition, speed * Time.deltaTime);
            
                var playerGameObject = GetPlayerTokenGameObject();
                playerGameObject.transform.position = playerPosition;
                yield return null;
            }
            _coroutineFinishedTaskCompletionSource.SetResult(true);
            yield return null;
        }

        private static bool IsPlayerPositionAlmostDestination(Vector3 destinationPosition, Vector3 playerPosition)
        {
            return Vector3.Magnitude(playerPosition - destinationPosition) < .1;
        }

        private GameObject GetPlayerTokenGameObject()
        {
            // TODO extract this to Constants
            return GameObject.Find($"Player {_playerNumber}");
        }
    
        private GameObject GetGameBoardGameObject()
        {
            return GameObject.Find(Constants.GameObjectNames.Board);
        }

        public void HandleCharacterMovement()
        {
            Task.Run(UpdatePosition).ConfigureAwait(false);
        }
    }
}