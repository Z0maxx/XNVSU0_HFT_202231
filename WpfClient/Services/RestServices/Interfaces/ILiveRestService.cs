using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.RestServices.Interfaces
{
    interface ILiveRestService<T> where T : TableModel
    {
        RestCollection<T> List { get; }
        RestSingle<T> Single { get; }
    }
}
