namespace SSF_NET_Contabilidad.Formularios
{
    partial class FrmInventarioIni
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInventarioIni));
            this.Tab1 = new System.Windows.Forms.TabControl();
            this.c1DockingTabPage1 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.LblNumReg = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PicClos1 = new System.Windows.Forms.PictureBox();
            this.label17 = new System.Windows.Forms.Label();
            this.DgLista = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.inventarioInicialBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.c1DockingTabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DtpFchVigencia = new System.Windows.Forms.DateTimePicker();
            this.TxtNumDoc = new System.Windows.Forms.TextBox();
            this.TxtNumSer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtDescripcion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CboMoneda = new System.Windows.Forms.ComboBox();
            this.kryptonHeaderGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.BtnCargarExcel = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.BtnAgregarItem = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.BtnEliminarItem = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.FgItems = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.inventarioInicialDetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CboAlmacen = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.PicClos2 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.inventarioInicialBindingSource)).BeginInit();
            this.c1DockingTabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).BeginInit();
            this.kryptonHeaderGroup2.Panel.SuspendLayout();
            this.kryptonHeaderGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inventarioInicialDetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicClos2)).BeginInit();
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
            this.Tab1.Size = new System.Drawing.Size(912, 503);
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
            this.c1DockingTabPage1.Size = new System.Drawing.Size(880, 495);
            this.c1DockingTabPage1.TabIndex = 0;
            this.c1DockingTabPage1.Text = "Consulta";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.LblNumReg);
            this.panel4.Controls.Add(this.label18);
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(4, 463);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(874, 30);
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
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel2.Controls.Add(this.PicClos1);
            this.panel2.Controls.Add(this.label17);
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(874, 28);
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
            this.label17.Size = new System.Drawing.Size(874, 28);
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
            this.DgLista.DataSource = this.inventarioInicialBindingSource;
            this.DgLista.FilterBar = true;
            this.DgLista.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.Popup;
            this.DgLista.ForeColor = System.Drawing.Color.Black;
            this.DgLista.GroupByCaption = "Drag a column header here to group by that column";
            this.DgLista.Images.Add(((System.Drawing.Image)(resources.GetObject("DgLista.Images"))));
            this.DgLista.Location = new System.Drawing.Point(4, 36);
            this.DgLista.Name = "DgLista";
            this.DgLista.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.DgLista.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.DgLista.PreviewInfo.ZoomFactor = 75D;
            this.DgLista.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("DgLista.PrintInfo.PageSettings")));
            this.DgLista.Size = new System.Drawing.Size(874, 423);
            this.DgLista.TabIndex = 0;
            this.DgLista.Text = "c1TrueDBGrid1";
            this.DgLista.DoubleClick += new System.EventHandler(this.DgLista_DoubleClick);
            this.DgLista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DgLista_KeyPress);
            this.DgLista.PropBag = resources.GetString("DgLista.PropBag");
            // 
            // inventarioInicialBindingSource
            // 
            this.inventarioInicialBindingSource.DataSource = typeof(SIAC_DATOS.Models.Almacen.InventarioInicial);
            // 
            // c1DockingTabPage2
            // 
            this.c1DockingTabPage2.Controls.Add(this.panel1);
            this.c1DockingTabPage2.Controls.Add(this.panel3);
            this.c1DockingTabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.c1DockingTabPage2.ForeColor = System.Drawing.Color.Black;
            this.c1DockingTabPage2.Location = new System.Drawing.Point(28, 4);
            this.c1DockingTabPage2.Name = "c1DockingTabPage2";
            this.c1DockingTabPage2.Size = new System.Drawing.Size(880, 495);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Detalle";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.DtpFchVigencia);
            this.panel1.Controls.Add(this.TxtNumDoc);
            this.panel1.Controls.Add(this.TxtNumSer);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.TxtDescripcion);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.CboMoneda);
            this.panel1.Controls.Add(this.kryptonHeaderGroup2);
            this.panel1.Controls.Add(this.CboAlmacen);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.PicClos2);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label7);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(2, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 461);
            this.panel1.TabIndex = 0;
            // 
            // DtpFchVigencia
            // 
            this.DtpFchVigencia.Enabled = false;
            this.DtpFchVigencia.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpFchVigencia.Location = new System.Drawing.Point(121, 63);
            this.DtpFchVigencia.Name = "DtpFchVigencia";
            this.DtpFchVigencia.Size = new System.Drawing.Size(200, 20);
            this.DtpFchVigencia.TabIndex = 140;
            // 
            // TxtNumDoc
            // 
            this.TxtNumDoc.BackColor = System.Drawing.Color.White;
            this.TxtNumDoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNumDoc.Enabled = false;
            this.TxtNumDoc.ForeColor = System.Drawing.Color.Black;
            this.TxtNumDoc.Location = new System.Drawing.Point(628, 38);
            this.TxtNumDoc.MaxLength = 10;
            this.TxtNumDoc.Name = "TxtNumDoc";
            this.TxtNumDoc.Size = new System.Drawing.Size(220, 20);
            this.TxtNumDoc.TabIndex = 139;
            this.TxtNumDoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumDoc_KeyPress);
            this.TxtNumDoc.Validated += new System.EventHandler(this.TxtNumDoc_Validated);
            // 
            // TxtNumSer
            // 
            this.TxtNumSer.BackColor = System.Drawing.Color.White;
            this.TxtNumSer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNumSer.Enabled = false;
            this.TxtNumSer.ForeColor = System.Drawing.Color.Black;
            this.TxtNumSer.Location = new System.Drawing.Point(549, 38);
            this.TxtNumSer.MaxLength = 4;
            this.TxtNumSer.Name = "TxtNumSer";
            this.TxtNumSer.Size = new System.Drawing.Size(73, 20);
            this.TxtNumSer.TabIndex = 138;
            this.TxtNumSer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumSer_KeyPress);
            this.TxtNumSer.Validated += new System.EventHandler(this.TxtNumSer_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(435, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 137;
            this.label5.Text = "Nº Documento";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TxtDescripcion
            // 
            this.TxtDescripcion.Enabled = false;
            this.TxtDescripcion.Location = new System.Drawing.Point(121, 37);
            this.TxtDescripcion.Name = "TxtDescripcion";
            this.TxtDescripcion.Size = new System.Drawing.Size(299, 20);
            this.TxtDescripcion.TabIndex = 136;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(435, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 135;
            this.label1.Text = "Moneda";
            // 
            // CboMoneda
            // 
            this.CboMoneda.BackColor = System.Drawing.Color.White;
            this.CboMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMoneda.Enabled = false;
            this.CboMoneda.ForeColor = System.Drawing.Color.Black;
            this.CboMoneda.FormattingEnabled = true;
            this.CboMoneda.Location = new System.Drawing.Point(549, 9);
            this.CboMoneda.Name = "CboMoneda";
            this.CboMoneda.Size = new System.Drawing.Size(299, 21);
            this.CboMoneda.TabIndex = 134;
            // 
            // kryptonHeaderGroup2
            // 
            this.kryptonHeaderGroup2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonHeaderGroup2.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.BtnCargarExcel,
            this.BtnAgregarItem,
            this.BtnEliminarItem});
            this.kryptonHeaderGroup2.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.DockActive;
            this.kryptonHeaderGroup2.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup2.Location = new System.Drawing.Point(3, 89);
            this.kryptonHeaderGroup2.Name = "kryptonHeaderGroup2";
            // 
            // kryptonHeaderGroup2.Panel
            // 
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.FgItems);
            this.kryptonHeaderGroup2.Size = new System.Drawing.Size(869, 369);
            this.kryptonHeaderGroup2.StateNormal.HeaderPrimary.Back.Color1 = System.Drawing.SystemColors.Control;
            this.kryptonHeaderGroup2.StateNormal.HeaderPrimary.Back.Color2 = System.Drawing.SystemColors.Control;
            this.kryptonHeaderGroup2.TabIndex = 133;
            this.kryptonHeaderGroup2.ValuesPrimary.Heading = "Listado de Items";
            this.kryptonHeaderGroup2.ValuesPrimary.Image = null;
            // 
            // BtnCargarExcel
            // 
            this.BtnCargarExcel.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            this.BtnCargarExcel.Image = global::SSF_NET_Contabilidad.Properties.Resources.exporttoxls_16x16;
            this.BtnCargarExcel.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Standalone;
            this.BtnCargarExcel.Text = "Cargar Excel";
            this.BtnCargarExcel.UniqueName = "6D2407A9EE62412FCC964A2ABFC2386B";
            this.BtnCargarExcel.Click += new System.EventHandler(this.BtnCargarExcel_Click);
            // 
            // BtnAgregarItem
            // 
            this.BtnAgregarItem.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            this.BtnAgregarItem.Image = global::SSF_NET_Contabilidad.Properties.Resources.add_16x16;
            this.BtnAgregarItem.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Standalone;
            this.BtnAgregarItem.Text = "Agregar Item";
            this.BtnAgregarItem.UniqueName = "732F8FBA093C4C7A4FA518682797C959";
            // 
            // BtnEliminarItem
            // 
            this.BtnEliminarItem.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            this.BtnEliminarItem.Image = global::SSF_NET_Contabilidad.Properties.Resources.trash_16x16;
            this.BtnEliminarItem.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Standalone;
            this.BtnEliminarItem.Text = "Eliminar Item";
            this.BtnEliminarItem.UniqueName = "21699ADBB6664104808E7FF130F84F81";
            // 
            // FgItems
            // 
            this.FgItems.AllowEditing = false;
            this.FgItems.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.FgItems.AutoClipboard = true;
            this.FgItems.AutoGenerateColumns = false;
            this.FgItems.AutoResize = true;
            this.FgItems.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.FgItems.BackColor = System.Drawing.Color.White;
            this.FgItems.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgItems.ColumnInfo = resources.GetString("FgItems.ColumnInfo");
            this.FgItems.DataSource = this.inventarioInicialDetBindingSource;
            this.FgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FgItems.ForeColor = System.Drawing.Color.Black;
            this.FgItems.Location = new System.Drawing.Point(0, 0);
            this.FgItems.Name = "FgItems";
            this.FgItems.Rows.Count = 1;
            this.FgItems.Rows.DefaultSize = 17;
            this.FgItems.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.FgItems.Size = new System.Drawing.Size(867, 338);
            this.FgItems.StyleInfo = resources.GetString("FgItems.StyleInfo");
            this.FgItems.TabIndex = 12;
            // 
            // inventarioInicialDetBindingSource
            // 
            this.inventarioInicialDetBindingSource.DataSource = typeof(SIAC_DATOS.Models.Almacen.InventarioInicialDet);
            // 
            // CboAlmacen
            // 
            this.CboAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboAlmacen.Enabled = false;
            this.CboAlmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboAlmacen.FormattingEnabled = true;
            this.CboAlmacen.Location = new System.Drawing.Point(121, 9);
            this.CboAlmacen.Name = "CboAlmacen";
            this.CboAlmacen.Size = new System.Drawing.Size(299, 21);
            this.CboAlmacen.TabIndex = 125;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(8, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 13);
            this.label11.TabIndex = 124;
            this.label11.Text = "Almacén";
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
            this.PicClos2.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(8, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 37;
            this.label9.Text = "Fch. Vigencia";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(8, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Descripción";
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
            this.panel3.Size = new System.Drawing.Size(878, 28);
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
            this.LblTitulo2.Size = new System.Drawing.Size(878, 28);
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
            this.ToolHerramientas.Size = new System.Drawing.Size(915, 39);
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
            // FrmInventarioIni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 548);
            this.Controls.Add(this.Tab1);
            this.Controls.Add(this.ToolHerramientas);
            this.Name = "FrmInventarioIni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Contabilidad - Configuración de Valorización";
            this.Activated += new System.EventHandler(this.FrmCostoProduccion_Activated);
            this.Load += new System.EventHandler(this.FrmConfigVal_Load);
            this.Tab1.ResumeLayout(false);
            this.c1DockingTabPage1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicClos1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inventarioInicialBindingSource)).EndInit();
            this.c1DockingTabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).EndInit();
            this.kryptonHeaderGroup2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).EndInit();
            this.kryptonHeaderGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inventarioInicialDetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicClos2)).EndInit();
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
        private System.Windows.Forms.Label LblNumReg;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label17;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid DgLista;
        private System.Windows.Forms.TabPage c1DockingTabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
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
        private System.Windows.Forms.ToolStripButton ToolManFun;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.PictureBox PicClos1;
        private System.Windows.Forms.PictureBox PicClos2;
        private System.Windows.Forms.ComboBox CboAlmacen;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label LblTitulo2;
        private System.Windows.Forms.TextBox TxtDescripcion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CboMoneda;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup2;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup BtnAgregarItem;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup BtnEliminarItem;
        private C1.Win.C1FlexGrid.C1FlexGrid FgItems;
        private System.Windows.Forms.BindingSource inventarioInicialBindingSource;
        private System.Windows.Forms.TextBox TxtNumDoc;
        private System.Windows.Forms.TextBox TxtNumSer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker DtpFchVigencia;
        private System.Windows.Forms.BindingSource inventarioInicialDetBindingSource;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup BtnCargarExcel;
    }
}