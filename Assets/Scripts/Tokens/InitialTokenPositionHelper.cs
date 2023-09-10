using System.Linq;
using UnityEngine;

namespace Tokens
{
    public class InitialTokenPositionHelper
    {
        private readonly int _numberOfPlayers;
        private readonly GameObject _boardGameObject;

        public InitialTokenPositionHelper(int numberOfPlayers, GameObject boardGameObject)
        {
            _boardGameObject = boardGameObject;
            _numberOfPlayers = numberOfPlayers;
        }

        public Vector3 GetFirstSquareTokenPosition(int playerNumber)
        {
            var firstSquareTokenPosition = _boardGameObject.transform.GetChild(0).gameObject
                .GetComponentsInChildren<Transform>()
                .Single(c =>
                {
                    var tokenPosition10 = string.Format(Constants.GameObjectNames.TokenPosition, _numberOfPlayers, playerNumber);
                    return c.gameObject.name == tokenPosition10;
                }).transform.position;
            return firstSquareTokenPosition;
        }
    }
}