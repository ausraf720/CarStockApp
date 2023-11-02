using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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

        // Filter by make only 
        public static List<CarStocks> FilterByMake(List<CarStocks> cars, string DesiredMake)
        {
            return cars.Where(car => car.Make == DesiredMake).ToList();
        }

        // Filter by model
        public static List<CarStocks> FilterByModel(List<CarStocks> cars, string DesiredMake, string DesiredModel)
        {
            return cars.Where(car => (car.Make == DesiredMake) && (car.Model == DesiredModel)).ToList();
        }


        // Filter by year only
        public static List<CarStocks> FilterByYear(List<CarStocks> cars, int DesiredYear)
        {
            return cars.Where(car => car.Year == DesiredYear).ToList();
        }

        // Filter by everything (make, model, year)
        public static List<CarStocks> FilterByMakeAndModel(List<CarStocks> cars, string DesiredMake, string DesiredModel, int DesiredYear)
        {
            return cars.Where(car => (car.Make == DesiredMake) && (car.Model == DesiredModel) && (car.Year == DesiredYear)).ToList();
        }

        // Update Stock for car
        public static CarStocks UpdateStock(List<CarStocks> cars, string DesiredMake, string DesiredModel, int DesiredYear, int newStock)
        {
            if (CarStocks.FilterByMakeAndModel(CarList.GetCars1(), DesiredMake, DesiredModel, DesiredYear).Count != 0)
            {
                CarStocks car = CarStocks.FilterByMakeAndModel(cars, DesiredMake, DesiredModel, DesiredYear)[0];
                car.Stock = newStock;
                return car;
            }
            else
            {
                return null;
            }
        }

        // Add new car
        public static CarStocks AddCar(List<CarStocks> cars, string NewMake, string NewModel, int NewYear, int NewStock)
        {
            cars.Add(new CarStocks(NewMake, NewModel, NewYear, NewStock));
            CarStocks car = CarStocks.FilterByMakeAndModel(cars, NewMake, NewModel, NewYear)[0];
            return car;
        }

        // Delete a car
        public static CarStocks DeleteCar(List<CarStocks> cars, string OldMake, string OldModel, int OldYear)
        {
            CarStocks OldCar = CarStocks.FilterByMakeAndModel(cars, OldMake, OldModel, OldYear)[0];
            cars.Remove(OldCar);

            return OldCar;
        }
    }

    // Generates list of cars for a particular dealer
    public class CarList
    {
        private static List<CarStocks> cars1;
        private static List<CarStocks> cars2;

        static CarList()
        {
            // Cars for dealer1
            cars1 = new List<CarStocks>
            {
                new CarStocks("Audi", "A4", 2023, 100),
                new CarStocks("Audi", "A4", 2022, 50),
                new CarStocks("Audi", "A6", 2023, 50),
                new CarStocks("Audi", "A8", 2023, 10),
                new CarStocks("Toyota", "Camry", 2023, 300),
                new CarStocks("Toyota", "Camry", 2022, 250),
                new CarStocks("Toyota", "Corolla", 2023, 200),
                new CarStocks("VW", "Golf", 2023, 150),
                new CarStocks("VW", "Polo", 2023, 70)
            };

            // Cars for dealer2
            cars2 = new List<CarStocks>
            {
                new CarStocks("Toyota", "Camry", 2022, 150),
                new CarStocks("BMW", "M3", 2023, 90),
                new CarStocks("BMW", "M3", 2022, 40)
            };

        }
        public static List<CarStocks> GetCars1()
        {
            return cars1;
        }
        public static List<CarStocks> GetCars2()
        {
            return cars2;
        }

        // Method to return list of cars based on current user
        public static List<CarStocks> Decider(string username)
        {
            if (username == "dealer1")
            {
                return GetCars1();
            }
            else if (username == "dealer2") {
                return GetCars2();
            }
            else
            {
                return null;
            }
        }

    }

    // Variable for current user (tried to decode JWT to get user, but couldn't get it working)
    public static class AppGlobals
    {
        public static string? CurrentUser { get; set; }
    }

    // Validate username matches password
    public interface IUserService
    {
        bool ValidateCredentials(string username, string password);
    }

    // Dictionary list of current users with their passwords
    public class UserService : IUserService
    {
        private readonly Dictionary<string, string> _users = new Dictionary<string, string>
        {
            { "dealer1", "pass1" },
            { "dealer2", "pass2" },
            // More dealers can be added if necessary
        
        };

        public bool ValidateCredentials(string username, string password)
        {

            return _users.TryGetValue(username, out var storedPassword) && storedPassword == password;
        }
    }

}



