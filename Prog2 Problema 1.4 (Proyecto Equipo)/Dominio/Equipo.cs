using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog2_Problema_1._4__Proyecto_Equipo_.Dominio
{
    class Equipo
    {
        public int NroEquipo { get; set; }
        public string Nombre { get; set; }
        public string DirectorTec { get; set; }
        public List<Jugador> Jugadores { get; set; }
        public string Activo { get; set; }


        public Equipo()
        {
            Jugadores = new List<Jugador>();
            NroEquipo = 0;
            Nombre = "";
            DirectorTec = "";
            Activo = "";
        }

        public void AgregarJugador(Jugador jugador)
        {
            Jugadores.Add(jugador);
        }

        public void EliminarJugador(int jugador)
        {
            Jugadores.RemoveAt(jugador);
        }
    }
}
