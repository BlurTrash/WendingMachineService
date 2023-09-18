using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIContracts
{
    public class CoinCountDto
    {
        public string CoinValue { get; set; }
        public int CoinCount { get; set; }
        public CoinCountDto(string coinValue, int coinCount)
        {
            CoinValue = coinValue;
            CoinCount = coinCount;
        }
    }
}
