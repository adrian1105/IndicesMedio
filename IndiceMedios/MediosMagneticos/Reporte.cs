using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;


namespace MediosMagneticos
{
    public partial class Reporte : Form
    {
        DataTable reporte = new DataTable();
        //string ConnString = "Server=ASD-PC\\SQLEXPRESS;Database=ANLA;Integrated Security=SSPI;";
        string ConnString = "Server=C46ASDDESAP2048;Database=ANLA;Integrated Security=SSPI;";
        public Reporte()
        {
            InitializeComponent();
            ReporteReferenciaCruzada();       
        }

        private void Reporte_Load(object sender, EventArgs e)
        {           
            string exportOption = "EXCELOPENXML";
            RenderingExtension extension = rvReporte.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
            if (extension != null)
            {
                System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                fieldInfo.SetValue(extension, false);
            }
            string exportOptionWord = "WORDOPENXML";
            RenderingExtension extensionWord = rvReporte.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOptionWord, StringComparison.CurrentCultureIgnoreCase));
            if (extensionWord != null)
            {
                System.Reflection.FieldInfo fieldInfo = extensionWord.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                fieldInfo.SetValue(extensionWord, false);
            }
            this.rvReporte.RefreshReport();
        }

        public DataTable ObtenerReporte()
        {
            DataTable dt = new DataTable();
            string SqlString = "SELECT CodigoMedio.Expediente, ProyectosExpedientes.Proyecto, CASE CodigoMedio.Radicado  WHEN '' THEN 'SIN RADICADO' ELSE CodigoMedio.Radicado END AS Radicado,CASE CodigoMedio.Sector  WHEN '' THEN 'SIN SECTOR' ELSE CodigoMedio.Sector END AS Sector,CASE CodigoMedio.FechaRadicado  WHEN '' THEN 'SIN FECHA RADICADO' ELSE CodigoMedio.FechaRadicado END AS FechaRadicado,CASE CAST(CodigoMedio.Descripcion AS NVARCHAR(MAX))   WHEN '' THEN 'SIN DESCRIPCION' ELSE CodigoMedio.Descripcion END AS Descripcion,CASE CAST(CodigoMedio.Empresa AS NVARCHAR(MAX))  WHEN '' THEN 'SIN NOMBRE EMPRESA' ELSE CodigoMedio.Empresa END AS Empresa,CASE CodigoMedio.TipoMedio WHEN 'CD' THEN '1' ELSE '' END AS contCD, CASE CodigoMedio.TipoMedio WHEN 'DISKETTE' THEN '1' ELSE '' END AS contDTT,  CASE CodigoMedio.TipoMedio WHEN 'DVD' THEN '1' WHEN 'USB' THEN '1' WHEN 'D.D' THEN '1' WHEN 'BLU-RAY' THEN '1' WHEN 'CASETTE' THEN '1' WHEN 'VHS/BETA' THEN '1' ELSE '' END AS contOtros FROM  CodigoMedio INNER JOIN ProyectosExpedientes ON CodigoMedio.Expediente = ProyectosExpedientes.Expediente ORDER BY CodigoMedio.Expediente";

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(SqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;
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

        public void ReporteReferenciaCruzada()
        {
            reporte = ObtenerReporte();
            rvReporte.LocalReport.ReportEmbeddedResource = "MediosMagneticos.ReportePrincipal.rdlc";
            rvReporte.LocalReport.DataSources.Clear();
            Microsoft.Reporting.WinForms.ReportDataSource datasource = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", reporte);
            rvReporte.LocalReport.DataSources.Add(datasource);
            rvReporte.LocalReport.Refresh();
        }
    }
}
