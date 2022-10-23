using ConsoleTools;
using System;
using System.Reflection;
using System.ComponentModel;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using XNVSU0_HFT_202231.Models;
using System.Collections.Generic;

namespace XNVSU0_HFT_202231.Client
{
    abstract class Client<T> where T : class, new()
    {
        protected readonly RestService rest;
        protected readonly string[] args;
        protected readonly string endpoint;
        protected readonly string[] propOrder;
        protected delegate IEnumerable<IModel> ModelDelegate<IModel>(string endpoint);
        protected readonly Dictionary<string, (ModelDelegate<IModel>, string)> optionsDict;
        public Client(RestService rest, string[] args, string[] propOrder)
        {
            this.rest = rest;
            this.args = args;
            this.propOrder = propOrder;
            endpoint = typeof(T).Name.ToLower();
            optionsDict = new();
        }
        public void Read()
        {
            DisplayOperation();
            Console.Write("Enter id: ");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine($"This is not a number");
                Console.ReadLine();
                return;
            }
            DisplayProcessing();
            try
            {
                var item = rest.Get<T>(id, endpoint);
                DisplayOperation();
                DisplayProperties(item as IModel);
                Console.WriteLine();
            }
            catch (Exception e)
            {
                DisplayOperation();
                Console.WriteLine(e.Message);
            }
            Continue();
        }
        public void ReadAll()
        {
            DisplayProcessing();
            var items = rest.Get<T>(endpoint);
            DisplayOperation();
            foreach (var item in items)
            {
                DisplayProperties(item as IModel);
                Console.WriteLine();
            }
            Continue();
        }
        public void Create()
        {
            T newItem = new();
            var propInfos = newItem.GetType().GetProperties();
            foreach (var prop in propOrder)
            {
                var propInfo = propInfos.First(p => p.Name == prop);
                if (propInfo.GetAccessors().FirstOrDefault(a => a.IsVirtual) == null)
                {
                    DisplayOperation();
                    if (prop != "Id" && prop.Substring(prop.Length - 2, 2) == "Id")
                    {
                        SetOption(prop, propInfo, newItem);
                    }
                    else
                    {
                        var attributes = propInfo.GetCustomAttributes<ValidationAttribute>();
                        bool required = true;
                        if (attributes.FirstOrDefault(a => a is RequiredAttribute) == null)
                        {
                            required = false;
                            Console.WriteLine($"{prop} is not required");
                        }
                        foreach (var attribute in attributes)
                        {
                            Console.WriteLine(attribute.ErrorMessage);
                        }
                        Console.Write($"Enter {prop}: ");
                        string input = Console.ReadLine();
                        try
                        {
                            if (input == "" && required) throw new ArgumentException("Required");
                            if (input != "") propInfo.SetValue(newItem, TypeDescriptor.GetConverter(propInfo.PropertyType).ConvertFromString(input));
                        }
                        catch
                        {
                            Console.WriteLine("This is not a correct value");
                            Console.ReadLine();
                            Continue();
                            return;
                        }
                    }
                }
            }
            DisplayProcessing();
            DisplayResults(rest.Post(newItem, endpoint));
            Continue();
        }
        public void Update()
        {
            T newItem = new();
            var propInfos = newItem.GetType().GetProperties();
            foreach (var prop in propOrder)
            {
                var propInfo = propInfos.First(p => p.Name == prop);
                if (propInfo.GetAccessors().FirstOrDefault(a => a.IsVirtual) == null)
                {
                    DisplayOperation();
                    if (prop != "Id" && prop.Substring(prop.Length - 2, 2) == "Id")
                    {
                        SetOption(prop, propInfo, newItem);
                    }
                    else
                    {
                        var attributes = propInfo.GetCustomAttributes<ValidationAttribute>();
                        bool required = true;
                        if (attributes.FirstOrDefault(a => a is RequiredAttribute) == null && propInfo.Name != "Id")
                        {
                            required = false;
                            Console.WriteLine($"{propInfo.Name} is not required");
                        }
                        else if (propInfo.Name == "Id")
                        {
                            Console.WriteLine("Id is required");
                        }
                        foreach (var attribute in attributes)
                        {
                            Console.WriteLine(attribute.ErrorMessage);
                        }
                        Console.Write($"Enter {propInfo.Name}: ");
                        string input = Console.ReadLine();
                        try
                        {
                            if (input == "" && required) throw new ArgumentException("Required");
                            if (input != "") propInfo.SetValue(newItem, TypeDescriptor.GetConverter(propInfo.PropertyType).ConvertFromString(input));
                        }
                        catch
                        {
                            Console.WriteLine("This is not a correct value");
                            Continue();
                            return;
                        }
                    }
                }
            }
            DisplayProcessing();
            DisplayResults(rest.Put(newItem, endpoint));
            Continue();
        }
        public void Delete()
        {
            DisplayOperation();
            Console.Write("Enter id: ");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine($"This is not a number");
                Console.ReadLine();
                return;
            }
            DisplayProcessing();
            DisplayResults(rest.Delete(id, endpoint));
            Continue();
        }
        public void ShowMethods()
        {
            new ConsoleMenu(args, level: 1)
                .Configure(config =>
                {
                    config.Title = $"[{typeof(T).Name}]\n";
                    config.EnableWriteTitle = true;
                })
                .Add("Create", (thisMenu) => { Create(); thisMenu.CloseMenu(); })
                .Add("Read", (thisMenu) => { Read(); thisMenu.CloseMenu(); })
                .Add("ReadAll", (thisMenu) => { ReadAll(); thisMenu.CloseMenu(); })
                .Add("Update", (thisMenu) => { Update(); thisMenu.CloseMenu(); })
                .Add("Delete", (thisMenu) => { Delete(); thisMenu.CloseMenu(); })
                .Add("Exit", ConsoleMenu.Close)
                .Show();
        }
        void DisplayProperties(IModel item, string level = "")
        {
            foreach (var prop in item.GetType().GetProperties())
            {
                var propValue = prop.GetValue(item);
                if (prop.GetAccessors().FirstOrDefault(a => a.IsVirtual) == null)
                {
                    Console.WriteLine($"{level}{prop.Name}: {propValue}");
                }
                else if (propValue != null)
                {
                    Console.WriteLine($"{level}{prop.Name}:");
                    string newLevel = level + "    ";
                    DisplayProperties(propValue as IModel, newLevel);
                }
            }
        }
        void Continue()
        {
            Console.WriteLine("Press a button to continue");
            Console.ReadLine();
            ShowMethods();
        }
        void DisplayOperation([CallerMemberName] string callerName = "")
        {
            Console.Clear();
            Console.WriteLine($"[{callerName} {typeof(T).Name}]\n");
        }
        void DisplayResults(string[] results, [CallerMemberName] string callerName = "")
        {
            DisplayOperation(callerName);
            foreach (var result in results) Console.WriteLine(result);
        }
        void DisplayProcessing([CallerMemberName] string callerName = "")
        {
            DisplayOperation(callerName);
            Console.WriteLine("Processing");
        }
        void SetOption(string prop, PropertyInfo propInfo, T newItem, [CallerMemberName] string callerName = "")
        {
            prop = prop.Substring(0, prop.Length - 2);
            var menu = new ConsoleMenu();
            menu.Configure(config =>
            {
                config.Title = $"[{callerName} {typeof(T).Name}]\n\n{prop} is required\n{prop} options"; ;
                config.EnableWriteTitle = true;
            });
            ;
            var items = optionsDict[prop];
            var options = items.Item1.Invoke(items.Item2);
            ;
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
                menu.Add(option.ToString(), (thisMenu) => { propInfo.SetValue(newItem, id); thisMenu.CloseMenu(); });
            }
            menu.Show();
        }
    }
}