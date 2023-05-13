using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Services.RestServices;
using XNVSU0_HFT_202231.Models.StatModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics
{
    internal interface IStatsLogic : INotifyPropertyChanged
    {
        RestCollection<EventType>? Eventtypes { get; }
        RestCollection<Job>? Jobs { get; }
        ObservableCollection<IncomeFromJob> IncomeFromJobs { get; }
        OrdersCount? OrdersCount { get; }
        ObservableCollection<EmployeeOrdersCount> EmployeesOrdersCount { get; }
        ObservableCollection<EmployeeAverageHours> EmployeesAverageHours { get; }
        ObservableCollection<IncomeFromOrder> IncomesFromOrder { get; }
        Overview? Overview { get; }
        double? IncomeInYear { get; }
        void UpdateIncomeFromJobs(int eventtypeId);
        void UpdateOrdersCount(int jobId);
        void UpdateIncomeInYear(int year);
        void Update(string nameof);
    }
}
