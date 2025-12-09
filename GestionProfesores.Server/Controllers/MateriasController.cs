using Microsoft.AspNetCore.Mvc;
using GestionProfesores.BD.Data.Entity;
using GestionProfesores.Shared.DTO;
using AutoMapper;
using GestionProfesores.Server.Repositorio;

namespace GestionProfesores.Server.Controllers
{
    [ApiController]
    [Route("api/Materias")]
    public class MateriasController : ControllerBase
    {
        private readonly IMateriaRepositorio repositorio;
        private readonly IMapper mapper;

        public MateriasController(IMateriaRepositorio repositorio,
                                  IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MateriaDTO>>> Get()
        {
            var materias = await repositorio.Select();
            return mapper.Map<List<MateriaDTO>>(materias);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MateriaDTO>> Get(int id)
        {
            var materia = await repositorio.SelectById(id);
            if (materia == null)
            {
                return NotFound();
            }
            return mapper.Map<MateriaDTO>(materia);
        }

        [HttpGet("GetByNombre/{nombre}")]
        public async Task<ActionResult<MateriaDTO>> GetByNombre(string nombre)
        {
            var materia = await repositorio.SelectByNombre(nombre);
            if (materia == null)
            {
                return NotFound();
            }
            return mapper.Map<MateriaDTO>(materia);
        }

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearMateriaDTO entidadDTO)
        {
            try
            {
                // Verificar si ya existe una materia con ese nombre
                var existe = await repositorio.SelectByNombre(entidadDTO.Nombre);
                if (existe != null)
                {
                    return BadRequest("Ya existe una materia con ese nombre.");
                }

                var materia = mapper.Map<Materia>(entidadDTO);
                return await repositorio.Insert(materia);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Materia entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos Incorrectos");
            }

            var materiaExistente = await repositorio.SelectById(id);
            if (materiaExistente == null)
            {
                return NotFound("No existe la materia buscada.");
            }

            materiaExistente.Nombre = entidad.Nombre;

            try
            {
                await repositorio.Update(id, materiaExistente);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"La materia con ID {id} no existe.");
            }

            if (await repositorio.Delete(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("withDetails")]
        public async Task<ActionResult<List<MateriaDTO>>> GetWithDetails()
        {
            var materias = await repositorio.SelectWithDetails();
            return mapper.Map<List<MateriaDTO>>(materias);
        }
    }
}