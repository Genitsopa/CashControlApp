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
    public class TeamController : Controller
    {
        private readonly ApplicationDb dbObj; // Use a more descriptive variable name and make it private

        public TeamController(ApplicationDb dbContext)
        {
            dbObj = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Team(Team obj)
        {
            ViewBag.TeamList = dbObj.Team.ToList(); // Populate the list here
            return View(obj);
        }


        [HttpPost]

        public ActionResult AddTeam(Team model)
        {
            Team obj = new Team();
            if (ModelState.IsValid)
            {

                obj.ID = model.ID;
                obj.Name = model.Name;

                if (model.ID == 0)
                {
                    dbObj.Team.Add(obj);
                    dbObj.SaveChanges();

                }
                else
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();
                }

                ModelState.Clear();
            }


            return View("Team");
        }

        public ActionResult TeamList()
        {
            var res = dbObj.Team.ToList();
            return View(res);
        }

        private Team GetTeamById(int TeamId)
        {
            return dbObj.Team.FirstOrDefault(a => a.ID == TeamId);
        }


        public ActionResult Delete(int id)
        {
            var res = dbObj.Team.Where(x => x.ID == id).First();
            dbObj.Team.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.Team.ToList();

            return View("TeamList", list);
        }


    }
}

