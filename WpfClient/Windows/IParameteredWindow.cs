using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfClient.Windows
{
    interface IParameteredWindow<T>
    {
        public void Setup(T item);
        public bool? ShowDialog();
    }
}
