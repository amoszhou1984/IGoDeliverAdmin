using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IGoDeliverAdmin.Models;

namespace IGoDeliverAdmin.Controllers
{

    public class RestaurantController : Controller
    {
        private IGoDeliverEntities iGoDeliverEntities = new IGoDeliverEntities();
        //
        // GET: /Restaurant/

        public ActionResult Index()
        {
            var model =
                iGoDeliverEntities.Restaurants.OrderByDescending(r => r.RestaurantName).AsEnumerable();
            var toReturn = new List<RestaurantModel>();
            foreach (var r in model)
            {
                var r2 = r;
                toReturn.Add(new RestaurantModel
                {
                    Id = r.Id,
                    Name = r.RestaurantName,
                    ContactName = r.ContactName,
                    ContactPhone = r.ContactPhone,
                    Descriptions = r.Description,
                    //UnitNumber = r.Geolocation.UnitNo,
                    //StreetNumber = r.Geolocation.StreetNo,
                    //Street = r.Geolocation.Street,
                    //Suburb = r.Geolocation.Suburb.Name,
                    //StateProvince = r.Geolocation.Suburb.StateProvince1.Name,
                    Dishes = r.Dishes
                });
            }
            return View(toReturn);
        }

        //
        // GET: /Restaurant/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Restaurant/Create
        [Authorize]
        public ActionResult Create()
        {

            ViewBag.SuburbIds = new SelectList(iGoDeliverEntities.Suburbs, "Id", "Name");
            return View();
        }

        //
        // POST: /Restaurant/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Restaurant/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            RestaurantModel restaurantModel = null;

            var restaurant = iGoDeliverEntities.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            restaurantModel = new RestaurantModel
            {
                Id = restaurant.Id,
                Name = restaurant.RestaurantName,
                ContactName = restaurant.ContactName,
                ContactPhone = restaurant.ContactPhone,
                UnitNumber = restaurant.Geolocation.UnitNo,
                StreetNumber = restaurant.Geolocation.StreetNo,
                Street = restaurant.Geolocation.Street,
                Suburb = restaurant.Geolocation.Suburb.Name,
                Dishes = restaurant.Dishes
            };

            return View(restaurantModel);
        }

        //
        // POST: /Restaurant/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                //Save new Geolocation First

                var restaurant = iGoDeliverEntities.Restaurants.Find(id);

                if (restaurant == null)
                {
                    return HttpNotFound();
                }
                Geolocation newGeolocation = restaurant.Geolocation;
                newGeolocation.UnitNo = collection.Get("UnitNumber");
                newGeolocation.StreetNo = int.Parse(collection.Get("StreetNumber"));
                newGeolocation.Street = collection.Get("Street");
                restaurant.RestaurantName = collection.Get("Name");
                restaurant.ContactName = collection.Get("ContactName");
                restaurant.ContactPhone = collection.Get("ContactPhone");
                restaurant.Description = collection.Get("Descriptions");
                iGoDeliverEntities.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Restaurant/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            RestaurantModel restaurantModel = null;

            var restaurant = iGoDeliverEntities.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            restaurantModel = new RestaurantModel
            {
                Id = restaurant.Id,
                Name = restaurant.RestaurantName,
                ContactName = restaurant.ContactName,
                ContactPhone = restaurant.ContactPhone,
                Dishes = restaurant.Dishes
            };

            return View(restaurantModel);
        }

        //
        // POST: /Restaurant/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Restaurant restaurant = iGoDeliverEntities.Restaurants.Find(id);
            iGoDeliverEntities.Restaurants.Remove(restaurant);
            iGoDeliverEntities.SaveChanges();
            //还需删除菜品、订单
            return RedirectToAction("Index");

        }
    }
}
