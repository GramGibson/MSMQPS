namespace MSMQPS.Web.Controllers {
	using System.Web.Mvc;

	public class MessageController : Controller {
		public ActionResult Create() {
			return View();
		}

		[HttpPost]
		public ActionResult Create(IEmail email) {
			return RedirectToAction("");
		}
	}
}
