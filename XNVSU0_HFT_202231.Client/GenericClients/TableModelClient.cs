using ConsoleTools;
using System;
using System.Reflection;
using System.ComponentModel;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using XNVSU0_HFT_202231.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace XNVSU0_HFT_202231.Client
{
    abstract class TableModelClient<T> : IClient where T : TableModel, new()
    {
        protected readonly RestService rest;
        protected readonly string[] args;
        protected readonly string endpoint;
        protected readonly string[] propOrder;
        protected delegate IEnumerable<S> RestGetDelegate<S>(string endpoint) where S : TableModel;
        protected readonly Dictionary<string, Dictionary<string, object>> optionsDict;
        protected readonly ConsoleMenu MethodsMenu;
        protected static readonly string modelName = GetDisplayName(typeof(T));
        public TableModelClient(RestService rest, string[] args, string[] propOrder)
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
                .Add(GetDisplayName(Create), () => Create())
                .Add(GetDisplayName(Read), () => Read())
                .Add(GetDisplayName(ReadAll), () => ReadAll())
                .Add(GetDisplayName(Update), () => Update())
                .Add(GetDisplayName(Delete), () => Delete())
                .Add("Exit", ConsoleMenu.Close);
        }
        [DisplayName("Get by id")]
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
                catch (ArgumentException e)
                {
                    DisplayError(e.Message);
                }
            }
            else DisplayError($"This is not a number: {input}");
            Continue();
        }
        [DisplayName("List all")]
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
        [DisplayName("Create new")]
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
                DisplayError(e.Message);
            }
            Continue();
        }
        [DisplayName("Update by id")]
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
                DisplayError(e.Message);
            }
            Continue();
        }
        [DisplayName("Delete by id")]
        public void Delete()
        {
            DisplayOperation();
            Console.Write("Enter id: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int id))
            {
                DisplayProcessing();
                DisplayResult(rest.Delete(id, endpoint));
            }
            else DisplayError($"This is not a number: {input}");
            Continue();
        }
        public T GetNewItem(string callerName = "", bool update = false)
        {
            if (callerName == "")
            {
                callerName = GetDisplayName(new StackTrace().GetFrame(1).GetMethod());
            }
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
                            config.Title = $"[{modelName} | {callerName}]\n\nSelect a property of {modelName} to update";
                            config.EnableWriteTitle = true;
                        });
                    foreach (var prop in propOrder.Where(p => p != "Id"))
                    {
                        var propInfo = propInfos.First(p => p.Name == prop);
                        if (propInfo.GetAccessors().FirstOrDefault(a => a.IsVirtual) == null)
                        {
                            if (prop.Contains("Id"))
                            {
                                var navProp = propInfos.First(p => p.Name == prop.Substring(0, prop.Length - 2));
                                updateMenu.Add($"{GetDisplayName(navProp)}", () => SetPropValue(newItem, propInfo, callerName, navProp, update));
                            }
                            else updateMenu.Add($"{GetDisplayName(propInfo)}", () => SetPropValue(newItem, propInfo, callerName, update: true));
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
                        if (prop != "Id" && prop.Contains("Id"))
                        {
                            var navProp = propInfos.First(p => p.Name == prop.Substring(0, prop.Length - 2));
                            SetPropValue(newItem, propInfo, callerName, navProp);
                        }
                        else SetPropValue(newItem, propInfo, callerName);
                    }
                }
            }
            return newItem;
        }
        void DisplayProperties(TableModel item, string level = "")
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
                    DisplayProperties(propValue as TableModel, newLevel);
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
                callerName = GetDisplayName(new StackTrace().GetFrame(1).GetMethod());
            }
            Console.Clear();
            Console.WriteLine($"[{modelName} | {callerName}]\n");
        }
        static void DisplayResult(Result result, string callerName = "")
        {
            var caller = new StackTrace().GetFrame(1).GetMethod();
            if (callerName == "")
            {
                callerName = caller.Name;
            }
            ConsoleColor originalColor = Console.ForegroundColor;
            ConsoleColor originalBColor = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Black;
            DisplayOperation(GetDisplayName(caller));
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
        static void DisplayError(string error, string callerName = "")
        {
            if (callerName == "")
            {
                callerName = GetDisplayName(new StackTrace().GetFrame(1).GetMethod());
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
        static void DisplayProcessing(string callerName = "")
        {
            if (callerName == "")
            {
                callerName = GetDisplayName(new StackTrace().GetFrame(1).GetMethod());
            }
            DisplayOperation(callerName);
            Console.WriteLine("Processing");
        }
        void SetPropValue(T newItem, PropertyInfo propInfo, string callerName, PropertyInfo navProp = null, bool update = false)
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
                        var error = "Incorrect input value";
                        if (update)
                        {
                            DisplayError(error);
                            Continue();
                        }
                        else throw new ArgumentException(error);
                    }
                }
            }
        }
        void SetOption(PropertyInfo navProp, PropertyInfo foreignKey, T newItem, string callerName, bool update = false)
        {
            string navPropName = GetDisplayName(navProp);
            var menu = new ConsoleMenu();
            if (update)
            {
                menu.Configure(config =>
                {
                    config.Title = $"[{modelName} | {callerName}]\n\n{navPropName} is currently: {navProp.GetValue(newItem)}\n{navPropName} options";
                });
            }
            else
            {
                menu.Configure(config =>
                {
                    config.Title = $"[{modelName} | {callerName}]\n\n{navPropName} is required\n{navPropName} options";
                });
            }
            menu.Configure(config =>
            {
                config.EnableWriteTitle = true;
            });
            var restDict = optionsDict[navProp.Name];
            var options = (restDict["get"] as RestGetDelegate<TableModel>).Invoke((string)restDict["endpoint"]);
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
                menu.Add(option.ToString(), (thisMenu) => { foreignKey.SetValue(newItem, id); thisMenu.CloseMenu(); });
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
        static string GetDisplayName(Action a)
        {
            var attribute = a.Method.GetCustomAttribute<DisplayNameAttribute>();
            if (attribute == null) return a.Method.Name;
            return attribute.DisplayName;
        }
        static string GetDisplayName(MethodBase m)
        {
            var attribute = m.GetCustomAttribute<DisplayNameAttribute>();
            if (attribute == null) return m.Name;
            return attribute.DisplayName;
        }
    }
}