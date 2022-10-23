using System.ComponentModel;
using System.Collections.Generic;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Client
{
    [DisplayName("Fixed wage order")]
    class FixedWageOrderClient : Client<FixedWageOrder>, IClient
    {
        public FixedWageOrderClient(RestService rest, string[] args)
            : base(rest, args, new string[] { "Id", "FirstName", "LastName", "OrderDate", "EmailAddress", "EmployeeId", "EventTypeId" })
        {
            optionsDict.Add(
                "Employee",
                new Dictionary<string, object>() {
                    { "get", new RestGetDelegate<IModel>(rest.Get<FixedWageEmployee>) },
                    { "endpoint", "fixedwageemployee" }
                }
            );
            optionsDict.Add(
                "EventType",
                new Dictionary<string, object>()
                {
                    { "get", new RestGetDelegate<IModel>(rest.Get<EventType>) },
                    {"endpoint", "eventtype" }
                }
            );
        }
    }
}