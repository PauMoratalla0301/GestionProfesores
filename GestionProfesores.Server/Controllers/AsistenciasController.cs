using Microsoft.AspNetCore.Mvc;
using GestionProfesores.BD.Data.Entity;
using GestionProfesores.Shared.DTO;
using AutoMapper;
using GestionProfesores.Server.Repositorio;

namespace GestionProfesores.Server.Controllers
{
    [ApiController]
    [Route("api/Asistencias")]
    public class AsistenciasController : ControllerBase
    {
        private readonly IAsistenciaRepositorio repositorio;
        private readonly IMapper mapper;

        public AsistenciasController(IAsistenciaRepositorio repositorio,
                                     IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AsistenciaDTO>>> Get()
        {
            List<Asistencia> asistencias = await repositorio.Select();
            return Ok(mapper.Map<List<AsistenciaDTO>>(asistencias));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AsistenciaDTO>> Get(int id)
        {
            Asistencia? asistencia = await repositorio.SelectById(id);
            if (asistencia == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<AsistenciaDTO>(asistencia));
        }

        [HttpGet("byAlumno/{alumnoId:int}")]
        public async Task<ActionResult<List<AsistenciaDTO>>> GetByAlumno(int alumnoId)
        {
            List<Asistencia> asistencias = await repositorio.SelectByAlumnoId(alumnoId);
            return Ok(mapper.Map<List<AsistenciaDTO>>(asistencias));
        }

        [HttpGet("byMateria/{materiaId:int}")]
        public async Task<ActionResult<List<AsistenciaDTO>>> GetByMateria(int materiaId)
        {
            List<Asistencia> asistencias = await repositorio.SelectByMateriaId(materiaId);
            return Ok(mapper.Map<List<AsistenciaDTO>>(asistencias));
        }

        [HttpGet("byFecha/{fecha}")]
        public async Task<ActionResult<List<AsistenciaDTO>>> GetByFecha(DateOnly fecha)
        {
            List<Asistencia> asistencias = await repositorio.SelectByFecha(fecha);
            return Ok(mapper.Map<List<AsistenciaDTO>>(asistencias));
        }

        [HttpGet("byAlumnoMateriaFecha/{alumnoId:int}/{materiaId:int}/{fecha}")]
        public async Task<ActionResult<AsistenciaDTO>> GetByAlumnoMateriaFecha(int alumnoId, int materiaId, DateOnly fecha)
        {
            Asistencia? asistencia = await repositorio.SelectByAlumnoMateriaFecha(alumnoId, materiaId, fecha);
            if (asistencia == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<AsistenciaDTO>(asistencia));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearAsistenciaDTO entidadDTO)
        {
            try
            {
                // Verificar si ya existe una asistencia para ese alumno, materia y fecha
                Asistencia? existe = await repositorio.SelectByAlumnoMateriaFecha(
                    entidadDTO.AlumnoId,
                    entidadDTO.MateriaId,
                    entidadDTO.Fecha);

                if (existe != null)
                {
                    return BadRequest("Ya existe un registro de asistencia para este alumno, materia y fecha.");
                }

                Asistencia asistencia = mapper.Map<Asistencia>(entidadDTO);
                int idInsertado = await repositorio.Insert(asistencia);
                return Ok(idInsertado);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Asistencia entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos Incorrectos");
            }

            Asistencia? asistenciaExistente = await repositorio.SelectById(id);
            if (asistenciaExistente == null)
            {
                return NotFound("No existe la asistencia buscada.");
            }

            asistenciaExistente.AlumnoId = entidad.AlumnoId;
            asistenciaExistente.MateriaId = entidad.MateriaId;
            asistenciaExistente.Fecha = entidad.Fecha;
            asistenciaExistente.Presente = entidad.Presente;

            try
            {
                bool actualizado = await repositorio.Update(id, asistenciaExistente);
                if (actualizado)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Error al actualizar la asistencia.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"La asistencia con ID {id} no existe.");
            }

            bool eliminado = await repositorio.Delete(id);
            if (eliminado)
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