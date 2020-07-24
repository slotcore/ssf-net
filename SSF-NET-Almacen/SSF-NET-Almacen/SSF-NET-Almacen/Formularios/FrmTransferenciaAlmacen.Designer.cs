namespace SSF_NET_Almacen.Formularios
{
    partial class FrmTransferenciaAlmacen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTransferenciaAlmacen));
            this.Tab1 = new System.Windows.Forms.TabControl();
            this.c1DockingTabPage1 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.CboMeses = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.LblNumReg = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PicClos1 = new System.Windows.Forms.PictureBox();
            this.label17 = new System.Windows.Forms.Label();
            this.DgLista = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1DockingTabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CboAlmacenOrigen = new System.Windows.Forms.ComboBox();
            this.CboAlmacenDestino = new System.Windows.Forms.ComboBox();
            this.PicClos2 = new System.Windows.Forms.PictureBox();
            this.TxtObs = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.FgItems = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label8 = new System.Windows.Forms.Label();
            this.CboResponsable = new System.Windows.Forms.ComboBox();
            this.TxtNumDoc = new System.Windows.Forms.TextBox();
            this.TxtNumSer = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtFchDoc = new System.Windows.Forms.DateTimePicker();
            this.txtFchIng = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CmdDelItem = new System.Windows.Forms.Button();
            this.CmdAddItem = new System.Windows.Forms.Button();
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
            this.ToolManFun = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            this.Tab1.SuspendLayout();
            this.c1DockingTabPage1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicClos1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).BeginInit();
            this.c1DockingTabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicClos2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.Tab1.Location = new System.Drawing.Point(1, 41);
            this.Tab1.Multiline = true;
            this.Tab1.Name = "Tab1";
            this.Tab1.SelectedIndex = 1;
            this.Tab1.Size = new System.Drawing.Size(942, 503);
            this.Tab1.TabIndex = 13;
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
            this.c1DockingTabPage1.Location = new System.Drawing.Point(28, 4);
            this.c1DockingTabPage1.Name = "c1DockingTabPage1";
            this.c1DockingTabPage1.Size = new System.Drawing.Size(910, 495);
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
            this.panel4.Location = new System.Drawing.Point(4, 463);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(904, 30);
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
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel2.Controls.Add(this.PicClos1);
            this.panel2.Controls.Add(this.label17);
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(904, 28);
            this.panel2.TabIndex = 1;
            // 
            // PicClos1
            // 
            this.PicClos1.Image = ((System.Drawing.Image)(resources.GetObject("PicClos1.Image")));
            this.PicClos1.Location = new System.Drawing.Point(838, -2);
            this.PicClos1.Name = "PicClos1";
            this.PicClos1.Size = new System.Drawing.Size(32, 32);
            this.PicClos1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PicClos1.TabIndex = 5;
            this.PicClos1.TabStop = false;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(904, 28);
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
            this.DgLista.ForeColor = System.Drawing.Color.Black;
            this.DgLista.GroupByCaption = "Drag a column header here to group by that column";
            this.DgLista.Images.Add(((System.Drawing.Image)(resources.GetObject("DgLista.Images"))));
            this.DgLista.Location = new System.Drawing.Point(4, 36);
            this.DgLista.Name = "DgLista";
            this.DgLista.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.DgLista.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.DgLista.PreviewInfo.ZoomFactor = 75D;
            this.DgLista.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("DgLista.PrintInfo.PageSettings")));
            this.DgLista.Size = new System.Drawing.Size(904, 423);
            this.DgLista.TabIndex = 0;
            this.DgLista.Text = "c1TrueDBGrid1";
            this.DgLista.DoubleClick += new System.EventHandler(this.DgLista_DoubleClick);
            this.DgLista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DgLista_KeyPress);
            this.DgLista.PropBag = resources.GetString("DgLista.PropBag");
            // 
            // c1DockingTabPage2
            // 
            this.c1DockingTabPage2.Controls.Add(this.panel1);
            this.c1DockingTabPage2.Controls.Add(this.panel3);
            this.c1DockingTabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.c1DockingTabPage2.ForeColor = System.Drawing.Color.Black;
            this.c1DockingTabPage2.Location = new System.Drawing.Point(28, 4);
            this.c1DockingTabPage2.Name = "c1DockingTabPage2";
            this.c1DockingTabPage2.Size = new System.Drawing.Size(910, 495);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Detalle";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.CboAlmacenOrigen);
            this.panel1.Controls.Add(this.CboAlmacenDestino);
            this.panel1.Controls.Add(this.PicClos2);
            this.panel1.Controls.Add(this.TxtObs);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.FgItems);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.CboResponsable);
            this.panel1.Controls.Add(this.TxtNumDoc);
            this.panel1.Controls.Add(this.TxtNumSer);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TxtFchDoc);
            this.panel1.Controls.Add(this.txtFchIng);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(2, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(875, 461);
            this.panel1.TabIndex = 0;
            // 
            // CboAlmacenOrigen
            // 
            this.CboAlmacenOrigen.BackColor = System.Drawing.Color.White;
            this.CboAlmacenOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboAlmacenOrigen.Enabled = false;
            this.CboAlmacenOrigen.ForeColor = System.Drawing.Color.Black;
            this.CboAlmacenOrigen.FormattingEnabled = true;
            this.CboAlmacenOrigen.Location = new System.Drawing.Point(121, 63);
            this.CboAlmacenOrigen.Name = "CboAlmacenOrigen";
            this.CboAlmacenOrigen.Size = new System.Drawing.Size(299, 21);
            this.CboAlmacenOrigen.TabIndex = 123;
            // 
            // CboAlmacenDestino
            // 
            this.CboAlmacenDestino.BackColor = System.Drawing.Color.White;
            this.CboAlmacenDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboAlmacenDestino.Enabled = false;
            this.CboAlmacenDestino.ForeColor = System.Drawing.Color.Black;
            this.CboAlmacenDestino.FormattingEnabled = true;
            this.CboAlmacenDestino.Location = new System.Drawing.Point(568, 61);
            this.CboAlmacenDestino.Name = "CboAlmacenDestino";
            this.CboAlmacenDestino.Size = new System.Drawing.Size(299, 21);
            this.CboAlmacenDestino.TabIndex = 122;
            // 
            // PicClos2
            // 
            this.PicClos2.Image = ((System.Drawing.Image)(resources.GetObject("PicClos2.Image")));
            this.PicClos2.Location = new System.Drawing.Point(810, 6);
            this.PicClos2.Name = "PicClos2";
            this.PicClos2.Size = new System.Drawing.Size(57, 54);
            this.PicClos2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicClos2.TabIndex = 121;
            this.PicClos2.TabStop = false;
            // 
            // TxtObs
            // 
            this.TxtObs.BackColor = System.Drawing.Color.White;
            this.TxtObs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtObs.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtObs.Enabled = false;
            this.TxtObs.ForeColor = System.Drawing.Color.Black;
            this.TxtObs.Location = new System.Drawing.Point(122, 90);
            this.TxtObs.Multiline = true;
            this.TxtObs.Name = "TxtObs";
            this.TxtObs.Size = new System.Drawing.Size(745, 50);
            this.TxtObs.TabIndex = 15;
            this.TxtObs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtObs_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(8, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 37;
            this.label9.Text = "Observaciones";
            // 
            // FgItems
            // 
            this.FgItems.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.FgItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FgItems.BackColor = System.Drawing.Color.White;
            this.FgItems.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgItems.ColumnInfo = "3,1,0,0,0,85,Columns:0{Width:9;}\t1{Width:217;Caption:\"Unidad Medida\";Style:\"TextA" +
    "lign:CenterCenter;\";StyleFixed:\"TextAlign:CenterCenter;\";}\t2{Width:30;}\t";
            this.FgItems.ForeColor = System.Drawing.Color.Black;
            this.FgItems.Location = new System.Drawing.Point(11, 146);
            this.FgItems.Name = "FgItems";
            this.FgItems.Rows.DefaultSize = 17;
            this.FgItems.Size = new System.Drawing.Size(856, 264);
            this.FgItems.StyleInfo = resources.GetString("FgItems.StyleInfo");
            this.FgItems.TabIndex = 9;
            this.FgItems.RowColChange += new System.EventHandler(this.FgItems_RowColChange);
            this.FgItems.EnterCell += new System.EventHandler(this.FgItems_EnterCell);
            this.FgItems.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgItems_CellButtonClick);
            this.FgItems.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.FgItems_KeyPressEdit);
            this.FgItems.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgItems_CellChanged);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(7, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "Encargado";
            // 
            // CboResponsable
            // 
            this.CboResponsable.BackColor = System.Drawing.Color.White;
            this.CboResponsable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboResponsable.Enabled = false;
            this.CboResponsable.ForeColor = System.Drawing.Color.Black;
            this.CboResponsable.FormattingEnabled = true;
            this.CboResponsable.Location = new System.Drawing.Point(121, 36);
            this.CboResponsable.Name = "CboResponsable";
            this.CboResponsable.Size = new System.Drawing.Size(299, 21);
            this.CboResponsable.TabIndex = 13;
            this.CboResponsable.SelectedValueChanged += new System.EventHandler(this.CboResponsable_SelectedValueChanged);
            // 
            // TxtNumDoc
            // 
            this.TxtNumDoc.BackColor = System.Drawing.Color.White;
            this.TxtNumDoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNumDoc.Enabled = false;
            this.TxtNumDoc.ForeColor = System.Drawing.Color.Black;
            this.TxtNumDoc.Location = new System.Drawing.Point(627, 36);
            this.TxtNumDoc.MaxLength = 10;
            this.TxtNumDoc.Name = "TxtNumDoc";
            this.TxtNumDoc.Size = new System.Drawing.Size(106, 20);
            this.TxtNumDoc.TabIndex = 9;
            this.TxtNumDoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumDoc_KeyPress);
            this.TxtNumDoc.Validated += new System.EventHandler(this.TxtNumDoc_Validated);
            // 
            // TxtNumSer
            // 
            this.TxtNumSer.BackColor = System.Drawing.Color.White;
            this.TxtNumSer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNumSer.Enabled = false;
            this.TxtNumSer.ForeColor = System.Drawing.Color.Black;
            this.TxtNumSer.Location = new System.Drawing.Point(568, 37);
            this.TxtNumSer.MaxLength = 4;
            this.TxtNumSer.Name = "TxtNumSer";
            this.TxtNumSer.Size = new System.Drawing.Size(52, 20);
            this.TxtNumSer.TabIndex = 8;
            this.TxtNumSer.TextChanged += new System.EventHandler(this.TxtNumSer_TextChanged);
            this.TxtNumSer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumSer_KeyPress);
            this.TxtNumSer.Validated += new System.EventHandler(this.TxtNumSer_Validated);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(8, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 18);
            this.label7.TabIndex = 8;
            this.label7.Text = "Almacen Origen";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(454, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Nº Documento";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(454, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "Almacén Destino";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(245, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fch. Documento";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fch. Salida";
            // 
            // TxtFchDoc
            // 
            this.TxtFchDoc.BackColor = System.Drawing.Color.White;
            this.TxtFchDoc.Enabled = false;
            this.TxtFchDoc.ForeColor = System.Drawing.Color.Black;
            this.TxtFchDoc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchDoc.Location = new System.Drawing.Point(337, 10);
            this.TxtFchDoc.Name = "TxtFchDoc";
            this.TxtFchDoc.Size = new System.Drawing.Size(83, 20);
            this.TxtFchDoc.TabIndex = 1;
            this.TxtFchDoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtFchDoc_KeyPress);
            // 
            // txtFchIng
            // 
            this.txtFchIng.BackColor = System.Drawing.Color.White;
            this.txtFchIng.Enabled = false;
            this.txtFchIng.ForeColor = System.Drawing.Color.Black;
            this.txtFchIng.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtFchIng.Location = new System.Drawing.Point(122, 10);
            this.txtFchIng.Name = "txtFchIng";
            this.txtFchIng.Size = new System.Drawing.Size(83, 20);
            this.txtFchIng.TabIndex = 0;
            this.txtFchIng.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFchIng_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.CmdDelItem);
            this.groupBox1.Controls.Add(this.CmdAddItem);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(11, 409);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(856, 47);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            // 
            // CmdDelItem
            // 
            this.CmdDelItem.BackColor = System.Drawing.Color.White;
            this.CmdDelItem.Enabled = false;
            this.CmdDelItem.ForeColor = System.Drawing.Color.Black;
            this.CmdDelItem.Location = new System.Drawing.Point(139, 12);
            this.CmdDelItem.Name = "CmdDelItem";
            this.CmdDelItem.Size = new System.Drawing.Size(109, 32);
            this.CmdDelItem.TabIndex = 11;
            this.CmdDelItem.Text = "Eliminar Item";
            this.CmdDelItem.UseVisualStyleBackColor = false;
            this.CmdDelItem.Click += new System.EventHandler(this.CmdDelItem_Click);
            // 
            // CmdAddItem
            // 
            this.CmdAddItem.BackColor = System.Drawing.Color.White;
            this.CmdAddItem.Enabled = false;
            this.CmdAddItem.ForeColor = System.Drawing.Color.Black;
            this.CmdAddItem.Location = new System.Drawing.Point(24, 12);
            this.CmdAddItem.Name = "CmdAddItem";
            this.CmdAddItem.Size = new System.Drawing.Size(109, 32);
            this.CmdAddItem.TabIndex = 10;
            this.CmdAddItem.Text = "Agregar Item";
            this.CmdAddItem.UseVisualStyleBackColor = false;
            this.CmdAddItem.Click += new System.EventHandler(this.CmdAddItem_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel3.Controls.Add(this.LblTitulo2);
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(1, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(877, 28);
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
            this.LblTitulo2.Size = new System.Drawing.Size(877, 28);
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
            this.ToolImprimir,
            this.ToolManFun,
            this.toolStripSeparator3,
            this.ToolSalir});
            this.ToolHerramientas.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolHerramientas.Location = new System.Drawing.Point(0, 0);
            this.ToolHerramientas.Name = "ToolHerramientas";
            this.ToolHerramientas.Size = new System.Drawing.Size(945, 39);
            this.ToolHerramientas.TabIndex = 12;
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
            // ToolManFun
            // 
            this.ToolManFun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolManFun.Image = ((System.Drawing.Image)(resources.GetObject("ToolManFun.Image")));
            this.ToolManFun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolManFun.Name = "ToolManFun";
            this.ToolManFun.Size = new System.Drawing.Size(36, 36);
            this.ToolManFun.Text = "toolStripButton1";
            this.ToolManFun.ToolTipText = "Manual Funciones";
            this.ToolManFun.Click += new System.EventHandler(this.ToolManFun_Click);
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
            // FrmTransferenciaAlmacen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 548);
            this.Controls.Add(this.Tab1);
            this.Controls.Add(this.ToolHerramientas);
            this.Name = "FrmTransferenciaAlmacen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSalidaAlmacen3";
            this.Activated += new System.EventHandler(this.FrmSalidaAlmacen3_Activated);
            this.Load += new System.EventHandler(this.FrmSalidaAlmacen3_Load);
            this.Resize += new System.EventHandler(this.FrmSalidaAlmacen3_Resize);
            this.Tab1.ResumeLayout(false);
            this.c1DockingTabPage1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicClos1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).EndInit();
            this.c1DockingTabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicClos2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).EndInit();
            this.groupBox1.ResumeLayout(false);
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
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label LblNumReg;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label17;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid DgLista;
        private System.Windows.Forms.TabPage c1DockingTabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TxtObs;
        private System.Windows.Forms.Label label9;
        private C1.Win.C1FlexGrid.C1FlexGrid FgItems;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox CboResponsable;
        private System.Windows.Forms.TextBox TxtNumDoc;
        private System.Windows.Forms.TextBox TxtNumSer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker TxtFchDoc;
        private System.Windows.Forms.DateTimePicker txtFchIng;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CmdDelItem;
        private System.Windows.Forms.Button CmdAddItem;
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
        private System.Windows.Forms.ComboBox CboMeses;
        private System.Windows.Forms.ToolStripButton ToolManFun;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.PictureBox PicClos1;
        private System.Windows.Forms.PictureBox PicClos2;
        private System.Windows.Forms.ComboBox CboAlmacenDestino;
        private System.Windows.Forms.ComboBox CboAlmacenOrigen;
    }
}