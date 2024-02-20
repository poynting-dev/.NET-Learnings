using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Creational_Design_Patterns
{
    public sealed class SingletonDemo
    {
        private SingletonDemo() { }

        public static SingletonDemo instance;

        public static SingletonDemo GetInstance()
        {
            if(instance == null)
                instance = new SingletonDemo();
            return instance;
        }

        // Finally, any singleton should define some business logic, which can
        // be executed on its instance.
        public void someBusinessLogic()
        {
            // ...
        }
    }

    class SingletonThreadSafe
    {
        private SingletonThreadSafe() { }

        private static SingletonThreadSafe _instance;

        // We now have a lock object that will be used to synchronize threads
        // during first access to the Singleton.
        private static readonly object _lock = new object();

        public static SingletonThreadSafe GetInstance(string value)
        {
            // This conditional is needed to prevent threads stumbling over the
            // lock once the instance is ready.
            if (_instance == null)
            {
                // Now, imagine that the program has just been launched. Since
                // there's no Singleton instance yet, multiple threads can
                // simultaneously pass the previous conditional and reach this
                // point almost at the same time. The first of them will acquire
                // lock and will proceed further, while the rest will wait here.
                lock (_lock)
                {
                    // The first thread to acquire the lock, reaches this
                    // conditional, goes inside and creates the Singleton
                    // instance. Once it leaves the lock block, a thread that
                    // might have been waiting for the lock release may then
                    // enter this section. But since the Singleton field is
                    // already initialized, the thread won't create a new
                    // object.
                    if (_instance == null)
                    {
                        _instance = new SingletonThreadSafe();
                        _instance.Value = value;
                    }
                }
            }
            return _instance;
        }

        // We'll use this property to prove that our Singleton really works.
        public string Value { get; set; }
    }

    class LoggerSingleton
    {
        public static LoggerSingleton instance;
        public List<string> logMessages = new List<string>();

        private LoggerSingleton() { }

        public static LoggerSingleton Instance
        {
            get
            {
                if(instance == null)
                    instance = new LoggerSingleton();
                return instance;
            }
        }

        // Example of mutability: This method changes the state by adding a new message.
        public void Log(string message)
        {
            logMessages.Add(message);
        }

        // Method to read logs, illustrating that state has changed.
        public IEnumerable<string> GetLogs()
        {
            return logMessages;
        }
    }
}
