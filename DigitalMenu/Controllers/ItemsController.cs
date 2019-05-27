using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DigitalMenu.Models;
using Microsoft.AspNet.Identity;

namespace DigitalMenu.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Items
        public ActionResult Index()
        {
            var memberId = User.Identity.GetUserId();
            ViewBag.CategoryId = new SelectList(db.Categories.Where(i=>i.MemberId==memberId), "Id", "CategoryName");
            var items = db.Items.Where(i=>i.MemberId==memberId).Include(i => i.Category);
            return View(items.ToList());
        }
        [HttpPost]
        public ActionResult Index(string ItemName, int CategoryId)
        {
            var memberId = User.Identity.GetUserId();
            ViewBag.CategoryId = new SelectList(db.Categories.Where(i => i.MemberId == memberId), "Id", "CategoryName");
            var items = db.Items.Where(i => i.MemberId == memberId).Include(i => i.Category);
            if (CategoryId != -1)
                items = db.Items.Include(i => i.Category).Where(i => i.ItemName.Contains(ItemName) && i.CategoryId == CategoryId);
            return View(items.ToList());
        }

      

        // GET: Items/Create
        public ActionResult Create()
        {
            var memberId = User.Identity.GetUserId();
            ViewBag.CategoryId = new SelectList(db.Categories.Where(i => i.MemberId == memberId), "Id", "CategoryName");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Contents,ItemName,SpecialText,Price1,Price2,Rank,Image,CategoryId")] Item item)
        {
            var memberId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
              
                HttpPostedFileBase file = Request.Files["ImageData"];
                item.Image = ConvertToBytes(file);
                item.MemberId = memberId;
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories.Where(i => i.MemberId == memberId), "Id", "CategoryName");
            return View(item);
        }
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            var memberId = User.Identity.GetUserId();
            ViewBag.CategoryId = new SelectList(db.Categories.Where(i => i.MemberId == memberId), "Id", "CategoryName");
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Contents,ItemName,SpecialText,Price1,Price2,Rank,CategoryId")] Item item)
        {
            var memberId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {

                Item item1 = db.Items.Find(item.ID);

                item1.ID = item.ID;
                item1.Title = item.Title;
                item1.Description = item.Description;
                item1.Contents = item.Contents;
                item1.ItemName = item.ItemName;
                item1.SpecialText = item.SpecialText;
                item1.Price1 = item.Price1;
                item1.Price2 = item.Price2;
                item1.Rank = item.Rank;
                item1.CategoryId = item.CategoryId;
                item1.Category = item.Category;
                item1.MemberId= User.Identity.GetUserId(); 
                HttpPostedFileBase file = Request.Files["ImageData"];

                if (!string.IsNullOrEmpty(file.FileName))
                    item1.Image = ConvertToBytes(file);
                //db.Items.Remove(item);
                //db.SaveChanges();


                db.Entry(item1).State = EntityState.Modified;
            
               
             
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories.Where(i => i.MemberId == memberId), "Id", "CategoryName");
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Retrive Image from database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public byte[] GetImageFromDataBase(int Id)
        {
            var q = from temp in db.Items where temp.ID == Id select temp.Image;
            byte[] cover = q.First();
            return cover;
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
