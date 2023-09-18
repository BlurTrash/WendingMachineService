using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIContracts.Base;

namespace WebAPIContracts
{
    public class WendingMachineDto : BaseEntityDto<int>
    {
        /// <summary>
        /// Текущий баланс в автомате
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Состояние репозитория напитков
        /// </summary>
        public List<DrinkDto> Drinks { get; set; }

        /// <summary>
        /// Состояние репозитория монет
        /// </summary>
        public List<CoinDto> Coins { get; set; }
    }
}
