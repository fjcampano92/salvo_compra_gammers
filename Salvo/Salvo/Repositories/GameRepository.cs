using Microsoft.EntityFrameworkCore;
using Salvo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salvo.Repositories
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(SalvoContext repositoryContext) : base(repositoryContext)
        {

        }
        public IEnumerable<Game> GetAllGames()
        {
            return FindAll() //retornamos todos los elementos de forma ordenada mediante su fecha de creacion en forma de lista.
                .OrderBy(game => game.CreationDate)
                .ToList();
        }

        public IEnumerable<Game> GetAllGamesWhitPlayers()
        {
            return FindAll(source => source.Include(game => game.GamePlayers)
                    .ThenInclude(gameplayer => gameplayer.Player)
                        .ThenInclude(player => player.Scores))
                .OrderBy(game => game.CreationDate)
                .ToList();
        }
    }
}
