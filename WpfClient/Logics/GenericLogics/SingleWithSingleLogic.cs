using System.Linq;
using WpfClient.Logics.GenericLogics.Interfaces;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.GenericLogics
{
    class SingleWithSingleLogic<T, S> : SingleLogic<T>, ISingleWithSingleLogic<T, S> where T : TableModel where S : TableModel
    {
        private readonly ISingleService<S> singleService;
        public SingleWithSingleLogic(ILiveRestService<T> liveRestService, IUpdateService<T> updateService, ISingleService<S> singleService)
            : base(liveRestService, updateService)
        {
            this.singleService = singleService;
        }
        public void ShowSingle()
        {
            var propInfos = typeof(T).GetProperties().ToList();
            var value = propInfos.First(p => p.PropertyType.Name == typeof(S).Name).GetValue(Single.Item);
            if (value is S innerItem)
            {
                singleService.Single(innerItem);
            }
        }
    }
}
