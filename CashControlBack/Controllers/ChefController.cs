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
    public class ChefController : Controller
    {
        private readonly ApplicationDb dbObj; // Use a more descriptive variable name and make it private

        public ChefController(ApplicationDb dbContext)
        {
            dbObj = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Chef(Chef obj)
        {
            ViewBag.ChefList = dbObj.Chef.ToList(); // Populate the list here
            return View(obj);
        }


        [HttpPost]

        public ActionResult AddChef(Chef model)
        {
            Chef obj = new Chef();
            if (ModelState.IsValid)
            {

                obj.ID = model.ID;
                obj.Name = model.Name;
                obj.BirthYear = model.BirthYear;

                if (model.ID == 0)
                {
                    dbObj.Chef.Add(obj);
                    dbObj.SaveChanges();

                }
                else
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();
                }

                ModelState.Clear();
            }


            return View("Chef");
        }

        public ActionResult ChefList()
        {
            var res = dbObj.Chef.ToList();
            return View(res);
        }

        private Chef GetChefById(int ChefId)
        {
            return dbObj.Chef.FirstOrDefault(a => a.ID == ChefId);
        }


        public ActionResult Delete(int id)
        {
            var res = dbObj.Chef.Where(x => x.ID == id).First();
            dbObj.Chef.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.Chef.ToList();

            return View("ChefList", list);
        }


    }
}

