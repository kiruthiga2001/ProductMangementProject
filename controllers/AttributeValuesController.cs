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
    public class AttributeValuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AttributeValues
        public ActionResult Index()
        {
            var values = db.AttributeValues.Include(v => v.Attribute);
            return View(values.ToList());
        }

        // GET: AttributeValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            AttributeValue value = db.AttributeValues.Include(v => v.Attribute).FirstOrDefault(v => v.AttributeValueId == id);
            if (value == null) return HttpNotFound();
            return View(value);
        }

        // GET: AttributeValues/Create
        public ActionResult Create()
        {
            ViewBag.AttributeId = new SelectList(db.Attributes, "AttributeId", "Name");
            return View();
        }

        // POST: AttributeValues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttributeValueId,Value,AttributeId")] AttributeValue attributeValue)
        {
            if (ModelState.IsValid)
            {
                db.AttributeValues.Add(attributeValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AttributeId = new SelectList(db.Attributes, "AttributeId", "Name", attributeValue.AttributeId);
            return View(attributeValue);
        }

        // GET: AttributeValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            AttributeValue attributeValue = db.AttributeValues.Find(id);
            if (attributeValue == null) return HttpNotFound();
            ViewBag.AttributeId = new SelectList(db.Attributes, "AttributeId", "Name", attributeValue.AttributeId);
            return View(attributeValue);
        }

        // POST: AttributeValues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttributeValueId,Value,AttributeId")] AttributeValue attributeValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attributeValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AttributeId = new SelectList(db.Attributes, "AttributeId", "Name", attributeValue.AttributeId);
            return View(attributeValue);
        }

        // GET: AttributeValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            AttributeValue attributeValue = db.AttributeValues.Include(v => v.Attribute).FirstOrDefault(v => v.AttributeValueId == id);
            if (attributeValue == null) return HttpNotFound();
            return View(attributeValue);
        }

        // POST: AttributeValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AttributeValue attributeValue = db.AttributeValues.Find(id);
            db.AttributeValues.Remove(attributeValue);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

