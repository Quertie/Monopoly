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
            ActivateDiceUI();
            return _tcs.Task;
        }

        private void ActivateDiceUI()
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                var enabled = GameObject.Find(Constants.GameObjectNames.UICanvas).GetComponent<Canvas>().enabled;
                if (!enabled) GameObject.Find(Constants.GameObjectNames.UICanvas).GetComponent<Canvas>().enabled = true;
            });
        }

        private void SubscribeToDiceRollSelection()
        {
            var diceRollPanel = GameObject.Find("Dice Roll Provider");
            diceRollPanel.GetComponent<UIDiceRollProviderPanel>().DiceRolled += HandleDiceRollSelected;
        }

        private void HandleDiceRollSelected(int diceRoll)
        {
            DeactivateDiceUI();
            _tcs.SetResult(diceRoll);
        }

        private void DeactivateDiceUI()
        {
            UnityMainThreadDispatcher.Instance().Enqueue(()=> GameObject.Find(Constants.GameObjectNames.UICanvas).GetComponent<Canvas>().enabled = false);
        }
    }
}