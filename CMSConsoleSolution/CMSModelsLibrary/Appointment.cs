using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSModelsLibrary
{
    public class Appointment 
    {
        public int App_Id { get; set; }
        public int Pat_Id { get; set; }
        public int Doc_Id { get; set; }
        public DateTime App_Date { get; set; }
        public double App_Outstanding { get; set; }
        public int App_PayStatus { get; set; }
        //1- Not Raised, 2- Raised 3- Payed
        public string App_PatRemarks { get; set; }
        public string App_DocRemarks { get; set; }

        public Appointment()
        {

        }

        public DateTime GetAppointmentDate()
        {
            int choice = 0;
            int choiceDate;
            int choiceTime;
            DateTime choiceDateTime;
            var currentDate = (DateTime.Now).Date;
            Console.WriteLine("Please select appointment date");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("{0} - {1} ",i+1, currentDate.AddDays(i+1).ToString("dd/MM/yyyy"));
            }
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 5)
            {
                Console.WriteLine("Try again. Please enter a number within 1 to 5");
            }
            choiceDate = choice;
            Console.WriteLine("Please select time slot");
            Console.WriteLine("1- 10AM ");
            Console.WriteLine("2- 2PM");
            Console.WriteLine("3- 4PM");
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 3)
            {
                Console.WriteLine("Try again. Please enter a number within 1 to 3");
            }
            if (choice == 1)
                choiceTime = 10;
            else if (choice == 2)
                choiceTime = 14;
            else
                choiceTime = 16;
            choiceDateTime = currentDate.AddDays(choiceDate).AddHours(choiceTime);
            return choiceDateTime;
        }

        public override string ToString()
        {
            return "Appointment ID: " + App_Id
                + "\nPatient ID: " + Pat_Id
                + "\nDoctor ID: " + Doc_Id
                + "\nAppointment Date: " + App_Date
                + "\nPatient Remarks: " + App_PatRemarks
                + "\nDoctor Remarks: " + App_DocRemarks
                + "\nOutstanding Payment: " + App_Outstanding
                + "\nPay Status: " + (App_PayStatus == 1 ? "Not Raised" : (App_PayStatus == 2 ? "Payment Raised" : "Payment Settled"));
        }
    }
}
