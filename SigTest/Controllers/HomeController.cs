using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SigTest.Controllers {
	public class HomeController : Controller {
		public ActionResult Index() {
			return View();
		}

		public ActionResult About() {
			string pInfo = $"Es ist {DateTime.Now}";
			TestHub.InformClients(pInfo);
			ViewBag.Message = "Information sent to clients";
			return View();
		}

		public ActionResult Contact() {
			ViewBag.Message = "Your contact page.";
			return View();
		}
	}
}