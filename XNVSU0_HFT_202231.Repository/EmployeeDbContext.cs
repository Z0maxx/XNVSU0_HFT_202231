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
                },
                new EventType()
                {
                    Id = 3,
                    Name = "Születésnapi Parti"
                },
                new EventType()
                {
                    Id = 4,
                    Name = "Érettségi Ünnepség"
                },
                new EventType()
                {
                    Id = 5,
                    Name = "Évzáró Ünnepség"
                }
            );
            modelBuilder.Entity<FixedWageEmployee>().HasData(
                new FixedWageEmployee()
                {
                    Id = 1,
                    FirstName = "János",
                    LastName = "Kis",
                    EmailAddress = "kis.janos27@gmail.com",
                    PhoneNumber = "06301571698",
                    HireDate = DateTime.Parse("2005.07.18"),
                    JobId = 1,
                    Wage = 35000,
                    Hours = 8
                },
                new FixedWageEmployee()
                {
                    Id = 2,
                    FirstName = "Béla",
                    LastName = "Nagy",
                    EmailAddress = "nagy.bela7534@gmail.com",
                    PhoneNumber = "06308453849",
                    HireDate = DateTime.Parse("2007.02.04"),
                    JobId = 2,
                    Wage = 19000,
                    Hours = 4
                },
                new FixedWageEmployee()
                {
                    Id = 3,
                    FirstName = "Károly",
                    LastName = "Erdős",
                    EmailAddress = "erdos.karoly6@gmail.com",
                    PhoneNumber = "06301654782",
                    HireDate = DateTime.Parse("1999.10.01"),
                    JobId = 1,
                    Wage = 26000,
                    Hours = 5.5
                },
                new FixedWageEmployee()
                {
                    Id = 4,
                    FirstName = "Ferenc",
                    LastName = "Mérő",
                    EmailAddress = "mero.ferenc125@gmail.com",
                    PhoneNumber = "06307489623",
                    HireDate = DateTime.Parse("2011.07.23"),
                    JobId = 1,
                    Wage = 24000,
                    Hours = 4.5
                },
                new FixedWageEmployee()
                {
                    Id = 5,
                    FirstName = "Viktor",
                    LastName = "Új",
                    EmailAddress = "uj.viktor04@gmail.com",
                    PhoneNumber = "06308465264",
                    HireDate = DateTime.Parse("2022.01.14"),
                    JobId = 2,
                    Wage = 15000,
                    Hours = 6
                }
            );
            modelBuilder.Entity<HourlyWageEmployee>().HasData(
                new HourlyWageEmployee()
                {
                    Id = 1,
                    FirstName = "Pál",
                    LastName ="Fehér",
                    EmailAddress = "feher.pal935@gmail.com",
                    PhoneNumber = "06307085389",
                    HireDate = DateTime.Parse("2016.04.17"),
                    Wage = 2500,
                    MinHours = 2.5,
                    MaxHours = 4,
                    JobId = 2
                },
                new HourlyWageEmployee()
                {
                    Id = 2,
                    FirstName = "Bence",
                    LastName = "Létrás",
                    EmailAddress = "letras.bence21@gmail.com",
                    PhoneNumber = "06304056753",
                    HireDate = DateTime.Parse("2012.11.21"),
                    Wage = 4000,
                    MinHours = 5,
                    MaxHours = 8.5,
                    JobId = 1
                },
                new HourlyWageEmployee()
                {
                    Id = 3,
                    FirstName = "Jenő",
                    LastName = "Horvát",
                    EmailAddress = "horvat.jeno986@gmail.com",
                    PhoneNumber = "06308490675",
                    HireDate = DateTime.Parse("2020.08.09"),
                    Wage = 1900,
                    MinHours = 1.5,
                    MaxHours = 6,
                    JobId = 2
                },
                new HourlyWageEmployee()
                {
                    Id = 4,
                    FirstName = "Ernő",
                    LastName = "Erős",
                    EmailAddress = "eros.erno9@gmail.com",
                    PhoneNumber = "06304539637",
                    HireDate = DateTime.Parse("2014.07.13"),
                    Wage = 3100,
                    MinHours = 4.5,
                    MaxHours = 9.5,
                    JobId = 2
                },
                new HourlyWageEmployee()
                {
                    Id = 5,
                    FirstName = "Dénes",
                    LastName = "Olvasó",
                    EmailAddress = "olvaso.denes81@gmail.com",
                    PhoneNumber = "06301023463",
                    HireDate = DateTime.Parse("2015.05.27"),
                    Wage = 3600,
                    MinHours = 5.5,
                    MaxHours = 8,
                    JobId = 1
                }
            );
            
            modelBuilder.Entity<FixedWageOrder>().HasData(
                new FixedWageOrder()
                {
                    Id = 1,
                    OrderDate = DateTime.Parse("2022.11.05"),
                    EmployeeId = 1,
                    EventTypeId = 1,
                    FirstName = "Géza",
                    LastName = "Sas",
                    EmailAddress = "sas.geza124@gmail.com"
                },
                new FixedWageOrder()
                {
                    Id = 2,
                    OrderDate = DateTime.Parse("2022.10.24"),
                    EmployeeId = 3,
                    EventTypeId = 1,
                    FirstName = "Áron",
                    LastName = "Kocsis",
                    EmailAddress = "kocsis.aron2541@gmail.com",
                    PhoneNumber = "06207794431"
                },
                new FixedWageOrder()
                {
                    Id = 3,
                    OrderDate = DateTime.Parse("2022.11.02"),
                    EmployeeId = 4,
                    EventTypeId = 2,
                    FirstName = "Bence",
                    LastName = "Festő",
                    EmailAddress = "festo.bence4@gmail.com",
                },
                new FixedWageOrder()
                {
                    Id = 4,
                    OrderDate = DateTime.Parse("2022.12.15"),
                    EmployeeId = 5,
                    EventTypeId = 4,
                    FirstName = "Ilona",
                    LastName = "Ugró",
                    EmailAddress = "ugro.ilona754@gmail.com",
                    PhoneNumber = "06201597538"
                },
                new FixedWageOrder()
                {
                    Id = 5,
                    OrderDate = DateTime.Parse("2022.11.16"),
                    EmployeeId = 2,
                    EventTypeId = 3,
                    FirstName = "Helga",
                    LastName = "Egres",
                    EmailAddress = "egres.helga32@gmail.com",
                    PhoneNumber = "06208468493"
                }
            );
            modelBuilder.Entity<HourlyWageOrder>().HasData(
                new HourlyWageOrder()
                {
                    Id = 1,
                    OrderDate = DateTime.Parse("2022.10.14"),
                    EmployeeId = 1,
                    Hours = 3,
                    FirstName = "Aladár",
                    LastName = "Mézes",
                    EmailAddress = "mezes.aladar78@gmail.com",
                    PhoneNumber = "06201267561"
                },
                new HourlyWageOrder()
                {
                    Id = 2,
                    OrderDate = DateTime.Parse("2022.12.07"),
                    EmployeeId = 4,
                    Hours = 7,
                    FirstName = "Lőrinc",
                    LastName = "Alvó",
                    EmailAddress = "alvo.lorinc257@gmail.com",
                },
                new HourlyWageOrder()
                {
                    Id = 3,
                    OrderDate = DateTime.Parse("2022.10.14"),
                    EmployeeId = 2,
                    Hours = 6,
                    FirstName = "Tibor",
                    LastName = "Kerti",
                    EmailAddress = "kerti.tibor67@gmail.com",
                    PhoneNumber = "06202304201"
                },
                new HourlyWageOrder()
                {
                    Id = 4,
                    OrderDate = DateTime.Parse("2022.10.14"),
                    EmployeeId = 5,
                    Hours = 7.5,
                    FirstName = "Örs",
                    LastName = "Tavi",
                    EmailAddress = "tavi.ors402@gmail.com",
                    PhoneNumber = "06205607164"
                },
                new HourlyWageOrder()
                {
                    Id = 5,
                    OrderDate = DateTime.Parse("2022.10.14"),
                    EmployeeId = 3,
                    Hours = 3,
                    FirstName = "Eszter",
                    LastName = "Pék",
                    EmailAddress = "pek.eszter0267@gmail.com",
                }
            );
        }
    }
}
