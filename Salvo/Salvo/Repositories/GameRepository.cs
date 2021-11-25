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

        public Game FindById(long id)
        {
            return FindByCondition(game => game.Id == id) //del game el Id debe ser igual al id que pasamos por parametro
                .Include(game => game.GamePlayers) //del game que incluya los gameplayers
                    .ThenInclude(gp => gp.Player) //de nuestro gameplayer de nuestro game entonces incluye al player
                .FirstOrDefault(); //de todo esto retorname el primero o el por defecto
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
