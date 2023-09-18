using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models
{
    public class DrinkViewModel
    {
        ///<summary>
        /// id напитка
        /// </summary>
        public int Id { get; set; }

        ///<summary>
        /// Название напитка
        /// </summary>
        [Required(ErrorMessage = "Введите название напитка")]
        public string Title { get; set; }

        ///<summary>
        /// Цена напитка
        /// </summary>"[^0-9]+"
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(typeof(int), "1", "200000", ErrorMessage ="Цена должна быть целочисленной, от 1 до 200000")]
        public int Price { get; set; }

        ///<summary>
        /// Количество
        /// </summary>
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(typeof(int), "0", "2000000",  ErrorMessage = "Кол-во должно быть целочисленной, от 0 до 200000")]
        public int Count { get; set; }

        /// <summary>
        /// Путь к изображению напитка
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
