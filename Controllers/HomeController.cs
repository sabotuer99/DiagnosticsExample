using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DiagnosticsExample.Controllers
{
    public class HomeController : Controller
    {
        TraceSource trace = new TraceSource("myTraceSource");

        public ActionResult Index()
        {
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

        public string Slow()
        {
            Trace.TraceInformation("Started Slow()");
            var start = DateTime.Now;

            int bits = 0;

            for (int i = 0; i < 3; i++)
            {
                string url = "http://espn.go.com/?rand=" + new Random().Next().ToString();
                Trace.WriteLine("Fetching " + url);
                bits += new WebClient().DownloadString(url).Length;
            }

            Trace.TraceInformation("Slow() finished in " + (DateTime.Now - start) + "ms");
            return "yawn, I'm so sleepy... (≚ᄌ≚)ƶƵ  ... " + bits + "b downloaded";
        }

        public string HighCPU()
        {
            Trace.TraceInformation("Started HighCPU()");
            var start = DateTime.Now;

            for (int i = 0; i < 1000000; i++)
            {
                Console.Write(i * i);
            }

            Trace.TraceInformation("HighCPU() finished in " + (DateTime.Now - start) + "ms");
            return "TOO MUCH COFFEE!!! ಠ_ಠ";
        }

        public string ThreadContention()
        {
            Trace.TraceInformation("Started ThreadContention()");
            var start = DateTime.Now;

            Parallel.For(1, 1000000, (i) => Console.WriteLine(i));


            Trace.TraceInformation("ThreadContention() finished in " + (DateTime.Now - start) + "ms");
            return "Can't we all just get along? ¯\\_(ツ)_/¯";
        }

        public string MemoryRelease()
        {
            junk = new LinkedList<string>();
            GC.Collect();

            return "I release you Memory! Be free! (ノ^о^)ノ ";
        }

        LinkedList<string> junk = new LinkedList<string>();
        public string MemorySink()
        {
            Trace.TraceInformation("Started MemorySink()");
            var start = DateTime.Now;

            var sb = new StringBuilder();
            bool go = true;

            for(int i = 0; i < 100; i++)
                sb.Append("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");

            for (var i = 0; i < 10000 && go; i++ )
            {
                try
                {
                    if(i%100 == 0)
                        Trace.WriteLine(i);

                    junk.AddLast(sb.ToString());
                }
                catch
                {
                    go = false;
                }
            }


            Trace.TraceInformation("MemorySink() finished in " + (DateTime.Now - start) + "ms");
            return "Y U HAVE NO MO MEMRY??  ლ(ಠ益ಠლ) " + (junk.Count * junk.First().Length)/1000000 + "Mb";
        }
    }
}