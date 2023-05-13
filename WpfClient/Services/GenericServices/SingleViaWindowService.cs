using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Windows;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.GenericServices
{
    class SingleViaWindowService<T, W> : ISingleService<T> where T : TableModel where W : IParameteredWindow<T>, new()
    {
        public void Single(T item)
        {
            W window = new();
            window.Setup(item);
            window.ShowDialog();
        }
    }
}
