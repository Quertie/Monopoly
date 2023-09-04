using System;
using System.Collections.Generic;

namespace Movement
{
    internal class PlayerMovementObserver
    {
        private readonly List<ICharacterMovementSubscriber> _subscribers = new List<ICharacterMovementSubscriber>();

        public void Subscribe(ICharacterMovementSubscriber movementController)
        {
            _subscribers.Add(movementController);
        }

        public void AddSource(ICharacterMovementController characterMovementController)
        {
            characterMovementController.PlayerMovesOneSquare += HandleCharacterMovement;
        }

        private void HandleCharacterMovement(object sender, EventArgs args)
        {
            _subscribers.ForEach(s =>
            {
                if (s != sender) s.HandleCharacterMovement();
            });
        }
    }
}