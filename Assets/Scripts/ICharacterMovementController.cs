using System.Threading.Tasks;

public interface ICharacterMovementController
{
    Task MoveSquares(int numberOfSquares);
}