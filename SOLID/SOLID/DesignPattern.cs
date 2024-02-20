using SOLID.Creational_Design_Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    internal class DesignPattern
    {
        public void Execute()
        {
            //SingletonExecute();
            SingletonLoggger();
        }

        public void SingletonExecute()
        {
            SingletonDemo s1 = SingletonDemo.GetInstance();
            SingletonDemo s2 = SingletonDemo.GetInstance();

            if (s1 == s2)
            {
                Console.WriteLine("SingletonDemo works, both variables contain the same instance.");
            }
            else
            {
                Console.WriteLine("SingletonDemo failed, variables contain different instances.");
            }

            SingletonThreadSafe singletonThreadSafe1 = SingletonThreadSafe.GetInstance("Remote1");
            SingletonThreadSafe singletonThreadSafe2 = SingletonThreadSafe.GetInstance("Remote2");
            Console.WriteLine(singletonThreadSafe1.Value);
            Console.WriteLine(singletonThreadSafe2.Value);
        }

        public void SingletonLoggger()
        {
            LoggerSingleton Sqllogger = LoggerSingleton.Instance;

            Sqllogger.Log("Loading DB operation....");

            LoggerSingleton Mongodblogger = LoggerSingleton.Instance;
            Mongodblogger.Log("Starting update operation....");

            Mongodblogger.Log("Updating Student collection....");
            foreach(var log in Mongodblogger.GetLogs())
            {
                Console.WriteLine(log);
            }
        }
    }
}
