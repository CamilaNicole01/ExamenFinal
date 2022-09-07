using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Final.Logic;
using Final.Dominio;

namespace Final.AppWin
{
    public partial class Consulta : Form
    {
        Prestamo prestamo;

        public Consulta(Prestamo prestao)
        {
            InitializeComponent();
        }

        private void iniciarFormulario(object sender, EventArgs e)
        { 
          cargarDatosCbo();
            
         }

        private void cargarDatosCbo()
         {
        cboPrestamo.DataSource = PrestamoBL.Listar();
        cboPrestamo.DisplayMember = "Numero";
        cboPrestamo.ValueMember = "ID";
         }

        private void generarConsulta(object sender, EventArgs e)
        {

            asignarObjeto();
            var exito = false;
            if (prestamo.ID == 0)
            {
                exito = PrestamoBL.Insertar(prestamo);
            }
            else
            {
                exito = PrestamoBL.Actualizar(prestamo);
            }
            if (exito)
            {
                MessageBox.Show("El préstamo ha sido registrado", "Final",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("No se ha podido completar la operación", "Final",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void asignarObjeto()
        {
            
        }
    }
}
