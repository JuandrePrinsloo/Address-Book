using AddressBook1.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBook1.Database
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //declare foreignkeys etc..

            modelBuilder.Entity<Contact>().HasMany(x => x.PhoneNumbers).WithOne(x => x.Contact);

            modelBuilder.Entity<Contact>().HasMany(x => x.ContactEmailAddress).WithOne(x => x.Contact);
        }

        //declare dbsets

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<PhoneNumbers> Numbers { get; set; }

        public DbSet<ContactEmailAddress> Emails { get; set; }
    }
}