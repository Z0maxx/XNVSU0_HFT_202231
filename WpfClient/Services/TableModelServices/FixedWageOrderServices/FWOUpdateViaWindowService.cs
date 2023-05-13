using WpfClient.Services.GenericServices;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Windows.FixedWageOrderWindows;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.TableModelServices.FixedWageOrderServices
{
    class FWOUpdateViaWindowService : UpdateViaWindowService<FixedWageOrder, FWOUpdateWindow>
    {
    }
}
