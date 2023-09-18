using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WEB.Models;
using WebAPIContracts;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _client = new HttpClient();
        }

        public async Task<IActionResult> Index(string? id)
        {
            var url = string.Format(GetAbsolutePath("GetMachine"), 1);
            var urlTakeTips = string.Format(GetAbsolutePath("TakeTips"), 1);
            var urlOrderDrink = GetAbsolutePath("OrderDrink");
            var urlAddBalanceCoin = GetAbsolutePath("AddBalanceCoin");

            ViewBag.Url = url;
            ViewBag.TakeTipsUrl = urlTakeTips;
            ViewBag.UrlOrderDrink = urlOrderDrink;
            ViewBag.UrlAddBalanceCoin = urlAddBalanceCoin;


            //using (var response = await _client.GetAsync(url))
            //{
            //    WendingMachineDto machine = await response.Content.ReadAsAsync<WendingMachineDto>();
            //    var machineVM = Mapper.Map<WendingMachineViewModel>(machine);
            //    ViewBag.Coins = machineVM.Coins;
            //    ViewBag.Balance = machine.Balance;

            //    return View(machineVM);
            //}
            return View();
        }

        //public async Task<IActionResult> AddBalanceCoin(int value, int machineId)
        //{
        //    //var url = string.Format(GetAbsolutePath("AddBalanceCoin"), value);

        //    var url = GetAbsolutePath("AddBalanceCoin");
        //    AddBalanceDto add = new AddBalanceDto { Cash = value, MachineId = machineId };
        //    using(var result = await _client.PutAsJsonAsync(url, add))
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        //public async Task<IActionResult> OrderDrink(int drinkId, int machineId)
        //{
        //    var url = GetAbsolutePath("OrderDrink");
        //    OrderDrinkDto order = new OrderDrinkDto { DrinkId = drinkId, MachineId = machineId };
        //    using(var result = await _client.PutAsJsonAsync(url, order))
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult> TakeTipsModel(int machineId)
        //{
        //    try
        //    {
        //        TipsDto tips = new TipsDto();
        //        if (ModelState.IsValid)
        //        {
        //            var url = string.Format(GetAbsolutePath("TakeTips"), machineId);
        //            using (var result = await _client.GetAsync(url))
        //            {
        //                if (result.IsSuccessStatusCode)
        //                {
        //                    tips = await result.Content.ReadAsAsync<TipsDto>();
        //                }
        //            }
        //        }

        //        return Json(tips);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message);
        //    }
        //}

        public IActionResult Error()
        {
            var feature = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            return View("~/Views/Shared/Error.cshtml", feature?.Error);
        }

        private string GetAbsolutePath(string methodName)
        {
            return $"{_configuration["CoreServiceApi:BaseUrl"]}{_configuration[$"CoreServiceApi:Areas:WendingMachine:{methodName}"]}";
        }
    }
}
