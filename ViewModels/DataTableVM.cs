using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMNH_05012024.ViewModels
{
    public class DataTableVM
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public string error { get; set; }
        public int start { get; set; }
        public int length { get; set; }

    }
}
