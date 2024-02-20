using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Creational_Design_Patterns
{
    internal class FactoryMethodPatternDemo
    {
        public void Execute()
        {
            #region Non-compliant code 1
            LogisticsApp app = new LogisticsApp();
            app.TransportGoods();
            #endregion

            #region compliant code 1
            TruckFactory truckFactory = new TruckFactory();
            ShipFactory shipFactory = new ShipFactory();
            LogisticsApp1 truck = new LogisticsApp1(truckFactory);
            LogisticsApp1 ship = new LogisticsApp1(shipFactory);
            truck.TransportGoods();
            ship.TransportGoods();

            #endregion

            #region compliant code 2
            var notificationSystem = new NotificationSystem();
            // For a user who prefers email notifications:
            notificationSystem.NotifyUser(new EmailNotifierFactory(), "This is an email notification!");
            // For a user who prefers SMS notifications:
            notificationSystem.NotifyUser(new SMSNotifierFactory(), "This is an SMS notification!");
            // For a user who prefers push notifications:
            notificationSystem.NotifyUser(new PushNotifierFactory(), "This is a push notification!");
            #endregion

            #region compliant code 3
            var documentService = new DocumentService();
            // User wants to export to PDF:
            documentService.ExportDocument(new PdfConverterFactory(), "source.docx", "output.pdf");
            // User wants to export to DOCX:
            documentService.ExportDocument(new DocxConverterFactory(), "source.pdf", "output.docx");
            // User wants to export to TXT:
            documentService.ExportDocument(new TxtConverterFactory(), "source.pdf", "output.txt");
            #endregion
        }
    }

    #region Non-compliant code 1
    public class Truck
    {
        public void Deliver()
        {
            Console.WriteLine("Delivering goods by truck.");
            // Truck-specific delivery logic...
        }
    }

    public class LogisticsApp
    {
        public void TransportGoods()
        {
            Truck truck = new Truck();
            truck.Deliver();
        }
    }

    #endregion

    #region compliant code 1
    public interface ITransport
    {
        void Deliver();
    }

    public class TruckDemo: ITransport
    {
        public void Deliver()
        {
            Console.WriteLine("Delivering goods by truck.");
            // Truck-specific delivery logic...
        }
    }

    public class ShipDemo: ITransport
    {
        public void Deliver()
        {
            Console.WriteLine("Delivering goods by ship.");
            // Ship-specific delivery logic...
        }
    }

    public abstract class TransportFactory
    {
        public abstract ITransport CreateTransport();
    }

    public class TruckFactory : TransportFactory
    {
        public override ITransport CreateTransport()
        {
            return new TruckDemo();
        }
    }

    public class ShipFactory: TransportFactory
    {
        public override ITransport CreateTransport()
        {
            return new ShipDemo();  
        }
    }


    public class LogisticsApp1
    {
        private TransportFactory transportFactory;

        public LogisticsApp1(TransportFactory transportFactory)
        {
            this.transportFactory = transportFactory;
        }

        public void TransportGoods()
        {
            ITransport transport = transportFactory.CreateTransport();
            transport.Deliver();
        }
    }
    //In this refactored version:

    //The Transport interface defines the common behavior for all types of transportation.
    //The Truck and Ship classes implement the Transport interface.
    //The TransportFactory abstract class defines a factory method(CreateTransport) that returns an instance of ITransport.
    //Concrete factory classes (TruckFactory and ShipFactory) override the CreateTransport method to return instances of specific transportation types.
    //The LogisticsApp class accepts a TransportFactory as a parameter and uses it to create instances of ITransport, making the application agnostic to the specific type of transportation.
    #endregion

    #region compliant code 2
    //Abstract Product
    public interface INotifier
    {
        void SendNotification(string message);
    }

    //Concrete Product
    public class EmailNotifier : INotifier
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"Sending Email: {message}");
            // Actual email sending logic goes here...
        }
    }

    //Concrete Product
    public class SMSNotifier : INotifier
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"Sending SMS: {message}");
            // Actual SMS sending logic goes here...
        }
    }

    //Concrete Product
    public class PushNotifier : INotifier
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"Sending Push Notification: {message}");
            // Actual push notification logic goes here...
        }
    }

    //Creator (Abstract Class)
    public abstract class NotifierFactory
    {
        public abstract INotifier CreateNotifier();
    }

    //Concrete Creators
    public class EmailNotifierFactory : NotifierFactory
    {
        public override INotifier CreateNotifier()
        {
            return new EmailNotifier();
        }
    }
    public class SMSNotifierFactory : NotifierFactory
    {
        public override INotifier CreateNotifier()
        {
            return new SMSNotifier();
        }
    }
    public class PushNotifierFactory : NotifierFactory
    {
        public override INotifier CreateNotifier()
        {
            return new PushNotifier();
        }
    }

    //Client Code
    public class NotificationSystem
    {
        public void NotifyUser(NotifierFactory factory, string message)
        {
            INotifier notifier = factory.CreateNotifier();
            notifier.SendNotification(message);
        }
    }

    #endregion

    #region compliant code 3 - Document Format Converters
    //Product(Interface)
    public interface IDocumentConverter
    {
        void Convert(string inputFile, string outputFile);
    }
    // Concrete Products
    public class PdfConverter : IDocumentConverter
    {
        public void Convert(string inputFile, string outputFile)
        {
            Console.WriteLine($"Converting {inputFile} to PDF and saving as {outputFile}.");
            // Actual PDF conversion logic goes here...
        }
    }
    public class DocxConverter : IDocumentConverter
    {
        public void Convert(string inputFile, string outputFile)
        {
            Console.WriteLine($"Converting {inputFile} to DOCX and saving as {outputFile}.");
            // Actual DOCX conversion logic goes here...
        }
    }
    public class TxtConverter : IDocumentConverter
    {
        public void Convert(string inputFile, string outputFile)
        {
            Console.WriteLine($"Converting {inputFile} to TXT and saving as {outputFile}.");
            // Actual TXT conversion logic goes here...
        }
    }

    //Creator (Abstract Class)
    public abstract class DocumentConverterFactory
    {
        public abstract IDocumentConverter CreateConverter();
    }
    //Concrete Creators
    public class PdfConverterFactory : DocumentConverterFactory
    {
        public override IDocumentConverter CreateConverter()
        {
            return new PdfConverter();
        }
    }
    public class DocxConverterFactory : DocumentConverterFactory
    {
        public override IDocumentConverter CreateConverter()
        {
            return new DocxConverter();
        }
    }
    public class TxtConverterFactory : DocumentConverterFactory
    {
        public override IDocumentConverter CreateConverter()
        {
            return new TxtConverter();
        }
    }

    //Client Code
    public class DocumentService
    {
        public void ExportDocument(DocumentConverterFactory factory, string inputFile, string outputFile)
        {
            IDocumentConverter converter = factory.CreateConverter();
            converter.Convert(inputFile, outputFile);
        }
    }
    #endregion
}
