using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIContracts
{
    public class AddBalanceDto
    {
        /// <summary>
        /// сколько добавить
        /// </summary>
        public int Cash { get; set; }

        /// <summary>
        /// id автомата
        /// </summary>
        public int MachineId { get; set; }
    }
}
