using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.RestServices
{
    class JobLiveRestService : LiveRestService<Job>
    {
        public JobLiveRestService() : base("http://localhost:14784/api/", "job", "hub")
        {
        }
    }
}
