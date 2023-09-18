using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIContracts
{
    public class TipsDto
    {
        public List<CoinCountDto> CoinsCountList { get; set; }
        public decimal Tips { get; set; }

        public TipsDto()
        {
            CoinsCountList = new List<CoinCountDto>();
        }
    }
}
