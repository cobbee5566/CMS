using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS.Models;

using PagedList;
using System.IO;
using Microsoft.AspNet.Identity;

namespace CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private CMSDatabaseEntities db = new CMSDatabaseEntities();

        // GET: Admin/Articles
        public ActionResult Index(string q, int page = 1, int pageSize = 3)
        {
            var model = db.Article.AsQueryable();
            if (string.IsNullOrWhiteSpace(q))
            {
                ViewData.Model = model.OrderByDescending(d => d.CreateDate).ToPagedList(page,pageSize);
            }
            else
            {
                ViewData.Model = model.Where(d => d.Subject.Contains(q) || d.Summary.Contains(q)).OrderByDescending(d => d.CreateDate).ToPagedList(page,pageSize);
            }

            return View();
        }

        // GET: Admin/Articles/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Admin/Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Articles/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Subject,Summary,ContentText,IsPublich,PublishDate,ViewCount")] Article article, HttpPostedFileBase image)
        {
            string ImageFileName = SaveImageAndGetFileName(image);

            if (ModelState.IsValid)
            {
                article.ID = Guid.NewGuid();
                article.CreateDate = DateTime.Now;
                article.UpdateDate = DateTime.Now;
                article.CreateUser = new Guid(User.Identity.GetUserId());
                article.UpdateUser = Guid.Empty;
                article.Image = ImageFileName;
                db.Article.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        /// <summary>
        /// 儲存圖片檔案
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private string SaveImageAndGetFileName(HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    //var fileName = Path.GetFileName(file.FileName);
                    double timeStamp = DateTime.UtcNow.AddHours(8).Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    var fileName = Convert.ToInt32(timeStamp).ToString() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/FileUploads"), fileName);
                    file.SaveAs(path);
                    return fileName;
                }
            }

            return "";
        }

        // GET: Admin/Articles/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Admin/Articles/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Subject,Summary,ContentText,IsPublich,PublishDate,ViewCount,CreateUser,CreateDate,UpdateUser,UpdateDate")] Article article)
        public ActionResult Edit([Bind(Include = "ID,Subject,Summary,ContentText,IsPublich,PublishDate,ViewCount", Exclude = "Image,CreateDate,CreateUser,UpdateDate,UpdateUser")] Article article, HttpPostedFileBase image)
        {
            string ImageFileName = "";
            if (image != null)
            {
                ImageFileName = SaveImageAndGetFileName(image);
            }

            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.Entry(article).Property(x => x.CreateUser).IsModified = false;
                db.Entry(article).Property(x => x.CreateDate).IsModified = false;
                article.UpdateUser = new Guid(User.Identity.GetUserId());
                article.UpdateDate = DateTime.Now;

                if (!string.IsNullOrEmpty(ImageFileName))
                {
                    article.Image = ImageFileName;
                }
                else
                {
                    db.Entry(article).Property(x => x.Image).IsModified = false;
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Admin/Articles/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Admin/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Article article = db.Article.Find(id);
            db.Article.Remove(article);
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
