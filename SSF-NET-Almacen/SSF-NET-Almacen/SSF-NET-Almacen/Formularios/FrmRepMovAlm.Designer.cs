namespace SSF_NET_Almacen.Formularios
{
    partial class FrmRepMovAlm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRepMovAlm));
            this.TxtFchIni = new System.Windows.Forms.DateTimePicker();
            this.TxtFchFin = new System.Windows.Forms.DateTimePicker();
            this.OptIng = new System.Windows.Forms.RadioButton();
            this.OptSal = new System.Windows.Forms.RadioButton();
            this.CboAlmacen = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Cs1 = new C1.Win.C1Sizer.C1Sizer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CboItem = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.CboTipExi = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.CboTipOpe = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.FgFlex = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.ToolHerramientas = new System.Windows.Forms.ToolStrip();
            this.ToolNuevo = new System.Windows.Forms.ToolStripButton();
            this.ToolModificar = new System.Windows.Forms.ToolStripButton();
            this.ToolEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolGrabar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolCancelar = new System.Windows.Forms.ToolStripButton();
            this.ToolImprimir = new System.Windows.Forms.ToolStripButton();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.OptEst1 = new System.Windows.Forms.RadioButton();
            this.OptEst2 = new System.Windows.Forms.RadioButton();
            this.OptEst3 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.Cs1)).BeginInit();
            this.Cs1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgFlex)).BeginInit();
            this.ToolHerramientas.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtFchIni
            // 
            this.TxtFchIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchIni.Location = new System.Drawing.Point(114, 77);
            this.TxtFchIni.Name = "TxtFchIni";
            this.TxtFchIni.Size = new System.Drawing.Size(98, 20);
            this.TxtFchIni.TabIndex = 6;
            // 
            // TxtFchFin
            // 
            this.TxtFchFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchFin.Location = new System.Drawing.Point(361, 77);
            this.TxtFchFin.Name = "TxtFchFin";
            this.TxtFchFin.Size = new System.Drawing.Size(98, 20);
            this.TxtFchFin.TabIndex = 7;
            // 
            // OptIng
            // 
            this.OptIng.AutoSize = true;
            this.OptIng.Location = new System.Drawing.Point(114, 6);
            this.OptIng.Name = "OptIng";
            this.OptIng.Size = new System.Drawing.Size(60, 17);
            this.OptIng.TabIndex = 0;
            this.OptIng.TabStop = true;
            this.OptIng.Text = "Ingreso";
            this.OptIng.UseVisualStyleBackColor = true;
            this.OptIng.CheckedChanged += new System.EventHandler(this.OptIng_CheckedChanged);
            // 
            // OptSal
            // 
            this.OptSal.AutoSize = true;
            this.OptSal.Location = new System.Drawing.Point(226, 6);
            this.OptSal.Name = "OptSal";
            this.OptSal.Size = new System.Drawing.Size(54, 17);
            this.OptSal.TabIndex = 1;
            this.OptSal.TabStop = true;
            this.OptSal.Text = "Salida";
            this.OptSal.UseVisualStyleBackColor = true;
            this.OptSal.CheckedChanged += new System.EventHandler(this.OptSal_CheckedChanged);
            // 
            // CboAlmacen
            // 
            this.CboAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboAlmacen.FormattingEnabled = true;
            this.CboAlmacen.Location = new System.Drawing.Point(114, 29);
            this.CboAlmacen.Name = "CboAlmacen";
            this.CboAlmacen.Size = new System.Drawing.Size(345, 21);
            this.CboAlmacen.TabIndex = 2;
            this.CboAlmacen.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CboAlmacen_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Almacen";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(293, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Fch Final";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Fch Inicio";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Tipo Movimiento";
            // 
            // Cs1
            // 
            this.Cs1.Border.Corners = new C1.Win.C1Sizer.Corners(1, 1, 1, 1);
            this.Cs1.Border.Thickness = new System.Windows.Forms.Padding(1);
            this.Cs1.Controls.Add(this.panel1);
            this.Cs1.Controls.Add(this.FgFlex);
            this.Cs1.GridDefinition = "18.6131386861314:False:True;80.4744525547445:False:False;\t99.6153846153846:False:" +
    "False;";
            this.Cs1.Location = new System.Drawing.Point(1, 43);
            this.Cs1.Margin = new System.Windows.Forms.Padding(1);
            this.Cs1.Name = "Cs1";
            this.Cs1.Padding = new System.Windows.Forms.Padding(1);
            this.Cs1.Size = new System.Drawing.Size(1040, 548);
            this.Cs1.SplitterWidth = 1;
            this.Cs1.TabIndex = 15;
            this.Cs1.Text = "c1Sizer1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.CboItem);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.CboTipExi);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.CboTipOpe);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.CboAlmacen);
            this.panel1.Controls.Add(this.OptSal);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.OptIng);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.TxtFchFin);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TxtFchIni);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 102);
            this.panel1.TabIndex = 1;
            // 
            // CboItem
            // 
            this.CboItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboItem.FormattingEnabled = true;
            this.CboItem.Location = new System.Drawing.Point(617, 52);
            this.CboItem.Name = "CboItem";
            this.CboItem.Size = new System.Drawing.Size(345, 21);
            this.CboItem.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(577, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Item";
            // 
            // CboTipExi
            // 
            this.CboTipExi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTipExi.FormattingEnabled = true;
            this.CboTipExi.Location = new System.Drawing.Point(617, 29);
            this.CboTipExi.Name = "CboTipExi";
            this.CboTipExi.Size = new System.Drawing.Size(345, 21);
            this.CboTipExi.TabIndex = 4;
            this.CboTipExi.SelectedValueChanged += new System.EventHandler(this.CboTipExi_SelectedValueChanged);
            this.CboTipExi.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CboTipExi_KeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(525, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Tipo Existencia";
            // 
            // CboTipOpe
            // 
            this.CboTipOpe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTipOpe.FormattingEnabled = true;
            this.CboTipOpe.Location = new System.Drawing.Point(114, 52);
            this.CboTipOpe.Name = "CboTipOpe";
            this.CboTipOpe.Size = new System.Drawing.Size(345, 21);
            this.CboTipOpe.TabIndex = 3;
            this.CboTipOpe.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CboTipOpe_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Tipo Operacion";
            // 
            // FgFlex
            // 
            this.FgFlex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgFlex.ColumnInfo = "10,1,0,0,0,85,Columns:";
            this.FgFlex.Location = new System.Drawing.Point(2, 105);
            this.FgFlex.Name = "FgFlex";
            this.FgFlex.Rows.DefaultSize = 17;
            this.FgFlex.Size = new System.Drawing.Size(1036, 441);
            this.FgFlex.TabIndex = 0;
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
            this.toolStripSeparator2,
            this.ToolCancelar,
            this.ToolImprimir,
            this.ToolSalir});
            this.ToolHerramientas.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolHerramientas.Location = new System.Drawing.Point(0, 0);
            this.ToolHerramientas.Name = "ToolHerramientas";
            this.ToolHerramientas.Size = new System.Drawing.Size(1041, 39);
            this.ToolHerramientas.TabIndex = 16;
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
            this.ToolGrabar.Image = ((System.Drawing.Image)(resources.GetObject("ToolGrabar.Image")));
            this.ToolGrabar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolGrabar.Name = "ToolGrabar";
            this.ToolGrabar.Size = new System.Drawing.Size(36, 36);
            this.ToolGrabar.Text = "toolStripButton4";
            this.ToolGrabar.ToolTipText = "Grabar registro";
            this.ToolGrabar.Click += new System.EventHandler(this.ToolGrabar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            this.toolStripSeparator2.Visible = false;
            // 
            // ToolCancelar
            // 
            this.ToolCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolCancelar.Image = ((System.Drawing.Image)(resources.GetObject("ToolCancelar.Image")));
            this.ToolCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolCancelar.Name = "ToolCancelar";
            this.ToolCancelar.Size = new System.Drawing.Size(36, 36);
            this.ToolCancelar.Text = "toolStripButton5";
            this.ToolCancelar.ToolTipText = "Cancelar";
            this.ToolCancelar.Click += new System.EventHandler(this.ToolCancelar_Click);
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
            // panel2
            // 
            this.panel2.Controls.Add(this.OptEst3);
            this.panel2.Controls.Add(this.OptEst2);
            this.panel2.Controls.Add(this.OptEst1);
            this.panel2.Location = new System.Drawing.Point(617, 75);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(346, 24);
            this.panel2.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(564, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Estado";
            // 
            // OptEst1
            // 
            this.OptEst1.AutoSize = true;
            this.OptEst1.Location = new System.Drawing.Point(11, 4);
            this.OptEst1.Name = "OptEst1";
            this.OptEst1.Size = new System.Drawing.Size(55, 17);
            this.OptEst1.TabIndex = 1;
            this.OptEst1.TabStop = true;
            this.OptEst1.Text = "Todos";
            this.OptEst1.UseVisualStyleBackColor = true;
            // 
            // OptEst2
            // 
            this.OptEst2.AutoSize = true;
            this.OptEst2.Location = new System.Drawing.Point(119, 4);
            this.OptEst2.Name = "OptEst2";
            this.OptEst2.Size = new System.Drawing.Size(78, 17);
            this.OptEst2.TabIndex = 2;
            this.OptEst2.TabStop = true;
            this.OptEst2.Text = "Facturados";
            this.OptEst2.UseVisualStyleBackColor = true;
            // 
            // OptEst3
            // 
            this.OptEst3.AutoSize = true;
            this.OptEst3.Location = new System.Drawing.Point(243, 4);
            this.OptEst3.Name = "OptEst3";
            this.OptEst3.Size = new System.Drawing.Size(95, 17);
            this.OptEst3.TabIndex = 3;
            this.OptEst3.TabStop = true;
            this.OptEst3.Text = "No Facturados";
            this.OptEst3.UseVisualStyleBackColor = true;
            // 
            // FrmRepMovAlm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 591);
            this.Controls.Add(this.ToolHerramientas);
            this.Controls.Add(this.Cs1);
            this.Name = "FrmRepMovAlm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmRepMovAlm";
            this.Load += new System.EventHandler(this.FrmRepMovAlm_Load);
            this.Resize += new System.EventHandler(this.FrmRepMovAlm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.Cs1)).EndInit();
            this.Cs1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgFlex)).EndInit();
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker TxtFchIni;
        private System.Windows.Forms.DateTimePicker TxtFchFin;
        private System.Windows.Forms.RadioButton OptIng;
        private System.Windows.Forms.RadioButton OptSal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CboAlmacen;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1Sizer.C1Sizer Cs1;
        private System.Windows.Forms.Panel panel1;
        private C1.Win.C1FlexGrid.C1FlexGrid FgFlex;
        private System.Windows.Forms.ComboBox CboTipOpe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStrip ToolHerramientas;
        private System.Windows.Forms.ToolStripButton ToolNuevo;
        private System.Windows.Forms.ToolStripButton ToolModificar;
        private System.Windows.Forms.ToolStripButton ToolEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolGrabar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ToolCancelar;
        private System.Windows.Forms.ToolStripButton ToolImprimir;
        private System.Windows.Forms.ToolStripButton ToolSalir;
        private System.Windows.Forms.ComboBox CboItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox CboTipExi;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton OptEst3;
        private System.Windows.Forms.RadioButton OptEst2;
        private System.Windows.Forms.RadioButton OptEst1;
    }
}