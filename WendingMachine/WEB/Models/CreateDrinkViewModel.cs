using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models
{
    public class CreateDrinkViewModel
    {
        ///<summary>
        /// Название напитка
        /// </summary>
        [Required(ErrorMessage = "Введите название напитка")]
        public string Title { get; set; }

        ///<summary>
        /// Цена напитка
        /// </summary>"[^0-9]+"
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(typeof(int), "1", "200000", ErrorMessage = "Цена должна быть целочисленной, от 1 до 200000")]
        public int Price { get; set; }

        ///<summary>
        /// Количество
        /// </summary>
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(typeof(int), "0", "200000", ErrorMessage = "Мин кол-во: 0, макс кол-во: 200000")]
        public int Count { get; set; }

        public bool IsAvailable { get; set; }

        /// <summary>
        /// Изображение
        /// </summary>
        [Required(ErrorMessage = "Выберите фото")]
        public IFormFile File  { get; set; }
    }
}
