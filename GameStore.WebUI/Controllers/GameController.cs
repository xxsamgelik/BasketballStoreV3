﻿using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class GameController : Controller
    {
        private IGameRepository repository;
        public int pageSize = 4;

        public GameController(IGameRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {

            GamesListViewModel model = new GamesListViewModel
            {
                Games = repository.Games
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(game => game.GameId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null
                        ? repository.Games.Count()
                        : repository.Games.Where(game => game.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int gameId)
        {
            Game game = repository.Games
                .FirstOrDefault(g => g.GameId == gameId);

            if (game != null)
            {
                return File(game.ImageData, game.ImageMimeType);
            }
            else
            {
                return null;
            }
        }



        public ActionResult Search(string searchTerm)
        {
            // Выполняем поиск игр по названию
            var games = repository.GetGames2(searchTerm);

            // Возвращаем представление с результатами поиска
            return View(games);
        }

    }
}

