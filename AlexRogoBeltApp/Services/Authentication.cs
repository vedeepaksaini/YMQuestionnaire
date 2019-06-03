using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlexRogoBeltApp.Services
{
    public class Authentication: ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    var currentUrl = filterContext.HttpContext.Request.Url;
        //    string url = filterContext.HttpContext.Request.Url.ToString();
        //    Uri uri = new Uri(url);
        //    EventItemBLL eventBll = new EventItemBLL();
        //    int id = 0;
        //    string Eventurl = (uri.Segments[2].Replace("/", "")).ToString();
        //    id = eventBll.GetEventId(Eventurl);


        //    if (id > 0)
        //    {
        //        if (filterContext.HttpContext.Session.Contents["eventurl"] == null)
        //        {

        //            filterContext.Result = new RedirectResult("/event/" + Eventurl);
        //            return;
        //        }
        //        else if (filterContext.HttpContext.Session.Contents["eventurl"] != null && filterContext.HttpContext.Session.Contents["eventurl"].ToString() != Eventurl)
        //        {
        //            filterContext.Result = new RedirectResult("/event/" + Eventurl);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        filterContext.HttpContext.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["defaultUrl"].ToString());
        //    }





       // }
    }
}