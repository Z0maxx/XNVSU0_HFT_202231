using WpfClient.Logics.GenericLogics.Interfaces;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.GenericLogics
{
    abstract class ListWithInnerListLogic<T> : ListLogic<T>, IListWithInnerListLogic<T> where T : TableModel
    {
        private readonly IInnerListService<T> innerListService;
        public ListWithInnerListLogic(ILiveRestService<T> liveRestService, IUpdateService<T> updateService, IInnerListService<T> innerListService, ICreateService<T> createService)
            :base(liveRestService, updateService, createService)
        {
            this.innerListService = innerListService;
        }
        public void InnerList(T item)
        {
            innerListService.InnerList(item);
        }
    }
}
