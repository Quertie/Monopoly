using System.Threading.Tasks;
using Squares;

public interface ICharacterMovementController
{
    Task MoveToSquare(Square square);
}