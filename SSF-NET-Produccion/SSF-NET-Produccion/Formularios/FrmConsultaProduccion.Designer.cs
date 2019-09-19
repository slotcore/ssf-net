namespace SSF_NET_Produccion.Formularios
{
    partial class FrmConsultaProduccion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultaProduccion));
            this.Sz1 = new C1.Win.C1Sizer.C1Sizer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.OptGru3 = new System.Windows.Forms.RadioButton();
            this.OptGru2 = new System.Windows.Forms.RadioButton();
            this.OptGru1 = new System.Windows.Forms.RadioButton();
            this.FgIns = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.FgPro = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TxtFchIni = new System.Windows.Forms.DateTimePicker();
            this.TxtFchFin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OptTip2 = new System.Windows.Forms.RadioButton();
            this.OptTip1 = new System.Windows.Forms.RadioButton();
            this.FgDatos = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.ToolHerramientas = new System.Windows.Forms.ToolStrip();
            this.ToolBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolImprimir = new System.Windows.Forms.ToolStripButton();
            this.ToolExportar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.Sz1)).BeginInit();
            this.Sz1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgIns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgPro)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgDatos)).BeginInit();
            this.ToolHerramientas.SuspendLayout();
            this.SuspendLayout();
            // 
            // Sz1
            // 
            this.Sz1.Border.Corners = new C1.Win.C1Sizer.Corners(1, 1, 1, 1);
            this.Sz1.Border.Thickness = new System.Windows.Forms.Padding(1);
            this.Sz1.Controls.Add(this.panel3);
            this.Sz1.Controls.Add(this.FgDatos);
            this.Sz1.GridDefinition = "17.9190751445087:False:True;81.1175337186898:False:False;\t99.6275605214153:False:" +
    "False;";
            this.Sz1.Location = new System.Drawing.Point(1, 42);
            this.Sz1.Margin = new System.Windows.Forms.Padding(1);
            this.Sz1.Name = "Sz1";
            this.Sz1.Padding = new System.Windows.Forms.Padding(1);
            this.Sz1.Size = new System.Drawing.Size(1074, 519);
            this.Sz1.SplitterWidth = 1;
            this.Sz1.TabIndex = 40;
            this.Sz1.Text = "c1Sizer1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Controls.Add(this.FgIns);
            this.panel3.Controls.Add(this.FgPro);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Location = new System.Drawing.Point(2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1070, 93);
            this.panel3.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.OptGru3);
            this.groupBox3.Controls.Add(this.OptGru2);
            this.groupBox3.Controls.Add(this.OptGru1);
            this.groupBox3.Location = new System.Drawing.Point(272, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(89, 87);
            this.groupBox3.TabIndex = 81;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "[ Agrupar Por ]";
            // 
            // OptGru3
            // 
            this.OptGru3.AutoSize = true;
            this.OptGru3.Location = new System.Drawing.Point(12, 64);
            this.OptGru3.Name = "OptGru3";
            this.OptGru3.Size = new System.Drawing.Size(67, 17);
            this.OptGru3.TabIndex = 2;
            this.OptGru3.TabStop = true;
            this.OptGru3.Text = "x Insumo";
            this.OptGru3.UseVisualStyleBackColor = true;
            // 
            // OptGru2
            // 
            this.OptGru2.AutoSize = true;
            this.OptGru2.Location = new System.Drawing.Point(12, 42);
            this.OptGru2.Name = "OptGru2";
            this.OptGru2.Size = new System.Drawing.Size(65, 17);
            this.OptGru2.TabIndex = 1;
            this.OptGru2.TabStop = true;
            this.OptGru2.Text = "x Familia";
            this.OptGru2.UseVisualStyleBackColor = true;
            // 
            // OptGru1
            // 
            this.OptGru1.AutoSize = true;
            this.OptGru1.Location = new System.Drawing.Point(12, 19);
            this.OptGru1.Name = "OptGru1";
            this.OptGru1.Size = new System.Drawing.Size(76, 17);
            this.OptGru1.TabIndex = 0;
            this.OptGru1.TabStop = true;
            this.OptGru1.Text = "x Producto";
            this.OptGru1.UseVisualStyleBackColor = true;
            // 
            // FgIns
            // 
            this.FgIns.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgIns.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:7;}\t";
            this.FgIns.Location = new System.Drawing.Point(718, 6);
            this.FgIns.Name = "FgIns";
            this.FgIns.Rows.DefaultSize = 17;
            this.FgIns.Size = new System.Drawing.Size(350, 79);
            this.FgIns.TabIndex = 80;
            this.FgIns.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgIns_CellButtonClick);
            this.FgIns.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FgIns_KeyUp);
            // 
            // FgPro
            // 
            this.FgPro.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgPro.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:7;}\t";
            this.FgPro.Location = new System.Drawing.Point(365, 6);
            this.FgPro.Name = "FgPro";
            this.FgPro.Rows.DefaultSize = 17;
            this.FgPro.Size = new System.Drawing.Size(350, 79);
            this.FgPro.TabIndex = 79;
            this.FgPro.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgPro_CellButtonClick);
            this.FgPro.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FgPro_KeyUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TxtFchIni);
            this.groupBox2.Controls.Add(this.TxtFchFin);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(9, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(167, 87);
            this.groupBox2.TabIndex = 78;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "[ Periodo ]";
            // 
            // TxtFchIni
            // 
            this.TxtFchIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchIni.Location = new System.Drawing.Point(75, 22);
            this.TxtFchIni.Name = "TxtFchIni";
            this.TxtFchIni.Size = new System.Drawing.Size(85, 20);
            this.TxtFchIni.TabIndex = 0;
            // 
            // TxtFchFin
            // 
            this.TxtFchFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchFin.Location = new System.Drawing.Point(75, 48);
            this.TxtFchFin.Name = "TxtFchFin";
            this.TxtFchFin.Size = new System.Drawing.Size(85, 20);
            this.TxtFchFin.TabIndex = 1;
            this.TxtFchFin.ValueChanged += new System.EventHandler(this.TxtFchFin_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 76;
            this.label1.Text = "Fch. Termino";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(0, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "Fch. Inicio";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OptTip2);
            this.groupBox1.Controls.Add(this.OptTip1);
            this.groupBox1.Location = new System.Drawing.Point(180, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(87, 87);
            this.groupBox1.TabIndex = 77;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "[ Tipo ]";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // OptTip2
            // 
            this.OptTip2.AutoSize = true;
            this.OptTip2.Location = new System.Drawing.Point(12, 42);
            this.OptTip2.Name = "OptTip2";
            this.OptTip2.Size = new System.Drawing.Size(70, 17);
            this.OptTip2.TabIndex = 1;
            this.OptTip2.TabStop = true;
            this.OptTip2.Text = "Detallado";
            this.OptTip2.UseVisualStyleBackColor = true;
            this.OptTip2.CheckedChanged += new System.EventHandler(this.OptTip2_CheckedChanged);
            // 
            // OptTip1
            // 
            this.OptTip1.AutoSize = true;
            this.OptTip1.Location = new System.Drawing.Point(12, 19);
            this.OptTip1.Name = "OptTip1";
            this.OptTip1.Size = new System.Drawing.Size(70, 17);
            this.OptTip1.TabIndex = 0;
            this.OptTip1.TabStop = true;
            this.OptTip1.Text = "Resumen";
            this.OptTip1.UseVisualStyleBackColor = true;
            this.OptTip1.CheckedChanged += new System.EventHandler(this.OptTip1_CheckedChanged);
            // 
            // FgDatos
            // 
            this.FgDatos.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgDatos.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:8;}\t";
            this.FgDatos.Location = new System.Drawing.Point(2, 96);
            this.FgDatos.Name = "FgDatos";
            this.FgDatos.Rows.DefaultSize = 17;
            this.FgDatos.Size = new System.Drawing.Size(1070, 421);
            this.FgDatos.TabIndex = 0;
            // 
            // ToolHerramientas
            // 
            this.ToolHerramientas.BackColor = System.Drawing.Color.White;
            this.ToolHerramientas.ForeColor = System.Drawing.Color.Black;
            this.ToolHerramientas.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolHerramientas.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ToolHerramientas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolBuscar,
            this.toolStripSeparator2,
            this.ToolImprimir,
            this.ToolExportar,
            this.toolStripSeparator1,
            this.ToolSalir});
            this.ToolHerramientas.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolHerramientas.Location = new System.Drawing.Point(0, 0);
            this.ToolHerramientas.Name = "ToolHerramientas";
            this.ToolHerramientas.Size = new System.Drawing.Size(1078, 39);
            this.ToolHerramientas.TabIndex = 39;
            this.ToolHerramientas.Text = "toolStrip1";
            // 
            // ToolBuscar
            // 
            this.ToolBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolBuscar.Image = ((System.Drawing.Image)(resources.GetObject("ToolBuscar.Image")));
            this.ToolBuscar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolBuscar.Name = "ToolBuscar";
            this.ToolBuscar.Size = new System.Drawing.Size(28, 36);
            this.ToolBuscar.Text = "toolStripButton1";
            this.ToolBuscar.ToolTipText = "Ejecutar Consulta";
            this.ToolBuscar.Click += new System.EventHandler(this.ToolBuscar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // ToolImprimir
            // 
            this.ToolImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolImprimir.Image = ((System.Drawing.Image)(resources.GetObject("ToolImprimir.Image")));
            this.ToolImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolImprimir.Name = "ToolImprimir";
            this.ToolImprimir.Size = new System.Drawing.Size(36, 36);
            this.ToolImprimir.Text = "toolStripButton1";
            this.ToolImprimir.Visible = false;
            this.ToolImprimir.Click += new System.EventHandler(this.ToolImprimir_Click);
            // 
            // ToolExportar
            // 
            this.ToolExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolExportar.Image = ((System.Drawing.Image)(resources.GetObject("ToolExportar.Image")));
            this.ToolExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolExportar.Name = "ToolExportar";
            this.ToolExportar.Size = new System.Drawing.Size(36, 36);
            this.ToolExportar.Text = "toolStripButton3";
            this.ToolExportar.ToolTipText = "Exportar";
            this.ToolExportar.Click += new System.EventHandler(this.ToolExportar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // ToolSalir
            // 
            this.ToolSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolSalir.Image = ((System.Drawing.Image)(resources.GetObject("ToolSalir.Image")));
            this.ToolSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolSalir.Name = "ToolSalir";
            this.ToolSalir.Size = new System.Drawing.Size(36, 36);
            this.ToolSalir.Text = "toolStripButton8";
            this.ToolSalir.ToolTipText = "Salir";
            this.ToolSalir.Click += new System.EventHandler(this.ToolSalir_Click);
            // 
            // FrmConsultaProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 563);
            this.Controls.Add(this.Sz1);
            this.Controls.Add(this.ToolHerramientas);
            this.Name = "FrmConsultaProduccion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmConsultaProduccion";
            this.Load += new System.EventHandler(this.FrmConsultaProduccion_Load);
            this.Resize += new System.EventHandler(this.FrmConsultaProduccion_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.Sz1)).EndInit();
            this.Sz1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgIns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgPro)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgDatos)).EndInit();
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Sizer.C1Sizer Sz1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker TxtFchIni;
        private System.Windows.Forms.DateTimePicker TxtFchFin;
        private C1.Win.C1FlexGrid.C1FlexGrid FgDatos;
        private System.Windows.Forms.ToolStrip ToolHerramientas;
        private System.Windows.Forms.ToolStripButton ToolBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ToolImprimir;
        private System.Windows.Forms.ToolStripButton ToolExportar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolSalir;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton OptGru3;
        private System.Windows.Forms.RadioButton OptGru2;
        private System.Windows.Forms.RadioButton OptGru1;
        private C1.Win.C1FlexGrid.C1FlexGrid FgIns;
        private C1.Win.C1FlexGrid.C1FlexGrid FgPro;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton OptTip2;
        private System.Windows.Forms.RadioButton OptTip1;
    }
}