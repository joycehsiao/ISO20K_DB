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
    public class ciController : Controller
    {
        private ISO20_DBEntities db = new ISO20_DBEntities();

        // GET: cis
        public ActionResult Index()
        {
            var ci = db.ci.Include(c => c.ci_family).Include(c => c.ci_severity);
            return View(ci.ToList());
        }

        // GET: cis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ci ci = db.ci.Find(id);
            if (ci == null)
            {
                return HttpNotFound();
            }
            return View(ci);
        }

        // GET: cis/Create
        public ActionResult Create()
        {
            ViewBag.family_id = new SelectList(db.ci_family, "family_id", "name");
            ViewBag.severity_id = new SelectList(db.ci_severity, "severity_id", "name");
            return View();
        }

        // POST: cis/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ci_id,name,parent_id,family_id,severity_id,org_id,site_id,description")] ci ci)
        {
            if (ModelState.IsValid)
            {
                db.ci.Add(ci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.family_id = new SelectList(db.ci_family, "family_id", "name", ci.family_id);
            ViewBag.severity_id = new SelectList(db.ci_severity, "severity_id", "name", ci.severity_id);
            return View(ci);
        }

        // GET: cis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ci ci = db.ci.Find(id);
            if (ci == null)
            {
                return HttpNotFound();
            }
            ViewBag.family_id = new SelectList(db.ci_family, "family_id", "name", ci.family_id);
            ViewBag.severity_id = new SelectList(db.ci_severity, "severity_id", "name", ci.severity_id);
            return View(ci);
        }

        // POST: cis/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ci_id,name,parent_id,family_id,severity_id,org_id,site_id,description")] ci ci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.family_id = new SelectList(db.ci_family, "family_id", "name", ci.family_id);
            ViewBag.severity_id = new SelectList(db.ci_severity, "severity_id", "name", ci.severity_id);
            return View(ci);
        }

        // GET: cis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ci ci = db.ci.Find(id);
            if (ci == null)
            {
                return HttpNotFound();
            }
            return View(ci);
        }

        // POST: cis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ci ci = db.ci.Find(id);
            db.ci.Remove(ci);
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
