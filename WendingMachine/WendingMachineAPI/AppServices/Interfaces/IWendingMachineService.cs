using System.Collections.Generic;
using WebAPIContracts;

namespace WendingMachineAPI.AppServices.Interfaces
{
    public interface IWendingMachineService
    {
        WendingMachineDto GetMachineById(int machineId);
        decimal AddBalance(AddBalanceDto balance);
        TipsDto TakeTips(int machineId);
        DrinkDto OrderDrink(OrderDrinkDto order);
        IEnumerable<DrinkDto> GetAvailableDrink(int machineId);
        IEnumerable<DrinkDto> GetAllDrinks(int machineId);
       
        //WendingMachineDto GetMachineByBalance(decimal balance);
        //WendingMachineDto GetMachineBy();


        //decimal SubBalance(decimal sub);
        //void AddDrinkToMachine(int machineId, int drinkId, int count);
        //void CreateDrink(int machineId, DrinkDto drink);

        //void MakeNotAvailableDrink(int id);
        //void MakeAvailableDrink(int id);
    }
}
