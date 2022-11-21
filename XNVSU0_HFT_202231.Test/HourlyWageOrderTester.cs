using Moq;
using NUnit.Framework;
using System;
using XNVSU0_HFT_202231.Models.TableModels;
using static XNVSU0_HFT_202231.Test.Helper;

namespace XNVSU0_HFT_202231.Test
{
    [TestFixture]
    class HourlyWageOrderTester
    {
        [Test]
        public void CreateInvalidHourOrder()
        {
            var order = new HourlyWageOrder()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Now,
                Hours = 8,
                EmployeeId = 1,
            };
            Assert.Throws<ArgumentException>(() => HourlyWageOrderLogic.Create(order));
        }
        
        [Test]
        public void CreateInvalidEmployeeOrder()
        {
            var order = new HourlyWageOrder()
            {
                Id = 1,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Now,
                Hours = 3,
                EmployeeId = 6,
            };
            Assert.Throws<ArgumentException>(() => HourlyWageOrderLogic.Create(order));
        }
        [Test]
        public void CreateInvalidDateOrder()
        {
            var order = new HourlyWageOrder()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Parse("2021.10.12"),
                Hours = 3,
                EmployeeId = 1,
            };
            Assert.Throws<ArgumentException>(() => HourlyWageOrderLogic.Create(order));
        }
        [Test]
        public void CreateValidOrder()
        {
            var order = new HourlyWageOrder()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Now,
                Hours = 3,
                EmployeeId = 1
            };
            HourlyWageOrderLogic.Create(order);
            HourlyWageOrderRepository.Verify(r => r.Create(order), Times.Once);
        }
        [Test]
        public void UpdateInvalidHourOrder()
        {
            var order = new HourlyWageOrder()
            {
                Id = 2,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Now,
                Hours = 8,
                EmployeeId = 1
            };
            Assert.Throws<ArgumentException>(() => HourlyWageOrderLogic.Update(order));
        }
        [Test]
        public void UpdateInvalidEmployeeOrder()
        {
            var order = new HourlyWageOrder()
            {
                Id = 2,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Now,
                Hours = 3,
                EmployeeId = 6
            };
            Assert.Throws<ArgumentException>(() => HourlyWageOrderLogic.Create(order));
        }
        [Test]
        public void UpdateInvalidDateOrder()
        {
            var order = new HourlyWageOrder()
            {
                Id = 2,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Parse("2021.10.12"),
                Hours = 3,
                EmployeeId = 1
            };
            Assert.Throws<ArgumentException>(() => HourlyWageOrderLogic.Create(order));
        }
        [Test]
        public void UpdateValidOrder()
        {
            var order = new HourlyWageOrder()
            {
                Id = 2,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Now,
                Hours = 3,
                EmployeeId = 1
            };
            HourlyWageOrderLogic.Update(order);
            HourlyWageOrderRepository.Verify(r => r.Update(order), Times.Once);
        }
    }
}
