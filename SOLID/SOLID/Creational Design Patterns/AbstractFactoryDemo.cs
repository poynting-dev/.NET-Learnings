using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Creational_Design_Patterns
{
    internal class AbstractFactoryDemo
    {
        public void Execute()
        {
            #region compliant code 1
            Console.WriteLine("Processing payment using Credit Card:");
            var creditCardFactory = new CreditCardPaymentFactory();
            var creditCardProcessor = new PaymentProcessor(creditCardFactory);
            creditCardProcessor.ProcessPayment(100.00M);
            Console.WriteLine("\nProcessing payment using PayPal:");
            var payPalFactory = new PayPalPaymentFactory();
            var payPalProcessor = new PaymentProcessor(payPalFactory);
            payPalProcessor.ProcessPayment(100.00M);
            #endregion

            #region compliant code 2
            Console.WriteLine("-----------------------------------");
            DatabaseManager sqlManager = new DatabaseManager(new SQLServerFactory());
            DatabaseManager mongoDbManager = new DatabaseManager(new MongoDBFactory());
            sqlManager.PerformDatabaseOperations("SELECT * FROM Users");
            mongoDbManager.PerformDatabaseOperations("db.topNRowsDemo.find().pretty();");
            #endregion

            #region compliant code 3
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Creating UI for Mobile:");
            var mobileFactory = new MobileUIFactory();
            var mobileUI = new UserInterface(mobileFactory);
            mobileUI.RenderUI();
            Console.WriteLine("\nCreating UI for Desktop:");
            var desktopFactory = new DesktopUIFactory();
            var desktopUI = new UserInterface(desktopFactory);
            desktopUI.RenderUI();
            #endregion
        }
    }

    #region compliant code 1
    //Abstract product
    public interface IPaymentAuthorization
    {
        bool AuthorizePayment(decimal amount);
    }

    public interface IPaymentTransfer
    {
        bool TransferPayment(decimal amount);
    }

    //Concrete Products for Credit card
    public class CreditCardAuthorization: IPaymentAuthorization
    {
        public bool AuthorizePayment(decimal amount)
        {
            Console.WriteLine($"Authorizing payment of {amount} via Credit Card...");
            return true; // Mocked success
        }
    }

    public class CreditCardTransfer : IPaymentTransfer
    {
        public bool TransferPayment(decimal amount)
        {
            Console.WriteLine($"Transferring payment of {amount} via Credit Card...");
            return true; // Mocked success
        }
    }
    // Concrete Products for PayPal
    public class PayPalAuthorization : IPaymentAuthorization
    {
        public bool AuthorizePayment(decimal amount)
        {
            Console.WriteLine($"Authorizing payment of {amount} via PayPal...");
            return true; // Mocked success
        }
    }
    public class PayPalTransfer : IPaymentTransfer
    {
        public bool TransferPayment(decimal amount)
        {
            Console.WriteLine($"Transferring payment of {amount} via PayPal...");
            return true; // Mocked success
        }
    }

    //Abstract Factory
    public interface IPaymentFactory
    {
        IPaymentAuthorization CreateAuthorization();
        IPaymentTransfer CreateTransfer();
    }

    //Concrete Factories
    public class CreditCardPaymentFactory : IPaymentFactory
    {
        public IPaymentAuthorization CreateAuthorization()
        {
            return new CreditCardAuthorization();
        }

        public IPaymentTransfer CreateTransfer()
        {
            return new CreditCardTransfer();
        }
    }

    public class PayPalPaymentFactory : IPaymentFactory
    {
        public IPaymentAuthorization CreateAuthorization()
        {
            return new PayPalAuthorization();
        }

        public IPaymentTransfer CreateTransfer()
        {
            return new PayPalTransfer();
        }
    }

    //Client Code
    public class PaymentProcessor
    {
        private readonly IPaymentAuthorization _authorization;
        private readonly IPaymentTransfer _transfer;

        public PaymentProcessor(IPaymentFactory factory)
        {
            _authorization = factory.CreateAuthorization();
            _transfer = factory.CreateTransfer();
        }

        public bool ProcessPayment(decimal amount)
        {
            if(_authorization.AuthorizePayment(amount))
            {
                return _transfer.TransferPayment(amount);
            }
            return false;
        }
    }

    #endregion

    #region compliant code 2
    // Abstract Products
    public interface ICommand
    {
        void Execute(string query);
    }
    public interface IConnection
    {
        void OpenConnection();
    }
    // Concrete Products for SQL Server
    public class SQLServerCommand : ICommand
    {
        public void Execute(string query)
        {
            Console.WriteLine($"SQL Server executing command: {query}");
        }
    }
    public class SQLServerConnection : IConnection
    {
        public void OpenConnection()
        {
            Console.WriteLine("SQL Server connection opened.");
        }
    }
    // Concrete Products for MongoDB
    public class MongoDBCommand : ICommand
    {
        public void Execute(string query)
        {
            Console.WriteLine($"MongoDB executing command: {query}");
        }
    }
    public class MongoDBConnection : IConnection
    {
        public void OpenConnection()
        {
            Console.WriteLine("MongoDB connection opened.");
        }
    }
    // Abstract Factory
    public interface IDatabaseFactory
    {
        ICommand CreateCommand();
        IConnection CreateConnection();
    }
    // Concrete Factories
    public class SQLServerFactory : IDatabaseFactory
    {
        public ICommand CreateCommand() => new SQLServerCommand();
        public IConnection CreateConnection() => new SQLServerConnection();
    }
    public class MongoDBFactory : IDatabaseFactory
    {
        public ICommand CreateCommand() => new MongoDBCommand();
        public IConnection CreateConnection() => new MongoDBConnection();
    }
    // Client Code
    public class DatabaseManager
    {
        private readonly ICommand _command;
        private readonly IConnection _connection;
        public DatabaseManager(IDatabaseFactory factory)
        {
            _command = factory.CreateCommand();
            _connection = factory.CreateConnection();
        }
        public void PerformDatabaseOperations(string query)
        {
            _connection.OpenConnection();
            _command.Execute(query);
        }
    }


    #endregion

    #region compliant code 3
    // Abstract Products
    public interface IButton
    {
        void Render();
    }
    public interface IMenu
    {
        void Display();
    }
    // Concrete Products for Mobile
    public class MobileButton : IButton
    {
        public void Render()
        {
            Console.WriteLine("Rendering a mobile button.");
        }
    }
    public class MobileMenu : IMenu
    {
        public void Display()
        {
            Console.WriteLine("Displaying a mobile menu.");
        }
    }
    // Concrete Products for Desktop
    public class DesktopButton : IButton
    {
        public void Render()
        {
            Console.WriteLine("Rendering a desktop button.");
        }
    }
    public class DesktopMenu : IMenu
    {
        public void Display()
        {
            Console.WriteLine("Displaying a desktop menu.");
        }
    }
    // Abstract Factory
    public interface IUIFactory
    {
        IButton CreateButton();
        IMenu CreateMenu();
    }
    // Concrete Factories
    public class MobileUIFactory : IUIFactory
    {
        public IButton CreateButton() => new MobileButton();
        public IMenu CreateMenu() => new MobileMenu();
    }
    public class DesktopUIFactory : IUIFactory
    {
        public IButton CreateButton() => new DesktopButton();
        public IMenu CreateMenu() => new DesktopMenu();
    }
    // Client Code
    public class UserInterface
    {
        private readonly IButton _button;
        private readonly IMenu _menu;
        public UserInterface(IUIFactory factory)
        {
            _button = factory.CreateButton();
            _menu = factory.CreateMenu();
        }
        public void RenderUI()
        {
            _button.Render();
            _menu.Display();
        }
    }
    #endregion
}