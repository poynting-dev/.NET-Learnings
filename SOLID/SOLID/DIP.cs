using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    internal class DIP
    {
        public void Execute()
        {
            #region with violation example 1
            //Client-code
            //NotificationService notificationService = new NotificationService();
            //notificationService.SendNotification("Hello, this is a notification", "user@example.com");
            #endregion

            #region without violation example 1
            
            IMessageSender emailService = new EmailServiceDIP();
            IMessageSender smsService = new SMSServiceDIP();
            NotificationServiceDIP notificationService = new NotificationServiceDIP(smsService);
            notificationService.SendNotification("Hello, this is a notification!", "user@example.com");

            #endregion

            #region with violation example 2

            #endregion


            #region without violation example 2
            ILogger logger = new FileLoggerDIP();
            BusinessLogicDIP businessLogic = new BusinessLogicDIP(logger);
            businessLogic.ProcessFile();
            #endregion
        }
    }

    #region class with violation example 1
    //Low-level module
    public class EmailService
    {
        public void SendEmail(string message, string receipent)
        {
            Console.WriteLine($"Sending message: {message} to receipent: {receipent}");
        }
    }

    //High-Level Module
    public class NotificationService
    {
        private EmailService emailService;

        public NotificationService()
        {
            //violation of DIP: High level module depends on low-level module
            emailService = new EmailService();
        }

        public void SendNotification(string message, string receipent)
        {
            // Some notification logic
            emailService.SendEmail(message, receipent);
            Console.WriteLine($"Sending email to {receipent}: {message}");
        }
    }
    #endregion

    #region non-violation class - example 1
    //Now, let's fix this violation by introducing an abstraction (interface) to decouple the high-level and low-level modules:
    //Abstraction (interface)
    public interface IMessageSender
    {
        void SendMessage(string message, string receipent);
    }

    //Low-Level Module
    public class EmailServiceDIP: IMessageSender
    {
        public void SendMessage(string message, string receipent)
        {
            // Actual email sending implementation
            Console.WriteLine($"Email sent to {receipent}: {message}");
        }
    }

    //Low-Level Module 2
    public class SMSServiceDIP : IMessageSender
    {
        public void SendMessage(string message, string receipent)
        {
            // Actual SMS sending implementation
            Console.WriteLine($"Email sent to {receipent}: {message}");
        }
    }

    //High-Level Module
    public class NotificationServiceDIP
    {
        private IMessageSender messageSender;

        public NotificationServiceDIP(IMessageSender sender)
        {
            messageSender = sender;
        }

        public void SendNotification(string message, string receipent)
        {
            // Some notification logic
            Console.WriteLine($"Sending message to {receipent}: {message}");
            messageSender.SendMessage(message, receipent);
        }
    }
    //Now, the NotificationService depends on the abstraction IMessageSender, and the actual implementation(EmailService) is injected through the constructor.This adheres to the Dependency Inversion Principle and makes the system more flexible and extensible.
    #endregion


    #region class with violation example 2
    //low-level modules
    public class FileLogger
    {
        public void Log(string message, string fileLocation)
        {
            Console.WriteLine($"Logging message: {message} to file {fileLocation}");
        }
    }

    //high-level modules
    public class BusinessLogic
    {
        private FileLogger logger = new FileLogger();
        public void ProcessFile()
        {
            //Some Business Logic

            //Logging
            logger.Log("Data Processing completed", "C:/example.txt");
        }
    }
    #endregion


    #region class without violation example 2
    //abstraction (interface)
    public interface ILogger
    {
        void Log(string message, string fileLocation);
    }

    //low-level modules
    public class FileLoggerDIP: ILogger
    {
        public void Log(string message, string fileLocation)
        {
            Console.WriteLine($"Logging message: {message} to file {fileLocation}");
        }
    }

    //high-level modules
    public class BusinessLogicDIP
    {
        private ILogger fileLogger;
        public BusinessLogicDIP(ILogger logger)
        {
            fileLogger = logger;
        }

        public void ProcessFile()
        {
            fileLogger.Log("Data processing completed.", "C:/example.txt");
        }
    }
    //In this fixed version:

    //We introduce an ILogger interface, which is an abstraction for logging.
    //The BusinessLogic class depends on the ILogger interface instead of a concrete implementation.
    //The FileLogger class implements the ILogger interface, providing the necessary details.
    //Now, the high-level module (BusinessLogic) depends on an abstraction (ILogger), adhering to the Dependency Inversion Principle. This allows for easier maintenance, testing, and future extensibility, as different loggers can be easily swapped in without modifying the high-level module.
    #endregion
}
