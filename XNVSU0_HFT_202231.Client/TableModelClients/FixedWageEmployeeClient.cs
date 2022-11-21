using XNVSU0_HFT_202231.Models.TableModels;
using System.Collections.Generic;
using System.ComponentModel;

namespace XNVSU0_HFT_202231.Client
{
    [DisplayName("Fixed wage employee")]
    class FixedWageEmployeeClient : TableModelClient<FixedWageEmployee>, IClient
    {
        public FixedWageEmployeeClient(RestService rest, string[] args)
            :base(rest, args, new string[] { "Id", "FirstName", "LastName", "Wage", "HireDate", "EmailAddress", "Hours", "PhoneNumber", "JobId" })
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