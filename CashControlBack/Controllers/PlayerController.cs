using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashControl.Context;
using CashControlBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CashControl.Controllers
{
    public class PlayerController : Controller
    {
        private readonly ApplicationDb dbObj; // Use a more descriptive variable name and make it private

        public PlayerController(ApplicationDb dbContext)
        {
            dbObj = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Player(Player obj)
        {
            ViewBag.TeamList = dbObj.Team.ToList(); // Populate the list here

            // If obj.Team_ID has a value, you can set the selected item in the dropdown
            if (obj.Team_ID > 0)
            {
                ViewBag.SelectedTeam = obj.Team_ID;
                ViewBag.TeamList = dbObj.Team.ToList();

            }
            else
            {
                ViewBag.SelectedTeam = null; // Ensure it's initialized even if obj.Team_ID is not set
            }

            return View(obj);
        }



        [HttpPost]
        public ActionResult AddPlayer(Player model)
        {
            Player obj = new Player();
            if (ModelState.IsValid)
            {
                obj.ID = model.ID;
                obj.Name = model.Name;
                obj.Number = model.Number;
                obj.BirthYear = model.BirthYear;
                obj.Team_ID = model.Team_ID;

                // Get the associated Artikulli object
                Team associatedTeam = GetTeamById(model.Team_ID);
                if (associatedTeam != null)
                {
                    obj.AssociatedTeam = associatedTeam;
                }

                if (model.ID == 0)
                {
                    dbObj.Player.Add(obj);
                    dbObj.SaveChanges();
                }
                else
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();
                }

                ModelState.Clear();
            }

            return View("Player");
        }

        private Team GetTeamById(int? Team_ID)
        {
            if (Team_ID == null)
            {
                return null;
            }

            return dbObj.Team.SingleOrDefault(a => a.ID == Team_ID);
        }


        public ActionResult PlayerList(string sortOrder)
        {
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.BirthYearSortParam = sortOrder == "BirthYear" ? "birthyear_desc" : "BirthYear";
            ViewBag.TeamSortParam = sortOrder == "Team" ? "team_desc" : "Team";

            var players = dbObj.Player.Include(p => p.AssociatedTeam).AsQueryable();

            switch (sortOrder)
            {
                case "name_desc":
                    players = players.OrderByDescending(p => p.Name);
                    break;
                case "BirthYear":
                    players = players.OrderBy(p => p.BirthYear);
                    break;
                case "birthyear_desc":
                    players = players.OrderByDescending(p => p.BirthYear);
                    break;
                case "Team":
                    players = players.OrderBy(p => p.AssociatedTeam.Name);
                    break;
                case "team_desc":
                    players = players.OrderByDescending(p => p.AssociatedTeam.Name);
                    break;
                default:
                    players = players.OrderBy(p => p.Name);
                    break;
            }

            var res = players.ToList();
            return View(res);
        }

        public IActionResult FilterPlayersByTeam(int teamId)
        {
            var players = dbObj.Player.Include(p => p.AssociatedTeam)
                           .Where(p => p.Team_ID == teamId)
                           .OrderBy(p => p.Name)
                           .ToList();

            return PartialView("_PlayerListPartial", players);
        }



        public ActionResult Delete(int id)
        {
            var res = dbObj.Player.Where(x => x.ID == id).First();
            dbObj.Player.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.Player.ToList();

            return View("PlayerList", list);
        }


    }
}

