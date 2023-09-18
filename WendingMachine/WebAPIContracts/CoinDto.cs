using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIContracts.Base;

namespace WebAPIContracts
{
    public class CoinDto : BaseEntityDto<int>
    {
        /// <summary>
        /// Номинал монеты
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Доступна ли монета?
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Изображение монетки
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Кол-во монет
        /// </summary>
        public int CountCoins { get; set; }
    }
}
