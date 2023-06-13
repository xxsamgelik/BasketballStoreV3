using GameStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class MyAccountController : Controller
    {
        private UserContext _dbContext; 
        // GET: MyAccount
        public MyAccountController()
        {
            _dbContext = new UserContext();
        }

        public ActionResult UserDetails()
        {
            string userName = User.Identity.Name;

            User user = _dbContext.Users.FirstOrDefault(u => u.Email == userName);

            try
            {

                // Используйте userName для поиска пользователя в базе данных

                if (user != null)
                {
                    // Другие действия с данными пользователя...
                    return View(user);

                }

            }
            catch (Exception ex)
            {
                // Обработка случая, если пользователь не найден...

            }
            return View("Error");
        }


    }
}