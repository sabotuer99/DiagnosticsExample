using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiagnosticsExample.Filters
{
    public class ActionLogFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using(var log = new StreamWriter( 
                new FileStream(@"c:\Trace\AttributeLog.log", FileMode.Append)))
            {
                var Controller = filterContext.ActionDescriptor.ControllerDescriptor
                    .ControllerName.PadRight(16).Substring(0,16);
                var Action = filterContext.ActionDescriptor.ActionName
                    .PadRight(24).Substring(0, 24);
                var IP = filterContext.HttpContext.Request.UserHostAddress
                    .PadRight(16).Substring(0, 16);
                var DateTime = filterContext.HttpContext.Timestamp;
                var Principle = "";
                try
                {
                    Principle = filterContext.HttpContext.User.Identity.Name
                        .PadRight(24).Substring(0, 24);
                }
                catch
                { }

                if (Principle.Equals(""))
                    Principle = "Anonymous".PadRight(24).Substring(0, 24);

                log.WriteLine(DateTime + " || " + 
                              IP + " || " + 
                              Controller + " || " + 
                              Action  + " || " + 
                              Principle);
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}