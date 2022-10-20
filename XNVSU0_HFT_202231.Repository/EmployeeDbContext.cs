using Microsoft.EntityFrameworkCore;
using System;
using XNVSU0_HFT_202231.Models;
using System.Linq;

namespace XNVSU0_HFT_202231.Repository
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<FixedWageEmployee> FixedWageEmployees { get; set; }
        public DbSet<HourlyWageEmployee> HourlyWageEmployees { get; set; }
        public DbSet<FixedWageOrder> FixedWageOrders { get; set; }
        public DbSet<HourlyWageOrder> HourlyWageOrders { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public EmployeeDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase("employees");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FixedWageEmployee>(emp => emp
                .HasOne(emp => emp.Job)
                .WithMany(job => job.FixedWageEmployees)
                .HasForeignKey(emp => emp.JobId)
            );
            modelBuilder.Entity<HourlyWageEmployee>(emp => emp
                .HasOne(emp => emp.Job)
                .WithMany(job => job.HourlyWageEmployees)
                .HasForeignKey(emp => emp.JobId)
            );
            modelBuilder.Entity<FixedWageOrder>(order => order
                .HasOne(order => order.EventType)
                .WithMany(eventType => eventType.Orders)
                .HasForeignKey(order => order.EventTypeId)
            );
            modelBuilder.Entity<FixedWageOrder>(order => order
                .HasOne(order => order.Employee)
                .WithMany(emp => emp.Orders)
                .HasForeignKey(order => order.EmployeeId)
            );
            modelBuilder.Entity<HourlyWageOrder>(order => order
                .HasOne(order => order.Employee)
                .WithMany(emp => emp.Orders)
                .HasForeignKey(order => order.EmployeeId)
            );

            modelBuilder.Entity<Job>().HasData(
                new Job()
                {
                    Id = 1,
                    Name = "Séf"
                },
                new Job()
                {
                    Id = 2,
                    Name = "Italkeverő"
                }
            );
            modelBuilder.Entity<EventType>().HasData(
                new EventType()
                {
                    Id = 1,
                    Name = "Esküvő"
                },
                new EventType()
                {
                    Id = 2,
                    Name = "Osztálytalálkozó"
                }
            );
            modelBuilder.Entity<FixedWageEmployee>().HasData(
                new FixedWageEmployee()
                {
                    Id = 1,
                    FirstName = "János",
                    LastName = "Kis",
                    HireDate = DateTime.Now,
                    JobId = 1
                }
            );
            modelBuilder.Entity<HourlyWageEmployee>().HasData(
                new HourlyWageEmployee()
                {
                    Id = 1,
                    FirstName = "Pál",
                    LastName ="Fehér",
                    HireDate = DateTime.Now,
                    JobId = 2
                }
            );
            
            modelBuilder.Entity<FixedWageOrder>().HasData(
                new FixedWageOrder()
                {
                    Id = 1,
                    OrderDate = DateTime.Now,
                    EmployeeId = 1,
                    EventTypeId = 1
                }
            );
            modelBuilder.Entity<HourlyWageOrder>().HasData(
                new HourlyWageOrder()
                {
                    Id = 1,
                    OrderDate = DateTime.Now,
                    EmployeeId = 1,
                    Hours = 5
                }
            );
        }
    }
}
