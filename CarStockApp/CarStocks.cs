using System;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace CarStockApp
{
    public class CarStocks
    {
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public int Stock { get; set; }

        public CarStocks(string make, string model, int year, int stock)
        {
            Make = make;
            Model = model;
            Year = year;
            Stock = stock;
        }

        public static List<CarStocks> FilterByMake(List<CarStocks> cars, string DesiredMake)
        {
            return cars.Where(car => car.Make == DesiredMake).ToList();
        }

        public static List<CarStocks> FilterByMakeAndModel(List<CarStocks> cars, string DesiredMake, string DesiredModel)
        {
            return cars.Where(car => (car.Make == DesiredMake) && (car.Model == DesiredModel)).ToList();
        }

        public static CarStocks UpdateStock(List<CarStocks> cars, string DesiredMake, string DesiredModel, int newStock)
        {
            if (CarStocks.FilterByMakeAndModel(CarList.GetCars(), DesiredMake, DesiredModel).Count != 0)
            {
                CarStocks car = CarStocks.FilterByMakeAndModel(cars, DesiredMake, DesiredModel)[0];
                car.Stock = newStock;
                return car;
            }
            else
            {
                return null;
            }
        }

        // Add new car
        public static CarStocks AddCar(List<CarStocks> cars, string NewMake, string NewModel, int NewStock)
        {
            cars.Add(new CarStocks(NewMake, NewModel, 2023, NewStock));
            CarStocks car = CarStocks.FilterByMakeAndModel(cars, NewMake, NewModel)[0];
            return car;
        }

        // Delete a car
        public static CarStocks DeleteCar(List<CarStocks> cars, string OldMake, string OldModel)
        {
            CarStocks OldCar = CarStocks.FilterByMakeAndModel(cars, OldMake, OldModel)[0];
            cars.Remove(OldCar);

            return OldCar;
        }
    }

    // Generates list of cars for a particular dealer
    public class CarList
    {
        private static List<CarStocks> cars;
        static CarList()
        {
           cars = new List<CarStocks>
            {
                new CarStocks("Audi", "A4", 2023, 100),
                new CarStocks("Audi", "A6", 2023, 50),
                new CarStocks("Audi", "A8", 2023, 10),
                new CarStocks("Toyota", "Camry", 2023, 300),
                new CarStocks("Toyota", "Corolla", 2023, 200),
                new CarStocks("VW", "Golf", 2023, 150),
                new CarStocks("VW", "Polo", 2023, 70)
            };
        }
        public static List<CarStocks> GetCars()
        {
            return cars;
        }

    }

}



