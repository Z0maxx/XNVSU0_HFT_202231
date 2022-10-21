using NUnit.Framework;
using Moq;
using System;
using XNVSU0_HFT_202231.Repository;
using XNVSU0_HFT_202231.Models;
using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Logic;
using System.ComponentModel.DataAnnotations;

namespace XNVSU0_HFT_202231.Test
{
    [TestFixture]
    public class Tester
    {
        Mock<IRepository<Job>> JobRepository;
        Mock<IRepository<EventType>> EventTypeRepository;
        Mock<IRepository<FixedWageEmployee>> FixedWageEmployeeRepository;
        Mock<IRepository<HourlyWageEmployee>> HourlyWageEmployeeRepository;
        Mock<IRepository<FixedWageOrder>> FixedWageOrderRepository;
        Mock<IRepository<HourlyWageOrder>> HourlyWageOrderRepository;

        JobLogic JobLogic;
        EventTypeLogic EventTypeLogic;
        FixedWageEmployeeLogic FixedWageEmployeeLogic;
        HourlyWageEmployeeLogic HourlyWageEmployeeLogic;
        FixedWageOrderLogic FixedWageOrderLogic;
        HourlyWageOrderLogic HourlyWageOrderLogic;

        IQueryable<Job> Jobs;
        IQueryable<EventType> EventTypes;
        IQueryable<FixedWageEmployee> FixedWageEmployees;
        IQueryable<HourlyWageEmployee> HourlyWageEmployees;
        IQueryable<FixedWageOrder> FixedWageOrders;
        IQueryable<HourlyWageOrder> HourlyWageOrders;

        [SetUp]
        public void Init()
        {
            Jobs = new List<Job>()
            {
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
            }.AsQueryable();
            EventTypes = new List<EventType>()
            {
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
            }.AsQueryable();
            FixedWageEmployees = new List<FixedWageEmployee>()
            {
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
            }.AsQueryable();
            HourlyWageEmployees = new List<HourlyWageEmployee>()
            {
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
            }.AsQueryable();
            FixedWageOrders = new List<FixedWageOrder>()
            {
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
            }.AsQueryable();
            HourlyWageOrders = new List<HourlyWageOrder>()
            {
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
            }.AsQueryable();

            JobRepository = new();
            EventTypeRepository = new();
            FixedWageEmployeeRepository = new();
            HourlyWageEmployeeRepository = new();
            FixedWageOrderRepository = new();
            HourlyWageOrderRepository = new();

            JobRepository.Setup(r => r.ReadAll()).Returns(Jobs);
            EventTypeRepository.Setup(r => r.ReadAll()).Returns(EventTypes);
            FixedWageEmployeeRepository.Setup(r => r.ReadAll()).Returns(FixedWageEmployees);
            HourlyWageEmployeeRepository.Setup(r => r.ReadAll()).Returns(HourlyWageEmployees);
            FixedWageOrderRepository.Setup(r => r.ReadAll()).Returns(FixedWageOrders);
            HourlyWageOrderRepository.Setup(r => r.ReadAll()).Returns(HourlyWageOrders);

            JobRepository.Setup(r => r.Read(It.IsAny<int>())).Returns((int id) => Jobs.FirstOrDefault(job => job.Id == id));
            EventTypeRepository.Setup(r => r.Read(It.IsAny<int>())).Returns((int id) => EventTypes.FirstOrDefault(eventType => eventType.Id == id));
            FixedWageEmployeeRepository.Setup(r => r.Read(It.IsAny<int>())).Returns((int id) => FixedWageEmployees.FirstOrDefault(emp => emp.Id == id));
            HourlyWageEmployeeRepository.Setup(r => r.Read(It.IsAny<int>())).Returns((int id) => HourlyWageEmployees.FirstOrDefault(emp => emp.Id == id));
            FixedWageOrderRepository.Setup(r => r.Read(It.IsAny<int>())).Returns((int id) => FixedWageOrders.FirstOrDefault(order => order.Id == id));
            HourlyWageOrderRepository.Setup(r => r.Read(It.IsAny<int>())).Returns((int id) => HourlyWageOrders.FirstOrDefault(order => order.Id == id));

            JobLogic = new(JobRepository.Object);
            EventTypeLogic = new(EventTypeRepository.Object);
            FixedWageEmployeeLogic = new(FixedWageEmployeeRepository.Object);
            HourlyWageEmployeeLogic = new(HourlyWageEmployeeRepository.Object);
            FixedWageOrderLogic = new(FixedWageOrderRepository.Object);
            HourlyWageOrderLogic = new(HourlyWageOrderRepository.Object);
        }
        [Test]
        public void CreateInvalidJob1()
        {
            var job = new Job()
            {
                Id = 1,
                Name = "Segítő"
            };
            Assert.Throws<ArgumentException>(() => JobLogic.Create(job));
        }
        [Test]
        public void CreateInvalidJob2()
        {
            var job = new Job();
            Assert.Throws<ValidationException>(() => JobLogic.Create(job));
        }
        [Test]
        public void CreateInvalidJob3()
        {
            var job = new Job()
            {
                Name = "A"
            };
            Assert.Throws<ValidationException>(() => JobLogic.Create(job));
        }
        [Test]
        public void CreateValidJob()
        {
            var job = new Job()
            {
                Name = "Segítő"
            };
            JobLogic.Create(job);
            JobRepository.Verify(r => r.Create(job), Times.Once);
        }
        [Test]
        public void GetInvalidJob()
        {
            Assert.Throws<ArgumentException>(() => JobLogic.Read(0));
        }
        [Test]
        public void GetValidJob()
        {
            var job = new Job()
            {
                Id = 1,
                Name = "Séf"
            };
            Assert.That(JobLogic.Read(1), Is.EqualTo(job));
        }
        [Test]
        public void UpdateInvalidJob()
        {
            var job = new Job()
            {
                Id = 0,
                Name = "Segítő"
            };
            Assert.Throws<ArgumentException>(() => JobLogic.Update(job));
        }
        [Test]
        public void UpdateValidJob()
        {
            var job = new Job()
            {
                Id = 1,
                Name = "Segítő"
            };
            JobLogic.Update(job);
            JobRepository.Verify(r => r.Update(job), Times.Once);
        }
        [Test]
        public void DeleteInvalidJob()
        {
            Assert.Throws<ArgumentException>(() => JobLogic.Delete(0));
        }
        [Test]
        public void DeleteValidJob()
        {
            JobLogic.Delete(1);
            JobRepository.Verify(r => r.Delete(1), Times.Once);
        }
        [Test]
        public void CreateInvalidEmployee1()
        {
            var emp = new FixedWageEmployee()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                HireDate = DateTime.Now,
                Wage = 1
            };
            Assert.Throws<ValidationException>(() => FixedWageEmployeeLogic.Create(emp));
        }
        [Test]
        public void CreateValidEmployee1()
        {
            var emp = new FixedWageEmployee()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                HireDate = DateTime.Now,
                Wage = 5000
            };
            FixedWageEmployeeLogic.Create(emp);
            FixedWageEmployeeRepository.Verify(r => r.Create(emp), Times.Once);
        }
        [Test]
        public void CreateInvalidEmployee2()
        {
            var emp = new HourlyWageEmployee()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                HireDate = DateTime.Now,
                MinHours = 5,
                MaxHours = 4
            };
            Assert.Throws<ArgumentException>(() => HourlyWageEmployeeLogic.Create(emp));
        }
        [Test]
        public void CreateValidEmployee2()
        {
            var emp = new HourlyWageEmployee()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                HireDate = DateTime.Now,
                MinHours = 5,
                MaxHours = 8
            };
            HourlyWageEmployeeLogic.Create(emp);
            HourlyWageEmployeeRepository.Verify(r => r.Create(emp), Times.Once);
        }
        [Test]
        public void CreateInvalidOrder()
        {
            var order = new HourlyWageOrder()
            {
                Id = 6,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Now,
                Hours = 8,
                EmployeeId = 1,
                Employee = HourlyWageEmployees.First(emp => emp.Id == 1)
            };
            Assert.Throws<ArgumentException>(() => HourlyWageOrderLogic.Create(order));
        }
        [Test]
        public void UpdateInvalidOrder()
        {
            var order = new HourlyWageOrder()
            {
                Id = 6,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Now,
                Hours = 8,
                EmployeeId = 1,
                Employee = HourlyWageEmployees.First(emp => emp.Id == 1)
            };
            Assert.Throws<ArgumentException>(() => HourlyWageOrderLogic.Update(order));
        }
        [Test]
        public void CreateValidOrder()
        {
            var order = new HourlyWageOrder()
            {
                Id = 6,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Now,
                Hours = 3,
                EmployeeId = 1,
                Employee = HourlyWageEmployees.First(emp => emp.Id == 1)
            };
            HourlyWageOrderLogic.Create(order);
            HourlyWageOrderRepository.Verify(r => r.Create(order), Times.Once);
        }
        [Test]
        public void UpdateValidOrder()
        {
            var order = new HourlyWageOrder()
            {
                Id = 1,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Now,
                Hours = 3,
                EmployeeId = 1,
                Employee = HourlyWageEmployees.First(emp => emp.Id == 1)
            };
            HourlyWageOrderLogic.Update(order);
            HourlyWageOrderRepository.Verify(r => r.Update(order), Times.Once);
        }
    }
}
