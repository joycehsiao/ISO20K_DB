using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ISO20K.Models;

namespace ISO20K.Controllers
{
    public class ci_severityController : Controller
    {
        private ISO20_DBEntities db = new ISO20_DBEntities();

        // GET: ci_severity
        public ActionResult Index()
        {
            return View(db.ci_severity.ToList());
        }

        // GET: ci_severity/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ci_severity ci_severity = db.ci_severity.Find(id);
            if (ci_severity == null)
            {
                return HttpNotFound();
            }
            return View(ci_severity);
        }

        // GET: ci_severity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ci_severity/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "severity_id,name,description,sort_order")] ci_severity ci_severity)
        {
            if (ModelState.IsValid)
            {
                db.ci_severity.Add(ci_severity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ci_severity);
        }

        // GET: ci_severity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ci_severity ci_severity = db.ci_severity.Find(id);
            if (ci_severity == null)
            {
                return HttpNotFound();
            }
            return View(ci_severity);
        }

        // POST: ci_severity/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "severity_id,name,description,sort_order")] ci_severity ci_severity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ci_severity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ci_severity);
        }

        // GET: ci_severity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ci_severity ci_severity = db.ci_severity.Find(id);
            if (ci_severity == null)
            {
                return HttpNotFound();
            }
            return View(ci_severity);
        }

        // POST: ci_severity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ci_severity ci_severity = db.ci_severity.Find(id);
            db.ci_severity.Remove(ci_severity);
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
    }
}
