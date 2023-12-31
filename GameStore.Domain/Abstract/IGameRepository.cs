﻿using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Abstract
{
    public interface IGameRepository
    {
        IEnumerable<Game> Games { get; }
        void SaveGame(Game game);
        Game DeleteGame(int gameId);
        IEnumerable<Game> GetGames();
        List<Game> GetGamesList();
        IEnumerable<Game> GetGames2(string searchName);
        IEnumerable<Game> GetGamesByPrice(decimal minPrice, decimal maxPrice);




    }
}
