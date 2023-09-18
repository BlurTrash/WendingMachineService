using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIContracts
{
    public class CreateDrinkDto
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
        public bool IsAvailable { get; set; } = true;

        /// <summary>
        /// Путь к изображению напитка
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// id вендингового аппарата
        /// </summary>
        public int MachineId { get; set; }
    }
}
