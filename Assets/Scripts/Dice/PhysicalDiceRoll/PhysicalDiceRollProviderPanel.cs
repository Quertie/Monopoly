using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Dice.PhysicalDiceRoll
{
    public class PhysicalDiceRollProviderPanel : MonoBehaviour, IDiceRollProvider
    {
        public GameObject physicalDiceRollProvider;
        public Canvas panel;
        public Button button;

        private TaskCompletionSource<int> _tcs;

        public async Task<int> GetDiceRoll()
        {
            _tcs = new TaskCompletionSource<int>();
            await UnityMainThreadDispatcher.Instance().EnqueueAsync(() => panel.enabled = true);
            button.onClick.AddListener(HandleButtonClick);
            return await _tcs.Task;
        }

        private async void HandleButtonClick()
        {
            panel.enabled = false;
            var diceRoll = await physicalDiceRollProvider.GetComponent<PhysicalDiceRollProvider>().GetDiceRoll();
            _tcs.SetResult(diceRoll);
            button.onClick.RemoveListener(HandleButtonClick);
        }
    }
}