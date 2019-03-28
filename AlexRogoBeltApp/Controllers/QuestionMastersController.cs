using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlexRogoBeltApp.Entities;

namespace AlexRogoBeltApp.Controllers
{
    public class QuestionMastersController : Controller
    {
        private TocicoEntities db = new TocicoEntities();

        // GET: QuestionMasters
        public ActionResult Index()
        {
            var questionMasters = db.QuestionMasters.Include(q => q.LevelMaster);
            return View(questionMasters.ToList());
        }

        // GET: QuestionMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionMaster questionMaster = db.QuestionMasters.Find(id);
            if (questionMaster == null)
            {
                return HttpNotFound();
            }
            return View(questionMaster);
        }

        // GET: QuestionMasters/Create
        public ActionResult Create()
        {
            ViewBag.LevelID = new SelectList(db.LevelMasters, "ID", "LevelName");
            return View();
        }

        // POST: QuestionMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Introduction,Question,Deactive,QuestionOrder,LevelID,SelectionType")] QuestionMaster questionMaster)
        {
            if (ModelState.IsValid)
            {
                db.QuestionMasters.Add(questionMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LevelID = new SelectList(db.LevelMasters, "ID", "LevelName", questionMaster.LevelID);
            return View(questionMaster);
        }

        // GET: QuestionMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionMaster questionMaster = db.QuestionMasters.Find(id);
            if (questionMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.LevelID = new SelectList(db.LevelMasters, "ID", "LevelName", questionMaster.LevelID);
            return View(questionMaster);
        }

        // POST: QuestionMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Introduction,Question,Deactive,QuestionOrder,LevelID,SelectionType")] QuestionMaster questionMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LevelID = new SelectList(db.LevelMasters, "ID", "LevelName", questionMaster.LevelID);
            return View(questionMaster);
        }

        // GET: QuestionMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionMaster questionMaster = db.QuestionMasters.Find(id);
            if (questionMaster == null)
            {
                return HttpNotFound();
            }
            return View(questionMaster);
        }

        // POST: QuestionMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionMaster questionMaster = db.QuestionMasters.Find(id);
            db.QuestionMasters.Remove(questionMaster);
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
