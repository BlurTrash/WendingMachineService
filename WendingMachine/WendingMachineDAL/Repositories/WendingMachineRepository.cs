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
    public class WendingMachineRepository : BaseRepository<WendingMachine, int>, IWendingMachineRepository
    {
        public WendingMachineRepository(WendingDbContext _dbContext) : base(_dbContext)
        {
        }

        public WendingMachine GetMachineById(int machineId)
        {
            var machine = _dbContext.WendingMachine.FirstOrDefault(x => x.Id == machineId && x.IsActive == true);
            return machine;
        }

        public decimal GetBalance(int machineId)
        {
            var machine = GetMachineById(machineId);
            return machine.Balance;
        }
    }
}
