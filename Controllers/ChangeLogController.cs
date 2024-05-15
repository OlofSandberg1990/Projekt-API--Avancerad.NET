using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt_API__Avancerad.NET.Services.Interfaces;

namespace Projekt_API__Avancerad.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeLogController : ControllerBase
    {
        private readonly IChangeLog _changeLogRepo;

        public ChangeLogController(IChangeLog changeLogRepo)
        {
            _changeLogRepo = changeLogRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _changeLogRepo.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
