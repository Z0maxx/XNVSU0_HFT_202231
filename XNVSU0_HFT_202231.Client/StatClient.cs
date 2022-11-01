using ConsoleTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Models.Stats;

namespace XNVSU0_HFT_202231.Client
{
    [DisplayName("Stats")]
    class StatClient : IClient
    {
        protected readonly RestService rest;
        protected readonly string[] args;
        protected readonly ConsoleMenu MethodsMenu;
        public StatClient(RestService rest, string[] args)
        {
            this.rest = rest;
            this.args = args;
            MethodsMenu = new ConsoleMenu(args, level: 1)
                .Configure(config =>
                {
                    config.Title = $"[Stats]\n";
                    config.EnableWriteTitle = true;
                })
                .Add("Income from event by jobs", () => IncomeFromEventByJobs())
                .Add("Orders count by job", () => OrdersCountByJob())
                .Add("Most popular fixed wage employees", () => MostPopularFixedWageEmployees())
                .Add("Income from fixed wage orders in month", () => IncomeFromFixedWageOrdersInMonth())
                .Add("Hourly wage employees average hours", () => HourlyWageEmployeesAverageHours())
                .Add("Hourly wage orders overview", () => HourlyWageOrdersOverview())
                .Add("Overall overview", () => OverallOverview())
                .Add("Exit", ConsoleMenu.Close);
        }
        public void ShowMenu()
        {
            MethodsMenu.Show();
        }
        void IncomeFromEventByJobs()
        {
            int eventTypeId = GetOption("Event type", rest.GetList<EventType>("eventtype"));
            DisplayProcessing();
            var items = rest.GetList<IncomeFromJob>(eventTypeId, "stat/incomefromeventbyjobs");
            DisplayOperation();
            foreach (var item in items)
            {
                DisplayProperties(item);
                Console.WriteLine();
            }
            Continue();
        }
        void OrdersCountByJob()
        {
            int jobId = GetOption("Job", rest.GetList<Job>("job"));
            DisplayProcessing();
            var item = rest.Get<OrdersCount>(jobId, "stat/orderscountbyjob");
            DisplayOperation();
            DisplayProperties(item);
            Continue();
        }
        void MostPopularFixedWageEmployees()
        {
            DisplayProcessing();
            var items = rest.GetList<EmployeeOrdersCount>("stat/mostpopularfixedwageemployees");
            DisplayOperation();
            foreach (var item in items)
            {
                DisplayProperties(item);
                Console.WriteLine();
            }
            Continue();
        }
        void IncomeFromFixedWageOrdersInMonth()
        {
            DisplayOperation();
            Console.Write("Enter month: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int id))
            {
                DisplayProcessing();
                var item = rest.Get<double>(id, "stat/incomefromfixedwageordersinmonth");
                DisplayOperation();
                Console.WriteLine(item);
            }
            else Console.WriteLine($"This is not a number");
            Continue();
        }
        void HourlyWageEmployeesAverageHours()
        {
            DisplayProcessing();
            var items = rest.GetList<EmployeeAverageHours>("stat/hourlywageemployeesaveragehours");
            DisplayOperation();
            foreach (var item in items)
            {
                DisplayProperties(item);
                Console.WriteLine();
            }
            Continue();
        }
        void HourlyWageOrdersOverview()
        {
            DisplayProcessing();
            var items = rest.GetList<IncomeFromOrder>("stat/hourlywageordersoverview");
            DisplayOperation();
            foreach (var item in items)
            {
                DisplayProperties(item);
                Console.WriteLine();
            }
            Continue();
        }
        void OverallOverview()
        {
            DisplayProcessing();
            var item = rest.Get<Overview>("stat/overalloverview");
            DisplayOperation();
            DisplayProperties(item);
            Continue();
        }
        void DisplayProperties(Stat item, string level = "")
        {
            foreach (var prop in item.GetType().GetProperties())
            {
                var propValue = prop.GetValue(item);
                if (prop.GetAccessors().FirstOrDefault(a => a.IsVirtual) == null)
                {
                    Console.WriteLine($"{level}{GetDisplayName(prop)}: {propValue}");
                }
                else if (propValue != null)
                {
                    Console.WriteLine($"{level}{GetDisplayName(prop)}:");
                    string newLevel = level + "    ";
                    DisplayProperties(propValue as Stat, newLevel);
                }
            }
        }
        static void Continue()
        {
            Console.WriteLine("Press a button to continue");
            Console.ReadLine();
        }
        static void DisplayOperation([CallerMemberName] string callerName = "")
        {
            Console.Clear();
            Console.WriteLine($"[Stats {callerName}]\n");
        }
        static string GetDisplayName(PropertyInfo prop)
        {
            var attribute = prop.GetCustomAttribute<DisplayNameAttribute>();
            if (attribute == null) return prop.Name;
            return attribute.DisplayName;
        }
        static void DisplayProcessing([CallerMemberName] string callerName = "")
        {
            DisplayOperation(callerName);
            Console.WriteLine("Processing");
        }
        static int GetOption(string prop, IEnumerable<Model> options, [CallerMemberName] string callerName = "")
        {
            int optionId = 0;
            var menu = new ConsoleMenu();
            menu.Configure(config =>
            {
                config.Title = $"[Stats {callerName}]\n\nSelect a(n) {prop}\n{prop} options";
                config.EnableWriteTitle = true;
            });
            foreach (var option in options)
            {
                int id = 0;
                foreach (var optionPropInfo in option.GetType().GetProperties())
                {
                    if (optionPropInfo.Name == "Id")
                    {
                        id = (int)optionPropInfo.GetValue(option);
                    }
                }
                menu.Add(option.ToString(), (thisMenu) => { optionId = id; thisMenu.CloseMenu(); });
            }
            menu.Show();
            return optionId;
        }
        
    }
}