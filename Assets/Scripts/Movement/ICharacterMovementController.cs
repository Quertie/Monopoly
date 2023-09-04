using System;
using System.Threading.Tasks;

namespace Movement
{
    public interface ICharacterMovementController
    {
        public delegate void PlayerMovesOneSquareHandler(int playerNumber);
        public event EventHandler PlayerMovesOneSquare; 
        Task UpdatePosition();
        Task MoveSquares(int numberOfSquares);
    }
}