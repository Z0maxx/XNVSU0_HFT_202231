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
    abstract class Client<T> : IClient where T : Model, new()
    {
        protected readonly RestService rest;
        protected readonly string[] args;
        protected readonly string endpoint;
        protected readonly string[] propOrder;
        protected delegate IEnumerable<S> RestGetDelegate<S>(string endpoint) where S : Model;
        protected readonly Dictionary<string, Dictionary<string, object>> optionsDict;
        protected readonly ConsoleMenu MethodsMenu;
        protected static readonly string modelName = GetDisplayName(typeof(T));
        public Client(RestService rest, string[] args, string[] propOrder)
        {
            this.rest = rest;
            this.args = args;
            this.propOrder = propOrder;
            endpoint = typeof(T).Name.ToLower();
            optionsDict = new();
            MethodsMenu = new ConsoleMenu(args, level: 1)
                .Configure(config =>
                 {
                     config.Title = $"[{modelName}]\n";
                     config.EnableWriteTitle = true;
                 })
                .Add("Create", () => Create())
                .Add("Read", () => Read())
                .Add("Read all", () => ReadAll())
                .Add("Update", () => Update())
                .Add("Delete", () => Delete())
                .Add("Exit", ConsoleMenu.Close);
        }
        public void Read()
        {
            DisplayOperation();
            Console.Write("Enter id: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int id))
            {
                DisplayProcessing();
                try
                {
                    var item = rest.Get<T>(id, endpoint);
                    DisplayOperation();
                    DisplayProperties(item);
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    DisplayOperation();
                    Console.WriteLine(e.Message);
                }
            }
            else Console.WriteLine($"This is not a number");
            Continue();
        }
        public void ReadAll()
        {
            DisplayProcessing();
            var items = rest.GetList<T>(endpoint);
            DisplayOperation();
            foreach (var item in items)
            {
                DisplayProperties(item);
                Console.WriteLine();
            }
            Continue();
        }
        public void Create()
        {
            try
            {
                var newItem = GetNewItem();
                DisplayProcessing();
                DisplayResult(rest.Post(newItem, endpoint));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            Continue();
        }
        public void Update()
        {
            try
            {
                var newItem = GetNewItem(update: true);
                DisplayProcessing();
                DisplayResult(rest.Put(newItem, endpoint));
            }
            catch (ArgumentException e)
            {
                DisplayOperation();
                Console.WriteLine(e.Message);
            }
            Continue();
        }
        public void Delete()
        {
            DisplayOperation();
            Console.Write("Enter id: ");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine($"This is not a number: {input}");
                Console.ReadLine();
                return;
            }
            DisplayProcessing();
            DisplayResult(rest.Delete(id, endpoint));
            Continue();
        }
        public T GetNewItem(bool update = false, [CallerMemberName] string callerName = "")
        {
            T newItem = new();
            var propInfos = newItem.GetType().GetProperties();
            if (update)
            {
                DisplayOperation(callerName);
                Console.Write("Enter id: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int id))
                {
                    newItem = rest.Get<T>(id, endpoint);
                    var updateMenu = new ConsoleMenu(args, level: 2)
                        .Configure(config =>
                        {
                            config.Title = $"[{modelName} {callerName}]\n\nSelect a property of {modelName} to update";
                            config.EnableWriteTitle = true;
                        });
                    foreach (var prop in propOrder)
                    {
                        var propInfo = propInfos.First(p => p.Name == prop);
                        if (propInfo.GetAccessors().FirstOrDefault(a => a.IsVirtual) == null)
                        {
                            if (prop != "Id" && prop.Contains("Id"))
                            {
                                var navProp = propInfos.First(p => p.Name == prop.Substring(0, prop.Length - 2));
                                updateMenu.Add($"{GetDisplayName(navProp)}", () => SetPropValue(newItem, propInfo, callerName, update, navProp));
                            }
                            else updateMenu.Add($"{GetDisplayName(propInfo)}", () => SetPropValue(newItem, propInfo, callerName, update));
                        }
                    }
                    updateMenu.Add("Exit", ConsoleMenu.Close);
                    updateMenu.Show();
                }
                else throw new ArgumentException($"This is not a number: {input}");
            }
            else
            {
                foreach (var prop in propOrder)
                {
                    var propInfo = propInfos.First(p => p.Name == prop);
                    if (propInfo.GetAccessors().FirstOrDefault(a => a.IsVirtual) == null)
                    {
                        SetPropValue(newItem, propInfo, callerName);
                    }
                }
            }
            return newItem;
        }
        void DisplayProperties(Model item, string level = "")
        {
            foreach (var prop in item.GetType().GetProperties())
            {
                var propValue = prop.GetValue(item);
                var propName = GetDisplayName(prop);
                if (prop.GetAccessors().FirstOrDefault(a => a.IsVirtual) == null)
                {
                    Console.WriteLine($"{level}{propName}: {propValue}");
                }
                else if (propValue != null)
                {
                    Console.WriteLine($"{level}{propName}:");
                    string newLevel = level + "    ";
                    DisplayProperties(propValue as Model, newLevel);
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
            Console.WriteLine($"[{modelName} {callerName}]\n");
        }
        static void DisplayResult(Result result, [CallerMemberName] string callerName = "")
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            ConsoleColor originalBColor = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Black;
            DisplayOperation(callerName);
            if (result.Success)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"{callerName} completed");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{callerName} failed");
                foreach (var error in result.Errors) Console.WriteLine(error);
            }
            Console.ForegroundColor = originalColor;
            Console.BackgroundColor = originalBColor;
        }
        static void DisplayProcessing([CallerMemberName] string callerName = "")
        {
            DisplayOperation(callerName);
            Console.WriteLine("Processing");
        }
        void SetPropValue(T newItem, PropertyInfo propInfo, [CallerMemberName] string callerName = "", bool update = false, PropertyInfo navProp = null)
        {
            DisplayOperation(callerName);
            
            if (navProp != null)
            {
                if (update) SetOption(navProp, propInfo, newItem, callerName, update);
                else SetOption(navProp, propInfo, newItem, callerName);
            }
            else
            {
                var attributes = propInfo.GetCustomAttributes<ValidationAttribute>();
                var propName = GetDisplayName(propInfo);
                if (update) attributes = attributes.Where(a => a is not RequiredAttribute);
                bool required = true;
                if (!update && attributes.FirstOrDefault(a => a is RequiredAttribute) == null)
                {
                    required = false;
                    Console.WriteLine($"{propName} is not required");
                }
                foreach (var attribute in attributes)
                {
                    Console.WriteLine(attribute.ErrorMessage);
                }
                if (update) Console.WriteLine($"{propName} is currently: {propInfo.GetValue(newItem)}");
                Console.Write($"Enter {propName}: ");
                string input = Console.ReadLine();
                if (!update && input == "" && required) throw new ArgumentException("Incorrect input value");
                if (input != "")
                {
                    try
                    {
                        propInfo.SetValue(newItem, TypeDescriptor.GetConverter(propInfo.PropertyType).ConvertFromString(input));
                    }
                    catch (ArgumentException)
                    {
                        if (update)
                        {
                            Console.WriteLine("Incorrect input value");
                            Continue();
                        }
                        else throw new ArgumentException("Incorrect input value");
                    }
                }
            }
        }
        void SetOption(PropertyInfo navProp, PropertyInfo foreignKey, T newItem, [CallerMemberName] string callerName = "", bool update = false)
        {
            string navPropName = GetDisplayName(navProp);
            var menu = new ConsoleMenu();
            if (update)
            {
                menu.Configure(config =>
                {
                    config.Title = $"[{modelName} {callerName}]\n\n{navPropName} is currently: {navProp.GetValue(newItem)}\n{navPropName} options";
                });
            }
            else
            {
                menu.Configure(config =>
                {
                    config.Title = $"[{modelName} {callerName}]\n\n{navPropName} is required\n{navPropName} options";
                });
            }
            menu.Configure(config =>
            {
                config.EnableWriteTitle = true;
            });
            var restDict = optionsDict[navProp.Name];
            var options = (restDict["get"] as RestGetDelegate<Model>).Invoke((string)restDict["endpoint"]);
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
                menu.Add(option.ToString(), (thisMenu) => { foreignKey.SetValue(newItem, id); navProp.SetValue(newItem, option); thisMenu.CloseMenu(); });
            }
            if (update) menu.Add("Exit", ConsoleMenu.Close);
            menu.Show();
        }
        public void ShowMenu()
        {
            MethodsMenu.Show();
        }
        static string GetDisplayName(PropertyInfo prop)
        {
            var attribute = prop.GetCustomAttribute<DisplayNameAttribute>();
            if (attribute == null) return prop.Name;
            return attribute.DisplayName;
        }
        static string GetDisplayName(Type type)
        {
            var attribute = type.GetCustomAttribute<DisplayNameAttribute>();
            if (attribute == null) return type.Name;
            return attribute.DisplayName;
        }
    }
}