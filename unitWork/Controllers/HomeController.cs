using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using unitWork.Models;

namespace unitWork.Controllers
{
    public class HomeController : Controller
    {
        static int refreshTimes = 1;
        public ActionResult Index()
        {
            StoresController storeCtrl = new StoresController();
            var store = new Store()
            {
                Name = $"bookstoe{refreshTimes}",
                Desc = "book store"
            };
            var book = new Book()
            {
                Name = $"book{refreshTimes++}",
                Desc = "book"
            };
            storeCtrl.PostStore(store);
            storeCtrl.PostBook(book);
            storeCtrl.GetStores();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}