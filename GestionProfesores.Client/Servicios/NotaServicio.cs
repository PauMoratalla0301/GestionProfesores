using GestionProfesores.Client.Servicios;
using GestionProfesores.Shared.DTO;
using System.Text.Json;

public class NotaServicio : INotaServicio
{
    private readonly IHttpServicio httpServicio;
    private readonly string url = "api/notas";

    public NotaServicio(IHttpServicio httpServicio)
    {
        this.httpServicio = httpServicio;
    }

    public async Task<HttpRespuesta<List<NotaDTO>>> GetNotas()
    {
        return await httpServicio.Get<List<NotaDTO>>(url);
    }

    public async Task<HttpRespuesta<NotaDTO>> GetNota(int id)
    {
        return await httpServicio.Get<NotaDTO>($"{url}/{id}");
    }

    public async Task<HttpRespuesta<List<NotaDTO>>> GetNotasByAlumno(int alumnoId)
    {
        return await httpServicio.Get<List<NotaDTO>>($"{url}/byAlumno/{alumnoId}");
    }

    public async Task<HttpRespuesta<List<NotaDTO>>> GetNotasByMateria(int materiaId)
    {
        return await httpServicio.Get<List<NotaDTO>>($"{url}/byMateria/{materiaId}");
    }

    public async Task<HttpRespuesta<List<NotaDTO>>> GetNotasByAlumnoMateria(int alumnoId, int materiaId)
    {
        return await httpServicio.Get<List<NotaDTO>>($"{url}/byAlumnoMateria/{alumnoId}/{materiaId}");
    }

    public async Task<HttpRespuesta<decimal?>> GetPromedioAlumnoMateria(int alumnoId, int materiaId)
    {
        return await httpServicio.Get<decimal?>($"{url}/promedio/{alumnoId}/{materiaId}");
    }

    public async Task<HttpRespuesta<int>> CrearNota(CrearNotaDTO nota)
    {
        var response = await httpServicio.Post(url, nota);
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

    public async Task<HttpRespuesta<object>> ActualizarNota(NotaDTO nota)
    {
        return await httpServicio.Put($"{url}/{nota.Id}", nota);
    }

    public async Task<HttpRespuesta<object>> EliminarNota(int id)
    {
        return await httpServicio.Delete($"{url}/{id}");
    }
}
