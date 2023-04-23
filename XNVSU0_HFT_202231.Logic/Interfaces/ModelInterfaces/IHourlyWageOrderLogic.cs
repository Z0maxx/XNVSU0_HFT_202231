using System.Collections.Generic;
using XNVSU0_HFT_202231.Models.TableModels;
using XNVSU0_HFT_202231.Models.StatModels;
using Microsoft.VisualBasic;
using System;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IHourlyWageOrderLogic : ILogic<HourlyWageOrder>
    {
        public IEnumerable<IncomeFromOrder> Overview();
        public IEnumerable<HourlyWageOrder> GetAllForCustomer(DateTime? orderDate, string firstName, string lastName, string emailAddress);
        public IEnumerable<HourlyWageEmployee> GetAvailableEmployeesForOrder(Order order);
    }
}