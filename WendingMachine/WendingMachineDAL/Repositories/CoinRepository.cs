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
    public class CoinRepository : BaseRepository<Coin, int>, ICoinRepository
    {
        public CoinRepository(WendingDbContext _dbContext) : base(_dbContext)
        {
        }

        public Coin Get(int id)
        {
            Coin result = _dbContext.Coins.FirstOrDefaultAsync(x => x.Id == id && x.IsActive == true).Result;
            return result;
        }

        public List<Coin> GetAllCoinsByMachineId(int machineId)
        {
            var coins = _dbContext.Coins.Where(x => x.WendingMachine.Id == machineId && x.IsActive == true).ToList();
            return coins;
        }
    }
}
