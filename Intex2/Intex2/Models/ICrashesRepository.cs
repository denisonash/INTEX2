using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models
{
    public interface ICrashesRepository
    {
        IQueryable<Crash> Crashes { get; }
        public void SaveCrash(Crash c);
        public void CreateCrash(Crash c);
        public void DeleteCrash(Crash c);
        public int GetMaxId();
        public List<string> GetCounties();
        public List<string> GetCities();
        //public IQueryable<Crash> FilteredCrashes(string city, string county, int severity, int pageNum, int pageSize);
    }
}
