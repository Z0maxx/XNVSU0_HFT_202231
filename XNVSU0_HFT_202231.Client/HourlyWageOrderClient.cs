using System.Collections.Generic;
using System.ComponentModel;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Client
{
    [DisplayName("Hourly wage order")]
    class HourlyWageOrderClient : Client<HourlyWageOrder>, IClient
    {
        public HourlyWageOrderClient(RestService rest, string[] args)
            : base(rest, args, new string[] { "Id", "FirstName", "LastName", "OrderDate", "EmailAddress", "Hours", "EmployeeId" })
        {
            optionsDict.Add(
                "Employee",
                new Dictionary<string, object>() {
                    { "get", new RestGetDelegate<IModel>(rest.Get<HourlyWageEmployee>) },
                    { "endpoint", "hourlywageemployee" }
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