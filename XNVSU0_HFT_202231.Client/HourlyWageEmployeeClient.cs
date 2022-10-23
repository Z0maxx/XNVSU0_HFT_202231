using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Client
{
    class HourlyWageEmployeeClient : Client<HourlyWageEmployee>
    {
        public HourlyWageEmployeeClient(RestService rest, string[] args)
            : base(rest, args, new string[] { "Id", "FirstName", "LastName", "Wage", "HireDate", "EmailAddress", "PhoneNumber", "MinHours", "MaxHours", "JobId" })
        {
            optionsDict.Add("Job", (new ModelDelegate<IModel>(rest.Get<Job>), "Job"));
        }
    }
}