using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameBoard GameBoard {get; set;}

    void Start()
    {
        GameBoard = new GameBoard();
        BuildBoard(GameBoard);
    }

    public void BuildBoard(GameBoard gameBoard)
    {
        var boardBuilder = new BoardBuilder(GameBoard, new SquareImageGenerator());
        boardBuilder.BuildBoard();
    }

    void Update()
    {
        
    }
}