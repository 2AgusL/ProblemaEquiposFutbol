using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prog2_Problema_1._4__Proyecto_Equipo_.Presentacion;

namespace Prog2_Problema_1._4__Proyecto_Equipo_
{
    public partial class FrmMenuPrincipal : Form
    {
        public FrmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void FrmMenuPrincipal_Load(object sender, EventArgs e)
        {

        }
        private void crearEquipoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmCrearEquipo CrearEquipo = new FrmCrearEquipo();
            CrearEquipo.ShowDialog();
            CrearEquipo.Dispose();
        }

        private void crearToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void agregarToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            FrmAgregarPersona AgregarPersona = new FrmAgregarPersona();
            AgregarPersona.ShowDialog();
            AgregarPersona.Dispose();
        }

    }
}
