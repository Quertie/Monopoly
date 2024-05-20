using System;
using UnityEngine;

namespace Dice.PhysicalDiceRoll
{
    public class PhysicalDice : MonoBehaviour
    {
        public event EventHandler<int> DiceRollEnded;

        public void OnTriggerEnter(Collider other)
        {
            var rigidBody = GetComponent<Rigidbody>();
            if (rigidBody.velocity.magnitude < 0.4 && rigidBody.angularVelocity.magnitude < 0.4)
            {
                DiceRollEnded?.Invoke(this, DiceResult());
            }
        }

        private int DiceResult()
        {
            if (transform.up.y > 0.5) return 4;
            if (transform.up.y < -0.5) return 3;
            if (transform.forward.y > 0.5) return 5;
            if (transform.forward.y < -0.5) return 2;
            if (transform.right.y > 0.5) return 1;
            return 6;
        }
    }
}