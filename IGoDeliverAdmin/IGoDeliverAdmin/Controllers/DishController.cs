using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using IGoDeliverAdmin.Entity;
using IGoDeliverAdmin.Models;

namespace IGoDeliverAdmin.Controllers
{
    public class DishController : Controller
    {
        private IGoDeliverEntities db = new IGoDeliverEntities();


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
        [Authorize]
        public ActionResult Create(int id)
        {
            ViewBag.RestaurantId = id;
            return View();
        }

        //
        // POST: /Dish/Create
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dish dish = db.Dishes.Find(id);
            db.Dishes.Remove(dish);
            db.SaveChanges();
            return RedirectToAction("Edit", "Restaurant", new { id = dish.RestaurantId });
        }

        [HttpPost]
        public ActionResult SaveFile(HttpPostedFileBase fileToUpload, int dishId)
        {
            if (fileToUpload != null && fileToUpload.ContentLength > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileToUpload.FileName);

                var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                fileToUpload.SaveAs(path);


                var p = new PictureOfDish() { Path = "/Images/" + fileName };
                db.PictureOfDishes.Add(p);
                db.SaveChanges();
                //var ip = new DishPicture{ DishId = dishId, PictureId = p.Id, IsDefault = false };
                //db.DishPictures.Add(ip);
                var ip = new DishPicture { DishId = dishId, PictureId = p.Id, IsDefault = false };
                db.DishPictures.Add(ip);
                db.SaveChanges();
            }
            return RedirectToAction("ManagePicture", new { id = dishId });
        }


        [HttpGet]
        public ActionResult ManagePicture(int id = 0)
        {
            Dish item = db.Dishes.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpGet]
        public ActionResult DeletePicForItem(int itemid, int picid)
        {
            DishPicture itemPic = db.DishPictures.FirstOrDefault(p => p.PictureId == picid && p.DishId == itemid);
            if (itemPic == null)
            {
                return HttpNotFound();
            }
            db.DishPictures.Remove(itemPic);
            db.SaveChanges();

            return RedirectToAction("ManagePicture", new { id = itemid });
        }

        [HttpGet]
        public ActionResult SetDefaultPic(int itemid, int picid)
        {
            DishPicture itemPic = db.DishPictures.FirstOrDefault(p => p.PictureId == picid && p.DishId == itemid);
            if (itemPic == null)
            {
                return HttpNotFound();
            }
            foreach (var p in db.DishPictures.Where(p => p.DishId == itemid))
            {
                p.IsDefault = false;
            }
            itemPic.IsDefault = true;
            db.SaveChanges();
            return RedirectToAction("ManagePicture", new { id = itemid });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}