using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Models;
using static XNVSU0_HFT_202231.Test.Helper;

namespace XNVSU0_HFT_202231.Test
{
    [TestFixture]
    class FixedWageOrderTester
    {
        [Test]
        public void CreateInvalidEventOrder()
        {
            var order = new FixedWageOrder()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Now,
                EmployeeId = 1,
                EventTypeId = 6
            };
            Assert.Throws<ArgumentException>(() => FixedWageOrderLogic.Create(order));
        }
        [Test]
        public void CreateInvalidEmployeeOrder()
        {
            var order = new FixedWageOrder()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Now,
                EmployeeId = 6,
                EventTypeId = 1
            };
            Assert.Throws<ArgumentException>(() => FixedWageOrderLogic.Create(order));
        }
        [Test]
        public void CreateInvalidDateOrder()
        {
            var order = new FixedWageOrder()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Parse("2021.11.05"),
                EmployeeId = 1,
                EventTypeId = 1
            };
            Assert.Throws<ArgumentException>(() => FixedWageOrderLogic.Create(order));
        }
        [Test]
        public void CreateValidOrder()
        {
            var order = new FixedWageOrder()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Now,
                EmployeeId = 1,
                EventTypeId = 1
            };
            FixedWageOrderLogic.Create(order);
            FixedWageOrderRepository.Verify(r => r.Create(order), Times.Once);
        }
        [Test]
        public void UpdateInvalidEventOrder()
        {
            var order = new FixedWageOrder()
            {
                Id = 2,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Now,
                EmployeeId = 1,
                EventTypeId = 6
            };
            Assert.Throws<ArgumentException>(() => FixedWageOrderLogic.Create(order));
        }
        [Test]
        public void UpdateInvalidEmployeeOrder()
        {
            var order = new FixedWageOrder()
            {
                Id = 2,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Now,
                EmployeeId = 6,
                EventTypeId = 1
            };
            Assert.Throws<ArgumentException>(() => FixedWageOrderLogic.Create(order));
        }
        [Test]
        public void UpdateInvalidDateOrder()
        {
            var order = new FixedWageOrder()
            {
                Id = 2,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Parse("2021.11.05"),
                EmployeeId = 1,
                EventTypeId = 1
            };
            Assert.Throws<ArgumentException>(() => FixedWageOrderLogic.Update(order));
        }
        [Test]
        public void UpdateValidOrder()
        {
            var order = new FixedWageOrder()
            {
                Id = 2,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                OrderDate = DateTime.Now,
                EmployeeId = 1,
                EventTypeId = 1
            };
            FixedWageOrderLogic.Update(order);
            FixedWageOrderRepository.Verify(r => r.Update(order), Times.Once);
        }
    }
}
