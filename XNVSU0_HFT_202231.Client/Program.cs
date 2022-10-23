using ConsoleTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace XNVSU0_HFT_202231.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferHeight = 1000;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            ShowTables(args);
        }
        static void ShowTables(string[] args)
        {
            var rest = new RestService("http://localhost:14784/");
            IClient[] clients = new IClient[]
            {
                new JobClient(rest, args),
                new EventTypeClient(rest, args),
                new FixedWageEmployeeClient(rest, args),
                new FixedWageOrderClient(rest, args),
                new HourlyWageEmployeeClient(rest, args),
                new HourlyWageOrderClient(rest, args)
            };
            var menu = new ConsoleMenu(args, level: 0)
                .Configure(config =>
                {
                    config.Title = "[Tables]\n";
                    config.EnableWriteTitle = true;
                });
            foreach (IClient client in clients)
            {
                menu.Add(GetDisplayName(client), () => client.ShowMenu());
            }
            menu
                .Add("Exit", ConsoleMenu.Close)
                .Show();
        }
        static string GetDisplayName(object obj)
        {
            var type = obj.GetType();
            var attribute = type.GetCustomAttribute<DisplayNameAttribute>();
            if (attribute == null) return type.Name;
            return attribute.DisplayName;
        }
    }
}
