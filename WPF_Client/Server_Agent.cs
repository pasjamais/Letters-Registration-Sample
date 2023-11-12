using Azure.Core;
using DB_Interaction.Entities;
using DB_Interaction.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using DB_Interaction;

namespace WPF_Client
{
    public class Server_Agent
    {
        private static string server_address = Get_Server_Address();
        public static string Get_Server_Address()
        {
            return IniFile.Get_Value_from_Settings_File("settings.ini", "Server", "CONNECTION");
        }
        public static void Set_Server_Address(string address_with_port)
        {
            server_address = address_with_port;
            IniFile.Set_Value_into_Settings_File("settings.ini", address_with_port, "Server", "CONNECTION");
        }

        public static T Get_Data_Async<T>(string apiUrl, out string control_message, int id = 0) where T : new()
        {
            string complete_apiUrl;
            if (id != 0)
                complete_apiUrl = $"{server_address}/{apiUrl}/{id}";
            else
                complete_apiUrl = $"{server_address}/{apiUrl}";
            T item = new T();
            try
            {
                var response = new HttpClient().GetAsync(complete_apiUrl).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                item = JsonConvert.DeserializeObject<T>(result);
                control_message = "";
            }
            catch (Exception e)
            {
                control_message = $"\n{e.Message}";
            }
            return item;
        }
        public static List<Letter_Caption> Get_Letter_Captions(out string control_message)
        {
            return Get_Data_Async<List<Letter_Caption>>("letter", out control_message);
        }

        public static Letter Get_Letter_by_ID(int id, out string control_message)
        {
            return Get_Data_Async<Letter>("letter", out control_message, id);
        }

        public static List<User> Get_Users(out string control_message)
        {
            return Get_Data_Async<List<User>>("user", out control_message).OrderBy(x => x.Name).ToList();
        }
        public static List<User_Caption> Get_User_Captions(out string control_message)
        {
            return Get_Data_Async<List<User_Caption>>("user", out control_message);
        }

        public static string Post_Async<T>(string apiUrl, T item)
        {
            JsonContent content = JsonContent.Create(item);
            string result;
            try
            {
                var response = new HttpClient().PostAsync($"{server_address}/{apiUrl}", content).Result;
                result = response.Content.ReadAsStringAsync().Result;
                if (result != "")
                    return result;
                else return "\nError";
            }
            catch (Exception e)
            {
                return $"\n{e.Message}";
            }
        }

        public static string Create_User(User user)
        {
            return Post_Async<User>("user", user);
        }

        public static string Create_Letter(Letter letter)
        {
            return Post_Async<Letter>("letter", letter);
        }

        public static string Delete_User(int id)
        {
            string apiUrl = $"{server_address}/user/{id}";
            string result;
            try
            {
                var response = new HttpClient().DeleteAsync(apiUrl).Result;
                result = response.Content.ReadAsStringAsync().Result;
                if (result != "")
                    return result;
                else return "\nError";
            }
            catch (Exception e)
            {
                return $"\n{e.Message}";
            }
        }

        public static string Initialize_BD_with_Default_Values(string keyword)
        {
            string apiUrl = $"{server_address}/system/{keyword}";
            string result;
            try
            {
                var response = new HttpClient().GetAsync(apiUrl).Result;
                result = response.Content.ReadAsStringAsync().Result;
                if (result != "")
                    return result;
                else return "\nError";
            }
            catch (Exception e)
            {
                return $"\n{e.Message}";
            };
        }


    }
}
