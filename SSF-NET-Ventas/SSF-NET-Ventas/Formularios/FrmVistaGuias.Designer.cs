namespace SSF_NET_Ventas.Formularios
{
    partial class FrmVistaGuias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVistaGuias));
            this.Sz1 = new C1.Win.C1Sizer.C1Sizer();
            this.FgDatos = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1DockingTab1 = new C1.Win.C1Command.C1DockingTab();
            this.c1DockingTabPage1 = new C1.Win.C1Command.C1DockingTabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.LblNumReg = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.FgItems = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.OptIte2 = new System.Windows.Forms.RadioButton();
            this.OptIte1 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TxtFchFin = new System.Windows.Forms.DateTimePicker();
            this.TxtFchIni = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OptOpcion3 = new System.Windows.Forms.RadioButton();
            this.OptOpcion2 = new System.Windows.Forms.RadioButton();
            this.OptOpcion1 = new System.Windows.Forms.RadioButton();
            this.c1DockingTabPage2 = new C1.Win.C1Command.C1DockingTabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FgCliente = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.OptSel = new System.Windows.Forms.RadioButton();
            this.OptAll = new System.Windows.Forms.RadioButton();
            this.ToolHerramientas = new System.Windows.Forms.ToolStrip();
            this.ToolBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolImprimir = new System.Windows.Forms.ToolStripButton();
            this.ToolExportar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.Sz1)).BeginInit();
            this.Sz1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).BeginInit();
            this.c1DockingTab1.SuspendLayout();
            this.c1DockingTabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.c1DockingTabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgCliente)).BeginInit();
            this.ToolHerramientas.SuspendLayout();
            this.SuspendLayout();
            // 
            // Sz1
            // 
            this.Sz1.Border.Corners = new C1.Win.C1Sizer.Corners(1, 1, 1, 1);
            this.Sz1.Border.Thickness = new System.Windows.Forms.Padding(1);
            this.Sz1.Controls.Add(this.FgDatos);
            this.Sz1.Controls.Add(this.c1DockingTab1);
            this.Sz1.GridDefinition = "17.9190751445087:False:True;81.1175337186898:False:False;\t99.6011964107677:False:" +
    "False;";
            this.Sz1.Location = new System.Drawing.Point(3, 40);
            this.Sz1.Margin = new System.Windows.Forms.Padding(1);
            this.Sz1.Name = "Sz1";
            this.Sz1.Padding = new System.Windows.Forms.Padding(1);
            this.Sz1.Size = new System.Drawing.Size(1003, 519);
            this.Sz1.SplitterWidth = 1;
            this.Sz1.TabIndex = 40;
            this.Sz1.Text = "c1Sizer1";
            // 
            // FgDatos
            // 
            this.FgDatos.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgDatos.ColumnInfo = "10,1,0,0,0,85,Columns:";
            this.FgDatos.Location = new System.Drawing.Point(2, 96);
            this.FgDatos.Margin = new System.Windows.Forms.Padding(1);
            this.FgDatos.Name = "FgDatos";
            this.FgDatos.Rows.DefaultSize = 17;
            this.FgDatos.Size = new System.Drawing.Size(999, 421);
            this.FgDatos.TabIndex = 2;
            // 
            // c1DockingTab1
            // 
            this.c1DockingTab1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.c1DockingTab1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage1);
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage2);
            this.c1DockingTab1.Location = new System.Drawing.Point(2, 2);
            this.c1DockingTab1.Name = "c1DockingTab1";
            this.c1DockingTab1.Size = new System.Drawing.Size(999, 93);
            this.c1DockingTab1.TabIndex = 1;
            this.c1DockingTab1.TabsSpacing = 0;
            this.c1DockingTab1.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // c1DockingTabPage1
            // 
            this.c1DockingTabPage1.Controls.Add(this.panel1);
            this.c1DockingTabPage1.Location = new System.Drawing.Point(24, 1);
            this.c1DockingTabPage1.Name = "c1DockingTabPage1";
            this.c1DockingTabPage1.Size = new System.Drawing.Size(974, 91);
            this.c1DockingTabPage1.TabIndex = 0;
            this.c1DockingTabPage1.Text = "Inicio";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(974, 93);
            this.panel1.TabIndex = 2;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.LblNumReg);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Location = new System.Drawing.Point(835, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(134, 87);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "[ Datos ]";
            // 
            // LblNumReg
            // 
            this.LblNumReg.BackColor = System.Drawing.Color.White;
            this.LblNumReg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblNumReg.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumReg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.LblNumReg.Location = new System.Drawing.Point(6, 43);
            this.LblNumReg.Name = "LblNumReg";
            this.LblNumReg.Size = new System.Drawing.Size(111, 33);
            this.LblNumReg.TabIndex = 78;
            this.LblNumReg.Text = "LblNumReg";
            this.LblNumReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 77;
            this.label2.Text = "Nº de Registros";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.FgItems);
            this.groupBox5.Controls.Add(this.OptIte2);
            this.groupBox5.Controls.Add(this.OptIte1);
            this.groupBox5.Location = new System.Drawing.Point(380, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(453, 83);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "[ Filtros ]";
            // 
            // FgItems
            // 
            this.FgItems.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgItems.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:8;}\t";
            this.FgItems.Location = new System.Drawing.Point(116, 10);
            this.FgItems.Margin = new System.Windows.Forms.Padding(1);
            this.FgItems.Name = "FgItems";
            this.FgItems.Rows.DefaultSize = 17;
            this.FgItems.Size = new System.Drawing.Size(333, 68);
            this.FgItems.TabIndex = 84;
            this.FgItems.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgItems_CellButtonClick);
            this.FgItems.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FgItems_KeyUp);
            // 
            // OptIte2
            // 
            this.OptIte2.AutoSize = true;
            this.OptIte2.Location = new System.Drawing.Point(11, 44);
            this.OptIte2.Name = "OptIte2";
            this.OptIte2.Size = new System.Drawing.Size(104, 17);
            this.OptIte2.TabIndex = 83;
            this.OptIte2.TabStop = true;
            this.OptIte2.Text = "Seleccionar Item";
            this.OptIte2.UseVisualStyleBackColor = true;
            // 
            // OptIte1
            // 
            this.OptIte1.AutoSize = true;
            this.OptIte1.Location = new System.Drawing.Point(11, 21);
            this.OptIte1.Name = "OptIte1";
            this.OptIte1.Size = new System.Drawing.Size(99, 17);
            this.OptIte1.TabIndex = 82;
            this.OptIte1.TabStop = true;
            this.OptIte1.Text = "Todos los Items";
            this.OptIte1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TxtFchFin);
            this.groupBox3.Controls.Add(this.TxtFchIni);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(173, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(206, 87);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "[ Rango de fechas ]";
            // 
            // TxtFchFin
            // 
            this.TxtFchFin.Enabled = false;
            this.TxtFchFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchFin.Location = new System.Drawing.Point(107, 48);
            this.TxtFchFin.Name = "TxtFchFin";
            this.TxtFchFin.Size = new System.Drawing.Size(88, 20);
            this.TxtFchFin.TabIndex = 81;
            // 
            // TxtFchIni
            // 
            this.TxtFchIni.Enabled = false;
            this.TxtFchIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchIni.Location = new System.Drawing.Point(107, 22);
            this.TxtFchIni.Name = "TxtFchIni";
            this.TxtFchIni.Size = new System.Drawing.Size(88, 20);
            this.TxtFchIni.TabIndex = 80;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(6, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 79;
            this.label4.Text = "Fecha de Termino";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 78;
            this.label1.Text = "Fecha Inicio";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OptOpcion3);
            this.groupBox1.Controls.Add(this.OptOpcion2);
            this.groupBox1.Controls.Add(this.OptOpcion1);
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 87);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "[ Opciones de Reporte ]";
            // 
            // OptOpcion3
            // 
            this.OptOpcion3.AutoSize = true;
            this.OptOpcion3.Location = new System.Drawing.Point(11, 65);
            this.OptOpcion3.Name = "OptOpcion3";
            this.OptOpcion3.Size = new System.Drawing.Size(118, 17);
            this.OptOpcion3.TabIndex = 3;
            this.OptOpcion3.TabStop = true;
            this.OptOpcion3.Text = "Acumulado por mes";
            this.OptOpcion3.UseVisualStyleBackColor = true;
            this.OptOpcion3.CheckedChanged += new System.EventHandler(this.OptOpcion3_CheckedChanged);
            // 
            // OptOpcion2
            // 
            this.OptOpcion2.AutoSize = true;
            this.OptOpcion2.Location = new System.Drawing.Point(11, 43);
            this.OptOpcion2.Name = "OptOpcion2";
            this.OptOpcion2.Size = new System.Drawing.Size(128, 17);
            this.OptOpcion2.TabIndex = 2;
            this.OptOpcion2.TabStop = true;
            this.OptOpcion2.Text = "Por Periodo Detallado";
            this.OptOpcion2.UseVisualStyleBackColor = true;
            this.OptOpcion2.CheckedChanged += new System.EventHandler(this.OptOpcion2_CheckedChanged);
            // 
            // OptOpcion1
            // 
            this.OptOpcion1.AutoSize = true;
            this.OptOpcion1.Location = new System.Drawing.Point(11, 21);
            this.OptOpcion1.Name = "OptOpcion1";
            this.OptOpcion1.Size = new System.Drawing.Size(130, 17);
            this.OptOpcion1.TabIndex = 1;
            this.OptOpcion1.TabStop = true;
            this.OptOpcion1.Text = "Por Periodo Resumido";
            this.OptOpcion1.UseVisualStyleBackColor = true;
            this.OptOpcion1.CheckedChanged += new System.EventHandler(this.OptOpcion1_CheckedChanged);
            // 
            // c1DockingTabPage2
            // 
            this.c1DockingTabPage2.Controls.Add(this.panel2);
            this.c1DockingTabPage2.Location = new System.Drawing.Point(24, 1);
            this.c1DockingTabPage2.Name = "c1DockingTabPage2";
            this.c1DockingTabPage2.Size = new System.Drawing.Size(974, 91);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Mas";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Location = new System.Drawing.Point(3, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(939, 93);
            this.panel2.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FgCliente);
            this.groupBox2.Controls.Add(this.OptSel);
            this.groupBox2.Controls.Add(this.OptAll);
            this.groupBox2.Location = new System.Drawing.Point(3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(474, 87);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "[ Filtros Adicionales ]";
            // 
            // FgCliente
            // 
            this.FgCliente.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgCliente.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:7;}\t";
            this.FgCliente.Location = new System.Drawing.Point(134, 10);
            this.FgCliente.Margin = new System.Windows.Forms.Padding(1);
            this.FgCliente.Name = "FgCliente";
            this.FgCliente.Rows.DefaultSize = 17;
            this.FgCliente.Size = new System.Drawing.Size(333, 68);
            this.FgCliente.TabIndex = 85;
            this.FgCliente.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgCliente_CellButtonClick);
            this.FgCliente.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FgCliente_KeyUp);
            // 
            // OptSel
            // 
            this.OptSel.AutoSize = true;
            this.OptSel.Location = new System.Drawing.Point(7, 42);
            this.OptSel.Name = "OptSel";
            this.OptSel.Size = new System.Drawing.Size(116, 17);
            this.OptSel.TabIndex = 81;
            this.OptSel.TabStop = true;
            this.OptSel.Text = "Seleccionar Cliente";
            this.OptSel.UseVisualStyleBackColor = true;
            // 
            // OptAll
            // 
            this.OptAll.AutoSize = true;
            this.OptAll.Location = new System.Drawing.Point(6, 19);
            this.OptAll.Name = "OptAll";
            this.OptAll.Size = new System.Drawing.Size(111, 17);
            this.OptAll.TabIndex = 80;
            this.OptAll.TabStop = true;
            this.OptAll.Text = "Todos los Clientes";
            this.OptAll.UseVisualStyleBackColor = true;
            this.OptAll.CheckedChanged += new System.EventHandler(this.OptAll_CheckedChanged);
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
            this.ToolHerramientas.Size = new System.Drawing.Size(1008, 39);
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
            this.ToolBuscar.Size = new System.Drawing.Size(36, 36);
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
            this.ToolImprimir.ToolTipText = "Imprimir";
            // 
            // ToolExportar
            // 
            this.ToolExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolExportar.Image = ((System.Drawing.Image)(resources.GetObject("ToolExportar.Image")));
            this.ToolExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolExportar.Name = "ToolExportar";
            this.ToolExportar.Size = new System.Drawing.Size(36, 36);
            this.ToolExportar.Text = "toolStripButton3";
            this.ToolExportar.ToolTipText = "Exportar a Excel";
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
            // FrmVistaGuias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 561);
            this.Controls.Add(this.Sz1);
            this.Controls.Add(this.ToolHerramientas);
            this.Name = "FrmVistaGuias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmVistaGuias";
            this.Load += new System.EventHandler(this.FrmVistaGuias_Load);
            this.Resize += new System.EventHandler(this.FrmVistaGuias_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.Sz1)).EndInit();
            this.Sz1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).EndInit();
            this.c1DockingTab1.ResumeLayout(false);
            this.c1DockingTabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.c1DockingTabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgCliente)).EndInit();
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Sizer.C1Sizer Sz1;
        private C1.Win.C1Command.C1DockingTab c1DockingTab1;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label LblNumReg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton OptSel;
        private System.Windows.Forms.RadioButton OptAll;
        private System.Windows.Forms.ToolStrip ToolHerramientas;
        private System.Windows.Forms.ToolStripButton ToolBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ToolImprimir;
        private System.Windows.Forms.ToolStripButton ToolExportar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolSalir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton OptOpcion3;
        private System.Windows.Forms.RadioButton OptOpcion2;
        private System.Windows.Forms.RadioButton OptOpcion1;
        private System.Windows.Forms.DateTimePicker TxtFchFin;
        private System.Windows.Forms.DateTimePicker TxtFchIni;
        private C1.Win.C1FlexGrid.C1FlexGrid FgItems;
        private System.Windows.Forms.RadioButton OptIte2;
        private System.Windows.Forms.RadioButton OptIte1;
        private C1.Win.C1FlexGrid.C1FlexGrid FgCliente;
        private C1.Win.C1FlexGrid.C1FlexGrid FgDatos;
    }
}