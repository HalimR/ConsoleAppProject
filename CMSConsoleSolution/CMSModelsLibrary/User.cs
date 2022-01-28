using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSModelsLibrary;

namespace CMSModelsLibrary
{
    
    public class User
    {
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public string userPassword { get; set; }
        public int User_Type { get; set; }
        //1 - Patient, 2 - Doctor
        public int User_Experience { get; set; }
        public string User_Specialization { get; set; }
        public string User_Password
        {
            get
            {
                string masked = "XXXXXX" + userPassword.Substring(6, 2);
                return masked;
            }
            set
            {
                userPassword = value;
            }
        }

        public User()
        {

        }

        public void UserLogin()
        {
            int userId;
            string userPass;
            Console.WriteLine("-------------------------");
            Console.WriteLine("Please enter User ID");
            while (!int.TryParse(Console.ReadLine(), out userId))
            {
                Console.WriteLine("Try again. Please enter a number");
            }
            User_Id = userId;
            Console.WriteLine("Please enter User Password");
            userPass = Console.ReadLine();
            while (userPass.Length < 8 || userPass.Length > 8)
            {
                Console.WriteLine("Try again. The password is 8 character long");
                userPass = Console.ReadLine();
            }
            User_Password = userPass;
            Console.WriteLine("-------------------------");
        }

        public virtual void PrintUserDetail(User user)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine(user);
        }

        public virtual void PrintDocDetail(User user)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Doctor ID : " + user.User_Id);
            Console.WriteLine("Doctor Name : " + user.User_Name);
            Console.WriteLine("Password : " + user.User_Password);
            Console.WriteLine("Specialization : " + user.User_Specialization);
            Console.WriteLine("Years of Experience : " + user.User_Experience);
            Console.WriteLine("User Type: " + (User_Type == 1 ? "Patient" : "Doctor"));
        }

        public override string ToString()
        {
            return "User ID: " + User_Id
                + "\nUser Name: " + User_Name
                + "\nUser Password: " + User_Password
                + "\nUser Type: " + (User_Type == 1 ? "Patient" : "Doctor");
        }
    }
}
