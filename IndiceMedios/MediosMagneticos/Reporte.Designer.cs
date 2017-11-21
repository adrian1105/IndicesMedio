namespace MediosMagneticos
{
    partial class Reporte
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.CodigoMedio1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dtReporteProyectoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dtReporteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rvReporte = new Microsoft.Reporting.WinForms.ReportViewer();
            this.CodigoMedioBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.CodigoMedio1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtReporteProyectoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtReporteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CodigoMedioBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dtReporteProyectoBindingSource
            // 
            this.dtReporteProyectoBindingSource.DataMember = "dtReporteProyecto";
            // 
            // dtReporteBindingSource
            // 
            this.dtReporteBindingSource.DataMember = "dtReporte";
            // 
            // rvReporte
            // 
            this.rvReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.CodigoMedio1BindingSource;
            this.rvReporte.LocalReport.DataSources.Add(reportDataSource1);
            this.rvReporte.LocalReport.ReportEmbeddedResource = "MediosMagneticos.ReportePrincipal.rdlc";
            this.rvReporte.Location = new System.Drawing.Point(0, 0);
            this.rvReporte.Name = "rvReporte";
            this.rvReporte.Size = new System.Drawing.Size(820, 567);
            this.rvReporte.TabIndex = 0;
            // 
            // Reporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 567);
            this.Controls.Add(this.rvReporte);
            this.MaximizeBox = false;
            this.Name = "Reporte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte";
            this.Load += new System.EventHandler(this.Reporte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CodigoMedio1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtReporteProyectoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtReporteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CodigoMedioBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource dtReporteBindingSource;
        private System.Windows.Forms.BindingSource dtReporteProyectoBindingSource;
        private System.Windows.Forms.BindingSource CodigoMedioBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer rvReporte;
        private System.Windows.Forms.BindingSource CodigoMedio1BindingSource;
    }
}