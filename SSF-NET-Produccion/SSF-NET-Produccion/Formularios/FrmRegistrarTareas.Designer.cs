namespace SSF_NET_Produccion.Formularios
{
    partial class FrmRegistrarTareas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegistrarTareas));
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
            this.guiasDelMesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            this.Tab1 = new C1.Win.C1Command.C1DockingTab();
            this.c1DockingTabPage1 = new C1.Win.C1Command.C1DockingTabPage();
            this.Sc01 = new C1.Win.C1Sizer.C1Sizer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.CboMeses = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LblNumReg = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.DgLista = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1DockingTabPage2 = new C1.Win.C1Command.C1DockingTabPage();
            this.Sc02 = new C1.Win.C1Sizer.C1Sizer();
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.panel5 = new System.Windows.Forms.Panel();
            this.CmdAddPer = new System.Windows.Forms.Button();
            this.CmdDelPer = new System.Windows.Forms.Button();
            this.FgPer = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.FgTar = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CmdBusProd = new System.Windows.Forms.Button();
            this.TxtFchPro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtNumLot = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtCan = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CboUniMed = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtPro = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtNumPro = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
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
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgPer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgTar)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
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
            this.ToolHerramientas.Size = new System.Drawing.Size(954, 39);
            this.ToolHerramientas.TabIndex = 36;
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
            this.ToolNuevo.Visible = false;
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
            this.ToolImprimir.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emitirGuiaToolStripMenuItem,
            this.guiasDelMesToolStripMenuItem});
            this.ToolImprimir.Image = ((System.Drawing.Image)(resources.GetObject("ToolImprimir.Image")));
            this.ToolImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolImprimir.Name = "ToolImprimir";
            this.ToolImprimir.Size = new System.Drawing.Size(48, 36);
            this.ToolImprimir.Text = "toolStripSplitButton1";
            // 
            // emitirGuiaToolStripMenuItem
            // 
            this.emitirGuiaToolStripMenuItem.Name = "emitirGuiaToolStripMenuItem";
            this.emitirGuiaToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.emitirGuiaToolStripMenuItem.Text = "Imprimir Orden de Requerimiento";
            // 
            // guiasDelMesToolStripMenuItem
            // 
            this.guiasDelMesToolStripMenuItem.Name = "guiasDelMesToolStripMenuItem";
            this.guiasDelMesToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.guiasDelMesToolStripMenuItem.Text = "Imprimir Ordenes del Mes";
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
            this.Tab1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.Tab1.Controls.Add(this.c1DockingTabPage1);
            this.Tab1.Controls.Add(this.c1DockingTabPage2);
            this.Tab1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tab1.ForeColor = System.Drawing.Color.Black;
            this.Tab1.Location = new System.Drawing.Point(2, 41);
            this.Tab1.Name = "Tab1";
            this.Tab1.SelectedIndex = 1;
            this.Tab1.Size = new System.Drawing.Size(938, 525);
            this.Tab1.TabAreaBackColor = System.Drawing.Color.White;
            this.Tab1.TabIndex = 37;
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
            this.c1DockingTabPage1.Size = new System.Drawing.Size(908, 523);
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
            this.Sc01.GridDefinition = "5.35372848948375:False:True;85.8508604206501:False:False;5.73613766730402:False:T" +
    "rue;\t99.1189427312775:False:False;";
            this.Sc01.Location = new System.Drawing.Point(0, 0);
            this.Sc01.Name = "Sc01";
            this.Sc01.Size = new System.Drawing.Size(908, 523);
            this.Sc01.TabIndex = 0;
            this.Sc01.Text = "c1Sizer1";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.CboMeses);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.LblNumReg);
            this.panel4.Controls.Add(this.label18);
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(4, 489);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(900, 30);
            this.panel4.TabIndex = 2;
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
            this.panel2.Size = new System.Drawing.Size(900, 28);
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
            this.label17.Size = new System.Drawing.Size(900, 28);
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
            this.DgLista.Size = new System.Drawing.Size(900, 449);
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
            this.c1DockingTabPage2.Size = new System.Drawing.Size(908, 523);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Detalle";
            // 
            // Sc02
            // 
            this.Sc02.Controls.Add(this.c1Sizer1);
            this.Sc02.Controls.Add(this.panel3);
            this.Sc02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sc02.ForeColor = System.Drawing.Color.Black;
            this.Sc02.GridDefinition = "5.35372848948375:False:True;93.4990439770555:False:False;\t99.7797356828194:False:" +
    "False;";
            this.Sc02.Location = new System.Drawing.Point(0, 0);
            this.Sc02.Margin = new System.Windows.Forms.Padding(1);
            this.Sc02.Name = "Sc02";
            this.Sc02.Padding = new System.Windows.Forms.Padding(1);
            this.Sc02.Size = new System.Drawing.Size(908, 523);
            this.Sc02.TabIndex = 0;
            this.Sc02.Text = "c1Sizer2";
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.Controls.Add(this.panel5);
            this.c1Sizer1.Controls.Add(this.FgPer);
            this.c1Sizer1.Controls.Add(this.FgTar);
            this.c1Sizer1.Controls.Add(this.panel1);
            this.c1Sizer1.ForeColor = System.Drawing.Color.Black;
            this.c1Sizer1.GridDefinition = "21.4723926380368:False:True;26.5848670756646:True:True;42.1267893660532:False:Fal" +
    "se;8.58895705521472:False:True;\t100:False:False;";
            this.c1Sizer1.Location = new System.Drawing.Point(1, 33);
            this.c1Sizer1.Margin = new System.Windows.Forms.Padding(1);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Padding = new System.Windows.Forms.Padding(0);
            this.c1Sizer1.Size = new System.Drawing.Size(906, 489);
            this.c1Sizer1.SplitterWidth = 2;
            this.c1Sizer1.TabIndex = 10;
            this.c1Sizer1.Text = "c1Sizer1";
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.CmdAddPer);
            this.panel5.Controls.Add(this.CmdDelPer);
            this.panel5.Location = new System.Drawing.Point(0, 447);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(906, 42);
            this.panel5.TabIndex = 94;
            // 
            // CmdAddPer
            // 
            this.CmdAddPer.Location = new System.Drawing.Point(16, 4);
            this.CmdAddPer.Name = "CmdAddPer";
            this.CmdAddPer.Size = new System.Drawing.Size(103, 32);
            this.CmdAddPer.TabIndex = 120;
            this.CmdAddPer.Text = "Agregar Personal";
            this.CmdAddPer.UseVisualStyleBackColor = true;
            this.CmdAddPer.Click += new System.EventHandler(this.CmdAddPer_Click);
            // 
            // CmdDelPer
            // 
            this.CmdDelPer.Location = new System.Drawing.Point(125, 4);
            this.CmdDelPer.Name = "CmdDelPer";
            this.CmdDelPer.Size = new System.Drawing.Size(103, 32);
            this.CmdDelPer.TabIndex = 119;
            this.CmdDelPer.Text = "Eliminar Personal";
            this.CmdDelPer.UseVisualStyleBackColor = true;
            this.CmdDelPer.Click += new System.EventHandler(this.CmdDelPer_Click);
            // 
            // FgPer
            // 
            this.FgPer.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgPer.ColumnInfo = "4,1,0,0,0,85,Columns:0{Width:10;}\t";
            this.FgPer.Location = new System.Drawing.Point(0, 239);
            this.FgPer.Name = "FgPer";
            this.FgPer.Rows.DefaultSize = 17;
            this.FgPer.Size = new System.Drawing.Size(906, 206);
            this.FgPer.TabIndex = 93;
            this.FgPer.EnterCell += new System.EventHandler(this.FgPer_EnterCell);
            this.FgPer.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.FgPer_KeyPressEdit);
            this.FgPer.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgPer_CellChanged);
            // 
            // FgTar
            // 
            this.FgTar.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgTar.ColumnInfo = "4,1,0,0,0,85,Columns:0{Width:10;}\t";
            this.FgTar.Location = new System.Drawing.Point(0, 107);
            this.FgTar.Name = "FgTar";
            this.FgTar.Rows.DefaultSize = 17;
            this.FgTar.Size = new System.Drawing.Size(906, 130);
            this.FgTar.TabIndex = 24;
            this.FgTar.RowColChange += new System.EventHandler(this.FgTar_RowColChange);
            this.FgTar.EnterCell += new System.EventHandler(this.FgTar_EnterCell);
            this.FgTar.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.FgTar_KeyPressEdit);
            this.FgTar.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgTar_CellChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.CmdBusProd);
            this.panel1.Controls.Add(this.TxtFchPro);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.TxtNumLot);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.TxtCan);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.CboUniMed);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.TxtPro);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TxtNumPro);
            this.panel1.Controls.Add(this.label11);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(906, 105);
            this.panel1.TabIndex = 0;
            // 
            // CmdBusProd
            // 
            this.CmdBusProd.Location = new System.Drawing.Point(240, 11);
            this.CmdBusProd.Name = "CmdBusProd";
            this.CmdBusProd.Size = new System.Drawing.Size(121, 22);
            this.CmdBusProd.TabIndex = 118;
            this.CmdBusProd.Text = "Buscar Produccion";
            this.CmdBusProd.UseVisualStyleBackColor = true;
            this.CmdBusProd.Visible = false;
            // 
            // TxtFchPro
            // 
            this.TxtFchPro.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchPro.Location = new System.Drawing.Point(105, 80);
            this.TxtFchPro.Name = "TxtFchPro";
            this.TxtFchPro.Size = new System.Drawing.Size(98, 20);
            this.TxtFchPro.TabIndex = 83;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 82;
            this.label3.Text = "Fch. Produccion";
            // 
            // TxtNumLot
            // 
            this.TxtNumLot.Enabled = false;
            this.TxtNumLot.Location = new System.Drawing.Point(419, 80);
            this.TxtNumLot.MaxLength = 25;
            this.TxtNumLot.Name = "TxtNumLot";
            this.TxtNumLot.Size = new System.Drawing.Size(102, 20);
            this.TxtNumLot.TabIndex = 81;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(6, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 79;
            this.label5.Text = "Unidad Medida";
            // 
            // TxtCan
            // 
            this.TxtCan.Enabled = false;
            this.TxtCan.Location = new System.Drawing.Point(419, 56);
            this.TxtCan.MaxLength = 25;
            this.TxtCan.Name = "TxtCan";
            this.TxtCan.Size = new System.Drawing.Size(102, 20);
            this.TxtCan.TabIndex = 78;
            this.TxtCan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(6, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 77;
            this.label4.Text = "Producto";
            // 
            // CboUniMed
            // 
            this.CboUniMed.BackColor = System.Drawing.Color.White;
            this.CboUniMed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboUniMed.Enabled = false;
            this.CboUniMed.ForeColor = System.Drawing.Color.Black;
            this.CboUniMed.FormattingEnabled = true;
            this.CboUniMed.Location = new System.Drawing.Point(105, 56);
            this.CboUniMed.Name = "CboUniMed";
            this.CboUniMed.Size = new System.Drawing.Size(98, 21);
            this.CboUniMed.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(358, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 76;
            this.label6.Text = "Cantidad";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtPro
            // 
            this.TxtPro.Enabled = false;
            this.TxtPro.Location = new System.Drawing.Point(105, 33);
            this.TxtPro.MaxLength = 25;
            this.TxtPro.Name = "TxtPro";
            this.TxtPro.Size = new System.Drawing.Size(491, 20);
            this.TxtPro.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 69;
            this.label2.Text = "Nº Produccion";
            // 
            // TxtNumPro
            // 
            this.TxtNumPro.Enabled = false;
            this.TxtNumPro.Location = new System.Drawing.Point(105, 11);
            this.TxtNumPro.MaxLength = 25;
            this.TxtNumPro.Name = "TxtNumPro";
            this.TxtNumPro.Size = new System.Drawing.Size(119, 20);
            this.TxtNumPro.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(364, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 69;
            this.label11.Text = "Nº Lote";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel3.Controls.Add(this.LblTitulo2);
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(1, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(906, 28);
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
            this.LblTitulo2.Size = new System.Drawing.Size(906, 28);
            this.LblTitulo2.TabIndex = 0;
            this.LblTitulo2.Text = "DETALLE DEL REGISTRO";
            this.LblTitulo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmRegistrarTareas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 578);
            this.Controls.Add(this.ToolHerramientas);
            this.Controls.Add(this.Tab1);
            this.Name = "FrmRegistrarTareas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmRegistrarTareas";
            this.Activated += new System.EventHandler(this.FrmRegistrarTareas_Activated);
            this.Load += new System.EventHandler(this.FrmRegistrarTareas_Load);
            this.Resize += new System.EventHandler(this.FrmRegistrarTareas_Resize);
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
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgPer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgTar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.ToolStripSplitButton ToolImprimir;
        private System.Windows.Forms.ToolStripMenuItem emitirGuiaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guiasDelMesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton ToolSalir;
        private C1.Win.C1Command.C1DockingTab Tab1;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage1;
        private C1.Win.C1Sizer.C1Sizer Sc01;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox CboMeses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblNumReg;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label17;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid DgLista;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage2;
        private C1.Win.C1Sizer.C1Sizer Sc02;
        private C1.Win.C1Sizer.C1Sizer c1Sizer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TxtNumLot;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtCan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CboUniMed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtPro;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtNumPro;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label LblTitulo2;
        private System.Windows.Forms.DateTimePicker TxtFchPro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CmdBusProd;
        private C1.Win.C1FlexGrid.C1FlexGrid FgTar;
        private C1.Win.C1FlexGrid.C1FlexGrid FgPer;
        private System.Windows.Forms.Button CmdAddPer;
        private System.Windows.Forms.Button CmdDelPer;
        private System.Windows.Forms.Panel panel5;

    }
}