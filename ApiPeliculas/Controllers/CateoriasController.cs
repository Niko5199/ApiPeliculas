using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiPeliculas.Repositorio.IRepositorio;
using AutoMapper;
using ApiPeliculas.Modelos.Dtos;
using ApiPeliculas.Modelos;

namespace ApiPeliculas.Controllers
{
    // [Route("api/[controller]")] esta es una opcion esta asociada a la clase
    [ApiController]
    [Route("api/categorias")]
    public class CateoriasController : ControllerBase
    {
        private readonly ICategoriaRepositorio _ctRepo;
        private readonly IMapper _mapper;

        public CateoriasController(ICategoriaRepositorio ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategorias()
        {
            var listaCategorias = _ctRepo.GetCategorias();
            var listaCategoriasDto = new List<CategoriaDto>();

            foreach (var item in listaCategorias)
            {
                listaCategoriasDto.Add(_mapper.Map<CategoriaDto>(item));
            }

            return Ok(listaCategoriasDto);
        }

        [HttpGet("{categoriaId:int}",Name = "GetCategoria")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoria(int categoriaId)
        {
            var categoria = _ctRepo.GetCategoria(categoriaId);
            if(categoria == null) return NotFound();
            var itemCategoriaDto = _mapper.Map<CategoriaDto>(categoria); 
            return Ok(itemCategoriaDto);
        }

        [HttpPost]
        [ProducesResponseType(201,Type = typeof(CategoriaDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearCategoria([FromBody] CrearCategoriaDto crearCategoriaDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if(crearCategoriaDto == null) return BadRequest(ModelState);
                    
            if (_ctRepo.ExisteCategoria(crearCategoriaDto.Nombre))
            {
                ModelState.AddModelError("", "La categoria ya existe");
                return StatusCode(404, ModelState);
            }

            var categoria = _mapper.Map<Categoria>(crearCategoriaDto);
            if(!_ctRepo.CrearCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo saluo mal guardando el registro {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.Id },categoria);
        }

        [HttpPatch("{categoriaId:int}",Name = "ActtualizarPatchCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActualizarPatchCategoria(int categoriaId, Categoria categoriaDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (categoriaDto == null || categoriaId != categoriaDto.Id) return BadRequest(ModelState);

            var categoria = _mapper.Map<Categoria> (categoriaDto);

            if (!_ctRepo.ActualizarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo saluo mal al actualizar el registro {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{categoriaId:int}", Name = "BorrarCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BorrarCategoria(int categoriaId)
        {
            if(!_ctRepo.ExisteCategoria(categoriaId)) return NotFound();

            var categoria = _ctRepo.GetCategoria(categoriaId);

            if (!_ctRepo.BorrarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro ${categoria.Nombre}");
            }

            return NoContent();
        }
    }
}