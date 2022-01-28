using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSModelsLibrary;

namespace CMSConsoleApp
{
    class Program
    {
        ManageAppointment menuApp = new ManageAppointment();
        ManageUser menuUser = new ManageUser();
        User user = new User();
        Appointment app = new Appointment(); 

        void manageHomepage()
        {
            Console.WriteLine("Welcome to Clinic Management System (CMS) Console App");
            int choice = 0;
            do
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Select user type to login");
                Console.WriteLine("1: Patient");
                Console.WriteLine("2: Doctor");
                Console.WriteLine("0: Exit");
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Try again. Please enter a number");
                }
                try
                {
                    switch (choice)
                    {
                        case 1:
                            user.UserLogin();
                            var userLogin = menuUser.GetUserByIdNPass(user);
                            if (userLogin == null)
                            {
                                Console.WriteLine("Invalid User Id and/or Password");
                                break;
                            }
                            else if (userLogin.User_Type !=1)
                            {
                                Console.WriteLine("Invalid User Type, please choose the correct user type");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Hello there {0}", userLogin.User_Name);
                                manageAppointment(userLogin);
                                break;
                            }
                        case 2:
                            user.UserLogin();
                            var userLogin2 = menuUser.GetUserByIdNPass(user);
                            if (userLogin2 == null)
                            {
                                Console.WriteLine("Invalid User Id and/or Password");
                                break;
                            }
                            else if (userLogin2.User_Type != 2)
                            {
                                Console.WriteLine("Invalid User Type, please choose the correct user type");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Hello there Dr.{0}", userLogin2.User_Name);
                                manageAppointment(userLogin2);
                                break;
                            }
                        case 0:
                            Console.WriteLine("Thank you and good bye...");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again");
                            break;
                    }
                    Console.WriteLine("-------------------------");
                }
                catch (NullReferenceException nre)
                {
                    Console.WriteLine("Null by mistake");
                    Console.WriteLine(nre.Message);
                }
                catch (ArgumentOutOfRangeException aore)
                {
                    Console.WriteLine("The user could not be found");
                    Console.WriteLine(aore.Message);
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("expecting a number");
                    Console.WriteLine(fe.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong");
                    Console.WriteLine(e.Message);
                }
            } while (choice != 0);
        }

        void manageAppointment(User user)
        {
            int choice = 0;
            if (user.User_Type == 1) //Patient
            {
                do
                {
                    Console.WriteLine("-------------------------");
                    Console.WriteLine("1: Book Appointment");
                    Console.WriteLine("2: Pay Appointment");
                    Console.WriteLine("3: View Upcoming Appointment");
                    Console.WriteLine("4: View Past Appointment");
                    Console.WriteLine("5: View Profile Detail");
                    Console.WriteLine("0: Exit");
                    while (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Try again. Please enter a number");
                    }
                    try
                    {
                        switch (choice)
                        {
                            case 1:
                                menuApp.BookAppointmentByPatId(user);
                                break;
                            case 2:
                                menuApp.MakePayment(user);
                                break;
                            case 3:
                                menuApp.PrintUpcomingAppDetailsByPatId(user.User_Id);
                                break;
                            case 4:
                                menuApp.PrintPastAppDetailsByPatId(user.User_Id);
                                break;
                            case 5:
                                user.PrintUserDetail(user);
                                break;
                            case 0:
                                Console.WriteLine("Going back...");
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again");
                                break;
                        }
                        Console.WriteLine("-------------------------");
                    }
                    catch (NullReferenceException nre)
                    {
                        Console.WriteLine("Null by mistake");
                        Console.WriteLine(nre.Message);
                    }
                    catch (ArgumentOutOfRangeException aore)
                    {
                        Console.WriteLine("The appointment could not be found");
                        Console.WriteLine(aore.Message);
                    }
                    catch (FormatException fe)
                    {
                        Console.WriteLine("expecting a number");
                        Console.WriteLine(fe.Message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Something went wrong");
                        Console.WriteLine(e.Message);
                    }
                } while (choice != 0);
            }
            else // type = 2 "Doctor"
            {
                do
                {
                    Console.WriteLine("-------------------------");
                    Console.WriteLine("1: View Upcoming Appointment");
                    Console.WriteLine("2: View Past Appointmen/Records");
                    Console.WriteLine("3: Raise payment for Appointment");
                    Console.WriteLine("4: View Profile Detail");
                    Console.WriteLine("0: Exit");
                    while (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Try again. Please enter a number");
                    }
                    try
                    {
                        switch (choice)
                        {
                            case 1:
                                menuApp.PrintUpcomingAppDetailsByDocId(user.User_Id);
                                break;
                            case 2:
                                menuApp.PrintPastAppDetailsByDocId(user.User_Id);
                                break;
                            case 3:
                                menuApp.RaisePayment(user);
                                break;
                            case 4:
                                user.PrintDocDetail(user);
                                break;
                            case 0:
                                Console.WriteLine("Going back...");
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again");
                                break;
                        }
                        Console.WriteLine("-------------------------");
                    }
                    catch (NullReferenceException nre)
                    {
                        Console.WriteLine("Null by mistake");
                        Console.WriteLine(nre.Message);
                    }
                    catch (ArgumentOutOfRangeException aore)
                    {
                        Console.WriteLine("The appointment could not be found");
                        Console.WriteLine(aore.Message);
                    }
                    catch (FormatException fe)
                    {
                        Console.WriteLine("expecting a number");
                        Console.WriteLine(fe.Message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Something went wrong");
                        Console.WriteLine(e.Message);
                    }
                } while (choice != 0);
            }
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.manageHomepage();

            Console.ReadKey();
        }
      
    }
}
