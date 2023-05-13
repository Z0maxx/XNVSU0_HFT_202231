using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using WpfClient.Services.RestServices;
using WpfClient.Logics.GenericLogics.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace WpfClient.Logics.GenericLogics
{
    class SingleLogic<T> : ISingleLogic<T> where T : TableModel
    {
        private readonly ILiveRestService<T> liveRestService;
        private readonly IUpdateService<T> updateService;
        public RestSingle<T> Single { get => liveRestService.Single; }
        public SingleLogic(ILiveRestService<T> liveRestService, IUpdateService<T> updateService)
        {
            this.liveRestService = liveRestService;
            this.updateService = updateService;
        }

        public void Update()
        {
            if (Single.Item != null)
            {
                updateService.Update(Single.Item);
            }
        }
        public async Task<Result?> Delete()
        {
            if (Single.Item != null && Single.Item.Id != null)
            {
                return await liveRestService.List.Delete((int)Single.Item.Id);
            }
            return null;
        }
        public void Setup(T item)
        {
            if (item.Id != null)
            {
                liveRestService.Single.Setup((int)item.Id);
            }
        }
    }
}
