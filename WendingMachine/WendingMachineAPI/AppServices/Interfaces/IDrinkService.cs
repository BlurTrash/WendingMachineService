using System.Collections.Generic;
using WebAPIContracts;

namespace WendingMachineAPI.AppServices.Interfaces
{
    public interface IDrinkService
    {
        DrinkDto GetDrink(int drinkId);
        DrinkDto CreateDrink(CreateDrinkDto newDrinkDto);
        DrinkDto DeleteDrink(int drinkDtoId);
        public DrinkDto UpdateDrink(DrinkDto updatedrink);
    }
}
