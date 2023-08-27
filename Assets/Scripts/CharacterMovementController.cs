using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Squares;
using UnityEngine;

internal class CharacterMovementController : ICharacterMovementController
{
    private readonly IGameBoard _gameBoard;
    private bool _coroutineFinished;
    private TaskCompletionSource<bool> _coroutineFinishedTaskCompletionSource = new TaskCompletionSource<bool>();
    
    
    private static readonly TaskCompletionSource<bool> TaskCompletionSource = new TaskCompletionSource<bool>();

    public CharacterMovementController(IGameBoard gameBoard)
    {
        _gameBoard = gameBoard;
    }

    public async Task MoveToSquare(Square square)
    {
        Debug.Log($"About to start moving to square {square.Name}");
        var waypoint = _gameBoard.GetWaypoint(_gameBoard.CurrentSquare, square);
        Debug.Log($"Found waypoint with squares {string.Join(", ", waypoint.Select(s => s.Name))}");
        foreach (Square step in waypoint)
        {
            var squareIndex = _gameBoard.GetSquareIndex(step);                                      
            var squareGameObjectName = string.Format(Constants.GameObjectNames.Square, squareIndex);   
            
            var tokenPositionMarkerGameObjectName = Constants.GameObjectNames.TokenPosition10;

            _coroutineFinishedTaskCompletionSource = new TaskCompletionSource<bool>();
            await UnityMainThreadDispatcher.Instance().EnqueueAsync(dispatcher =>
                MoveAction(dispatcher, tokenPositionMarkerGameObjectName, squareGameObjectName));
            Debug.Log($"Enqueued move task to destination {step.Name}");
            await _coroutineFinishedTaskCompletionSource.Task;
            Debug.Log("Step complete");
        }

        _gameBoard.CurrentSquare = square;
    }
    
    private void MoveAction(MonoBehaviour dispatcher, string positionMarkerGameObjectName, string destinationSquareGameObjectName)
    {
        var destinationSquareTransform = GetGameBoardGameObject().GetComponentsInChildren<Transform>().Single(c => c.gameObject.name == destinationSquareGameObjectName);
        var destinationPosition = destinationSquareTransform
            .GetComponentsInChildren<Transform>().Single(c => c.gameObject.name == positionMarkerGameObjectName).position;
        dispatcher.StartCoroutine(ProgressivelyMoveTowards(destinationPosition, 5f));
        Debug.Log($"Started move coroutine to destination {destinationSquareGameObjectName}");
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

    private static GameObject GetPlayerTokenGameObject()
    {
        return GameObject.Find("Player");
    }
    
    private GameObject GetGameBoardGameObject()
    {
        return GameObject.Find(Constants.GameObjectNames.Board);
    }
}