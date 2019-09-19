namespace SSF_NET_Almacen.Formularios
{
    partial class FrmKardexResumen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKardexResumen));
            this.ToolHerramientas = new System.Windows.Forms.ToolStrip();
            this.ToolNuevo = new System.Windows.Forms.ToolStripButton();
            this.ToolModificar = new System.Windows.Forms.ToolStripButton();
            this.ToolEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolGrabar = new System.Windows.Forms.ToolStripButton();
            this.ToolCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolExportar = new System.Windows.Forms.ToolStripButton();
            this.ToolImprimir = new System.Windows.Forms.ToolStripButton();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.DgLista = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.CboAlmacen = new System.Windows.Forms.ComboBox();
            this.TxtAñoTra = new System.Windows.Forms.NumericUpDown();
            this.CmdVerTodKar = new System.Windows.Forms.Button();
            this.CboPer = new System.Windows.Forms.ComboBox();
            this.CmdVerKardex = new System.Windows.Forms.Button();
            this.CmdBuscar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CboTipExi = new System.Windows.Forms.ComboBox();
            this.FgKar = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.ToolHerramientas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
            this.c1Sizer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAñoTra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgKar)).BeginInit();
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
            this.ToolExportar,
            this.ToolImprimir,
            this.ToolSalir});
            this.ToolHerramientas.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolHerramientas.Location = new System.Drawing.Point(0, 0);
            this.ToolHerramientas.Name = "ToolHerramientas";
            this.ToolHerramientas.Size = new System.Drawing.Size(1067, 39);
            this.ToolHerramientas.TabIndex = 13;
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
            this.ToolNuevo.Visible = false;
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
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            this.toolStripSeparator1.Visible = false;
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
            this.ToolGrabar.Visible = false;
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
            this.ToolCancelar.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            this.toolStripSeparator2.Visible = false;
            // 
            // ToolExportar
            // 
            this.ToolExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolExportar.Image = ((System.Drawing.Image)(resources.GetObject("ToolExportar.Image")));
            this.ToolExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolExportar.Name = "ToolExportar";
            this.ToolExportar.Size = new System.Drawing.Size(36, 36);
            this.ToolExportar.Text = "Exportar Excel";
            this.ToolExportar.Click += new System.EventHandler(this.ToolExportar_Click);
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
            // c1Sizer1
            // 
            this.c1Sizer1.Controls.Add(this.DgLista);
            this.c1Sizer1.Controls.Add(this.panel1);
            this.c1Sizer1.GridDefinition = "11.0497237569061:False:True;86.0036832412523:False:False;0:False:True;\t99.2509363" +
    "29588:False:False;";
            this.c1Sizer1.Location = new System.Drawing.Point(0, 39);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Size = new System.Drawing.Size(1068, 543);
            this.c1Sizer1.TabIndex = 14;
            this.c1Sizer1.Text = "c1Sizer1";
            // 
            // DgLista
            // 
            this.DgLista.BackColor = System.Drawing.Color.White;
            this.DgLista.ForeColor = System.Drawing.Color.Black;
            this.DgLista.GroupByCaption = "Drag a column header here to group by that column";
            this.DgLista.Images.Add(((System.Drawing.Image)(resources.GetObject("DgLista.Images"))));
            this.DgLista.Location = new System.Drawing.Point(4, 68);
            this.DgLista.Name = "DgLista";
            this.DgLista.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.DgLista.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.DgLista.PreviewInfo.ZoomFactor = 75D;
            this.DgLista.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("DgLista.PrintInfo.PageSettings")));
            this.DgLista.Size = new System.Drawing.Size(1060, 467);
            this.DgLista.TabIndex = 1;
            this.DgLista.Text = "c1TrueDBGrid1";
            this.DgLista.DoubleClick += new System.EventHandler(this.DgLista_DoubleClick);
            this.DgLista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DgLista_KeyPress);
            this.DgLista.PropBag = resources.GetString("DgLista.PropBag");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.CboAlmacen);
            this.panel1.Controls.Add(this.TxtAñoTra);
            this.panel1.Controls.Add(this.CmdVerTodKar);
            this.panel1.Controls.Add(this.CboPer);
            this.panel1.Controls.Add(this.CmdVerKardex);
            this.panel1.Controls.Add(this.CmdBuscar);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.CboTipExi);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1060, 60);
            this.panel1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(469, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "Almacen";
            // 
            // CboAlmacen
            // 
            this.CboAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboAlmacen.FormattingEnabled = true;
            this.CboAlmacen.Location = new System.Drawing.Point(523, 7);
            this.CboAlmacen.Name = "CboAlmacen";
            this.CboAlmacen.Size = new System.Drawing.Size(325, 21);
            this.CboAlmacen.TabIndex = 49;
            this.CboAlmacen.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CboAlmacen_KeyUp);
            // 
            // TxtAñoTra
            // 
            this.TxtAñoTra.Location = new System.Drawing.Point(90, 32);
            this.TxtAñoTra.Maximum = new decimal(new int[] {
            2013,
            0,
            0,
            0});
            this.TxtAñoTra.Minimum = new decimal(new int[] {
            2008,
            0,
            0,
            0});
            this.TxtAñoTra.Name = "TxtAñoTra";
            this.TxtAñoTra.Size = new System.Drawing.Size(75, 20);
            this.TxtAñoTra.TabIndex = 48;
            this.TxtAñoTra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtAñoTra.Value = new decimal(new int[] {
            2008,
            0,
            0,
            0});
            // 
            // CmdVerTodKar
            // 
            this.CmdVerTodKar.Image = ((System.Drawing.Image)(resources.GetObject("CmdVerTodKar.Image")));
            this.CmdVerTodKar.Location = new System.Drawing.Point(989, 5);
            this.CmdVerTodKar.Name = "CmdVerTodKar";
            this.CmdVerTodKar.Size = new System.Drawing.Size(55, 51);
            this.CmdVerTodKar.TabIndex = 47;
            this.CmdVerTodKar.UseVisualStyleBackColor = true;
            this.CmdVerTodKar.Click += new System.EventHandler(this.CmdVerTodKar_Click);
            // 
            // CboPer
            // 
            this.CboPer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboPer.FormattingEnabled = true;
            this.CboPer.Location = new System.Drawing.Point(267, 32);
            this.CboPer.Name = "CboPer";
            this.CboPer.Size = new System.Drawing.Size(161, 21);
            this.CboPer.TabIndex = 46;
            // 
            // CmdVerKardex
            // 
            this.CmdVerKardex.Image = ((System.Drawing.Image)(resources.GetObject("CmdVerKardex.Image")));
            this.CmdVerKardex.Location = new System.Drawing.Point(928, 5);
            this.CmdVerKardex.Name = "CmdVerKardex";
            this.CmdVerKardex.Size = new System.Drawing.Size(55, 51);
            this.CmdVerKardex.TabIndex = 45;
            this.CmdVerKardex.UseVisualStyleBackColor = true;
            this.CmdVerKardex.Click += new System.EventHandler(this.button1_Click);
            // 
            // CmdBuscar
            // 
            this.CmdBuscar.Image = ((System.Drawing.Image)(resources.GetObject("CmdBuscar.Image")));
            this.CmdBuscar.Location = new System.Drawing.Point(870, 5);
            this.CmdBuscar.Name = "CmdBuscar";
            this.CmdBuscar.Size = new System.Drawing.Size(55, 51);
            this.CmdBuscar.TabIndex = 44;
            this.CmdBuscar.UseVisualStyleBackColor = true;
            this.CmdBuscar.Click += new System.EventHandler(this.CmdBuscar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Periodo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Año";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tipo Existencia";
            // 
            // CboTipExi
            // 
            this.CboTipExi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTipExi.FormattingEnabled = true;
            this.CboTipExi.Location = new System.Drawing.Point(90, 7);
            this.CboTipExi.Name = "CboTipExi";
            this.CboTipExi.Size = new System.Drawing.Size(338, 21);
            this.CboTipExi.TabIndex = 0;
            // 
            // FgKar
            // 
            this.FgKar.ColumnInfo = "10,1,0,0,0,85,Columns:";
            this.FgKar.Location = new System.Drawing.Point(1105, 48);
            this.FgKar.Name = "FgKar";
            this.FgKar.Rows.DefaultSize = 17;
            this.FgKar.Size = new System.Drawing.Size(661, 467);
            this.FgKar.TabIndex = 1;
            // 
            // FrmKardexResumen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 582);
            this.Controls.Add(this.FgKar);
            this.Controls.Add(this.c1Sizer1);
            this.Controls.Add(this.ToolHerramientas);
            this.Name = "FrmKardexResumen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmKardexResumen";
            this.Activated += new System.EventHandler(this.FrmKardexResumen_Activated);
            this.Load += new System.EventHandler(this.FrmKardexResumen_Load);
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAñoTra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgKar)).EndInit();
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
        private C1.Win.C1Sizer.C1Sizer c1Sizer1;
        private C1.Win.C1FlexGrid.C1FlexGrid FgKar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CboTipExi;
        private System.Windows.Forms.Button CmdBuscar;
        private System.Windows.Forms.Button CmdVerKardex;
        private System.Windows.Forms.ComboBox CboPer;
        private System.Windows.Forms.Button CmdVerTodKar;
        internal System.Windows.Forms.NumericUpDown TxtAñoTra;
        private System.Windows.Forms.ToolStripButton ToolExportar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CboAlmacen;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid DgLista;
    }
}