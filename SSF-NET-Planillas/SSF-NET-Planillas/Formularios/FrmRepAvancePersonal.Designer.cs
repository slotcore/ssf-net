namespace SSF_NET_Planillas.Formularios
{
    partial class FrmRepAvancePersonal
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
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.BtnBuscar = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.BtnExportarExcel = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.DgvResultado = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.kryptonHeaderGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.BtnAgregarEmpleado = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.BtnEliminarEmpleado = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.productoRecetaLineaTareaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TxtProducto = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.BtnBuscarProducto = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonDataGridView2 = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.cnumdocideDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cape1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cape2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnom1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empleadoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DtpFechaFin = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.DtpFechaInicio = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvResultado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).BeginInit();
            this.kryptonHeaderGroup2.Panel.SuspendLayout();
            this.kryptonHeaderGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productoRecetaLineaTareaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empleadoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonHeaderGroup1.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.BtnBuscar,
            this.BtnExportarExcel});
            this.kryptonHeaderGroup1.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlGroupBox;
            this.kryptonHeaderGroup1.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlGroupBox;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Calendar;
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(3, 180);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.DgvResultado);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(1287, 511);
            this.kryptonHeaderGroup1.TabIndex = 42;
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "Resultado";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = null;
            // 
            // BtnBuscar
            // 
            this.BtnBuscar.Image = global::SSF_NET_Planillas.Properties.Resources.lookup_reference_16x16;
            this.BtnBuscar.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Standalone;
            this.BtnBuscar.Text = "Buscar";
            this.BtnBuscar.UniqueName = "BB40F94D01F746BD9689517B4896CDAA";
            this.BtnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // BtnExportarExcel
            // 
            this.BtnExportarExcel.Image = global::SSF_NET_Planillas.Properties.Resources.exporttoxls_16x16;
            this.BtnExportarExcel.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Standalone;
            this.BtnExportarExcel.Text = "Exportar Excel";
            this.BtnExportarExcel.UniqueName = "05562010624B4341B480C7C538C84DDA";
            this.BtnExportarExcel.Click += new System.EventHandler(this.BtnExportarExcel_Click);
            // 
            // DgvResultado
            // 
            this.DgvResultado.AllowUserToAddRows = false;
            this.DgvResultado.AllowUserToDeleteRows = false;
            this.DgvResultado.AllowUserToOrderColumns = true;
            this.DgvResultado.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvResultado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvResultado.Location = new System.Drawing.Point(0, 0);
            this.DgvResultado.Name = "DgvResultado";
            this.DgvResultado.ReadOnly = true;
            this.DgvResultado.RowHeadersWidth = 20;
            this.DgvResultado.RowTemplate.Height = 24;
            this.DgvResultado.Size = new System.Drawing.Size(1283, 475);
            this.DgvResultado.TabIndex = 0;
            // 
            // kryptonHeaderGroup2
            // 
            this.kryptonHeaderGroup2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonHeaderGroup2.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.BtnAgregarEmpleado,
            this.BtnEliminarEmpleado});
            this.kryptonHeaderGroup2.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Calendar;
            this.kryptonHeaderGroup2.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup2.Location = new System.Drawing.Point(3, 2);
            this.kryptonHeaderGroup2.Name = "kryptonHeaderGroup2";
            // 
            // kryptonHeaderGroup2.Panel
            // 
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.kryptonPanel1);
            this.kryptonHeaderGroup2.Size = new System.Drawing.Size(1287, 177);
            this.kryptonHeaderGroup2.TabIndex = 43;
            this.kryptonHeaderGroup2.ValuesPrimary.Heading = "Filtros";
            this.kryptonHeaderGroup2.ValuesPrimary.Image = null;
            // 
            // BtnAgregarEmpleado
            // 
            this.BtnAgregarEmpleado.Image = global::SSF_NET_Planillas.Properties.Resources.bodepartment_16x16;
            this.BtnAgregarEmpleado.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Standalone;
            this.BtnAgregarEmpleado.Text = "Agregar Empleado";
            this.BtnAgregarEmpleado.UniqueName = "66E90F087CB241747C87D692DD1339F9";
            this.BtnAgregarEmpleado.Click += new System.EventHandler(this.BtnAgregarEmpleado_Click);
            // 
            // BtnEliminarEmpleado
            // 
            this.BtnEliminarEmpleado.Image = global::SSF_NET_Planillas.Properties.Resources.trash_16x16;
            this.BtnEliminarEmpleado.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Standalone;
            this.BtnEliminarEmpleado.Text = "Eliminar Empleado";
            this.BtnEliminarEmpleado.UniqueName = "7D11F81DB6FC496B949A37481D413E2F";
            this.BtnEliminarEmpleado.Click += new System.EventHandler(this.BtnEliminarEmpleado_Click);
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.comboBox1);
            this.kryptonPanel1.Controls.Add(this.TxtProducto);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.DtpFechaFin);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.DtpFechaInicio);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1285, 142);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.productoRecetaLineaTareaBindingSource;
            this.comboBox1.DisplayMember = "c_destar";
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(89, 99);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(355, 24);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.ValueMember = "n_idtar";
            // 
            // productoRecetaLineaTareaBindingSource
            // 
            this.productoRecetaLineaTareaBindingSource.DataSource = typeof(SIAC_DATOS.Models.Produccion.ProductoRecetaLineaTarea);
            // 
            // TxtProducto
            // 
            this.TxtProducto.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.BtnBuscarProducto});
            this.TxtProducto.Location = new System.Drawing.Point(89, 63);
            this.TxtProducto.Name = "TxtProducto";
            this.TxtProducto.Size = new System.Drawing.Size(355, 30);
            this.TxtProducto.TabIndex = 9;
            // 
            // BtnBuscarProducto
            // 
            this.BtnBuscarProducto.Image = global::SSF_NET_Planillas.Properties.Resources.lookup_reference_16x16;
            this.BtnBuscarProducto.Text = "Buscar";
            this.BtnBuscarProducto.UniqueName = "8ABD91CBB0FB4B46AEA8ECE6AD1409AF";
            this.BtnBuscarProducto.Click += new System.EventHandler(this.BtnBuscarProducto_Click);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(8, 98);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(49, 24);
            this.kryptonLabel4.TabIndex = 8;
            this.kryptonLabel4.Values.Text = "Tarea";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(8, 66);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(74, 24);
            this.kryptonLabel3.TabIndex = 6;
            this.kryptonLabel3.Values.Text = "Producto";
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(494, 3);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonDataGridView2);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(710, 130);
            this.kryptonGroupBox1.TabIndex = 4;
            this.kryptonGroupBox1.Values.Heading = "Empleados";
            this.kryptonGroupBox1.Values.Image = global::SSF_NET_Planillas.Properties.Resources.bodepartment_16x16;
            // 
            // kryptonDataGridView2
            // 
            this.kryptonDataGridView2.AllowUserToAddRows = false;
            this.kryptonDataGridView2.AllowUserToDeleteRows = false;
            this.kryptonDataGridView2.AllowUserToOrderColumns = true;
            this.kryptonDataGridView2.AutoGenerateColumns = false;
            this.kryptonDataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.kryptonDataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kryptonDataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cnumdocideDataGridViewTextBoxColumn,
            this.cape1DataGridViewTextBoxColumn,
            this.cape2DataGridViewTextBoxColumn,
            this.cnom1DataGridViewTextBoxColumn});
            this.kryptonDataGridView2.DataSource = this.empleadoBindingSource;
            this.kryptonDataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonDataGridView2.Location = new System.Drawing.Point(0, 0);
            this.kryptonDataGridView2.Name = "kryptonDataGridView2";
            this.kryptonDataGridView2.ReadOnly = true;
            this.kryptonDataGridView2.RowHeadersWidth = 20;
            this.kryptonDataGridView2.RowTemplate.Height = 24;
            this.kryptonDataGridView2.Size = new System.Drawing.Size(706, 102);
            this.kryptonDataGridView2.TabIndex = 0;
            // 
            // cnumdocideDataGridViewTextBoxColumn
            // 
            this.cnumdocideDataGridViewTextBoxColumn.DataPropertyName = "c_numdocide";
            this.cnumdocideDataGridViewTextBoxColumn.HeaderText = "DNI";
            this.cnumdocideDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.cnumdocideDataGridViewTextBoxColumn.Name = "cnumdocideDataGridViewTextBoxColumn";
            this.cnumdocideDataGridViewTextBoxColumn.ReadOnly = true;
            this.cnumdocideDataGridViewTextBoxColumn.Width = 68;
            // 
            // cape1DataGridViewTextBoxColumn
            // 
            this.cape1DataGridViewTextBoxColumn.DataPropertyName = "c_ape1";
            this.cape1DataGridViewTextBoxColumn.HeaderText = "Ap. Paterno";
            this.cape1DataGridViewTextBoxColumn.MinimumWidth = 6;
            this.cape1DataGridViewTextBoxColumn.Name = "cape1DataGridViewTextBoxColumn";
            this.cape1DataGridViewTextBoxColumn.ReadOnly = true;
            this.cape1DataGridViewTextBoxColumn.Width = 118;
            // 
            // cape2DataGridViewTextBoxColumn
            // 
            this.cape2DataGridViewTextBoxColumn.DataPropertyName = "c_ape2";
            this.cape2DataGridViewTextBoxColumn.HeaderText = "Ap. Materno";
            this.cape2DataGridViewTextBoxColumn.MinimumWidth = 6;
            this.cape2DataGridViewTextBoxColumn.Name = "cape2DataGridViewTextBoxColumn";
            this.cape2DataGridViewTextBoxColumn.ReadOnly = true;
            this.cape2DataGridViewTextBoxColumn.Width = 124;
            // 
            // cnom1DataGridViewTextBoxColumn
            // 
            this.cnom1DataGridViewTextBoxColumn.DataPropertyName = "c_nom1";
            this.cnom1DataGridViewTextBoxColumn.HeaderText = "Nombres";
            this.cnom1DataGridViewTextBoxColumn.MinimumWidth = 6;
            this.cnom1DataGridViewTextBoxColumn.Name = "cnom1DataGridViewTextBoxColumn";
            this.cnom1DataGridViewTextBoxColumn.ReadOnly = true;
            this.cnom1DataGridViewTextBoxColumn.Width = 103;
            // 
            // empleadoBindingSource
            // 
            this.empleadoBindingSource.DataSource = typeof(SIAC_Datos.Models.Planillas.Empleado);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(263, 31);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(63, 24);
            this.kryptonLabel2.TabIndex = 3;
            this.kryptonLabel2.Values.Text = "Fch. Fin";
            // 
            // DtpFechaFin
            // 
            this.DtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpFechaFin.Location = new System.Drawing.Point(331, 31);
            this.DtpFechaFin.Name = "DtpFechaFin";
            this.DtpFechaFin.Size = new System.Drawing.Size(113, 25);
            this.DtpFechaFin.TabIndex = 2;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(8, 29);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(62, 24);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "Fch. Ini.";
            // 
            // DtpFechaInicio
            // 
            this.DtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpFechaInicio.Location = new System.Drawing.Point(88, 29);
            this.DtpFechaInicio.Name = "DtpFechaInicio";
            this.DtpFechaInicio.Size = new System.Drawing.Size(113, 25);
            this.DtpFechaInicio.TabIndex = 0;
            // 
            // FrmRepAvancePersonal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1290, 692);
            this.Controls.Add(this.kryptonHeaderGroup2);
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmRepAvancePersonal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Planilla - Reporte de Avance de Personal";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvResultado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).EndInit();
            this.kryptonHeaderGroup2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).EndInit();
            this.kryptonHeaderGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productoRecetaLineaTareaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empleadoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup BtnExportarExcel;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView DgvResultado;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup2;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker DtpFechaFin;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker DtpFechaInicio;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup BtnAgregarEmpleado;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup BtnEliminarEmpleado;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView kryptonDataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnumdocideDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cape1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cape2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnom1DataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource empleadoBindingSource;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox TxtProducto;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny BtnBuscarProducto;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.BindingSource productoRecetaLineaTareaBindingSource;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup BtnBuscar;
    }
}