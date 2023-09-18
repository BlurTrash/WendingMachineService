using System;
using System.Collections.Generic;
using System.Text;
using WendingMachineDAL.Entities.Base;

namespace WendingMachineDAL.Entities
{
    /// <summary>
    /// Напиток
    /// </summary>
    public class Drink : BaseEntity<int>
    {
        ///<summary>
        /// Название напитка
        /// </summary>
        public string Title { get; set; }

        ///<summary>
        /// Цена напитка
        /// </summary>
        public decimal Price { get; set; }

        ///<summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }

        ///<summary>
        /// Доступен ли напиток
        /// </summary>
        public bool IsAvailable { get; set; }
        /// <summary>
        /// Путь к изображению напитка
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// В каком вендинговом аппарате находится напиток (если будет несколько аппаратов в будущем)
        /// </summary>
        public virtual WendingMachine WendingMachine { get; set; }
    }
}
