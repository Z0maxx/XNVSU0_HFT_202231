using System.Collections.Generic;
using System.ComponentModel;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Client
{
    [DisplayName("Hourly wage employee")]
    class HourlyWageEmployeeClient : TableModelClient<HourlyWageEmployee>, IClient
    {
        public HourlyWageEmployeeClient(RestService rest, string[] args)
            : base(rest, args, new string[] { "Id", "FirstName", "LastName", "Wage", "HireDate", "EmailAddress", "PhoneNumber", "MinHours", "MaxHours", "JobId" })
        {
            optionsDict.Add(
                "Job",
                new Dictionary<string, object>()
                {
                    { "get", new RestGetDelegate<TableModel>(rest.GetList<Job>) },
                    { "endpoint", "job" }
                }
            );
        }
    }
}