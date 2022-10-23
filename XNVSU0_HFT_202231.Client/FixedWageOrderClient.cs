using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Client
{
    class FixedWageOrderClient : Client<FixedWageOrder>
    {
        public FixedWageOrderClient(RestService rest, string[] args)
            : base(rest, args, new string[] { "Id", "FirstName", "LastName", "OrderDate", "EmailAddress", "EmployeeId", "EventTypeId" })
        {
            optionsDict.Add("Employee", (new ModelDelegate<IModel>(rest.Get<FixedWageEmployee>), "FixedWageEmployee"));
            optionsDict.Add("EventType", (new ModelDelegate<IModel>(rest.Get<EventType>), "EventType"));
        }
    }
}