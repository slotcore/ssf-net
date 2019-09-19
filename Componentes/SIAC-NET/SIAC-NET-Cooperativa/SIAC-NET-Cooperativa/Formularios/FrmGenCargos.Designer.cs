namespace SIAC_NET_Cooperativa.Formularios
{
    partial class FrmGenCargos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGenCargos));
            this.Tab1 = new C1.Win.C1Command.C1DockingTab();
            this.c1DockingTabPage1 = new C1.Win.C1Command.C1DockingTabPage();
            this.Sc01 = new C1.Win.C1Sizer.C1Sizer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.CboMeses = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.LblNumReg = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.DgLista = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1DockingTabPage2 = new C1.Win.C1Command.C1DockingTabPage();
            this.Sc02 = new C1.Win.C1Sizer.C1Sizer();
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtNumSoc = new System.Windows.Forms.TextBox();
            this.TxtImpBru = new System.Windows.Forms.TextBox();
            this.TxtImpIgv = new System.Windows.Forms.TextBox();
            this.TxtImpTot = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.FgLisCar = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TxtFchEmi = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CboTipDoc = new System.Windows.Forms.ComboBox();
            this.CmdGenerar = new System.Windows.Forms.Button();
            this.TxtGlosa = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtNumDoc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtAnoTra = new System.Windows.Forms.TextBox();
            this.CboMesTra = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtNumSer = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.LblTitulo2 = new System.Windows.Forms.Label();
            this.ToolHerramientas = new System.Windows.Forms.ToolStrip();
            this.ToolNuevo = new System.Windows.Forms.ToolStripButton();
            this.ToolSDDModificar = new System.Windows.Forms.ToolStripDropDownButton();
            this.modificarRegistroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarNumeracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolModificar = new System.Windows.Forms.ToolStripButton();
            this.ToolEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolGrabar = new System.Windows.Forms.ToolStripButton();
            this.ToolCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolImprimir = new System.Windows.Forms.ToolStripButton();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.CmdCan = new System.Windows.Forms.Button();
            this.CmdIni = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.TxtNumEmp = new System.Windows.Forms.TextBox();
            this.TxtNumMod = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
            this.c1Sizer1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgLisCar)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.ToolHerramientas.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tab1
            // 
            this.Tab1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.Tab1.Controls.Add(this.c1DockingTabPage1);
            this.Tab1.Controls.Add(this.c1DockingTabPage2);
            this.Tab1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tab1.ForeColor = System.Drawing.Color.Black;
            this.Tab1.Location = new System.Drawing.Point(3, 44);
            this.Tab1.Name = "Tab1";
            this.Tab1.SelectedIndex = 1;
            this.Tab1.Size = new System.Drawing.Size(981, 531);
            this.Tab1.TabAreaBackColor = System.Drawing.Color.White;
            this.Tab1.TabIndex = 32;
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
            this.c1DockingTabPage1.Size = new System.Drawing.Size(951, 529);
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
            this.Sc01.GridDefinition = "5.2930056710775:False:True;86.0113421550095:False:False;5.6710775047259:False:Tru" +
    "e;\t99.1587802313354:False:False;";
            this.Sc01.Location = new System.Drawing.Point(0, 0);
            this.Sc01.Name = "Sc01";
            this.Sc01.Size = new System.Drawing.Size(951, 529);
            this.Sc01.TabIndex = 0;
            this.Sc01.Text = "c1Sizer1";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.CboMeses);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.LblNumReg);
            this.panel4.Controls.Add(this.label18);
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(4, 495);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(943, 30);
            this.panel4.TabIndex = 2;
            // 
            // CboMeses
            // 
            this.CboMeses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMeses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboMeses.FormattingEnabled = true;
            this.CboMeses.Location = new System.Drawing.Point(666, 3);
            this.CboMeses.Name = "CboMeses";
            this.CboMeses.Size = new System.Drawing.Size(143, 24);
            this.CboMeses.TabIndex = 5;
            this.CboMeses.SelectedValueChanged += new System.EventHandler(this.CboMeses_SelectedValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(558, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Mes trabajo :";
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
            this.panel2.Size = new System.Drawing.Size(943, 28);
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
            this.label17.Size = new System.Drawing.Size(943, 28);
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
            this.DgLista.Size = new System.Drawing.Size(943, 455);
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
            this.c1DockingTabPage2.Size = new System.Drawing.Size(951, 529);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Detalle";
            // 
            // Sc02
            // 
            this.Sc02.Controls.Add(this.c1Sizer1);
            this.Sc02.Controls.Add(this.panel3);
            this.Sc02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sc02.ForeColor = System.Drawing.Color.Black;
            this.Sc02.GridDefinition = "5.2930056710775:False:True;91.6824196597353:False:False;0:False:True;\t99.15878023" +
    "13354:False:False;";
            this.Sc02.Location = new System.Drawing.Point(0, 0);
            this.Sc02.Name = "Sc02";
            this.Sc02.Size = new System.Drawing.Size(951, 529);
            this.Sc02.TabIndex = 0;
            this.Sc02.Text = "c1Sizer2";
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.Controls.Add(this.panel5);
            this.c1Sizer1.Controls.Add(this.FgLisCar);
            this.c1Sizer1.Controls.Add(this.panel1);
            this.c1Sizer1.ForeColor = System.Drawing.Color.Black;
            this.c1Sizer1.GridDefinition = "20.8247422680412:False:True;66.1855670103093:False:False;9.69072164948454:False:T" +
    "rue;\t99.1516436903499:False:False;";
            this.c1Sizer1.Location = new System.Drawing.Point(4, 36);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Size = new System.Drawing.Size(943, 485);
            this.c1Sizer1.TabIndex = 10;
            this.c1Sizer1.Text = "c1Sizer1";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.TxtNumSoc);
            this.panel5.Controls.Add(this.TxtImpBru);
            this.panel5.Controls.Add(this.TxtImpIgv);
            this.panel5.Controls.Add(this.TxtImpTot);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Location = new System.Drawing.Point(4, 434);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(935, 47);
            this.panel5.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(814, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Imp. Total";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(720, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "IGV (          )";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(532, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Nº Socios";
            // 
            // TxtNumSoc
            // 
            this.TxtNumSoc.Location = new System.Drawing.Point(535, 21);
            this.TxtNumSoc.Name = "TxtNumSoc";
            this.TxtNumSoc.Size = new System.Drawing.Size(88, 20);
            this.TxtNumSoc.TabIndex = 4;
            this.TxtNumSoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtImpBru
            // 
            this.TxtImpBru.Location = new System.Drawing.Point(629, 21);
            this.TxtImpBru.Name = "TxtImpBru";
            this.TxtImpBru.Size = new System.Drawing.Size(88, 20);
            this.TxtImpBru.TabIndex = 3;
            this.TxtImpBru.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtImpIgv
            // 
            this.TxtImpIgv.Location = new System.Drawing.Point(723, 21);
            this.TxtImpIgv.Name = "TxtImpIgv";
            this.TxtImpIgv.Size = new System.Drawing.Size(88, 20);
            this.TxtImpIgv.TabIndex = 2;
            this.TxtImpIgv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtImpTot
            // 
            this.TxtImpTot.Location = new System.Drawing.Point(817, 21);
            this.TxtImpTot.Name = "TxtImpTot";
            this.TxtImpTot.Size = new System.Drawing.Size(88, 20);
            this.TxtImpTot.TabIndex = 1;
            this.TxtImpTot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(626, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Imp. Bruto";
            // 
            // FgLisCar
            // 
            this.FgLisCar.ColumnInfo = "10,1,0,0,0,85,Columns:";
            this.FgLisCar.Location = new System.Drawing.Point(4, 109);
            this.FgLisCar.Name = "FgLisCar";
            this.FgLisCar.Rows.DefaultSize = 17;
            this.FgLisCar.Size = new System.Drawing.Size(935, 321);
            this.FgLisCar.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.TxtFchEmi);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.CboTipDoc);
            this.panel1.Controls.Add(this.CmdGenerar);
            this.panel1.Controls.Add(this.TxtGlosa);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.TxtNumDoc);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TxtAnoTra);
            this.panel1.Controls.Add(this.CboMesTra);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TxtNumSer);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(935, 101);
            this.panel1.TabIndex = 0;
            // 
            // TxtFchEmi
            // 
            this.TxtFchEmi.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchEmi.Location = new System.Drawing.Point(398, 52);
            this.TxtFchEmi.Name = "TxtFchEmi";
            this.TxtFchEmi.Size = new System.Drawing.Size(117, 20);
            this.TxtFchEmi.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(314, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Fch. Emision";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(5, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Tipo Documento ";
            // 
            // CboTipDoc
            // 
            this.CboTipDoc.BackColor = System.Drawing.Color.White;
            this.CboTipDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTipDoc.Enabled = false;
            this.CboTipDoc.ForeColor = System.Drawing.Color.Black;
            this.CboTipDoc.FormattingEnabled = true;
            this.CboTipDoc.Location = new System.Drawing.Point(111, 28);
            this.CboTipDoc.Name = "CboTipDoc";
            this.CboTipDoc.Size = new System.Drawing.Size(404, 21);
            this.CboTipDoc.TabIndex = 22;
            this.CboTipDoc.SelectedValueChanged += new System.EventHandler(this.CboTipDoc_SelectedValueChanged);
            // 
            // CmdGenerar
            // 
            this.CmdGenerar.Location = new System.Drawing.Point(860, 18);
            this.CmdGenerar.Name = "CmdGenerar";
            this.CmdGenerar.Size = new System.Drawing.Size(72, 54);
            this.CmdGenerar.TabIndex = 21;
            this.CmdGenerar.Text = "Generar";
            this.CmdGenerar.UseVisualStyleBackColor = true;
            this.CmdGenerar.Click += new System.EventHandler(this.CmdGenerar_Click);
            // 
            // TxtGlosa
            // 
            this.TxtGlosa.Enabled = false;
            this.TxtGlosa.Location = new System.Drawing.Point(111, 75);
            this.TxtGlosa.MaxLength = 25;
            this.TxtGlosa.Multiline = true;
            this.TxtGlosa.Name = "TxtGlosa";
            this.TxtGlosa.Size = new System.Drawing.Size(638, 21);
            this.TxtGlosa.TabIndex = 19;
            this.TxtGlosa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtGlosa_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(5, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Glosa Documento";
            // 
            // TxtNumDoc
            // 
            this.TxtNumDoc.Enabled = false;
            this.TxtNumDoc.Location = new System.Drawing.Point(178, 52);
            this.TxtNumDoc.MaxLength = 25;
            this.TxtNumDoc.Name = "TxtNumDoc";
            this.TxtNumDoc.Size = new System.Drawing.Size(112, 20);
            this.TxtNumDoc.TabIndex = 3;
            this.TxtNumDoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumDoc_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(5, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Nº Documento";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(315, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Mes Trabajo";
            // 
            // TxtAnoTra
            // 
            this.TxtAnoTra.Enabled = false;
            this.TxtAnoTra.Location = new System.Drawing.Point(111, 3);
            this.TxtAnoTra.MaxLength = 12;
            this.TxtAnoTra.Name = "TxtAnoTra";
            this.TxtAnoTra.Size = new System.Drawing.Size(68, 20);
            this.TxtAnoTra.TabIndex = 1;
            // 
            // CboMesTra
            // 
            this.CboMesTra.BackColor = System.Drawing.Color.White;
            this.CboMesTra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMesTra.Enabled = false;
            this.CboMesTra.ForeColor = System.Drawing.Color.Black;
            this.CboMesTra.FormattingEnabled = true;
            this.CboMesTra.Location = new System.Drawing.Point(398, 4);
            this.CboMesTra.Name = "CboMesTra";
            this.CboMesTra.Size = new System.Drawing.Size(117, 21);
            this.CboMesTra.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(5, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Año Trabajo";
            // 
            // TxtNumSer
            // 
            this.TxtNumSer.Enabled = false;
            this.TxtNumSer.Location = new System.Drawing.Point(111, 52);
            this.TxtNumSer.MaxLength = 25;
            this.TxtNumSer.Name = "TxtNumSer";
            this.TxtNumSer.Size = new System.Drawing.Size(53, 20);
            this.TxtNumSer.TabIndex = 2;
            this.TxtNumSer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumSer_KeyPress);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel3.Controls.Add(this.LblTitulo2);
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(943, 28);
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
            this.LblTitulo2.Size = new System.Drawing.Size(943, 28);
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
            this.ToolSDDModificar,
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
            this.ToolHerramientas.Size = new System.Drawing.Size(996, 39);
            this.ToolHerramientas.TabIndex = 31;
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
            // ToolSDDModificar
            // 
            this.ToolSDDModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolSDDModificar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modificarRegistroToolStripMenuItem,
            this.modificarNumeracionToolStripMenuItem});
            this.ToolSDDModificar.Image = ((System.Drawing.Image)(resources.GetObject("ToolSDDModificar.Image")));
            this.ToolSDDModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolSDDModificar.Name = "ToolSDDModificar";
            this.ToolSDDModificar.Size = new System.Drawing.Size(45, 36);
            this.ToolSDDModificar.Text = "ToolSDDModificar";
            this.ToolSDDModificar.ToolTipText = "Modificar";
            this.ToolSDDModificar.Click += new System.EventHandler(this.ToolSDDModificar_Click);
            // 
            // modificarRegistroToolStripMenuItem
            // 
            this.modificarRegistroToolStripMenuItem.Name = "modificarRegistroToolStripMenuItem";
            this.modificarRegistroToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.modificarRegistroToolStripMenuItem.Text = "Modificar Registro";
            this.modificarRegistroToolStripMenuItem.Click += new System.EventHandler(this.modificarRegistroToolStripMenuItem_Click);
            // 
            // modificarNumeracionToolStripMenuItem
            // 
            this.modificarNumeracionToolStripMenuItem.Name = "modificarNumeracionToolStripMenuItem";
            this.modificarNumeracionToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.modificarNumeracionToolStripMenuItem.Text = "Modificar Numeracion";
            this.modificarNumeracionToolStripMenuItem.Click += new System.EventHandler(this.modificarNumeracionToolStripMenuItem_Click);
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
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.CmdCan);
            this.panel6.Controls.Add(this.CmdIni);
            this.panel6.Controls.Add(this.label15);
            this.panel6.Controls.Add(this.label14);
            this.panel6.Controls.Add(this.TxtNumEmp);
            this.panel6.Controls.Add(this.TxtNumMod);
            this.panel6.Controls.Add(this.label13);
            this.panel6.Controls.Add(this.label12);
            this.panel6.Location = new System.Drawing.Point(151, 459);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(379, 153);
            this.panel6.TabIndex = 33;
            this.panel6.Visible = false;
            // 
            // CmdCan
            // 
            this.CmdCan.Location = new System.Drawing.Point(191, 109);
            this.CmdCan.Name = "CmdCan";
            this.CmdCan.Size = new System.Drawing.Size(89, 31);
            this.CmdCan.TabIndex = 7;
            this.CmdCan.Text = "Cancelar";
            this.CmdCan.UseVisualStyleBackColor = true;
            this.CmdCan.Click += new System.EventHandler(this.CmdCan_Click);
            // 
            // CmdIni
            // 
            this.CmdIni.Location = new System.Drawing.Point(95, 109);
            this.CmdIni.Name = "CmdIni";
            this.CmdIni.Size = new System.Drawing.Size(89, 31);
            this.CmdIni.TabIndex = 6;
            this.CmdIni.Text = "Iniciar";
            this.CmdIni.UseVisualStyleBackColor = true;
            this.CmdIni.Click += new System.EventHandler(this.CmdIni_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(26, 74);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "Empezar en";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(26, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(95, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Remeplazar desde";
            // 
            // TxtNumEmp
            // 
            this.TxtNumEmp.Location = new System.Drawing.Point(135, 71);
            this.TxtNumEmp.Name = "TxtNumEmp";
            this.TxtNumEmp.Size = new System.Drawing.Size(141, 20);
            this.TxtNumEmp.TabIndex = 3;
            this.TxtNumEmp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumEmp_KeyPress);
            // 
            // TxtNumMod
            // 
            this.TxtNumMod.Location = new System.Drawing.Point(135, 45);
            this.TxtNumMod.Name = "TxtNumMod";
            this.TxtNumMod.Size = new System.Drawing.Size(141, 20);
            this.TxtNumMod.TabIndex = 2;
            this.TxtNumMod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumMod_KeyPress);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Navy;
            this.label13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(12, 8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(197, 21);
            this.label13.TabIndex = 1;
            this.label13.Text = "Actualizar Numeracion";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Navy;
            this.label12.Location = new System.Drawing.Point(1, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(374, 30);
            this.label12.TabIndex = 0;
            // 
            // FrmGenCargos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 621);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.Tab1);
            this.Controls.Add(this.ToolHerramientas);
            this.Name = "FrmGenCargos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmGenCargos";
            this.Activated += new System.EventHandler(this.FrmGenCargos_Activated);
            this.Load += new System.EventHandler(this.FrmGenCargos_Load);
            this.Resize += new System.EventHandler(this.FrmGenCargos_Resize);
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
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgLisCar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
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
        private C1.Win.C1Sizer.C1Sizer c1Sizer1;
        private C1.Win.C1FlexGrid.C1FlexGrid FgLisCar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CmdGenerar;
        private System.Windows.Forms.TextBox TxtGlosa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtNumDoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtAnoTra;
        private System.Windows.Forms.ComboBox CboMesTra;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtNumSer;
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
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtNumSoc;
        private System.Windows.Forms.TextBox TxtImpBru;
        private System.Windows.Forms.TextBox TxtImpIgv;
        private System.Windows.Forms.TextBox TxtImpTot;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker TxtFchEmi;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CboTipDoc;
        private System.Windows.Forms.ComboBox CboMeses;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button CmdCan;
        private System.Windows.Forms.Button CmdIni;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox TxtNumEmp;
        private System.Windows.Forms.TextBox TxtNumMod;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolStripDropDownButton ToolSDDModificar;
        private System.Windows.Forms.ToolStripMenuItem modificarRegistroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarNumeracionToolStripMenuItem;
    }
}