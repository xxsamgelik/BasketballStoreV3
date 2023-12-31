﻿using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.ApplicationServices;

namespace GameStore.Domain.Concrete
{
    public class EFGameRepository : IGameRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Game> Games
        {
            get { return context.Games; }
        }
        public void SaveGame(Game game)
        {
            if (game.GameId == 0)
                context.Games.Add(game);
            else
            {
                Game dbEntry = context.Games.Find(game.GameId);
                if (dbEntry != null)
                {
                    dbEntry.Name = game.Name;
                    dbEntry.Description = game.Description;
                    dbEntry.Price = game.Price;
                    dbEntry.Category = game.Category;
                    dbEntry.ImageData = game.ImageData;
                    dbEntry.ImageMimeType = game.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
        public IEnumerable<Game> GetGames()
        {

            var data = context.Games.ToList();

            return data;
        }


        public List<Game> GetGamesList()
        {
            var data = context.Games.ToList();

            return data;
        }

        public IEnumerable<Game> GetGames2(string searchName)
        {
            var data = context.Games.Where(g => g.Name.Contains(searchName)).ToList();
            return data;
        }

        public Game DeleteGame(int gameId)
        {
            Game dbEntry = context.Games.Find(gameId);
            if (dbEntry != null)
            {
                context.Games.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
        public IEnumerable<Game> GetGamesByPrice(decimal minPrice, decimal maxPrice)
        {
            return context.Games.Where(g => g.Price >= minPrice && g.Price <= maxPrice);
        }
    }
}
