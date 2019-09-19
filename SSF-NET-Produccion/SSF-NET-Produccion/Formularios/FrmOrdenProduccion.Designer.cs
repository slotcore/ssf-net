namespace SSF_NET_Produccion.Formularios
{
    partial class FrmOrdenProduccion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrdenProduccion));
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
            this.c1Sizer2 = new C1.Win.C1Sizer.C1Sizer();
            this.panel8 = new System.Windows.Forms.Panel();
            this.CmdAddFch = new System.Windows.Forms.Button();
            this.CmdDelFch = new System.Windows.Forms.Button();
            this.FgLisPro = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel6 = new System.Windows.Forms.Panel();
            this.CmdVerDoc = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.LblIdDocRef = new System.Windows.Forms.Label();
            this.TxtNumDocRef = new System.Windows.Forms.TextBox();
            this.TxtNumSerDocRef = new System.Windows.Forms.TextBox();
            this.CmdBusDocRef = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.CboTipDocRef = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.CboPri = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CboRes = new System.Windows.Forms.ComboBox();
            this.TxtFchEnt = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtFchEmi = new System.Windows.Forms.DateTimePicker();
            this.TxtNumDoc = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.TxtNumSer = new System.Windows.Forms.TextBox();
            this.TxtObs = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
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
            this.ToolMenuImprimir = new System.Windows.Forms.ToolStripSplitButton();
            this.imprimirRecetaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
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
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
            this.c1Sizer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer2)).BeginInit();
            this.c1Sizer2.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgLisPro)).BeginInit();
            this.panel6.SuspendLayout();
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
            this.Tab1.Location = new System.Drawing.Point(0, 40);
            this.Tab1.Name = "Tab1";
            this.Tab1.SelectedIndex = 1;
            this.Tab1.Size = new System.Drawing.Size(913, 537);
            this.Tab1.TabAreaBackColor = System.Drawing.Color.White;
            this.Tab1.TabIndex = 18;
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
            this.c1DockingTabPage1.Size = new System.Drawing.Size(883, 535);
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
            this.Sc01.GridDefinition = "5.23364485981308:False:True;86.1682242990654:False:False;5.60747663551402:False:T" +
    "rue;\t99.0939977349943:False:False;";
            this.Sc01.Location = new System.Drawing.Point(0, 0);
            this.Sc01.Name = "Sc01";
            this.Sc01.Size = new System.Drawing.Size(883, 535);
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
            this.panel4.Location = new System.Drawing.Point(4, 501);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(875, 30);
            this.panel4.TabIndex = 2;
            // 
            // CboMeses
            // 
            this.CboMeses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMeses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboMeses.FormattingEnabled = true;
            this.CboMeses.Location = new System.Drawing.Point(663, 3);
            this.CboMeses.Name = "CboMeses";
            this.CboMeses.Size = new System.Drawing.Size(143, 24);
            this.CboMeses.TabIndex = 8;
            this.CboMeses.SelectedIndexChanged += new System.EventHandler(this.CboMeses_SelectedIndexChanged_1);
            this.CboMeses.SelectedValueChanged += new System.EventHandler(this.CboMeses_SelectedValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(553, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Mes trabajo ";
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
            this.panel2.Size = new System.Drawing.Size(875, 28);
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
            this.label17.Size = new System.Drawing.Size(875, 28);
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
            this.DgLista.Size = new System.Drawing.Size(875, 461);
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
            this.c1DockingTabPage2.Size = new System.Drawing.Size(883, 535);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Detalle";
            // 
            // Sc02
            // 
            this.Sc02.Controls.Add(this.c1Sizer1);
            this.Sc02.Controls.Add(this.panel3);
            this.Sc02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sc02.ForeColor = System.Drawing.Color.Black;
            this.Sc02.GridDefinition = "5.23364485981308:False:True;91.7757009345794:False:False;0:False:True;\t99.0939977" +
    "349943:False:False;";
            this.Sc02.Location = new System.Drawing.Point(0, 0);
            this.Sc02.Name = "Sc02";
            this.Sc02.Size = new System.Drawing.Size(883, 535);
            this.Sc02.TabIndex = 0;
            this.Sc02.Text = "c1Sizer2";
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.Controls.Add(this.c1Sizer2);
            this.c1Sizer1.ForeColor = System.Drawing.Color.Black;
            this.c1Sizer1.GridDefinition = "0.610997963340122:False:True;96.7413441955193:False:False;0.610997963340122:False" +
    ":True;\t0.228571428571429:False:True;98.4:False:False;0.228571428571429:False:Tru" +
    "e;";
            this.c1Sizer1.Location = new System.Drawing.Point(4, 36);
            this.c1Sizer1.Margin = new System.Windows.Forms.Padding(1);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Padding = new System.Windows.Forms.Padding(1);
            this.c1Sizer1.Size = new System.Drawing.Size(875, 491);
            this.c1Sizer1.TabIndex = 10;
            this.c1Sizer1.Text = "c1Sizer1";
            // 
            // c1Sizer2
            // 
            this.c1Sizer2.Controls.Add(this.panel8);
            this.c1Sizer2.Controls.Add(this.FgLisPro);
            this.c1Sizer2.Controls.Add(this.panel6);
            this.c1Sizer2.GridDefinition = "36.2105263157895:False:True;26.7368421052632:False:False;3.78947368421053:False:T" +
    "rue;22.1052631578947:False:False;6.94736842105263:False:True;\t99.5386389850058:F" +
    "alse:False;";
            this.c1Sizer2.Location = new System.Drawing.Point(7, 8);
            this.c1Sizer2.Margin = new System.Windows.Forms.Padding(1);
            this.c1Sizer2.Name = "c1Sizer2";
            this.c1Sizer2.Padding = new System.Windows.Forms.Padding(2);
            this.c1Sizer2.Size = new System.Drawing.Size(867, 475);
            this.c1Sizer2.TabIndex = 0;
            this.c1Sizer2.Text = "c1Sizer2";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.CmdAddFch);
            this.panel8.Controls.Add(this.CmdDelFch);
            this.panel8.Location = new System.Drawing.Point(2, 440);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(863, 33);
            this.panel8.TabIndex = 92;
            // 
            // CmdAddFch
            // 
            this.CmdAddFch.Location = new System.Drawing.Point(20, 2);
            this.CmdAddFch.Name = "CmdAddFch";
            this.CmdAddFch.Size = new System.Drawing.Size(113, 30);
            this.CmdAddFch.TabIndex = 4;
            this.CmdAddFch.Text = "Agregar Item";
            this.CmdAddFch.UseVisualStyleBackColor = true;
            // 
            // CmdDelFch
            // 
            this.CmdDelFch.AutoSize = true;
            this.CmdDelFch.Location = new System.Drawing.Point(139, 2);
            this.CmdDelFch.Name = "CmdDelFch";
            this.CmdDelFch.Size = new System.Drawing.Size(113, 30);
            this.CmdDelFch.TabIndex = 5;
            this.CmdDelFch.Text = "Eliminar Item";
            this.CmdDelFch.UseVisualStyleBackColor = true;
            // 
            // FgLisPro
            // 
            this.FgLisPro.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgLisPro.ColumnInfo = "4,1,0,0,0,85,Columns:0{Width:10;}\t";
            this.FgLisPro.Location = new System.Drawing.Point(2, 178);
            this.FgLisPro.Name = "FgLisPro";
            this.FgLisPro.Rows.DefaultSize = 17;
            this.FgLisPro.Size = new System.Drawing.Size(863, 258);
            this.FgLisPro.TabIndex = 84;
            this.FgLisPro.RowColChange += new System.EventHandler(this.FgLisPro_RowColChange);
            this.FgLisPro.EnterCell += new System.EventHandler(this.FgLisPro_EnterCell);
            this.FgLisPro.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.FgLisPro_KeyPressEdit);
            this.FgLisPro.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgLisPro_CellChanged);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.CmdVerDoc);
            this.panel6.Controls.Add(this.label10);
            this.panel6.Controls.Add(this.LblIdDocRef);
            this.panel6.Controls.Add(this.TxtNumDocRef);
            this.panel6.Controls.Add(this.TxtNumSerDocRef);
            this.panel6.Controls.Add(this.CmdBusDocRef);
            this.panel6.Controls.Add(this.label13);
            this.panel6.Controls.Add(this.CboTipDocRef);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.CboPri);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.CboRes);
            this.panel6.Controls.Add(this.TxtFchEnt);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.TxtFchEmi);
            this.panel6.Controls.Add(this.TxtNumDoc);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Controls.Add(this.TxtNumSer);
            this.panel6.Controls.Add(this.TxtObs);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.label12);
            this.panel6.Location = new System.Drawing.Point(2, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(863, 172);
            this.panel6.TabIndex = 0;
            // 
            // CmdVerDoc
            // 
            this.CmdVerDoc.Enabled = false;
            this.CmdVerDoc.Image = ((System.Drawing.Image)(resources.GetObject("CmdVerDoc.Image")));
            this.CmdVerDoc.Location = new System.Drawing.Point(828, 51);
            this.CmdVerDoc.Name = "CmdVerDoc";
            this.CmdVerDoc.Size = new System.Drawing.Size(34, 23);
            this.CmdVerDoc.TabIndex = 117;
            this.CmdVerDoc.UseVisualStyleBackColor = true;
            this.CmdVerDoc.Click += new System.EventHandler(this.CmdVerDoc_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label10.Location = new System.Drawing.Point(5, 156);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(193, 13);
            this.label10.TabIndex = 116;
            this.label10.Text = "..:: LISTA DE PRODUCCION ::..";
            // 
            // LblIdDocRef
            // 
            this.LblIdDocRef.AutoSize = true;
            this.LblIdDocRef.BackColor = System.Drawing.Color.White;
            this.LblIdDocRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIdDocRef.ForeColor = System.Drawing.Color.Maroon;
            this.LblIdDocRef.Location = new System.Drawing.Point(666, 36);
            this.LblIdDocRef.Name = "LblIdDocRef";
            this.LblIdDocRef.Size = new System.Drawing.Size(117, 13);
            this.LblIdDocRef.TabIndex = 115;
            this.LblIdDocRef.Text = "Nº Documento Ref.";
            this.LblIdDocRef.Visible = false;
            // 
            // TxtNumDocRef
            // 
            this.TxtNumDocRef.BackColor = System.Drawing.Color.White;
            this.TxtNumDocRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNumDocRef.Enabled = false;
            this.TxtNumDocRef.ForeColor = System.Drawing.Color.Black;
            this.TxtNumDocRef.Location = new System.Drawing.Point(572, 52);
            this.TxtNumDocRef.MaxLength = 4;
            this.TxtNumDocRef.Name = "TxtNumDocRef";
            this.TxtNumDocRef.ReadOnly = true;
            this.TxtNumDocRef.Size = new System.Drawing.Size(122, 20);
            this.TxtNumDocRef.TabIndex = 5;
            this.TxtNumDocRef.TextChanged += new System.EventHandler(this.TxtNumDocRef_TextChanged);
            this.TxtNumDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumDocRef_KeyPress);
            // 
            // TxtNumSerDocRef
            // 
            this.TxtNumSerDocRef.BackColor = System.Drawing.Color.White;
            this.TxtNumSerDocRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNumSerDocRef.Enabled = false;
            this.TxtNumSerDocRef.ForeColor = System.Drawing.Color.Black;
            this.TxtNumSerDocRef.Location = new System.Drawing.Point(505, 52);
            this.TxtNumSerDocRef.MaxLength = 4;
            this.TxtNumSerDocRef.Name = "TxtNumSerDocRef";
            this.TxtNumSerDocRef.ReadOnly = true;
            this.TxtNumSerDocRef.Size = new System.Drawing.Size(60, 20);
            this.TxtNumSerDocRef.TabIndex = 4;
            this.TxtNumSerDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumSerDocRef_KeyPress);
            // 
            // CmdBusDocRef
            // 
            this.CmdBusDocRef.Enabled = false;
            this.CmdBusDocRef.Location = new System.Drawing.Point(702, 51);
            this.CmdBusDocRef.Name = "CmdBusDocRef";
            this.CmdBusDocRef.Size = new System.Drawing.Size(127, 23);
            this.CmdBusDocRef.TabIndex = 6;
            this.CmdBusDocRef.Text = "Buscar";
            this.CmdBusDocRef.UseVisualStyleBackColor = true;
            this.CmdBusDocRef.Click += new System.EventHandler(this.CmdBusDocRef_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(1, 55);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(106, 13);
            this.label13.TabIndex = 111;
            this.label13.Text = "Tip. Doc. Referencia";
            // 
            // CboTipDocRef
            // 
            this.CboTipDocRef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTipDocRef.Enabled = false;
            this.CboTipDocRef.FormattingEnabled = true;
            this.CboTipDocRef.Location = new System.Drawing.Point(115, 52);
            this.CboTipDocRef.Name = "CboTipDocRef";
            this.CboTipDocRef.Size = new System.Drawing.Size(256, 21);
            this.CboTipDocRef.TabIndex = 3;
            this.CboTipDocRef.SelectedValueChanged += new System.EventHandler(this.CboTipDocRef_SelectedValueChanged);
            this.CboTipDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CboTipDocRef_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(1, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 110;
            this.label9.Text = "Observaciones";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(449, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 109;
            this.label7.Text = "Prioridad";
            // 
            // CboPri
            // 
            this.CboPri.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboPri.Enabled = false;
            this.CboPri.FormattingEnabled = true;
            this.CboPri.Location = new System.Drawing.Point(505, 81);
            this.CboPri.Name = "CboPri";
            this.CboPri.Size = new System.Drawing.Size(106, 21);
            this.CboPri.TabIndex = 8;
            this.CboPri.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CboPri_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(397, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 108;
            this.label5.Text = "Nº Documento Ref.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(1, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 107;
            this.label4.Text = "Responsable";
            // 
            // CboRes
            // 
            this.CboRes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboRes.Enabled = false;
            this.CboRes.FormattingEnabled = true;
            this.CboRes.Location = new System.Drawing.Point(115, 81);
            this.CboRes.Name = "CboRes";
            this.CboRes.Size = new System.Drawing.Size(256, 21);
            this.CboRes.TabIndex = 7;
            this.CboRes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CboRes_KeyPress);
            // 
            // TxtFchEnt
            // 
            this.TxtFchEnt.Enabled = false;
            this.TxtFchEnt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchEnt.Location = new System.Drawing.Point(722, 81);
            this.TxtFchEnt.Name = "TxtFchEnt";
            this.TxtFchEnt.Size = new System.Drawing.Size(106, 20);
            this.TxtFchEnt.TabIndex = 9;
            this.TxtFchEnt.ValueChanged += new System.EventHandler(this.TxtFchEnt_ValueChanged);
            this.TxtFchEnt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtFchEnt_KeyPress);
            this.TxtFchEnt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtFchEnt_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(637, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 106;
            this.label2.Text = "Fch. Entrega";
            // 
            // TxtFchEmi
            // 
            this.TxtFchEmi.Enabled = false;
            this.TxtFchEmi.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchEmi.Location = new System.Drawing.Point(505, 28);
            this.TxtFchEmi.Name = "TxtFchEmi";
            this.TxtFchEmi.Size = new System.Drawing.Size(106, 20);
            this.TxtFchEmi.TabIndex = 2;
            this.TxtFchEmi.ValueChanged += new System.EventHandler(this.TxtFchEmi_ValueChanged);
            this.TxtFchEmi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtFchEmi_KeyPress);
            // 
            // TxtNumDoc
            // 
            this.TxtNumDoc.BackColor = System.Drawing.Color.White;
            this.TxtNumDoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNumDoc.Enabled = false;
            this.TxtNumDoc.ForeColor = System.Drawing.Color.Black;
            this.TxtNumDoc.Location = new System.Drawing.Point(174, 28);
            this.TxtNumDoc.MaxLength = 4;
            this.TxtNumDoc.Name = "TxtNumDoc";
            this.TxtNumDoc.ReadOnly = true;
            this.TxtNumDoc.Size = new System.Drawing.Size(122, 20);
            this.TxtNumDoc.TabIndex = 1;
            this.TxtNumDoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumDoc_KeyPress);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.White;
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(1, 31);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(108, 13);
            this.label22.TabIndex = 105;
            this.label22.Text = "Nº Orden Produccion";
            // 
            // TxtNumSer
            // 
            this.TxtNumSer.BackColor = System.Drawing.Color.White;
            this.TxtNumSer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNumSer.Enabled = false;
            this.TxtNumSer.ForeColor = System.Drawing.Color.Black;
            this.TxtNumSer.Location = new System.Drawing.Point(115, 28);
            this.TxtNumSer.MaxLength = 4;
            this.TxtNumSer.Name = "TxtNumSer";
            this.TxtNumSer.ReadOnly = true;
            this.TxtNumSer.Size = new System.Drawing.Size(53, 20);
            this.TxtNumSer.TabIndex = 0;
            this.TxtNumSer.TextChanged += new System.EventHandler(this.TxtNumSer_TextChanged);
            this.TxtNumSer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumSer_KeyPress);
            // 
            // TxtObs
            // 
            this.TxtObs.BackColor = System.Drawing.Color.White;
            this.TxtObs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtObs.Enabled = false;
            this.TxtObs.ForeColor = System.Drawing.Color.Black;
            this.TxtObs.Location = new System.Drawing.Point(115, 105);
            this.TxtObs.MaxLength = 200;
            this.TxtObs.Multiline = true;
            this.TxtObs.Name = "TxtObs";
            this.TxtObs.Size = new System.Drawing.Size(713, 40);
            this.TxtObs.TabIndex = 10;
            this.TxtObs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtObs_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(430, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 104;
            this.label8.Text = "Fch. Emision";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label12.Location = new System.Drawing.Point(5, 2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(178, 13);
            this.label12.TabIndex = 68;
            this.label12.Text = "..:: DATOS DE LA ORDEN ::..";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel3.Controls.Add(this.LblTitulo2);
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(875, 28);
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
            this.LblTitulo2.Size = new System.Drawing.Size(875, 28);
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
            this.ToolMenuImprimir,
            this.toolStripSeparator3,
            this.ToolSalir});
            this.ToolHerramientas.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolHerramientas.Location = new System.Drawing.Point(0, 0);
            this.ToolHerramientas.Name = "ToolHerramientas";
            this.ToolHerramientas.Size = new System.Drawing.Size(917, 39);
            this.ToolHerramientas.TabIndex = 17;
            this.ToolHerramientas.Text = "toolStrip1";
            this.ToolHerramientas.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ToolHerramientas_ItemClicked);
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
            // ToolMenuImprimir
            // 
            this.ToolMenuImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolMenuImprimir.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imprimirRecetaToolStripMenuItem});
            this.ToolMenuImprimir.Image = ((System.Drawing.Image)(resources.GetObject("ToolMenuImprimir.Image")));
            this.ToolMenuImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolMenuImprimir.Name = "ToolMenuImprimir";
            this.ToolMenuImprimir.Size = new System.Drawing.Size(48, 36);
            this.ToolMenuImprimir.Text = "toolStripSplitButton1";
            // 
            // imprimirRecetaToolStripMenuItem
            // 
            this.imprimirRecetaToolStripMenuItem.Name = "imprimirRecetaToolStripMenuItem";
            this.imprimirRecetaToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.imprimirRecetaToolStripMenuItem.Text = "Imprimir Receta";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
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
            // FrmOrdenProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 581);
            this.Controls.Add(this.Tab1);
            this.Controls.Add(this.ToolHerramientas);
            this.Name = "FrmOrdenProduccion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmOrdenProduccion";
            this.Activated += new System.EventHandler(this.FrmOrdenProduccion_Activated);
            this.Load += new System.EventHandler(this.FrmOrdenProduccion_Load);
            this.Resize += new System.EventHandler(this.FrmOrdenProduccion_Resize);
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
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer2)).EndInit();
            this.c1Sizer2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgLisPro)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
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
        private System.Windows.Forms.ComboBox CboMeses;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label LblNumReg;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label17;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid DgLista;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage2;
        private C1.Win.C1Sizer.C1Sizer Sc02;
        private C1.Win.C1Sizer.C1Sizer c1Sizer1;
        private System.Windows.Forms.Label label12;
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
        private System.Windows.Forms.ToolStripSplitButton ToolMenuImprimir;
        private System.Windows.Forms.ToolStripMenuItem imprimirRecetaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton ToolSalir;
        private System.Windows.Forms.Button CmdAddFch;
        private System.Windows.Forms.Button CmdDelFch;
        private C1.Win.C1Sizer.C1Sizer c1Sizer2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label LblIdDocRef;
        private System.Windows.Forms.TextBox TxtNumDocRef;
        private System.Windows.Forms.TextBox TxtNumSerDocRef;
        private System.Windows.Forms.Button CmdBusDocRef;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox CboTipDocRef;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox CboPri;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CboRes;
        private System.Windows.Forms.DateTimePicker TxtFchEnt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker TxtFchEmi;
        private System.Windows.Forms.TextBox TxtNumDoc;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox TxtNumSer;
        private System.Windows.Forms.TextBox TxtObs;
        private System.Windows.Forms.Label label8;
        private C1.Win.C1FlexGrid.C1FlexGrid FgLisPro;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button CmdVerDoc;
    }
}