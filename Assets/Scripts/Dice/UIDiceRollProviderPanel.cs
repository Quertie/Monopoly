using System;
using UnityEngine;

namespace Dice
{
    public class UIDiceRollProviderPanel : MonoBehaviour
    {
        public delegate void DiceRolledEventHandler(int diceRoll);
        
        public void ReturnDiceRoll(int diceRoll)
        {
            DiceRolled?.Invoke(diceRoll);
        }

        public event DiceRolledEventHandler DiceRolled ;
    }
}