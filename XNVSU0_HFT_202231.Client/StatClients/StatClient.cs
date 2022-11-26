using ConsoleTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using XNVSU0_HFT_202231.Models.TableModels;
using XNVSU0_HFT_202231.Models.StatModels;
using static XNVSU0_HFT_202231.Client.CustomAttributeExtension;

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
                .Add(GetDisplayName(IncomeFromEventByJobs), () => IncomeFromEventByJobs())
                .Add(GetDisplayName(OrdersCountByJob), () => OrdersCountByJob())
                .Add(GetDisplayName(MostPopularFixedWageEmployees), () => MostPopularFixedWageEmployees())
                .Add(GetDisplayName(IncomeFromFixedWageOrdersInYear), () => IncomeFromFixedWageOrdersInYear())
                .Add(GetDisplayName(HourlyWageEmployeesAverageHours), () => HourlyWageEmployeesAverageHours())
                .Add(GetDisplayName(HourlyWageOrdersOverview), () => HourlyWageOrdersOverview())
                .Add(GetDisplayName(OverallOverview), () => OverallOverview())
                .Add("Exit", ConsoleMenu.Close);
        }
        public void ShowMenu()
        {
            MethodsMenu.Show();
        }
        [DisplayName("Income from event by jobs")]
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
        [DisplayName("Orders count by job")]
        void OrdersCountByJob()
        {
            int jobId = GetOption("Job", rest.GetList<Job>("job"));
            DisplayProcessing();
            var item = rest.Get<OrdersCount>(jobId, "stat/orderscountbyjob");
            DisplayOperation();
            DisplayProperties(item);
            Continue();
        }
        [DisplayName("Most popular fixed wage employees")]
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
        [DisplayName("Income from fixed wage orders in year")]
        void IncomeFromFixedWageOrdersInYear()
        {
            DisplayOperation();
            Console.Write("Enter year: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int id))
            {
                DisplayProcessing();
                var item = rest.Get<double>(id, "stat/incomefromfixedwageordersinyear");
                DisplayOperation();
                Console.WriteLine(item);
            }
            else DisplayError($"This is not a number: {input}");
            Continue();
        }
        [DisplayName("Hourly wage employees average hours")]
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
        [DisplayName("Hourly wage orders overview")]
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
        [DisplayName("Overall overview")]
        void OverallOverview()
        {
            DisplayProcessing();
            var item = rest.Get<Overview>("stat/overalloverview");
            DisplayOperation();
            DisplayProperties(item);
            Continue();
        }
        void DisplayProperties(StatModel item, string level = "")
        {
            foreach (var prop in item.GetType().GetProperties())
            {
                var propValue = prop.GetValue(item);
                if (prop.GetAccessors().FirstOrDefault(a => a.IsVirtual) == null)
                {
                    Console.WriteLine($"{level}{prop.GetDisplayName()}: {propValue}");
                }
                else if (propValue != null)
                {
                    Console.WriteLine($"{level}{prop.GetDisplayName()}:");
                    string newLevel = level + "    ";
                    DisplayProperties(propValue as StatModel, newLevel);
                }
            }
        }
        static void Continue()
        {
            Console.WriteLine("Press a button to continue");
            Console.ReadLine();
        }
        static void DisplayOperation(string callerName = "")
        {
            if (callerName == "")
            {
                callerName = new StackTrace().GetFrame(1).GetMethod().GetDisplayName();
            }
            Console.Clear();
            Console.WriteLine($"[Stats | {callerName}]\n");
        }
        static void DisplayProcessing(string callerName = "")
        {
            if (callerName == "")
            {
                callerName = new StackTrace().GetFrame(1).GetMethod().GetDisplayName();
            }
            DisplayOperation(callerName);
            Console.WriteLine("Processing");
        }
        static int GetOption(string prop, IEnumerable<TableModel> options, string callerName = "")
        {
            if (callerName == "")
            {
                callerName = new StackTrace().GetFrame(1).GetMethod().GetDisplayName();
            }
            int optionId = 0;
            var menu = new ConsoleMenu();
            menu.Configure(config =>
            {
                config.Title = $"[Stats | {callerName}]\n\nSelect a(n) {prop}\n{prop} options";
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
        static void DisplayError(string error, string callerName = "")
        {
            if (callerName == "")
            {
                callerName = new StackTrace().GetFrame(1).GetMethod().GetDisplayName();
            }
            DisplayOperation(callerName);
            ConsoleColor originalColor = Console.ForegroundColor;
            ConsoleColor originalBColor = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(error);
            Console.ForegroundColor = originalColor;
            Console.BackgroundColor = originalBColor;
        }
    }
}