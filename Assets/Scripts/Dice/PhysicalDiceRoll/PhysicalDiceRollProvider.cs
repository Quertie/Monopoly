using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace Dice.PhysicalDiceRoll
{
    public class PhysicalDiceRollProvider : MonoBehaviour, IDiceRollProvider
    {
        public float distanceBetweenDice;
        
        private TaskCompletionSource<int> _tcs1;
        private TaskCompletionSource<int> _tcs2;

        private void HandleDiceRoll1(object sender, int diceRoll)
        {
            _tcs1.SetResult(diceRoll);
            ((PhysicalDice)sender).DiceRollEnded -= HandleDiceRoll1;
        }
        private void HandleDiceRoll2(object sender, int diceRoll)
        {
            _tcs2.SetResult(diceRoll);
            ((PhysicalDice)sender).DiceRollEnded -= HandleDiceRoll2;
        }

        public async Task<int> GetDiceRoll()
        {
            _tcs1 = new TaskCompletionSource<int>();
            _tcs2 = new TaskCompletionSource<int>();
            UnityMainThreadDispatcher.Instance().Enqueue(InstantiateDice);
            await Task.WhenAll(_tcs1.Task, _tcs2.Task);
            return _tcs1.Task.Result + _tcs2.Task.Result;
        }

        private void InstantiateDice()
        {
            var existingDices = GameObject.FindGameObjectsWithTag("Dice");
            foreach (var existingDice in existingDices)
            {
                Destroy(existingDice);
            }
            var dice1 = CreateDiceGameObject(-distanceBetweenDice/2);
            var dice2 = CreateDiceGameObject(distanceBetweenDice / 2);
            dice1.GetComponent<PhysicalDice>().DiceRollEnded += HandleDiceRoll1;
            dice2.GetComponent<PhysicalDice>().DiceRollEnded += HandleDiceRoll2;
            var random = new Random();
            foreach (var dice in new[] { dice1, dice2 })
            {
                dice.transform.eulerAngles = new Vector3((float)(random.NextDouble() * 360),
                    (float)(random.NextDouble() * 360),
                    (float)(random.NextDouble() * 360));
                dice.GetComponent<Rigidbody>().AddForce(-400 - (float)random.NextDouble() * 600, 0, 0);
            }
        }

        private GameObject CreateDiceGameObject(float offset)
        {
            return (GameObject)Instantiate(Resources.Load("Prefabs/Dice/Dice"),
                transform.position + new Vector3(0,0,offset),
                new Quaternion(0, 0, 0, 0));
        }
    }
}