using ApricodeApplicantWork.Dto;
using ApricodeApplicantWork.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ApricodeApplicantWork.Controllers
{
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IApplicationServices _applicationServices;
        public GameController(IApplicationServices applicationServices)
        {
            _applicationServices = applicationServices ?? throw new ArgumentNullException(nameof(applicationServices));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var list = await _applicationServices.GetAllGames();
            return Ok(list);
        }

        [Route("getByGenreId/{genreId}"), HttpGet]
        public async Task<ActionResult> GetByGenreId(long genreId)
        {
            return Ok(await _applicationServices.GetGameListByGenreId(genreId));
        }

        [Route("{id}"), HttpGet]
        public async Task<ActionResult> Get(long id)
        {
            var game = await _applicationServices.GetGameModel(id);

            if (game == null) return NotFound();
            else return Ok(game);
        }

        [Route("{id}"), HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            var game = await _applicationServices.GetGame(id);

            if (game != null)
            {
                await _applicationServices.DeleteGame(game);
                return Ok();
            }
            else return NotFound();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateGameModel model)
        {
            await _applicationServices.UpdateGame(model);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddGameModel model)
        {
            await _applicationServices.AddGame(model);
            return Ok();
        }
    }
}