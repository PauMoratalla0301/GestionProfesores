using GestionProfesores.Client.Servicios;
using GestionProfesores.Shared.DTO;
using System.Text.Json;

public class MateriaServicio : IMateriaServicio
{
    private readonly IHttpServicio httpServicio;
    private readonly string url = "api/materias";

    public MateriaServicio(IHttpServicio httpServicio)
    {
        this.httpServicio = httpServicio;
    }

    public async Task<HttpRespuesta<List<MateriaDTO>>> GetMaterias()
    {
        return await httpServicio.Get<List<MateriaDTO>>(url);
    }

    public async Task<HttpRespuesta<MateriaDTO>> GetMateria(int id)
    {
        return await httpServicio.Get<MateriaDTO>($"{url}/{id}");
    }

    public async Task<HttpRespuesta<MateriaDTO>> GetMateriaByNombre(string nombre)
    {
        return await httpServicio.Get<MateriaDTO>($"{url}/GetByNombre/{nombre}");
    }

    public async Task<HttpRespuesta<int>> CrearMateria(CrearMateriaDTO materia)
    {
        var response = await httpServicio.Post(url, materia);
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

    public async Task<HttpRespuesta<object>> ActualizarMateria(MateriaDTO materia)
    {
        return await httpServicio.Put($"{url}/{materia.Id}", materia);
    }

    public async Task<HttpRespuesta<object>> EliminarMateria(int id)
    {
        return await httpServicio.Delete($"{url}/{id}");
    }
}
