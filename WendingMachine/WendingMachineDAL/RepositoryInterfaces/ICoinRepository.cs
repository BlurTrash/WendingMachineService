using System;
using System.Collections.Generic;
using System.Text;
using WendingMachineDAL.Entities;
using WendingMachineDAL.RepositoryInterfaces.Base;

namespace WendingMachineDAL.RepositoryInterfaces
{
    public interface ICoinRepository : IRepositoryBase<Coin, int>
    {
        public Coin Get(int Id);
        public List<Coin> GetAllCoinsByMachineId(int machineId);
    }
}
