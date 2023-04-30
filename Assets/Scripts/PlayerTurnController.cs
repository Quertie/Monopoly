using System.Threading.Tasks;

public class PlayerTurnController
{
    private readonly IDiceRollProvider _diceRollProvider;
    private readonly ICharacterMovementController _characterMovementController;
    private readonly IGameBoard _gameBoard;
    
    public PlayerTurnController(IDiceRollProvider diceRollProvider, ICharacterMovementController characterMovementController, IGameBoard gameBoard)
    {
        _diceRollProvider = diceRollProvider;
        _characterMovementController = characterMovementController;
        _gameBoard = gameBoard;
    }

    public async Task ExecuteTurn()
    {
        var diceRoll = _diceRollProvider.GetDiceRoll();
        var originSquare = _gameBoard.CurrentSquare;
        var destinationSquare = _gameBoard.GetLandingSquare(originSquare, diceRoll);
        await _characterMovementController.MoveToSquare(destinationSquare);
    }
}