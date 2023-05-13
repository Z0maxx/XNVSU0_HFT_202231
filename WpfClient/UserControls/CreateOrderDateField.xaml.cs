using System;
using System.Windows.Controls;

namespace WpfClient.UserControls
{
    /// <summary>
    /// Interaction logic for OrderDateField.xaml
    /// </summary>
    public partial class CreateOrderDateField : UserControl
    {
        public CreateOrderDateField()
        {
            InitializeComponent();
            DateTime now = DateTime.Now;
            dtp.Minimum = new DateTime(now.Year, now.Month, now.Day + 1, 8, 0, 0);
            dtp.DefaultValue = new DateTime(now.Year, now.Month, now.Day + 1, 8, 0, 0);
        }
    }
}
