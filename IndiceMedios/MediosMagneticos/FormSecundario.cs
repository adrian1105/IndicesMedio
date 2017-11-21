using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;
using System.Data.SqlClient;

namespace IndiceMedios
{
    public partial class FormSecundario : Form
    {
        //string ConnString = "Server=ASD-PC\\SQLEXPRESS;Database=ANLA;Integrated Security=SSPI;";
        string ConnString = "Server=C46ASDDESAP2048;Database=ANLA;Integrated Security=SSPI;";
        string var = string.Empty;
        public FormSecundario(string exp, string tomo)
        {
            InitializeComponent();
            dgvDatosGuardado.DataSource = ObtenerResultado(exp, tomo);
        }

        public DataTable ObtenerResultado(string expediente, string tomo)
        {
            DataTable obtenerConciliador = new DataTable();
            string SqlString = "SELECT d.Expediente, d.Tomo, Empresa, Sector, TipoMedio, Descripcion,d.ConsecutivoUnidad,Radicado,FechaRadicado,Nombre,Extencion,Tamaño,Tipo,FechaModificacion,FechaCreacion, Ubicacion,Observacion FROM CodigoMedio d,PropiedadesMedios c where d.Expediente =@expediente and  d.Tomo =@tomo and c.Expediente =@expediente and  c.Tomo =@tomo";

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(SqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@expediente", expediente);
                    cmd.Parameters.AddWithValue("@tomo", tomo);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter SqlDataAdapter = new SqlDataAdapter(cmd);
                    SqlDataAdapter.Fill(obtenerConciliador);
                }
            }
            return obtenerConciliador;
        }
    }
}
