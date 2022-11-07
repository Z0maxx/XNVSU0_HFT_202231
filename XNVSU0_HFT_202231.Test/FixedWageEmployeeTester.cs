using Moq;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using XNVSU0_HFT_202231.Models;
using static XNVSU0_HFT_202231.Test.Helper;

namespace XNVSU0_HFT_202231.Test
{
    [TestFixture]
    class FixedWageEmployeeTester
    {
        [Test]
        public void CreateInvalidWageEmployee()
        {
            var emp = new FixedWageEmployee()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                HireDate = DateTime.Now,
                Wage = 1,
                Hours = 5,
                JobId = 1
            };
            Assert.Throws<ValidationException>(() => FixedWageEmployeeLogic.Create(emp));
        }
        [Test]
        public void CreateInvalidHoursEmployee()
        {
            var emp = new FixedWageEmployee()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                HireDate = DateTime.Now,
                Wage = 5000,
                Hours = 0,
                JobId = 1
            };
            Assert.Throws<ValidationException>(() => FixedWageEmployeeLogic.Create(emp));
        }
        [Test]
        public void CreateInvalidJobEmployee()
        {
            var emp = new FixedWageEmployee()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                HireDate = DateTime.Now,
                Wage = 5000,
                Hours = 5,
                JobId = 3
            };
            Assert.Throws<ArgumentException>(() => FixedWageEmployeeLogic.Create(emp));
        }
        [Test]
        public void CreateNoHireDateEmployee()
        {
            var emp = new FixedWageEmployee()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                Wage = 5000,
                Hours = 5,
                JobId = 1
            };
            Assert.Throws<ValidationException>(() => FixedWageEmployeeLogic.Create(emp));
        }
        [Test]
        public void CreateValidEmployee()
        {
            var emp = new FixedWageEmployee()
            {
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                HireDate = DateTime.Now,
                Wage = 5000,
                Hours = 5,
                JobId = 1
            };
            FixedWageEmployeeLogic.Create(emp);
            FixedWageEmployeeRepository.Verify(r => r.Create(emp), Times.Once);
        }
        [Test]
        public void UpdateInvalidJobEmployee()
        {
            var emp = new FixedWageEmployee()
            {
                Id = 1,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                HireDate = DateTime.Now,
                Wage = 5000,
                Hours = 5,
                JobId = 3
            };
            Assert.Throws<ArgumentException>(() => FixedWageEmployeeLogic.Update(emp));
        }
        [Test]
        public void UpdateValidEmployee()
        {
            var emp = new FixedWageEmployee()
            {
                Id = 1,
                FirstName = "Nagy",
                LastName = "János",
                EmailAddress = "nagy.janos@gmail.com",
                HireDate = DateTime.Now,
                Wage = 5000,
                Hours = 5,
                JobId = 1
            };
            FixedWageEmployeeLogic.Update(emp);
            FixedWageEmployeeRepository.Verify(r => r.Update(emp), Times.Once);
        }
    }
}
