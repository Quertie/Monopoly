using Boards.Classic;
using Boards.Classic.GameBoardCreation;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const float SquareWidth = 4f;
    private const float SquareHeight = 6.5f;
    
    private GameBoard GameBoard {get; set;}
    
    private void Start()
    {
        GameBoard = new GameBoard();
        BuildBoard();
    }

    private void BuildBoard()
    {
        var boardBuilder = new BoardBuilder(GameBoard, new SquareGameObjectGeneratorFactory(GameBoard, SquareWidth, SquareHeight), SquareWidth, SquareHeight);
        boardBuilder.BuildBoard();
    }
}