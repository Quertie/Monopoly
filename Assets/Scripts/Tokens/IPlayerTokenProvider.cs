using UnityEngine;

namespace Tokens
{
    public interface IPlayerTokenProvider
    {
        GameObject CreatePlayerToken(int playerNumber, InitialTokenPositionHelper initialTokenPositionHelper);
    }
}