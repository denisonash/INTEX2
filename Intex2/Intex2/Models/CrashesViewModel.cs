using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models
{
    public class CrashesViewModel
    {
        public IQueryable<Crash> Crashes { get; set; }
        public PageInfo PageInfo { get; set; }
        public int CrashSev { get; set; }
        public string CrashCounty { get; set; }
        public string CrashCity { get; set; }
    }
}

        

