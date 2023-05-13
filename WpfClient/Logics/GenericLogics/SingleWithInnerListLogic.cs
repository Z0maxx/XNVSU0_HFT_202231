using WpfClient.Logics.GenericLogics.Interfaces;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.GenericLogics
{
    class SingleWithInnerListLogic<T> : SingleLogic<T>, ISingleWithInnerListLogic<T> where T : TableModel
    {
        private readonly IInnerListService<T> innerListService;
        public SingleWithInnerListLogic(ILiveRestService<T> liveRestService, IUpdateService<T> updateService, IInnerListService<T> innerListService)
            : base(liveRestService, updateService)
        {
            this.innerListService = innerListService;
        }
        public void InnerList()
        {
            if (Single.Item != null)
            {
                innerListService.InnerList(Single.Item);
            }
        }
    }
}
