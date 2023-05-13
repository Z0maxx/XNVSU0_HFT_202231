using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfClient.Services.RestServices;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.GenericViewModels
{
    abstract class ViewModel<T> : ObservableRecipient where T : TableModel
    {
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public string ModelName { get; }

        public ViewModel()
        {
            Type type = typeof(T);
            ModelName = type.Name;
            DisplayNameAttribute? attribute = type.GetCustomAttribute<DisplayNameAttribute>();
            if (attribute != null)
            {
                ModelName = attribute.DisplayName;
            }
        }

        protected static void ShowResult(Result? result, string action)
        {
            if (result == null || result.Success) return;

            else
            {
                string errors = string.Join('\n', result.Errors);
                Task.Run(() => MessageBox.Show(errors, $"{action} Failed"));
            }
        }
    }
}
