using System.Collections.Generic;
using WendingMachineDAL.Entities.Base;

namespace WendingMachineDAL.Entities
{
    /// <summary>
    /// Описание самого аппарата
    /// </summary>
    public class WendingMachine : BaseEntity<int>
    {
        public WendingMachine()
        {
            this.Drinks = new List<Drink>();
            this.Coins = new List<Coin>();
        }
        /// <summary>
        /// Текущий баланс в автомате
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Напитки в автомате
        /// </summary>
        public virtual List<Drink> Drinks { get; set; }

        /// <summary>
        /// Монеты в автомате
        /// </summary>
        public virtual List<Coin> Coins { get; set; }
    }
}
