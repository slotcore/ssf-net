namespace SIAC_NET_Cooperativa.Formularios
{
    partial class FrmRegServicios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegServicios));
            this.Tab1 = new C1.Win.C1Command.C1DockingTab();
            this.c1DockingTabPage1 = new C1.Win.C1Command.C1DockingTabPage();
            this.Sc01 = new C1.Win.C1Sizer.C1Sizer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.LblNumReg = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.DgLista = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1DockingTabPage2 = new C1.Win.C1Command.C1DockingTabPage();
            this.Sc02 = new C1.Win.C1Sizer.C1Sizer();
            this.c1Sizer2 = new C1.Win.C1Sizer.C1Sizer();
            this.c1Sizer3 = new C1.Win.C1Sizer.C1Sizer();
            this.panel5 = new System.Windows.Forms.Panel();
            this.CmdDelPue = new System.Windows.Forms.Button();
            this.CmdCarPue = new System.Windows.Forms.Button();
            this.FgSocios = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtFchFin = new System.Windows.Forms.DateTimePicker();
            this.TxtFchIni = new System.Windows.Forms.DateTimePicker();
            this.CboTipSer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtImpTotSer = new System.Windows.Forms.TextBox();
            this.TxtObs = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.LblTitulo2 = new System.Windows.Forms.Label();
            this.ToolHerramientas = new System.Windows.Forms.ToolStrip();
            this.ToolNuevo = new System.Windows.Forms.ToolStripButton();
            this.ToolModificar = new System.Windows.Forms.ToolStripButton();
            this.ToolEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolGrabar = new System.Windows.Forms.ToolStripButton();
            this.ToolCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolImprimir = new System.Windows.Forms.ToolStripButton();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.Tab1)).BeginInit();
            this.Tab1.SuspendLayout();
            this.c1DockingTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sc01)).BeginInit();
            this.Sc01.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).BeginInit();
            this.c1DockingTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sc02)).BeginInit();
            this.Sc02.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer2)).BeginInit();
            this.c1Sizer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer3)).BeginInit();
            this.c1Sizer3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgSocios)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.ToolHerramientas.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tab1
            // 
            this.Tab1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.Tab1.Controls.Add(this.c1DockingTabPage1);
            this.Tab1.Controls.Add(this.c1DockingTabPage2);
            this.Tab1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tab1.ForeColor = System.Drawing.Color.Black;
            this.Tab1.Location = new System.Drawing.Point(3, 42);
            this.Tab1.Name = "Tab1";
            this.Tab1.SelectedIndex = 1;
            this.Tab1.Size = new System.Drawing.Size(919, 561);
            this.Tab1.TabAreaBackColor = System.Drawing.Color.White;
            this.Tab1.TabIndex = 30;
            this.Tab1.TabsSpacing = -10;
            this.Tab1.TabStyle = C1.Win.C1Command.TabStyleEnum.Sloping;
            this.Tab1.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            this.Tab1.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            this.Tab1.SelectedIndexChanging += new C1.Win.C1Command.SelectedIndexChangingEventHandler(this.Tab1_SelectedIndexChanging);
            // 
            // c1DockingTabPage1
            // 
            this.c1DockingTabPage1.BackColor = System.Drawing.Color.White;
            this.c1DockingTabPage1.Controls.Add(this.Sc01);
            this.c1DockingTabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.c1DockingTabPage1.ForeColor = System.Drawing.Color.Black;
            this.c1DockingTabPage1.Location = new System.Drawing.Point(29, 1);
            this.c1DockingTabPage1.Name = "c1DockingTabPage1";
            this.c1DockingTabPage1.Size = new System.Drawing.Size(889, 559);
            this.c1DockingTabPage1.TabIndex = 0;
            this.c1DockingTabPage1.Text = "Consulta";
            // 
            // Sc01
            // 
            this.Sc01.Controls.Add(this.panel4);
            this.Sc01.Controls.Add(this.panel2);
            this.Sc01.Controls.Add(this.DgLista);
            this.Sc01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sc01.ForeColor = System.Drawing.Color.Black;
            this.Sc01.GridDefinition = "5.00894454382826:False:True;86.7620751341682:False:False;5.36672629695885:False:T" +
    "rue;\t99.1001124859393:False:False;";
            this.Sc01.Location = new System.Drawing.Point(0, 0);
            this.Sc01.Name = "Sc01";
            this.Sc01.Size = new System.Drawing.Size(889, 559);
            this.Sc01.TabIndex = 0;
            this.Sc01.Text = "c1Sizer1";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.LblNumReg);
            this.panel4.Controls.Add(this.label18);
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(4, 525);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(881, 30);
            this.panel4.TabIndex = 2;
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel2.Controls.Add(this.label17);
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(881, 28);
            this.panel2.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(881, 28);
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
            this.DgLista.Size = new System.Drawing.Size(881, 485);
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
            this.c1DockingTabPage2.Size = new System.Drawing.Size(889, 559);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Detalle";
            // 
            // Sc02
            // 
            this.Sc02.Controls.Add(this.c1Sizer2);
            this.Sc02.Controls.Add(this.panel3);
            this.Sc02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sc02.ForeColor = System.Drawing.Color.Black;
            this.Sc02.GridDefinition = "5.00894454382826:False:True;94.2754919499106:False:False;0:False:True;\t99.7750281" +
    "214848:False:False;";
            this.Sc02.Location = new System.Drawing.Point(0, 0);
            this.Sc02.Margin = new System.Windows.Forms.Padding(1);
            this.Sc02.Name = "Sc02";
            this.Sc02.Padding = new System.Windows.Forms.Padding(1);
            this.Sc02.Size = new System.Drawing.Size(889, 559);
            this.Sc02.SplitterWidth = 1;
            this.Sc02.TabIndex = 0;
            this.Sc02.Text = "c1Sizer2";
            // 
            // c1Sizer2
            // 
            this.c1Sizer2.Controls.Add(this.c1Sizer3);
            this.c1Sizer2.Controls.Add(this.panel1);
            this.c1Sizer2.GridDefinition = "18.9753320683112:False:True;80.4554079696395:False:False;\t99.7745208568207:False:" +
    "False;";
            this.c1Sizer2.Location = new System.Drawing.Point(1, 30);
            this.c1Sizer2.Margin = new System.Windows.Forms.Padding(1);
            this.c1Sizer2.Name = "c1Sizer2";
            this.c1Sizer2.Padding = new System.Windows.Forms.Padding(1);
            this.c1Sizer2.Size = new System.Drawing.Size(887, 527);
            this.c1Sizer2.SplitterWidth = 1;
            this.c1Sizer2.TabIndex = 9;
            this.c1Sizer2.Text = "c1Sizer2";
            // 
            // c1Sizer3
            // 
            this.c1Sizer3.Controls.Add(this.panel5);
            this.c1Sizer3.Controls.Add(this.FgSocios);
            this.c1Sizer3.GridDefinition = "89.8584905660377:False:False;9.43396226415094:False:True;\t99.774011299435:False:F" +
    "alse;";
            this.c1Sizer3.Location = new System.Drawing.Point(1, 102);
            this.c1Sizer3.Margin = new System.Windows.Forms.Padding(1);
            this.c1Sizer3.Name = "c1Sizer3";
            this.c1Sizer3.Padding = new System.Windows.Forms.Padding(1);
            this.c1Sizer3.Size = new System.Drawing.Size(885, 424);
            this.c1Sizer3.SplitterWidth = 1;
            this.c1Sizer3.TabIndex = 1;
            this.c1Sizer3.Text = "c1Sizer3";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.CmdDelPue);
            this.panel5.Controls.Add(this.CmdCarPue);
            this.panel5.Location = new System.Drawing.Point(1, 383);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(883, 40);
            this.panel5.TabIndex = 1;
            // 
            // CmdDelPue
            // 
            this.CmdDelPue.Location = new System.Drawing.Point(119, 5);
            this.CmdDelPue.Name = "CmdDelPue";
            this.CmdDelPue.Size = new System.Drawing.Size(106, 31);
            this.CmdDelPue.TabIndex = 7;
            this.CmdDelPue.Text = "Eliminar Puesto";
            this.CmdDelPue.UseVisualStyleBackColor = true;
            // 
            // CmdCarPue
            // 
            this.CmdCarPue.Location = new System.Drawing.Point(12, 5);
            this.CmdCarPue.Name = "CmdCarPue";
            this.CmdCarPue.Size = new System.Drawing.Size(106, 31);
            this.CmdCarPue.TabIndex = 6;
            this.CmdCarPue.Text = "Cargar Puestos";
            this.CmdCarPue.UseVisualStyleBackColor = true;
            this.CmdCarPue.Click += new System.EventHandler(this.CmdCarPue_Click);
            // 
            // FgSocios
            // 
            this.FgSocios.ColumnInfo = "10,1,0,0,0,85,Columns:";
            this.FgSocios.Location = new System.Drawing.Point(1, 1);
            this.FgSocios.Name = "FgSocios";
            this.FgSocios.Rows.DefaultSize = 17;
            this.FgSocios.Size = new System.Drawing.Size(883, 381);
            this.FgSocios.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TxtFchFin);
            this.panel1.Controls.Add(this.TxtFchIni);
            this.panel1.Controls.Add(this.CboTipSer);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TxtImpTotSer);
            this.panel1.Controls.Add(this.TxtObs);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label4);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(885, 100);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(269, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Fch. Termino";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Fch. Inicio";
            // 
            // TxtFchFin
            // 
            this.TxtFchFin.Enabled = false;
            this.TxtFchFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchFin.Location = new System.Drawing.Point(356, 7);
            this.TxtFchFin.Name = "TxtFchFin";
            this.TxtFchFin.Size = new System.Drawing.Size(88, 20);
            this.TxtFchFin.TabIndex = 1;
            this.TxtFchFin.ValueChanged += new System.EventHandler(this.TxtFchFin_ValueChanged);
            this.TxtFchFin.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtFchFin_KeyUp);
            // 
            // TxtFchIni
            // 
            this.TxtFchIni.Enabled = false;
            this.TxtFchIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchIni.Location = new System.Drawing.Point(109, 7);
            this.TxtFchIni.Name = "TxtFchIni";
            this.TxtFchIni.Size = new System.Drawing.Size(88, 20);
            this.TxtFchIni.TabIndex = 0;
            this.TxtFchIni.ValueChanged += new System.EventHandler(this.TxtFchIni_ValueChanged);
            this.TxtFchIni.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtFchIni_KeyUp);
            // 
            // CboTipSer
            // 
            this.CboTipSer.BackColor = System.Drawing.Color.White;
            this.CboTipSer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTipSer.Enabled = false;
            this.CboTipSer.ForeColor = System.Drawing.Color.Black;
            this.CboTipSer.FormattingEnabled = true;
            this.CboTipSer.Location = new System.Drawing.Point(109, 30);
            this.CboTipSer.Name = "CboTipSer";
            this.CboTipSer.Size = new System.Drawing.Size(335, 21);
            this.CboTipSer.TabIndex = 2;
            this.CboTipSer.SelectedValueChanged += new System.EventHandler(this.CboTipSer_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(10, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Glosa";
            // 
            // TxtImpTotSer
            // 
            this.TxtImpTotSer.Enabled = false;
            this.TxtImpTotSer.Location = new System.Drawing.Point(109, 54);
            this.TxtImpTotSer.MaxLength = 8;
            this.TxtImpTotSer.Name = "TxtImpTotSer";
            this.TxtImpTotSer.Size = new System.Drawing.Size(82, 20);
            this.TxtImpTotSer.TabIndex = 3;
            this.TxtImpTotSer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtImpTotSer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtImpTotSer_KeyPress);
            this.TxtImpTotSer.Validated += new System.EventHandler(this.TxtImpTotSer_Validated);
            // 
            // TxtObs
            // 
            this.TxtObs.Enabled = false;
            this.TxtObs.Location = new System.Drawing.Point(109, 77);
            this.TxtObs.MaxLength = 200;
            this.TxtObs.Name = "TxtObs";
            this.TxtObs.Size = new System.Drawing.Size(769, 20);
            this.TxtObs.TabIndex = 4;
            this.TxtObs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtObs_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(10, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Tipo Servicio";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(10, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Importe Total";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel3.Controls.Add(this.LblTitulo2);
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(1, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(887, 28);
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
            this.LblTitulo2.Size = new System.Drawing.Size(887, 28);
            this.LblTitulo2.TabIndex = 0;
            this.LblTitulo2.Text = "DETALLE DEL REGISTRO";
            this.LblTitulo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.ToolHerramientas.Size = new System.Drawing.Size(924, 39);
            this.ToolHerramientas.TabIndex = 29;
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
            this.ToolImprimir.Image = ((System.Drawing.Image)(resources.GetObject("ToolImprimir.Image")));
            this.ToolImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolImprimir.Name = "ToolImprimir";
            this.ToolImprimir.Size = new System.Drawing.Size(36, 36);
            this.ToolImprimir.Text = "toolStripButton6";
            this.ToolImprimir.ToolTipText = "Imprimir";
            this.ToolImprimir.Click += new System.EventHandler(this.ToolImprimir_Click);
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
            // FrmRegServicios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 604);
            this.Controls.Add(this.Tab1);
            this.Controls.Add(this.ToolHerramientas);
            this.Name = "FrmRegServicios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmRegServicios";
            this.Activated += new System.EventHandler(this.FrmRegServicios_Activated);
            this.Load += new System.EventHandler(this.FrmRegServicios_Load);
            this.Resize += new System.EventHandler(this.FrmRegServicios_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.Tab1)).EndInit();
            this.Tab1.ResumeLayout(false);
            this.c1DockingTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Sc01)).EndInit();
            this.Sc01.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).EndInit();
            this.c1DockingTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Sc02)).EndInit();
            this.Sc02.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer2)).EndInit();
            this.c1Sizer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer3)).EndInit();
            this.c1Sizer3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgSocios)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Command.C1DockingTab Tab1;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage1;
        private C1.Win.C1Sizer.C1Sizer Sc01;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label LblNumReg;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label17;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid DgLista;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage2;
        private C1.Win.C1Sizer.C1Sizer Sc02;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox CboTipSer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtImpTotSer;
        private System.Windows.Forms.TextBox TxtObs;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label LblTitulo2;
        private System.Windows.Forms.ToolStrip ToolHerramientas;
        private System.Windows.Forms.ToolStripButton ToolNuevo;
        private System.Windows.Forms.ToolStripButton ToolModificar;
        private System.Windows.Forms.ToolStripButton ToolEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolGrabar;
        private System.Windows.Forms.ToolStripButton ToolCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ToolImprimir;
        private System.Windows.Forms.ToolStripButton ToolSalir;
        private C1.Win.C1Sizer.C1Sizer c1Sizer2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker TxtFchFin;
        private System.Windows.Forms.DateTimePicker TxtFchIni;
        private C1.Win.C1Sizer.C1Sizer c1Sizer3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button CmdDelPue;
        private System.Windows.Forms.Button CmdCarPue;
        private C1.Win.C1FlexGrid.C1FlexGrid FgSocios;
    }
}