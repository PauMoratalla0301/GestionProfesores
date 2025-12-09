using Microsoft.AspNetCore.Mvc;
using GestionProfesores.BD.Data.Entity;
using GestionProfesores.Shared.DTO;
using AutoMapper;
using GestionProfesores.Server.Repositorio;

namespace GestionProfesores.Server.Controllers
{
    [ApiController]
    [Route("api/Notas")]
    public class NotasController : ControllerBase
    {
        private readonly INotaRepositorio repositorio;
        private readonly IMapper mapper;

        public NotasController(INotaRepositorio repositorio,
                               IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<NotaDTO>>> Get()
        {
            var notas = await repositorio.Select();
            return mapper.Map<List<NotaDTO>>(notas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<NotaDTO>> Get(int id)
        {
            var nota = await repositorio.SelectById(id);
            if (nota == null)
            {
                return NotFound();
            }
            return mapper.Map<NotaDTO>(nota);
        }

        [HttpGet("byAlumno/{alumnoId:int}")]
        public async Task<ActionResult<List<NotaDTO>>> GetByAlumno(int alumnoId)
        {
            var notas = await repositorio.SelectByAlumnoId(alumnoId);
            return mapper.Map<List<NotaDTO>>(notas);
        }

        [HttpGet("byMateria/{materiaId:int}")]
        public async Task<ActionResult<List<NotaDTO>>> GetByMateria(int materiaId)
        {
            var notas = await repositorio.SelectByMateriaId(materiaId);
            return mapper.Map<List<NotaDTO>>(notas);
        }

        [HttpGet("byAlumnoMateria/{alumnoId:int}/{materiaId:int}")]
        public async Task<ActionResult<List<NotaDTO>>> GetByAlumnoMateria(int alumnoId, int materiaId)
        {
            var notas = await repositorio.SelectByAlumnoMateria(alumnoId, materiaId);
            return mapper.Map<List<NotaDTO>>(notas);
        }

        [HttpGet("promedio/{alumnoId:int}/{materiaId:int}")]
        public async Task<ActionResult<decimal?>> GetPromedio(int alumnoId, int materiaId)
        {
            var promedio = await repositorio.GetPromedioByAlumnoMateria(alumnoId, materiaId);
            return promedio;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearNotaDTO entidadDTO)
        {
            try
            {
                var nota = mapper.Map<Nota>(entidadDTO);
                return await repositorio.Insert(nota);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Nota entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos Incorrectos");
            }

            var notaExistente = await repositorio.SelectById(id);
            if (notaExistente == null)
            {
                return NotFound("No existe la nota buscada.");
            }

            notaExistente.AlumnoId = entidad.AlumnoId;
            notaExistente.MateriaId = entidad.MateriaId;
            notaExistente.Valor = entidad.Valor;
            notaExistente.Fecha = entidad.Fecha;

            try
            {
                await repositorio.Update(id, notaExistente);
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
                return NotFound($"La nota con ID {id} no existe.");
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
    }
}