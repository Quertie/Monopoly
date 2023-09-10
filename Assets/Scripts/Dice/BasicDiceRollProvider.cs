using System.Threading.Tasks;
using UnityEngine;

namespace Dice
{
    public class BasicDiceRollProvider : IDiceRollProvider
    {
        private TaskCompletionSource<int> _tcs;
        private GameObject _diceRollPanel;
        private Canvas _uiCanvas;

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
                var uiCanvas = GetUiCanvas();
                var enabled = uiCanvas.enabled;
                if (!enabled) uiCanvas.enabled = true;
            });
        }

        private Canvas GetUiCanvas()
        {
            return _uiCanvas ??= GameObject.Find(Constants.GameObjectNames.UICanvas).GetComponent<Canvas>();
        }

        private void SubscribeToDiceRollSelection()
        {
            var diceRollPanel = _diceRollPanel ??= GameObject.Find(Constants.GameObjectNames.DiceRollProvider);
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
            UnityMainThreadDispatcher.Instance().Enqueue(()=> GetUiCanvas().enabled = false);
        }
    }
}