using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSModelsLibrary;

namespace CMSConsoleApp
{
    public class ManageUser
    {
        List<User> users = new List<User>();

        public ManageUser()
        {
            users.Add(new User
            {
                User_Id = 101, User_Name = "Bob",
                User_Password = "abcd1234", User_Type = 1
            });

            users.Add(new User
            {
                User_Id = 103, User_Name = "Ash",
                User_Password = "abcd1234", User_Type = 1
            });

            users.Add(new User
            {
                User_Id = 102, User_Name = "Sarah",
                User_Password = "abcd1234", User_Type = 2,
                User_Experience = 5, User_Specialization = "Family Physician"
            });

            users.Add(new User
            {
                User_Id = 104,User_Name = "Liam",
                User_Password = "abcd1234", User_Type = 2,
                User_Experience = 4, User_Specialization = "Cardiologist"
            });
        }

        public User GetUserById(int id)
        {
            User user = users.Find(p => p.User_Id == id);
            return user;
        }

        public User GetUserByIdNPass(User user)
        {
            User userLogin = users.Find(p => p.User_Id == user.User_Id && p.User_Password == user.User_Password);
            return userLogin;
        }

        public void PrintAllUserDetails()
        {
            var sortedUsers = users.OrderBy(p => p.User_Id);
            foreach (var item in sortedUsers)
            {
                if (item != null)
                    PrintUserDetail(item);
            }
        }

        public User GetDoctorById(int docId)
        {
            User user = users.Find(p => p.User_Id == docId && p.User_Type == 2);
            return user;
        }

        public void PrintDoctorList()
        {
            var DoctorList = users
                .Where(e => e.User_Type == 2)
                .OrderBy(e => e.User_Id)
                .Select(
                e => new
                {
                    UserID = e.User_Id,
                    UserName = e.User_Name,
                    Experience = e.User_Experience,
                    Special = e.User_Specialization
                });
            Console.WriteLine("-------------------------");
            Console.WriteLine("Plese enter Doctor ID (on the far left)");
            foreach (var item in DoctorList)
            {
                Console.WriteLine("["+item.UserID + "] Dr. [" + item.UserName+ "] specialize in [" + item.Special
                    + "] with [" + item.Experience + "] years of experience");
            }
            Console.WriteLine("-------------------------");
        }

        private void PrintUserDetail(User item)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine(item);
        }

    }
}
