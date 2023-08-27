using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Squares;
using UnityEngine;

internal class CharacterMovementController : ICharacterMovementController
{
    private readonly IGameBoard _gameBoard;
    private bool _coroutineFinished;
    private TaskCompletionSource<bool> _coroutineFinishedTaskCompletionSource = new TaskCompletionSource<bool>();
    private readonly int _playerNumber;

    public CharacterMovementController(IGameBoard gameBoard, int playerNumber)
    {
        _gameBoard = gameBoard;
        _playerNumber = playerNumber;
    }

    public async Task MoveSquares(int numberOfSquares)
    {
        var originSquare = _gameBoard.CurrentSquare[_playerNumber];
        var destinationSquare = _gameBoard.GetLandingSquare(originSquare, numberOfSquares);
        await MoveToSquare(destinationSquare);
    }
    
    private async Task MoveToSquare(Square square)
    {
        var waypoint = _gameBoard.GetWaypoint(_gameBoard.CurrentSquare[_playerNumber], square);
        foreach (Square step in waypoint)
        {
            var squareIndex = _gameBoard.GetSquareIndex(step);                                      
            var squareGameObjectName = string.Format(Constants.GameObjectNames.Square, squareIndex);   
            
            var tokenPositionMarkerGameObjectName = GetDestinationTokenPositionMarkerName(step);

            _coroutineFinishedTaskCompletionSource = new TaskCompletionSource<bool>();
            await UnityMainThreadDispatcher.Instance().EnqueueAsync(dispatcher =>
                MoveAction(dispatcher, tokenPositionMarkerGameObjectName, squareGameObjectName));
            await _coroutineFinishedTaskCompletionSource.Task;
        }
        _gameBoard.CurrentSquare[_playerNumber] = square;
    }

    private string GetDestinationTokenPositionMarkerName(Square destinationSquare)
    {
        var numberOfPlayersOnLandingSquareIncludingCurrent =
            _gameBoard.CurrentSquare.Count(somePlayerCurrentSquare => somePlayerCurrentSquare.Name == destinationSquare.Name) + 1;
        
        return string.Format(Constants.GameObjectNames.TokenPosition, numberOfPlayersOnLandingSquareIncludingCurrent,
            numberOfPlayersOnLandingSquareIncludingCurrent - 1);
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
}