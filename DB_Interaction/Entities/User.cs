using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Interaction.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email_Address { get; set; }

        public List<Letter> InboxLetters { get; set; } = new();
        public List<Letter> OutgoingLetters { get; set; } = new();

    }
}
