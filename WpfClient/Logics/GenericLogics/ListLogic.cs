using System.Threading.Tasks;
using WpfClient.Logics.GenericLogics.Interfaces;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics
{
    abstract class ListLogic<T> : IListLogic<T> where T : TableModel
    {
        private readonly ILiveRestService<T> liveRestService;
        private readonly IUpdateService<T> updateService;
        private readonly ICreateService<T> createService;
        public RestCollection<T> List { get; }
        public ListLogic(ILiveRestService<T> liveRestService, IUpdateService<T> updateService, ICreateService<T> createService)
        {
            this.liveRestService = liveRestService;
            this.updateService = updateService;
            this.createService = createService;
            List = liveRestService.List;
        }

        public void Create()
        {
            createService.Create();
        }

        public void Update(T item)
        {
            updateService.Update(item);
        }
        public async Task<Result?> Delete(T item)
        {
            if (item.Id != null)
            {
                return await liveRestService.List.Delete((int)item.Id);
            }
            return null;
        }
    }
}
