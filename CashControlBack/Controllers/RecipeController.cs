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
    public class RecipeController : Controller
    {
        private readonly ApplicationDb dbObj; // Use a more descriptive variable name and make it private

        public RecipeController(ApplicationDb dbContext)
        {
            dbObj = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Recipe(Recipe obj)
        {
            ViewBag.ChefList = dbObj.Chef.ToList(); // Populate the list here

            // If obj.Team_ID has a value, you can set the selected item in the dropdown
            if (obj.Chef_ID > 0)
            {
                ViewBag.SelectedChef = obj.Chef_ID;
                ViewBag.ChefList = dbObj.Chef.ToList();

            }
            else
            {
                ViewBag.SelectedRecipe = null; // Ensure it's initialized even if obj.Team_ID is not set
            }

            return View(obj);
        }



        [HttpPost]
        public ActionResult AddRecipe(Recipe model)
        {
            Recipe obj = new Recipe();
            if (ModelState.IsValid)
            {
                obj.ID = model.ID;
                obj.Title = model.Title;
                obj.Difficulty = model.Difficulty;
                obj.Chef_ID = model.Chef_ID;

                // Get the associated Artikulli object
                Chef associatedChef = GetChefById(model.Chef_ID);
                if (associatedChef != null)
                {
                    obj.AssociatedChef = associatedChef;
                }

                if (model.ID == 0)
                {
                    dbObj.Recipe.Add(obj);
                    dbObj.SaveChanges();
                }
                else
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();
                }

                ModelState.Clear();
            }

            return View("Recipe");
        }

        private Chef GetChefById(int? Chef_ID)
        {
            if (Chef_ID == null)
            {
                return null;
            }

            return dbObj.Chef.SingleOrDefault(a => a.ID == Chef_ID);
        }


        public ActionResult RecipeList(string sortOrder, string difficulty)
        {
            ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.DifficultySortParam = sortOrder == "Difficulty" ? "difficulty_desc" : "Difficulty";
            ViewBag.ChefSortParam = sortOrder == "Chef" ? "Chef_desc" : "Chef";

            var recipes = dbObj.Recipe.Include(p => p.AssociatedChef).AsQueryable();

            // Filter by difficulty if a difficulty is specified
            if (!string.IsNullOrEmpty(difficulty))
            {
                recipes = recipes.Where(p => p.Difficulty == difficulty);
            }

            switch (sortOrder)
            {
                case "title_desc":
                    recipes = recipes.OrderByDescending(p => p.Title);
                    break;
                case "Difficulty":
                    recipes = recipes.OrderBy(p => p.Difficulty);
                    break;
                case "difficulty_desc":
                    recipes = recipes.OrderByDescending(p => p.Difficulty);
                    break;
                case "Chef":
                    recipes = recipes.OrderBy(p => p.AssociatedChef.Name);
                    break;
                case "Chef_desc":
                    recipes = recipes.OrderByDescending(p => p.AssociatedChef.Name);
                    break;
                default:
                    recipes = recipes.OrderBy(p => p.Title);
                    break;
            }

            var res = recipes.ToList();
            return View(res);
        }





        public IActionResult FilterRecipesByChefName(string chefName)
        {
            // Filter recipes by chef name
            var filteredRecipes = dbObj.Recipe
                .Include(r => r.AssociatedChef)
                .Where(r => EF.Functions.Like(r.AssociatedChef.Name, $"%{chefName}%"))
                .ToList();

            return View("RecipeList", filteredRecipes);
        }

        [HttpGet]
        public IActionResult FilterRecipesByDifficulty(string difficulty)
        {
            var recipes = dbObj.Recipe.Include(r => r.AssociatedChef).ToList();

            if (!string.IsNullOrEmpty(difficulty))
            {
                recipes = recipes.Where(r => r.Difficulty.Contains(difficulty)).ToList();
            }

            return View("RecipeList", recipes);
        }



        public ActionResult Delete(int id)
        {
            var res = dbObj.Recipe.Where(x => x.ID == id).First();
            dbObj.Recipe.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.Recipe.ToList();

            return View("RecipeList", list);
        }





    }
}