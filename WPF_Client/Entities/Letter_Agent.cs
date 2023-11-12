using DB_Interaction.Entities;
using DB_Interaction.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Client.Entities
{
    public class Letter_Agent
    {
        public static string Create_Letter(Letter letter)
        {
            return Server_Agent.Create_Letter(letter);
        }
        public static List<Letter_Caption> Get_Letter_Captions(out string control_message)
        {
            return Server_Agent.Get_Letter_Captions(out control_message);
        }
        public static Letter Get_Letter_by_ID(int id, out string control_message)
        {
            return Server_Agent.Get_Letter_by_ID(id, out control_message);
        }
    }
}
