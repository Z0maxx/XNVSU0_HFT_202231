using XNVSU0_HFT_202231.Models;
using System;
using System.Collections.Generic;

namespace XNVSU0_HFT_202231.Client
{
    class FixedWageEmployeeClient : Client<FixedWageEmployee>
    {
        public FixedWageEmployeeClient(RestService rest, string[] args)
            :base(rest, args, new string[] { "Id", "FirstName", "LastName", "Wage", "HireDate", "EmailAddress", "Hours", "PhoneNumber", "JobId" })
        {
            optionsDict.Add("Job", (new ModelDelegate<IModel>(rest.Get<Job>), "Job"));
        }
    }
}