using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Models;
using WebAPIContracts;

namespace WEB.Automapper
{
    public static class AutoMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<WendingMachineDto, WendingMachineViewModel>();
                config.CreateMap<DrinkDto, DrinkViewModel>();
                config.CreateMap<DrinkViewModel, DrinkDto>();
                config.CreateMap<DrinkViewModel, CreateDrinkDto>();
                config.CreateMap<CreateDrinkDto, DrinkViewModel>();
                config.CreateMap<CoinDto, CoinViewModel>();
                config.CreateMap<CoinViewModel, CoinDto>();
            });
        }
    }
}



