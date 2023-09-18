using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPIContracts;
using WendingMachineAPI.AppServices.Interfaces;
using WendingMachineDAL.Entities;
using WendingMachineDAL.RepositoryInterfaces;

namespace WendingMachineAPI.AppServices.Services
{
    public class WendingMachineService : IWendingMachineService
    {
        private readonly IWendingMachineRepository _wendingMachineRepository;
        private readonly IDrinksRepository _drinksRepository;
        private readonly ICoinRepository _coinsRepository;
        private readonly IDrinkService _drinkService;
        private readonly IHelpService _helpService;
        public WendingMachineService(IWendingMachineRepository wendingMachineRepository, IDrinksRepository drinksRepository, ICoinRepository coinsRepository, IDrinkService drinkService, IHelpService helpService)
        {
            _wendingMachineRepository = wendingMachineRepository;
            _drinksRepository = drinksRepository;
            _coinsRepository = coinsRepository;
            _drinkService = drinkService;
            _helpService = helpService;
        }
       
        public WendingMachineDto GetMachineById(int machineId)
        {
            //WendingMachine machine = _wendingMachineRepository.GetMachineById(machineId);
            WendingMachine machine = GetMachineWithRelations(machineId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<WendingMachine, WendingMachineDto>()).CreateMapper();
            var machineDto = mapper.Map<WendingMachine, WendingMachineDto>(machine);
            return machineDto;
        }

        public IEnumerable<DrinkDto> GetAvailableDrink(int machineId)
        {
            //WendingMachine machine = _wendingMachineRepository.GetMachineById(machineId);
            WendingMachine machine = GetMachineWithRelations(machineId);
            var avalibleDrinks = machine.Drinks.Where(d => d.IsAvailable == true && d.Count > 0).ToList();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Drink, DrinkDto>()).CreateMapper();
            return mapper.Map<List<Drink>, IEnumerable<DrinkDto>>(avalibleDrinks);
        }

        public IEnumerable<DrinkDto> GetAllDrinks(int machineId)
        {
            var machine = _wendingMachineRepository.GetMachineById(machineId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Drink, DrinkDto>()).CreateMapper();
            return mapper.Map<List<Drink>, IEnumerable<DrinkDto>>(machine.Drinks);
        }

        public decimal AddBalance(AddBalanceDto balance)
        {
            WendingMachine machine = GetMachineWithRelations(balance.MachineId);

            if (balance.Cash > 0)
            {
                var coin = machine.Coins.FirstOrDefault(c => c.Value == balance.Cash);
                _helpService.AddCoins(coin.Id, 1);
                machine.Balance += balance.Cash;
                _wendingMachineRepository.Update(machine);
            }
            return machine.Balance;
        }

        public TipsDto TakeTips(int machineId)
        {
            WendingMachine machine = GetMachineWithRelations(machineId);

            //Метод вычисления сдачи по монетам, монеты в автомате уменьшаются
            var tips = CalculationTips(ref machine);
            machine.Balance = 0;

            //var tips = machine.Balance;
            //machine.Balance = 0;

            _wendingMachineRepository.Update(machine);
            return tips;
        }

        public DrinkDto OrderDrink(OrderDrinkDto order)
        {
            WendingMachine machine = GetMachineWithRelations(order.MachineId);

            var drink = machine.Drinks.FirstOrDefault(x => x.Id == order.DrinkId);
            if (drink is null)
            {
                throw new ArgumentNullException($"Напиток с id={order.DrinkId} не найден!");
            }
            if (machine.Balance < drink.Price)
            {
                throw new ArithmeticException($"На автомате с id={order.MachineId} недостаточно средств!");
            }

            if (drink.Count <= 0)
            {
                throw new ArgumentNullException($"Кол-во напитка {drink.Title} автомате меньше 1шт!");
            }

            if (!drink.IsAvailable)
            {
                throw new ArgumentNullException($"Напиток недоступен для покупки!");
            }
            drink.Count--;
            machine.Balance -= drink.Price;
            _wendingMachineRepository.Update(machine);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Drink, DrinkDto>()).CreateMapper();
            return mapper.Map<Drink, DrinkDto>(drink);
        }
   
        private WendingMachine GetMachineWithRelations(int machineId)
        {
            WendingMachine machine = _wendingMachineRepository.GetMachineById(machineId);
            if (machine is null)
            {
                throw new ArgumentNullException($"Автомат с id={machineId} не найден!");
            }
            machine.Coins = _coinsRepository.GetAllCoinsByMachineId(machineId);
            machine.Drinks = _drinksRepository.GetAllDrinksByMachineId(machineId);
            return machine;
        }

        private TipsDto CalculationTips(ref WendingMachine machine)
        {
            var coinsList = machine.Coins;
            TipsDto tipsModel = new TipsDto();
            decimal tempBalance = machine.Balance;
            decimal tips = 0;
            try
            {
                if (tempBalance > 0)
                {
                    var coinTen = coinsList.FirstOrDefault(c => c.Value == 10);
                    if (coinTen != null)
                    {
                        int coinTenCount = 0;
                        while (tempBalance >= coinTen.Value && coinTen.CountCoins > 0)
                        {
                            tempBalance -= coinTen.Value;
                            coinTen.CountCoins--;
                            tips += coinTen.Value;
                            coinTenCount++;
                        }
                        if (coinTenCount != 0)
                        {
                            tipsModel.CoinsCountList.Add(new CoinCountDto(coinTen.Value.ToString(), coinTenCount));
                        }
                    }

                    var coinFive = coinsList.FirstOrDefault(c => c.Value == 5);
                    if (coinFive != null)
                    {
                        int coinFiveCount = 0;
                        while (tempBalance >= coinFive.Value && coinFive.CountCoins > 0)
                        {
                            tempBalance -= coinFive.Value;
                            coinFive.CountCoins--;
                            tips += coinFive.Value;
                            coinFiveCount++;
                        }
                        if (coinFiveCount != 0)
                        {
                            tipsModel.CoinsCountList.Add(new CoinCountDto(coinFive.Value.ToString(), coinFiveCount));
                        }
                    }

                    var coinTwo = coinsList.FirstOrDefault(c => c.Value == 2);
                    if (coinTwo != null)
                    {
                        int coinTwoCount = 0;
                        while (tempBalance >= coinTwo.Value && coinTwo.CountCoins > 0)
                        {
                            tempBalance -= coinTwo.Value;
                            coinTwo.CountCoins--;
                            tips += coinTwo.Value;
                            coinTwoCount++;
                        }
                        if (coinTwoCount != 0)
                        {
                            tipsModel.CoinsCountList.Add(new CoinCountDto(coinTwo.Value.ToString(), coinTwoCount));
                        }
                    }

                    var coinOne = coinsList.FirstOrDefault(c => c.Value == 1);
                    if (coinOne != null)
                    {
                        int coinOneCount = 0;
                        while (tempBalance >= coinOne.Value && coinOne.CountCoins > 0)
                        {
                            tempBalance -= coinOne.Value;
                            coinOne.CountCoins--;
                            tips += coinOne.Value;
                            coinOneCount++;
                        }
                        if (coinOneCount != 0)
                        {
                            tipsModel.CoinsCountList.Add(new CoinCountDto(coinOne.Value.ToString(), coinOneCount));
                        }
                    }

                }
            }
            catch (Exception)
            {
                throw new Exception("Ошибка вычесления сдачи!");
            }

            tipsModel.Tips = tips;
            return tipsModel;
        }
        //-----------

    }
}
