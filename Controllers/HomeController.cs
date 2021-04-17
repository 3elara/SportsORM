using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsORM.Models;


namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context _context;

        public HomeController(Context DBContext)
        {
            _context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = _context.Leagues
                .Where(l => l.Sport.Contains("Baseball"))
                .ToList();
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
            ViewBag.Womens = _context.Leagues.Where(Womens => Womens.Name.Contains("Womens")).ToList();  
            ViewBag.SportLeagues = _context.Leagues.Where(Sport => Sport.Sport.Contains("Hockey")).ToList();
            ViewBag.OtherSportLeagues= _context.Leagues.Where(OtherSport => OtherSport.Sport != "Football").ToList();
            ViewBag.Conferences= _context.Leagues.Where(Conference => Conference.Name.Contains("Conference")).ToList();
            ViewBag.Atlantic= _context.Leagues.Where(Atlantic => Atlantic.Name.Contains("Atlantic")).ToList();
            ViewBag.Dallas= _context.Teams.Where(Dallas => Dallas.Location.Contains("Dallas")).ToList();
            ViewBag.Raptors= _context.Teams.Where(Raptors => Raptors.TeamName.Contains("Raptors")).ToList();
            ViewBag.City= _context.Teams.Where(City => City.Location.Contains("City")).ToList();
            ViewBag.T= _context.Teams.Where(T => T.TeamName.StartsWith("T")).ToList();
            ViewBag.Alpha= _context.Teams.OrderBy(Alpha=>Alpha.Location).ToList();
            ViewBag.Reverse= _context.Teams.OrderByDescending(Reverse => Reverse.Location).ToList();
            ViewBag.Cooper= _context.Players.Where(Cooper=> Cooper.LastName.Contains("Cooper")).ToList();
            ViewBag.Joshua= _context.Players.Where(Joshua=> Joshua.FirstName.Contains("Joshua")).ToList();
            ViewBag.CooperNotJoshua = _context.Players.Where(CooperNotJoshua =>CooperNotJoshua.LastName.Contains("Cooper") && CooperNotJoshua.FirstName != "Joshua").ToList();
            ViewBag.AlexOrWyatt = _context.Players.Where(AlexOrWyatt =>AlexOrWyatt.FirstName.Contains("Alexander") ||AlexOrWyatt.FirstName.Contains("Wyatt")).ToList();
            return View();
        }

        

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
                List<Team> AtlanticTeams=_context.Teams.Include(t=>t.CurrLeague).Where(t=>t.CurrLeague.Name == "Atlantic Soccer Conference").ToList();
                ViewBag.Atlantic=AtlanticTeams;

                List<Player> Penguins=_context.Players.Include(t=>t.CurrentTeam).Where(t=>t.CurrentTeam.TeamName == "Penguins" && t.CurrentTeam.Location=="Boston").ToList();
                ViewBag.Penguins=Penguins;

                List<Player> Baseball=_context.Players.Include(t=>t.CurrentTeam).Where(p=>p.CurrentTeam.CurrLeague.Name == "International Collegiate Baseball Conference").ToList();
                ViewBag.BaseballPlayers=Baseball;    

                List<Player> Lopez=_context.Players.Include(p=>p.CurrentTeam).Where(p=>p.CurrentTeam.CurrLeague.Name == "American Conference of Amatueur Football" && p.LastName=="Lopez").ToList();
                ViewBag.Lopez=Lopez;   


            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            return View();
        }

    }
}