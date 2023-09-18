using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models
{
    public class CoinViewModel
    {
        /// <summary>
        /// id монеты
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Номинал монеты
        /// </summary>
        [Required(ErrorMessage = "Обязательное поле")]
        public int Value { get; set; }

        /// <summary>
        /// Доступна ли монета?
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Кол-во монет
        /// </summary>
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(typeof(int), "0", "200000", ErrorMessage = "Мин кол-во: 0, макс кол-во: 200000")]
        public int CountCoins { get; set; }
    }
}
