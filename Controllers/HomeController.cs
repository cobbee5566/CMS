using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//using System.Data;
//using System.Data.Entity;

using CMS.Models;


namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private CMSDatabaseEntities db = new CMSDatabaseEntities();

        // GET: Home
        public ActionResult Index()
        {
            var result = (from article in db.Article
                          orderby article.CreateDate descending
                              select article
                              );

            CMS.ViewModels.IndexViewModel vm = new CMS.ViewModels.IndexViewModel();
            if (result != null)
            {
                vm.Head = result.First();
            }

            vm.MyProperty = 123;

            return View(vm);
        }
    }
}