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
        var boardGameObject = BuildBoard(gameBoard);
        
        var playerMovementObserver = new CharacterMovementObserver();
        var playerTokenProvider = new PlayerTokenProvider();
        var playerTurnControllers = GetPlayerTurnControllers(gameBoard, numberOfPlayers, playerMovementObserver, playerTokenProvider, boardGameObject);
        
        Task.Run(() => GameLoop(playerTurnControllers)).ConfigureAwait(false);
    }

    private GameObject BuildBoard(IGameBoard gameBoard)
    {
        var boardBuilder = new ClassicBoardGameObjectBuilder(gameBoard, new SquareGameObjectGeneratorFactory(gameBoard, SquareWidth, SquareHeight), SquareWidth, SquareHeight);
        return boardBuilder.BuildBoard();
    }
    
    private List<PlayerTurnController> GetPlayerTurnControllers(IGameBoard gameBoard,
                                                                int numberOfPlayers,
                                                                CharacterMovementObserver characterMovementObserver,
                                                                IPlayerTokenProvider playerTokenProvider,
                                                                GameObject boardGameObject)
    {
        var initialTokenPositionHelper = new InitialTokenPositionHelper(numberOfPlayers, boardGameObject);
        
        var playerTurnControllers = new List<PlayerTurnController>();

        for (var i = 0; i < numberOfPlayers; i++)
        {
            var playerIndex = i;
            var tokenGameObject = playerTokenProvider.CreatePlayerToken(playerIndex, initialTokenPositionHelper);
            var characterMovementController = new CharacterMovementController(gameBoard, playerIndex, tokenGameObject, boardGameObject);
            
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
}