using UnityEngine;

namespace Tokens
{
    public class PlayerTokenProvider : IPlayerTokenProvider
    {
        public GameObject CreatePlayerToken(int playerNumber, InitialTokenPositionHelper initialTokenPositionHelper)
        {
            var firstSquareTokenPosition = initialTokenPositionHelper.GetFirstSquareTokenPosition(playerNumber);

            var playerToken = Object.Instantiate(Resources.Load($"Prefabs/Tokens/Token {playerNumber}"), firstSquareTokenPosition,
                new Quaternion(0, 0, 0, 0));
            playerToken.name = string.Format(Constants.GameObjectNames.PlayerToken, playerNumber);
            return (GameObject)playerToken;
        }
    }
}