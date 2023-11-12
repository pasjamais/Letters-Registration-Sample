using Azure.Core;
using Azure;
using DB_Interaction.Entities;
using DB_Interaction.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DB_Interaction
{
    public class DB_Agent
    {
        public static MailRegistrationContext db = new MailRegistrationContext();
        public static string Get_Connection_String()
        {
          return IniFile.Get_Value_from_Settings_File("settings.ini", "ConnectionString", "CONNECTION");
        }
        public static List<Letter> Get_Letters()
        {
            return db.Letters.ToList();
        }

        public static List<User> Get_Users()
        {
            return db.Users.ToList();
        }
        public static List<Letter_Caption> Get_Letter_Captions()
        {
            List<Letter_Caption> captions = Get_Letters().Select(l => new Letter_Caption
            {
                Id = l.Id,
                Date = l.Date,
                Subject = l.Subject,
                Sender = l.Sender is not null ? l.Sender.Name : "",
                Addressee = l.Addressee is not null ? l.Addressee.Name : ""
            }).ToList();
            return captions;
        }

        public static List<User_Caption> Get_User_Captions()
        {
            List<User_Caption> captions = Get_Users().Select(u => new User_Caption
            {
                Id = u.Id,
                Name = u.Name,
                Email_Address = u.Email_Address
            }).ToList();
            return captions;
        }
        public static Letter? Get_Letter_by_ID(int id)
        {
            return db.Letters.FindAsync(id).Result;
        }
        public static User? Get_User_by_ID(int id)
        {
            return db.Users.FindAsync(id).Result;
        }

        public static void Save_Changes()
        {
            db.SaveChanges();
        }
        public static int? Add_User(User item)
        {
            db.Users.Add(item);
            Save_Changes();
            return item.Id;
        }
        public static int? Add_Letter(Letter item)
        {
            db.Letters.Add(item);
            Save_Changes();
            return item.Id;
        }

        public static void Delete_User(int id)
        {
               db.Users.Remove(Get_User_by_ID(id));
               Save_Changes();
        }

        public static void Delete_Letter(Letter item)
        {
            db.Letters.Remove(item);
            Save_Changes();
        }

        public static void RecreateDB()
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }

        public static void Initialize_BD_with_Default_Values()
        {
            User user1 = new User { Name = "Bob", Email_Address = "Bob@mail.com" };
            User user2 = new User { Name = "Pierre", Email_Address = "Pierre@mail.fr" };
            User user3 = new User { Name = "Maria", Email_Address = "Maria@mail.com" };
            User user4 = new User { Name = "Anna", Email_Address = "Anya@mail.ru" };
            User user5 = new User { Name = "Fyodor", Email_Address = "Fedya@mail.ru" };
            User user6 = new User { Name = "Luise", Email_Address = "Luise@mail.fr" };
            Letter l1 = new Letter { Addressee = user1, Sender = user2, Content = "This is interesting. Extending a default style for a specific scope works if it all happens within one file. But if I define a default style for a whole application and I want to extend it in one specific file this is not possible?", Date = DateTime.Now, Subject = "Attention!" };
            Letter l2 = new Letter { Addressee = user2, Sender = user3, Content = "Along with the request, the application may receive various data. And to get this data, we can use different methods. The most common way is to use parameters.\r\n\r\nDefining parameters in controller methods is no different from defining parameters in C#. For example, let's define the following method in the controller:", Date = DateTime.Now, Subject = "Receiving data" };
            Letter l3 = new Letter { Addressee = user3, Sender = user4, Content = "It depends... files are not a good description of XAML resource scopes... if you merge that one specific file into the same scope as your whole application style, then it won't work. If you merge one specific file into the resources of some nested framework element then it will work", Date = DateTime.Now, Subject = "Extend style without setting" };
            db.Users.AddRange(user1, user2, user3, user4, user5, user6);
            db.Letters.AddRange(l1, l2, l3);
            db.SaveChanges();
        }

    }
}
