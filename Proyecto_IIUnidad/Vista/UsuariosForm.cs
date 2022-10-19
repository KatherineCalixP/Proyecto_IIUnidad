using Datos;
using Entidades;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class UsuariosForm : Form
    {
        public UsuariosForm()

        {
            InitializeComponent();
        }
        //instanciar objeto
        UsuarioDatos userDatos = new UsuarioDatos();
        string tipoOperacion = "";
        Usuario user;
        private void UsuariosForm_Load(object sender, EventArgs e)
        {
            LlenarDataGrid();//procedimiento
        }

        //procidimieto privdo asincrono
        private async void LlenarDataGrid()
        {
            UsuariosdataGridView.DataSource = await userDatos.DevolverListaAsync();
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            HabilitarControles();
            tipoOperacion = "Nuevo";
        }
        private void HabilitarControles()
        {
            CodigoTextBox.Enabled = true;
            NombreTextBox.Enabled = true;
            ClaveTextBox.Enabled = true;
            CorreoTextBox.Enabled = true;
            RolComboBox.Enabled = true;
            EstaActivoCheckBox.Enabled = true;
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            DesabilitarControles();
            LimpiarControles();
        }
        private void DesabilitarControles()
        {
            CodigoTextBox.Enabled = false;
            NombreTextBox.Enabled = false;
            ClaveTextBox.Enabled = false;
            CorreoTextBox.Enabled = false;
            RolComboBox.Enabled = false;
            EstaActivoCheckBox.Enabled = false;
        }

        private void LimpiarControles()
        {
            CodigoTextBox.Clear();
            NombreTextBox.Text = String.Empty;
            ClaveTextBox.Text = "";
            CorreoTextBox.Clear();
            RolComboBox.Text = String.Empty;
            EstaActivoCheckBox.Checked = false;
        }

        private async void GuardarButton_Click(object sender, EventArgs e)
        {
            user = new Usuario();

            if (tipoOperacion == "Nuevo")
            {
                if (string.IsNullOrEmpty(CodigoTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese un codigo");
                    CodigoTextBox.Focus();
                    return;

                }
                if (string.IsNullOrEmpty(NombreTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese un Nombre");
                    NombreTextBox.Focus();
                    return;

                }
                if (string.IsNullOrEmpty(ClaveTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese una Clave");
                    ClaveTextBox.Focus();
                    return;

                }
                if (string.IsNullOrEmpty(RolComboBox.Text))
                {
                    errorProvider1.SetError(RolComboBox, "Seleccione un rol");
                    RolComboBox.Focus();
                    return;
                }

                user.Codigo = CodigoTextBox.Text;
                user.Nombre = NombreTextBox.Text;
                user.Clave = ClaveTextBox.Text;
                user.Correo = CorreoTextBox.Text;
                user.Rol = RolComboBox.Text;
                user.EstActivo = EstaActivoCheckBox.Checked;

                bool inserto = await userDatos.InsertarAsync(user);

                if (inserto)
                {
                    LlenarDataGrid();
                    LimpiarControles();
                    DesabilitarControles();
                    MessageBox.Show("Usuario Guardado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Usuario No se pudo Guardar", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else if (tipoOperacion == "Modificar")
            {
                if (string.IsNullOrEmpty(CodigoTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese un codigo");
                    CodigoTextBox.Focus();
                    return;

                }
                if (string.IsNullOrEmpty(NombreTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese un Nombre");
                    NombreTextBox.Focus();
                    return;

                }
                if (string.IsNullOrEmpty(ClaveTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese una Clave");
                    ClaveTextBox.Focus();
                    return;

                }
                if (string.IsNullOrEmpty(RolComboBox.Text))
                {
                    errorProvider1.SetError(RolComboBox, "Seleccione un rol");
                    RolComboBox.Focus();
                    return;
                }
                user.Codigo = CodigoTextBox.Text;
                user.Nombre = NombreTextBox.Text;
                user.Clave = ClaveTextBox.Text;
                user.Correo = CorreoTextBox.Text;
                user.Rol = RolComboBox.Text;
                user.EstActivo = EstaActivoCheckBox.Checked;

                bool modifico = await userDatos.ActualizarAsync(user);
                if (modifico)
                {
                    LlenarDataGrid();
                    LimpiarControles();
                    DesabilitarControles();
                    MessageBox.Show("Usuario Guardado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Usuario No se pudo Guardar", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            tipoOperacion = "Modificar";
            if (UsuariosdataGridView.SelectedRows.Count > 0)
            {
                CodigoTextBox.Text = UsuariosdataGridView.CurrentRow.Cells["Codigo"].Value.ToString();
                NombreTextBox.Text = UsuariosdataGridView.CurrentRow.Cells["Nombre"].Value.ToString();
                ClaveTextBox.Text = UsuariosdataGridView.CurrentRow.Cells["Clave"].Value.ToString();
                CorreoTextBox.Text = UsuariosdataGridView.CurrentRow.Cells["Correo"].Value.ToString();
                RolComboBox.Text = UsuariosdataGridView.CurrentRow.Cells["Rol"].Value.ToString();
                //EstaActivoCheckBox.Checked =Convert.ToBoolean(UsuariosdataGridView.CurrentRow.Cells["EstActivo"].Value);
                HabilitarControles();
                CodigoTextBox.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("Debe selecionar un registro", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void EliminarButton_Click(object sender, EventArgs e)
        {
            if (UsuariosdataGridView.SelectedRows.Count > 0)
            {
                bool eliminar = await userDatos.EliminarAsync(UsuariosdataGridView.CurrentRow.Cells["Codigo"].Value.ToString());
                if (eliminar)
                {
                    LlenarDataGrid();
                    LimpiarControles();
                    DesabilitarControles();
                    MessageBox.Show("Usuario Eliminado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Usuario No se pudo Eleminar", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
    }
}

