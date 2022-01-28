using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSModelsLibrary;

namespace CMSConsoleApp
{
    public class ManageAppointment
    {
        List<Appointment> apps = new List<Appointment>();
        ManageUser manageUser = new ManageUser();

        public ManageAppointment()
        {
            apps.Add(new Appointment
            {
                App_Id = 001,
                Pat_Id = 101,
                Doc_Id = 102,
                App_Date = new DateTime(2022, 01, 25, 10, 00, 00),
                App_Outstanding = 0,
                App_PayStatus = 1
            });

            apps.Add(new Appointment
            {
                App_Id = 002,
                Pat_Id = 101,
                Doc_Id = 102,
                App_Date = new DateTime(2022, 01, 26, 10, 00, 00),
                App_Outstanding = 124.61,
                App_PayStatus = 2
            });

            apps.Add(new Appointment
            {
                App_Id = 003,
                Pat_Id = 103,
                Doc_Id = 104,
                App_Date = new DateTime(2022, 01, 28, 14, 00, 00),
                App_Outstanding = 21.21,
                App_PayStatus = 2
            });

            apps.Add(new Appointment
            {
                App_Id = 004,
                Pat_Id = 103,
                Doc_Id = 104,
                App_Date = new DateTime(2022, 01, 25, 14, 00, 00),
                App_Outstanding = 0,
                App_PayStatus = 3
            });
        }

        #region Appointment
        public Appointment GetAppByAppId(int id)
        {
            Appointment app = apps.Find(p => p.App_Id == id);
            return app;
        }

        int GetAppIdFromUser()
        {
            Console.WriteLine("Please enter the Appointment Id");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid entry ID. Please try again...");
            }
            return id;
        }
        public Appointment GetAppByUserIdNAppId(User user, int appId)
        {
            if (user.User_Type == 1) //Patient
            {
                Appointment app = apps.Find(p => p.App_Id == appId && p.Pat_Id == user.User_Id);
                return app;
            }
            else //Doctor
            {
                Appointment app = apps.Find(p => p.App_Id == appId && p.Doc_Id == user.User_Id);
                return app;
            }
        }

        public void PrintAppDetail(Appointment item)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine(item);
        }

        public void PrintPatAppDetail(Appointment item)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Appointment ID : " + item.App_Id);
            Console.WriteLine("Doctor ID : " + item.Doc_Id);
            Console.WriteLine("Patient Remarks : " + item.App_PatRemarks);
            Console.WriteLine("Doctor Remarks : " + item.App_DocRemarks);
            Console.WriteLine("Appointment Date : " + item.App_Date);
            Console.WriteLine("Outstanding Payment : " + item.App_Outstanding);
            Console.WriteLine("Pay Status : " + (item.App_PayStatus == 1 ? "Not Raised" : (item.App_PayStatus == 2 ? "Payment Raised" : "Payment Settled")));
        }

        public void PrintDocAppDetail(Appointment item)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Appointment ID : " + item.App_Id);
            Console.WriteLine("Patient ID : " + item.Pat_Id);
            Console.WriteLine("Patient Remarks : " + item.App_PatRemarks);
            Console.WriteLine("Doctor Remarks : " + item.App_DocRemarks);
            Console.WriteLine("Appointment Date : " + item.App_Date);
            Console.WriteLine("Pay Status : " + (item.App_PayStatus == 1 ? "Not Raised" : (item.App_PayStatus == 2 ? "Payment Raised" : "Payment Settled")));
        }
        #endregion

        #region Patient Appointment
        private int GenerateAppId()
        {
            if (apps.Count == 0)
                return 001;
            else
                return (apps.Count + 1);
        }
        public Appointment GetAppByPatId(int patId)
        {
            Appointment app = apps.Find(p => p.Pat_Id == patId);
            return app;
        }
        public void PrintAppDetailsByPatId(int patId)
        {
            var PatAppointments = apps
                .Where(e => e.Pat_Id == patId);
            foreach (var item in PatAppointments)
            {
                PrintPatAppDetail(item);
            }
        }

        public void PrintUpcomingAppDetailsByPatId(int patId)
        {
            DateTime today = DateTime.Today;

            var PatAppointments = apps
                .Where(e => e.Pat_Id == patId && e.App_Date.Date >= today);
            if (PatAppointments.Count() > 0)
            {
                foreach (var item in PatAppointments)
                {
                    PrintPatAppDetail(item);
                }
            }
            else
                Console.WriteLine("No Upcoming Appointments");

        }

        public void PrintPastAppDetailsByPatId(int patId)
        {
            DateTime today = DateTime.Today;

            var PatAppointments = apps
                .Where(e => e.Pat_Id == patId && e.App_Date.Date < today);
            if (PatAppointments.Count() > 0)
            {
                foreach (var item in PatAppointments)
                {
                    PrintPatAppDetail(item);
                }
            }
            else
                Console.WriteLine("No Previous Appointments/Records");
        }

        public void BookAppointmentByPatId(User user)
        {
            Appointment app = new Appointment();

            //Get Doctor
            int docId;
            User doctorUser;
            manageUser.PrintDoctorList();
            Console.WriteLine("Enter Doctor ID");
            while (!int.TryParse(Console.ReadLine(), out docId))
            {
                Console.WriteLine("Invalid entry for Doctor ID. Please try again...");
            }
            doctorUser = manageUser.GetDoctorById(docId);
            if (doctorUser == null)
            {
                Console.WriteLine("Doctor ID does not exist..");
                Console.WriteLine("Booking cancelled");
                return;
            }

            //Get Date
            DateTime choiceDate = app.GetAppointmentDate();

            //Check User duplicate Timeslot
            var UserAppointments = apps
            .Where(e => e.Pat_Id == user.User_Id && e.App_Date == choiceDate);
            if (UserAppointments.Count() > 0)
            {
                Console.WriteLine("Already book the same time slot");
                Console.WriteLine("Booking cancelled");
                return;
            }

            //Check Doctor Availibility
            var OpenAppointments = apps
            .Where(e => e.Doc_Id == docId && e.App_Date == choiceDate);
            if (OpenAppointments.Count() > 0)
            {
                Console.WriteLine("The Doctor is not available on the select date and time slot");
                Console.WriteLine("Booking cancelled");
                return;
            }
            else
            {
                Console.WriteLine("Please enter remarks");
                string remarks = Console.ReadLine();
                app.App_Date = choiceDate;
                app.Doc_Id = docId;
                app.App_Id = GenerateAppId();
                app.Pat_Id = user.User_Id;
                app.App_Outstanding = 0;
                app.App_PayStatus = 1;
                app.App_PatRemarks = remarks;
                apps.Add(app);
                Console.WriteLine("Booking Appointment Success! Please check upcoming appointment to review");
                return;
            }
        }

        public void MakePayment(User user)
        {
            int appId = GetAppIdFromUser();
            Appointment app = GetAppByUserIdNAppId(user, appId);
            if (app == null)
            {
                Console.WriteLine("Invalid Id. Cannot edit");
                return;
            }
            if (app.App_PayStatus != 2)
            {
                if (app.App_PayStatus == 1)
                    Console.WriteLine("Unable to make payment. The doctor have yet to raise payment charge");
                else
                    Console.WriteLine("Unable to make payment. The payment already settled");
                return;
            }
            PrintAppDetail(app);
            Console.WriteLine("Appointment above have been selected to make payment ");
            double payment;
            Console.WriteLine("Please enter the amount to make payment");
            while (!double.TryParse(Console.ReadLine(), out payment) || payment > app.App_Outstanding || payment < 0)
            {
                Console.WriteLine("Invalid entry for amount. Please try again...");
            }
            app.App_Outstanding = app.App_Outstanding - payment;
            if (app.App_Outstanding == 0)
                app.App_PayStatus = 3;

            PrintAppDetail(app);
            Console.WriteLine("Payment have been made to the appointment above");
        }

        #endregion

        #region Doctor Appointment
        public Appointment GetAppByDocId(int docId)
        {
            Appointment app = apps.Find(p => p.Doc_Id == docId);
            return app;
        }

        public void PrintAppDetailsByDocId(int docId)
        {
            var DocAppointments = apps
                .Where(e => e.Doc_Id == docId);
            foreach (var item in DocAppointments)
            {
                PrintDocAppDetail(item);
            }
        }

        public void PrintPastAppDetailsByDocId(int docId)
        {
            DateTime today = DateTime.Today;

            var DocAppointments = apps
                .Where(e => e.Doc_Id == docId && e.App_Date.Date < today);
            if (DocAppointments.Count() > 0)
            {
                foreach (var item in DocAppointments)
                {
                    PrintDocAppDetail(item);
                }
            }
            else
                Console.WriteLine("No Previous Appointments/Records");
        }

        public void PrintUpcomingAppDetailsByDocId(int docId)
        {
            DateTime today = DateTime.Today;

            var DocAppointments = apps
                .Where(e => e.Doc_Id == docId && e.App_Date.Date >= today);
            if (DocAppointments.Count() > 0)
            {
                foreach (var item in DocAppointments)
                {
                    PrintDocAppDetail(item);
                }
            }
            else
                Console.WriteLine("No Upcoming Appointments");
        }

        public void RaisePayment(User user)
        {
            int appId = GetAppIdFromUser();
            Appointment app = GetAppByUserIdNAppId(user, appId);
            if (app == null)
            {
                Console.WriteLine("Invalid Id. Cannot edit");
                return;
            }
            if (app.App_PayStatus != 1)
            {
                Console.WriteLine("Payment already Raised/Settled");
                return;
            }
            PrintAppDetail(app);
            Console.WriteLine("The Appointment above have been selected to raise payment request");
            double payment;
            Console.WriteLine("Please enter amount for the patient to pay");
            while (!double.TryParse(Console.ReadLine(), out payment) || payment < 0)
            {
                Console.WriteLine("Invalid entry for amount. Please try again...");
            }
            Console.WriteLine("Please enter remarks for patient");
            string remarks = Console.ReadLine();
            app.App_DocRemarks = remarks;
            app.App_Outstanding = payment;
            app.App_PayStatus = 2;

            PrintAppDetail(app);
            Console.WriteLine("Payment request raised for the appointment above");
        }
        #endregion

    }
}
