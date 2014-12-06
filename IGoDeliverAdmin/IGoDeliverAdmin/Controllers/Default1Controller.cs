using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IGoDeliverAdmin.Models;

namespace IGoDeliverAdmin.Controllers
{
    public class Default1Controller : Controller
    {
        private IGoDeliverEntities db = new IGoDeliverEntities();

        //
        // GET: /Default1/

        public ActionResult Index()
        {
            var dishes = db.Dishes.Include(d => d.Restaurant);
            return View(dishes.ToList());
        }

        //
        // GET: /Default1/Details/5

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
        // GET: /Default1/Create

        public ActionResult Create()
        {
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "RestaurantName");
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dish dish)
        {
            if (ModelState.IsValid)
            {
                db.Dishes.Add(dish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "RestaurantName", dish.RestaurantId);
            return View(dish);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "RestaurantName", dish.RestaurantId);
            return View(dish);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dish dish)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dish).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "RestaurantName", dish.RestaurantId);
            return View(dish);
        }

        //
        // GET: /Default1/Delete/5

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
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dish dish = db.Dishes.Find(id);
            db.Dishes.Remove(dish);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}