using System.Windows;
using WpfClient.Services.GenericServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.GenericServices
{
    class CreateViaWindowService<T, W> : ICreateService<T> where T : TableModel, new() where W : Window, new()
    {
        public void Create()
        {
            W window = new();
            window.ShowDialog();
        }
    }
}
