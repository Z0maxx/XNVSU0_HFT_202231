using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfClient.Services.RestServices;
using WpfClient.Services.RestServices.Interfaces;
using Xceed.Wpf.Toolkit.Core;
using XNVSU0_HFT_202231.Models.StatModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics
{
    internal class StatsLogic : IStatsLogic
    {
        readonly Dictionary<string, Action> updates;
        public RestCollection<EventType>? Eventtypes { get => eventtypeService?.List; }
        public RestCollection<Job>? Jobs { get => jobService?.List; }
        public ObservableCollection<IncomeFromJob> IncomeFromJobs { get; }
        public event PropertyChangedEventHandler? PropertyChanged;
        OrdersCount? ordersCount;
        public OrdersCount? OrdersCount
        {
            get => ordersCount;
            private set
            {
                ordersCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OrdersCount)));
            }
        }
        public ObservableCollection<EmployeeOrdersCount> EmployeesOrdersCount { get; }
        double? incomeInYear;
        public double? IncomeInYear
        {
            get => incomeInYear;
            private set
            {
                incomeInYear = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IncomeInYear)));
            }
        }
        public ObservableCollection<EmployeeAverageHours> EmployeesAverageHours { get; }
        public ObservableCollection<IncomeFromOrder> IncomesFromOrder { get; }
        Overview? overview;
        public Overview? Overview
        {
            get => overview;
            private set
            {
                overview = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Overview)));
            }
        }
        readonly IStatsRestService statsService;
        readonly ILiveRestService<EventType> eventtypeService;
        readonly ILiveRestService<Job> jobService;
        public StatsLogic(IStatsRestService statsService, ILiveRestService<EventType> eventtypeService, ILiveRestService<Job> jobService)
        {
            this.statsService = statsService;
            this.eventtypeService = eventtypeService;
            this.jobService = jobService;

            IncomeFromJobs = new();
            EmployeesOrdersCount = new();
            EmployeesAverageHours = new();
            IncomesFromOrder = new();

            updates = new()
            {
                { nameof(EmployeesOrdersCount), UpdateEmployeesOrdersCount },
                { nameof(EmployeesAverageHours), UpdateEmployeesAverageHours },
                { nameof (IncomesFromOrder), UpdateIncomesFromOrder },
                { nameof(Overview), UpdateOverview }
            };
        }
        public async void UpdateIncomeFromJobs(int eventtypeId)
        {
            IncomeFromJobs.Clear();
            if (statsService != null)
            {
                var items = await statsService.IncomeFromEventByJobs(eventtypeId);
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        IncomeFromJobs.Add(item);
                    }
                }
            }
        }
        public async void UpdateOrdersCount(int jobId)
        {
            if (statsService != null)
            {
                OrdersCount = await statsService.OrdersCountByJob(jobId);
            }

        }
        async void UpdateEmployeesOrdersCount()
        {
            if (statsService != null)
            {
                EmployeesOrdersCount.Clear();
                var items = await statsService.MostPopularFixedWageEmployees();
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        EmployeesOrdersCount.Add(item);
                    }
                }
            }
        }
        async void UpdateEmployeesAverageHours()
        {
            if (statsService != null)
            {
                EmployeesAverageHours.Clear();
                var items = await statsService.HourlyWageEmployeesAverageHours();
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        EmployeesAverageHours.Add(item);
                    }
                }
            }
        }
        async void UpdateIncomesFromOrder()
        {
            if (statsService != null)
            {
                IncomesFromOrder.Clear();
                var items = await statsService.HourlyWageOrdersOverview();
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        IncomesFromOrder.Add(item);
                    }
                }
            }
        }
        async void UpdateOverview()
        {
            if (statsService != null)
            {
                Overview = await statsService.OverallOverview();
            }
        }
        public async void UpdateIncomeInYear(int year)
        {
            if (statsService != null)
            {
                IncomeInYear = await statsService.IncomeFromFixedWageOrdersInYear(year);
            }
        }
        public void Update(string nameof)
        {
            updates[nameof]?.Invoke();
        }
    }
}
