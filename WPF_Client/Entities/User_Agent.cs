using DB_Interaction.Entities;
using DB_Interaction.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Client.Entities
{
    public class User_Agent
    {
        public static bool Create_User(string name, string email, out string control_message)
        {
            if (IsValidEmail(email))
            {
                User user = new User();
                user.Name = name;
                user.Email_Address = email;
                control_message = Server_Agent.Create_User(user);
                return true;
            }
            else
            {
                control_message = "\nНекорректно указана электронная почта";
                return false;
            }
        }

        private static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith("."))
                return false;
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static List<User_Caption> Get_User_Captions(out string control_message)
        {
            return Server_Agent.Get_User_Captions(out control_message);
        }

        public static string Delete_User(int id)
        {
            return Server_Agent.Delete_User(id);
        }
    }
}
