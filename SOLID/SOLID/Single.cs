using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    //Example 1 violation
    public class Animal
    {
        public string Name { get; set; }
        public string FeedingType { get; set; }
        public string SoundMade { get; set; }

        public Animal(string name, string feedingType, string soundMade)
        {
            Name = name;
            FeedingType = feedingType;
            SoundMade = soundMade;
        }

        public void Nomenclature()
        {
            Console.WriteLine($"Name of Animal is {Name}");
        }

        public void Feeding()
        {
            Console.WriteLine($"Feeding type is {FeedingType}");
        }

        public void Sound()
        {
            Console.WriteLine($"Sound Made is {SoundMade}");
        }
    }

    //Example 2 violation
    public class Report
    {
        public string Content { get; set; }

        public void GenerateReport()
        {
            Console.WriteLine("Generating report...");
            // Logic to generate a report based on Content
        }

        public void SaveToFile()
        {
            Console.WriteLine("Saving report to file...");
            // Logic to save the report to a file
        }

        public void SendEmail()
        {
            Console.WriteLine("Sending email...");
            // Logic to send the report via email
        }
    }

    //Example 2 non-violation
    public class GenReport
    {
        public string Content { get; set; }

        public void GenerateReport()
        {
            Console.WriteLine("Generating report...");
            // Logic to generate a report based on Content
        }
    }

    //Example 2 non-violation
    public class Save
    {
        public void SaveToFile(Report report)
        {
            Console.WriteLine("Saving report to file...");
            // Logic to save the report to a file
        }
    }

    //Example 2 non-violation
    public class EmailLogic
    { 
        public void SendEmail(Report report)
        {
            Console.WriteLine("Sending email...");
            // Logic to send the report via email
        }
    }

    public class Single
    {
        //Method 1 violation
        public void ExecuteAnimal()
        {
            Animal dog = new Animal("Dog", "Omnivore", "bark");
            dog.Sound();
            dog.Nomenclature();

            Animal elephant = new Animal("elephant ", "Herbivore", "Triumphet");
            elephant.Sound();
            elephant.Nomenclature();

            Console.ReadLine();

        }

        //Metod 2 violation
        public void ExecuteReport()
        {
            Report report = new Report();
            report.GenerateReport();
            report.SaveToFile();
            report.SendEmail();
        }

        //Method 2 non-violation
        public void ExecuteGenReport()
        {
            GenReport report = new GenReport() { Content = "This is my book" };
            report.GenerateReport();
        }
    }



}
