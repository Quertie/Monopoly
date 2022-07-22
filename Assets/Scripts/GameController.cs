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
        print(string.Join(" - ", GameBoard.Tiles.Select(t => t.Name).ToArray()));
        BuildBoard(GameBoard);
    }

    public void BuildBoard(GameBoard gameBoard)
    {
        
    }

    void Update()
    {
        
    }
}