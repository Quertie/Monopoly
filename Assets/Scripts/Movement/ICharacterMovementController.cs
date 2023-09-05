using System;
using System.Threading.Tasks;

namespace Movement
{
    public interface ICharacterMovementController
    {
        public event EventHandler PlayerMovesOneSquare;
        Task MoveSquares(int numberOfSquares);
    }
}