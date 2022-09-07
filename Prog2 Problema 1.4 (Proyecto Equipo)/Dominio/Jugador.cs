using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog2_Problema_1._4__Proyecto_Equipo_.Dominio
{
    class Jugador
    {

        public Persona Persona { get; set; }
        public int NumCamiseta { get; set; }
        public int Posicion { get; set; }

        public Jugador(Persona persona, int numCamiseta, int posicion)
        {
            Persona = persona;
            NumCamiseta = numCamiseta;
            Posicion = posicion;
        }

        public Jugador()
        {
            Persona = null;
            NumCamiseta = 0;
            Posicion = 0;
        }
    }
}
