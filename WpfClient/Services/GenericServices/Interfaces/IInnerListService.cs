using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.GenericServices.Interfaces
{
    interface IInnerListService<T> where T : TableModel
    {
        void InnerList(T item);
    }
}
