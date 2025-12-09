using GestionProfesores.Client.Servicios;
using GestionProfesores.Shared.DTO;
using System.Text.Json;

public class AlumnoServicio : IAlumnoServicio
{
    private readonly IHttpServicio httpServicio;
    private readonly string url = "api/alumnos";

    public AlumnoServicio(IHttpServicio httpServicio)
    {
        this.httpServicio = httpServicio;
    }

    public async Task<HttpRespuesta<List<AlumnoDTO>>> GetAlumnos()
    {
        return await httpServicio.Get<List<AlumnoDTO>>(url);
    }

    public async Task<HttpRespuesta<AlumnoDTO>> GetAlumno(int id)
    {
        return await httpServicio.Get<AlumnoDTO>($"{url}/{id}");
    }

    public async Task<HttpRespuesta<int>> CrearAlumno(CrearAlumnoDTO alumno)
    {
        var response = await httpServicio.Post(url, alumno);
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

    public async Task<HttpRespuesta<object>> ActualizarAlumno(AlumnoDTO alumno)
    {
        return await httpServicio.Put($"{url}/{alumno.Id}", alumno);
    }

    public async Task<HttpRespuesta<object>> EliminarAlumno(int id)
    {
        return await httpServicio.Delete($"{url}/{id}");
    }

    public async Task<HttpRespuesta<AlumnoDTO>> GetAlumnoByNombre(string nombre, string apellido)
    {
        return await httpServicio.Get<AlumnoDTO>($"{url}/GetByNombre/{nombre}/{apellido}");
    }
}
