using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Dice.PhysicalDiceRoll
{
    public class PhysicalDiceRollProvider : MonoBehaviour, IDiceRollProvider
    {
        public float distanceBetweenDice;
        public GameObject dicePrefab;
        
        private GameObject _dice1;
        private GameObject _dice2;
        
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
            await UnityMainThreadDispatcher.Instance().EnqueueAsync(ResetDice);
            await Task.WhenAll(_tcs1.Task, _tcs2.Task);
            return _tcs1.Task.Result + _tcs2.Task.Result;
        }

        private void ResetDice()
        {
            ResetDicePosition();
            AddDiceHandlers();
            StartDicePhysics();
        }

        private void ResetDicePosition()
        {
            var position = transform.position;
            var existingDices = GameObject.FindGameObjectsWithTag("Dice");
            if (!existingDices.Any()) InstantiateDices(position);
            
            _dice1.transform.position = position + new Vector3(0, 0, -distanceBetweenDice / 2);
            _dice1.transform.rotation = new Quaternion(0, 0, 0, 0);
            _dice2.transform.position = position + new Vector3(0, 0, distanceBetweenDice / 2);
            _dice2.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        private void InstantiateDices(Vector3 position)
        {
            var rotation = new Quaternion(0, 0, 0, 0);
            _dice1 = Instantiate(dicePrefab,
                position,
                rotation);
            _dice2 = Instantiate(dicePrefab,
                position,
                rotation);
        }
        
        private void AddDiceHandlers()
        {
            _dice1.GetComponent<PhysicalDice>().DiceRollEnded += HandleDiceRoll1;
            _dice2.GetComponent<PhysicalDice>().DiceRollEnded += HandleDiceRoll2;
        }
        
        private void StartDicePhysics()
        {
            var random = new Random();
            foreach (var dice in new[] { _dice1, _dice2 })
            {
                dice.transform.eulerAngles = new Vector3((float)(random.NextDouble() * 360),
                    (float)(random.NextDouble() * 360),
                    (float)(random.NextDouble() * 360));
                dice.GetComponent<Rigidbody>().AddForce(-400 - (float)random.NextDouble() * 600, 0, 0);
            }
        }
    }
}