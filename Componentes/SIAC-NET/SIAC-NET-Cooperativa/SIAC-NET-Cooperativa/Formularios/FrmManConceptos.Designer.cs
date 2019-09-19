namespace SIAC_NET_Cooperativa.Formularios
{
    partial class FrmManConceptos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManConceptos));
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
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TxtImp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtCod = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtDes = new System.Windows.Forms.TextBox();
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
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            this.OptSi = new System.Windows.Forms.RadioButton();
            this.OptNo = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
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
            this.Tab1.Location = new System.Drawing.Point(3, 43);
            this.Tab1.Name = "Tab1";
            this.Tab1.SelectedIndex = 1;
            this.Tab1.Size = new System.Drawing.Size(933, 531);
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
            this.c1DockingTabPage1.Size = new System.Drawing.Size(903, 529);
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
    "e;\t99.1140642303433:False:False;";
            this.Sc01.Location = new System.Drawing.Point(0, 0);
            this.Sc01.Name = "Sc01";
            this.Sc01.Size = new System.Drawing.Size(903, 529);
            this.Sc01.TabIndex = 0;
            this.Sc01.Text = "c1Sizer1";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.LblNumReg);
            this.panel4.Controls.Add(this.label18);
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(4, 495);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(895, 30);
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
            this.panel2.Size = new System.Drawing.Size(895, 28);
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
            this.label17.Size = new System.Drawing.Size(895, 28);
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
            this.DgLista.Size = new System.Drawing.Size(895, 455);
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
            this.c1DockingTabPage2.Size = new System.Drawing.Size(903, 529);
            this.c1DockingTabPage2.TabIndex = 1;
            this.c1DockingTabPage2.Text = "Detalle";
            // 
            // Sc02
            // 
            this.Sc02.Controls.Add(this.c1Sizer1);
            this.Sc02.Controls.Add(this.panel3);
            this.Sc02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sc02.ForeColor = System.Drawing.Color.Black;
            this.Sc02.GridDefinition = "5.2930056710775:False:True;91.6824196597353:False:False;0:False:True;\t99.11406423" +
    "03433:False:False;";
            this.Sc02.Location = new System.Drawing.Point(0, 0);
            this.Sc02.Name = "Sc02";
            this.Sc02.Size = new System.Drawing.Size(903, 529);
            this.Sc02.TabIndex = 0;
            this.Sc02.Text = "c1Sizer2";
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.Controls.Add(this.panel1);
            this.c1Sizer1.ForeColor = System.Drawing.Color.Black;
            this.c1Sizer1.GridDefinition = "4.94845360824742:False:False;86.8041237113402:False:True;4.94845360824742:False:F" +
    "alse;\t3.57541899441341:False:False;91.0614525139665:False:True;3.57541899441341:" +
    "False:False;";
            this.c1Sizer1.Location = new System.Drawing.Point(4, 36);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Size = new System.Drawing.Size(895, 485);
            this.c1Sizer1.TabIndex = 10;
            this.c1Sizer1.Text = "c1Sizer1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.OptNo);
            this.panel1.Controls.Add(this.OptSi);
            this.panel1.Controls.Add(this.TxtImp);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.TxtCod);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TxtDes);
            this.panel1.Controls.Add(this.label7);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(40, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(815, 421);
            this.panel1.TabIndex = 0;
            // 
            // TxtImp
            // 
            this.TxtImp.Enabled = false;
            this.TxtImp.Location = new System.Drawing.Point(116, 70);
            this.TxtImp.MaxLength = 25;
            this.TxtImp.Name = "TxtImp";
            this.TxtImp.Size = new System.Drawing.Size(88, 20);
            this.TxtImp.TabIndex = 2;
            this.TxtImp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtImp_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(5, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Importe";
            // 
            // TxtCod
            // 
            this.TxtCod.Enabled = false;
            this.TxtCod.Location = new System.Drawing.Point(116, 25);
            this.TxtCod.MaxLength = 12;
            this.TxtCod.Name = "TxtCod";
            this.TxtCod.Size = new System.Drawing.Size(149, 20);
            this.TxtCod.TabIndex = 0;
            this.TxtCod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCod_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(5, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Codigo";
            // 
            // TxtDes
            // 
            this.TxtDes.Enabled = false;
            this.TxtDes.Location = new System.Drawing.Point(116, 47);
            this.TxtDes.MaxLength = 25;
            this.TxtDes.Name = "TxtDes";
            this.TxtDes.Size = new System.Drawing.Size(674, 20);
            this.TxtDes.TabIndex = 1;
            this.TxtDes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDes_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(5, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Descripcion";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel3.Controls.Add(this.LblTitulo2);
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(895, 28);
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
            this.LblTitulo2.Size = new System.Drawing.Size(895, 28);
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
            this.ToolHerramientas.Size = new System.Drawing.Size(985, 39);
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
            // OptSi
            // 
            this.OptSi.AutoSize = true;
            this.OptSi.Location = new System.Drawing.Point(123, 105);
            this.OptSi.Name = "OptSi";
            this.OptSi.Size = new System.Drawing.Size(56, 17);
            this.OptSi.TabIndex = 3;
            this.OptSi.TabStop = true;
            this.OptSi.Text = "Afecto";
            this.OptSi.UseVisualStyleBackColor = true;
            // 
            // OptNo
            // 
            this.OptNo.AutoSize = true;
            this.OptNo.Location = new System.Drawing.Point(204, 105);
            this.OptNo.Name = "OptNo";
            this.OptNo.Size = new System.Drawing.Size(72, 17);
            this.OptNo.TabIndex = 4;
            this.OptNo.TabStop = true;
            this.OptNo.Text = "No afecto";
            this.OptNo.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(5, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Afecto al I.G.V.";
            // 
            // FrmManConceptos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 616);
            this.Controls.Add(this.Tab1);
            this.Controls.Add(this.ToolHerramientas);
            this.Name = "FrmManConceptos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmManConceptos";
            this.Activated += new System.EventHandler(this.FrmManConceptos_Activated);
            this.Load += new System.EventHandler(this.FrmManConceptos_Load);
            this.Resize += new System.EventHandler(this.FrmManConceptos_Resize);
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
        private C1.Win.C1Sizer.C1Sizer c1Sizer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TxtImp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtCod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtDes;
        private System.Windows.Forms.Label label7;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton OptNo;
        private System.Windows.Forms.RadioButton OptSi;
    }
}