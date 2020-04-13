using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ISO20K.Models;
using Newtonsoft.Json;

namespace ISO20K.Controllers
{
	public class ci_familyController : Controller
	{
		private ISO20_DBEntities db = new ISO20_DBEntities();

		// GET: ci_family
		public ActionResult Index()
		{			
			List<ci_family> items = new List<ci_family>();
			items = db.ci_family.ToList();
			//嘗試利用ViewBag將model資料丟到前端，html可以讀但是JS檔無法，所以要透過HTML 的data value讓JS讀取，所以要將資料庫資料先轉成JSON格式字串，用ViewBag,data value傳給JS
			ViewBag.items = items;
			//下列JOSN嘗試沒成功
			//string itemJson = JsonConvert.SerializeObject(db.ci_family.ToList());
			//System.Diagnostics.Debug.WriteLine("以下是測試");
			//System.Diagnostics.Debug.WriteLine(itemJson);



			return View(db.ci_family.ToList());


		}

		// GET: ci_family/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ci_family ci_family = db.ci_family.Find(id);
			if (ci_family == null)
			{
				return HttpNotFound();
			}
			return View(ci_family);
		}

		// GET: ci_family/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: ci_family/Create
		// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
		// 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "family_id,name,description,sort_order")] ci_family ci_family)
		{
			if (ModelState.IsValid)
			{
				db.ci_family.Add(ci_family);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(ci_family);
		}

		// GET: ci_family/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ci_family ci_family = db.ci_family.Find(id);
			if (ci_family == null)
			{
				return HttpNotFound();
			}
			return View(ci_family);
		}

		// POST: ci_family/Edit/5
		// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
		// 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "family_id,name,description,sort_order")] ci_family ci_family)
		{
			if (ModelState.IsValid)
			{
				db.Entry(ci_family).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(ci_family);
		}

		// GET: ci_family/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ci_family ci_family = db.ci_family.Find(id);
			if (ci_family == null)
			{
				return HttpNotFound();
			}
			return View(ci_family);
		}

		// POST: ci_family/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			ci_family ci_family = db.ci_family.Find(id);
			db.ci_family.Remove(ci_family);
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
