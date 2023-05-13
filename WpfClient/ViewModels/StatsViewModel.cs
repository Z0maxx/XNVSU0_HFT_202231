using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfClient.Logics;
using WpfClient.Services.RestServices;
using WpfClient.ViewModels.Interfaces;
using XNVSU0_HFT_202231.Models.StatModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels
{
    class StatsViewModel : ObservableRecipient, ICloseWindow
    {
        
        TabItem? selectedTabItem;
        public TabItem? SelectedTabItem
        {
            get => selectedTabItem;
            set
            {
                selectedTabItem = value;
                if (selectedTabItem != null && selectedTabItem.Name != null && selectedTabItem.Name != "")
                {
                    logic?.Update(selectedTabItem.Name);
                }
            }
        }
        public ObservableCollection<IncomeFromJob>? IncomeFromJobs { get => logic?.IncomeFromJobs; }
        public int? SelectedEventtypeId
        {
            set
            {
                if (value != null)
                {
                    logic?.UpdateIncomeFromJobs((int)value);
                }
            }
        }
        public int? SelectedJobId
        {
            set
            {
                if (value != null)
                {
                    logic?.UpdateOrdersCount((int)value);
                }
            }
        }
        public ICommand SearchYearCommand { get; }
        int? selectedYear;
        public int? SelectedYear
        {
            get => selectedYear;
            set
            {
                SetProperty(ref selectedYear, value);
                (SearchYearCommand as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }
        public RestCollection<EventType>? Eventtypes { get => logic?.Eventtypes; }
        public RestCollection<Job>? Jobs { get => logic?.Jobs; }
        OrdersCount? ordersCount;
        public OrdersCount? OrdersCount { get => ordersCount; set => SetProperty(ref ordersCount, value); }
        public ObservableCollection<EmployeeOrdersCount>? EmployeesOrdersCount { get => logic?.EmployeesOrdersCount; }
        double? incomeInYear;
        public double? IncomeInYear { get => incomeInYear; set => SetProperty(ref incomeInYear, value); }
        public ObservableCollection<EmployeeAverageHours>? EmployeesAverageHours { get => logic?.EmployeesAverageHours; }
        public ObservableCollection<IncomeFromOrder>? IncomesFromOrder { get => logic?.IncomesFromOrder; }
        Overview? overview;
        public Overview? Overview { get => overview; set => SetProperty(ref overview, value); }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public Action? Close { get; set; }

        readonly IStatsLogic? logic;
        
        public StatsViewModel() :
            this(IsInDesignMode ? null : Ioc.Default.GetService<IStatsLogic>())
        {
        }
        public StatsViewModel(IStatsLogic? logic)
        {
            this.logic = logic;
            if (logic != null)
            {
                logic.PropertyChanged += Logic_PropertyChanged;
                Close += () => logic.PropertyChanged -= Logic_PropertyChanged;
            }
            

            SearchYearCommand = new RelayCommand(
                () =>
                {
                    if (SelectedYear != null)
                    {
                        logic?.UpdateIncomeInYear((int)SelectedYear);
                    }
                },
                () =>
                {
                    return SelectedYear != null;
                }
            );
        }

        private void Logic_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OrdersCount))
            {
                OrdersCount = logic?.OrdersCount;
            }
            else if (e.PropertyName == nameof(IncomeInYear))
            {
                IncomeInYear = logic?.IncomeInYear;
            }
            else if (e.PropertyName == nameof(Overview))
            {
                Overview = logic?.Overview;
            }
        }
    }
}
