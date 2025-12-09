using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProfesores.BD.Data.Entity
{
    public class Alumno : EntityBase
    {
        
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;

        // Relaciones
        public List<Nota> Notas { get; set; } = new();
        public List<Asistencia> Asistencias { get; set; } = new();
    }
}
