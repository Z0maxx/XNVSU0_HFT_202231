using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.StatModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.RestServices
{
    class StatsRestService : IStatsRestService
    {
        readonly RestService restService;
        public StatsRestService()
        {
            restService = new("http://localhost:14784/api/");
        }
        public async Task<List<IncomeFromJob>> IncomeFromEventByJobs(int eventTypeId)
        {
            return await restService.GetAsync<IncomeFromJob>(eventTypeId, "stat/incomefromeventbyjobs");
        }
        public async Task<OrdersCount> OrdersCountByJob(int jobId)
        {
            return await restService.GetSingleAsync<OrdersCount>(jobId, "stat/orderscountbyjob");
        }
        public async Task<List<EmployeeOrdersCount>> MostPopularFixedWageEmployees()
        {
            return await restService.GetAsync<EmployeeOrdersCount>("stat/mostpopularfixedwageemployees");
        }
        public async Task<double> IncomeFromFixedWageOrdersInYear(int year)
        {
            return await restService.GetSingleAsync<double>(year, "stat/incomefromfixedwageordersinyear");
        }
        public async Task<List<EmployeeAverageHours>> HourlyWageEmployeesAverageHours()
        {
            return await restService.GetAsync<EmployeeAverageHours>("stat/hourlywageemployeesaveragehours");
        }
        public async Task<List<IncomeFromOrder>> HourlyWageOrdersOverview()
        {
            return await restService.GetAsync<IncomeFromOrder>("stat/hourlywageordersoverview");
        }
        public async Task<Overview> OverallOverview()
        {
            return await restService.GetSingleAsync<Overview>("stat/overalloverview");
        }
    }
}
