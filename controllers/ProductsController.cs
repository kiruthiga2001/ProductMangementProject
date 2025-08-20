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
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Product product = db.Products.Include(p => p.Category)
                                         .Include(p => p.ProductAttributeValues.Select(av => av.AttributeValue.Attribute))
                                         .FirstOrDefault(p => p.ProductId == id);
            if (product == null) return HttpNotFound();
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.AttributeValues = new MultiSelectList(db.AttributeValues, "AttributeValueId", "Value");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, int[] selectedAttributeValues)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();

                if (selectedAttributeValues != null)
                {
                    foreach (var attrValueId in selectedAttributeValues)
                    {
                        db.ProductAttributeValues.Add(new ProductAttributeValue
                        {
                            ProductId = product.ProductId,
                            AttributeValueId = attrValueId
                        });
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            ViewBag.AttributeValues = new MultiSelectList(db.AttributeValues, "AttributeValueId", "Value", selectedAttributeValues);
            return View(product);
        }
    }
}
