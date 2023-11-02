using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System;


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
        [HttpGet("SearchByMakeOnly/{brand}", Name = "SearchByMakeOnly")]
        public IEnumerable<CarStocks> GetCarsByMakeOnly(string brand)
        {
            return CarStocks.FilterByMake(CarList.GetCars(), brand);
        }

        // Get car by make and model
        [HttpGet("SearchByMakeAndModel/{brand}/{name}", Name = "SearchByMakeAndModel")]
        public IEnumerable<CarStocks> GetCarsByMakeAndModel(string brand, string name)
        {
            return CarStocks.FilterByMakeAndModel(CarList.GetCars(), brand, name);
        }

        // Update Stock for a make/model
        [HttpPatch("UpdateCarStock/{brand}/{name}/{num}", Name = "UpdateCarStock")]
        public IActionResult UpdateCarStock(string brand, string name, int num)
        {

            CarStocks car = CarStocks.UpdateStock(CarList.GetCars(), brand, name, num);
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
        [HttpPost("AddCarStock/{brand}/{name}/{num}", Name = "AddCarStock")]
        public IActionResult AddCarStock(string brand, string name, int num)
        {
            if (CarStocks.FilterByMakeAndModel(CarList.GetCars(), brand, name).Count != 0)
            {
                return Conflict("Car already exists.");
            }
            else
            {
                CarStocks car = CarStocks.AddCar(CarList.GetCars(), brand, name, num);
                return Ok(car);
            }
        }

        // Delete Car
        [HttpDelete("DeleteCarStock/{brand}/{name}/", Name = "DeleteCarStock")]
        public IActionResult DeleteCarStock(string brand, string name)
        {
            if (CarStocks.FilterByMakeAndModel(CarList.GetCars(), brand, name).Count == 0)
            {
                return NotFound("Car doesn't exist.");
            }
            else
            {
                CarStocks car = CarStocks.DeleteCar(CarList.GetCars(), brand, name);
                return Ok(car);
            }

        }
    }
}