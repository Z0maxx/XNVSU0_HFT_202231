using System.Collections.Generic;
using System.Threading.Tasks;
using XNVSU0_HFT_202231.Models.StatModels;

namespace WpfClient.Services.RestServices.Interfaces
{
    interface IStatsRestService
    {
        Task<List<IncomeFromJob>> IncomeFromEventByJobs(int eventTypeId);
        Task<OrdersCount> OrdersCountByJob(int jobId);
        Task<List<EmployeeOrdersCount>> MostPopularFixedWageEmployees();
        Task<double> IncomeFromFixedWageOrdersInYear(int year);
        Task<List<EmployeeAverageHours>> HourlyWageEmployeesAverageHours();
        Task<List<IncomeFromOrder>> HourlyWageOrdersOverview();
        Task<Overview> OverallOverview();
    }
}
