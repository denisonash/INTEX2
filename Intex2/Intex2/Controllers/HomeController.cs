using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Intex2.Models;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;


namespace Intex2.Controllers
{
    public class HomeController : Controller
    {
        private InferenceSession _session;
        private ICrashesRepository _repo { get; set; }
        public HomeController(ICrashesRepository temp, InferenceSession session)
        {
            _repo = temp;
            _session = session;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult StatsBreakdown()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Story()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Story(CrashData data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("int64_input", data.AsTensor())
            });
            Tensor<long> score = result.First().AsTensor<long>();
            var prediction = new Prediction { PredictedValue = score.First() * 1 };
            result.Dispose();
            ViewBag.result = prediction.PredictedValue;
            return View("Result");
        }

        [HttpGet]
        public IActionResult AddCrash()
        {
            ViewBag.CrashId = _repo.GetMaxId() +1;

            return View("AddCrash");
        }

        [HttpPost]
        public IActionResult AddCrash(Crash c)
        {

            if (ModelState.IsValid)
            {
                _repo.CreateCrash(c);
                return RedirectToAction("ViewCrashes");
            }
            else
            {
                return View(c);
            }
        }

        [HttpGet]
        public IActionResult Edit(int crashid)
        {
            var crash = _repo.Crashes.Single(x => x.crash_id == crashid);
            ViewBag.CrashId = crash.crash_id;

            return View("AddCrash", crash);

        }

        [HttpPost]
        public IActionResult Edit(Crash c)
        {
            if (ModelState.IsValid)
            {
                _repo.SaveCrash(c);

                return RedirectToAction("ViewCrashes");
            }
            else
            {
                return RedirectToAction ("Edit", c);
            }

        }

        [HttpGet]
        public IActionResult Delete(int crashid)
        {
            var crash = _repo.Crashes.Single(x => x.crash_id == crashid);

            return View("Delete", crash);

        }
        [HttpPost]
        public IActionResult Delete(Crash c)
        {
            _repo.DeleteCrash(c);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Summary(int crashid)
        {
            var crash = _repo.Crashes.Single(x => x.crash_id == crashid);
            ViewBag.CrashId = crash.crash_id;

            return View("Summary", crash);

        }
        public IActionResult ViewCrashes(int pageNum = 1)
        {
            int pageSize = 20;
            List<Crash> AllCrashes = _repo.Crashes.Take(20).ToList();
            //AllAccidents = AllAccidents.Where(x => x.CRASH_DATETIME.ToString("yyyy") == "2019" && x.CRASH_DATETIME.ToString("M") == "12").ToList();
            ViewBag.crashes = AllCrashes;
            ViewBag.Counties = _repo.GetCounties();
            ViewBag.Cities = _repo.GetCities();
            var x = new CrashesViewModel
            {
                Crashes = _repo.Crashes
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumCrashes = 
                        _repo.Crashes.Count(),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum

                }
            };
            return View(x);
        }
        public IActionResult Filter(CrashesViewModel blah, string city, string county, int severity, int pageNum = 1)
        {
            //var blah = _repo.Crashes
            //    .Where(x => x.crash_severity_id == teamID)
            //    .ToList();
            //ViewBag.Teams = _repo.Crashes.ToList();
            //Team team = _repo.Teams.Single(x => x.TeamID == teamID);
            //ViewBag.SelectedTeam = team.TeamName;
            string crashCounty = blah.CrashCounty;
            if (String.IsNullOrEmpty(county))
            {
                crashCounty = blah.CrashCounty;
            }
            else
            {
                crashCounty = county;
            }
            int crashSeverity = blah.CrashSev;
            if (severity == 0)
            {
                crashSeverity = blah.CrashSev;
            }
            else
            {
                crashSeverity = severity;
            }
            string crashCity = blah.CrashCity;
            if (String.IsNullOrEmpty(city))
            {
                crashCity = blah.CrashCity;
            }
            else
            {
                crashCity = city;
            }
            int pageSize = 20;
            ViewBag.Counties = _repo.GetCounties();
            ViewBag.Cities = _repo.GetCities();
            var x = new CrashesViewModel
            {
                //Crashes = _repo.FilteredCrashes(city, county, crashSeverity, pageNum, pageSize),
                Crashes = _repo.Crashes
                    .Where(c => c.city == crashCity || crashCity == null)
                    .Where(c => c.county_name == crashCounty || crashCounty == null)
                    .Where(c => c.crash_severity_id == crashSeverity || crashSeverity == 0)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),
                CrashSev = crashSeverity,
                CrashCounty = crashCounty,
                CrashCity = crashCity,
                PageInfo = new PageInfo
                {
                    //TotalNumCrashes =
                    //    _repo.FilteredCrashes(city, county, crashSeverity, pageNum, pageSize).Count(),
                    //CrashesPerPage = pageSize,
                    //CurrentPage = pageNum
                    TotalNumCrashes = _repo.Crashes
                    .Where(c => c.city == crashCity || crashCity == null)
                    .Where(c => c.county_name == crashCounty || crashCounty == null)
                    .Where(c => c.crash_severity_id == crashSeverity || crashSeverity == 0)
                    .Count(),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum,
                    CurrentCity = crashCity,
                    CurrentCounty = crashCounty,
                    CurrentSeverity = crashSeverity,
                },
            };
            return View("ViewCrashes", x);
        }

        public IActionResult TestAdd()
        {

            List<Crash> AllCrashes = _repo.Crashes.Take(100).ToList();
            //AllAccidents = AllAccidents.Where(x => x.CRASH_DATETIME.ToString("yyyy") == "2019" && x.CRASH_DATETIME.ToString("M") == "12").ToList();
            ViewBag.crashes = AllCrashes;

            return View();
        }



    }
}
