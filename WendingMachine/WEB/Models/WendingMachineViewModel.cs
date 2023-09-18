using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIContracts;

namespace WEB.Models
{
    public class WendingMachineViewModel
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public List<DrinkDto> Drinks { get; set; }
        public List<CoinDto> Coins { get; set; }
        public string ImageUrl { get; set; }
    }
}
