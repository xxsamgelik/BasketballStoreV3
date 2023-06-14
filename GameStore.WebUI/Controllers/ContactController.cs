using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Domain.Concrete;
using GameStore.Domain.Entities;
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
        using (var dbContext = new EFDbContext())
        {
            // Создаем объект Feedback и заполняем его данными из модели
            var feedback = new Feedback
            {
                Message = model.Message
            };

            // Добавляем объект Feedback в контекст базы данных
            dbContext.Feedbacks.Add(feedback);

            // Сохраняем изменения в базе данных
            dbContext.SaveChanges();
        }

        return RedirectToAction("ThankYou");
    }

    return View("Index", model);
}



        // GET: /Contact/ThankYou
        public ActionResult ThankYou()
        {
            return View();
        }

    }
}