namespace IndiceMedios
{
    partial class FormPrincipal
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
            this.tbConsecutivo = new System.Windows.Forms.TextBox();
            this.tbRadicado = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dgvDatosUnidad = new System.Windows.Forms.DataGridView();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.tbExpediente = new System.Windows.Forms.TextBox();
            this.tbTomo = new System.Windows.Forms.TextBox();
            this.lblExpediente = new System.Windows.Forms.Label();
            this.lblTomo = new System.Windows.Forms.Label();
            this.tbEmpresa = new System.Windows.Forms.TextBox();
            this.tbSector = new System.Windows.Forms.TextBox();
            this.rtbDescripcion = new System.Windows.Forms.RichTextBox();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.lblSector = new System.Windows.Forms.Label();
            this.lblTipoMedio = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.cbTipoMedio = new System.Windows.Forms.ComboBox();
            this.rtbObservacion = new System.Windows.Forms.RichTextBox();
            this.lblObservacion = new System.Windows.Forms.Label();
            this.tbFechaRadicado = new System.Windows.Forms.MaskedTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblCont = new System.Windows.Forms.Label();
            this.msOpciones = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.referenciaCruzadaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosUnidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.msOpciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbConsecutivo
            // 
            this.tbConsecutivo.Location = new System.Drawing.Point(282, 102);
            this.tbConsecutivo.Name = "tbConsecutivo";
            this.tbConsecutivo.Size = new System.Drawing.Size(100, 20);
            this.tbConsecutivo.TabIndex = 3;
            // 
            // tbRadicado
            // 
            this.tbRadicado.Location = new System.Drawing.Point(21, 141);
            this.tbRadicado.Name = "tbRadicado";
            this.tbRadicado.Size = new System.Drawing.Size(100, 20);
            this.tbRadicado.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(279, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Consecutivo Unidad";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Radicado";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(153, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Fecha Radicado";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(150, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Verificar Unidad";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(21, 47);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 21);
            this.comboBox1.TabIndex = 20;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dgvDatosUnidad
            // 
            this.dgvDatosUnidad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatosUnidad.Location = new System.Drawing.Point(1, 353);
            this.dgvDatosUnidad.Name = "dgvDatosUnidad";
            this.dgvDatosUnidad.Size = new System.Drawing.Size(661, 294);
            this.dgvDatosUnidad.TabIndex = 14;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(292, 317);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(238, 50);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(474, 337);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(110, 13);
            this.lblInfo.TabIndex = 20;
            this.lblInfo.Text = "Cantidad de archivos:";
            this.lblInfo.Visible = false;
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(580, 337);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(0, 13);
            this.lblCantidad.TabIndex = 21;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(282, 45);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Limpiar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(385, 563);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(240, 84);
            this.dataGridView2.TabIndex = 19;
            this.dataGridView2.Visible = false;
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // tbExpediente
            // 
            this.tbExpediente.Location = new System.Drawing.Point(21, 102);
            this.tbExpediente.Name = "tbExpediente";
            this.tbExpediente.Size = new System.Drawing.Size(100, 20);
            this.tbExpediente.TabIndex = 0;
            this.tbExpediente.TextChanged += new System.EventHandler(this.tbExpediente_TextChanged);
            // 
            // tbTomo
            // 
            this.tbTomo.Location = new System.Drawing.Point(153, 102);
            this.tbTomo.Name = "tbTomo";
            this.tbTomo.Size = new System.Drawing.Size(100, 20);
            this.tbTomo.TabIndex = 2;
            this.tbTomo.TextChanged += new System.EventHandler(this.tbTomo_TextChanged);
            // 
            // lblExpediente
            // 
            this.lblExpediente.AutoSize = true;
            this.lblExpediente.Location = new System.Drawing.Point(32, 86);
            this.lblExpediente.Name = "lblExpediente";
            this.lblExpediente.Size = new System.Drawing.Size(60, 13);
            this.lblExpediente.TabIndex = 26;
            this.lblExpediente.Text = "Expediente";
            // 
            // lblTomo
            // 
            this.lblTomo.AutoSize = true;
            this.lblTomo.Location = new System.Drawing.Point(183, 86);
            this.lblTomo.Name = "lblTomo";
            this.lblTomo.Size = new System.Drawing.Size(34, 13);
            this.lblTomo.TabIndex = 27;
            this.lblTomo.Text = "Tomo";
            // 
            // tbEmpresa
            // 
            this.tbEmpresa.Location = new System.Drawing.Point(282, 141);
            this.tbEmpresa.Name = "tbEmpresa";
            this.tbEmpresa.Size = new System.Drawing.Size(100, 20);
            this.tbEmpresa.TabIndex = 4;
            // 
            // tbSector
            // 
            this.tbSector.Location = new System.Drawing.Point(24, 181);
            this.tbSector.Name = "tbSector";
            this.tbSector.Size = new System.Drawing.Size(100, 20);
            this.tbSector.TabIndex = 5;
            // 
            // rtbDescripcion
            // 
            this.rtbDescripcion.Location = new System.Drawing.Point(172, 233);
            this.rtbDescripcion.Name = "rtbDescripcion";
            this.rtbDescripcion.Size = new System.Drawing.Size(329, 78);
            this.rtbDescripcion.TabIndex = 9;
            this.rtbDescripcion.Text = "";
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.Location = new System.Drawing.Point(302, 125);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(48, 13);
            this.lblEmpresa.TabIndex = 32;
            this.lblEmpresa.Text = "Empresa";
            // 
            // lblSector
            // 
            this.lblSector.AutoSize = true;
            this.lblSector.Location = new System.Drawing.Point(42, 165);
            this.lblSector.Name = "lblSector";
            this.lblSector.Size = new System.Drawing.Size(38, 13);
            this.lblSector.TabIndex = 33;
            this.lblSector.Text = "Sector";
            // 
            // lblTipoMedio
            // 
            this.lblTipoMedio.AutoSize = true;
            this.lblTipoMedio.Location = new System.Drawing.Point(168, 165);
            this.lblTipoMedio.Name = "lblTipoMedio";
            this.lblTipoMedio.Size = new System.Drawing.Size(60, 13);
            this.lblTipoMedio.TabIndex = 34;
            this.lblTipoMedio.Text = "Tipo Medio";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(294, 217);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(63, 13);
            this.lblDescripcion.TabIndex = 35;
            this.lblDescripcion.Text = "Descripción";
            // 
            // cbTipoMedio
            // 
            this.cbTipoMedio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoMedio.FormattingEnabled = true;
            this.cbTipoMedio.Location = new System.Drawing.Point(153, 180);
            this.cbTipoMedio.Name = "cbTipoMedio";
            this.cbTipoMedio.Size = new System.Drawing.Size(100, 21);
            this.cbTipoMedio.TabIndex = 6;
            // 
            // rtbObservacion
            // 
            this.rtbObservacion.BackColor = System.Drawing.Color.White;
            this.rtbObservacion.Location = new System.Drawing.Point(405, 102);
            this.rtbObservacion.Name = "rtbObservacion";
            this.rtbObservacion.Size = new System.Drawing.Size(228, 100);
            this.rtbObservacion.TabIndex = 36;
            this.rtbObservacion.Text = "";
            // 
            // lblObservacion
            // 
            this.lblObservacion.AutoSize = true;
            this.lblObservacion.Location = new System.Drawing.Point(486, 86);
            this.lblObservacion.Name = "lblObservacion";
            this.lblObservacion.Size = new System.Drawing.Size(67, 13);
            this.lblObservacion.TabIndex = 37;
            this.lblObservacion.Text = "Observacion";
            // 
            // tbFechaRadicado
            // 
            this.tbFechaRadicado.Location = new System.Drawing.Point(153, 141);
            this.tbFechaRadicado.Mask = "00/00/0000";
            this.tbFechaRadicado.Name = "tbFechaRadicado";
            this.tbFechaRadicado.Size = new System.Drawing.Size(100, 20);
            this.tbFechaRadicado.TabIndex = 39;
            this.tbFechaRadicado.ValidatingType = typeof(System.DateTime);
            this.tbFechaRadicado.TypeValidationCompleted += new System.Windows.Forms.TypeValidationEventHandler(this.tbFechaRadicado_TypeValidationCompleted);
            // 
            // lblCont
            // 
            this.lblCont.AutoSize = true;
            this.lblCont.Location = new System.Drawing.Point(412, 50);
            this.lblCont.Name = "lblCont";
            this.lblCont.Size = new System.Drawing.Size(0, 13);
            this.lblCont.TabIndex = 40;
            // 
            // msOpciones
            // 
            this.msOpciones.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.msOpciones.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.msOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.msOpciones.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.msOpciones.Location = new System.Drawing.Point(0, 0);
            this.msOpciones.Name = "msOpciones";
            this.msOpciones.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.msOpciones.Size = new System.Drawing.Size(663, 24);
            this.msOpciones.TabIndex = 42;
            this.msOpciones.Text = "msOpciones";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.referenciaCruzadaToolStripMenuItem,
            this.exportarToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // referenciaCruzadaToolStripMenuItem
            // 
            this.referenciaCruzadaToolStripMenuItem.Name = "referenciaCruzadaToolStripMenuItem";
            this.referenciaCruzadaToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.referenciaCruzadaToolStripMenuItem.Text = "Referencia Cruzada";
            this.referenciaCruzadaToolStripMenuItem.Click += new System.EventHandler(this.referenciaCruzadaToolStripMenuItem_Click);
            // 
            // exportarToolStripMenuItem
            // 
            this.exportarToolStripMenuItem.Name = "exportarToolStripMenuItem";
            this.exportarToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.exportarToolStripMenuItem.Text = "Exportar";
            this.exportarToolStripMenuItem.Click += new System.EventHandler(this.exportarToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(663, 657);
            this.Controls.Add(this.lblCont);
            this.Controls.Add(this.tbFechaRadicado);
            this.Controls.Add(this.lblObservacion);
            this.Controls.Add(this.rtbObservacion);
            this.Controls.Add(this.cbTipoMedio);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblTipoMedio);
            this.Controls.Add(this.lblSector);
            this.Controls.Add(this.lblEmpresa);
            this.Controls.Add(this.rtbDescripcion);
            this.Controls.Add(this.tbSector);
            this.Controls.Add(this.tbEmpresa);
            this.Controls.Add(this.lblTomo);
            this.Controls.Add(this.lblExpediente);
            this.Controls.Add(this.tbTomo);
            this.Controls.Add(this.tbExpediente);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dgvDatosUnidad);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbRadicado);
            this.Controls.Add(this.tbConsecutivo);
            this.Controls.Add(this.msOpciones);
            this.MainMenuStrip = this.msOpciones;
            this.MaximizeBox = false;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Indice de Medios Magneticos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosUnidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.msOpciones.ResumeLayout(false);
            this.msOpciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbConsecutivo;
        private System.Windows.Forms.TextBox tbRadicado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dgvDatosUnidad;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.TextBox tbExpediente;
        private System.Windows.Forms.TextBox tbTomo;
        private System.Windows.Forms.Label lblExpediente;
        private System.Windows.Forms.Label lblTomo;
        private System.Windows.Forms.TextBox tbEmpresa;
        private System.Windows.Forms.TextBox tbSector;
        private System.Windows.Forms.RichTextBox rtbDescripcion;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.Label lblSector;
        private System.Windows.Forms.Label lblTipoMedio;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.ComboBox cbTipoMedio;
        private System.Windows.Forms.RichTextBox rtbObservacion;
        private System.Windows.Forms.Label lblObservacion;
        private System.Windows.Forms.MaskedTextBox tbFechaRadicado;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblCont;
        private System.Windows.Forms.MenuStrip msOpciones;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem referenciaCruzadaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
    }
}

