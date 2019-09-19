namespace SSF_NET_Contabilidad.Formularios
{
    partial class FrmBalanceComprobacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBalanceComprobacion));
            this.Sz1 = new C1.Win.C1Sizer.C1Sizer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CboLib = new System.Windows.Forms.ComboBox();
            this.CboMon = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CboMesFin = new System.Windows.Forms.ComboBox();
            this.CboMesIni = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.FgDatos = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.ToolHerramientas = new System.Windows.Forms.ToolStrip();
            this.ToolBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolImprimir = new System.Windows.Forms.ToolStripButton();
            this.ToolExportar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.Sz1)).BeginInit();
            this.Sz1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgDatos)).BeginInit();
            this.ToolHerramientas.SuspendLayout();
            this.SuspendLayout();
            // 
            // Sz1
            // 
            this.Sz1.Border.Corners = new C1.Win.C1Sizer.Corners(1, 1, 1, 1);
            this.Sz1.Border.Thickness = new System.Windows.Forms.Padding(1);
            this.Sz1.Controls.Add(this.panel1);
            this.Sz1.Controls.Add(this.FgDatos);
            this.Sz1.GridDefinition = "15.4142581888247:False:True;83.6223506743738:False:False;\t99.6275605214153:False:" +
    "False;";
            this.Sz1.Location = new System.Drawing.Point(4, 43);
            this.Sz1.Margin = new System.Windows.Forms.Padding(1);
            this.Sz1.Name = "Sz1";
            this.Sz1.Padding = new System.Windows.Forms.Padding(1);
            this.Sz1.Size = new System.Drawing.Size(1074, 519);
            this.Sz1.SplitterWidth = 1;
            this.Sz1.TabIndex = 43;
            this.Sz1.Text = "c1Sizer1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1070, 80);
            this.panel1.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.CboLib);
            this.groupBox2.Controls.Add(this.CboMon);
            this.groupBox2.Location = new System.Drawing.Point(285, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(428, 75);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "[ Criterio Busqueda ]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(8, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 82;
            this.label3.Text = "Libro";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(8, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 81;
            this.label2.Text = "Moneda";
            // 
            // CboLib
            // 
            this.CboLib.BackColor = System.Drawing.Color.White;
            this.CboLib.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboLib.ForeColor = System.Drawing.Color.Black;
            this.CboLib.FormattingEnabled = true;
            this.CboLib.Location = new System.Drawing.Point(75, 45);
            this.CboLib.Name = "CboLib";
            this.CboLib.Size = new System.Drawing.Size(338, 21);
            this.CboLib.TabIndex = 80;
            this.CboLib.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CboLib_KeyUp);
            // 
            // CboMon
            // 
            this.CboMon.BackColor = System.Drawing.Color.White;
            this.CboMon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMon.ForeColor = System.Drawing.Color.Black;
            this.CboMon.FormattingEnabled = true;
            this.CboMon.Location = new System.Drawing.Point(75, 21);
            this.CboMon.Name = "CboMon";
            this.CboMon.Size = new System.Drawing.Size(177, 21);
            this.CboMon.TabIndex = 79;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CboMesFin);
            this.groupBox1.Controls.Add(this.CboMesIni);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 75);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "[ Fecha de Consulta ]";
            // 
            // CboMesFin
            // 
            this.CboMesFin.BackColor = System.Drawing.Color.White;
            this.CboMesFin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMesFin.ForeColor = System.Drawing.Color.Black;
            this.CboMesFin.FormattingEnabled = true;
            this.CboMesFin.Location = new System.Drawing.Point(84, 45);
            this.CboMesFin.Name = "CboMesFin";
            this.CboMesFin.Size = new System.Drawing.Size(177, 21);
            this.CboMesFin.TabIndex = 78;
            // 
            // CboMesIni
            // 
            this.CboMesIni.BackColor = System.Drawing.Color.White;
            this.CboMesIni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMesIni.ForeColor = System.Drawing.Color.Black;
            this.CboMesIni.FormattingEnabled = true;
            this.CboMesIni.Location = new System.Drawing.Point(84, 21);
            this.CboMesIni.Name = "CboMesIni";
            this.CboMesIni.Size = new System.Drawing.Size(177, 21);
            this.CboMesIni.TabIndex = 77;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(5, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 76;
            this.label1.Text = "Per. Final";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(5, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "Per. Inicio";
            // 
            // FgDatos
            // 
            this.FgDatos.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.FgDatos.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgDatos.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:10;}\t";
            this.FgDatos.Location = new System.Drawing.Point(2, 83);
            this.FgDatos.Name = "FgDatos";
            this.FgDatos.Rows.DefaultSize = 17;
            this.FgDatos.ShowSort = false;
            this.FgDatos.Size = new System.Drawing.Size(1070, 434);
            this.FgDatos.TabIndex = 0;
            this.FgDatos.DoubleClick += new System.EventHandler(this.FgDatos_DoubleClick);
            // 
            // ToolHerramientas
            // 
            this.ToolHerramientas.BackColor = System.Drawing.Color.White;
            this.ToolHerramientas.ForeColor = System.Drawing.Color.Black;
            this.ToolHerramientas.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolHerramientas.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ToolHerramientas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolBuscar,
            this.toolStripSeparator2,
            this.ToolImprimir,
            this.ToolExportar,
            this.toolStripSeparator1,
            this.ToolSalir});
            this.ToolHerramientas.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolHerramientas.Location = new System.Drawing.Point(0, 0);
            this.ToolHerramientas.Name = "ToolHerramientas";
            this.ToolHerramientas.Size = new System.Drawing.Size(1082, 39);
            this.ToolHerramientas.TabIndex = 42;
            this.ToolHerramientas.Text = "toolStrip1";
            // 
            // ToolBuscar
            // 
            this.ToolBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolBuscar.Image = ((System.Drawing.Image)(resources.GetObject("ToolBuscar.Image")));
            this.ToolBuscar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolBuscar.Name = "ToolBuscar";
            this.ToolBuscar.Size = new System.Drawing.Size(28, 36);
            this.ToolBuscar.Text = "toolStripButton1";
            this.ToolBuscar.ToolTipText = "Ejecutar Consulta";
            this.ToolBuscar.Click += new System.EventHandler(this.ToolBuscar_Click);
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
            this.ToolImprimir.Text = "toolStripButton1";
            this.ToolImprimir.Visible = false;
            // 
            // ToolExportar
            // 
            this.ToolExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolExportar.Image = ((System.Drawing.Image)(resources.GetObject("ToolExportar.Image")));
            this.ToolExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolExportar.Name = "ToolExportar";
            this.ToolExportar.Size = new System.Drawing.Size(36, 36);
            this.ToolExportar.Text = "toolStripButton3";
            this.ToolExportar.ToolTipText = "Exportar";
            this.ToolExportar.Click += new System.EventHandler(this.ToolExportar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
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
            // FrmBalanceComprobacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 590);
            this.Controls.Add(this.Sz1);
            this.Controls.Add(this.ToolHerramientas);
            this.Name = "FrmBalanceComprobacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmBalanceComprobacion";
            this.Load += new System.EventHandler(this.FrmBalanceComprobacion_Load);
            this.Resize += new System.EventHandler(this.FrmBalanceComprobacion_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.Sz1)).EndInit();
            this.Sz1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgDatos)).EndInit();
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Sizer.C1Sizer Sz1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1FlexGrid.C1FlexGrid FgDatos;
        private System.Windows.Forms.ToolStrip ToolHerramientas;
        private System.Windows.Forms.ToolStripButton ToolBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ToolImprimir;
        private System.Windows.Forms.ToolStripButton ToolExportar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolSalir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CboLib;
        private System.Windows.Forms.ComboBox CboMon;
        private System.Windows.Forms.ComboBox CboMesFin;
        private System.Windows.Forms.ComboBox CboMesIni;
    }
}