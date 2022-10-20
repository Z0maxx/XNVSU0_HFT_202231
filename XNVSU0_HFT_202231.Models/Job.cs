using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNVSU0_HFT_202231.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
        public Job()
        {
        }
        public Job(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
