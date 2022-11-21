using System.Collections.Generic;
using System.ComponentModel;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Client
{
    [DisplayName("Hourly wage order")]
    class HourlyWageOrderClient : TableModelClient<HourlyWageOrder>, IClient
    {
        public HourlyWageOrderClient(RestService rest, string[] args)
            : base(rest, args, new string[] { "Id", "FirstName", "LastName", "OrderDate", "EmailAddress", "EmployeeId", "Hours" })
        {
            optionsDict.Add(
                "Employee",
                new Dictionary<string, object>() {
                    { "get", new RestGetDelegate<TableModel>(rest.GetList<HourlyWageEmployee>) },
                    { "endpoint", "hourlywageemployee" }
                }
            );
            optionsDict.Add(
                "EventType",
                new Dictionary<string, object>()
                {
                    { "get", new RestGetDelegate<TableModel>(rest.GetList<EventType>) },
                    {"endpoint", "eventtype" }
                }
            );
        }
    }
}