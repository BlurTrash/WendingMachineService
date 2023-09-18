using System;
using System.Collections.Generic;
using System.Text;
using WendingMachineDAL.Entities;
using WendingMachineDAL.RepositoryInterfaces.Base;

namespace WendingMachineDAL.RepositoryInterfaces
{
    public interface IDrinksRepository : IRepositoryBase<Drink, int>
    {
        Drink Get(int Id);
        List<Drink> GetAllDrinksByMachineId(int machineId);
    }
}
