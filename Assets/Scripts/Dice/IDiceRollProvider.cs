using System.Threading.Tasks;

namespace Dice
{
    public interface IDiceRollProvider
    {
        Task<int> GetDiceRoll();
    }
}