using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.Classic;
using Boards.Classic.GameBoardGameObjectCreation;
using Dice;
using Movement;
using Tokens;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const float SquareWidth = 4f;
    private const float SquareHeight = 6.5f;

    private void Start()
    {
        var numberOfPlayers = 4;

        var gameBoardProvider = new ClassicGameBoardProvider(numberOfPlayers);
        var gameBoard = gameBoardProvider.GetBoard();
        BuildBoard(gameBoard);
        
        var playerMovementObserver = new CharacterMovementObserver();
        var playerTokenProvider = new PlayerTokenProvider();
        var playerTurnControllers = GetPlayerTurnControllers(gameBoard, numberOfPlayers, playerMovementObserver, playerTokenProvider);
        
        Task.Run(() => GameLoop(playerTurnControllers)).ConfigureAwait(false);
    }

    private List<PlayerTurnController> GetPlayerTurnControllers(IGameBoard gameBoard,
                                                                int numberOfPlayers,
                                                                CharacterMovementObserver characterMovementObserver,
                                                                IPlayerTokenProvider playerTokenProvider)
    {
        var playerTurnControllers = new List<PlayerTurnController>();

        for (var i = 0; i < numberOfPlayers; i++)
        {
            var playerIndex = i;
            var tokenGameObject = playerTokenProvider.CreatePlayerToken(playerIndex, numberOfPlayers);
            var characterMovementController = new CharacterMovementController(gameBoard, playerIndex, tokenGameObject);
            
            characterMovementObserver.AddSource(characterMovementController);
            characterMovementObserver.Subscribe(characterMovementController);
            
            playerTurnControllers.Add(new PlayerTurnController(new BasicDiceRollProvider(), characterMovementController));
        }

        return playerTurnControllers;
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

    private void BuildBoard(IGameBoard gameBoard)
    {
        var boardBuilder = new ClassicBoardGameObjectBuilder(gameBoard, new SquareGameObjectGeneratorFactory(gameBoard, SquareWidth, SquareHeight), SquareWidth, SquareHeight);
        boardBuilder.BuildBoard();
    }
}