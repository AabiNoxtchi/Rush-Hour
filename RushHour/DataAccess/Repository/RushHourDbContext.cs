using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class RushHourDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Activity> Activities { get; set; }

        public RushHourDbContext() : base("RushHourDbConnection")
        {
            this.Users = this.Set<User>();
            this.Appointments = this.Set<Appointment>();
            this.Activities = this.Set<Activity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                 .HasIndex(u => u.Email)
                 .IsUnique();

            modelBuilder.Entity<Appointment>()
                .Property(a => a.StartDateTime)
                .HasColumnType("datetime2")
                .HasPrecision(0);

            modelBuilder.Entity<Appointment>()
               .Property(a => a.EndDateTime)
               .HasColumnType("datetime2")
               .HasPrecision(0);

            modelBuilder.Entity<Activity>()
               .Property(a =>a.Price )
               .HasColumnType("Money");
        }  
    }
}
