using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Logics.TableModelLogics.EmployeeLogics;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.TableModelLogics.HourlyWageEmployeeLogics
{
    class HWEOrdersLogic : EmployeeOrdersLogic<HourlyWageEmployee, HourlyWageOrder>
    {
        public HWEOrdersLogic(ILiveRestService<HourlyWageEmployee> liveRestService, ISingleService<HourlyWageOrder> singleService) : base(liveRestService, singleService)
        {
        }
    }
}
