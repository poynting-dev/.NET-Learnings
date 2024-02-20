using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    internal class ISP
    {
        public void Execute()
        {
            #region with violation example 1

            #endregion

            #region without violation example 1

            #endregion

            #region with violation example 2

            #endregion


            #region without violation example 2
            #endregion
        }
    }

    #region class with violation example 1
    public interface IReport
    {
        void GeneratePDFReport();
        void GenerateExcel();
        void SendEmail();
    }

    public class FinancialReport : IReport
    {
        public void GeneratePDFReport()
        {
            Console.WriteLine("Generating PDF Report.");
        }

        public void GenerateExcel()
        {
            Console.WriteLine("Generating Excel.");
        }

        public void SendEmail()
        {
            Console.WriteLine("Sending Email.");
        }
    }
    #endregion

    #region non-violation class - example 1
    public interface IReportISP
    {
        void GenerateReport();
    }

    public interface IExcelReport
    {
        void GenerateExcel();
    }

    public interface ISendEmail
    {
        void SendEmail();
    }

    ///This way, the FinancialReport class is not forced to implement unnecessary methods, and each interface is focused on a specific functionality, adhering to the Interface Segregation Principle.
    public class FinancialReportISP : IReportISP
    {
        public void GenerateExcel()
        {
            Console.WriteLine("Generating Excel.");
        }

        public void GenerateReport()
        {
            throw new NotImplementedException();
        }
    }
    #endregion


    #region class with violation example 2
    public interface IWorker
    {
        void Work();
        void TakeRest();
        void AttendMeeting();
    }


    public class Watchman: IWorker
    {
        public void Work()
        {
            Console.WriteLine("Watchman is Working");
        }

        public void TakeRest()
        {
            Console.WriteLine("Watchman is taking a break.");
        }

        //Below method will cause violation  as a wacthman don't need to attend a Metting.
        public void AttendMeeting()
        {
            throw new NotImplementedException();
        }
    }
    #endregion


    #region class without violation example 2
    public interface IWorkerISP
    {
        void Work();
        void TakeRest();
    }

    public interface IMeetingAttendeeISP
    {
        void AttendMeeting();
    }

    public class Programmer : IWorker, IMeetingAttendeeISP
    {
        public void Work()
        {
            Console.WriteLine("Programmer is Working");
        }

        public void TakeRest()
        {
            Console.WriteLine("Programmer is taking a break.");
        }

        public void AttendMeeting()
        {
            Console.WriteLine("Programmer is attending a meeting.");
        }
    }

    public class WatchmanISP : IWorkerISP
    {
        public void TakeRest()
        {
            throw new NotImplementedException();
        }

        public void Work()
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
