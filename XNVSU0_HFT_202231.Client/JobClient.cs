using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Client
{
    class JobClient : Client<Job>
    {
        public JobClient(RestService rest, string[] args)
            :base(rest, args, new string[] { "Id", "Name" })
        {
        }
    }
}