using SOLID.Creational_Design_Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Single SRP = new Single();
            //SRP.ExecuteAnimal();
            OCP ocp = new OCP();
            //ocp.Execute();
            LSP lsp = new LSP();
            //lsp.Execute();
            DIP dip = new DIP();
            //dip.Execute();

            //DesignPattern singleton = new DesignPattern();
            //singleton.Execute();

            //BuilderDemo builder = new BuilderDemo();
            //builder.Execute();

            //PrototypeDemo withoutPrototypeExecute = new PrototypeDemo();
            //withoutPrototypeExecute.WithoutPrototypeExecute();

            //PrototypeDemo prototypeDemo = new PrototypeDemo();
            //prototypeDemo.Execute();

            FactoryMethodPatternDemo factoryMethod = new FactoryMethodPatternDemo();
            factoryMethod.Execute();

            Console.WriteLine("-----------AbstractFactoryDemo------------");
            AbstractFactoryDemo abstractFactory = new AbstractFactoryDemo();
            abstractFactory.Execute();

            Console.ReadKey();
        }
    }
}
