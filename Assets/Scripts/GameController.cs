using System.Linq;
using Boards.Classic;
using Boards.Classic.GameBoardGameObjectCreation;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const float SquareWidth = 4f;
    private const float SquareHeight = 6.5f;
    
    private IGameBoard GameBoard {get; set;}
    
    private void Start()
    {
        var gameBoardProvider = new ClassicGameBoardProvider();
        GameBoard = gameBoardProvider.GetBoard();
        BuildBoard();

        CreatePlayerToken();
    }

    private static void CreatePlayerToken()
    {
        var firstSquareTokenPosition = GameObject.Find(string.Format(Constants.GameObjectNames.Square, "0"))
            .GetComponentsInChildren<Transform>()
            .Single(c => c.gameObject.name == Constants.GameObjectNames.TokenPosition10).transform.position;

        var playerToken = Instantiate(Resources.Load("Prefabs/Tokens/Token"), firstSquareTokenPosition,
            new Quaternion(0, 0, 0, 0));
        playerToken.name = "Player";
    }

    private void BuildBoard()
    {
        var boardBuilder = new ClassicBoardGameObjectBuilder(GameBoard, new SquareGameObjectGeneratorFactory(GameBoard, SquareWidth, SquareHeight), SquareWidth, SquareHeight);
        boardBuilder.BuildBoard();
    }
}