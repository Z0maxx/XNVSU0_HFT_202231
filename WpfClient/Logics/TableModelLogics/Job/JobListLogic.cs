using WpfClient.Logics.GenericLogics;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.TableModelLogics.JobLogics
{
    class JobListLogic : ListWithInnerListLogic<Job>
    {
        public JobListLogic(ILiveRestService<Job> liveRestService, IUpdateService<Job> updateService, IInnerListService<Job> innerListService, ICreateService<Job> createService) :
            base(liveRestService, updateService, innerListService, createService)
        {
        }
    }
}
