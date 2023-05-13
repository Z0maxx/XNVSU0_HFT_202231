using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.GenericLogics.Interfaces
{
    interface ISingleWithSingleLogic<T, S> : ISingleLogic<T> where T : TableModel where S : TableModel
    {
        public void ShowSingle();
    }
}
