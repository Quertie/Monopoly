using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boards.Classic;
using Boards.Classic.GameBoardGameObjectCreation;
using Dice;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const float SquareWidth = 4f;
    private const float SquareHeight = 6.5f;
    
    private IGameBoard GameBoard {get; set;}

    private event EventHandler PlayerMovesOneSquare;

    private void Start()
    {
        var numberOfPlayers = 4;

        var gameBoardProvider = new ClassicGameBoardProvider(numberOfPlayers);
        GameBoard = gameBoardProvider.GetBoard();
        BuildBoard();
        var playerTurnControllers = GetPlayerTurnControllers(numberOfPlayers);
        
        Task.Run(() => GameLoop(playerTurnControllers)).ConfigureAwait(false);
    }

    private List<PlayerTurnController> GetPlayerTurnControllers(int numberOfPlayers)
    {
        var playerTurnControllers = new List<PlayerTurnController>();

        for (var i = 0; i < numberOfPlayers; i++)
        {
            var playerIndex = i;
            CreatePlayerToken(playerIndex, numberOfPlayers);
            var characterMovementController = new CharacterMovementController(GameBoard, playerIndex);
            characterMovementController.PlayerMovesOneSquare += HandlePlayerMovesOneSquare;
            PlayerMovesOneSquare += (sender, e) =>
                {
                    if ((ICharacterMovementController)sender != characterMovementController)
                        Task.Run(() => characterMovementController.UpdatePosition()).ConfigureAwait(false);
                }
            ;
            playerTurnControllers.Add(new PlayerTurnController(new BasicDiceRollProvider(), characterMovementController));
        }

        return playerTurnControllers;
    }

    private void HandlePlayerMovesOneSquare(object sender, EventArgs eventArgs)
    {
        PlayerMovesOneSquare?.Invoke(sender, eventArgs);
    }

    private async Task GameLoop(List<PlayerTurnController> playerTurnControllers)
    {
        while (true)
        {
            foreach (var playerTurnController in playerTurnControllers)
            {
                await playerTurnController.ExecuteTurn();
            }
        }
    }

    private static void CreatePlayerToken(int playerNumber, int numberOfPlayers)
    {
        var firstSquareTokenPosition = GameObject.Find(string.Format(Constants.GameObjectNames.Square, "0"))
            .GetComponentsInChildren<Transform>()
            .Single(c =>
            {
                var tokenPosition10 = string.Format(Constants.GameObjectNames.TokenPosition, numberOfPlayers, playerNumber);
                return c.gameObject.name == tokenPosition10;
            }).transform.position;

        var playerToken = Instantiate(Resources.Load("Prefabs/Tokens/Token"), firstSquareTokenPosition,
            new Quaternion(0, 0, 0, 0));
        playerToken.name = $"Player {playerNumber}";
    }

    private void BuildBoard()
    {
        var boardBuilder = new ClassicBoardGameObjectBuilder(GameBoard, new SquareGameObjectGeneratorFactory(GameBoard, SquareWidth, SquareHeight), SquareWidth, SquareHeight);
        boardBuilder.BuildBoard();
    }
}