using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using IGoDeliverAdmin.Models;

namespace IGoDeliverAdmin.Controllers
{
    public class DishController : Controller
    {
        private IGoDeliverEntities db = new IGoDeliverEntities();

        //
        // GET: /Dish/

        public ActionResult Index()
        {
            var dishes = db.Dishes.Include(d => d.Restaurant);
            return View(dishes.ToList());
        }

        //
        // GET: /Dish/Details/5

        public ActionResult Details(int id = 0)
        {
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        //
        // GET: /Dish/Create/id
        // The id refers to the restaurant id

        public ActionResult Create(int id)
        {
            ViewBag.RestaurantId = id;
            return View();
        }

        //
        // POST: /Dish/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            var dish = new Dish();
            dish.Name = collection.Get("Name");
            dish.Price = Double.Parse(collection.Get("Price"));
            dish.Spiciness = int.Parse(collection.Get("Spiciness"));
            string startTime = collection.Get("StartTime");
            dish.StartTime = startTime.IsEmpty() ? DateTime.Now : DateTime.Parse(startTime);
            string endTime = collection.Get("EndTime");
            dish.EndTime = endTime.IsEmpty() ? DateTime.MaxValue : DateTime.Parse(endTime);
            dish.RestaurantId = int.Parse(collection.Get("RestaurantId"));
            db.Dishes.Add(dish);
            db.SaveChanges();
            return RedirectToAction("Edit", "Restaurant", new { id = dish.RestaurantId });
        }

        //
        // GET: /Dish/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        //
        // POST: /Dish/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            //TODO: 没有做保护，所有parse都可能出问题
            Dish dish = db.Dishes.Find(id);
            dish.Name = collection.Get("Name");
            dish.Price = Double.Parse(collection.Get("Price"));
            dish.Spiciness = int.Parse(collection.Get("Spiciness"));
            string startTime = collection.Get("StartTime");
            dish.StartTime = startTime.IsEmpty() ? DateTime.Now : DateTime.Parse(startTime);
            string endTime = collection.Get("EndTime");
            dish.EndTime = endTime.IsEmpty() ? DateTime.MaxValue : DateTime.Parse(endTime);
            db.SaveChanges();
            return RedirectToAction("Edit", "Restaurant", new { id = dish.RestaurantId });
        }

        //
        // GET: /Dish/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        //
        // POST: /Dish/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dish dish = db.Dishes.Find(id);
            db.Dishes.Remove(dish);
            db.SaveChanges();
            return RedirectToAction("Edit", "Restaurant", new { id = dish.RestaurantId });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}