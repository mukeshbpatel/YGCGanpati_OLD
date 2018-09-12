using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YGCGanpati.Controllers
{
    public class DownloadController : Controller
    {
        // GET: Download
        public ActionResult App()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Download/YGC_Ganpati.apk"));
            string fileName = "YGC_Ganpati.apk";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}