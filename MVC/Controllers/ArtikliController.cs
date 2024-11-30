
using Microsoft.AspNetCore.Mvc;
using DataAccess;
using Models;
using Microsoft.AspNetCore.Authorization;

namespace MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtikliController : ControllerBase
    {
        private readonly ArtikliRepository _repository;

        public ArtikliController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _repository = new ArtikliRepository(connectionString);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Korisnik")]
        public IActionResult GetAllArtikli()
        {
            var artikli = _repository.GetAllArtikli();
            return Ok(artikli);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Korisnik")]
        public IActionResult GetArtiklById(int id)
        {
            var artikl = _repository.GetArtiklById(id);
            if (artikl == null) return NotFound();
            return Ok(artikl);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddArtikl([FromBody] Artikl artikl)
        {
            _repository.AddArtikl(artikl);
            return CreatedAtAction(nameof(GetArtiklById), new { id = artikl.ID }, artikl);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateArtikl(int id, [FromBody] Artikl artikl)
        {
            artikl.ID = id;
            _repository.UpdateArtikl(artikl);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteArtikl(int id)
        {
            _repository.DeleteArtikl(id);
            return NoContent();
        }
    }
}