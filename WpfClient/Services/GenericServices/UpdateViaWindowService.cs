using System.Windows;
using WpfClient.Services.GenericServices.Interfaces;
using WpfClient.Windows;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.GenericServices
{
    abstract class UpdateViaWindowService<T, W> : IUpdateService<T> where T : TableModel where W : IParameteredWindow<T>, new()
    {
        public void Update(T item)
        {
            W window = new();
            window.Setup(item);
            window.ShowDialog();
        }
    }
}
