using ConsoleTools;
using System;
using System.Collections.Generic;

namespace XNVSU0_HFT_202231.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            ShowTables(args);
        }
        static void ShowTables(string[] args)
        {
            var rest = new RestService("http://localhost:14784/");
            var Job = new JobClient(rest, args);
            var EventType = new EventTypeClient(rest, args);
            var FixedWageEmployee = new FixedWageEmployeeClient(rest, args);
            var FixedWageOrder = new FixedWageOrderClient(rest, args);
            var HourlyWageEmployee = new HourlyWageEmployeeClient(rest, args);
            var HourlyWageOrder = new HourlyWageOrderClient(rest, args);
            new ConsoleMenu(args, level: 0)
                .Configure(config =>
                {
                    config.Title = "Tables";
                    config.EnableWriteTitle = true;
                })
                .Add("Job", () => Job.ShowMethods())
                .Add("Event type", () => EventType.ShowMethods())
                .Add("Fixed wage employee", () => FixedWageEmployee.ShowMethods())
                .Add("Fixed wage order", () => FixedWageOrder.ShowMethods())
                .Add("Hourly wage employee", () => HourlyWageEmployee.ShowMethods())
                .Add("Hourly wage order", () => HourlyWageOrder.ShowMethods())
                .Add("Exit", ConsoleMenu.Close)
                .Show();
        }
    }
}
