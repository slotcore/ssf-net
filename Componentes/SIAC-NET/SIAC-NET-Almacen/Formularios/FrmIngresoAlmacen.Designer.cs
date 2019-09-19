namespace SIAC_NET_Almacen.Formularios
{
    partial class FrmIngresoAlmacen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIngresoAlmacen));
            this.ToolHerramientas = new System.Windows.Forms.ToolStrip();
            this.ToolNuevo = new System.Windows.Forms.ToolStripButton();
            this.ToolModificar = new System.Windows.Forms.ToolStripButton();
            this.ToolEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolGrabar = new System.Windows.Forms.ToolStripButton();
            this.ToolCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolCalendario = new System.Windows.Forms.ToolStripButton();
            this.ToolImprimir = new System.Windows.Forms.ToolStripButton();
            this.ToolExportar = new System.Windows.Forms.ToolStripButton();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            this.Tab1 = new C1.Win.C1Command.C1DockingTab();
            this.c1DockingTabPage1 = new C1.Win.C1Command.C1DockingTabPage();
            this.Sc01 = new C1.Win.C1Sizer.C1Sizer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.CboMeses = new MetroFramework.Controls.MetroComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.LblNumReg = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.DgLista = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1DockingTabPage2 = new C1.Win.C1Command.C1DockingTabPage();
            this.Sc02 = new C1.Win.C1Sizer.C1Sizer();
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TxtObs = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.FgItems = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label8 = new System.Windows.Forms.Label();
            this.CboResponsable = new System.Windows.Forms.ComboBox();
            this.CboAlmacen = new System.Windows.Forms.ComboBox();
            this.CboProveedor = new System.Windows.Forms.ComboBox();
            this.TxtNumDoc = new System.Windows.Forms.TextBox();
            this.TxtNumSer = new System.Windows.Forms.TextBox();
            this.CboTipDoc = new System.Windows.Forms.ComboBox();
            this.CboTipPro = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtFchDoc = new System.Windows.Forms.DateTimePicker();
            this.txtFchIng = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CmdDelItem = new System.Windows.Forms.Button();
            this.CmdAddItem = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.LblTitulo2 = new System.Windows.Forms.Label();
            this.ToolHerramientas.SuspendLayout();
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
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolHerramientas
            // 
            this.ToolHerramientas.BackColor = System.Drawing.Color.White;
            this.ToolHerramientas.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ToolHerramientas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolNuevo,
            this.ToolModificar,
            this.ToolEliminar,
            this.toolStripSeparator1,
            this.ToolGrabar,
            this.ToolCancelar,
            this.toolStripSeparator2,
            this.ToolCalendario,
            this.ToolImprimir,
            this.ToolExportar,
            this.ToolSalir});
            this.ToolHerramientas.Location = new System.Drawing.Point(20, 60);
            this.ToolHerramientas.Name = "ToolHerramientas";
            this.ToolHerramientas.Size = new System.Drawing.Size(878, 39);
            this.ToolHerramientas.TabIndex = 8;
            this.ToolHerramientas.Text = "toolStrip1";
            this.ToolHerramientas.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ToolHerramientas_ItemClicked);
            // 
            // ToolNuevo
            // 
            this.ToolNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolNuevo.Image = ((System.Drawing.Image)(resources.GetObject("ToolNuevo.Image")));
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
            // ToolCalendario
            // 
            this.ToolCalendario.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolCalendario.Image = global::SIAC_NET_Almacen.Properties.Resources.engranaje;
            this.ToolCalendario.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolCalendario.Name = "ToolCalendario";
            this.ToolCalendario.Size = new System.Drawing.Size(36, 36);
            this.ToolCalendario.Text = "Cambiar Mes";
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
            // ToolExportar
            // 
            this.ToolExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolExportar.Image = ((System.Drawing.Image)(resources.GetObject("ToolExportar.Image")));
            this.ToolExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolExportar.Name = "ToolExportar";
            this.ToolExportar.Size = new System.Drawing.Size(36, 36);
            this.ToolExportar.Text = "toolStripButton7";
            this.ToolExportar.ToolTipText = "Exportar Excel";
            this.ToolExportar.Click += new System.EventHandler(this.ToolExportar_Click);
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
            // Tab1
            // 
            this.Tab1.Controls.Add(this.c1DockingTabPage1);
            this.Tab1.Controls.Add(this.c1DockingTabPage2);
            this.Tab1.Location = new System.Drawing.Point(23, 102);
            this.Tab1.Name = "Tab1";
            this.Tab1.SelectedIndex = 1;
            this.Tab1.Size = new System.Drawing.Size(874, 561);
            this.Tab1.TabAreaBackColor = System.Drawing.Color.White;
            this.Tab1.TabIndex = 9;
            this.Tab1.TabsSpacing = -10;
            this.Tab1.TabStyle = C1.Win.C1Command.TabStyleEnum.Sloping;
            this.Tab1.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            this.Tab1.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            this.Tab1.SelectedIndexChanging += new C1.Win.C1Command.SelectedIndexChangingEventHandler(this.Tab1_SelectedIndexChanging);
            this.Tab1.SelectedIndexChanged += new System.EventHandler(this.Tab1_SelectedIndexChanged);
            // 
            // c1DockingTabPage1
            // 
            this.c1DockingTabPage1.BackColor = System.Drawing.Color.White;
            this.c1DockingTabPage1.Controls.Add(this.Sc01);
            this.c1DockingTabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.c1DockingTabPage1.Location = new System.Drawing.Point(1, 24);
            this.c1DockingTabPage1.Name = "c1DockingTabPage1";
            this.c1DockingTabPage1.Size = new System.Drawing.Size(872, 536);
            this.c1DockingTabPage1.TabIndex = 0;
            this.c1DockingTabPage1.Text = "Consulta";
            // 
            // Sc01
            // 
            this.Sc01.Controls.Add(this.panel4);
            this.Sc01.Controls.Add(this.panel2);
            this.Sc01.Controls.Add(this.DgLista);
            this.Sc01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sc01.GridDefinition = "5.22388059701493:False:True;86.1940298507463:False:False;5.59701492537313:False:T" +
    "rue;\t99.0825688073395:False:False;";
            this.Sc01.Location = new System.Drawing.Point(0, 0);
            this.Sc01.Name = "Sc01";
            this.Sc01.Size = new System.Drawing.Size(872, 536);
            this.Sc01.TabIndex = 0;
            this.Sc01.Text = "c1Sizer1";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.CboMeses);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.LblNumReg);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Location = new System.Drawing.Point(4, 502);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(864, 30);
            this.panel4.TabIndex = 2;
            // 
            // CboMeses
            // 
            this.CboMeses.FormattingEnabled = true;
            this.CboMeses.ItemHeight = 23;
            this.CboMeses.Items.AddRange(new object[] {
            "Normal Combobox 1",
            "Normal Combobox 2",
            "Normal Combobox 3",
            "Normal Combobox 4"});
            this.CboMeses.Location = new System.Drawing.Point(687, 1);
            this.CboMeses.Name = "CboMeses";
            this.CboMeses.PromptText = "Prompted ComboBox";
            this.CboMeses.Size = new System.Drawing.Size(175, 29);
            this.CboMeses.TabIndex = 6;
            this.CboMeses.UseSelectable = true;
            this.CboMeses.SelectedIndexChanged += new System.EventHandler(this.CboMeses_SelectedIndexChanged);
            this.CboMeses.SelectedValueChanged += new System.EventHandler(this.CboMeses_SelectedValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Good Times Rg", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Navy;
            this.label11.Location = new System.Drawing.Point(553, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(123, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Mes trabajo ";
            // 
            // LblNumReg
            // 
            this.LblNumReg.AutoSize = true;
            this.LblNumReg.Font = new System.Drawing.Font("Good Times Rg", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumReg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.LblNumReg.Location = new System.Drawing.Point(140, 7);
            this.LblNumReg.Name = "LblNumReg";
            this.LblNumReg.Size = new System.Drawing.Size(102, 13);
            this.LblNumReg.TabIndex = 1;
            this.LblNumReg.Text = "LblNumReg";
            this.LblNumReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Good Times Rg", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Navy;
            this.label18.Location = new System.Drawing.Point(9, 7);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(127, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Nº Registros :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label17);
            this.panel2.Location = new System.Drawing.Point(4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(864, 28);
            this.panel2.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Good Times Rg", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Navy;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(864, 28);
            this.label17.TabIndex = 0;
            this.label17.Text = "CONSULTA";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DgLista
            // 
            this.DgLista.GroupByCaption = "Drag a column header here to group by that column";
            this.DgLista.Images.Add(((System.Drawing.Image)(resources.GetObject("DgLista.Images"))));
            this.DgLista.Location = new System.Drawing.Point(4, 36);
            this.DgLista.Name = "DgLista";
            this.DgLista.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.DgLista.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.DgLista.PreviewInfo.ZoomFactor = 75D;
            this.DgLista.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("DgLista.PrintInfo.PageSettings")));
            this.DgLista.Size = new System.Drawing.Size(864, 462);
            this.DgLista.TabIndex = 0;
            this.DgLista.Text = "c1TrueDBGrid1";
            this.DgLista.Click += new System.EventHandler(this.DgLista_Click);
            this.DgLista.DoubleClick += new System.EventHandler(this.DgLista_DoubleClick);
            this.DgLista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DgLista_KeyPress);
            this.DgLista.PropBag = resources.GetString("DgLista.PropBag");
            // 
            // c1DockingTabPage2
            // 
            this.c1DockingTabPage2.Controls.Add(this.Sc02);
            this.c1DockingTabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.c1DockingTabPage2.Location = new System.Drawing.Point(1, 24);
            this.c1DockingTabPage2.Name = "c1DockingTabPage2";
            this.c1DockingTabPage2.Size = new System.Drawing.Size(872, 536);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Detalle";
            // 
            // Sc02
            // 
            this.Sc02.Controls.Add(this.c1Sizer1);
            this.Sc02.Controls.Add(this.panel3);
            this.Sc02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sc02.GridDefinition = "5.22388059701493:False:True;86.5671641791045:False:False;5.22388059701493:False:T" +
    "rue;\t99.0825688073395:False:False;";
            this.Sc02.Location = new System.Drawing.Point(0, 0);
            this.Sc02.Name = "Sc02";
            this.Sc02.Size = new System.Drawing.Size(872, 536);
            this.Sc02.TabIndex = 0;
            this.Sc02.Text = "c1Sizer2";
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.Controls.Add(this.panel1);
            this.c1Sizer1.GridDefinition = "1.20967741935484:False:False;94.5564516129032:False:True;1.00806451612903:False:F" +
    "alse;\t1.85185185185185:False:False;94.4444444444444:False:True;1.85185185185185:" +
    "False:False;";
            this.c1Sizer1.Location = new System.Drawing.Point(4, 36);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Size = new System.Drawing.Size(864, 496);
            this.c1Sizer1.TabIndex = 10;
            this.c1Sizer1.Text = "c1Sizer1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TxtObs);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.FgItems);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.CboResponsable);
            this.panel1.Controls.Add(this.CboAlmacen);
            this.panel1.Controls.Add(this.CboProveedor);
            this.panel1.Controls.Add(this.TxtNumDoc);
            this.panel1.Controls.Add(this.TxtNumSer);
            this.panel1.Controls.Add(this.CboTipDoc);
            this.panel1.Controls.Add(this.CboTipPro);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TxtFchDoc);
            this.panel1.Controls.Add(this.txtFchIng);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(24, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(816, 469);
            this.panel1.TabIndex = 0;
            // 
            // TxtObs
            // 
            this.TxtObs.Enabled = false;
            this.TxtObs.Location = new System.Drawing.Point(115, 138);
            this.TxtObs.Multiline = true;
            this.TxtObs.Name = "TxtObs";
            this.TxtObs.Size = new System.Drawing.Size(679, 40);
            this.TxtObs.TabIndex = 36;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 142);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 37;
            this.label9.Text = "Observaciones";
            // 
            // FgItems
            // 
            this.FgItems.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgItems.ColumnInfo = "3,1,0,0,0,85,Columns:0{Width:9;}\t1{Width:217;Caption:\"Unidad Medida\";Style:\"TextA" +
    "lign:CenterCenter;\";StyleFixed:\"TextAlign:CenterCenter;\";}\t2{Width:30;}\t";
            this.FgItems.Location = new System.Drawing.Point(19, 186);
            this.FgItems.Name = "FgItems";
            this.FgItems.Rows.DefaultSize = 17;
            this.FgItems.Size = new System.Drawing.Size(776, 226);
            this.FgItems.TabIndex = 9;
            this.FgItems.RowColChange += new System.EventHandler(this.FgItems_RowColChange);
            this.FgItems.EnterCell += new System.EventHandler(this.FgItems_EnterCell);
            this.FgItems.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.FgItems_KeyPressEdit);
            this.FgItems.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgItems_CellChanged);
            this.FgItems.Click += new System.EventHandler(this.FgItems_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(488, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Responsable";
            this.label8.Visible = false;
            // 
            // CboResponsable
            // 
            this.CboResponsable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboResponsable.Enabled = false;
            this.CboResponsable.FormattingEnabled = true;
            this.CboResponsable.Location = new System.Drawing.Point(491, 108);
            this.CboResponsable.Name = "CboResponsable";
            this.CboResponsable.Size = new System.Drawing.Size(65, 21);
            this.CboResponsable.TabIndex = 8;
            this.CboResponsable.Visible = false;
            // 
            // CboAlmacen
            // 
            this.CboAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboAlmacen.Enabled = false;
            this.CboAlmacen.FormattingEnabled = true;
            this.CboAlmacen.Location = new System.Drawing.Point(115, 111);
            this.CboAlmacen.Name = "CboAlmacen";
            this.CboAlmacen.Size = new System.Drawing.Size(360, 21);
            this.CboAlmacen.TabIndex = 7;
            // 
            // CboProveedor
            // 
            this.CboProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboProveedor.Enabled = false;
            this.CboProveedor.FormattingEnabled = true;
            this.CboProveedor.Location = new System.Drawing.Point(115, 88);
            this.CboProveedor.Name = "CboProveedor";
            this.CboProveedor.Size = new System.Drawing.Size(360, 21);
            this.CboProveedor.TabIndex = 6;
            // 
            // TxtNumDoc
            // 
            this.TxtNumDoc.Enabled = false;
            this.TxtNumDoc.Location = new System.Drawing.Point(180, 65);
            this.TxtNumDoc.MaxLength = 10;
            this.TxtNumDoc.Name = "TxtNumDoc";
            this.TxtNumDoc.Size = new System.Drawing.Size(106, 20);
            this.TxtNumDoc.TabIndex = 5;
            this.TxtNumDoc.TextChanged += new System.EventHandler(this.TxtNumDoc_TextChanged);
            this.TxtNumDoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumDoc_KeyPress);
            // 
            // TxtNumSer
            // 
            this.TxtNumSer.Enabled = false;
            this.TxtNumSer.Location = new System.Drawing.Point(115, 65);
            this.TxtNumSer.MaxLength = 4;
            this.TxtNumSer.Name = "TxtNumSer";
            this.TxtNumSer.Size = new System.Drawing.Size(52, 20);
            this.TxtNumSer.TabIndex = 4;
            this.TxtNumSer.TextChanged += new System.EventHandler(this.TxtNumSer_TextChanged);
            this.TxtNumSer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumSer_KeyPress);
            // 
            // CboTipDoc
            // 
            this.CboTipDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTipDoc.Enabled = false;
            this.CboTipDoc.FormattingEnabled = true;
            this.CboTipDoc.Location = new System.Drawing.Point(115, 42);
            this.CboTipDoc.Name = "CboTipDoc";
            this.CboTipDoc.Size = new System.Drawing.Size(360, 21);
            this.CboTipDoc.TabIndex = 3;
            // 
            // CboTipPro
            // 
            this.CboTipPro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTipPro.Enabled = false;
            this.CboTipPro.FormattingEnabled = true;
            this.CboTipPro.Location = new System.Drawing.Point(493, 32);
            this.CboTipPro.Name = "CboTipPro";
            this.CboTipPro.Size = new System.Drawing.Size(63, 21);
            this.CboTipPro.TabIndex = 2;
            this.CboTipPro.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Almacen";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Proveedor";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Nº Documento";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Tipo Documento";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(490, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tipo Producto";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fch. Documento";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fch. Ingreso";
            // 
            // TxtFchDoc
            // 
            this.TxtFchDoc.Enabled = false;
            this.TxtFchDoc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchDoc.Location = new System.Drawing.Point(374, 20);
            this.TxtFchDoc.Name = "TxtFchDoc";
            this.TxtFchDoc.Size = new System.Drawing.Size(101, 20);
            this.TxtFchDoc.TabIndex = 1;
            // 
            // txtFchIng
            // 
            this.txtFchIng.Enabled = false;
            this.txtFchIng.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtFchIng.Location = new System.Drawing.Point(115, 20);
            this.txtFchIng.Name = "txtFchIng";
            this.txtFchIng.Size = new System.Drawing.Size(101, 20);
            this.txtFchIng.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CmdDelItem);
            this.groupBox1.Controls.Add(this.CmdAddItem);
            this.groupBox1.Location = new System.Drawing.Point(19, 410);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 44);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            // 
            // CmdDelItem
            // 
            this.CmdDelItem.Enabled = false;
            this.CmdDelItem.Location = new System.Drawing.Point(126, 10);
            this.CmdDelItem.Name = "CmdDelItem";
            this.CmdDelItem.Size = new System.Drawing.Size(109, 32);
            this.CmdDelItem.TabIndex = 11;
            this.CmdDelItem.Text = "Eliminar Item";
            this.CmdDelItem.UseVisualStyleBackColor = true;
            this.CmdDelItem.Click += new System.EventHandler(this.CmdDelItem_Click);
            // 
            // CmdAddItem
            // 
            this.CmdAddItem.Enabled = false;
            this.CmdAddItem.Location = new System.Drawing.Point(11, 10);
            this.CmdAddItem.Name = "CmdAddItem";
            this.CmdAddItem.Size = new System.Drawing.Size(109, 32);
            this.CmdAddItem.TabIndex = 10;
            this.CmdAddItem.Text = "Agregar Item";
            this.CmdAddItem.UseVisualStyleBackColor = true;
            this.CmdAddItem.Click += new System.EventHandler(this.CmdAddItem_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.LblTitulo2);
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(864, 28);
            this.panel3.TabIndex = 8;
            // 
            // LblTitulo2
            // 
            this.LblTitulo2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblTitulo2.Font = new System.Drawing.Font("Good Times Rg", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitulo2.ForeColor = System.Drawing.Color.Navy;
            this.LblTitulo2.Location = new System.Drawing.Point(0, 0);
            this.LblTitulo2.Name = "LblTitulo2";
            this.LblTitulo2.Size = new System.Drawing.Size(864, 28);
            this.LblTitulo2.TabIndex = 0;
            this.LblTitulo2.Text = "DETALLE DEL REGISTRO";
            this.LblTitulo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmIngresoAlmacen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 667);
            this.Controls.Add(this.Tab1);
            this.Controls.Add(this.ToolHerramientas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmIngresoAlmacen";
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "FrmIngresoAlmacen";
            this.Activated += new System.EventHandler(this.FrmIngresoAlmacen_Activated);
            this.Load += new System.EventHandler(this.FrmIngresoAlmacen_Load);
            this.Resize += new System.EventHandler(this.FrmIngresoAlmacen_Resize);
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ToolHerramientas;
        private System.Windows.Forms.ToolStripButton ToolNuevo;
        private System.Windows.Forms.ToolStripButton ToolModificar;
        private System.Windows.Forms.ToolStripButton ToolEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolGrabar;
        private System.Windows.Forms.ToolStripButton ToolCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ToolImprimir;
        private System.Windows.Forms.ToolStripButton ToolExportar;
        private System.Windows.Forms.ToolStripButton ToolSalir;
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox CboResponsable;
        private System.Windows.Forms.ComboBox CboAlmacen;
        private System.Windows.Forms.ComboBox CboProveedor;
        private System.Windows.Forms.TextBox TxtNumDoc;
        private System.Windows.Forms.TextBox TxtNumSer;
        private System.Windows.Forms.ComboBox CboTipDoc;
        private System.Windows.Forms.ComboBox CboTipPro;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker TxtFchDoc;
        private System.Windows.Forms.DateTimePicker txtFchIng;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label LblTitulo2;
        private C1.Win.C1FlexGrid.C1FlexGrid FgItems;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CmdDelItem;
        private System.Windows.Forms.Button CmdAddItem;
        private System.Windows.Forms.TextBox TxtObs;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripButton ToolCalendario;
        private MetroFramework.Controls.MetroComboBox CboMeses;
        private System.Windows.Forms.Label label11;
    }
}