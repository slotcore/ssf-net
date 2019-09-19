namespace SSF_NET_Logistica.Formularios
{
    partial class FrmConsultaCompras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultaCompras));
            this.ToolHerramientas = new System.Windows.Forms.ToolStrip();
            this.ToolBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolImprimir = new System.Windows.Forms.ToolStripButton();
            this.ToolExportar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            this.Sz1 = new C1.Win.C1Sizer.C1Sizer();
            this.FgDatos = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1DockingTab1 = new C1.Win.C1Command.C1DockingTab();
            this.c1DockingTabPage1 = new C1.Win.C1Command.C1DockingTabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.CboTipPro = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton12 = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.OptPag = new System.Windows.Forms.RadioButton();
            this.OptPen = new System.Windows.Forms.RadioButton();
            this.OptTodDoc = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.OptDol = new System.Windows.Forms.RadioButton();
            this.OptSol = new System.Windows.Forms.RadioButton();
            this.OptTod = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.OptFchReg = new System.Windows.Forms.RadioButton();
            this.OptFchVen = new System.Windows.Forms.RadioButton();
            this.OptFchEmi = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.OptResDet = new System.Windows.Forms.RadioButton();
            this.OptDet = new System.Windows.Forms.RadioButton();
            this.OptRes = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtFchFin = new System.Windows.Forms.DateTimePicker();
            this.TxtFchIni = new System.Windows.Forms.DateTimePicker();
            this.c1DockingTabPage2 = new C1.Win.C1Command.C1DockingTabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CmdAddIte = new System.Windows.Forms.Button();
            this.CmdAddPro = new System.Windows.Forms.Button();
            this.CmdDelIte = new System.Windows.Forms.Button();
            this.CmdDelPro = new System.Windows.Forms.Button();
            this.FgItem = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.FgPro = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.ToolHerramientas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sz1)).BeginInit();
            this.Sz1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).BeginInit();
            this.c1DockingTab1.SuspendLayout();
            this.c1DockingTabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.c1DockingTabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgPro)).BeginInit();
            this.SuspendLayout();
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
            this.ToolHerramientas.Size = new System.Drawing.Size(1080, 39);
            this.ToolHerramientas.TabIndex = 35;
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
            // Sz1
            // 
            this.Sz1.Border.Corners = new C1.Win.C1Sizer.Corners(1, 1, 1, 1);
            this.Sz1.Border.Thickness = new System.Windows.Forms.Padding(1);
            this.Sz1.Controls.Add(this.FgDatos);
            this.Sz1.Controls.Add(this.c1DockingTab1);
            this.Sz1.GridDefinition = "17.9190751445087:False:True;81.1175337186898:False:False;\t99.6279069767442:False:" +
    "False;";
            this.Sz1.Location = new System.Drawing.Point(3, 41);
            this.Sz1.Margin = new System.Windows.Forms.Padding(1);
            this.Sz1.Name = "Sz1";
            this.Sz1.Padding = new System.Windows.Forms.Padding(1);
            this.Sz1.Size = new System.Drawing.Size(1075, 519);
            this.Sz1.SplitterWidth = 1;
            this.Sz1.TabIndex = 36;
            this.Sz1.Text = "c1Sizer1";
            // 
            // FgDatos
            // 
            this.FgDatos.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgDatos.ColumnInfo = "10,1,0,0,0,85,Columns:";
            this.FgDatos.Location = new System.Drawing.Point(2, 96);
            this.FgDatos.Name = "FgDatos";
            this.FgDatos.Rows.DefaultSize = 17;
            this.FgDatos.Size = new System.Drawing.Size(1071, 421);
            this.FgDatos.TabIndex = 15;
            // 
            // c1DockingTab1
            // 
            this.c1DockingTab1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.c1DockingTab1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage1);
            this.c1DockingTab1.Controls.Add(this.c1DockingTabPage2);
            this.c1DockingTab1.Location = new System.Drawing.Point(2, 2);
            this.c1DockingTab1.Name = "c1DockingTab1";
            this.c1DockingTab1.Size = new System.Drawing.Size(1071, 93);
            this.c1DockingTab1.TabIndex = 1;
            this.c1DockingTab1.TabsSpacing = 0;
            this.c1DockingTab1.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // c1DockingTabPage1
            // 
            this.c1DockingTabPage1.Controls.Add(this.panel1);
            this.c1DockingTabPage1.Location = new System.Drawing.Point(24, 1);
            this.c1DockingTabPage1.Name = "c1DockingTabPage1";
            this.c1DockingTabPage1.Size = new System.Drawing.Size(1046, 91);
            this.c1DockingTabPage1.TabIndex = 0;
            this.c1DockingTabPage1.Text = "Inicio";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 93);
            this.panel1.TabIndex = 2;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.CboTipPro);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.radioButton12);
            this.groupBox6.Location = new System.Drawing.Point(660, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(275, 87);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "[ Detallar ]";
            // 
            // CboTipPro
            // 
            this.CboTipPro.BackColor = System.Drawing.Color.White;
            this.CboTipPro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTipPro.Enabled = false;
            this.CboTipPro.ForeColor = System.Drawing.Color.Black;
            this.CboTipPro.FormattingEnabled = true;
            this.CboTipPro.Location = new System.Drawing.Point(9, 41);
            this.CboTipPro.Name = "CboTipPro";
            this.CboTipPro.Size = new System.Drawing.Size(262, 21);
            this.CboTipPro.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 77;
            this.label2.Text = "Tipo Producto";
            // 
            // radioButton12
            // 
            this.radioButton12.AutoSize = true;
            this.radioButton12.Location = new System.Drawing.Point(9, 65);
            this.radioButton12.Name = "radioButton12";
            this.radioButton12.Size = new System.Drawing.Size(67, 17);
            this.radioButton12.TabIndex = 2;
            this.radioButton12.TabStop = true;
            this.radioButton12.Text = "Pagados";
            this.radioButton12.UseVisualStyleBackColor = true;
            this.radioButton12.Visible = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.OptPag);
            this.groupBox5.Controls.Add(this.OptPen);
            this.groupBox5.Controls.Add(this.OptTodDoc);
            this.groupBox5.Location = new System.Drawing.Point(554, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(101, 87);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "[ Seleccionar ]";
            // 
            // OptPag
            // 
            this.OptPag.AutoSize = true;
            this.OptPag.Location = new System.Drawing.Point(12, 65);
            this.OptPag.Name = "OptPag";
            this.OptPag.Size = new System.Drawing.Size(67, 17);
            this.OptPag.TabIndex = 13;
            this.OptPag.TabStop = true;
            this.OptPag.Text = "Pagados";
            this.OptPag.UseVisualStyleBackColor = true;
            // 
            // OptPen
            // 
            this.OptPen.AutoSize = true;
            this.OptPen.Location = new System.Drawing.Point(12, 42);
            this.OptPen.Name = "OptPen";
            this.OptPen.Size = new System.Drawing.Size(78, 17);
            this.OptPen.TabIndex = 12;
            this.OptPen.TabStop = true;
            this.OptPen.Text = "Pendientes";
            this.OptPen.UseVisualStyleBackColor = true;
            // 
            // OptTodDoc
            // 
            this.OptTodDoc.AutoSize = true;
            this.OptTodDoc.Location = new System.Drawing.Point(12, 19);
            this.OptTodDoc.Name = "OptTodDoc";
            this.OptTodDoc.Size = new System.Drawing.Size(55, 17);
            this.OptTodDoc.TabIndex = 11;
            this.OptTodDoc.TabStop = true;
            this.OptTodDoc.Text = "Todos";
            this.OptTodDoc.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.OptDol);
            this.groupBox4.Controls.Add(this.OptSol);
            this.groupBox4.Controls.Add(this.OptTod);
            this.groupBox4.Location = new System.Drawing.Point(448, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(101, 87);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "[ Moneda ]";
            // 
            // OptDol
            // 
            this.OptDol.AutoSize = true;
            this.OptDol.Location = new System.Drawing.Point(12, 65);
            this.OptDol.Name = "OptDol";
            this.OptDol.Size = new System.Drawing.Size(79, 17);
            this.OptDol.TabIndex = 10;
            this.OptDol.TabStop = true;
            this.OptDol.Text = "US Dolares";
            this.OptDol.UseVisualStyleBackColor = true;
            // 
            // OptSol
            // 
            this.OptSol.AutoSize = true;
            this.OptSol.Location = new System.Drawing.Point(12, 42);
            this.OptSol.Name = "OptSol";
            this.OptSol.Size = new System.Drawing.Size(51, 17);
            this.OptSol.TabIndex = 9;
            this.OptSol.TabStop = true;
            this.OptSol.Text = "Soles";
            this.OptSol.UseVisualStyleBackColor = true;
            // 
            // OptTod
            // 
            this.OptTod.AutoSize = true;
            this.OptTod.Location = new System.Drawing.Point(12, 19);
            this.OptTod.Name = "OptTod";
            this.OptTod.Size = new System.Drawing.Size(55, 17);
            this.OptTod.TabIndex = 8;
            this.OptTod.TabStop = true;
            this.OptTod.Text = "Todas";
            this.OptTod.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.OptFchReg);
            this.groupBox3.Controls.Add(this.OptFchVen);
            this.groupBox3.Controls.Add(this.OptFchEmi);
            this.groupBox3.Location = new System.Drawing.Point(344, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(101, 87);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "[ Fecha ]";
            // 
            // OptFchReg
            // 
            this.OptFchReg.AutoSize = true;
            this.OptFchReg.Location = new System.Drawing.Point(12, 65);
            this.OptFchReg.Name = "OptFchReg";
            this.OptFchReg.Size = new System.Drawing.Size(88, 17);
            this.OptFchReg.TabIndex = 7;
            this.OptFchReg.TabStop = true;
            this.OptFchReg.Text = "Fch. Registro";
            this.OptFchReg.UseVisualStyleBackColor = true;
            // 
            // OptFchVen
            // 
            this.OptFchVen.AutoSize = true;
            this.OptFchVen.Location = new System.Drawing.Point(12, 42);
            this.OptFchVen.Name = "OptFchVen";
            this.OptFchVen.Size = new System.Drawing.Size(77, 17);
            this.OptFchVen.TabIndex = 6;
            this.OptFchVen.TabStop = true;
            this.OptFchVen.Text = "Fch. Venc.";
            this.OptFchVen.UseVisualStyleBackColor = true;
            // 
            // OptFchEmi
            // 
            this.OptFchEmi.AutoSize = true;
            this.OptFchEmi.Location = new System.Drawing.Point(12, 19);
            this.OptFchEmi.Name = "OptFchEmi";
            this.OptFchEmi.Size = new System.Drawing.Size(85, 17);
            this.OptFchEmi.TabIndex = 5;
            this.OptFchEmi.TabStop = true;
            this.OptFchEmi.Text = "Fch. Emision";
            this.OptFchEmi.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.OptResDet);
            this.groupBox2.Controls.Add(this.OptDet);
            this.groupBox2.Controls.Add(this.OptRes);
            this.groupBox2.Location = new System.Drawing.Point(5, 48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(336, 42);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "[ Tipo de Consulta ]";
            // 
            // OptResDet
            // 
            this.OptResDet.AutoSize = true;
            this.OptResDet.Location = new System.Drawing.Point(225, 20);
            this.OptResDet.Name = "OptResDet";
            this.OptResDet.Size = new System.Drawing.Size(101, 17);
            this.OptResDet.TabIndex = 4;
            this.OptResDet.TabStop = true;
            this.OptResDet.Text = "Detallado x Item";
            this.OptResDet.UseVisualStyleBackColor = true;
            this.OptResDet.CheckedChanged += new System.EventHandler(this.OptResDet_CheckedChanged);
            // 
            // OptDet
            // 
            this.OptDet.AutoSize = true;
            this.OptDet.Location = new System.Drawing.Point(105, 20);
            this.OptDet.Name = "OptDet";
            this.OptDet.Size = new System.Drawing.Size(104, 17);
            this.OptDet.TabIndex = 3;
            this.OptDet.TabStop = true;
            this.OptDet.Text = "Detallado x Doc.";
            this.OptDet.UseVisualStyleBackColor = true;
            this.OptDet.CheckedChanged += new System.EventHandler(this.OptDet_CheckedChanged);
            // 
            // OptRes
            // 
            this.OptRes.AutoSize = true;
            this.OptRes.Location = new System.Drawing.Point(15, 20);
            this.OptRes.Name = "OptRes";
            this.OptRes.Size = new System.Drawing.Size(70, 17);
            this.OptRes.TabIndex = 2;
            this.OptRes.TabStop = true;
            this.OptRes.Text = "Resumen";
            this.OptRes.UseVisualStyleBackColor = true;
            this.OptRes.CheckedChanged += new System.EventHandler(this.OptRes_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TxtFchFin);
            this.groupBox1.Controls.Add(this.TxtFchIni);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 42);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "[ Fecha de Consulta ]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(170, 22);
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
            this.label4.Location = new System.Drawing.Point(5, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "Fch. Inicio";
            // 
            // TxtFchFin
            // 
            this.TxtFchFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchFin.Location = new System.Drawing.Point(245, 19);
            this.TxtFchFin.Name = "TxtFchFin";
            this.TxtFchFin.Size = new System.Drawing.Size(85, 20);
            this.TxtFchFin.TabIndex = 1;
            this.TxtFchFin.ValueChanged += new System.EventHandler(this.TxtFchFin_ValueChanged);
            this.TxtFchFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtFchFin_KeyPress);
            this.TxtFchFin.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtFchFin_KeyUp);
            // 
            // TxtFchIni
            // 
            this.TxtFchIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchIni.Location = new System.Drawing.Point(69, 19);
            this.TxtFchIni.Name = "TxtFchIni";
            this.TxtFchIni.Size = new System.Drawing.Size(85, 20);
            this.TxtFchIni.TabIndex = 0;
            this.TxtFchIni.ValueChanged += new System.EventHandler(this.TxtFchIni_ValueChanged);
            this.TxtFchIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtFchIni_KeyPress);
            this.TxtFchIni.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtFchIni_KeyUp);
            // 
            // c1DockingTabPage2
            // 
            this.c1DockingTabPage2.Controls.Add(this.panel2);
            this.c1DockingTabPage2.Location = new System.Drawing.Point(24, 1);
            this.c1DockingTabPage2.Name = "c1DockingTabPage2";
            this.c1DockingTabPage2.Size = new System.Drawing.Size(1046, 91);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Mas";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.CmdAddIte);
            this.panel2.Controls.Add(this.CmdAddPro);
            this.panel2.Controls.Add(this.CmdDelIte);
            this.panel2.Controls.Add(this.CmdDelPro);
            this.panel2.Controls.Add(this.FgItem);
            this.panel2.Controls.Add(this.FgPro);
            this.panel2.Location = new System.Drawing.Point(3, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1043, 93);
            this.panel2.TabIndex = 3;
            // 
            // CmdAddIte
            // 
            this.CmdAddIte.Location = new System.Drawing.Point(967, 4);
            this.CmdAddIte.Name = "CmdAddIte";
            this.CmdAddIte.Size = new System.Drawing.Size(75, 35);
            this.CmdAddIte.TabIndex = 18;
            this.CmdAddIte.Text = "Agregar     Item";
            this.CmdAddIte.UseVisualStyleBackColor = true;
            this.CmdAddIte.Click += new System.EventHandler(this.CmdAddIte_Click);
            // 
            // CmdAddPro
            // 
            this.CmdAddPro.Location = new System.Drawing.Point(442, 4);
            this.CmdAddPro.Name = "CmdAddPro";
            this.CmdAddPro.Size = new System.Drawing.Size(75, 35);
            this.CmdAddPro.TabIndex = 16;
            this.CmdAddPro.Text = "Agregar Proveedor";
            this.CmdAddPro.UseVisualStyleBackColor = true;
            this.CmdAddPro.Click += new System.EventHandler(this.CmdAddPro_Click);
            // 
            // CmdDelIte
            // 
            this.CmdDelIte.Location = new System.Drawing.Point(967, 42);
            this.CmdDelIte.Name = "CmdDelIte";
            this.CmdDelIte.Size = new System.Drawing.Size(75, 35);
            this.CmdDelIte.TabIndex = 19;
            this.CmdDelIte.Text = "Eliminar     Item";
            this.CmdDelIte.UseVisualStyleBackColor = true;
            this.CmdDelIte.Click += new System.EventHandler(this.CmdDelIte_Click);
            // 
            // CmdDelPro
            // 
            this.CmdDelPro.Location = new System.Drawing.Point(442, 42);
            this.CmdDelPro.Name = "CmdDelPro";
            this.CmdDelPro.Size = new System.Drawing.Size(75, 35);
            this.CmdDelPro.TabIndex = 16;
            this.CmdDelPro.Text = "Eliminar Proveedor";
            this.CmdDelPro.UseVisualStyleBackColor = true;
            this.CmdDelPro.Click += new System.EventHandler(this.CmdDelPro_Click);
            // 
            // FgItem
            // 
            this.FgItem.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgItem.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:6;}\t";
            this.FgItem.Enabled = false;
            this.FgItem.Location = new System.Drawing.Point(530, 5);
            this.FgItem.Name = "FgItem";
            this.FgItem.Rows.DefaultSize = 17;
            this.FgItem.Size = new System.Drawing.Size(436, 79);
            this.FgItem.TabIndex = 2;
            this.FgItem.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgItem_CellButtonClick);
            // 
            // FgPro
            // 
            this.FgPro.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgPro.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:6;}\t";
            this.FgPro.Enabled = false;
            this.FgPro.Location = new System.Drawing.Point(5, 5);
            this.FgPro.Name = "FgPro";
            this.FgPro.Rows.DefaultSize = 17;
            this.FgPro.Size = new System.Drawing.Size(436, 79);
            this.FgPro.TabIndex = 1;
            this.FgPro.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgPro_CellButtonClick);
            // 
            // FrmConsultaCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 560);
            this.Controls.Add(this.Sz1);
            this.Controls.Add(this.ToolHerramientas);
            this.Name = "FrmConsultaCompras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmConsultaCompras";
            this.Activated += new System.EventHandler(this.FrmConsultaCompras_Activated);
            this.Load += new System.EventHandler(this.FrmConsultaCompras_Load);
            this.Resize += new System.EventHandler(this.FrmConsultaCompras_Resize);
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.c1DockingTabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgPro)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ToolHerramientas;
        private System.Windows.Forms.ToolStripButton ToolBuscar;
        private System.Windows.Forms.ToolStripButton ToolExportar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolSalir;
        private C1.Win.C1Sizer.C1Sizer Sz1;
        private C1.Win.C1Command.C1DockingTab c1DockingTab1;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox CboTipPro;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton12;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton OptPag;
        private System.Windows.Forms.RadioButton OptPen;
        private System.Windows.Forms.RadioButton OptTodDoc;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton OptDol;
        private System.Windows.Forms.RadioButton OptSol;
        private System.Windows.Forms.RadioButton OptTod;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton OptFchReg;
        private System.Windows.Forms.RadioButton OptFchVen;
        private System.Windows.Forms.RadioButton OptFchEmi;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton OptDet;
        private System.Windows.Forms.RadioButton OptRes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker TxtFchFin;
        private System.Windows.Forms.DateTimePicker TxtFchIni;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage2;
        private System.Windows.Forms.Panel panel2;
        private C1.Win.C1FlexGrid.C1FlexGrid FgItem;
        private C1.Win.C1FlexGrid.C1FlexGrid FgPro;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ToolImprimir;
        private System.Windows.Forms.RadioButton OptResDet;
        private C1.Win.C1FlexGrid.C1FlexGrid FgDatos;
        private System.Windows.Forms.Button CmdDelIte;
        private System.Windows.Forms.Button CmdDelPro;
        private System.Windows.Forms.Button CmdAddIte;
        private System.Windows.Forms.Button CmdAddPro;
    }
}