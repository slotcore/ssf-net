namespace SIAC_NET_Estacionamientos.Formularios
{
    partial class FrmGeneraCargos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGeneraCargos));
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.DgLista = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1DockingTabPage2 = new C1.Win.C1Command.C1DockingTabPage();
            this.Sc02 = new C1.Win.C1Sizer.C1Sizer();
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.panel5 = new System.Windows.Forms.Panel();
            this.LblNumRec = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtImpBru = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CmdDelPla = new System.Windows.Forms.Button();
            this.txtImpIgv = new System.Windows.Forms.TextBox();
            this.CmdCargar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtImpTot = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CboPla = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtFchIni = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtFchEmi = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtObs = new System.Windows.Forms.TextBox();
            this.CmdBusTra = new System.Windows.Forms.Button();
            this.FgPlacas = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.LblTitulo2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ToolHerramientas = new System.Windows.Forms.ToolStrip();
            this.ToolNuevo = new System.Windows.Forms.ToolStripButton();
            this.ToolModificar = new System.Windows.Forms.ToolStripButton();
            this.ToolEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolGrabar = new System.Windows.Forms.ToolStripButton();
            this.ToolCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolImprimir = new System.Windows.Forms.ToolStripSplitButton();
            this.emitirGuiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            this.LblNumReg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CboMeses = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Sc01 = new C1.Win.C1Sizer.C1Sizer();
            this.c1DockingTabPage1 = new C1.Win.C1Command.C1DockingTabPage();
            this.Tab1 = new C1.Win.C1Command.C1DockingTab();
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).BeginInit();
            this.c1DockingTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sc02)).BeginInit();
            this.Sc02.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
            this.c1Sizer1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgPlacas)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.ToolHerramientas.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sc01)).BeginInit();
            this.Sc01.SuspendLayout();
            this.c1DockingTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tab1)).BeginInit();
            this.Tab1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.White;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(9, 7);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(86, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Nº Registros :";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(824, 28);
            this.label17.TabIndex = 0;
            this.label17.Text = "CONSULTA";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DgLista
            // 
            this.DgLista.BackColor = System.Drawing.Color.White;
            this.DgLista.ForeColor = System.Drawing.Color.Black;
            this.DgLista.GroupByCaption = "Drag a column header here to group by that column";
            this.DgLista.Images.Add(((System.Drawing.Image)(resources.GetObject("DgLista.Images"))));
            this.DgLista.Location = new System.Drawing.Point(4, 36);
            this.DgLista.Name = "DgLista";
            this.DgLista.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.DgLista.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.DgLista.PreviewInfo.ZoomFactor = 75D;
            this.DgLista.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("DgLista.PrintInfo.PageSettings")));
            this.DgLista.Size = new System.Drawing.Size(824, 471);
            this.DgLista.TabIndex = 0;
            this.DgLista.Text = "c1TrueDBGrid1";
            this.DgLista.DoubleClick += new System.EventHandler(this.DgLista_DoubleClick);
            this.DgLista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DgLista_KeyPress);
            this.DgLista.PropBag = resources.GetString("DgLista.PropBag");
            // 
            // c1DockingTabPage2
            // 
            this.c1DockingTabPage2.Controls.Add(this.Sc02);
            this.c1DockingTabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.c1DockingTabPage2.ForeColor = System.Drawing.Color.Black;
            this.c1DockingTabPage2.Location = new System.Drawing.Point(29, 1);
            this.c1DockingTabPage2.Name = "c1DockingTabPage2";
            this.c1DockingTabPage2.Size = new System.Drawing.Size(832, 545);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Detalle";
            // 
            // Sc02
            // 
            this.Sc02.Controls.Add(this.c1Sizer1);
            this.Sc02.Controls.Add(this.panel3);
            this.Sc02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sc02.ForeColor = System.Drawing.Color.Black;
            this.Sc02.GridDefinition = "5.13761467889908:False:True;91.9266055045872:False:False;0:False:True;\t99.0384615" +
    "384615:False:False;";
            this.Sc02.Location = new System.Drawing.Point(0, 0);
            this.Sc02.Name = "Sc02";
            this.Sc02.Size = new System.Drawing.Size(832, 545);
            this.Sc02.TabIndex = 0;
            this.Sc02.Text = "c1Sizer2";
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.Controls.Add(this.panel5);
            this.c1Sizer1.Controls.Add(this.panel1);
            this.c1Sizer1.Controls.Add(this.FgPlacas);
            this.c1Sizer1.ForeColor = System.Drawing.Color.Black;
            this.c1Sizer1.GridDefinition = "14.9700598802395:False:True;73.4530938123752:False:False;10.7784431137725:False:T" +
    "rue;\t0.606796116504854:False:False;98.3009708737864:False:True;0.606796116504854" +
    ":False:False;";
            this.c1Sizer1.Location = new System.Drawing.Point(4, 36);
            this.c1Sizer1.Margin = new System.Windows.Forms.Padding(1);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Padding = new System.Windows.Forms.Padding(1);
            this.c1Sizer1.Size = new System.Drawing.Size(824, 501);
            this.c1Sizer1.SplitterWidth = 1;
            this.c1Sizer1.TabIndex = 10;
            this.c1Sizer1.Text = "c1Sizer1";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.LblNumRec);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.TxtImpBru);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.CmdDelPla);
            this.panel5.Controls.Add(this.txtImpIgv);
            this.panel5.Controls.Add(this.CmdCargar);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.TxtImpTot);
            this.panel5.Controls.Add(this.label19);
            this.panel5.Location = new System.Drawing.Point(1, 446);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(816, 54);
            this.panel5.TabIndex = 116;
            // 
            // LblNumRec
            // 
            this.LblNumRec.BackColor = System.Drawing.Color.Transparent;
            this.LblNumRec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblNumRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumRec.ForeColor = System.Drawing.Color.Black;
            this.LblNumRec.Location = new System.Drawing.Point(192, 26);
            this.LblNumRec.Name = "LblNumRec";
            this.LblNumRec.Size = new System.Drawing.Size(80, 20);
            this.LblNumRec.TabIndex = 120;
            this.LblNumRec.Text = "Nº Cargos";
            this.LblNumRec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label6.Location = new System.Drawing.Point(119, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 119;
            this.label6.Text = "Nº Cargos";
            // 
            // TxtImpBru
            // 
            this.TxtImpBru.Enabled = false;
            this.TxtImpBru.Location = new System.Drawing.Point(462, 26);
            this.TxtImpBru.MaxLength = 8;
            this.TxtImpBru.Name = "TxtImpBru";
            this.TxtImpBru.Size = new System.Drawing.Size(80, 20);
            this.TxtImpBru.TabIndex = 113;
            this.TxtImpBru.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(459, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 114;
            this.label4.Text = "Importe Bruto";
            // 
            // CmdDelPla
            // 
            this.CmdDelPla.Enabled = false;
            this.CmdDelPla.Location = new System.Drawing.Point(357, 15);
            this.CmdDelPla.Name = "CmdDelPla";
            this.CmdDelPla.Size = new System.Drawing.Size(96, 31);
            this.CmdDelPla.TabIndex = 118;
            this.CmdDelPla.Text = "Eliminar Placa";
            this.CmdDelPla.UseVisualStyleBackColor = true;
            this.CmdDelPla.Visible = false;
            // 
            // txtImpIgv
            // 
            this.txtImpIgv.Enabled = false;
            this.txtImpIgv.Location = new System.Drawing.Point(554, 26);
            this.txtImpIgv.MaxLength = 8;
            this.txtImpIgv.Name = "txtImpIgv";
            this.txtImpIgv.Size = new System.Drawing.Size(80, 20);
            this.txtImpIgv.TabIndex = 111;
            this.txtImpIgv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CmdCargar
            // 
            this.CmdCargar.Enabled = false;
            this.CmdCargar.Location = new System.Drawing.Point(10, 7);
            this.CmdCargar.Name = "CmdCargar";
            this.CmdCargar.Size = new System.Drawing.Size(96, 40);
            this.CmdCargar.TabIndex = 117;
            this.CmdCargar.Text = "Cargar";
            this.CmdCargar.UseVisualStyleBackColor = true;
            this.CmdCargar.Click += new System.EventHandler(this.CmdCargar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(552, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 112;
            this.label2.Text = "I.G.V. (            )";
            // 
            // TxtImpTot
            // 
            this.TxtImpTot.Enabled = false;
            this.TxtImpTot.Location = new System.Drawing.Point(646, 26);
            this.TxtImpTot.MaxLength = 8;
            this.TxtImpTot.Name = "TxtImpTot";
            this.TxtImpTot.Size = new System.Drawing.Size(80, 20);
            this.TxtImpTot.TabIndex = 17;
            this.TxtImpTot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(644, 7);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(69, 13);
            this.label19.TabIndex = 110;
            this.label19.Text = "Importe Total";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.CboPla);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.TxtFchIni);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.TxtFchEmi);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.TxtObs);
            this.panel1.Controls.Add(this.CmdBusTra);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(7, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(810, 75);
            this.panel1.TabIndex = 0;
            // 
            // CboPla
            // 
            this.CboPla.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboPla.Enabled = false;
            this.CboPla.FormattingEnabled = true;
            this.CboPla.Location = new System.Drawing.Point(115, 27);
            this.CboPla.Name = "CboPla";
            this.CboPla.Size = new System.Drawing.Size(392, 21);
            this.CboPla.TabIndex = 93;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(7, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 94;
            this.label5.Text = "Playa";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(329, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 122;
            this.label8.Text = "Fch. Inicio";
            // 
            // TxtFchIni
            // 
            this.TxtFchIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchIni.Location = new System.Drawing.Point(401, 4);
            this.TxtFchIni.Name = "TxtFchIni";
            this.TxtFchIni.Size = new System.Drawing.Size(106, 20);
            this.TxtFchIni.TabIndex = 121;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(7, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 120;
            this.label3.Text = "Fch. Emision";
            // 
            // TxtFchEmi
            // 
            this.TxtFchEmi.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchEmi.Location = new System.Drawing.Point(115, 4);
            this.TxtFchEmi.Name = "TxtFchEmi";
            this.TxtFchEmi.Size = new System.Drawing.Size(106, 20);
            this.TxtFchEmi.TabIndex = 119;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(7, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 89;
            this.label7.Text = "Observaciones";
            // 
            // TxtObs
            // 
            this.TxtObs.Enabled = false;
            this.TxtObs.Location = new System.Drawing.Point(115, 50);
            this.TxtObs.MaxLength = 100;
            this.TxtObs.Name = "TxtObs";
            this.TxtObs.Size = new System.Drawing.Size(599, 20);
            this.TxtObs.TabIndex = 3;
            // 
            // CmdBusTra
            // 
            this.CmdBusTra.BackColor = System.Drawing.Color.White;
            this.CmdBusTra.ForeColor = System.Drawing.Color.Black;
            this.CmdBusTra.Image = ((System.Drawing.Image)(resources.GetObject("CmdBusTra.Image")));
            this.CmdBusTra.Location = new System.Drawing.Point(736, 9);
            this.CmdBusTra.Name = "CmdBusTra";
            this.CmdBusTra.Size = new System.Drawing.Size(40, 28);
            this.CmdBusTra.TabIndex = 79;
            this.CmdBusTra.UseVisualStyleBackColor = false;
            this.CmdBusTra.Visible = false;
            // 
            // FgPlacas
            // 
            this.FgPlacas.BackColor = System.Drawing.Color.White;
            this.FgPlacas.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgPlacas.ColumnInfo = "3,1,0,0,0,85,Columns:0{Width:9;}\t1{Width:217;Caption:\"Unidad Medida\";Style:\"TextA" +
    "lign:CenterCenter;\";StyleFixed:\"TextAlign:CenterCenter;\";}\t2{Width:30;}\t";
            this.FgPlacas.ForeColor = System.Drawing.Color.Black;
            this.FgPlacas.Location = new System.Drawing.Point(7, 77);
            this.FgPlacas.Name = "FgPlacas";
            this.FgPlacas.Rows.DefaultSize = 17;
            this.FgPlacas.Size = new System.Drawing.Size(810, 368);
            this.FgPlacas.StyleInfo = resources.GetString("FgPlacas.StyleInfo");
            this.FgPlacas.TabIndex = 115;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel3.Controls.Add(this.LblTitulo2);
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(824, 28);
            this.panel3.TabIndex = 8;
            // 
            // LblTitulo2
            // 
            this.LblTitulo2.BackColor = System.Drawing.Color.Transparent;
            this.LblTitulo2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblTitulo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitulo2.ForeColor = System.Drawing.Color.Black;
            this.LblTitulo2.Location = new System.Drawing.Point(0, 0);
            this.LblTitulo2.Name = "LblTitulo2";
            this.LblTitulo2.Size = new System.Drawing.Size(824, 28);
            this.LblTitulo2.TabIndex = 0;
            this.LblTitulo2.Text = "DETALLE DEL REGISTRO";
            this.LblTitulo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel2.Controls.Add(this.label17);
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(824, 28);
            this.panel2.TabIndex = 1;
            // 
            // ToolHerramientas
            // 
            this.ToolHerramientas.BackColor = System.Drawing.Color.White;
            this.ToolHerramientas.ForeColor = System.Drawing.Color.Black;
            this.ToolHerramientas.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolHerramientas.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ToolHerramientas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolNuevo,
            this.ToolModificar,
            this.ToolEliminar,
            this.toolStripSeparator1,
            this.ToolGrabar,
            this.ToolCancelar,
            this.toolStripSeparator2,
            this.ToolImprimir,
            this.ToolSalir});
            this.ToolHerramientas.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolHerramientas.Location = new System.Drawing.Point(0, 0);
            this.ToolHerramientas.Name = "ToolHerramientas";
            this.ToolHerramientas.Size = new System.Drawing.Size(865, 39);
            this.ToolHerramientas.TabIndex = 46;
            this.ToolHerramientas.Text = "toolStrip1";
            // 
            // ToolNuevo
            // 
            this.ToolNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolNuevo.Image = ((System.Drawing.Image)(resources.GetObject("ToolNuevo.Image")));
            this.ToolNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolNuevo.Name = "ToolNuevo";
            this.ToolNuevo.Size = new System.Drawing.Size(36, 36);
            this.ToolNuevo.Text = "toolStripButton1";
            this.ToolNuevo.ToolTipText = "Agregar registro";
            this.ToolNuevo.Click += new System.EventHandler(this.ToolNuevo_Click);
            // 
            // ToolModificar
            // 
            this.ToolModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolModificar.Image = ((System.Drawing.Image)(resources.GetObject("ToolModificar.Image")));
            this.ToolModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolModificar.Name = "ToolModificar";
            this.ToolModificar.Size = new System.Drawing.Size(36, 36);
            this.ToolModificar.Text = "toolStripButton2";
            this.ToolModificar.ToolTipText = "Editar registro";
            this.ToolModificar.Visible = false;
            this.ToolModificar.Click += new System.EventHandler(this.ToolModificar_Click);
            // 
            // ToolEliminar
            // 
            this.ToolEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolEliminar.Image = ((System.Drawing.Image)(resources.GetObject("ToolEliminar.Image")));
            this.ToolEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolEliminar.Name = "ToolEliminar";
            this.ToolEliminar.Size = new System.Drawing.Size(36, 36);
            this.ToolEliminar.Text = "toolStripButton3";
            this.ToolEliminar.ToolTipText = "Eliminar registro";
            this.ToolEliminar.Click += new System.EventHandler(this.ToolEliminar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // ToolGrabar
            // 
            this.ToolGrabar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolGrabar.Enabled = false;
            this.ToolGrabar.Image = ((System.Drawing.Image)(resources.GetObject("ToolGrabar.Image")));
            this.ToolGrabar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolGrabar.Name = "ToolGrabar";
            this.ToolGrabar.Size = new System.Drawing.Size(36, 36);
            this.ToolGrabar.Text = "toolStripButton4";
            this.ToolGrabar.ToolTipText = "Grabar registro";
            this.ToolGrabar.Click += new System.EventHandler(this.ToolGrabar_Click);
            // 
            // ToolCancelar
            // 
            this.ToolCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolCancelar.Enabled = false;
            this.ToolCancelar.Image = ((System.Drawing.Image)(resources.GetObject("ToolCancelar.Image")));
            this.ToolCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolCancelar.Name = "ToolCancelar";
            this.ToolCancelar.Size = new System.Drawing.Size(36, 36);
            this.ToolCancelar.Text = "toolStripButton5";
            this.ToolCancelar.ToolTipText = "Cancelar";
            this.ToolCancelar.Click += new System.EventHandler(this.ToolCancelar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // ToolImprimir
            // 
            this.ToolImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolImprimir.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emitirGuiaToolStripMenuItem});
            this.ToolImprimir.Image = ((System.Drawing.Image)(resources.GetObject("ToolImprimir.Image")));
            this.ToolImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolImprimir.Name = "ToolImprimir";
            this.ToolImprimir.Size = new System.Drawing.Size(48, 36);
            this.ToolImprimir.Text = "toolStripSplitButton1";
            // 
            // emitirGuiaToolStripMenuItem
            // 
            this.emitirGuiaToolStripMenuItem.Name = "emitirGuiaToolStripMenuItem";
            this.emitirGuiaToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.emitirGuiaToolStripMenuItem.Text = "Listar cargos de playa";
            this.emitirGuiaToolStripMenuItem.Click += new System.EventHandler(this.emitirGuiaToolStripMenuItem_Click);
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
            // LblNumReg
            // 
            this.LblNumReg.AutoSize = true;
            this.LblNumReg.BackColor = System.Drawing.Color.White;
            this.LblNumReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumReg.ForeColor = System.Drawing.Color.Black;
            this.LblNumReg.Location = new System.Drawing.Point(140, 7);
            this.LblNumReg.Name = "LblNumReg";
            this.LblNumReg.Size = new System.Drawing.Size(72, 13);
            this.LblNumReg.TabIndex = 1;
            this.LblNumReg.Text = "LblNumReg";
            this.LblNumReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(395, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Mes trabajo ";
            // 
            // CboMeses
            // 
            this.CboMeses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMeses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboMeses.FormattingEnabled = true;
            this.CboMeses.Location = new System.Drawing.Point(505, 3);
            this.CboMeses.Name = "CboMeses";
            this.CboMeses.Size = new System.Drawing.Size(143, 24);
            this.CboMeses.TabIndex = 10;
            this.CboMeses.SelectedValueChanged += new System.EventHandler(this.CboMeses_SelectedValueChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.CboMeses);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.LblNumReg);
            this.panel4.Controls.Add(this.label18);
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(4, 511);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(824, 30);
            this.panel4.TabIndex = 2;
            // 
            // Sc01
            // 
            this.Sc01.Controls.Add(this.panel4);
            this.Sc01.Controls.Add(this.panel2);
            this.Sc01.Controls.Add(this.DgLista);
            this.Sc01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sc01.ForeColor = System.Drawing.Color.Black;
            this.Sc01.GridDefinition = "5.13761467889908:False:True;86.4220183486239:False:False;5.5045871559633:False:Tr" +
    "ue;\t99.0384615384615:False:False;";
            this.Sc01.Location = new System.Drawing.Point(0, 0);
            this.Sc01.Name = "Sc01";
            this.Sc01.Size = new System.Drawing.Size(832, 545);
            this.Sc01.TabIndex = 0;
            this.Sc01.Text = "c1Sizer1";
            // 
            // c1DockingTabPage1
            // 
            this.c1DockingTabPage1.BackColor = System.Drawing.Color.White;
            this.c1DockingTabPage1.Controls.Add(this.Sc01);
            this.c1DockingTabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.c1DockingTabPage1.ForeColor = System.Drawing.Color.Black;
            this.c1DockingTabPage1.Location = new System.Drawing.Point(29, 1);
            this.c1DockingTabPage1.Name = "c1DockingTabPage1";
            this.c1DockingTabPage1.Size = new System.Drawing.Size(832, 545);
            this.c1DockingTabPage1.TabIndex = 0;
            this.c1DockingTabPage1.Text = "Consulta";
            // 
            // Tab1
            // 
            this.Tab1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.Tab1.Controls.Add(this.c1DockingTabPage1);
            this.Tab1.Controls.Add(this.c1DockingTabPage2);
            this.Tab1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tab1.ForeColor = System.Drawing.Color.Black;
            this.Tab1.Location = new System.Drawing.Point(2, 42);
            this.Tab1.Name = "Tab1";
            this.Tab1.SelectedIndex = 1;
            this.Tab1.Size = new System.Drawing.Size(862, 547);
            this.Tab1.TabAreaBackColor = System.Drawing.Color.White;
            this.Tab1.TabIndex = 47;
            this.Tab1.TabsSpacing = -10;
            this.Tab1.TabStyle = C1.Win.C1Command.TabStyleEnum.Sloping;
            this.Tab1.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            this.Tab1.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            this.Tab1.SelectedIndexChanging += new C1.Win.C1Command.SelectedIndexChangingEventHandler(this.Tab1_SelectedIndexChanging);
            // 
            // FrmGeneraCargos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 590);
            this.Controls.Add(this.ToolHerramientas);
            this.Controls.Add(this.Tab1);
            this.Name = "FrmGeneraCargos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmGeneraCargos";
            this.Activated += new System.EventHandler(this.FrmGeneraCargos_Activated);
            this.Load += new System.EventHandler(this.FrmGeneraCargos_Load);
            this.Resize += new System.EventHandler(this.FrmGeneraCargos_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).EndInit();
            this.c1DockingTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Sc02)).EndInit();
            this.Sc02.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgPlacas)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sc01)).EndInit();
            this.Sc01.ResumeLayout(false);
            this.c1DockingTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tab1)).EndInit();
            this.Tab1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid DgLista;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage2;
        private C1.Win.C1Sizer.C1Sizer Sc02;
        private C1.Win.C1Sizer.C1Sizer c1Sizer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CmdDelPla;
        private System.Windows.Forms.Button CmdCargar;
        private C1.Win.C1FlexGrid.C1FlexGrid FgPlacas;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox TxtImpTot;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtObs;
        private System.Windows.Forms.Button CmdBusTra;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label LblTitulo2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip ToolHerramientas;
        private System.Windows.Forms.ToolStripButton ToolNuevo;
        private System.Windows.Forms.ToolStripButton ToolModificar;
        private System.Windows.Forms.ToolStripButton ToolEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolGrabar;
        private System.Windows.Forms.ToolStripButton ToolCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSplitButton ToolImprimir;
        private System.Windows.Forms.ToolStripMenuItem emitirGuiaToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton ToolSalir;
        private System.Windows.Forms.Label LblNumReg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CboMeses;
        private System.Windows.Forms.Panel panel4;
        private C1.Win.C1Sizer.C1Sizer Sc01;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage1;
        private C1.Win.C1Command.C1DockingTab Tab1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox TxtImpBru;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtImpIgv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker TxtFchIni;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker TxtFchEmi;
        private System.Windows.Forms.ComboBox CboPla;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LblNumRec;
        private System.Windows.Forms.Label label6;
    }
}