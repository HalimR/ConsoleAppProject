using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSModelsLibrary
{
    class Patient : User
    {
        public override void PrintUserDetail(User user)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine(user);
            Console.WriteLine("-------------------------");
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
