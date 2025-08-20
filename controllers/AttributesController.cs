using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProductManagement.Models;

namespace ProductManagement.Controllers
{
    public class AttributesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attributes
        public ActionResult Index()
        {
            var attributes = db.Attributes.Include(a => a.Category);
            return View(attributes.ToList());
        }

        // GET: Attributes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Models.Attribute attribute = db.Attributes.Include(a => a.Category).FirstOrDefault(a => a.AttributeId == id);
            if (attribute == null) return HttpNotFound();
            return View(attribute);
        }

        // GET: Attributes/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Attributes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttributeId,Name,CategoryId")] Models.Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                db.Attributes.Add(attribute);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", attribute.CategoryId);
            return View(attribute);
        }

        // GET: Attributes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Models.Attribute attribute = db.Attributes.Find(id);
            if (attribute == null) return HttpNotFound();
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", attribute.CategoryId);
            return View(attribute);
        }

        // POST: Attributes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttributeId,Name,CategoryId")] Models.Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attribute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", attribute.CategoryId);
            return View(attribute);
        }

        // GET: Attributes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Models.Attribute attribute = db.Attributes.Include(a => a.Category).FirstOrDefault(a => a.AttributeId == id);
            if (attribute == null) return HttpNotFound();
            return View(attribute);
        }

        // POST: Attributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.Attribute attribute = db.Attributes.Find(id);
            db.Attributes.Remove(attribute);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
