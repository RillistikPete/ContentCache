using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using System.Diagnostics;
using System.Threading;
using ContentCache.Infrastructure;

namespace ContentCache.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(CacheProfile = "cp1")]
        public ActionResult Index()
        {
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(30));
            Response.Cache.SetCacheability(HttpCacheability.Server);
            Response.Cache.AddValidationCallback(CheckCachedItem, Request.UserAgent);


            Thread.Sleep(1000);
            int counterValue = AppStateHelper.IncrementAndGet(AppStateKeys.INDEX_COUNTER);
            Debug.WriteLine(String.Format("INDEX_COUNTER: {0}", counterValue));
            return View(counterValue);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public PartialViewResult GetTime()
        {
            return PartialView((object)DateTime.Now.ToShortTimeString());
        }

        public void CheckCachedItem(HttpContext context, object data, ref HttpValidationStatus status)
        {
            status = data.ToString() == context.Request.UserAgent ? HttpValidationStatus.Valid : HttpValidationStatus.Invalid;
            Debug.WriteLine("Cache status: " + status);
        }
    }
}