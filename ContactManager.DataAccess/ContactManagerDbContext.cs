using ContactManager.DataAccess.Models;
using ContactManager.DataAccess.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.DataAccess
{
    public class ContactManagerDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public ContactManagerDbContext(DbContextOptions options) : base(options)
        {

        }

        public ContactManagerDbContext() : base()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PersonConfiguration());
        }
    }
}
