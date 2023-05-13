using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WpfClient.Logics;
using WpfClient.Logics.GenericLogics.Interfaces;
using WpfClient.Logics.TableModelLogics.EventtypeLogics;
using WpfClient.Logics.TableModelLogics.FixedWageEmployeeLogics;
using WpfClient.Logics.TableModelLogics.FixedWageOrderLogics;
using WpfClient.Logics.TableModelLogics.HourlyWageEmployeeLogics;
using WpfClient.Logics.TableModelLogics.HourlyWageOrderLogics;
using WpfClient.Logics.TableModelLogics.JobLogics;
using WpfClient.Services;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices;
using WpfClient.Services.RestServices.Interfaces;
using WpfClient.Services.TableModelServices.EventtypeServices;
using WpfClient.Services.TableModelServices.FixedWageEmployeeServices;
using WpfClient.Services.TableModelServices.FixedWageOrderServices;
using WpfClient.Services.TableModelServices.HourlyWageEmployeeServices;
using WpfClient.Services.TableModelServices.HourlyWageOrderServices;
using WpfClient.Services.TableModelServices.JobServices;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ServiceCollection serviceCollection = new();

            serviceCollection
                .AddSingleton<ILiveRestService<Job>, JobLiveRestService>()
                .AddSingleton<ILiveRestService<EventType>, EventTypeLiveRestService>()
                .AddSingleton<ILiveRestService<FixedWageEmployee>, FWELiveRestService>()
                .AddSingleton<ILiveRestService<HourlyWageEmployee>, HWELiveRestService>()
                .AddSingleton<ILiveRestService<FixedWageOrder>, FWOLiveRestService>()
                .AddSingleton<ILiveRestService<HourlyWageOrder>, HWOLiveRestService>();

            serviceCollection
                .AddSingleton<IListWithInnerListLogic<Job>, JobListLogic>()
                .AddSingleton<IListWithInnerListLogic<EventType>, EventtypeListLogic>()
                .AddSingleton<IListWithInnerListLogic<FixedWageEmployee>, FWEListLogic>()
                .AddSingleton<IListWithInnerListLogic<HourlyWageEmployee>, HWEListLogic>();

            serviceCollection
                .AddSingleton<IListWithSingleLogic<FixedWageOrder, FixedWageEmployee>, FWOListLogic>()
                .AddSingleton<IListWithSingleLogic<HourlyWageOrder, HourlyWageEmployee>, HWOListLogic>();

            serviceCollection
                .AddSingleton<ISingleWithInnerListLogic<FixedWageEmployee>, FWESingleLogic>()
                .AddSingleton<ISingleWithInnerListLogic<HourlyWageEmployee>, HWESingleLogic>();

            serviceCollection
                .AddSingleton<ISingleWithSingleLogic<FixedWageOrder, FixedWageEmployee>, FWOSingleLogic>()
                .AddSingleton<ISingleWithSingleLogic<HourlyWageOrder, HourlyWageEmployee>, HWOSingleLogic>();

            serviceCollection
                .AddSingleton<ISingleService<FixedWageEmployee>, FWESingleViaWindowService>()
                .AddSingleton<ISingleService<HourlyWageEmployee>, HWESingleViaWindowService>()
                .AddSingleton<ISingleService<FixedWageOrder>, FWOSingleViaWindowService>()
                .AddSingleton<ISingleService<HourlyWageOrder>, HWOSingleViaWindowService>();

            serviceCollection
                .AddSingleton<IStatsService, StatsViaWindowService>();

            serviceCollection
                .AddSingleton<IInnerListLogic<Job, FixedWageEmployee, HourlyWageEmployee>, JobEmployeesLogic>()
                .AddSingleton<IInnerListLogic<EventType, FixedWageOrder>, EventtypeOrdersLogic>()
                .AddSingleton<IInnerListLogic<FixedWageEmployee, FixedWageOrder>, FWEOrdersLogic>()
                .AddSingleton<IInnerListLogic<HourlyWageEmployee, HourlyWageOrder>, HWEOrdersLogic>();

            serviceCollection
                .AddSingleton<IStatsLogic, StatsLogic>();

            serviceCollection
                .AddSingleton<IListService<Job>, JobListViaWindowService>()
                .AddSingleton<IListService<EventType>, EventtypeListViaWindowService>()
                .AddSingleton<IListService<FixedWageEmployee>, FWEListViaWindowService>()
                .AddSingleton<IListService<HourlyWageEmployee>, HWEListViaWindowService>()
                .AddSingleton<IListService<FixedWageOrder>, FWOListViaWindowService>()
                .AddSingleton<IListService<HourlyWageOrder>, HWOListViaWindowService>();

            serviceCollection
                .AddSingleton<IStatsRestService, StatsRestService>();

            serviceCollection
                .AddSingleton<ICreateService<Job>, JobCreateViaWindowService>()
                .AddSingleton<ICreateService<EventType>, EventtypeCreateViaWindowService>()
                .AddSingleton<ICreateService<FixedWageEmployee>, FWECreateViaWindowService>()
                .AddSingleton<ICreateService<HourlyWageEmployee>, HWECreateViaWindowService>()
                .AddSingleton<ICreateService<FixedWageOrder>, FWOCreateViaWindowService>()
                .AddSingleton<ICreateService<HourlyWageOrder>, HWOCreateViaWindowService>();

            serviceCollection
                .AddSingleton<IUpdateService<Job>, JobUpdateViaWindowService>()
                .AddSingleton<IUpdateService<EventType>, EventtypeUpdateViaWindowService>()
                .AddSingleton<IUpdateService<FixedWageEmployee>, FWEUpdateViaWindowService>()
                .AddSingleton<IUpdateService<HourlyWageEmployee>, HWEUpdateViaWindowService>()
                .AddSingleton<IUpdateService<FixedWageOrder>, FWOUpdateViaWindowService>()
                .AddSingleton<IUpdateService<HourlyWageOrder>, HWOUpdateViaWindowService>();

            serviceCollection
                .AddSingleton<IInnerListService<Job>, JobEmployeesListViaWindowService>()
                .AddSingleton<IInnerListService<EventType>, EventtypeOrdersViaWindowService>()
                .AddSingleton<IInnerListService<FixedWageEmployee>, FWEOrdersViaWindowService>()
                .AddSingleton<IInnerListService<HourlyWageEmployee>, HWEOrdersViaWindowService>();

            Ioc.Default.ConfigureServices(serviceCollection.BuildServiceProvider());
        }        
    }
}
