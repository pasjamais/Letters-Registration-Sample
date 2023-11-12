using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DB_Interaction.Entities
{
    public partial class Letter
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public int? SenderId { get; set; }
        [ForeignKey("SenderId")]
        public User? Sender { get; set; }
        public int? AddresseeId { get; set; }
        [ForeignKey("AddresseeId")]
        public User? Addressee { get; set; }
    }
}
