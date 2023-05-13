using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.GenericLogics.Interfaces
{
    interface IListWithInnerListLogic<T> : IListLogic<T> where T : TableModel
    {
        void InnerList(T item);
    }
}
