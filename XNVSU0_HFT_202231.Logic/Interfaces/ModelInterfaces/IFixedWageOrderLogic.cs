using System;
using System.Collections.Generic;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IFixedWageOrderLogic : ILogic<FixedWageOrder>
    {
        public double? IncomeInYear(int year);
        public IEnumerable<FixedWageOrder> GetAllForCustomer(DateTime? orderDate, string firstName, string lastName, string emailAddress);
        public IEnumerable<FixedWageEmployee> GetAvailableEmployeesForOrder(Order order);
    }
}