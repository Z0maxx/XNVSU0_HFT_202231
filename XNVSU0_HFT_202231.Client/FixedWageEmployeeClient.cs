using XNVSU0_HFT_202231.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace XNVSU0_HFT_202231.Client
{
    [DisplayName("Fixed wage employee")]
    class FixedWageEmployeeClient : Client<FixedWageEmployee>, IClient
    {
        public FixedWageEmployeeClient(RestService rest, string[] args)
            :base(rest, args, new string[] { "Id", "FirstName", "LastName", "Wage", "HireDate", "EmailAddress", "Hours", "PhoneNumber", "JobId" })
        {
            optionsDict.Add(
                "Job",
                new Dictionary<string, object>()
                {
                    { "get", new RestGetDelegate<IModel>(rest.GetList<Job>) },
                    { "endpoint", "job" }
                }
            );
        }
    }
}