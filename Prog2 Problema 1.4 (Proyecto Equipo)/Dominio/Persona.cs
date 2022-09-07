using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog2_Problema_1._4__Proyecto_Equipo_.Dominio
{
    class Persona
    {
        public int NroPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI { get; set; }
        public DateTime Fecha_nac { get; set; }
        public string Activo { get; set; }

        public Persona(int nro_persona,string nombre, string apellido, int dni, DateTime fecha_nac)
        {
            NroPersona = nro_persona;
            Nombre = nombre;
            Apellido = apellido;
            DNI = dni;
            Fecha_nac = fecha_nac;
            Activo = "";
        }

        public Persona()
        {
            NroPersona = 0;
            Nombre = "";
            Apellido = "";
            DNI = 0;
            Fecha_nac = DateTime.Today;
            Activo = "";
        }

    }
}
