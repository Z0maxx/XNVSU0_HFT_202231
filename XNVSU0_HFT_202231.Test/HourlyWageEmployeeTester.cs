using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using XNVSU0_HFT_202231.Models;
using static XNVSU0_HFT_202231.Test.Helper;

namespace XNVSU0_HFT_202231.Test
{
    [TestFixture]
    class HourlyWageEmployeeTester
    {
        [Test]
        public void CreateInvalidHoursEmployee()
        {
            var emp = new HourlyWageEmployee()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                HireDate = DateTime.Now,
                Wage = 5000,
                MinHours = 5,
                MaxHours = 2,
                JobId = 1
            };
            Assert.Throws<ArgumentException>(() => HourlyWageEmployeeLogic.Create(emp));
        }
        [Test]
        public void CreateInvalidJobEmployee()
        {
            var emp = new HourlyWageEmployee()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                HireDate = DateTime.Now,
                Wage = 5000,
                MinHours = 3,
                MaxHours = 5,
                JobId = 3
            };
            Assert.Throws<ArgumentException>(() => HourlyWageEmployeeLogic.Create(emp));
        }
        [Test]
        public void CreateValidEmployee()
        {
            var emp = new HourlyWageEmployee()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                HireDate = DateTime.Now,
                Wage = 5000,
                MinHours = 3,
                MaxHours = 5,
                JobId = 1
            };
            HourlyWageEmployeeLogic.Create(emp);
            HourlyWageEmployeeRepository.Verify(r => r.Create(emp), Times.Once);
        }
        [Test]
        public void UpdateInvalidJobEmployee()
        {
            var emp = new HourlyWageEmployee()
            {
                Id = 1,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                HireDate = DateTime.Now,
                Wage = 5000,
                MinHours = 3,
                MaxHours = 5,
                JobId = 3
            };
            Assert.Throws<ArgumentException>(() => HourlyWageEmployeeLogic.Update(emp));
        }
        [Test]
        public void UpdateInvalidHoursEmployee()
        {
            var emp = new HourlyWageEmployee()
            {
                Id = 1,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                HireDate = DateTime.Now,
                Wage = 5000,
                MinHours = 5,
                MaxHours = 3,
                JobId = 1
            };
            Assert.Throws<ArgumentException>(() => HourlyWageEmployeeLogic.Update(emp));
        }
        [Test]
        public void UpdateValidEmployee()
        {
            var emp = new HourlyWageEmployee()
            {
                Id = 1,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                HireDate = DateTime.Now,
                Wage = 5000,
                MinHours = 3,
                MaxHours = 5,
                JobId = 1
            };
            HourlyWageEmployeeLogic.Update(emp);
            HourlyWageEmployeeRepository.Verify(r => r.Update(emp), Times.Once);
        }
    }
}
