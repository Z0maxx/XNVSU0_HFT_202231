using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Client
{
    class HourlyWageOrderClient : Client<HourlyWageOrder>
    {
        public HourlyWageOrderClient(RestService rest, string[] args)
            : base(rest, args, new string[] { "Id", "FirstName", "LastName", "OrderDate", "EmailAddress", "Hours", "EmployeeId" })
        {
            optionsDict.Add("Employee", (new ModelDelegate<IModel>(rest.Get<HourlyWageEmployee>), "HourlyWageEmployee"));
        }
    }
}