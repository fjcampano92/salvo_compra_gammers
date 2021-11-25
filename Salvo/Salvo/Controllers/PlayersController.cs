using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Salvo.Models;
using Salvo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Salvo.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private IPlayerRepository _repository;
        public PlayersController(IPlayerRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PlayerDTO player)
        {
            try
            {
                bool isMatchEmail = Regex.IsMatch(player.Email, @"([\w\.?\-?]+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})");
                bool isMatchPass = Regex.IsMatch(player.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$");
                
                //verificar email y password no vacios
                if (String.IsNullOrEmpty(player.Email) || String.IsNullOrEmpty(player.Password))
                    return StatusCode(403, "error datos incompletos");

                //verificar el formato del email
                if (isMatchEmail == false)
                    return StatusCode(403, "email con formato erroneo");

                //verificar que la contraseña cumpla con los requisitos
                if (isMatchPass == false)
                    return StatusCode(403, "contraseña no cumple con los requisitos");

                //verificamos si jugador existe mediante el mail
                Player dbPlayer = _repository.FindByEmail(player.Email);                
                if (dbPlayer != null)
                    return StatusCode(403, "Email esta en uso");

                //creacion de una nueva entidad player mediante playerDTO
                Player newPlayer = new Player
                {
                    Email = player.Email,
                    Password = player.Password
                };

                _repository.Save(newPlayer);
                //retornamos el nuevo jugador
                return StatusCode(201, newPlayer);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
