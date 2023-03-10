using ApiPeliculas.Modelos;
using ApiPeliculas.Modelos.Dtos;
using ApiPeliculas.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    [Route("api/peliculas")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaRepositorio _plRepo;
        private readonly IMapper _mapper;
        public PeliculasController(IPeliculaRepositorio plRepo, IMapper mapper)
        {
            _plRepo= plRepo;
            _mapper= mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetPeliculas()
        {
            var peliculas = _plRepo.GetPeliculas();
            var peliculasDto = new List<PeliculaDto>();

            foreach (var item in peliculas)
            {   
                peliculasDto.Add(_mapper.Map<PeliculaDto>(item));
            }

            return Ok(peliculasDto);
        }

        [HttpGet("{peliculaId:int}", Name = "GetPelicula")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPelicula(int peliculaId)
        {
            var pelicula = _plRepo.GetPelicula(peliculaId);
            if (pelicula == null) return NotFound();
            var itemPelicula = _mapper.Map<PeliculaDto>(pelicula);
            return Ok(itemPelicula);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearPelicula([FromBody] PeliculaDto peliculaJson)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (peliculaJson == null) return BadRequest(ModelState);

            if (_plRepo.ExistePelicula(peliculaJson.Nombre))
            {
                ModelState.AddModelError("", $"La pelicula ${peliculaJson.Nombre} ya existe");
                return StatusCode(400, ModelState);
            }

            var pelicula = _mapper.Map<Pelicula>(peliculaJson);
            if (_plRepo.CrearPelicula(pelicula))
            {
                ModelState.AddModelError("", $"Hubo un error al crear {pelicula.Nombre}");
                return StatusCode(400, ModelState);
            }

            return CreatedAtRoute("GetPelicula", new { peliculaId = pelicula.Id }, pelicula);

        }

    }
}
