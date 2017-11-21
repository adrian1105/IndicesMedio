using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
using System.Threading.Tasks;

namespace IndiceMedios
{
    public partial class FormPrincipal : Form
    {
        DataTable dtPropidadFinal = new DataTable();
        //string ConnString = "Server=ASD-PC\\SQLEXPRESS;Database=ANLA;Integrated Security=SSPI;";
        string ConnString = "Server=C46ASDDESAP2048;Database=ANLA;Integrated Security=SSPI;";
        int contador = 0;
        public FormPrincipal()
        {
            InitializeComponent();
            rtbObservacion.ReadOnly = true;
            dgvDatosUnidad.Visible = false;
            comboBox1_SelectedIndexChanged(null, null);
            this.tbExpediente.Focus();
        }
        //METODO PARA VER UNIDAD ACTIVA
        private void CargarUnidades()
        {
            contador = 0;
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            List<String> unidades = new List<String>();

            foreach (DriveInfo d in allDrives)
            {
                string tipo = Convert.ToString(d.DriveType);
                string unidad = d.Name;

                if (d.Name != "C:\\" && tipo != "Network" && tipo != "NoRootDirectory" && tipo != "Ram" && d.IsReady)
                {
                    unidades.Add(d.Name);
                    contador += 1;
                }
            }
            if (contador == 1)
            {
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";
                comboBox1.DataSource = unidades;
            }
            else
            {
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";
                comboBox1.DataSource = null;
                dtPropidadFinal = null;
                lblCantidad.Visible = false;
                lblInfo.Visible = false;
                dgvDatosUnidad.Visible = false;
                checkBox1.Checked = false;
            }
        }

        //TIMER PARA ACTUALIZACION DE UNIDAD
        private void timer1_Tick(object sender, EventArgs e)
        {
            CargarUnidades();
        }
        //SI HAY CAMBIO EN COMBOBOX ACTUALIZA LISTA
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                comboBox1.DataSource = null;
                dgvDatosUnidad.Visible = false;
                lblInfo.Visible = false;
                lblCantidad.Visible = false;
            }
            else
            {
                tbConsecutivo.Visible = true;
                tbRadicado.Visible = true;
                label1.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                tbFechaRadicado.Visible = true;
            }
        }
        //MUESTRA LAS PROPIEDADES INICIALES DE LOS ARCHVOS EN LA UNIDAD
        public DataTable PropiedadesDatos()
        {
            string fechaModificacion = string.Empty;
            DataTable dtPropidad = new DataTable();
            if (comboBox1.SelectedValue == null)
            {
                checkBox1.Checked = false;
                dgvDatosUnidad.Visible = false;
                lblCantidad.Visible = false;
            }
            else
            {
                dgvDatosUnidad.Visible = true;
                DirectoryInfo di = new DirectoryInfo(@"" + comboBox1.SelectedValue + "");
                List<String> dato = new List<String>();
                dtPropidad.Columns.Add("Nombre");
                dtPropidad.Columns.Add("Extencion");
                dtPropidad.Columns.Add("Tamaño");
                dtPropidad.Columns.Add("Tipo");
                dtPropidad.Columns.Add("FechaModificacion");
                dtPropidad.Columns.Add("FechaCreacion");

                int cont = 0;
                try
                {
                    foreach (var fi in di.GetFiles("*", SearchOption.AllDirectories))
                    {
                        cont += 1;

                        string fechaCreacion = string.Empty;
                        string tipof = string.Empty;
                        string extencion = fi.Extension;
                        string name = fi.FullName;
                        string ruta = name.Substring(3);
                        string directorio = fi.DirectoryName;
                        string tamaño = fi.Length.ToString() + " Bytes";
                        string tipoArchivo = ValidarExtencion(extencion);
                        fechaCreacion = Convert.ToString(File.GetCreationTime(name.ToString()));
                        fechaModificacion = Convert.ToString(File.GetLastWriteTime(name.ToString()));
                        if (extencion == "")
                        {
                            extencion = "Sin extension";
                        }
                        DataRow dr = dtPropidad.NewRow();
                        dr["Nombre"] = ruta;
                        dr["Extencion"] = extencion;
                        dr["Tamaño"] = tamaño;
                        dr["Tipo"] = tipoArchivo;
                        dr["FechaModificacion"] = fechaModificacion;
                        dr["FechaCreacion"] = fechaCreacion;
                        dtPropidad.Rows.Add(dr);
                        dtPropidad.AcceptChanges();
                    }
                    lblInfo.Visible = true;
                    lblCantidad.Visible = true;
                    lblCantidad.Text = cont.ToString();
                    dgvDatosUnidad.DataSource = dtPropidad;
                    dtPropidadFinal = dtPropidad.Clone();
                    dtPropidadFinal = dtPropidad;
                }
                catch (UnauthorizedAccessException)
                {

                }
            }
            return dtPropidad;
        }

        private List<string> GetFiles(string path, string pattern)
        {
            var files = new List<string>();
            try
            {
                if (comboBox1.SelectedValue == null)
                {
                    checkBox1.Checked = false;
                    dgvDatosUnidad.Visible = false;
                    lblCantidad.Visible = false;
                }
                else
                {
                    if (Path.IsPathRooted(path))
                    {

                    }
                    else
                    {
                        files.AddRange(Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly));
                        foreach (var directory in Directory.GetDirectories(path))
                        {
                            files.AddRange(GetFiles(directory, pattern));
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException) { }

            return files;
        }
        //
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult boton = new DialogResult();

            if (contador == 0 || contador > 1)
            {
                boton = MessageBox.Show("No ha ingresado o no se puede leer la unidad en el equipo.\nAceptar - para guardar.\nCancelar - para verificar ", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (boton == DialogResult.OK)
                {

                    List<String> dato = new List<String>();
                    string fechaModificacion = string.Empty;
                    string fechaCreacion = string.Empty;
                    string consecutivoAnexo = string.Empty;
                    string codigoExpediente = string.Empty;
                    string radicado = string.Empty;
                    string FechaRadicado = string.Empty;
                    DataTable valida = ValidarConsecutivo(tbExpediente.Text, tbTomo.Text);
                    dataGridView2.DataSource = valida;
                    string conteo = dataGridView2.Rows[0].Cells[0].Value.ToString();
                    if (conteo == "0")
                    {
                        if (tbExpediente.Text != "" && tbTomo.Text != "")
                        {
                            radicado = tbRadicado.Text;
                            if (tbFechaRadicado.Text == "  /  /")
                            {
                                FechaRadicado = "";
                            }
                            else
                            {
                                FechaRadicado = tbFechaRadicado.Text;
                            }

                            if ((tbEmpresa.Text != "" && tbSector.Text != "" && tbRadicado.Text != "" && FechaRadicado != "" && rtbDescripcion.Text != ""))
                            {
                                Object tipoMedio = cbTipoMedio.Text;
                                InsertarDatosCodigo(tbExpediente.Text, tbEmpresa.Text, tbSector.Text, tbTomo.Text, tipoMedio.ToString(), rtbDescripcion.Text, tbConsecutivo.Text, radicado, FechaRadicado);
                                if (comboBox1.SelectedValue == null)
                                {
                                    InsertarPropiedades(tbExpediente.Text, tbTomo.Text, "", "", "", "", "", "", rtbObservacion.Text);
                                    string expedinte = tbExpediente.Text;
                                    string tomo = tbTomo.Text;
                                    FormSecundario frm = new FormSecundario(expedinte, tomo);
                                    frm.Show();
                                    dgvDatosUnidad.Visible = false;
                                    checkBox1.Checked = false;
                                    tbConsecutivo.Visible = true;
                                    tbRadicado.Visible = true;
                                    label1.Visible = true;
                                    label4.Visible = true;
                                    label5.Visible = true;
                                    tbFechaRadicado.Visible = true;
                                    tbConsecutivo.Text = "";
                                    tbRadicado.Text = "";
                                    lblCantidad.Visible = false;
                                    lblInfo.Visible = false;
                                    tbExpediente.Text = "";
                                    tbTomo.Text = "";
                                    tbEmpresa.Text = "";
                                    tbSector.Text = "";
                                    rtbDescripcion.Text = "";
                                    rtbObservacion.Text = "";

                                    MessageBox.Show("Los datos se guardaron correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    for (int i = 0; i < dtPropidadFinal.Rows.Count; i++)
                                    {

                                        string name = dtPropidadFinal.Rows[i].ItemArray.GetValue(0).ToString();
                                        string ruta = name.Substring(3);
                                        string extencion = dtPropidadFinal.Rows[i].ItemArray.GetValue(1).ToString();

                                        if (extencion == "")
                                        {
                                            extencion = "Sin extension";
                                        }
                                        InsertarPropiedades(tbExpediente.Text, tbTomo.Text, ruta, extencion.ToString(), dtPropidadFinal.Rows[i].ItemArray.GetValue(2).ToString(), dtPropidadFinal.Rows[i].ItemArray.GetValue(3).ToString(), dtPropidadFinal.Rows[i].ItemArray.GetValue(4).ToString(), dtPropidadFinal.Rows[i].ItemArray.GetValue(5).ToString(), rtbObservacion.Text);
                                    }
                                    string expedinte = tbExpediente.Text;
                                    string tomo = tbTomo.Text;
                                    FormSecundario frm = new FormSecundario(expedinte, tomo);
                                    frm.Show();
                                    dgvDatosUnidad.Visible = false;
                                    checkBox1.Checked = false;
                                    tbConsecutivo.Visible = true;
                                    tbRadicado.Visible = true;
                                    label1.Visible = true;
                                    label4.Visible = true;
                                    label5.Visible = true;
                                    tbFechaRadicado.Visible = true;
                                    tbConsecutivo.Text = "";
                                    tbRadicado.Text = "";
                                    lblCantidad.Visible = false;
                                    lblInfo.Visible = false;
                                    tbExpediente.Text = "";
                                    tbTomo.Text = "";
                                    tbEmpresa.Text = "";
                                    tbSector.Text = "";
                                    rtbDescripcion.Text = "";
                                    rtbObservacion.Text = "";

                                    MessageBox.Show("Los datos se guardaron correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                if (rtbObservacion.Text != "" && (tbEmpresa.Text == "" || tbSector.Text == "" || tbRadicado.Text == "" || FechaRadicado == "" || rtbDescripcion.Text == ""))
                                {
                                    Object tipoMedio = cbTipoMedio.Text;
                                    if (checkBox1.Checked == true)
                                        InsertarDatosCodigo(tbExpediente.Text, tbEmpresa.Text, tbSector.Text, tbTomo.Text, tipoMedio.ToString(), rtbDescripcion.Text, tbConsecutivo.Text, radicado, FechaRadicado);

                                    if (comboBox1.SelectedValue == null)
                                    {
                                        InsertarPropiedades(tbExpediente.Text, tbTomo.Text, "", "", "", "", "", "", rtbObservacion.Text);
                                        string expedinte = tbExpediente.Text;
                                        string tomo = tbTomo.Text;
                                        FormSecundario frm = new FormSecundario(expedinte, tomo);
                                        frm.Show();
                                        dgvDatosUnidad.Visible = false;
                                        checkBox1.Checked = false;
                                        tbConsecutivo.Visible = true;
                                        tbRadicado.Visible = true;
                                        label1.Visible = true;
                                        label4.Visible = true;
                                        label5.Visible = true;
                                        tbFechaRadicado.Visible = true;
                                        tbConsecutivo.Text = "";
                                        tbRadicado.Text = "";
                                        lblCantidad.Visible = false;
                                        lblInfo.Visible = false;
                                        tbExpediente.Text = "";
                                        tbTomo.Text = "";
                                        tbEmpresa.Text = "";
                                        tbSector.Text = "";
                                        rtbDescripcion.Text = "";
                                        rtbObservacion.Text = "";

                                        MessageBox.Show("Los datos se guardaron correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        for (int i = 0; i < dtPropidadFinal.Rows.Count; i++)
                                        {

                                            string name = dtPropidadFinal.Rows[i].ItemArray.GetValue(0).ToString();
                                            string ruta = name.Substring(3);
                                            string extencion = dtPropidadFinal.Rows[i].ItemArray.GetValue(1).ToString();

                                            if (extencion == "")
                                            {
                                                extencion = "Sin extension";
                                            }
                                            InsertarPropiedades(tbExpediente.Text, tbTomo.Text, ruta, extencion.ToString(), dtPropidadFinal.Rows[i].ItemArray.GetValue(2).ToString(), dtPropidadFinal.Rows[i].ItemArray.GetValue(3).ToString(), dtPropidadFinal.Rows[i].ItemArray.GetValue(4).ToString(), dtPropidadFinal.Rows[i].ItemArray.GetValue(5).ToString(), rtbObservacion.Text);
                                        }
                                        string expedinte = tbExpediente.Text;
                                        string tomo = tbTomo.Text;
                                        FormSecundario frm = new FormSecundario(expedinte, tomo);
                                        frm.Show();
                                        dgvDatosUnidad.Visible = false;
                                        checkBox1.Checked = false;
                                        tbConsecutivo.Visible = true;
                                        tbRadicado.Visible = true;
                                        label1.Visible = true;
                                        label4.Visible = true;
                                        label5.Visible = true;
                                        tbFechaRadicado.Visible = true;
                                        tbConsecutivo.Text = "";
                                        tbRadicado.Text = "";
                                        lblCantidad.Visible = false;
                                        lblInfo.Visible = false;
                                        tbExpediente.Text = "";
                                        tbTomo.Text = "";
                                        tbEmpresa.Text = "";
                                        tbSector.Text = "";
                                        rtbDescripcion.Text = "";
                                        rtbObservacion.Text = "";

                                        MessageBox.Show("Los datos se guardaron correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else if (rtbObservacion.Text == "" && (tbEmpresa.Text == "" || tbSector.Text == "" || tbRadicado.Text == "" || FechaRadicado == "" || rtbDescripcion.Text == ""))
                                {
                                    MessageBox.Show("Hay Campos vacíos, Por favor ingrese observación", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ingrese numero de expediente y el numero del tomo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El expediente y el tomo ya existen en la Base de Datos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {

                }
            }
            else
            {
                List<String> dato = new List<String>();
                string fechaModificacion = string.Empty;
                string fechaCreacion = string.Empty;
                string consecutivoAnexo = string.Empty;
                string codigoExpediente = string.Empty;
                string radicado = string.Empty;
                string FechaRadicado = string.Empty;
                DataTable valida = ValidarConsecutivo(tbExpediente.Text, tbTomo.Text);
                dataGridView2.DataSource = valida;
                string conteo = dataGridView2.Rows[0].Cells[0].Value.ToString();
                if (conteo == "0")
                {
                    if (tbExpediente.Text != "" && tbTomo.Text != "")
                    {
                        radicado = tbRadicado.Text;
                        if (tbFechaRadicado.Text == "  /  /")
                        {
                            FechaRadicado = "";
                        }
                        else
                        {
                            FechaRadicado = tbFechaRadicado.Text;
                        }

                        if ((tbEmpresa.Text != "" && tbSector.Text != "" && tbRadicado.Text != "" && FechaRadicado != "" && rtbDescripcion.Text != ""))
                        {
                            Object tipoMedio = cbTipoMedio.Text;
                            if (checkBox1.Checked == true)
                            {
                                InsertarDatosCodigo(tbExpediente.Text, tbEmpresa.Text, tbSector.Text, tbTomo.Text, tipoMedio.ToString(), rtbDescripcion.Text, tbConsecutivo.Text, radicado, FechaRadicado);
                            }

                            if (comboBox1.SelectedValue == null)
                            {
                                InsertarPropiedades(tbExpediente.Text, tbTomo.Text, "", "", "", "", "", "", rtbObservacion.Text);
                            }
                            else
                            {
                                if (checkBox1.Checked == true)
                                {
                                    for (int i = 0; i < dtPropidadFinal.Rows.Count; i++)
                                    {

                                        string name = dtPropidadFinal.Rows[i].ItemArray.GetValue(0).ToString();
                                        string ruta = name.Substring(3);
                                        string extencion = dtPropidadFinal.Rows[i].ItemArray.GetValue(1).ToString();

                                        if (extencion == "")
                                        {
                                            extencion = "Sin extension";
                                        }
                                        InsertarPropiedades(tbExpediente.Text, tbTomo.Text, ruta, extencion.ToString(), dtPropidadFinal.Rows[i].ItemArray.GetValue(2).ToString(), dtPropidadFinal.Rows[i].ItemArray.GetValue(3).ToString(), dtPropidadFinal.Rows[i].ItemArray.GetValue(4).ToString(), dtPropidadFinal.Rows[i].ItemArray.GetValue(5).ToString(), rtbObservacion.Text);
                                    }
                                    string expedinte = tbExpediente.Text;
                                    string tomo = tbTomo.Text;
                                    FormSecundario frm = new FormSecundario(expedinte, tomo);
                                    frm.Show();
                                    dgvDatosUnidad.Visible = false;
                                    checkBox1.Checked = false;
                                    tbConsecutivo.Visible = true;
                                    tbRadicado.Visible = true;
                                    label1.Visible = true;
                                    label4.Visible = true;
                                    label5.Visible = true;
                                    tbFechaRadicado.Visible = true;
                                    tbConsecutivo.Text = "";
                                    tbRadicado.Text = "";
                                    lblCantidad.Visible = false;
                                    lblInfo.Visible = false;
                                    tbExpediente.Text = "";
                                    tbTomo.Text = "";
                                    tbEmpresa.Text = "";
                                    tbSector.Text = "";
                                    rtbDescripcion.Text = "";
                                    rtbObservacion.Text = "";

                                    MessageBox.Show("Los datos se guardaron correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Verifique la unidad", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                        {
                            if (rtbObservacion.Text != "" && (tbEmpresa.Text == "" || tbSector.Text == "" || tbRadicado.Text == "" || FechaRadicado == "" || rtbDescripcion.Text == ""))
                            {
                                Object tipoMedio = cbTipoMedio.Text;
                                if (checkBox1.Checked == true)
                                {
                                    InsertarDatosCodigo(tbExpediente.Text, tbEmpresa.Text, tbSector.Text, tbTomo.Text, tipoMedio.ToString(), rtbDescripcion.Text, tbConsecutivo.Text, radicado, FechaRadicado);
                                }
                                if (comboBox1.SelectedValue == null)
                                {
                                    InsertarPropiedades(tbExpediente.Text, tbTomo.Text, "", "", "", "", "", "", rtbObservacion.Text);
                                }
                                else
                                {
                                    if (checkBox1.Checked == true)
                                    {
                                        for (int i = 0; i < dtPropidadFinal.Rows.Count; i++)
                                        {

                                            string name = dtPropidadFinal.Rows[i].ItemArray.GetValue(0).ToString();
                                            string ruta = name.Substring(3);
                                            string extencion = dtPropidadFinal.Rows[i].ItemArray.GetValue(1).ToString();

                                            if (extencion == "")
                                            {
                                                extencion = "Sin extension";
                                            }
                                            InsertarPropiedades(tbExpediente.Text, tbTomo.Text, ruta, extencion.ToString(), dtPropidadFinal.Rows[i].ItemArray.GetValue(2).ToString(), dtPropidadFinal.Rows[i].ItemArray.GetValue(3).ToString(), dtPropidadFinal.Rows[i].ItemArray.GetValue(4).ToString(), dtPropidadFinal.Rows[i].ItemArray.GetValue(5).ToString(), rtbObservacion.Text);
                                        }
                                        string expedinte = tbExpediente.Text;
                                        string tomo = tbTomo.Text;
                                        FormSecundario frm = new FormSecundario(expedinte, tomo);
                                        frm.Show();
                                        dgvDatosUnidad.Visible = false;
                                        checkBox1.Checked = false;
                                        tbConsecutivo.Visible = true;
                                        tbRadicado.Visible = true;
                                        label1.Visible = true;
                                        label4.Visible = true;
                                        label5.Visible = true;
                                        tbFechaRadicado.Visible = true;
                                        tbConsecutivo.Text = "";
                                        tbRadicado.Text = "";
                                        lblCantidad.Visible = false;
                                        lblInfo.Visible = false;
                                        tbExpediente.Text = "";
                                        tbTomo.Text = "";
                                        tbEmpresa.Text = "";
                                        tbSector.Text = "";
                                        rtbDescripcion.Text = "";
                                        rtbObservacion.Text = "";

                                        MessageBox.Show("Los datos se guardaron correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Verifique la unidad", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                            else if (rtbObservacion.Text == "" && (tbEmpresa.Text == "" || tbSector.Text == "" || tbRadicado.Text == "" || FechaRadicado == "" || rtbDescripcion.Text == ""))
                            {
                                MessageBox.Show("Hay Campos vacíos, Por favor ingrese observación", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Verifique todos los campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("El expediente y el tomo ya existen en la Base de Datos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //** COLSULTAS
        public void InsertarDatosCodigo(string expediente, string empresa, string sector, string tomo, string tipoMedio, string tescripcion, string consecutivo, string radicado, string FechaRadicado)
        {
            string SqlString = "Insert Into CodigoMedio (Expediente, Empresa, Sector, Tomo, TipoMedio, Descripcion,ConsecutivoUnidad, radicado, FechaRadicado) Values (@expediente,@empresa,@sector,@tomo,@tipoMedio,@Descripcion,@ConsecutivoUnidad,@radicado, @FechaRadicado)";

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(SqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ConsecutivoUnidad", consecutivo);
                    cmd.Parameters.AddWithValue("@radicado", radicado);
                    cmd.Parameters.AddWithValue("@FechaRadicado", FechaRadicado);
                    cmd.Parameters.AddWithValue("@expediente", expediente);
                    cmd.Parameters.AddWithValue("@empresa", empresa);
                    cmd.Parameters.AddWithValue("@sector", sector);
                    cmd.Parameters.AddWithValue("@tomo", tomo);
                    cmd.Parameters.AddWithValue("@tipoMedio", tipoMedio);
                    cmd.Parameters.AddWithValue("@Descripcion", tescripcion);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }

        public void InsertarPropiedades(string expediente, string tomo, string nombre, string extencion, string tamaño, string tipo, string fechaModificacion, string fechaCreacion, string observacion)
        {
            string SqlString = "Insert Into PropiedadesMedios(expediente,tomo,Nombre, Extencion,Tamaño,Tipo, FechaModificacion, FechaCreacion,Observacion) Values (@expediente,@tomo, @Nombre, @Extencion, @Tamaño,@Tipo, @FechaModificacion, @FechaCreacion,@observacion)";

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(SqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@expediente", expediente);
                    cmd.Parameters.AddWithValue("@tomo", tomo);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Extencion", extencion);
                    cmd.Parameters.AddWithValue("@Tamaño", tamaño);
                    cmd.Parameters.AddWithValue("@Tipo", tipo);
                    cmd.Parameters.AddWithValue("@FechaModificacion", fechaModificacion);
                    cmd.Parameters.AddWithValue("@FechaCreacion", fechaCreacion);
                    cmd.Parameters.AddWithValue("@observacion", observacion);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }

        public DataTable ObtenerResultado()
        {
            DataTable obtenerConciliador = new DataTable();
            string SqlString = "SELECT d.Expediente, d.Tomo, Empresa, Sector, TipoMedio, ltrim(CAST(Descripcion AS NVARCHAR)) AS Descripcion,ConsecutivoUnidad,Radicado,FechaRadicado,Nombre,Extencion,Tamaño,Tipo,FechaModificacion,FechaCreacion, Ubicacion, Observacion FROM CodigoMedio d,PropiedadesMedios c where d.Expediente = c.Expediente and d.Tomo = c.Tomo";

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(SqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter SqlDataAdapter = new SqlDataAdapter(cmd);
                        SqlDataAdapter.Fill(obtenerConciliador);
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            return obtenerConciliador;
        }

        public DataTable ValidarConsecutivo(string expediente, string tomo)
        {
            DataTable dt = new DataTable();
            string SqlString = "Select count(ConsecutivoUnidad) from CodigoMedio where Expediente = @expediente and Tomo = @tomo";

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(SqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@expediente", expediente);
                    cmd.Parameters.AddWithValue("@tomo", tomo);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        adap.Fill(dt);
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            return dt;
        }

        public DataTable ObtenerDatos(string expediente, string tomo)
        {
            DataTable dt = new DataTable();
            string SqlString = "select Empresa, Sector,Radicado,FechaRadicado, Descripcion from ConsolidadoMedios where Expediente = @expediente and  Tomo = @tomo";

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(SqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@expediente", expediente);
                    cmd.Parameters.AddWithValue("@tomo", tomo);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        adap.Fill(dt);
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            return dt;
        }

        public DataTable ObtenerTipoMedio(string expediente, string tomo)
        {
            DataTable dt = new DataTable();
            string SqlString = "select  TipoMedio from ConsolidadoMedios where Expediente = @expediente and Tomo = @tomo union all select distinct TipoMedio from ConsolidadoMedios ";

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(SqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@expediente", expediente);
                    cmd.Parameters.AddWithValue("@tomo", tomo);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        adap.Fill(dt);
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            return dt;
        }
        //** FIN DE COLSULTAS

        //VALIDAR EXTENCIONES Y TIPO DE ARCHIVOS

        public string ValidarExtencion(string ext)
        {
            string tipo = string.Empty;
            switch (ext)
            {
                case ".iso":
                    tipo = "Archivo iso";
                    break;
                case ".doc":
                    tipo = "Documento de Word 97-2003";
                    break;
                case ".dotx":
                    tipo = "Plantilla de Word";
                    break;
                case ".docx":
                    tipo = "Documento de Microsoft Word";
                    break;
                case ".xls":
                    tipo = "Hoja de trabajo de Microsoft Excel 97-2003 ";
                    break;
                case ".xlsx":
                    tipo = "Hoja de trabajo de Microsoft Excel";
                    break;
                case ".csv":
                    tipo = "Archivo CSV";
                    break;
                case ".pdf":
                    tipo = "Documento Adobe Acrobat";
                    break;
                case ".txt":
                    tipo = "Documento de texto";
                    break;
                case ".ppt":
                    tipo = "Presentación de Microsoft PowerPoint 97-2003";
                    break;
                case ".pptx":
                    tipo = "Presentación de Microsoft PowerPoint";
                    break;
                case ".jpg":
                    tipo = "Imagen JPEG";
                    break;
                case ".png":
                    tipo = "Imagen PNG";
                    break;
                case ".gif":
                    tipo = "Imagen GIF";
                    break;
                case ".mp4":
                    tipo = "Vídeo MP4";
                    break;
                case ".mp3":
                    tipo = "Sonido en formato MP3";
                    break;
                case ".pub":
                    tipo = "Documento de Microsoft Publisher";
                    break;
                case ".config":
                    tipo = "XML Archivo de Configuracion";
                    break;
                case ".sql":
                    tipo = "SQL Server Query File";
                    break;
                case ".zip":
                    tipo = "Archivo zip";
                    break;
                case ".rar":
                    tipo = "Archivo rar";
                    break;
                case ".7z":
                    tipo = "Archivo 7z";
                    break;
                case ".wmv":
                    tipo = "Archivo de audio o vídeo de Windows Media";
                    break;
                case ".bd":
                    tipo = "Archivo Base de Datos";
                    break;
                case ".dll":
                    tipo = "Extensión de la aplicación";
                    break;
                case ".css":
                    tipo = "Archivo CSS";
                    break;
                case ".exe":
                    tipo = "Aplicación";
                    break;
                case ".msi":
                    tipo = "Paquete de Windows Installer";
                    break;
                case ".avi":
                    tipo = "Clip de vídeo";
                    break;
                case ".jpeg":
                    tipo = "Imagen JPEG";
                    break;
                case ".odt":
                    tipo = "Texto de OpenDocument";
                    break;
                case ".ods":
                    tipo = "Hoja de cálculo OpenDocumen";
                    break;
                case ".odp":
                    tipo = "Presentación de OpenDocument";
                    break;
                case ".odg":
                    tipo = "Dibujo de Open Office";
                    break;
                case ".mbd":
                    tipo = "Archivo de Multimedia";
                    break;
                case ".mdb":
                    tipo = "Archivo Microsoft Access";
                    break;
                case ".rtf":
                    tipo = "Documento de Texto Rico";
                    break;
                case ".ico":
                    tipo = "Icono";
                    break;
                case ".aspx":
                    tipo = "ASP.NET Server Page";
                    break;
                case ".Master":
                    tipo = "ASP.NET Master Page";
                    break;
                case ".xml":
                    tipo = "Documento XML";
                    break;
                case ".inf":
                    tipo = "Información sobre la instalación";
                    break;
                case ".htm":
                    tipo = "Documento HTML";
                    break;
                case ".log":
                    tipo = "Documento de texto";
                    break;
                case ".ogg":
                    tipo = "Archivo OGG";
                    break;
                case ".pps":
                    tipo = "Presentación con diapositivas de Microsoft PowerPoint 97-2003";
                    break;
                case ".bmp":
                    tipo = "Imagen de mapa de bits";
                    break;
                case ".tif":
                    tipo = "Imagen TIFF";
                    break;
                case ".wav":
                    tipo = "Archivo de sonido";
                    break;
                case ".wma":
                    tipo = "Archivo de audio de Windows Media";
                    break;
                case ".ace":
                    tipo = "Archivo ACE";
                    break;
                case ".ai":
                    tipo = "Documento Adobe Ilustrator";
                    break;
                case ".aif":
                    tipo = "Archivo de intercambio de audio";
                    break;
                case ".bin":
                    tipo = "Archivo binario";
                    break;
                case ".bup":
                    tipo = "Archivo de copia de seguridad";
                    break;
                case ".cab":
                    tipo = "Microsoft Windows archivo comprimido";
                    break;
                case ".cda":
                    tipo = "Atajo CD Audio Track";
                    break;
                case ".cdr":
                    tipo = "Corel Draw Vector";
                    break;
                case ".divx":
                    tipo = "Vídeo DivX";
                    break;
                case ".dmg":
                    tipo = "Imagen de disco";
                    break;
                case ".dwg":
                    tipo = "AutoCAD DWG";
                    break;
                case ".eml":
                    tipo = "Mensaje de correo electrónico";
                    break;
                case ".fla":
                    tipo = "Adobe Flash";
                    break;
                case ".flv":
                    tipo = "Flash Video";
                    break;
                case ".jar":
                    tipo = "Archivo Java";
                    break;
                case ".lnk":
                    tipo = "Acceso directo";
                    break;
                case ".mov":
                    tipo = "QuickTime";
                    break;
                case ".mp2":
                    tipo = "MPEG-1 Audio Layer II";
                    break;
                case ".mswmm":
                    tipo = "Windows Movie Maker Archivo de Proyecto";
                    break;
                case ".ps":
                    tipo = "Archivo PostScript";
                    break;
                case ".psd":
                    tipo = "Documento Photoshop";
                    break;
                case ".pst":
                    tipo = "Microsoft Outlook emails";
                    break;
                case ".rm":
                    tipo = "Archivo RealMedia";
                    break;
                case ".torrent":
                    tipo = "BitTorrent";
                    break;
                case ".ttf":
                    tipo = "Tipos de Fuente";
                    break;
                case ".vcd":
                    tipo = "Virtual CD-ROM CD Archivo de imagen";
                    break;
                case ".vob":
                    tipo = "DVD-Video Object";
                    break;
                case ".wpd":
                    tipo = "WordPerfect Document";
                    break;
                case ".wps":
                    tipo = "Documento de texto";
                    break;
                case ".bak":
                    tipo = "Backup";
                    break;
                case ".xlw":
                    tipo = "Area de trabajo de Microsoft Excel";
                    break;
                case ".xlt":
                    tipo = "Plantilla de Microsoft Excel ";
                    break;
                case ".xlm":
                    tipo = "Macro de Microsoft Excel";
                    break;
                case ".xlk":
                    tipo = "Archivo de copia de seguridad de Microsoft Exce";
                    break;
                case ".xld":
                    tipo = "Hoja de cuadros de diálogo de Microsoft Excel";
                    break;
                case ".ide":
                    tipo = "Development environment configuration";
                    break;
                //EXTENCIONES MAYUSCULAS
                case ".ISO":
                    tipo = "Archivo iso";
                    break;
                case ".DOC":
                    tipo = "Documento de Word 97-2003";
                    break;
                case ".DOTX":
                    tipo = "Plantilla de Word";
                    break;
                case ".DOCX":
                    tipo = "Documento de Microsoft Word";
                    break;
                case ".XLS":
                    tipo = "Hoja de trabajo de Microsoft Excel 97-2003 ";
                    break;
                case ".XLSX":
                    tipo = "Hoja de trabajo de Microsoft Excel";
                    break;
                case ".CSV":
                    tipo = "Archivo CSV";
                    break;
                case ".PDF":
                    tipo = "Documento Adobe Acrobat";
                    break;
                case ".TXT":
                    tipo = "Documento de texto";
                    break;
                case ".PPT":
                    tipo = "Presentación de Microsoft PowerPoint 97-2003";
                    break;
                case ".PPTX":
                    tipo = "Presentación de Microsoft PowerPoint";
                    break;
                case ".JPG":
                    tipo = "Imagen JPEG";
                    break;
                case ".PNG":
                    tipo = "Imagen PNG";
                    break;
                case ".GIF":
                    tipo = "Imagen GIF";
                    break;
                case ".MP4":
                    tipo = "Vídeo MP4";
                    break;
                case ".MP3":
                    tipo = "Sonido en formato MP3";
                    break;
                case ".PUB":
                    tipo = "Documento de Microsoft Publisher";
                    break;
                case ".CONFIG":
                    tipo = "XML Archivo de Configuracion";
                    break;
                case ".SQL":
                    tipo = "SQL Server Query File";
                    break;
                case ".ZIP":
                    tipo = "Archivo zip";
                    break;
                case ".RAR":
                    tipo = "Archivo rar";
                    break;
                case ".7Z":
                    tipo = "Archivo 7z";
                    break;
                case ".WMV":
                    tipo = "Archivo de audio o vídeo de Windows Media";
                    break;
                case ".BD":
                    tipo = "Archivo Base de Datos";
                    break;
                case ".DLL":
                    tipo = "Extensión de la aplicación";
                    break;
                case ".CSS":
                    tipo = "Archivo CSS";
                    break;
                case ".EXE":
                    tipo = "Aplicación";
                    break;
                case ".MSI":
                    tipo = "Paquete de Windows Installer";
                    break;
                case ".AVI":
                    tipo = "Clip de vídeo";
                    break;
                case ".JPEG":
                    tipo = "Imagen JPEG";
                    break;
                case ".ODT":
                    tipo = "Texto de OpenDocument";
                    break;
                case ".ODS":
                    tipo = "Hoja de cálculo OpenDocumen";
                    break;
                case ".ODP":
                    tipo = "Presentación de OpenDocument";
                    break;
                case ".ODG":
                    tipo = "Dibujo de Open Office";
                    break;
                case ".MBD":
                    tipo = "Archivo de Multimedia";
                    break;
                case ".MDB":
                    tipo = "Archivo Microsoft Access";
                    break;
                case ".RTF":
                    tipo = "Documento de Texto Rico";
                    break;
                case ".ICO":
                    tipo = "Icono";
                    break;
                case ".ASPX":
                    tipo = "ASP.NET Server Page";
                    break;
                case ".MASTER":
                    tipo = "ASP.NET Master Page";
                    break;
                case ".XML":
                    tipo = "Documento XML";
                    break;
                case ".INF":
                    tipo = "Información sobre la instalación";
                    break;
                case ".HTM":
                    tipo = "Documento HTML";
                    break;
                case ".LOG":
                    tipo = "Documento de texto";
                    break;
                case ".OGG":
                    tipo = "Archivo OGG";
                    break;
                case ".PPS":
                    tipo = "Presentación con diapositivas de Microsoft PowerPoint 97-2003";
                    break;
                case ".BMP":
                    tipo = "Imagen de mapa de bits";
                    break;
                case ".TIF":
                    tipo = "Imagen TIFF";
                    break;
                case ".WAV":
                    tipo = "Archivo de sonido";
                    break;
                case ".WMA":
                    tipo = "Archivo de audio de Windows Media";
                    break;
                case ".ACE":
                    tipo = "Archivo ACE";
                    break;
                case ".AI":
                    tipo = "Documento Adobe Ilustrator";
                    break;
                case ".AIF":
                    tipo = "Archivo de intercambio de audio";
                    break;
                case ".BIN":
                    tipo = "Archivo binario";
                    break;
                case ".BUP":
                    tipo = "Archivo de copia de seguridad";
                    break;
                case ".CAB":
                    tipo = "Microsoft Windows archivo comprimido";
                    break;
                case ".CDA":
                    tipo = "Atajo CD Audio Track";
                    break;
                case ".CDR":
                    tipo = "Corel Draw Vector";
                    break;
                case ".DIVX":
                    tipo = "Vídeo DivX";
                    break;
                case ".DMG":
                    tipo = "Imagen de disco";
                    break;
                case ".DWG":
                    tipo = "AutoCAD DWG";
                    break;
                case ".EML":
                    tipo = "Mensaje de correo electrónico";
                    break;
                case ".FLA":
                    tipo = "Adobe Flash";
                    break;
                case ".FLV":
                    tipo = "Flash Video";
                    break;
                case ".JAR":
                    tipo = "Archivo Java";
                    break;
                case ".LNK":
                    tipo = "Acceso directo";
                    break;
                case ".MOV":
                    tipo = "QuickTime";
                    break;
                case ".MP2":
                    tipo = "MPEG-1 Audio Layer II";
                    break;
                case ".MSWMM":
                    tipo = "Windows Movie Maker Archivo de Proyecto";
                    break;
                case ".PS":
                    tipo = "Archivo PostScript";
                    break;
                case ".PSD":
                    tipo = "Documento Photoshop";
                    break;
                case ".PST":
                    tipo = "Microsoft Outlook emails";
                    break;
                case ".RM":
                    tipo = "Archivo RealMedia";
                    break;
                case ".TORRENT":
                    tipo = "BitTorrent";
                    break;
                case ".TTF":
                    tipo = "Tipos de Fuente";
                    break;
                case ".VCD":
                    tipo = "Virtual CD-ROM CD Archivo de imagen";
                    break;
                case ".VOB":
                    tipo = "DVD-Video Object";
                    break;
                case ".WPD":
                    tipo = "WordPerfect Document";
                    break;
                case ".WPS":
                    tipo = "Documento de texto";
                    break;
                case ".BAK":
                    tipo = "Backup";
                    break;
                case ".XLW":
                    tipo = "Area de trabajo de Microsoft Excel";
                    break;
                case ".XLT":
                    tipo = "Plantilla de Microsoft Excel ";
                    break;
                case ".XLM":
                    tipo = "Macro de Microsoft Excel";
                    break;
                case ".XLK":
                    tipo = "Archivo de copia de seguridad de Microsoft Exce";
                    break;
                case ".XLD":
                    tipo = "Hoja de cuadros de diálogo de Microsoft Excel";
                    break;
                case ".IDE":
                    tipo = "Development environment configuration";
                    break;
            }
            if (tipo == "" && ext != "")
            {
                tipo = "Archivo " + ext.Substring(1).ToUpper();
            }
            if (ext == "")
            {
                tipo = "Desconocido";
            }
            return tipo;
        }

        //** FIN DE VALIDAR EXTENCIONES Y TIPO DE ARCHIVOS
        //
        //
        // BOTON PARA LIMPIAR CASILLA
        private void button2_Click(object sender, EventArgs e)
        {
            tbConsecutivo.Text = "";
            tbRadicado.Text = "";
            tbExpediente.Text = "";
            tbTomo.Text = "";
        }
        //** FIN DE BOTON PARA LIMPIAR CASILLA
        //
        //
        //ACTUALIZAR PROPIEDADES DE UNIDAD
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                GetFiles(@"" + comboBox1.SelectedValue + "", "*");
                lblCont.Text = "";
                PropiedadesDatos();
            }
        }

        private void tbTomo_TextChanged(object sender, EventArgs e)
        {
            string fecha = "";
            if (tbExpediente.Text != "" && tbTomo.Text != "")
            {
                DataTable dtDatos = ObtenerDatos(tbExpediente.Text, tbTomo.Text);
                if (dtDatos.Rows.Count > 0)
                {
                    tbEmpresa.Text = dtDatos.Rows[0][0].ToString();
                    tbSector.Text = dtDatos.Rows[0][1].ToString();
                    tbRadicado.Text = dtDatos.Rows[0][2].ToString();
                    fecha = dtDatos.Rows[0][3].ToString();
                    tbFechaRadicado.Text = fecha;
                    List<String> medios = new List<String>();

                    medios.Add("D.D");
                    medios.Add("DVD");
                    medios.Add("CD");
                    medios.Add("USB");
                    medios.Add("DISKETTE");
                    medios.Add("CASETTE");
                    medios.Add("VHS/BETA");
                    medios.Add("BLU-RAY");

                    cbTipoMedio.DisplayMember = "TipoMedio";
                    cbTipoMedio.ValueMember = "Key";
                    cbTipoMedio.DataSource = medios;
                    rtbDescripcion.Text = dtDatos.Rows[0][4].ToString();
                }
                else
                {
                    tbEmpresa.Text = "";
                    tbSector.Text = "";
                    tbRadicado.Text = "";
                    fecha ="";
                    tbFechaRadicado.Text = "";
                    rtbDescripcion.Text = "";
                }

                if (cbTipoMedio.SelectedItem == null)
                {
                    List<String> medios = new List<String>();

                    medios.Add("D.D");
                    medios.Add("DVD");
                    medios.Add("CD");
                    medios.Add("USB");
                    medios.Add("DISKETTE");
                    medios.Add("CASETTE");
                    medios.Add("VHS/BETA");
                    medios.Add("BLU-RAY");

                    cbTipoMedio.DisplayMember = "TipoMedio";
                    cbTipoMedio.ValueMember = "Key";
                    cbTipoMedio.DataSource = medios;
                }

                if (tbEmpresa.Text == "" || tbSector.Text == "" || tbRadicado.Text == "" || fecha == "" || rtbDescripcion.Text == "" || comboBox1.SelectedValue == null)
                {
                    rtbObservacion.ReadOnly = false;
                }
            }
            else
            {
                tbExpediente.Text = "";
                tbEmpresa.Text = "";
                tbSector.Text = "";
                rtbDescripcion.Text = "";
                rtbObservacion.Text = "";
                tbRadicado.Text = "";
                DateTime thisDay = DateTime.Today;
                string fechaHoy = thisDay.ToString("d");
                tbFechaRadicado.Text = "Sin fecha";
                cbTipoMedio.DisplayMember = "TipoMedio";
                cbTipoMedio.ValueMember = "Key";
                cbTipoMedio.DataSource = null;
                rtbObservacion.ReadOnly = true;
            }
        }

        private void tbExpediente_TextChanged(object sender, EventArgs e)
        {
            string fecha = "";
            if (tbExpediente.Text != "" && tbTomo.Text != "")
            {

                DataTable dtDatos = ObtenerDatos(tbExpediente.Text, tbTomo.Text);
                if (dtDatos.Rows.Count > 0)
                {
                    tbEmpresa.Text = dtDatos.Rows[0][0].ToString();
                    tbSector.Text = dtDatos.Rows[0][1].ToString();
                    tbRadicado.Text = dtDatos.Rows[0][2].ToString();
                    fecha = dtDatos.Rows[0][3].ToString();
                    tbFechaRadicado.Text = fecha;
                    List<String> medios = new List<String>();

                    medios.Add("D.D");
                    medios.Add("DVD");
                    medios.Add("CD");
                    medios.Add("USB");
                    medios.Add("DISKETTE");
                    medios.Add("CASETTE");
                    medios.Add("VHS/BETA");
                    medios.Add("BLU-RAY");

                    cbTipoMedio.DisplayMember = "TipoMedio";
                    cbTipoMedio.ValueMember = "Key";
                    cbTipoMedio.DataSource = medios;
                    rtbDescripcion.Text = dtDatos.Rows[0][4].ToString();
                }
                else
                {
                    tbEmpresa.Text = "";
                    tbSector.Text = "";
                    tbRadicado.Text = "";
                    fecha = "";
                    tbFechaRadicado.Text = "";
                    rtbDescripcion.Text = "";
                }
                if (tbEmpresa.Text == "" || tbSector.Text == "" || tbRadicado.Text == "" || fecha == "" || rtbDescripcion.Text == "" || comboBox1.SelectedValue == null)
                {
                    rtbObservacion.ReadOnly = false;
                }
            }
            else
            {
                tbTomo.Text = "";
                tbEmpresa.Text = "";
                tbSector.Text = "";
                rtbDescripcion.Text = "";
                rtbObservacion.Text = "";
                tbRadicado.Text = "";
                DateTime thisDay = DateTime.Today;
                string fechaHoy = thisDay.ToString("d");
                tbFechaRadicado.Text = "Sin Fecha";
                cbTipoMedio.DisplayMember = "TipoMedio";
                cbTipoMedio.ValueMember = "Key";
                cbTipoMedio.DataSource = null;
                rtbObservacion.ReadOnly = true;
            }
        }

        private void tbFechaRadicado_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (!e.IsValidInput)
            {
                toolTip1.ToolTipTitle = "Fecha Invalida";
                toolTip1.Show("Verifique la fecha(dd/mm/yyyy)", tbFechaRadicado, 0, -20, 5000);
                rtbObservacion.ReadOnly = false;
            }
            else
            {
                rtbObservacion.ReadOnly = true;
            }
        }

        private void referenciaCruzadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MediosMagneticos.Reporte rpt = new MediosMagneticos.Reporte();
            rpt.Show();
        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Seleccione la ruta";
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            string ruta = string.Empty;

            try
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    ruta = fbd.SelectedPath;
                    DataTable dt = ObtenerResultado();
                    StreamWriter sw = new StreamWriter(ruta + "\\ReporteMediosMagneticos.txt");
                    string linea = "";
                    lblCont.Text = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lblCont.Text = "Lineas exportadas: " + (i + 1) + " de " + dt.Rows.Count;
                        Application.DoEvents();
                        linea = "";
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            linea += dt.Rows[i][j].ToString() + "|";
                        }
                        sw.WriteLine(linea);
                    }
                    sw.Flush();
                    sw.Dispose();
                    sw.Close();
                    sw = null;
                    MessageBox.Show("El archivo fue exportado exitosamente a documentos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        //** FIN DE ACTUALIZAR PROPIEDADES DE UNIDAD
    }
}
