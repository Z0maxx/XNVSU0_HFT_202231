using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.ViewModels.TableModelViewModels
{
    public static class PropOrders
    {
        private static readonly string[] job = { "Id", "Name" };
        private static readonly string[] eventtype = { "Id", "Name" };
        private static readonly string[] fixedWageEmployee = { "Id", "HireDate", "FirstName", "LastName", "Job", "Wage", "Hours", "EmailAddress", "PhoneNumber" };
        private static readonly string[] fixedWageOrder = { "Id", "OrderDate", "FirstName", "LastName", "EventType", "EmailAddress", "PhoneNumber", "Employee.FirstName", "Employee.LastName" };
        private static readonly string[] hourlyWageEmployee = { "Id", "HireDate", "FirstName", "LastName", "Job", "Wage", "MinHours", "MaxHours", "EmailAddress", "PhoneNumber" };
        private static readonly string[] hourlyWageOrder = { "Id", "OrderDate", "FirstName", "LastName", "Hours", "EmailAddress", "PhoneNumber", "Employee.FirstName", "Employee.LastName" };
        private static readonly string[] innerListOrder = { "Id", "OrderDate", "FirstName", "LastName", "EmailAddress", "PhoneNumber" };
        private static readonly string[] innerListEmployee = { "Id", "HireDate", "FirstName", "LastName", "EmailAddress", "PhoneNumber" };
        public static string[] Job { get => job; }
        public static string[] Eventtype { get => eventtype; }
        public static string[] FixedWageEmployee { get => fixedWageEmployee; }
        public static string[] FixedWageOrder { get => fixedWageOrder; }
        public static string[] HourlyWageEmployee { get => hourlyWageEmployee; }
        public static string[] HourlyWageOrder { get => hourlyWageOrder; }
        public static string[] InnerListOrder { get => innerListOrder; }
        public static string[] InnerListEmployee { get => innerListEmployee; }
    }
}
