using Moq;
using System;
using XNVSU0_HFT_202231.Repository;
using XNVSU0_HFT_202231.Models.TableModels;
using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Logic;

namespace XNVSU0_HFT_202231.Test
{
    static class Helper
    {
        public static IQueryable<Job> Jobs = new List<Job>()
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
        public static Mock<IRepository<Job>> JobRepository = SetupJobRepository();
        public static JobLogic JobLogic = new(JobRepository.Object);

        public static IQueryable<EventType> EventTypes = new List<EventType>()
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
        public static Mock<IRepository<EventType>> EventTypeRepository = SetupEventTypeRepository();
        public static EventTypeLogic EventTypeLogic = new(EventTypeRepository.Object);

        public static IQueryable<FixedWageEmployee> FixedWageEmployees = new List<FixedWageEmployee>()
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
                    HireDate = DateTime.Parse("2020.01.14"),
                    JobId = 2,
                    Wage = 15000,
                    Hours = 6
                }
            }.AsQueryable();
        public static Mock<IRepository<FixedWageEmployee>> FixedWageEmployeeRepository = SetupFixedWageEmployeeRepository();
        public static FixedWageEmployeeLogic FixedWageEmployeeLogic = new(FixedWageEmployeeRepository.Object, JobRepository.Object);

        public static IQueryable<HourlyWageEmployee> HourlyWageEmployees = new List<HourlyWageEmployee>()
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
        public static Mock<IRepository<HourlyWageEmployee>> HourlyWageEmployeeRepository = SetupHourlyWageEmployeeRepository();
        public static HourlyWageEmployeeLogic HourlyWageEmployeeLogic = new(HourlyWageEmployeeRepository.Object, JobRepository.Object);

        public static IQueryable<FixedWageOrder> FixedWageOrders = new List<FixedWageOrder>()
            {
                new FixedWageOrder()
                {
                    Id = 1,
                    OrderDate = DateTime.Parse("2021.11.05"),
                    EmployeeId = 1,
                    EventTypeId = 1,
                    FirstName = "Géza",
                    LastName = "Sas",
                    EmailAddress = "sas.geza124@gmail.com"
                },
                new FixedWageOrder()
                {
                    Id = 2,
                    OrderDate = DateTime.Parse("2021.10.24"),
                    EmployeeId = 2,
                    EventTypeId = 1,
                    FirstName = "Áron",
                    LastName = "Kocsis",
                    EmailAddress = "kocsis.aron2541@gmail.com",
                    PhoneNumber = "06207794431"
                },
                new FixedWageOrder()
                {
                    Id = 3,
                    OrderDate = DateTime.Parse("2021.11.02"),
                    EmployeeId = 1,
                    EventTypeId = 2,
                    FirstName = "Bence",
                    LastName = "Festő",
                    EmailAddress = "festo.bence4@gmail.com",
                },
                new FixedWageOrder()
                {
                    Id = 4,
                    OrderDate = DateTime.Parse("2020.12.15"),
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
                    OrderDate = DateTime.Parse("2021.11.16"),
                    EmployeeId = 2,
                    EventTypeId = 3,
                    FirstName = "Helga",
                    LastName = "Egres",
                    EmailAddress = "egres.helga32@gmail.com",
                    PhoneNumber = "06208468493"
                }
            }.AsQueryable();
        public static Mock<IRepository<FixedWageOrder>> FixedWageOrderRepository = SetupFixedWageOrderRepository();
        public static FixedWageOrderLogic FixedWageOrderLogic = new(FixedWageOrderRepository.Object, FixedWageEmployeeRepository.Object, EventTypeRepository.Object);

        public static IQueryable<HourlyWageOrder> HourlyWageOrders = new List<HourlyWageOrder>()
            {
                new HourlyWageOrder()
                {
                    Id = 1,
                    OrderDate = DateTime.Parse("2021.10.12"),
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
                    OrderDate = DateTime.Parse("2021.12.07"),
                    EmployeeId = 4,
                    Hours = 7,
                    FirstName = "Lőrinc",
                    LastName = "Alvó",
                    EmailAddress = "alvo.lorinc257@gmail.com",
                },
                new HourlyWageOrder()
                {
                    Id = 3,
                    OrderDate = DateTime.Parse("2021.10.14"),
                    EmployeeId = 1,
                    Hours = 6,
                    FirstName = "Tibor",
                    LastName = "Kerti",
                    EmailAddress = "kerti.tibor67@gmail.com",
                    PhoneNumber = "06202304201"
                },
                new HourlyWageOrder()
                {
                    Id = 4,
                    OrderDate = DateTime.Parse("2021.10.14"),
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
                    OrderDate = DateTime.Parse("2021.10.14"),
                    EmployeeId = 3,
                    Hours = 3,
                    FirstName = "Eszter",
                    LastName = "Pék",
                    EmailAddress = "pek.eszter0267@gmail.com",
                }
            }.AsQueryable();
        public static Mock<IRepository<HourlyWageOrder>> HourlyWageOrderRepository = SetupHourlyWageOrderRepository();
        public static HourlyWageOrderLogic HourlyWageOrderLogic = new(HourlyWageOrderRepository.Object, HourlyWageEmployeeRepository.Object);

        static Mock<IRepository<Job>> SetupJobRepository()
        {
            Mock<IRepository<Job>> JobRepository = new();
            JobRepository.Setup(r => r.ReadAll()).Returns(Jobs);
            JobRepository.Setup(r => r.Read(It.IsAny<int>())).Returns((int id) => Jobs.FirstOrDefault(job => job.Id == id));
            return JobRepository;
        }
        static Mock<IRepository<EventType>> SetupEventTypeRepository()
        {
            Mock<IRepository<EventType>> EventTypeRepository = new();
            EventTypeRepository.Setup(r => r.ReadAll()).Returns(EventTypes);
            EventTypeRepository.Setup(r => r.Read(It.IsAny<int>())).Returns((int id) => EventTypes.FirstOrDefault(eventType => eventType.Id == id));
            return EventTypeRepository;
        }
        static Mock<IRepository<FixedWageEmployee>> SetupFixedWageEmployeeRepository()
        {
            Mock<IRepository<FixedWageEmployee>> FixedWageEmployeeRepository = new();
            FixedWageEmployeeRepository.Setup(r => r.ReadAll()).Returns(FixedWageEmployees);
            FixedWageEmployeeRepository.Setup(r => r.Read(It.IsAny<int>())).Returns((int id) => FixedWageEmployees.FirstOrDefault(emp => emp.Id == id));
            SetNavPropsOneToMany(Jobs, FixedWageEmployees);
            return FixedWageEmployeeRepository;
        }
        static Mock<IRepository<HourlyWageEmployee>> SetupHourlyWageEmployeeRepository()
        {
            Mock<IRepository<HourlyWageEmployee>> HourlyWageEmployeeRepository = new();
            HourlyWageEmployeeRepository.Setup(r => r.ReadAll()).Returns(HourlyWageEmployees);
            HourlyWageEmployeeRepository.Setup(r => r.Read(It.IsAny<int>())).Returns((int id) => HourlyWageEmployees.FirstOrDefault(emp => emp.Id == id));
            SetNavPropsOneToMany(Jobs, HourlyWageEmployees);
            return HourlyWageEmployeeRepository;
        }
        static Mock<IRepository<FixedWageOrder>> SetupFixedWageOrderRepository()
        {
            Mock<IRepository<FixedWageOrder>> FixedWageOrderRepository = new();
            FixedWageOrderRepository.Setup(r => r.ReadAll()).Returns(FixedWageOrders);
            FixedWageOrderRepository.Setup(r => r.Read(It.IsAny<int>())).Returns((int id) => FixedWageOrders.FirstOrDefault(order => order.Id == id));
            SetNavPropsOneToMany(EventTypes, FixedWageOrders);
            SetNavPropsOneToMany(FixedWageEmployees, FixedWageOrders);
            return FixedWageOrderRepository;
        }
        static Mock<IRepository<HourlyWageOrder>> SetupHourlyWageOrderRepository()
        {
            Mock<IRepository<HourlyWageOrder>> HourlyWageOrderRepository = new();
            HourlyWageOrderRepository.Setup(r => r.ReadAll()).Returns(HourlyWageOrders);
            HourlyWageOrderRepository.Setup(r => r.Read(It.IsAny<int>())).Returns((int id) => HourlyWageOrders.FirstOrDefault(order => order.Id == id));
            SetNavPropsOneToMany(HourlyWageEmployees, HourlyWageOrders);
            return HourlyWageOrderRepository;
        }
        static void SetNavPropsOneToMany<Model2, Model1>(IQueryable<Model2> models2, IQueryable<Model1> models1)
            where Model1 : TableModel
            where Model2 : TableModel
        {
            var model1Props = models1.First().GetType().GetProperties();
            var model1NavProp = model1Props.First(m => m.PropertyType.Name == typeof(Model2).Name);
            var model1NavPropId = model1Props.First(m => m.Name == model1NavProp.Name + "Id");
            foreach (var model1 in models1)
            {
                var navValue = models2.FirstOrDefault(m => m.Id == (int)model1NavPropId.GetValue(model1));
                model1NavProp.SetValue(model1, navValue);
            }
            var model2Props = models2.First().GetType().GetProperties();
            var model2NavProp = model2Props.First(m => m.PropertyType.GetGenericArguments().Length > 0 && m.PropertyType.GetGenericArguments()[0].Name == typeof(Model1).Name);
            foreach (var model2 in models2)
            {
                var valueList = new List<Model1>();
                foreach (var model1 in models1)
                {
                    if ((model1NavProp.GetValue(model1) as Model2).Id == model2.Id)
                    {
                        valueList.Add(model1);
                    }
                }
                model2NavProp.SetValue(model2, valueList);
            }
        }
    }
}
