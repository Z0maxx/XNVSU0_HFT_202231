using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.RestServices
{
    abstract class LiveRestService<T> : ILiveRestService<T> where T : TableModel
    {
        protected readonly string baseurl;
        protected readonly string endpoint;
        public RestCollection<T> List { get; }
        public RestSingle<T> Single { get; }
        public LiveRestService(string baseurl, string endpoint, string hub)
        {
            List = new RestCollection<T>(baseurl, endpoint, hub);
            Single = new RestSingle<T>(baseurl, endpoint, hub);
            this.baseurl = baseurl;
            this.endpoint = endpoint;
        }
        
    }
}
