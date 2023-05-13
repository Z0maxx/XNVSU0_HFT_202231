using System.Collections.Generic;
using WpfClient.Logics.GenericLogics;
using WpfClient.Logics.GenericLogics.Interfaces;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.TableModelLogics.JobLogics
{
    class JobEmployeesLogic : InnerListLogic<Job, FixedWageEmployee, HourlyWageEmployee>
    {
        public JobEmployeesLogic(ILiveRestService<Job> liveRestService, ISingleService<FixedWageEmployee> fweSingleService, ISingleService<HourlyWageEmployee> hweSingleService) :
            base(liveRestService, fweSingleService, hweSingleService, new string[] { "FixedWageEmployees", "HourlyWageEmployees" })
        {
        }
    }
}
