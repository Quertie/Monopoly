using Boards.Classic;
using Boards.Classic.GameBoardCreation;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameBoard GameBoard {get; set;}

    private void Start()
    {
        GameBoard = new GameBoard();
        BuildBoard();
    }

    private void BuildBoard()
    {
        var boardBuilder = new BoardBuilder(GameBoard, new SquareGameObjectGeneratorFactory(GameBoard, 4f, 6.5f));
        boardBuilder.BuildBoard();
    }
}