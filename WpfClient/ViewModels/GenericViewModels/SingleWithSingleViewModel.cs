using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Linq;
using System.Windows.Input;
using WpfClient.Logics.GenericLogics.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.GenericViewModels
{
    abstract class SingleWithSingleViewModel<T, S> : SingleViewModel<T> where T : TableModel where S : TableModel
    {
        public ICommand SingleCommand { get; }
        public string SingleName { get; }
        public override T? Item {
            get => base.Item;
            protected set
            {
                base.Item = value;
                (SingleCommand as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }
        public SingleWithSingleViewModel(string[] propOrder, string singleName) :
            this(IsInDesignMode ? null : Ioc.Default.GetService<ISingleWithSingleLogic<T, S>>(), propOrder, singleName)
        {
        }
        public SingleWithSingleViewModel(ISingleWithSingleLogic<T, S>? logic, string[] propOrder, string singleName) :
            base(logic, propOrder)
        {
            SingleName = singleName;

            SingleCommand = new RelayCommand(
            () =>
            {
                logic?.ShowSingle();
            },
            () =>
            {
                if (Item == null) return false;
                var propInfos = typeof(T).GetProperties();
                var value = propInfos.First(p => p.PropertyType.Name == typeof(S).Name).GetValue(Item);
                return value != null;
            });
        }
    }
}
