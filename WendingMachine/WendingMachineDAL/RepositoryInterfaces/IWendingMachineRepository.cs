using System;
using System.Collections.Generic;
using System.Text;
using WendingMachineDAL.Entities;
using WendingMachineDAL.RepositoryInterfaces.Base;

namespace WendingMachineDAL.RepositoryInterfaces
{
    public interface IWendingMachineRepository : IRepositoryBase<WendingMachine, int>
    {
        decimal GetBalance(int machineId);
        WendingMachine GetMachineById(int machineId);
    }
}
