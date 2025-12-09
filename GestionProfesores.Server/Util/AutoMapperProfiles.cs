using AutoMapper;
using GestionProfesores.BD.Data.Entity;
using GestionProfesores.Shared.DTO;

namespace GestionProfesores.Server.Util
{


public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CrearAlumnoDTO, Alumno>();
            CreateMap<Alumno, AlumnoDTO>();

            CreateMap<CrearAsistenciaDTO, Asistencia>();
            CreateMap<Asistencia, AsistenciaDTO>();

            CreateMap<CrearMateriaDTO, Materia>();
            CreateMap<Materia, MateriaDTO>();

            CreateMap<CrearNotaDTO, Nota>();
            CreateMap<Nota, NotaDTO>();
        }
    }
}
