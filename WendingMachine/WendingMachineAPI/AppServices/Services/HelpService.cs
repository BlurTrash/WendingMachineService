using System;
using System.Collections.Generic;
using AutoMapper;
using WebAPIContracts;
using WendingMachineAPI.AppServices.Interfaces;
using WendingMachineDAL.Entities;
using WendingMachineDAL.RepositoryInterfaces;

namespace WendingMachineAPI.AppServices.Services
{
    public class HelpService : IHelpService
    {
        protected readonly ICoinRepository _coinRepository;
        protected readonly IServiceProvider _serviceProvider;

        public HelpService(ICoinRepository coinRepository, IServiceProvider serviceProvider)
        {
            _coinRepository = coinRepository;
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<CoinDto> GetAllCoinsByMachineId(int machineId)
        {
            var storage = _coinRepository.GetAllCoinsByMachineId(machineId);
            if (storage is null)
            {
                throw new ArgumentNullException($"Автомат с id={machineId} не найден!");
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Coin, CoinDto>()).CreateMapper();
            return mapper.Map<IEnumerable<Coin>, List<CoinDto>>(storage);
        }

        public void AddCoins(int coinId, int coinsCoint)
        {
            var coin = _coinRepository.Get(coinId);
            if (coin is null)
            {
                throw new ArgumentNullException($"Монета с id={coinId} не найдена!");
            }
            coin.CountCoins += coinsCoint;
            _coinRepository.Update(coin);
        }

        public CoinDto GetCoin(int coinId)
        {
            var coin = _coinRepository.Get(coinId);
            if (coin is null)
            {
                throw new ArgumentNullException($"Монета с id={coinId} не найден!");
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Coin, CoinDto>()).CreateMapper();
            return mapper.Map<Coin, CoinDto>(coin);
        }

        public CoinDto UpdateCoin(CoinDto updateCoin)
        {
            var coin = _coinRepository.Get(updateCoin.Id);
            if (coin is null)
            {
                throw new ArgumentNullException($"Монета с id={updateCoin.Id} не найден!");
            }
            coin.IsAvailable = updateCoin.IsAvailable;
            coin.CountCoins = updateCoin.CountCoins;
            _coinRepository.Update(coin);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Coin, CoinDto>()).CreateMapper();
            return mapper.Map<Coin, CoinDto>(coin);
        }
    }
}
