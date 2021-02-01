namespace SSF_NET_Gestion.Formularios
{
    partial class FrmManPlanProduccion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManPlanProduccion));
            this.ToolHerramientas = new System.Windows.Forms.ToolStrip();
            this.ToolNuevo = new System.Windows.Forms.ToolStripButton();
            this.ToolModificar = new System.Windows.Forms.ToolStripButton();
            this.ToolEliminar2 = new System.Windows.Forms.ToolStripSplitButton();
            this.eliminarRegistroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.desactivarPlanDeProduccionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolGrabar = new System.Windows.Forms.ToolStripButton();
            this.ToolCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolImprimir = new System.Windows.Forms.ToolStripButton();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.CboMesIni = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CmdBusCli = new System.Windows.Forms.Button();
            this.LblIdPlaVen = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtPlaVen = new System.Windows.Forms.TextBox();
            this.TxtDes = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtFchFin = new System.Windows.Forms.DateTimePicker();
            this.TxtFchIni = new System.Windows.Forms.DateTimePicker();
            this.Tab2 = new System.Windows.Forms.TabControl();
            this.c1DockingTabPage3 = new System.Windows.Forms.TabPage();
            this.FgProd = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1DockingTabPage4 = new System.Windows.Forms.TabPage();
            this.FgInter = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1DockingTabPage5 = new System.Windows.Forms.TabPage();
            this.FgTodo = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.LblTitulo2 = new System.Windows.Forms.Label();
            this.CmdAddValores = new System.Windows.Forms.Button();
            this.CmdVerRec = new System.Windows.Forms.Button();
            this.CmdVerEstaciona = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ToolHerramientas.SuspendLayout();
            this.Tab1.SuspendLayout();
            this.c1DockingTabPage1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).BeginInit();
            this.c1DockingTabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.Tab2.SuspendLayout();
            this.c1DockingTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgProd)).BeginInit();
            this.c1DockingTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgInter)).BeginInit();
            this.c1DockingTabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgTodo)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
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
            this.ToolEliminar2,
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
            this.ToolHerramientas.Size = new System.Drawing.Size(1273, 39);
            this.ToolHerramientas.TabIndex = 17;
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
            // ToolEliminar2
            // 
            this.ToolEliminar2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolEliminar2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarRegistroToolStripMenuItem,
            this.toolStripMenuItem1,
            this.desactivarPlanDeProduccionToolStripMenuItem});
            this.ToolEliminar2.Image = ((System.Drawing.Image)(resources.GetObject("ToolEliminar2.Image")));
            this.ToolEliminar2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolEliminar2.Name = "ToolEliminar2";
            this.ToolEliminar2.Size = new System.Drawing.Size(51, 36);
            this.ToolEliminar2.Text = "toolStripSplitButton1";
            this.ToolEliminar2.ToolTipText = "Eliminar registro";
            // 
            // eliminarRegistroToolStripMenuItem
            // 
            this.eliminarRegistroToolStripMenuItem.Name = "eliminarRegistroToolStripMenuItem";
            this.eliminarRegistroToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.eliminarRegistroToolStripMenuItem.Text = "Eliminar Registro";
            this.eliminarRegistroToolStripMenuItem.Click += new System.EventHandler(this.eliminarRegistroToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(289, 6);
            // 
            // desactivarPlanDeProduccionToolStripMenuItem
            // 
            this.desactivarPlanDeProduccionToolStripMenuItem.Name = "desactivarPlanDeProduccionToolStripMenuItem";
            this.desactivarPlanDeProduccionToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.desactivarPlanDeProduccionToolStripMenuItem.Text = "Desactivar Plan de Produccion";
            this.desactivarPlanDeProduccionToolStripMenuItem.Click += new System.EventHandler(this.desactivarPlanDeProduccionToolStripMenuItem_Click);
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
            this.ToolEliminar.Visible = false;
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
            this.Tab1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tab1.Controls.Add(this.c1DockingTabPage1);
            this.Tab1.Controls.Add(this.c1DockingTabPage2);
            this.Tab1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tab1.ForeColor = System.Drawing.Color.Black;
            this.Tab1.Location = new System.Drawing.Point(3, 48);
            this.Tab1.Margin = new System.Windows.Forms.Padding(4);
            this.Tab1.Multiline = true;
            this.Tab1.Name = "Tab1";
            this.Tab1.SelectedIndex = 1;
            this.Tab1.Size = new System.Drawing.Size(1269, 617);
            this.Tab1.TabIndex = 16;
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
            this.c1DockingTabPage1.Size = new System.Drawing.Size(1233, 609);
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
            this.panel4.Location = new System.Drawing.Point(5, 574);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1225, 39);
            this.panel4.TabIndex = 2;
            // 
            // CboMeses
            // 
            this.CboMeses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMeses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboMeses.FormattingEnabled = true;
            this.CboMeses.Location = new System.Drawing.Point(881, 4);
            this.CboMeses.Margin = new System.Windows.Forms.Padding(4);
            this.CboMeses.Name = "CboMeses";
            this.CboMeses.Size = new System.Drawing.Size(189, 28);
            this.CboMeses.TabIndex = 3;
            this.CboMeses.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(737, 10);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 17);
            this.label11.TabIndex = 2;
            this.label11.Text = "Mes trabajo :";
            this.label11.Visible = false;
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
            this.panel2.Location = new System.Drawing.Point(5, 5);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1225, 28);
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
            this.label17.Size = new System.Drawing.Size(1225, 28);
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
            this.DgLista.Size = new System.Drawing.Size(1225, 535);
            this.DgLista.TabIndex = 0;
            this.DgLista.Text = "c1TrueDBGrid1";
            this.DgLista.DoubleClick += new System.EventHandler(this.DgLista_DoubleClick);
            this.DgLista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DgLista_KeyPress);
            this.DgLista.PropBag = resources.GetString("DgLista.PropBag");
            // 
            // c1DockingTabPage2
            // 
            this.c1DockingTabPage2.Controls.Add(this.panel1);
            this.c1DockingTabPage2.Controls.Add(this.Tab2);
            this.c1DockingTabPage2.Controls.Add(this.panel3);
            this.c1DockingTabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.c1DockingTabPage2.ForeColor = System.Drawing.Color.Black;
            this.c1DockingTabPage2.Location = new System.Drawing.Point(32, 4);
            this.c1DockingTabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.c1DockingTabPage2.Name = "c1DockingTabPage2";
            this.c1DockingTabPage2.Size = new System.Drawing.Size(1233, 609);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Detalle";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.CboMesIni);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.CmdBusCli);
            this.panel1.Controls.Add(this.LblIdPlaVen);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.TxtPlaVen);
            this.panel1.Controls.Add(this.TxtDes);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TxtFchFin);
            this.panel1.Controls.Add(this.TxtFchIni);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(1, 50);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1223, 91);
            this.panel1.TabIndex = 0;
            // 
            // CboMesIni
            // 
            this.CboMesIni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMesIni.FormattingEnabled = true;
            this.CboMesIni.Location = new System.Drawing.Point(125, 31);
            this.CboMesIni.Margin = new System.Windows.Forms.Padding(4);
            this.CboMesIni.Name = "CboMesIni";
            this.CboMesIni.Size = new System.Drawing.Size(199, 25);
            this.CboMesIni.TabIndex = 1;
            this.CboMesIni.SelectedValueChanged += new System.EventHandler(this.CboMesIni_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(5, 34);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 17);
            this.label4.TabIndex = 47;
            this.label4.Text = "Mes de Inicio";
            // 
            // CmdBusCli
            // 
            this.CmdBusCli.BackColor = System.Drawing.Color.White;
            this.CmdBusCli.Enabled = false;
            this.CmdBusCli.ForeColor = System.Drawing.Color.Black;
            this.CmdBusCli.Image = ((System.Drawing.Image)(resources.GetObject("CmdBusCli.Image")));
            this.CmdBusCli.Location = new System.Drawing.Point(951, 0);
            this.CmdBusCli.Margin = new System.Windows.Forms.Padding(4);
            this.CmdBusCli.Name = "CmdBusCli";
            this.CmdBusCli.Size = new System.Drawing.Size(53, 34);
            this.CmdBusCli.TabIndex = 46;
            this.CmdBusCli.UseVisualStyleBackColor = false;
            this.CmdBusCli.Click += new System.EventHandler(this.CmdBusCli_Click);
            // 
            // LblIdPlaVen
            // 
            this.LblIdPlaVen.AutoSize = true;
            this.LblIdPlaVen.BackColor = System.Drawing.Color.Transparent;
            this.LblIdPlaVen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIdPlaVen.ForeColor = System.Drawing.Color.Red;
            this.LblIdPlaVen.Location = new System.Drawing.Point(840, 32);
            this.LblIdPlaVen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblIdPlaVen.Name = "LblIdPlaVen";
            this.LblIdPlaVen.Size = new System.Drawing.Size(94, 17);
            this.LblIdPlaVen.TabIndex = 45;
            this.LblIdPlaVen.Text = "LblIdPlaVen";
            this.LblIdPlaVen.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(5, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 17);
            this.label3.TabIndex = 44;
            this.label3.Text = "Plan de Ventas";
            // 
            // TxtPlaVen
            // 
            this.TxtPlaVen.BackColor = System.Drawing.Color.White;
            this.TxtPlaVen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPlaVen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtPlaVen.Enabled = false;
            this.TxtPlaVen.ForeColor = System.Drawing.Color.Black;
            this.TxtPlaVen.Location = new System.Drawing.Point(125, 4);
            this.TxtPlaVen.Margin = new System.Windows.Forms.Padding(4);
            this.TxtPlaVen.Multiline = true;
            this.TxtPlaVen.Name = "TxtPlaVen";
            this.TxtPlaVen.Size = new System.Drawing.Size(819, 25);
            this.TxtPlaVen.TabIndex = 0;
            // 
            // TxtDes
            // 
            this.TxtDes.BackColor = System.Drawing.Color.White;
            this.TxtDes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDes.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtDes.Enabled = false;
            this.TxtDes.ForeColor = System.Drawing.Color.Black;
            this.TxtDes.Location = new System.Drawing.Point(125, 59);
            this.TxtDes.Margin = new System.Windows.Forms.Padding(4);
            this.TxtDes.Multiline = true;
            this.TxtDes.Name = "TxtDes";
            this.TxtDes.Size = new System.Drawing.Size(819, 25);
            this.TxtDes.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(5, 62);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 17);
            this.label9.TabIndex = 37;
            this.label9.Text = "Descripcion";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(376, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fch. Termino";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(624, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fch. Inicio";
            this.label1.Visible = false;
            // 
            // TxtFchFin
            // 
            this.TxtFchFin.BackColor = System.Drawing.Color.White;
            this.TxtFchFin.Enabled = false;
            this.TxtFchFin.ForeColor = System.Drawing.Color.Black;
            this.TxtFchFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchFin.Location = new System.Drawing.Point(477, 31);
            this.TxtFchFin.Margin = new System.Windows.Forms.Padding(4);
            this.TxtFchFin.Name = "TxtFchFin";
            this.TxtFchFin.Size = new System.Drawing.Size(133, 23);
            this.TxtFchFin.TabIndex = 1;
            this.TxtFchFin.Visible = false;
            // 
            // TxtFchIni
            // 
            this.TxtFchIni.BackColor = System.Drawing.Color.White;
            this.TxtFchIni.Enabled = false;
            this.TxtFchIni.ForeColor = System.Drawing.Color.Black;
            this.TxtFchIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchIni.Location = new System.Drawing.Point(704, 31);
            this.TxtFchIni.Margin = new System.Windows.Forms.Padding(4);
            this.TxtFchIni.Name = "TxtFchIni";
            this.TxtFchIni.Size = new System.Drawing.Size(133, 23);
            this.TxtFchIni.TabIndex = 0;
            this.TxtFchIni.Visible = false;
            // 
            // Tab2
            // 
            this.Tab2.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.Tab2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tab2.Controls.Add(this.c1DockingTabPage3);
            this.Tab2.Controls.Add(this.c1DockingTabPage4);
            this.Tab2.Controls.Add(this.c1DockingTabPage5);
            this.Tab2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tab2.ForeColor = System.Drawing.Color.Black;
            this.Tab2.Location = new System.Drawing.Point(5, 152);
            this.Tab2.Margin = new System.Windows.Forms.Padding(4);
            this.Tab2.Name = "Tab2";
            this.Tab2.SelectedIndex = 2;
            this.Tab2.Size = new System.Drawing.Size(1225, 455);
            this.Tab2.TabIndex = 36;
            // 
            // c1DockingTabPage3
            // 
            this.c1DockingTabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.c1DockingTabPage3.Controls.Add(this.FgProd);
            this.c1DockingTabPage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.c1DockingTabPage3.ForeColor = System.Drawing.Color.Black;
            this.c1DockingTabPage3.Location = new System.Drawing.Point(4, 4);
            this.c1DockingTabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.c1DockingTabPage3.Name = "c1DockingTabPage3";
            this.c1DockingTabPage3.Size = new System.Drawing.Size(1217, 425);
            this.c1DockingTabPage3.TabIndex = 0;
            this.c1DockingTabPage3.Text = "Terminado";
            // 
            // FgProd
            // 
            this.FgProd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FgProd.BackColor = System.Drawing.Color.White;
            this.FgProd.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgProd.ColumnInfo = "3,1,0,0,0,100,Columns:0{Width:9;}\t1{Width:217;Caption:\"Unidad Medida\";Style:\"Text" +
    "Align:CenterCenter;\";StyleFixed:\"TextAlign:CenterCenter;\";}\t2{Width:30;}\t";
            this.FgProd.ForeColor = System.Drawing.Color.Black;
            this.FgProd.Location = new System.Drawing.Point(0, 0);
            this.FgProd.Margin = new System.Windows.Forms.Padding(4);
            this.FgProd.Name = "FgProd";
            this.FgProd.Rows.DefaultSize = 20;
            this.FgProd.Size = new System.Drawing.Size(1223, 421);
            this.FgProd.StyleInfo = resources.GetString("FgProd.StyleInfo");
            this.FgProd.TabIndex = 13;
            // 
            // c1DockingTabPage4
            // 
            this.c1DockingTabPage4.Controls.Add(this.FgInter);
            this.c1DockingTabPage4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.c1DockingTabPage4.ForeColor = System.Drawing.Color.Black;
            this.c1DockingTabPage4.Location = new System.Drawing.Point(4, 4);
            this.c1DockingTabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.c1DockingTabPage4.Name = "c1DockingTabPage4";
            this.c1DockingTabPage4.Size = new System.Drawing.Size(1217, 425);
            this.c1DockingTabPage4.TabIndex = 1;
            this.c1DockingTabPage4.Text = "Intermedio";
            // 
            // FgInter
            // 
            this.FgInter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FgInter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.FgInter.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgInter.ColumnInfo = "3,1,0,0,0,100,Columns:0{Width:9;}\t1{Width:217;Caption:\"Unidad Medida\";Style:\"Text" +
    "Align:CenterCenter;\";StyleFixed:\"TextAlign:CenterCenter;\";}\t2{Width:30;}\t";
            this.FgInter.ForeColor = System.Drawing.Color.Black;
            this.FgInter.Location = new System.Drawing.Point(0, 0);
            this.FgInter.Margin = new System.Windows.Forms.Padding(4);
            this.FgInter.Name = "FgInter";
            this.FgInter.Rows.DefaultSize = 20;
            this.FgInter.Size = new System.Drawing.Size(1223, 421);
            this.FgInter.StyleInfo = resources.GetString("FgInter.StyleInfo");
            this.FgInter.TabIndex = 36;
            // 
            // c1DockingTabPage5
            // 
            this.c1DockingTabPage5.Controls.Add(this.FgTodo);
            this.c1DockingTabPage5.Location = new System.Drawing.Point(4, 4);
            this.c1DockingTabPage5.Margin = new System.Windows.Forms.Padding(4);
            this.c1DockingTabPage5.Name = "c1DockingTabPage5";
            this.c1DockingTabPage5.Size = new System.Drawing.Size(1217, 425);
            this.c1DockingTabPage5.TabIndex = 2;
            this.c1DockingTabPage5.Text = "Todos";
            // 
            // FgTodo
            // 
            this.FgTodo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FgTodo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FgTodo.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgTodo.ColumnInfo = "3,1,0,0,0,85,Columns:0{Width:9;}\t1{Width:217;Caption:\"Unidad Medida\";Style:\"TextA" +
    "lign:CenterCenter;\";StyleFixed:\"TextAlign:CenterCenter;\";}\t2{Width:30;}\t";
            this.FgTodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FgTodo.ForeColor = System.Drawing.Color.Black;
            this.FgTodo.Location = new System.Drawing.Point(0, 0);
            this.FgTodo.Margin = new System.Windows.Forms.Padding(4);
            this.FgTodo.Name = "FgTodo";
            this.FgTodo.Rows.DefaultSize = 17;
            this.FgTodo.Size = new System.Drawing.Size(1223, 421);
            this.FgTodo.StyleInfo = resources.GetString("FgTodo.StyleInfo");
            this.FgTodo.TabIndex = 37;
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
            this.panel3.Size = new System.Drawing.Size(1225, 28);
            this.panel3.TabIndex = 8;
            // 
            // LblTitulo2
            // 
            this.LblTitulo2.BackColor = System.Drawing.Color.Transparent;
            this.LblTitulo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitulo2.ForeColor = System.Drawing.Color.Black;
            this.LblTitulo2.Location = new System.Drawing.Point(1, 0);
            this.LblTitulo2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblTitulo2.Name = "LblTitulo2";
            this.LblTitulo2.Size = new System.Drawing.Size(1228, 34);
            this.LblTitulo2.TabIndex = 0;
            this.LblTitulo2.Text = "DETALLE DEL REGISTRO";
            this.LblTitulo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CmdAddValores
            // 
            this.CmdAddValores.Enabled = false;
            this.CmdAddValores.Image = ((System.Drawing.Image)(resources.GetObject("CmdAddValores.Image")));
            this.CmdAddValores.Location = new System.Drawing.Point(4, 16);
            this.CmdAddValores.Margin = new System.Windows.Forms.Padding(4);
            this.CmdAddValores.Name = "CmdAddValores";
            this.CmdAddValores.Size = new System.Drawing.Size(64, 52);
            this.CmdAddValores.TabIndex = 42;
            this.CmdAddValores.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.CmdAddValores.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.CmdAddValores.UseVisualStyleBackColor = true;
            this.CmdAddValores.Click += new System.EventHandler(this.CmdAddValores_Click);
            // 
            // CmdVerRec
            // 
            this.CmdVerRec.Image = ((System.Drawing.Image)(resources.GetObject("CmdVerRec.Image")));
            this.CmdVerRec.Location = new System.Drawing.Point(69, 16);
            this.CmdVerRec.Margin = new System.Windows.Forms.Padding(4);
            this.CmdVerRec.Name = "CmdVerRec";
            this.CmdVerRec.Size = new System.Drawing.Size(64, 52);
            this.CmdVerRec.TabIndex = 47;
            this.CmdVerRec.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.CmdVerRec.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.CmdVerRec.UseVisualStyleBackColor = true;
            this.CmdVerRec.Click += new System.EventHandler(this.CmdVerRec_Click);
            // 
            // CmdVerEstaciona
            // 
            this.CmdVerEstaciona.Image = ((System.Drawing.Image)(resources.GetObject("CmdVerEstaciona.Image")));
            this.CmdVerEstaciona.Location = new System.Drawing.Point(133, 16);
            this.CmdVerEstaciona.Margin = new System.Windows.Forms.Padding(4);
            this.CmdVerEstaciona.Name = "CmdVerEstaciona";
            this.CmdVerEstaciona.Size = new System.Drawing.Size(64, 52);
            this.CmdVerEstaciona.TabIndex = 48;
            this.CmdVerEstaciona.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.CmdVerEstaciona.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.CmdVerEstaciona.UseVisualStyleBackColor = true;
            this.CmdVerEstaciona.Click += new System.EventHandler(this.CmdVerEstaciona_Click);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.CmdVerEstaciona);
            this.panel5.Controls.Add(this.CmdVerRec);
            this.panel5.Controls.Add(this.CmdAddValores);
            this.panel5.Location = new System.Drawing.Point(1012, 6);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(207, 81);
            this.panel5.TabIndex = 48;
            // 
            // FrmManPlanProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1273, 690);
            this.Controls.Add(this.ToolHerramientas);
            this.Controls.Add(this.Tab1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmManPlanProduccion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmManPlanProduccion";
            this.Activated += new System.EventHandler(this.FrmManPlanProduccion_Activated);
            this.Load += new System.EventHandler(this.FrmManPlanProduccion_Load);
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
            this.Tab1.ResumeLayout(false);
            this.c1DockingTabPage1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).EndInit();
            this.c1DockingTabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Tab2.ResumeLayout(false);
            this.c1DockingTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgProd)).EndInit();
            this.c1DockingTabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgInter)).EndInit();
            this.c1DockingTabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgTodo)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripButton ToolSalir;
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TxtDes;
        private System.Windows.Forms.Label label9;
        private C1.Win.C1FlexGrid.C1FlexGrid FgProd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker TxtFchFin;
        private System.Windows.Forms.DateTimePicker TxtFchIni;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label LblTitulo2;
        private System.Windows.Forms.TabControl Tab2;
        private System.Windows.Forms.TabPage c1DockingTabPage3;
        private System.Windows.Forms.TabPage c1DockingTabPage4;
        private System.Windows.Forms.TabPage c1DockingTabPage5;
        private C1.Win.C1FlexGrid.C1FlexGrid FgInter;
        private C1.Win.C1FlexGrid.C1FlexGrid FgTodo;
        private System.Windows.Forms.Label LblIdPlaVen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtPlaVen;
        private System.Windows.Forms.Button CmdBusCli;
        private System.Windows.Forms.ToolStripSplitButton ToolEliminar2;
        private System.Windows.Forms.ToolStripMenuItem eliminarRegistroToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem desactivarPlanDeProduccionToolStripMenuItem;
        private System.Windows.Forms.ComboBox CboMesIni;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button CmdVerEstaciona;
        private System.Windows.Forms.Button CmdVerRec;
        private System.Windows.Forms.Button CmdAddValores;
    }
}