using System.Linq;
using UnityEngine;

namespace Tokens
{
    public class PlayerTokenProvider : IPlayerTokenProvider
    {
        public GameObject CreatePlayerToken(int playerNumber, int numberOfPlayers)
        {
            var firstSquareTokenPosition = GameObject.Find(string.Format(Constants.GameObjectNames.Square, "0"))
                .GetComponentsInChildren<Transform>()
                .Single(c =>
                {
                    var tokenPosition10 = string.Format(Constants.GameObjectNames.TokenPosition, numberOfPlayers, playerNumber);
                    return c.gameObject.name == tokenPosition10;
                }).transform.position;

            var playerToken = Object.Instantiate(Resources.Load($"Prefabs/Tokens/Token {playerNumber}"), firstSquareTokenPosition,
                new Quaternion(0, 0, 0, 0));
            playerToken.name = string.Format(Constants.GameObjectNames.PlayerToken, playerNumber);
            return (GameObject)playerToken;
        }
    }
}