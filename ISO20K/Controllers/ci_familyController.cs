using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
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
			//利用ViewBag將model資料丟到前端，html可以讀但是JS檔無法，所以要透過HTML 的data value讓JS讀取，所以要將資料庫資料先轉成JSON格式字串，用ViewBag,data value傳給JS
			ViewBag.items = items;
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("{\"nodes\":[");
			
			foreach (ISO20K.Models.ci_family i in items) {
				sb.AppendLine("{\"id\":"+ i.family_id + ",\"name\":"+"\""+i.name+"\",");
				sb.AppendLine("\"label\":\""+i.description+"\"},");
			}
			
			string s = sb.ToString();
			s =s.Substring(0,s.Length - 2);
			s = s.Remove(s.Length - 1, 1);
			sb = new StringBuilder(s);
			sb.AppendLine("]}");
			string jsonItem = sb.ToString();
			
			ViewBag.sb = jsonItem;

			List<SelectListItem> source_items = new List<SelectListItem>();
			foreach (var i in items) {
				source_items.Add(new SelectListItem()
				{
					Text = i.name,
					Value = i.family_id.ToString()
				});
			}
			
			ViewBag.sourceItem = source_items;
			ViewBag.targetItem = source_items;
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
