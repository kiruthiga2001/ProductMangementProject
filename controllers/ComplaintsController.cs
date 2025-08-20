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
    public class ComplaintsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Complaints
        public ActionResult Index()
        {
            var complaints = db.Complaints.Include(c => c.User).Include(c => c.Product);
            return View(complaints.ToList());
        }

        // GET: Complaints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null) return HttpNotFound();
            return View(complaint);
        }

        // GET: Complaints/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FullName");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: Complaints/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                db.Complaints.Add(complaint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FullName", complaint.UserId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", complaint.ProductId);
            return View(complaint);
        }

        // GET: Complaints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null) return HttpNotFound();
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FullName", complaint.UserId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", complaint.ProductId);
            return View(complaint);
        }

        // POST: Complaints/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complaint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FullName", complaint.UserId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", complaint.ProductId);
            return View(complaint);
        }

        // GET: Complaints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null) return HttpNotFound();
            return View(complaint);
        }

        // POST: Complaints/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Complaint complaint = db.Complaints.Find(id);
            db.Complaints.Remove(complaint);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
