namespace SSF_NET_Planillas.Formularios
{
    partial class FrmManEmpleadoPeriodo
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
            System.Windows.Forms.Label categoriaLabel;
            System.Windows.Forms.Label d_fchfinLabel;
            System.Windows.Forms.Label d_fchiniLabel;
            System.Windows.Forms.Label finperiodoLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManEmpleadoPeriodo));
            this.ToolHerramientas = new System.Windows.Forms.ToolStrip();
            this.ToolGrabar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolCancelar = new System.Windows.Forms.ToolStripButton();
            this.categoriaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.finPeriodoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LblTitulo2 = new System.Windows.Forms.Label();
            this.periodoLaboralBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.d_fchfinDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.d_fchiniDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.finperiodoComboBox = new System.Windows.Forms.ComboBox();
            this.categoriaComboBox = new System.Windows.Forms.ComboBox();
            this.kryptonDateTimePicker1 = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            categoriaLabel = new System.Windows.Forms.Label();
            d_fchfinLabel = new System.Windows.Forms.Label();
            d_fchiniLabel = new System.Windows.Forms.Label();
            finperiodoLabel = new System.Windows.Forms.Label();
            this.ToolHerramientas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.categoriaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.finPeriodoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodoLaboralBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // categoriaLabel
            // 
            categoriaLabel.AutoSize = true;
            categoriaLabel.Location = new System.Drawing.Point(65, 165);
            categoriaLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            categoriaLabel.Name = "categoriaLabel";
            categoriaLabel.Size = new System.Drawing.Size(73, 17);
            categoriaLabel.TabIndex = 46;
            categoriaLabel.Text = "Categoria:";
            // 
            // d_fchfinLabel
            // 
            d_fchfinLabel.AutoSize = true;
            d_fchfinLabel.Location = new System.Drawing.Point(65, 263);
            d_fchfinLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            d_fchfinLabel.Name = "d_fchfinLabel";
            d_fchfinLabel.Size = new System.Drawing.Size(74, 17);
            d_fchfinLabel.TabIndex = 48;
            d_fchfinLabel.Text = "Fecha Fin:";
            // 
            // d_fchiniLabel
            // 
            d_fchiniLabel.AutoSize = true;
            d_fchiniLabel.Location = new System.Drawing.Point(65, 231);
            d_fchiniLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            d_fchiniLabel.Name = "d_fchiniLabel";
            d_fchiniLabel.Size = new System.Drawing.Size(87, 17);
            d_fchiniLabel.TabIndex = 50;
            d_fchiniLabel.Text = "Fecha Inicio:";
            // 
            // finperiodoLabel
            // 
            finperiodoLabel.AutoSize = true;
            finperiodoLabel.Location = new System.Drawing.Point(65, 198);
            finperiodoLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            finperiodoLabel.Name = "finperiodoLabel";
            finperiodoLabel.Size = new System.Drawing.Size(178, 17);
            finperiodoLabel.TabIndex = 52;
            finperiodoLabel.Text = "Tipo Extinción de Contrato:";
            // 
            // ToolHerramientas
            // 
            this.ToolHerramientas.BackColor = System.Drawing.Color.White;
            this.ToolHerramientas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ToolHerramientas.ForeColor = System.Drawing.Color.Black;
            this.ToolHerramientas.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolHerramientas.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ToolHerramientas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolGrabar,
            this.toolStripSeparator2,
            this.ToolCancelar});
            this.ToolHerramientas.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolHerramientas.Location = new System.Drawing.Point(0, 0);
            this.ToolHerramientas.Name = "ToolHerramientas";
            this.ToolHerramientas.Size = new System.Drawing.Size(739, 39);
            this.ToolHerramientas.TabIndex = 37;
            this.ToolHerramientas.Text = "toolStrip1";
            // 
            // ToolGrabar
            // 
            this.ToolGrabar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolGrabar.Image = ((System.Drawing.Image)(resources.GetObject("ToolGrabar.Image")));
            this.ToolGrabar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolGrabar.Name = "ToolGrabar";
            this.ToolGrabar.Size = new System.Drawing.Size(36, 36);
            this.ToolGrabar.Text = "toolStripButton4";
            this.ToolGrabar.ToolTipText = "Grabar registro";
            this.ToolGrabar.Click += new System.EventHandler(this.ToolGrabar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // ToolCancelar
            // 
            this.ToolCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolCancelar.Image = ((System.Drawing.Image)(resources.GetObject("ToolCancelar.Image")));
            this.ToolCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolCancelar.Name = "ToolCancelar";
            this.ToolCancelar.Size = new System.Drawing.Size(36, 36);
            this.ToolCancelar.Text = "toolStripButton5";
            this.ToolCancelar.ToolTipText = "Cancelar";
            this.ToolCancelar.Click += new System.EventHandler(this.ToolCancelar_Click);
            // 
            // categoriaBindingSource
            // 
            this.categoriaBindingSource.DataSource = typeof(SIAC_Datos.Models.Maestros.Categoria);
            // 
            // finPeriodoBindingSource
            // 
            this.finPeriodoBindingSource.DataSource = typeof(SIAC_Datos.Models.Maestros.FinPeriodo);
            // 
            // LblTitulo2
            // 
            this.LblTitulo2.BackColor = System.Drawing.Color.Transparent;
            this.LblTitulo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitulo2.ForeColor = System.Drawing.Color.Black;
            this.LblTitulo2.Location = new System.Drawing.Point(-4, 53);
            this.LblTitulo2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblTitulo2.Name = "LblTitulo2";
            this.LblTitulo2.Size = new System.Drawing.Size(743, 28);
            this.LblTitulo2.TabIndex = 46;
            this.LblTitulo2.Text = "MANTENIMIENTO PERIODO LABORAL";
            this.LblTitulo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // periodoLaboralBindingSource
            // 
            this.periodoLaboralBindingSource.DataSource = typeof(SIAC_Datos.Models.Planillas.PeriodoLaboral);
            // 
            // d_fchfinDateTimePicker
            // 
            this.d_fchfinDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.periodoLaboralBindingSource, "d_fchfin", true));
            this.d_fchfinDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.d_fchfinDateTimePicker.Location = new System.Drawing.Point(253, 258);
            this.d_fchfinDateTimePicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.d_fchfinDateTimePicker.Name = "d_fchfinDateTimePicker";
            this.d_fchfinDateTimePicker.Size = new System.Drawing.Size(196, 22);
            this.d_fchfinDateTimePicker.TabIndex = 49;
            // 
            // d_fchiniDateTimePicker
            // 
            this.d_fchiniDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.periodoLaboralBindingSource, "d_fchini", true));
            this.d_fchiniDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.d_fchiniDateTimePicker.Location = new System.Drawing.Point(253, 226);
            this.d_fchiniDateTimePicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.d_fchiniDateTimePicker.Name = "d_fchiniDateTimePicker";
            this.d_fchiniDateTimePicker.Size = new System.Drawing.Size(196, 22);
            this.d_fchiniDateTimePicker.TabIndex = 51;
            // 
            // finperiodoComboBox
            // 
            this.finperiodoComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.periodoLaboralBindingSource, "finperiodo", true));
            this.finperiodoComboBox.DataSource = this.finPeriodoBindingSource;
            this.finperiodoComboBox.DisplayMember = "c_descripcion";
            this.finperiodoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.finperiodoComboBox.FormattingEnabled = true;
            this.finperiodoComboBox.Location = new System.Drawing.Point(253, 194);
            this.finperiodoComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.finperiodoComboBox.Name = "finperiodoComboBox";
            this.finperiodoComboBox.Size = new System.Drawing.Size(409, 24);
            this.finperiodoComboBox.TabIndex = 53;
            this.finperiodoComboBox.ValueMember = "n_idfinperiodo";
            // 
            // categoriaComboBox
            // 
            this.categoriaComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.periodoLaboralBindingSource, "categoria", true));
            this.categoriaComboBox.DataSource = this.categoriaBindingSource;
            this.categoriaComboBox.DisplayMember = "c_descripcion";
            this.categoriaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoriaComboBox.FormattingEnabled = true;
            this.categoriaComboBox.Location = new System.Drawing.Point(253, 161);
            this.categoriaComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.categoriaComboBox.Name = "categoriaComboBox";
            this.categoriaComboBox.Size = new System.Drawing.Size(409, 24);
            this.categoriaComboBox.TabIndex = 47;
            this.categoriaComboBox.ValueMember = "n_idcategoria";
            // 
            // kryptonDateTimePicker1
            // 
            this.kryptonDateTimePicker1.Checked = false;
            this.kryptonDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.kryptonDateTimePicker1.Location = new System.Drawing.Point(253, 288);
            this.kryptonDateTimePicker1.Name = "kryptonDateTimePicker1";
            this.kryptonDateTimePicker1.Size = new System.Drawing.Size(196, 25);
            this.kryptonDateTimePicker1.TabIndex = 54;
            // 
            // FrmManEmpleadoPeriodo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 402);
            this.Controls.Add(this.kryptonDateTimePicker1);
            this.Controls.Add(categoriaLabel);
            this.Controls.Add(this.categoriaComboBox);
            this.Controls.Add(d_fchfinLabel);
            this.Controls.Add(this.d_fchfinDateTimePicker);
            this.Controls.Add(d_fchiniLabel);
            this.Controls.Add(this.d_fchiniDateTimePicker);
            this.Controls.Add(finperiodoLabel);
            this.Controls.Add(this.finperiodoComboBox);
            this.Controls.Add(this.LblTitulo2);
            this.Controls.Add(this.ToolHerramientas);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmManEmpleadoPeriodo";
            this.Text = "Mantenimiento Periodo Laboral";
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.categoriaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.finPeriodoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodoLaboralBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ToolHerramientas;
        private System.Windows.Forms.ToolStripButton ToolGrabar;
        private System.Windows.Forms.ToolStripButton ToolCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label LblTitulo2;
        private System.Windows.Forms.BindingSource categoriaBindingSource;
        private System.Windows.Forms.BindingSource finPeriodoBindingSource;
        private System.Windows.Forms.BindingSource periodoLaboralBindingSource;
        private System.Windows.Forms.DateTimePicker d_fchfinDateTimePicker;
        private System.Windows.Forms.DateTimePicker d_fchiniDateTimePicker;
        private System.Windows.Forms.ComboBox finperiodoComboBox;
        private System.Windows.Forms.ComboBox categoriaComboBox;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker kryptonDateTimePicker1;
    }
}