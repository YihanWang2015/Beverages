using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cis237Assignment6.Models;


namespace cis237Assignment6.Controllers
{
    public class BeverageController : Controller
    {

        [Authorize]
    
        
        private BeverageEntries db = new BeverageEntries();


        // GET: Beverage
        public ActionResult Index()
        {

            DbSet<Beverage> BeverageSearch = db.Beverages;

            string filterName = "";
            string filterPack = "";
            string filterMinPrice = "";
            string filterMaxPrice = "";
            string filteractive = "";


            int minPrice;
            int maxPrice;

            if (Session["name"] != null && !String.IsNullOrWhiteSpace((string)Session["name"]))
            {
                filterName = (string)Session["name"];
            }

            if (Session["pack"] != null && !String.IsNullOrWhiteSpace((string)Session["pack"]))
            {
                filterName = (string)Session["name"];
            }
            //same as above but for min, and we are parsing the string
            if (Session["min"] != null && !String.IsNullOrWhiteSpace((string)Session["min"]))
            {
                filterMinPrice = (string)Session["min"];
                minPrice = Int32.Parse(filterMinPrice);
            }
            //same as above but for max, and we are parsing the string
            if (Session["max"] != null && !String.IsNullOrWhiteSpace((string)Session["max"]))
            {
                filterMaxPrice = (string)Session["max"];
                maxPrice = Int32.Parse(filterMaxPrice);
            }
            if (Session["active"] != null & !String.IsNullOrWhiteSpace((string)Session["active"]))
            {
                filteractive = (string)Session["active"];
            }

            IEnumerable<Beverage> filtered = BeveragessToSearch.Where(Beverage => Beverage.price >= minPrice &&
                                                          Beverage.price <= maxPrice &&
                                                          beverage.name.Contains(filterName));

            //Convert the database set to a list now that the query work is done on it.
            IEnumerable<Beverage> finalFiltered = filtered.ToList();

            //Place the string representation of the values in the session into the
            //ViewBag so that they can be retrived and displayed on the view.
            ViewBag.filterName = filterName;
            ViewBag.filterMin = filterMinPrice;
            ViewBag.filterMax = filterMaxPrice;

            return View(finalFiltered);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,year,make,model,type,horsepower,cylinders")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Beverage.Add(beverage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(beverage);
        }

        // GET: /Beverages/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverage.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // POST: /Beverages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,pack,price,active")] Beverage beverage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beverage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(beverage);
        }

        // GET: /beverage/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.beverage.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // POST: /beverage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Beverage beverage = db.Beverages.Find(id);
            db.Beverages.Remove(beverage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost, ActionName("Filter")]
        [ValidateAntiForgeryToken]
        public ActionResult Filter()
        {
            //Get the form data that was sent out of the Request object.
            //The string that is used as a key to get the data matches the
            //name property of the form control.
            String name = Request.Form.Get("name");
            String pack = Request.Form.Get("pack");
            String minPrice = Request.Form.Get("min");
            String maxPrice = Request.Form.Get("max");
            string active = Request.Form.Get("active");

            //Store the form data into the session so that it can be retrived later
            //on to filter the data.
            Session["name"] = name;
            Session["pack"] = pack;
            Session["min"] = minPrice;
            Session["max"] = maxPrice;
            Session["active"] = active;


            //Redirect the user to the index page. We will do the work of actually
            //fiiltering the list in the index method.
            return RedirectToAction("Index");
        }

    }
}