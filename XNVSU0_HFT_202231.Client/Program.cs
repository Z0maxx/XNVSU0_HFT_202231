using ConsoleTools;
using System;
using System.Runtime.InteropServices;

namespace XNVSU0_HFT_202231.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) Console.BufferHeight = 1000;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            ShowTables(args);
        }
        static void ShowTables(string[] args)
        {
            var rest = new RestService("http://localhost:14784/api/");
            IClient[] clients = new IClient[]
            {
                new JobClient(rest, args),
                new EventTypeClient(rest, args),
                new FixedWageEmployeeClient(rest, args),
                new FixedWageOrderClient(rest, args),
                new HourlyWageEmployeeClient(rest, args),
                new HourlyWageOrderClient(rest, args),
                new StatClient(rest, args)
            };
            var menu = new ConsoleMenu(args, level: 0)
                .Configure(config =>
                {
                    config.Title = "[Tables and Stats]\n";
                    config.EnableWriteTitle = true;
                });
            foreach (IClient client in clients)
            {
                menu.Add(client.GetDisplayName(), () => client.ShowMenu());
            }
            menu
                .Add("Exit", ConsoleMenu.Close)
                .Show();
        }
    }
}
