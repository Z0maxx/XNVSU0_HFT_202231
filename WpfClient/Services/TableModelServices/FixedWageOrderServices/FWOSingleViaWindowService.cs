using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Services.GenericServices;
using WpfClient.Windows.FixedWageOrderWindows;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.TableModelServices.FixedWageOrderServices
{
    class FWOSingleViaWindowService : SingleViaWindowService<FixedWageOrder, FWOSingleWindow>
    {
    }
}
