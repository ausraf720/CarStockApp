using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System;
using System.Security.Cryptography;

namespace CarStockApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;

        public CarController(ILogger<CarController> logger)
        {
            _logger = logger;
        }

        // Get list of cars for a certain make (brand)
        [HttpGet("SearchByMakeOnly", Name = "SearchByMakeOnly")]
        public IEnumerable<CarStocks> GetCarsByMakeOnly(string brand)
        {
            List<CarStocks> cars = CarList.GetCars1();
            return CarStocks.FilterByMake(cars, brand);
        }

        // Get list of cars for a certain model
        // Car brand also needs to be known, as in some rare cases, model name shared between 2 brands
        [HttpGet("SearchByModel", Name = "SearchByModel")]
        public IEnumerable<CarStocks> GetCarsByModel(string brand, string model)
        {
            List<CarStocks> cars = CarList.GetCars1();
            return CarStocks.FilterByModel(cars, brand, model);
        }


        // Get list of cars for a certain year
        [HttpGet("SearchByYearOnly", Name = "SearchByYearOnly")]
        public IEnumerable<CarStocks> GetCarsByYearOnly(int year)
        {
            List<CarStocks> cars = CarList.GetCars1();
            return CarStocks.FilterByYear(cars, year);
        }

        // Get car by everything
        [HttpGet("SearchByEverything", Name = "SearchByEverything")]
        public IEnumerable<CarStocks> GetCarsByMakeAndModel(string brand, string name, int year)
        {
            List<CarStocks> cars = CarList.GetCars1();
            return CarStocks.FilterByMakeAndModel(cars, brand, name, year);
        }

        // Update Stock for a make/model
        [HttpPatch("UpdateCarStock", Name = "UpdateCarStock")]
        public IActionResult UpdateCarStock(string brand, string name, int year, int num)
        {
            List<CarStocks> cars = CarList.GetCars1();
            CarStocks car = CarStocks.UpdateStock(cars, brand, name, year, num);
            if (car != null)
            {
                return Ok(car);
            }
            else
            {
                return NotFound("Can't find this car type.");
            }
            
        }

        // Add Car
        [HttpPost("AddCarStock", Name = "AddCarStock")]
        public IActionResult AddCarStock(string brand, string name, int year, int num)
        {
            List<CarStocks> cars = CarList.GetCars1();
            if (CarStocks.FilterByMakeAndModel(cars, brand, name, year).Count != 0)
            {
                return Conflict("Car already exists.");
            }
            else
            {
                CarStocks car = CarStocks.AddCar(cars, brand, name, year, num);
                return Ok(car);
            }
        }

        // Delete Car
        [HttpDelete("DeleteCarStock", Name = "DeleteCarStock")]
        public IActionResult DeleteCarStock(string brand, string name, int year)
        {
            if (CarStocks.FilterByMakeAndModel(CarList.GetCars1(), brand, name, year).Count == 0)
            {
                return NotFound("Car doesn't exist.");
            }
            else
            {
                CarStocks car = CarStocks.DeleteCar(CarList.GetCars1(), brand, name, year);
                return Ok(car);
            }

        }
    }
}