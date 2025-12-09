using Microsoft.AspNetCore.Mvc;
using GestionProfesores.BD.Data.Entity;
using GestionProfesores.Shared.DTO;
using AutoMapper;
using GestionProfesores.Server.Repositorio;

namespace GestionProfesores.Server.Controllers
{
    [ApiController]
    [Route("api/Alumnos")]
    public class AlumnosController : ControllerBase
    {
        private readonly IAlumnoRepositorio repositorio;
        private readonly IMapper mapper;

        public AlumnosController(IAlumnoRepositorio repositorio,
                                 IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlumnoDTO>>> Get()
        {
            var alumnos = await repositorio.Select();
            return mapper.Map<List<AlumnoDTO>>(alumnos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AlumnoDTO>> Get(int id)
        {
            var alumno = await repositorio.SelectById(id);
            if (alumno == null)
            {
                return NotFound();
            }
            return mapper.Map<AlumnoDTO>(alumno);
        }

        [HttpGet("GetByNombre/{nombre}/{apellido}")]
        public async Task<ActionResult<AlumnoDTO>> GetByNombreCompleto(string nombre, string apellido)
        {
            var alumno = await repositorio.SelectByNombreCompleto(nombre, apellido);
            if (alumno == null)
            {
                return NotFound();
            }
            return mapper.Map<AlumnoDTO>(alumno);
        }

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearAlumnoDTO entidadDTO)
        {
            try
            {
                var alumno = mapper.Map<Alumno>(entidadDTO);
                return await repositorio.Insert(alumno);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Alumno entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos Incorrectos");
            }

            var alumnoExistente = await repositorio.SelectById(id);
            if (alumnoExistente == null)
            {
                return NotFound("No existe el alumno buscado.");
            }

            alumnoExistente.Nombre = entidad.Nombre;
            alumnoExistente.Apellido = entidad.Apellido;

            try
            {
                await repositorio.Update(id, alumnoExistente);
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
                return NotFound($"El alumno con ID {id} no existe.");
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
        public async Task<ActionResult<List<AlumnoDTO>>> GetWithDetails()
        {
            var alumnos = await repositorio.SelectWithDetails();
            return mapper.Map<List<AlumnoDTO>>(alumnos);
        }
    }
}