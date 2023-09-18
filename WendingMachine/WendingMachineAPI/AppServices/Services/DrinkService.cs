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
    public class DrinkService : IDrinkService
    {

        protected readonly IDrinksRepository _drinkRepository;
        protected readonly IWendingMachineRepository _wendingMachineRepository;

        public DrinkService(IDrinksRepository drinkRepository, IWendingMachineRepository wendingMachineRepository)
        {
            _drinkRepository = drinkRepository;
            _wendingMachineRepository = wendingMachineRepository;
        }

        public DrinkDto GetDrink(int drinkId)
        {
            var drink = _drinkRepository.Get(drinkId);
            if (drink is null)
            {
                throw new ArgumentNullException($"Напиток с id={drinkId} не найден!");
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Drink, DrinkDto>()).CreateMapper();
            return mapper.Map<Drink, DrinkDto>(drink);
        }

        public DrinkDto CreateDrink(CreateDrinkDto newDrinkDto)
        {
            var machine = _wendingMachineRepository.GetMachineById((int)newDrinkDto.MachineId);
            if (machine is null)
            {
                throw new ArgumentNullException($"Автомат с id={newDrinkDto.MachineId} не найден!");
            }

            var newDrink = new Drink
            {
                Id = 0,
                Count = newDrinkDto.Count,
                IsAvailable = newDrinkDto.IsAvailable,
                ImageUrl = newDrinkDto.ImageUrl,
                Price = newDrinkDto.Price,
                Title = newDrinkDto.Title,
                WendingMachine = machine,
                IsActive = true
            };

            _drinkRepository.Create(newDrink);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Drink, DrinkDto>()).CreateMapper();
            return mapper.Map<Drink, DrinkDto>(newDrink);
        }

        public DrinkDto DeleteDrink(int drinkId)
        {
            var drink = _drinkRepository.Get(drinkId);
            if (drink is null)
            {
                throw new ArgumentNullException($"Напиток с id={drinkId} не найден!");
            }
            drink.IsActive = false;
            _drinkRepository.Update(drink);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Drink, DrinkDto>()).CreateMapper();
            return mapper.Map<Drink, DrinkDto>(drink);
        }

        public DrinkDto UpdateDrink(DrinkDto updatedrink)
        {
            var drink = _drinkRepository.Get(updatedrink.Id);
            if (drink is null)
            {
                throw new ArgumentNullException($"Напиток с id={updatedrink.Id} не найден!");
            }
            drink.ImageUrl = updatedrink.ImageUrl;
            drink.Count = updatedrink.Count;
            drink.Price = updatedrink.Price;
            _drinkRepository.Update(drink);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Drink, DrinkDto>()).CreateMapper();
            return mapper.Map<Drink, DrinkDto>(drink);
        }
    }
}
