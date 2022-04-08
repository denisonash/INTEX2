using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models
{
    //combining the List of Crashes and the page info as well as the current filtering information
    public class CrashesViewModel
    {
        public IQueryable<Crash> Crashes { get; set; }
        public PageInfo PageInfo { get; set; }
        public int CrashSev { get; set; }
        public string CrashCounty { get; set; }
        public string CrashCity { get; set; }
    }
}

        

