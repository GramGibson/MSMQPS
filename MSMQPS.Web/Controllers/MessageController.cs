﻿namespace MSMQPS.Web.Controllers {
	using System.Web.Mvc;
	using MSMQPS.Web.Lib;
	using MSMQPS.Web.Models;

	public class MessageController : Controller {
		public ActionResult Create() {
			return View(new Email());
		}

		[HttpPost]
		public ActionResult Create(Email email) {
			var messageService = new MessageService(email);
			return RedirectToAction("Create");
		}
	}
}
