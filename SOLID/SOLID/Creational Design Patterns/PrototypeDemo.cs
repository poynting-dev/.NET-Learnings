using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Creational_Design_Patterns
{
    internal class PrototypeDemo
    {
        public void Execute()
        {
            //CircleDemo circle1 = new CircleDemo() { Color= "Red", Radius=4, X=4, Y=5};
            //CircleDemo circle2 = circle1.GetClone();
            //circle2.Color = "Green";

            //Console.WriteLine($"{ circle1.Color } {circle1.Radius} {circle1.X} {circle1.Y}");
            //Console.WriteLine($"{ circle2.Color } {circle2.Radius} {circle2.X} {circle2.Y}");

            Example2WithoutPrototype.Call();
            Example2Prototype.Call();

        }

        public void WithoutPrototypeExecute()
        {
            // Usage
            var originalCircle = new Circle(10, 20, "blue", 15);
            // Whenever you need a copy, you'd have to manually create a new instance and copy the properties.
            var copiedCircle = new Circle(originalCircle.X, originalCircle.Y, originalCircle.Color, originalCircle.Radius);
        }
    }

    #region with Prototype Pattern example 2
    public class DocumentDemo : ICloneable
    {
        public string Text { get; set; }
        public List<string> Images { get; set; }

        public DocumentDemo()
        {
            Images = new List<string>();
        }

        // Implementing the Clone method using ICloneable interface
        public object Clone()
        {
            DocumentDemo clone = (DocumentDemo)this.MemberwiseClone(); // Shallow copy
            clone.Images = new List<string>(Images); // Deep copy of images list
            return clone;
        }
    }

    class Example2Prototype
    {
        public static void Call()
        {
            Document originalDoc = new Document();
            originalDoc.Text = "This is the original document.";
            originalDoc.Images.Add("image1.png");
            originalDoc.Images.Add("image2.png");

            // Cloning the document with the Prototype pattern
            Document clonedDoc = (Document)originalDoc.Clone();

            Console.WriteLine("Original Document Images Count: " + originalDoc.Images.Count);
            Console.WriteLine("Cloned Document Images Count: " + clonedDoc.Images.Count);
        }
    }
    #endregion

    #region without Prototype Pattern example 2
    public class Document
    {
        public string Text { get; set; }
        public List<string> Images { get; set; }

        public Document()
        {
            Images = new List<string>();
        }

        // Method to manually clone the document (cumbersome and error-prone)
        public Document Clone()
        {
            Document newDoc = new Document();
            newDoc.Text = Text;
            foreach (var image in Images)
            {
                newDoc.Images.Add(image);
            }
            return newDoc;
        }
    }

    public class Example2WithoutPrototype
    {
        public static void Call()
        {
            Document originalDoc = new Document();
            originalDoc.Text = "This is the original document.";
            originalDoc.Images.Add("image1.png");
            originalDoc.Images.Add("image2.png");

            // Cloning the document without the Prototype pattern
            Document clonedDoc = originalDoc.Clone();

            Console.WriteLine("Original Document Images Count: " + originalDoc.Images.Count);
            Console.WriteLine("Cloned Document Images Count: " + clonedDoc.Images.Count);
        }
    }

    #endregion

    #region with Prototype Pattern example 1
    public class CircleDemo
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Color { get; set; }
        public int Radius { get; set; }

        //Shallow Copy
        public CircleDemo GetClone()
        {
            return (CircleDemo) this.MemberwiseClone();
        }
    }

    #endregion

    #region Without Prototype Pattern example 1
    public abstract class Shape
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Color { get; set; }

        //Assume there's a complex setup process involved
    }

    public class Circle: Shape
    {
        public int Radius { get; set; }
        public Circle(int x, int y, string color, int radius)
        {
            X = x;
            Y = y;
            Color = color;
            Radius = radius;
        }
    }

    #endregion


}