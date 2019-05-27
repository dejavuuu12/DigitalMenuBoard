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
    public class MenuSetupController : Controller
    {
        private DBContext db = new DBContext();
        // GET: MenuSetup
        public ActionResult Index()
        {
            var memberId = User.Identity.GetUserId();
            ViewBag.CategoryId = new SelectList(db.Categories.Where(i => i.MemberId == memberId), "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult Index(int NoOfItemInRow,int CategoryId, string Template)
        {
            // var skillSetRepository = new SkillSetRepository();
            var memberId = User.Identity.GetUserId();
            ViewBag.NoOfItemInRow = NoOfItemInRow;
            var items = db.Items.Where(i => i.CategoryId == CategoryId && i.MemberId == memberId).Include(i => i.Category).OrderBy(i=>i.Rank);
            if (items.Count()>0) { 
                ViewBag.Title = items.FirstOrDefault().Category.CategoryName.ToString();
            }
            else ViewBag.Title = "No items";
            return View(Template,items.ToList());

          //  return View(Template);
        }

        [HttpPost]
        public ActionResult VerticalMenuAction(int NoOfItemInCol,  string Template)
        {
            // var skillSetRepository = new SkillSetRepository();
            ViewBag.NoOfItemInRow = NoOfItemInCol;
            var memberId = User.Identity.GetUserId();
            ViewBag.Categories = db.Categories.Where(i => i.MemberId == memberId).Select(i=>i.CategoryName).ToList();
            ViewBag.NoOfSlides = Math.Ceiling((decimal)db.Categories.Where(i => i.MemberId == memberId).Select(i => i.CategoryName).ToList().Count / NoOfItemInCol);
           var items = db.Items.Where(i => i.MemberId == memberId).Include(i => i.Category).OrderBy(i => i.Rank);
            
            return View(Template, items.ToList());

            //  return View(Template);
        }
    }
}