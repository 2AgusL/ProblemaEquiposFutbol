using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prog2_Problema_1._4__Proyecto_Equipo_.Dominio;
using Prog2_Problema_1._4__Proyecto_Equipo_.AccesoDatos;

namespace Prog2_Problema_1._4__Proyecto_Equipo_.Presentacion
{
    public partial class FrmCrearEquipo : Form
    {
        DBHelper Helper;
        Equipo Equipo;

        public FrmCrearEquipo()
        {
            InitializeComponent();
            Helper = new DBHelper();
            Equipo = new Equipo();
        }

        private void FrmCrearEquipo_Load(object sender, EventArgs e)
        {
            cboPersona.SelectedIndex = -1;
            cboPosicion.SelectedIndex = -1;
            btnAceptar.Enabled = false;
            CargarPosiciones();
            CargarPersonas();
            CargarIDEquipo();
        }

        ////////   METODOS
        
        private void CargarPosiciones()
        {
            DataTable Tabla = new DataTable();
            Tabla = Helper.ConsultarDB("SP_CONSULTAR_POSICIONES");
            cboPosicion.DataSource = Tabla;
            cboPosicion.ValueMember = Tabla.Columns[0].ColumnName;
            cboPosicion.DisplayMember = Tabla.Columns[1].ColumnName;
        }

        private void CargarPersonas()
        {
            DataTable Tabla = new DataTable();
            Tabla = Helper.ConsultarDB("SP_CONSULTAR_PERSONAS");
            cboPersona.DataSource = Tabla;
            cboPersona.ValueMember = Tabla.Columns[0].ColumnName;
            cboPersona.DisplayMember = Tabla.Columns[5].ColumnName; 
        }

        private void CargarIDEquipo()
        {
            int ID = Helper.MostrarID();
            if (ID > 0) lblNroEquipo.Text += ID.ToString();
            else MessageBox.Show("Equipo no encontrado", "ERROR");

        }

        private void GuargarEquipo()
        {
            Equipo.Nombre = txtNomEquipo.Text;
            Equipo.DirectorTec = txtDireTec.Text;

            if (Helper.InsertarEquipo(Equipo))
            {
                MessageBox.Show("Se ha cargado el equipo correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error, no se ha cargado el equipo correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        //////// BOTONES

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(txtNomEquipo.Text == "")
            {
                MessageBox.Show("Debes escribir el nombre del equipo","ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtDireTec.Text == "")
            {
                MessageBox.Show("Debes escribir el nombre del director tecnico", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cboPersona.Text == "") 
            {
                MessageBox.Show("Debes seleccionar una persona", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cboPosicion.Text == "") 
            {
                MessageBox.Show("Debes seleccionar la posicion del jugador", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtNumCamiseta.Text == "")
            {
                MessageBox.Show("Debes escribir el numero de la camiseta del jugador", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (DataGridViewRow row in dgvEquipo.Rows)
            {
                if (row.Cells["ColumNombreJug"].Value.ToString().Equals(cboPersona.Text))
                {
                    MessageBox.Show("Este jugador ya esta registrado en este equipo", "ERROR");
                    return;
                }
            }

            DataRowView item = (DataRowView)cboPersona.SelectedItem;
            int NroPersona = Convert.ToInt32(item.Row.ItemArray[0]);
            string Nombre = item.Row.ItemArray[1].ToString();
            string Apellido = item.Row.ItemArray[2].ToString();
            int DNI = Convert.ToInt32(item.Row.ItemArray[3]);
            DateTime Fecha_Nac = Convert.ToDateTime(item.Row.ItemArray[4]);
            Persona persona = new Persona(NroPersona, Nombre, Apellido, DNI, Fecha_Nac);

            int posicion = Convert.ToInt32(cboPosicion.SelectedValue);
            int NroCamiseta = Convert.ToInt32(txtNumCamiseta.Text);

            Jugador Jugador = new Jugador(persona, NroCamiseta, posicion);

            Equipo.AgregarJugador(Jugador);

            dgvEquipo.Rows.Add(new object[] {item.Row.ItemArray[0], item.Row.ItemArray[5], txtNumCamiseta.Text, cboPosicion.Text});

            btnAceptar.Enabled = true;
        }

        private void dgvEquipo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEquipo.CurrentCell.ColumnIndex == 4)
            {
                Equipo.EliminarJugador(dgvEquipo.CurrentRow.Index);
                dgvEquipo.Rows.Remove(dgvEquipo.CurrentRow);

                if(dgvEquipo.Rows.Count == 0)
                {
                    btnAceptar.Enabled = false;
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            GuargarEquipo();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de que desea cancelar la creacion de un equipo?", "Aviso",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) this.Dispose();
        }

    }
}
