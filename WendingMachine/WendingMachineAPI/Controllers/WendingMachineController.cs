using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIContracts;
using WendingMachineAPI.AppServices.Interfaces;

namespace WendingMachineAPI.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WendingMachineController : ControllerBase
    {
        private readonly IDrinkService _drinkService;
        private readonly IWendingMachineService _wendingMachineService;
        private readonly IHelpService _helpService;

        public WendingMachineController(IDrinkService drinkService, IWendingMachineService wendingMachineService, IHelpService helpService)
        {
            _drinkService = drinkService;
            _wendingMachineService = wendingMachineService;
            _helpService = helpService;
        }

        [HttpGet("{machineId}")]      
        public async Task<ActionResult<IEnumerable<CoinDto>>> GetCoinsByMachineId(int machineId)
        {
            try
            {
                var coins = _helpService.GetAllCoinsByMachineId(machineId);
                return Ok(coins);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<WendingMachineDto>>GetMachine(int id)
        {
            try
            {
                var machine = _wendingMachineService.GetMachineById(id);
                return Ok(machine);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<decimal>> AddBalanceCoin([FromBody]AddBalanceDto balance)
        {
            try
            {
                if (balance.Cash < 0)
                {
                    throw new ArgumentNullException("Сумма добавляемого баланса не может быть отрицательной!");
                }
                decimal newBalance = _wendingMachineService.AddBalance(balance);
                return Ok(newBalance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{machineId}")]
        public async Task<ActionResult<TipsDto>>TakeTips(int machineId)
        {
            try
            {
                var tips = _wendingMachineService.TakeTips(machineId);
                return Ok(tips);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult<DrinkDto>> OrderDrink([FromBody]OrderDrinkDto order)
        {
            try
            {
                var drink = _wendingMachineService.OrderDrink(order);
                return Ok(drink);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Из Свагера работает, при обращении с клиент через httpClient.DeleteAsync(url) c параметром - почему то не работает и в апи-метод не заходит
        //[HttpDelete("{id}")] 
        //public async Task<ActionResult<DrinkDto>> DeleteDrink(int id)
        //{
        //    try
        //    {
        //        var drink = _drinkService.DeleteDrink(deleteModel.DrinkId);
        //        return Ok(drink);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPut]
        public async Task<ActionResult<DrinkDto>> DeleteDrink([FromBody] DeleteDrinkDto deleteModel)
        {
            try
            {
                var drink = _drinkService.DeleteDrink(deleteModel.DrinkId);
                return Ok(drink);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{drinkId}")]
        public async Task<ActionResult<DrinkDto>> GetDrink(int drinkId)
        {
            try
            {
                var drink = _drinkService.GetDrink(drinkId);
                return Ok(drink);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<DrinkDto>> UpdateDrink([FromBody] DrinkDto updateDrink)
        {
            try
            {
                var drink = _drinkService.UpdateDrink(updateDrink);
                return Ok(drink);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DrinkDto>> CreateDrink([FromBody] CreateDrinkDto newDrink)
        {
            try
            {
                var drink = _drinkService.CreateDrink(newDrink);
                return Ok(drink);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{coinId}")]
        public async Task<ActionResult<CoinDto>> GetCoin(int coinId)
        {
            try
            {
                var coin = _helpService.GetCoin(coinId);
                return Ok(coin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<CoinDto>> UpdateCoin([FromBody] CoinDto updateCoin)
        {
            try
            {
                var coin = _helpService.UpdateCoin(updateCoin);
                return Ok(coin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //-----
    }
}
