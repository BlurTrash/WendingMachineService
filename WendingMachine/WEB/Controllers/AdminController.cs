using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WEB.Models;
using WebAPIContracts;


namespace WEB.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public AdminController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _logger = logger;
            _configuration = configuration;
            _environment = environment;
            _client = new HttpClient();
        }

        public async Task<IActionResult> Index(string? id)
        {
            var url = string.Format(GetAbsolutePath("GetMachine"), 1);
            var urlDeleteDrink = GetAbsolutePath("DeleteDrinkFromBody");
            ViewBag.UrlDeleteDrink = urlDeleteDrink;

            try
            {
                using (var response = await _client.GetAsync(url))
                {
                    WendingMachineDto machine = new WendingMachineDto();
                    ViewBag.IsError = false;
                    if (response.IsSuccessStatusCode)
                    {
                        machine = await response.Content.ReadAsAsync<WendingMachineDto>();
                        var machineVM = Mapper.Map<WendingMachineViewModel>(machine);
                        ViewBag.Coins = machineVM.Coins;
                        ViewBag.Balance = machine.Balance;

                        return View(machineVM);
                    }

                    var statusCode = response.StatusCode;
                    var error = await response.Content.ReadAsStringAsync();
                    ViewBag.IsError = true;
                    ViewBag.ErrorMessage = $"Ошибка загрузки! {(int)statusCode}-{statusCode.ToString()}! {error}";
                    return View();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
           
        }

        public async Task<IActionResult> AddDrink()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDrink(CreateDrinkViewModel drinkModel)
        {
            if (!ModelState.IsValid)
            {
                return View(drinkModel);
            }

            string imageUrl = "";

            if (drinkModel.File != null)
            {
                if (drinkModel.File.Length > 0)
                {
                    string[] validFormat = new string[3] { "jpg", "png", "jpeg" };
                    var fileImgFormat = drinkModel.File.FileName.Split(".").Last().ToLower();
                    if (!validFormat.Contains(fileImgFormat))
                    {
                        ViewBag.ErrorMessage = "Допустимые форматы: .jpg, .png, .jpeg!";
                        return View(drinkModel);
                    }
                    long maxFileSizeInBytes = 5 * 1024 * 1024; // 5MB
                    if (drinkModel.File.Length > maxFileSizeInBytes)
                    {
                        ViewBag.ErrorMessage = "Максимальный размер файла 5 МБ!";
                        return View();
                    }

                    string DIRECTORY_PATH = @"drinks\";
                    string path = Path.Combine(_environment.ContentRootPath, @"wwwroot\images");
                    string uploads = Path.Combine(path, DIRECTORY_PATH);
                    string imgPath = Path.Combine(uploads, drinkModel.File.FileName);

                    using (Stream fileStream = new FileStream(imgPath, FileMode.Create))
                    {
                        await drinkModel.File.CopyToAsync(fileStream);
                    }
                    var partPath = Path.Combine(DIRECTORY_PATH, drinkModel.File.FileName);
                    imageUrl = partPath.Replace('\\', '/');
                }
            }

            CreateDrinkDto newDrink = new CreateDrinkDto
            {
                Title = drinkModel.Title,
                Price = drinkModel.Price,
                Count = drinkModel.Count,
                IsAvailable = drinkModel.IsAvailable,
                ImageUrl = imageUrl,
                MachineId = 1
            };

            var url = GetAbsolutePath("CreateDrink");
            using (var result = await _client.PostAsJsonAsync(url, newDrink))
            {
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                var statusCode = result.StatusCode;
                var error = await result.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = $"{(int)statusCode}-{statusCode.ToString()}! {error}";
                return View(drinkModel);
            }
        }

        public async Task<IActionResult> Drink(int id)
        {
            var url = string.Format(GetAbsolutePath("GetDrink"), id);

            try
            {
                using (var response = await _client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        DrinkDto drink = await response.Content.ReadAsAsync<DrinkDto>();
                        var drinkVM = Mapper.Map<DrinkViewModel>(drink);
                        return View(drinkVM);
                    }
                    var statusCode = response.StatusCode;
                    var error = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = $"{(int)statusCode}-{statusCode.ToString()}! {error}";
                    return View(new DrinkDto());
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Drink(DrinkViewModel drinkModel, IFormFile file)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(drinkModel);
                }

                if (file != null)
                {
                    if (file.Length > 0)
                    {
                        string[] validFormat = new string[3] { "jpg", "png", "jpeg" };
                        var fileImgFormat = file.FileName.Split(".").Last().ToLower();
                        if (!validFormat.Contains(fileImgFormat))
                        {
                            ViewBag.ErrorMessage = "Допустимые форматы: .jpg, .png, .jpeg!";
                            return View(drinkModel);
                        }
                        long maxFileSizeInBytes = 5 * 1024 * 1024; // 5MB
                        if (file.Length > maxFileSizeInBytes)
                        {
                            ViewBag.ErrorMessage = "Максимальный размер файла 5 МБ!";
                            return View(drinkModel);
                        }

                        string DIRECTORY_PATH = @"drinks\";
                        string path = Path.Combine(_environment.ContentRootPath, @"wwwroot\images");
                        string uploads = Path.Combine(path, DIRECTORY_PATH);
                        string imgPath = Path.Combine(uploads, file.FileName);

                        using (Stream fileStream = new FileStream(imgPath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        var partPath = Path.Combine(DIRECTORY_PATH, file.FileName);
                        drinkModel.ImageUrl = partPath.Replace('\\', '/');
                    }
                }

                var url = GetAbsolutePath("UpdateDrink");
                var drinkDto = Mapper.Map<DrinkDto>(drinkModel);
                using (var result = await _client.PutAsJsonAsync(url, drinkDto))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    var statusCode = result.StatusCode;
                    var error = await result.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = $"{(int)statusCode}-{statusCode.ToString()}! {error}";
                    return View(drinkModel);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> Coin(int id)
        {
            try
            {
                var url = string.Format(GetAbsolutePath("GetCoin"), id);
                using (var response = await _client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        CoinDto coin = await response.Content.ReadAsAsync<CoinDto>();
                        var coinVM = Mapper.Map<CoinViewModel>(coin);
                        return View(coinVM);
                    }
                    var statusCode = response.StatusCode;
                    var error = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = $"{(int)statusCode}-{statusCode.ToString()}! {error}";
                    return View(new CoinDto());
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Coin(CoinViewModel coinModel)
        {
            if (!ModelState.IsValid)
            {
                return View(coinModel);
            }

            var intCount = (int)coinModel.CountCoins;
            coinModel.CountCoins = intCount;

            var url = GetAbsolutePath("UpdateCoin");
            var coinDto = Mapper.Map<CoinDto>(coinModel);
            using (var result = await _client.PutAsJsonAsync(url, coinDto))
            {
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                var statusCode = result.StatusCode;
                var error = await result.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = $"{(int)statusCode}-{statusCode.ToString()}! {error}";
                return View(coinModel);
            }
        }

        public IActionResult Error()
        {
            var feature = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            return View("~/Views/Shared/Error.cshtml", feature?.Error);
        }

        private string GetAbsolutePath(string methodName)
        {
            return $"{_configuration["CoreServiceApi:BaseUrl"]}{_configuration[$"CoreServiceApi:Areas:WendingMachine:{methodName}"]}";
        }

        //public async Task<IActionResult> DeleteDrink(int id, int machineId)
        //{
        //    try
        //    {
        //        var url = GetAbsolutePath("DeleteDrinkFromBody");
        //        DeleteDrinkDto deleteModel = new DeleteDrinkDto { DrinkId = id, MachineId = machineId };

        //        using (var result = await _client.PutAsJsonAsync(url, deleteModel))
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("Error");
        //    }
        //}
    }
}
