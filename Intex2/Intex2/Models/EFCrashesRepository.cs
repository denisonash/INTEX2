using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models
{
    public class EFCrashesRepository : ICrashesRepository
    {
        private CrashesDbContext _context;
        public IQueryable<Crash> Crashes => _context.Crashes;

        public EFCrashesRepository(CrashesDbContext temp)
        {
            _context = temp;
        }
        public void CreateCrash(Crash c)
        {
            _context.Add(c);
            _context.SaveChanges();
        }

        public void DeleteCrash(Crash c)
        {
            _context.Crashes.Remove(c);
            _context.SaveChanges();
        }

        public void SaveCrash(Crash c)
        {
            _context.Update(c);
            _context.SaveChanges();
        }

        public int GetMaxId()
        {
            int maxID;
            maxID = _context.Crashes.Max(p => p.crash_id);
            return maxID;
        }

        public List<string> GetCounties()
        {
            List<string> counties;
            counties = _context.Crashes
                .Select(c => c.county_name)
                .Distinct()
                .ToList();
            return counties;
        }
        public List<string> GetCities()
        {
            List<string> cities;
            cities = _context.Crashes
                .Where(c => c.city != "")
                .Select(c => c.city)
                .Distinct()
                .ToList();
            return cities;
        }
        //public IQueryable<Crash> FilteredCrashes(string city, string county, int severity, int pageNum, int pageSize)
        //{
        //    IQueryable<Crash> crashes;
        //    string where = "where";
        //    if (String.IsNullOrEmpty(city) == false)
        //    {
        //        where += "" city = ‘” +city + “‘”;
        //    }
        //    if ((String.IsNullOrEmpty(county) == false) && (where == “where”))
        //    {
        //        where += ” county_name = ‘” +county + “‘”;
        //    }
        //    else if ((String.IsNullOrEmpty(county) == false) && (where != “where”))
        //    {
        //        where += ” and county_name =‘” +county + “‘”;
        //    }
        //    if ((severity != 0) && (where == “where”))
        //    {
        //        where += ” crash_severity_id =‘” +severity + “‘”;
        //    }
        //    else if ((severity != 0) && (where != “where”))
        //    {
        //        where += ” and crash_severity_id =‘” +severity + “’”;
        //    }
        //    if (pageNum == 1)
        //    {
        //        where += ” Limit 0, “+pageSize;
        //    }
        //    else if (pageNum > 1)
        //    {
        //        where += ” Limit ” +(pageSize * (pageNum - 1)) + “, ” +(pageSize * pageNum);
        //    }
        //    crashes = _context.Crashes
        //    //.FromSqlRaw(“Select * from Crashes Limit 0, 20");
        //    .FromSqlRaw(“Select * from Crashes ” +where);
        //    return crashes;
        //}
    }
}

