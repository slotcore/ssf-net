namespace SSF_NET_Produccion.Formularios
{
    partial class FrmConsultaTareas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultaTareas));
            this.FgDatos = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtFchFin = new System.Windows.Forms.DateTimePicker();
            this.OptEnt = new System.Windows.Forms.RadioButton();
            this.OptPen = new System.Windows.Forms.RadioButton();
            this.OpTod = new System.Windows.Forms.RadioButton();
            this.TxtFchIni = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Sz1 = new C1.Win.C1Sizer.C1Sizer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolExportar = new System.Windows.Forms.ToolStripButton();
            this.ToolImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolBuscar = new System.Windows.Forms.ToolStripButton();
            this.ToolHerramientas = new System.Windows.Forms.ToolStrip();
            this.FgPro = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.FgDatos)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sz1)).BeginInit();
            this.Sz1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.ToolHerramientas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgPro)).BeginInit();
            this.SuspendLayout();
            // 
            // FgDatos
            // 
            this.FgDatos.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgDatos.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:10;}\t";
            this.FgDatos.Location = new System.Drawing.Point(2, 91);
            this.FgDatos.Name = "FgDatos";
            this.FgDatos.Rows.DefaultSize = 17;
            this.FgDatos.Size = new System.Drawing.Size(1070, 426);
            this.FgDatos.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 76;
            this.label1.Text = "Fch. Termino";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "Fch. Inicio";
            // 
            // TxtFchFin
            // 
            this.TxtFchFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchFin.Location = new System.Drawing.Point(92, 42);
            this.TxtFchFin.Name = "TxtFchFin";
            this.TxtFchFin.Size = new System.Drawing.Size(85, 20);
            this.TxtFchFin.TabIndex = 1;
            // 
            // OptEnt
            // 
            this.OptEnt.AutoSize = true;
            this.OptEnt.Location = new System.Drawing.Point(128, 18);
            this.OptEnt.Name = "OptEnt";
            this.OptEnt.Size = new System.Drawing.Size(79, 17);
            this.OptEnt.TabIndex = 2;
            this.OptEnt.TabStop = true;
            this.OptEnt.Text = "Entregados";
            this.OptEnt.UseVisualStyleBackColor = true;
            // 
            // OptPen
            // 
            this.OptPen.AutoSize = true;
            this.OptPen.Location = new System.Drawing.Point(61, 21);
            this.OptPen.Name = "OptPen";
            this.OptPen.Size = new System.Drawing.Size(78, 17);
            this.OptPen.TabIndex = 1;
            this.OptPen.TabStop = true;
            this.OptPen.Text = "Pendientes";
            this.OptPen.UseVisualStyleBackColor = true;
            // 
            // OpTod
            // 
            this.OpTod.AutoSize = true;
            this.OpTod.Location = new System.Drawing.Point(14, 20);
            this.OpTod.Name = "OpTod";
            this.OpTod.Size = new System.Drawing.Size(55, 17);
            this.OpTod.TabIndex = 0;
            this.OpTod.TabStop = true;
            this.OpTod.Text = "Todos";
            this.OpTod.UseVisualStyleBackColor = true;
            // 
            // TxtFchIni
            // 
            this.TxtFchIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchIni.Location = new System.Drawing.Point(92, 19);
            this.TxtFchIni.Name = "TxtFchIni";
            this.TxtFchIni.Size = new System.Drawing.Size(85, 20);
            this.TxtFchIni.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.OptEnt);
            this.groupBox2.Controls.Add(this.OptPen);
            this.groupBox2.Controls.Add(this.OpTod);
            this.groupBox2.Location = new System.Drawing.Point(653, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 47);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "[ Criterio Busqueda ]";
            this.groupBox2.Visible = false;
            // 
            // Sz1
            // 
            this.Sz1.Border.Corners = new C1.Win.C1Sizer.Corners(1, 1, 1, 1);
            this.Sz1.Border.Thickness = new System.Windows.Forms.Padding(1);
            this.Sz1.Controls.Add(this.panel1);
            this.Sz1.Controls.Add(this.FgDatos);
            this.Sz1.GridDefinition = "16.9556840077071:False:True;82.0809248554913:False:False;\t99.6275605214153:False:" +
    "False;";
            this.Sz1.Location = new System.Drawing.Point(0, 40);
            this.Sz1.Margin = new System.Windows.Forms.Padding(1);
            this.Sz1.Name = "Sz1";
            this.Sz1.Padding = new System.Windows.Forms.Padding(1);
            this.Sz1.Size = new System.Drawing.Size(1074, 519);
            this.Sz1.SplitterWidth = 1;
            this.Sz1.TabIndex = 46;
            this.Sz1.Text = "c1Sizer1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FgPro);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1070, 88);
            this.panel1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TxtFchFin);
            this.groupBox1.Controls.Add(this.TxtFchIni);
            this.groupBox1.Location = new System.Drawing.Point(5, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 89);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "[ Fecha de Consulta ]";
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
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
            // ToolImprimir
            // 
            this.ToolImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolImprimir.Image = ((System.Drawing.Image)(resources.GetObject("ToolImprimir.Image")));
            this.ToolImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolImprimir.Name = "ToolImprimir";
            this.ToolImprimir.Size = new System.Drawing.Size(36, 36);
            this.ToolImprimir.Text = "toolStripButton1";
            this.ToolImprimir.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
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
            this.ToolHerramientas.Size = new System.Drawing.Size(1076, 39);
            this.ToolHerramientas.TabIndex = 45;
            this.ToolHerramientas.Text = "toolStrip1";
            // 
            // FgPro
            // 
            this.FgPro.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgPro.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:7;}\t";
            this.FgPro.Location = new System.Drawing.Point(200, 5);
            this.FgPro.Name = "FgPro";
            this.FgPro.Rows.DefaultSize = 17;
            this.FgPro.Size = new System.Drawing.Size(425, 81);
            this.FgPro.TabIndex = 80;
            this.FgPro.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgPro_CellButtonClick);
            this.FgPro.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FgPro_KeyUp);
            // 
            // FrmConsultaTareas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 561);
            this.Controls.Add(this.Sz1);
            this.Controls.Add(this.ToolHerramientas);
            this.Name = "FrmConsultaTareas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmConsultaTareas";
            this.Load += new System.EventHandler(this.FrmConsultaTareas_Load);
            this.Resize += new System.EventHandler(this.FrmConsultaTareas_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.FgDatos)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sz1)).EndInit();
            this.Sz1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgPro)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid FgDatos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker TxtFchFin;
        private System.Windows.Forms.RadioButton OptEnt;
        private System.Windows.Forms.RadioButton OptPen;
        private System.Windows.Forms.RadioButton OpTod;
        private System.Windows.Forms.DateTimePicker TxtFchIni;
        private System.Windows.Forms.GroupBox groupBox2;
        private C1.Win.C1Sizer.C1Sizer Sz1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripButton ToolSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolExportar;
        private System.Windows.Forms.ToolStripButton ToolImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ToolBuscar;
        private System.Windows.Forms.ToolStrip ToolHerramientas;
        private C1.Win.C1FlexGrid.C1FlexGrid FgPro;
    }
}