using Microsoft.AspNetCore.Mvc;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Components
{
    public class TeamsViewComponent : ViewComponent
    {
        private IBowlersRepository repo { get; set; }

        public TeamsViewComponent (IBowlersRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedTeam = RouteData?.Values["bowlerTeam"];

            var teams = repo.Bowlers
                .Select(x => x.TeamID)
                .Distinct()
                .OrderBy(x => x);

            return View(teams);
        }
    }
}
