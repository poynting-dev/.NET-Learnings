using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Creational_Design_Patterns
{
    internal class BuilderDemo
    {
        public void Execute()
        {
            CarManufacturer toyota = new CarManufacturer();
            ICarBuilder builder = new CarBuilder(); //for dependency Injection

            toyota.SetCarBuilder(builder);
            Car fortuner = toyota.ConstructCar("Toyota", "Camry", "Silver", 2022);

            Console.WriteLine("Car Built:");
            fortuner.DisplayCarInfo();
        }
    }

    #region Builder Pattern For Creating Cars
    //Product: Represents a complex object under construction
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }

        public void DisplayCarInfo()
        {
            Console.WriteLine($"Car Info: {Year} {Make} {Model}, Color: {Color}");
        }
    }

    // Builder: Abstract interface for creating parts of a Product object
    public interface ICarBuilder
    {
        void BuildMake(string make);
        void BuildModel(string model);
        void BuildYear(int year);
        void BuildColor(string color);
        Car GetCar();
    }

    // Concrete Builder: Provides implementation for building different parts of the Product
    //Different builders execute the same task in various ways.
    //NOTE: Another Car bUilder can be created to build supercars, implementing - ICarBuilder interface & other interfaces
    public class CarBuilder : ICarBuilder
    {
        private Car car = new Car();

        public void BuildColor(string color)
        {
            car.Color = color;
        }

        public void BuildMake(string make)
        {
            car.Make = make;
        }

        public void BuildModel(string model)
        {
            car.Model = model;
        }

        public void BuildYear(int year)
        {
            car.Year = year;
        }

        public Car GetCar()
        {
            return car;
        }
    }

    // Director: Constructs an object using the Builder interface
    public class CarManufacturer
    {
        private ICarBuilder carBuilder;

        public void SetCarBuilder(ICarBuilder carBuilder)
        {
            this.carBuilder = carBuilder;
        }

        public Car ConstructCar(string make, string model, string color, int year)
        {
            carBuilder.BuildMake(make);
            carBuilder.BuildModel(model);
            carBuilder.BuildYear(year);
            carBuilder.BuildColor(color);

            return carBuilder.GetCar();
        }
    }
    #endregion
}
