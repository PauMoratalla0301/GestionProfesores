using GestionProfesores.Shared.DTO;
using System.Text.Json;

namespace GestionProfesores.Client.Servicios
{
    public class AsistenciaServicio : IAsistenciaServicio
    {
        private readonly IHttpServicio httpServicio;
        private readonly string url = "api/asistencias";

        public AsistenciaServicio(IHttpServicio httpServicio)
        {
           this.httpServicio = httpServicio;
        }

        public async Task<HttpRespuesta<List<AsistenciaDTO>>> GetAsistencias()
        {
            return await httpServicio.Get<List<AsistenciaDTO>>(url);
        }

        public async Task<HttpRespuesta<AsistenciaDTO>> GetAsistencia(int id)
        {
            return await httpServicio.Get<AsistenciaDTO>($"{url}/{id}");
        }

        public async Task<HttpRespuesta<List<AsistenciaDTO>>> GetAsistenciasByAlumno(int alumnoId)
        {
            return await httpServicio.Get<List<AsistenciaDTO>>($"{url}/byAlumno/{alumnoId}");
        }

        public async Task<HttpRespuesta<List<AsistenciaDTO>>> GetAsistenciasByMateria(int materiaId)
        {
            return await httpServicio.Get<List<AsistenciaDTO>>($"{url}/byMateria/{materiaId}");
        }

        public async Task<HttpRespuesta<List<AsistenciaDTO>>> GetAsistenciasByFecha(DateOnly fecha)
        {
            return await httpServicio.Get<List<AsistenciaDTO>>($"{url}/byFecha/{fecha:yyyy-MM-dd}");
        }

        public async Task<HttpRespuesta<int>> CrearAsistencia(CrearAsistenciaDTO asistencia)
        {
            var response = await httpServicio.Post(url, asistencia);
            if (!response.Error && response.Respuesta != null)
            {
                try
                {
                    var id = JsonSerializer.Deserialize<int>(response.Respuesta.ToString()!);
                    return new HttpRespuesta<int>(id, false, response.HttpResponseMessage);
                }
                catch
                {
                    return new HttpRespuesta<int>(0, true, response.HttpResponseMessage);
                }
            }
            return new HttpRespuesta<int>(0, true, response.HttpResponseMessage);
        }

        public async Task<HttpRespuesta<object>> ActualizarAsistencia(AsistenciaDTO asistencia)
        {
            return await httpServicio.Put($"{url}/{asistencia.Id}", asistencia);
        }

        public async Task<HttpRespuesta<object>> EliminarAsistencia(int id)
        {
            return await httpServicio.Delete($"{url}/{id}");
        }
    }
}

