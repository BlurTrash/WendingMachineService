using System;
using System.Collections.Generic;
using System.Text;
using WendingMachineDAL.Entities.Base;

namespace WendingMachineDAL.Entities
{
    public class Coin : BaseEntity<int>
    {
        /// <summary>
        /// Номинал монеты
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Доступна ли монета
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Картинка монетки
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Кол-во монет
        /// </summary>
        public int CountCoins { get; set; }

        /// <summary>
        /// Аппарат в котором находится данный тип монет
        /// </summary>
        public virtual WendingMachine WendingMachine { get; set; }
    }
}
