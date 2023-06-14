using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class ContactController : Controller
    {
        // GET: /Contact
        public ActionResult Index()
        {
            return View(new FeedbackFormModel());
        }

        // POST: /Contact/SubmitForm
        [HttpPost]
        public ActionResult SubmitForm(FeedbackFormModel model)
        {
            if (ModelState.IsValid)
            {
                // Выполните действия по обработке формы обратной связи, например, отправку письма или сохранение в базу данных

                return RedirectToAction("ThankYou");
            }

            // Если модель данных не прошла валидацию, верните представление с ошибками
            return View("Index", model);
        }

        // GET: /Contact/ThankYou
        public ActionResult ThankYou()
        {
            return View();
        }

    }
}