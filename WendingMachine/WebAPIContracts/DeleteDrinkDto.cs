using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIContracts
{
    public class DeleteDrinkDto
    {
        /// <summary>
        /// id напитка
        /// </summary>
        public int DrinkId { get; set; }

        /// <summary>
        /// id автомата
        /// </summary>
        public int MachineId { get; set; }
    }
}
