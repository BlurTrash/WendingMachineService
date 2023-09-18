using System.Collections.Generic;
using WebAPIContracts;

namespace WendingMachineAPI.AppServices.Interfaces
{
    public interface IHelpService
    {
        void AddCoins(int coinId, int coinsCoint);
        IEnumerable<CoinDto> GetAllCoinsByMachineId(int machineId);
        CoinDto GetCoin(int coinId);
        CoinDto UpdateCoin(CoinDto updateCoin);
    }
}
