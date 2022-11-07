using NUnit.Framework;
using System.Collections.Generic;
using XNVSU0_HFT_202231.Models.Stats;
using static XNVSU0_HFT_202231.Test.Helper;

namespace XNVSU0_HFT_202231.Test
{
    [TestFixture]
    class StatTester
    {
        [TestCaseSource(nameof(IncomeFromJobsCases))]
        public void IncomeFromEventByJobs(int eventTypeId, IEnumerable<IncomeFromJob> expected)
        {
            var result = EventTypeLogic.IncomeByJobs(eventTypeId);
            CollectionAssert.AreEquivalent(result, expected);
        }
        [TestCaseSource(nameof(OrdersCountCases))]
        public void OrdersCountByJob(int jobId, OrdersCount expected)
        {
            var result = JobLogic.OrdersCount(jobId);
            Assert.AreEqual(result, expected);
        }
        [Test]
        public void MostPopularFixedWageEmployees()
        {
            var expected = new List<EmployeeOrdersCount>
            {
                new EmployeeOrdersCount()
                {
                    EmployeeName = "Béla Nagy",
                    OrdersCount = 2
                },
                new EmployeeOrdersCount()
                {
                    EmployeeName = "János Kis",
                    OrdersCount = 2
                },
            };
            var result = FixedWageEmployeeLogic.MostPopular();
            CollectionAssert.AreEquivalent(result, expected);
        }
        [TestCaseSource(nameof(IncomeInYearCases))]
        public void IncomeFromFixedWageOrdersInYear(int year, double expected)
        {
            var result = FixedWageOrderLogic.IncomeInYear(year);
            Assert.AreEqual(result, expected);
        }
        public static IEnumerable<TestCaseData> IncomeFromJobsCases
        {
            get
            {
                var cases = new List<TestCaseData>();
                var incomeFrom1 = new List<IncomeFromJob>()
                {
                    new IncomeFromJob()
                    {
                        Job = "Séf",
                        Income = 35000
                    },
                    new IncomeFromJob()
                    {
                        Job = "Italkeverő",
                        Income = 19000
                    }
                };
                cases.Add(new TestCaseData(new object[] { 1, incomeFrom1 }));
                var incomeFrom2 = new List<IncomeFromJob>()
                {
                    new IncomeFromJob()
                    {
                        Job = "Séf",
                        Income = 35000
                    }
                };
                cases.Add(new TestCaseData(new object[] { 2, incomeFrom2 }));
                var incomeFrom5 = new List<IncomeFromJob>();
                cases.Add(new TestCaseData(new object[] { 5, incomeFrom5 }));
                return cases;
            }
        }
        public static IEnumerable<TestCaseData> OrdersCountCases
        {
            get
            {
                var cases = new List<TestCaseData>();
                var ordersBy1 = new OrdersCount()
                {
                    FixedWageOrderCount = 2,
                    HourlyWageOrderCount = 1
                };
                cases.Add(new TestCaseData(new object[] { 1, ordersBy1 }));
                var ordersBy2 = new OrdersCount()
                {
                    FixedWageOrderCount = 3,
                    HourlyWageOrderCount = 4
                };
                cases.Add(new TestCaseData(new object[] { 2, ordersBy2 }));
                return cases;
            }
        }
        public static IEnumerable<TestCaseData> IncomeInYearCases
        {
            get
            {
                var cases = new List<TestCaseData>();
                var incomeIn2020 = 15000;
                cases.Add(new TestCaseData(new object[] { 2020, incomeIn2020 }));
                var incomeIn2021 = 108000;
                cases.Add(new TestCaseData(new object[] { 2021, incomeIn2021 }));
                return cases;
            }
        }
    }
}
