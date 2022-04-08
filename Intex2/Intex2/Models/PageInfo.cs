using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//This is how we collected filtering and pagination information for the ViewCrashes page

namespace Intex2.Models
{
    public class PageInfo
    {
        public int TotalNumCrashes { get; set; }
        public int CrashesPerPage { get; set; }
        public int CurrentPage { get; set; }
        //figure out how many pages that we need
        public int TotalPages => (int)Math.Ceiling((double)TotalNumCrashes / CrashesPerPage);
        public string CurrentCounty { get; set; }
        public string CurrentCity { get; set; }
        public int CurrentSeverity { get; set; }
    }
}


