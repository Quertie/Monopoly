using System.Threading.Tasks;
using UnityEngine;

namespace Dice
{
    public class BasicDiceRollProvider : IDiceRollProvider
    {

        public BasicDiceRollProvider()
        {
            UnityMainThreadDispatcher.Instance().Enqueue(SubscribeToDiceRollSelection);
        }

        private TaskCompletionSource<int> _tcs;

        public Task<int> GetDiceRoll()
        {
            _tcs = new TaskCompletionSource<int>();
            return _tcs.Task;
        }

        private void SubscribeToDiceRollSelection()
        {
            var diceRollPanel = GameObject.Find("Dice Roll Provider");
            diceRollPanel.GetComponent<UIDiceRollProviderPanel>().DiceRolled += HandleDiceRollSelected;
        }

        private void HandleDiceRollSelected(int diceRoll)
        {
            _tcs.SetResult(diceRoll);
        }
    }
}