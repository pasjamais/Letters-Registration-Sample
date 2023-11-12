using DB_Interaction.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Interaction
{
    public partial class MailRegistrationContext : DbContext
    {

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Letter> Letters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DB_Agent.Get_Connection_String());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Letter>()
                .HasOne(l => l.Addressee)
                .WithMany(c => c.InboxLetters)
                .HasForeignKey(u => u.AddresseeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Letter>()
               .HasOne(l => l.Sender)
               .WithMany(c => c.OutgoingLetters)
               .HasForeignKey(u => u.SenderId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
