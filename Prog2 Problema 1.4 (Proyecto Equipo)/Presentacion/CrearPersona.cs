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
    public partial class FrmAgregarPersona : Form
    {
        DBHelper Helper = new DBHelper();
        Persona Persona = new Persona();

        public FrmAgregarPersona()
        {
            InitializeComponent();
        }

        private void FrmAgregarPersona_Load(object sender, EventArgs e)
        {
            dtpFechaNac.Value = DateTime.Today;
        }

        //BOTONES
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(txtNombre.Text == "")
            {
                MessageBox.Show("No se ha escrito el nombre de la persona", "ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtNombre.Focus();
                return;
            }
            if(txtApellido.Text == "")
            {
                MessageBox.Show("No se ha escrito el apellido de la persona", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtApellido.Focus();
                return;
            }
            if(txtDNI.Text == "")
            {
                MessageBox.Show("No se escrito el DNI de la persona", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDNI.Focus();
                return;
            }
            if(dtpFechaNac.Value >= DateTime.Today)
            {
                MessageBox.Show("La fecha de nacimiento esta mal colocada", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpFechaNac.Focus();
                return;
            }

            Persona.Nombre = txtNombre.Text;
            Persona.Apellido = txtApellido.Text;
            Persona.DNI = Convert.ToInt32(txtDNI.Text);
            Persona.Fecha_nac = Convert.ToDateTime(dtpFechaNac.Value);

            if (Helper.InsertarPersona(Persona))
            {
                MessageBox.Show("Se ha cargado correctamente la persona", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Hubo un error, no se cargo correctamente la persona", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quiere cancelar el proceso?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) this.Dispose();
        }
    }
}
