using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    public class OCP
    {
        public void Execute()
        {
            //Class with violation
            //AnimalOCP animal = new AnimalOCP("lion", "omnivore", "roar");
            //animal.getSpeed();

            #region without violation
            //OCPAnimal cheetah = new OCPAnimal("Cheetah", "Omnivore", new CheetahSpeedRate());
            //OCPAnimal elephant = new OCPAnimal("Elephant", "Herbivore", new ElephantSpeedRate());
            OCPAnimal horse = new OCPAnimal("Horse", "Herbivore", new HorseSpeedRate());
            Console.WriteLine($"{horse.Type} runs up to {horse.GetAnimalSpeed()} mph");
            //Console.WriteLine($"{cheetah.Type} runs up to {cheetah.GetAnimalSpeed()} mph");
            //Console.WriteLine($"{elephant.Type} runs up to {elephant.GetAnimalSpeed()} mph");
            #endregion

            #region with violation example 2
            Rectangle rectangle = new Rectangle { Width = 5, Height = 10 };

            AreaCalculator areaCalculator = new AreaCalculator();
            double area = areaCalculator.CalculateArea(rectangle);

            Console.WriteLine($"Area: {area}");

            #endregion


            #region
            RectangleOCP rect = new RectangleOCP() { Height=2, Width=4};
            CircleOCP circle = new CircleOCP() { Radius=6};
            Console.WriteLine($"Rectangle Area: {rect.CalculateArea()}");
            Console.WriteLine($"Circle Area: {circle.CalculateArea():F4}");
            #endregion

        }
    }

    #region class with violation example 1
    public class AnimalOCP
    {
        public string Type { get; set; }
        public string FeedingType { get; set; }
        public string SoundMade { get; set; }

        public AnimalOCP(string type, string feedingType, string soundMade)
        {
            Type = type;
            FeedingType = feedingType;
            SoundMade = soundMade;
        }

        public void getSpeed()
        {
            switch (Type)
            {
                case "cheetah":
                    Console.WriteLine("Cheetah runs up to 130mph ");
                    break;
                case "lion":
                    Console.WriteLine("Lion runs up to 80mph");
                    break;
                case "elephant":
                    Console.WriteLine("Elephant runs up to 40mph");
                    break;
                default:
                    throw new Exception($"Unsupported animal type: ${Type }");
            }

        }
    }
    #endregion

    #region non-violation class - example 1

    public abstract class SpeedRate
    {
        public abstract int getSpeed();
    }


    public class OCPAnimal
    {
        public string Type { get; set; }
        public string FeedingType { get; set; }
        public SpeedRate GetSpeed { get; }

        public OCPAnimal(string type, string feedingType, SpeedRate getSpeed)
        {
            Type = type;
            FeedingType = feedingType;
            GetSpeed = getSpeed;
        }


        public int GetAnimalSpeed()
        {
            return GetSpeed.getSpeed();
        }
    }

    public class CheetahSpeedRate: SpeedRate
    {
        public override int getSpeed()
        {
            return 130;
        }
    }

    public class ElephantSpeedRate : SpeedRate
    {
        public override int getSpeed()
        {
            return 40;
        }
    }

    public class HorseSpeedRate : SpeedRate
    {
        public override int getSpeed()
        {
            return 100;
        }
    }
    #endregion


    #region class with violation example 2
    public class Rectangle
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    // Client code that calculates area of rectangles
    public class AreaCalculator
    {
        public double CalculateArea(Rectangle rectangle)
        {
            return rectangle.Width * rectangle.Height;
        }
    }
    #endregion


    #region class without violation example 2
    public interface IShape
    {
        double CalculateArea();
    }
    public class RectangleOCP: IShape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public double CalculateArea()
        {
            return Width * Height;
        }
    }

    public class CircleOCP : IShape
    {
        public double Radius { get; set; }

        public double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }
    }
    #endregion

}