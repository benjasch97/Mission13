using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        private IBowlersRepository _repo { get; set; }

        // Constructor
        public HomeController(IBowlersRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index()
        {
            var blah = _repo.Bowlers.ToList();

            return View(blah);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var x = new Bowler();

            return View(x);
        }

        [HttpPost]
        public IActionResult Add(Bowler b)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(b);
                _repo.Save(b);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int bowlerid)
        {
            var x = _repo.Bowlers.Single(x => x.BowlerID == bowlerid);
            
            return View("Edit", x);
            
        }

        [HttpPost]
        public IActionResult Edit(Bowler b, int bowlerid)
        {
            if (ModelState.IsValid)
            {
                Bowler oldbowler = _repo.Bowlers.Single(x => x.BowlerID == bowlerid);
                _repo.Delete(oldbowler);
                _repo.Add(b);
                

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int bowlerid)
        {
            Bowler bowler = _repo.Bowlers.Single(x => x.BowlerID == bowlerid);
            _repo.Delete(bowler);

            return RedirectToAction("Index");
        }

    }
}
