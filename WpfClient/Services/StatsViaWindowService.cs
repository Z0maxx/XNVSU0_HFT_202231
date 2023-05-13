using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Windows;

namespace WpfClient.Services
{
    class StatsViaWindowService : IStatsService
    {
        public void Stats()
        {
            new StatsWindow().ShowDialog();
        }
    }
}
