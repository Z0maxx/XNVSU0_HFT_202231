using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using XNVSU0_HFT_202231.Logic;
using XNVSU0_HFT_202231.Models.StatModels;

namespace XNVSU0_HFT_202231.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        readonly IJobLogic jobLogic;
        readonly IEventTypeLogic eventTypeLogic;
        readonly IFixedWageEmployeeLogic fixedWageEmployeeLogic;
        readonly IFixedWageOrderLogic fixedWageOrderLogic;
        readonly IHourlyWageEmployeeLogic hourlyWageEmployeeLogic;
        readonly IHourlyWageOrderLogic hourlyWageOrderLogic;
        public StatController(
            IJobLogic jobLogic,
            IEventTypeLogic eventTypeLogic,
            IFixedWageEmployeeLogic fixedWageEmployeeLogic,
            IFixedWageOrderLogic fixedWageOrderLogic,
            IHourlyWageEmployeeLogic hourlyWageEmployeeLogic,
            IHourlyWageOrderLogic hourlyWageOrderLogic)
        {
            this.jobLogic = jobLogic;
            this.eventTypeLogic = eventTypeLogic;
            this.fixedWageEmployeeLogic = fixedWageEmployeeLogic;
            this.fixedWageOrderLogic = fixedWageOrderLogic;
            this.hourlyWageEmployeeLogic = hourlyWageEmployeeLogic;
            this.hourlyWageOrderLogic = hourlyWageOrderLogic;
        }
        [HttpGet("{id}")]
        public IEnumerable<IncomeFromJob> IncomeFromEventByJobs(int id)
        {
            return eventTypeLogic.IncomeByJobs(id);
        }
        [HttpGet("{id}")]
        public OrdersCount OrdersCountByJob(int id)
        {
            return jobLogic.OrdersCount(id);
        }
        [HttpGet]
        public IEnumerable<EmployeeOrdersCount> MostPopularFixedWageEmployees()
        {
            return fixedWageEmployeeLogic.MostPopular();
        }
        [HttpGet("{year}")]
        public double? IncomeFromFixedWageOrdersInYear(int year)
        {
            return fixedWageOrderLogic.IncomeInYear(year);
        }
        [HttpGet]
        public IEnumerable<EmployeeAverageHours> HourlyWageEmployeesAverageHours()
        {
            return hourlyWageEmployeeLogic.AverageHours();
        }
        [HttpGet]
        public IEnumerable<IncomeFromOrder> HourlyWageOrdersOverview()
        {
            return hourlyWageOrderLogic.Overview();
        }
        [HttpGet]
        public Overview OverallOverview()
        {
            return jobLogic.OverallOverview();
        }
    }
}