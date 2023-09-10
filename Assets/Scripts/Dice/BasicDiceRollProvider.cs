using System.Threading.Tasks;
using UnityEngine;

namespace Dice
{
    public class BasicDiceRollProvider : IDiceRollProvider
    {

        private TaskCompletionSource<int> _tcs;
        private GameObject _diceRollPanel;

        public Task<int> GetDiceRoll()
        {
            _tcs = new TaskCompletionSource<int>();
            UnityMainThreadDispatcher.Instance().Enqueue(SubscribeToDiceRollSelection);
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
            var diceRollPanel = _diceRollPanel ??= GameObject.Find("Dice Roll Provider");
            diceRollPanel.GetComponent<UIDiceRollProviderPanel>().DiceRolled += HandleDiceRollSelected;
        }

        private void HandleDiceRollSelected(int diceRoll)
        {
            DeactivateDiceUI();
            UnityMainThreadDispatcher.Instance().Enqueue(RemoveDiceRollSelectionHandler);
            _tcs.SetResult(diceRoll);
        }
        
        private void RemoveDiceRollSelectionHandler()
        {
            _diceRollPanel.GetComponent<UIDiceRollProviderPanel>().DiceRolled -= HandleDiceRollSelected;
        }

        private void DeactivateDiceUI()
        {
            UnityMainThreadDispatcher.Instance().Enqueue(()=> GameObject.Find(Constants.GameObjectNames.UICanvas).GetComponent<Canvas>().enabled = false);
        }
    }
}