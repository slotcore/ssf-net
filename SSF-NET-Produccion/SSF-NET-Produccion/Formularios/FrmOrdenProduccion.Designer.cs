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
            this.Tab1 = new System.Windows.Forms.TabControl();
            this.c1DockingTabPage1 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.CboMeses = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.LblNumReg = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.DgLista = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1DockingTabPage2 = new System.Windows.Forms.TabPage();
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
            this.Tab1.SuspendLayout();
            this.c1DockingTabPage1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).BeginInit();
            this.c1DockingTabPage2.SuspendLayout();
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
            this.Tab1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tab1.Controls.Add(this.c1DockingTabPage1);
            this.Tab1.Controls.Add(this.c1DockingTabPage2);
            this.Tab1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tab1.ForeColor = System.Drawing.Color.Black;
            this.Tab1.Location = new System.Drawing.Point(0, 49);
            this.Tab1.Margin = new System.Windows.Forms.Padding(4);
            this.Tab1.Multiline = true;
            this.Tab1.Name = "Tab1";
            this.Tab1.SelectedIndex = 1;
            this.Tab1.Size = new System.Drawing.Size(1124, 661);
            this.Tab1.TabIndex = 18;
            this.Tab1.SelectedIndexChanged += new System.EventHandler(this.Tab1_SelectedIndexChanging);
            // 
            // c1DockingTabPage1
            // 
            this.c1DockingTabPage1.BackColor = System.Drawing.Color.White;
            this.c1DockingTabPage1.Controls.Add(this.panel4);
            this.c1DockingTabPage1.Controls.Add(this.panel2);
            this.c1DockingTabPage1.Controls.Add(this.DgLista);
            this.c1DockingTabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.c1DockingTabPage1.ForeColor = System.Drawing.Color.Black;
            this.c1DockingTabPage1.Location = new System.Drawing.Point(32, 4);
            this.c1DockingTabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.c1DockingTabPage1.Name = "c1DockingTabPage1";
            this.c1DockingTabPage1.Size = new System.Drawing.Size(1088, 653);
            this.c1DockingTabPage1.TabIndex = 0;
            this.c1DockingTabPage1.Text = "Consulta";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.CboMeses);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.LblNumReg);
            this.panel4.Controls.Add(this.label18);
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(5, 610);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1079, 39);
            this.panel4.TabIndex = 2;
            // 
            // CboMeses
            // 
            this.CboMeses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CboMeses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMeses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboMeses.FormattingEnabled = true;
            this.CboMeses.Location = new System.Drawing.Point(874, 6);
            this.CboMeses.Margin = new System.Windows.Forms.Padding(4);
            this.CboMeses.Name = "CboMeses";
            this.CboMeses.Size = new System.Drawing.Size(189, 28);
            this.CboMeses.TabIndex = 8;
            this.CboMeses.SelectedIndexChanged += new System.EventHandler(this.CboMeses_SelectedIndexChanged_1);
            this.CboMeses.SelectedValueChanged += new System.EventHandler(this.CboMeses_SelectedValueChanged);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(727, 10);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 17);
            this.label11.TabIndex = 7;
            this.label11.Text = "Mes trabajo ";
            // 
            // LblNumReg
            // 
            this.LblNumReg.AutoSize = true;
            this.LblNumReg.BackColor = System.Drawing.Color.White;
            this.LblNumReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumReg.ForeColor = System.Drawing.Color.Black;
            this.LblNumReg.Location = new System.Drawing.Point(187, 9);
            this.LblNumReg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblNumReg.Name = "LblNumReg";
            this.LblNumReg.Size = new System.Drawing.Size(91, 17);
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
            this.label18.Location = new System.Drawing.Point(12, 9);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(109, 17);
            this.label18.TabIndex = 0;
            this.label18.Text = "Nº Registros :";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel2.Controls.Add(this.label17);
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(5, 4);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1079, 28);
            this.panel2.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1079, 28);
            this.label17.TabIndex = 0;
            this.label17.Text = "CONSULTA";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DgLista
            // 
            this.DgLista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgLista.BackColor = System.Drawing.Color.White;
            this.DgLista.CaptionHeight = 19;
            this.DgLista.ForeColor = System.Drawing.Color.Black;
            this.DgLista.GroupByCaption = "Drag a column header here to group by that column";
            this.DgLista.Images.Add(((System.Drawing.Image)(resources.GetObject("DgLista.Images"))));
            this.DgLista.Location = new System.Drawing.Point(5, 37);
            this.DgLista.Margin = new System.Windows.Forms.Padding(4);
            this.DgLista.Name = "DgLista";
            this.DgLista.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.DgLista.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.DgLista.PreviewInfo.ZoomFactor = 75D;
            this.DgLista.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("DgLista.PrintInfo.PageSettings")));
            this.DgLista.RowHeight = 17;
            this.DgLista.Size = new System.Drawing.Size(1079, 571);
            this.DgLista.TabIndex = 0;
            this.DgLista.Text = "c1TrueDBGrid1";
            this.DgLista.DoubleClick += new System.EventHandler(this.DgLista_DoubleClick);
            this.DgLista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DgLista_KeyPress);
            this.DgLista.PropBag = resources.GetString("DgLista.PropBag");
            // 
            // c1DockingTabPage2
            // 
            this.c1DockingTabPage2.Controls.Add(this.panel8);
            this.c1DockingTabPage2.Controls.Add(this.FgLisPro);
            this.c1DockingTabPage2.Controls.Add(this.panel6);
            this.c1DockingTabPage2.Controls.Add(this.panel3);
            this.c1DockingTabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.c1DockingTabPage2.ForeColor = System.Drawing.Color.Black;
            this.c1DockingTabPage2.Location = new System.Drawing.Point(32, 4);
            this.c1DockingTabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.c1DockingTabPage2.Name = "c1DockingTabPage2";
            this.c1DockingTabPage2.Size = new System.Drawing.Size(1088, 653);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Detalle";
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.Controls.Add(this.CmdAddFch);
            this.panel8.Controls.Add(this.CmdDelFch);
            this.panel8.Location = new System.Drawing.Point(12, 599);
            this.panel8.Margin = new System.Windows.Forms.Padding(4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1065, 47);
            this.panel8.TabIndex = 92;
            // 
            // CmdAddFch
            // 
            this.CmdAddFch.Location = new System.Drawing.Point(27, 5);
            this.CmdAddFch.Margin = new System.Windows.Forms.Padding(4);
            this.CmdAddFch.Name = "CmdAddFch";
            this.CmdAddFch.Size = new System.Drawing.Size(151, 37);
            this.CmdAddFch.TabIndex = 4;
            this.CmdAddFch.Text = "Agregar Item";
            this.CmdAddFch.UseVisualStyleBackColor = true;
            // 
            // CmdDelFch
            // 
            this.CmdDelFch.AutoSize = true;
            this.CmdDelFch.Location = new System.Drawing.Point(185, 5);
            this.CmdDelFch.Margin = new System.Windows.Forms.Padding(4);
            this.CmdDelFch.Name = "CmdDelFch";
            this.CmdDelFch.Size = new System.Drawing.Size(151, 37);
            this.CmdDelFch.TabIndex = 5;
            this.CmdDelFch.Text = "Eliminar Item";
            this.CmdDelFch.UseVisualStyleBackColor = true;
            // 
            // FgLisPro
            // 
            this.FgLisPro.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FgLisPro.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgLisPro.ColumnInfo = "4,1,0,0,0,100,Columns:0{Width:10;}\t";
            this.FgLisPro.Location = new System.Drawing.Point(12, 228);
            this.FgLisPro.Margin = new System.Windows.Forms.Padding(4);
            this.FgLisPro.Name = "FgLisPro";
            this.FgLisPro.Rows.DefaultSize = 20;
            this.FgLisPro.Size = new System.Drawing.Size(1065, 363);
            this.FgLisPro.StyleInfo = resources.GetString("FgLisPro.StyleInfo");
            this.FgLisPro.TabIndex = 84;
            this.FgLisPro.RowColChange += new System.EventHandler(this.FgLisPro_RowColChange);
            this.FgLisPro.EnterCell += new System.EventHandler(this.FgLisPro_EnterCell);
            this.FgLisPro.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.FgLisPro_KeyPressEdit);
            this.FgLisPro.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgLisPro_CellChanged);
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.panel6.Location = new System.Drawing.Point(12, 39);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1065, 182);
            this.panel6.TabIndex = 0;
            // 
            // CmdVerDoc
            // 
            this.CmdVerDoc.Enabled = false;
            this.CmdVerDoc.Image = ((System.Drawing.Image)(resources.GetObject("CmdVerDoc.Image")));
            this.CmdVerDoc.Location = new System.Drawing.Point(1009, 63);
            this.CmdVerDoc.Margin = new System.Windows.Forms.Padding(4);
            this.CmdVerDoc.Name = "CmdVerDoc";
            this.CmdVerDoc.Size = new System.Drawing.Size(45, 28);
            this.CmdVerDoc.TabIndex = 117;
            this.CmdVerDoc.UseVisualStyleBackColor = true;
            this.CmdVerDoc.Click += new System.EventHandler(this.CmdVerDoc_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label10.Location = new System.Drawing.Point(7, 192);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(249, 17);
            this.label10.TabIndex = 116;
            this.label10.Text = "..:: LISTA DE PRODUCCION ::..";
            // 
            // LblIdDocRef
            // 
            this.LblIdDocRef.AutoSize = true;
            this.LblIdDocRef.BackColor = System.Drawing.Color.White;
            this.LblIdDocRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIdDocRef.ForeColor = System.Drawing.Color.Maroon;
            this.LblIdDocRef.Location = new System.Drawing.Point(841, 44);
            this.LblIdDocRef.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblIdDocRef.Name = "LblIdDocRef";
            this.LblIdDocRef.Size = new System.Drawing.Size(146, 17);
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
            this.TxtNumDocRef.Location = new System.Drawing.Point(716, 64);
            this.TxtNumDocRef.Margin = new System.Windows.Forms.Padding(4);
            this.TxtNumDocRef.MaxLength = 4;
            this.TxtNumDocRef.Name = "TxtNumDocRef";
            this.TxtNumDocRef.ReadOnly = true;
            this.TxtNumDocRef.Size = new System.Drawing.Size(162, 23);
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
            this.TxtNumSerDocRef.Location = new System.Drawing.Point(626, 64);
            this.TxtNumSerDocRef.Margin = new System.Windows.Forms.Padding(4);
            this.TxtNumSerDocRef.MaxLength = 4;
            this.TxtNumSerDocRef.Name = "TxtNumSerDocRef";
            this.TxtNumSerDocRef.ReadOnly = true;
            this.TxtNumSerDocRef.Size = new System.Drawing.Size(79, 23);
            this.TxtNumSerDocRef.TabIndex = 4;
            this.TxtNumSerDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumSerDocRef_KeyPress);
            // 
            // CmdBusDocRef
            // 
            this.CmdBusDocRef.Enabled = false;
            this.CmdBusDocRef.Location = new System.Drawing.Point(889, 63);
            this.CmdBusDocRef.Margin = new System.Windows.Forms.Padding(4);
            this.CmdBusDocRef.Name = "CmdBusDocRef";
            this.CmdBusDocRef.Size = new System.Drawing.Size(118, 28);
            this.CmdBusDocRef.TabIndex = 6;
            this.CmdBusDocRef.Text = "Buscar";
            this.CmdBusDocRef.UseVisualStyleBackColor = true;
            this.CmdBusDocRef.Click += new System.EventHandler(this.CmdBusDocRef_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(1, 68);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(138, 17);
            this.label13.TabIndex = 111;
            this.label13.Text = "Tip. Doc. Referencia";
            // 
            // CboTipDocRef
            // 
            this.CboTipDocRef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTipDocRef.Enabled = false;
            this.CboTipDocRef.FormattingEnabled = true;
            this.CboTipDocRef.Location = new System.Drawing.Point(153, 64);
            this.CboTipDocRef.Margin = new System.Windows.Forms.Padding(4);
            this.CboTipDocRef.Name = "CboTipDocRef";
            this.CboTipDocRef.Size = new System.Drawing.Size(340, 25);
            this.CboTipDocRef.TabIndex = 3;
            this.CboTipDocRef.SelectedValueChanged += new System.EventHandler(this.CboTipDocRef_SelectedValueChanged);
            this.CboTipDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CboTipDocRef_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(1, 132);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 17);
            this.label9.TabIndex = 110;
            this.label9.Text = "Observaciones";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(529, 103);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 17);
            this.label7.TabIndex = 109;
            this.label7.Text = "Prioridad";
            // 
            // CboPri
            // 
            this.CboPri.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboPri.Enabled = false;
            this.CboPri.FormattingEnabled = true;
            this.CboPri.Location = new System.Drawing.Point(626, 100);
            this.CboPri.Margin = new System.Windows.Forms.Padding(4);
            this.CboPri.Name = "CboPri";
            this.CboPri.Size = new System.Drawing.Size(140, 25);
            this.CboPri.TabIndex = 8;
            this.CboPri.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CboPri_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(529, 69);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 17);
            this.label5.TabIndex = 108;
            this.label5.Text = "Nº Doc. Ref.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(1, 105);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 17);
            this.label4.TabIndex = 107;
            this.label4.Text = "Responsable";
            // 
            // CboRes
            // 
            this.CboRes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboRes.Enabled = false;
            this.CboRes.FormattingEnabled = true;
            this.CboRes.Location = new System.Drawing.Point(153, 100);
            this.CboRes.Margin = new System.Windows.Forms.Padding(4);
            this.CboRes.Name = "CboRes";
            this.CboRes.Size = new System.Drawing.Size(340, 25);
            this.CboRes.TabIndex = 7;
            this.CboRes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CboRes_KeyPress);
            // 
            // TxtFchEnt
            // 
            this.TxtFchEnt.Enabled = false;
            this.TxtFchEnt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchEnt.Location = new System.Drawing.Point(916, 100);
            this.TxtFchEnt.Margin = new System.Windows.Forms.Padding(4);
            this.TxtFchEnt.Name = "TxtFchEnt";
            this.TxtFchEnt.Size = new System.Drawing.Size(140, 23);
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
            this.label2.Location = new System.Drawing.Point(802, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 17);
            this.label2.TabIndex = 106;
            this.label2.Text = "Fch. Entrega";
            // 
            // TxtFchEmi
            // 
            this.TxtFchEmi.Enabled = false;
            this.TxtFchEmi.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchEmi.Location = new System.Drawing.Point(626, 34);
            this.TxtFchEmi.Margin = new System.Windows.Forms.Padding(4);
            this.TxtFchEmi.Name = "TxtFchEmi";
            this.TxtFchEmi.Size = new System.Drawing.Size(140, 23);
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
            this.TxtNumDoc.Location = new System.Drawing.Point(232, 34);
            this.TxtNumDoc.Margin = new System.Windows.Forms.Padding(4);
            this.TxtNumDoc.MaxLength = 4;
            this.TxtNumDoc.Name = "TxtNumDoc";
            this.TxtNumDoc.ReadOnly = true;
            this.TxtNumDoc.Size = new System.Drawing.Size(162, 23);
            this.TxtNumDoc.TabIndex = 1;
            this.TxtNumDoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumDoc_KeyPress);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(1, 38);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(142, 17);
            this.label22.TabIndex = 105;
            this.label22.Text = "Nº Orden Produccion";
            // 
            // TxtNumSer
            // 
            this.TxtNumSer.BackColor = System.Drawing.Color.White;
            this.TxtNumSer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNumSer.Enabled = false;
            this.TxtNumSer.ForeColor = System.Drawing.Color.Black;
            this.TxtNumSer.Location = new System.Drawing.Point(153, 34);
            this.TxtNumSer.Margin = new System.Windows.Forms.Padding(4);
            this.TxtNumSer.MaxLength = 4;
            this.TxtNumSer.Name = "TxtNumSer";
            this.TxtNumSer.ReadOnly = true;
            this.TxtNumSer.Size = new System.Drawing.Size(70, 23);
            this.TxtNumSer.TabIndex = 0;
            this.TxtNumSer.TextChanged += new System.EventHandler(this.TxtNumSer_TextChanged);
            this.TxtNumSer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumSer_KeyPress);
            // 
            // TxtObs
            // 
            this.TxtObs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtObs.BackColor = System.Drawing.Color.White;
            this.TxtObs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtObs.Enabled = false;
            this.TxtObs.ForeColor = System.Drawing.Color.Black;
            this.TxtObs.Location = new System.Drawing.Point(153, 129);
            this.TxtObs.Margin = new System.Windows.Forms.Padding(4);
            this.TxtObs.MaxLength = 200;
            this.TxtObs.Multiline = true;
            this.TxtObs.Name = "TxtObs";
            this.TxtObs.Size = new System.Drawing.Size(904, 49);
            this.TxtObs.TabIndex = 10;
            this.TxtObs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtObs_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(529, 36);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 17);
            this.label8.TabIndex = 104;
            this.label8.Text = "Fch. Emision";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label12.Location = new System.Drawing.Point(7, 2);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(227, 17);
            this.label12.TabIndex = 68;
            this.label12.Text = "..:: DATOS DE LA ORDEN ::..";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel3.Controls.Add(this.LblTitulo2);
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(5, 5);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1079, 28);
            this.panel3.TabIndex = 8;
            // 
            // LblTitulo2
            // 
            this.LblTitulo2.BackColor = System.Drawing.Color.Transparent;
            this.LblTitulo2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblTitulo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitulo2.ForeColor = System.Drawing.Color.Black;
            this.LblTitulo2.Location = new System.Drawing.Point(0, 0);
            this.LblTitulo2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblTitulo2.Name = "LblTitulo2";
            this.LblTitulo2.Size = new System.Drawing.Size(1079, 28);
            this.LblTitulo2.TabIndex = 0;
            this.LblTitulo2.Text = "DETALLE DEL REGISTRO";
            this.LblTitulo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ToolHerramientas
            // 
            this.ToolHerramientas.BackColor = System.Drawing.Color.White;
            this.ToolHerramientas.Font = new System.Drawing.Font("Segoe UI", 9F);
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
            this.ToolHerramientas.Size = new System.Drawing.Size(1130, 39);
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
            this.ToolMenuImprimir.Size = new System.Drawing.Size(51, 36);
            this.ToolMenuImprimir.Text = "toolStripSplitButton1";
            // 
            // imprimirRecetaToolStripMenuItem
            // 
            this.imprimirRecetaToolStripMenuItem.Name = "imprimirRecetaToolStripMenuItem";
            this.imprimirRecetaToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 715);
            this.Controls.Add(this.Tab1);
            this.Controls.Add(this.ToolHerramientas);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmOrdenProduccion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmOrdenProduccion";
            this.Activated += new System.EventHandler(this.FrmOrdenProduccion_Activated);
            this.Load += new System.EventHandler(this.FrmOrdenProduccion_Load);
            this.Resize += new System.EventHandler(this.FrmOrdenProduccion_Resize);
            this.Tab1.ResumeLayout(false);
            this.c1DockingTabPage1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).EndInit();
            this.c1DockingTabPage2.ResumeLayout(false);
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

        private System.Windows.Forms.TabControl Tab1;
        private System.Windows.Forms.TabPage c1DockingTabPage1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox CboMeses;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label LblNumReg;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label17;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid DgLista;
        private System.Windows.Forms.TabPage c1DockingTabPage2;
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