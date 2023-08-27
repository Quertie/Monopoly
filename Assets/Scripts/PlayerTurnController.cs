using System.Threading.Tasks;
using Dice;

public class PlayerTurnController
{
    private readonly IDiceRollProvider _diceRollProvider;
    private readonly ICharacterMovementController _characterMovementController;
    
    public PlayerTurnController(IDiceRollProvider diceRollProvider, ICharacterMovementController characterMovementController)
    {
        _diceRollProvider = diceRollProvider;
        _characterMovementController = characterMovementController;
    }

    public async Task ExecuteTurn()
    {
        var diceRoll = await _diceRollProvider.GetDiceRoll();
        await _characterMovementController.MoveSquares(diceRoll);
    }
}