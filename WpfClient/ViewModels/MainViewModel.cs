using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Windows.Input;
using WpfClient.Services;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.ViewModels.GenericViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels
{
    class MainViewModel : ViewModel<TableModel>
    {
        public ICommand JobCommand { get; }
        public ICommand EventtypeCommand { get; }
        public ICommand FixedWageEmployeeCommand { get; }
        public ICommand HourlyWageEmployeeCommand { get; }
        public ICommand FixedWageOrderCommand { get; }
        public ICommand HourlyWageOrderCommand { get; }
        public ICommand StatsCommand { get; }
        public MainViewModel() : this(
            IsInDesignMode ? null : Ioc.Default.GetService<IListService<Job>>(),
            IsInDesignMode ? null : Ioc.Default.GetService<IListService<EventType>>(),
            IsInDesignMode ? null : Ioc.Default.GetService<IListService<FixedWageEmployee>>(),
            IsInDesignMode ? null : Ioc.Default.GetService<IListService<HourlyWageEmployee>>(),
            IsInDesignMode ? null : Ioc.Default.GetService<IListService<FixedWageOrder>>(),
            IsInDesignMode ? null : Ioc.Default.GetService<IListService<HourlyWageOrder>>(),
            IsInDesignMode ? null : Ioc.Default.GetService<IStatsService>())
        {
        }

        public MainViewModel(
            IListService<Job>? jobListService,
            IListService<EventType>? eventtypeListService,
            IListService<FixedWageEmployee>? fweListService,
            IListService<HourlyWageEmployee>? hweListService,
            IListService<FixedWageOrder>? fwoListService,
            IListService<HourlyWageOrder>? hwoListService,
            IStatsService? statsService)
        {
            JobCommand = new RelayCommand(() => jobListService?.List());
            EventtypeCommand = new RelayCommand(() => eventtypeListService?.List());
            FixedWageEmployeeCommand = new RelayCommand(() => fweListService?.List());
            HourlyWageEmployeeCommand = new RelayCommand(() => hweListService?.List());
            FixedWageOrderCommand = new RelayCommand(() => fwoListService?.List());
            HourlyWageOrderCommand = new RelayCommand(() => hwoListService?.List());
            StatsCommand = new RelayCommand(() => statsService?.Stats());
        }
    }
}
