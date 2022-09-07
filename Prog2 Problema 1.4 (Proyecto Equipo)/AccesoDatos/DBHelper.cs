using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Prog2_Problema_1._4__Proyecto_Equipo_.Dominio;

namespace Prog2_Problema_1._4__Proyecto_Equipo_.AccesoDatos
{
    class DBHelper
    {
        SqlConnection Conexion = new SqlConnection(@"Data Source=LAPTOP-13H7495I\SQLEXPRESS;Initial Catalog=Prog2_Equipo_ej14;Integrated Security=True");
        SqlCommand Comando = new SqlCommand();

        public DataTable ConsultarDB(string nombreSP)
        {
            Conexion.Open();
            Comando.Connection = Conexion;
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.CommandText = nombreSP;
            DataTable Tabla = new DataTable();
            Tabla.Load(Comando.ExecuteReader());
            Conexion.Close();

            return Tabla;
        }

        public int MostrarID()
        {
            Conexion.Open();
            Comando.Connection = Conexion;
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.CommandText = "SP_CONSEGUIR_ID";
            SqlParameter Parametro = new SqlParameter("@next", SqlDbType.Int);
            Parametro.Direction = ParameterDirection.Output;
            Comando.Parameters.Add(Parametro);
            Comando.ExecuteNonQuery();
            int ID = Convert.ToInt32(Parametro.Value);
            Conexion.Close();

            return ID;
        }

        public bool InsertarEquipo(Equipo equipo)
        {
            SqlTransaction Transaccion = null;
            bool confirmar = true;

            try
            {
                //Insertar Maestro (Equipo)
                Conexion.Open();
                Transaccion = Conexion.BeginTransaction();
                Comando.Connection = Conexion;
                Comando.Transaction = Transaccion;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "SP_INSERTAR_MAESTRO";
                Comando.Parameters.Clear();
                Comando.Parameters.AddWithValue("@nombre", equipo.Nombre);
                Comando.Parameters.AddWithValue("@director_tec", equipo.DirectorTec);
                
                SqlParameter NroEquipo = new SqlParameter("@nro_equipo", SqlDbType.Int);
                NroEquipo.Direction = ParameterDirection.Output;
                Comando.Parameters.Add(NroEquipo);

                Comando.Parameters.AddWithValue("@activo", "si");
                Comando.ExecuteNonQuery();
                
                //Insertar Detalle (Jugadores)
                equipo.NroEquipo = Convert.ToInt32(NroEquipo.Value);
                int NumJugador = 1;

                foreach(Jugador jugador in equipo.Jugadores)
                {                    
                    SqlCommand ComandoDetalle = new SqlCommand("SP_INSERTAR_DETALLE",Conexion);
                    ComandoDetalle.CommandType = CommandType.StoredProcedure;
                    ComandoDetalle.Transaction = Transaccion;

                    ComandoDetalle.Parameters.Clear();
                    ComandoDetalle.Parameters.AddWithValue("@id_jugador", NumJugador);
                    ComandoDetalle.Parameters.AddWithValue("@id_persona", jugador.Persona.NroPersona);
                    ComandoDetalle.Parameters.AddWithValue("@id_equipo", equipo.NroEquipo);
                    ComandoDetalle.Parameters.AddWithValue("@camiseta", jugador.NumCamiseta);
                    ComandoDetalle.Parameters.AddWithValue("@id_posicion", jugador.Posicion);  
                    ComandoDetalle.ExecuteNonQuery();
                    NumJugador++;
                }

                Transaccion.Commit();
                return confirmar;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "A");
                Transaccion.Rollback();
                return confirmar = false;
            }
            finally
            {
                Conexion.Close();
            }

        }

        public bool InsertarPersona(Persona persona)
        {
            bool confirmar = true;
            SqlTransaction Transaction = null;
            try
            {
                Conexion.Open();
                Transaction = Conexion.BeginTransaction();
                Comando.Connection = Conexion;
                Comando.Transaction = Transaction;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "SP_INSERTAR_PERSONA";
                Comando.Parameters.Clear();
                Comando.Parameters.AddWithValue("@nombre",persona.Nombre);
                Comando.Parameters.AddWithValue("@apellido",persona.Apellido);
                Comando.Parameters.AddWithValue("@dni",persona.DNI);
                Comando.Parameters.AddWithValue("@fecha_nac",persona.Fecha_nac.ToString("yyyy/MM/dd"));
                Comando.Parameters.AddWithValue("@activo", "si");
                Comando.ExecuteNonQuery();
                Transaction.Commit();
                return confirmar;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "A");
                Transaction.Rollback();
                return confirmar = false;
            }
            finally
            {
                Conexion.Close();
            }

        }
    }
}
