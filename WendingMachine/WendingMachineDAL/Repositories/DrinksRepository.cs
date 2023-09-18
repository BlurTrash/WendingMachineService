using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WendingMachineDAL.EF;
using WendingMachineDAL.Entities;
using WendingMachineDAL.Repositories.Base;
using WendingMachineDAL.RepositoryInterfaces;

namespace WendingMachineDAL.Repositories
{
    public class DrinksRepository : BaseRepository<Drink, int>, IDrinksRepository
    {
        public DrinksRepository(WendingDbContext _dbContext) : base(_dbContext)
        {
        }

        public Drink Get(int id)
        {
            Drink result = _dbContext.Drinks.FirstOrDefault(x => x.Id == id && x.IsActive == true);
            return result;
        }

        public List<Drink> GetAllDrinksByMachineId(int machineId)
        {
            var drinks = _dbContext.Drinks.Where(x => x.WendingMachine.Id == machineId && x.IsActive == true).ToList();
            return drinks;
        }
    }
}
