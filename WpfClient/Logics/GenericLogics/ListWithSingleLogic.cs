using System.Linq;
using WpfClient.Logics.GenericLogics.Interfaces;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using Xceed.Wpf.Toolkit.Primitives;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.GenericLogics
{
    abstract class ListWithSingleLogic<T, S> : ListLogic<T>, IListWithSingleLogic<T, S> where T : TableModel where S : TableModel
    {
        private readonly ISingleService<S> singleService;
        public ListWithSingleLogic(ILiveRestService<T> liveRestService, IUpdateService<T> updateService, ICreateService<T> createService, ISingleService<S> singleService)
            : base(liveRestService, updateService, createService)
        {
            this.singleService = singleService;
        }
        public void Single(T item)
        {
            var propInfos = typeof(T).GetProperties().ToList();
            var value = propInfos.First(p => p.PropertyType.Name == typeof(S).Name).GetValue(item);
            if (value is S innerItem)
            {
                singleService.Single(innerItem);
            }
        }
    }
}
