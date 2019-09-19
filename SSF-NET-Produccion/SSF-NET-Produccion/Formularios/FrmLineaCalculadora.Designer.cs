namespace SSF_NET_Produccion.Formularios
{
    partial class FrmLineaCalculadora
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLineaCalculadora));
            this.Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.CmdDelTar = new System.Windows.Forms.Button();
            this.CmdAddTar = new System.Windows.Forms.Button();
            this.TxtDato4 = new System.Windows.Forms.TextBox();
            this.TxtDato5 = new System.Windows.Forms.TextBox();
            this.TxtDato3 = new System.Windows.Forms.TextBox();
            this.TxtDato2 = new System.Windows.Forms.TextBox();
            this.TxtDato1 = new System.Windows.Forms.TextBox();
            this.FgTarea = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CmdCalLin = new System.Windows.Forms.Button();
            this.TxtPreLin = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtEfiTot = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtNumOpe = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtTieMax = new System.Windows.Forms.DateTimePicker();
            this.TxtCanPro = new System.Windows.Forms.TextBox();
            this.CboLin = new System.Windows.Forms.ComboBox();
            this.CboPro = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ToolHerramientas = new System.Windows.Forms.ToolStrip();
            this.ToolNuevo = new System.Windows.Forms.ToolStripButton();
            this.ToolModificar = new System.Windows.Forms.ToolStripButton();
            this.ToolEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolGrabar = new System.Windows.Forms.ToolStripButton();
            this.ToolCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolImprimir = new System.Windows.Forms.ToolStripSplitButton();
            this.toomenimp1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolSalir = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.Sizer1)).BeginInit();
            this.Sizer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgTarea)).BeginInit();
            this.panel1.SuspendLayout();
            this.ToolHerramientas.SuspendLayout();
            this.SuspendLayout();
            // 
            // Sizer1
            // 
            this.Sizer1.Controls.Add(this.panel2);
            this.Sizer1.Controls.Add(this.FgTarea);
            this.Sizer1.Controls.Add(this.panel1);
            this.Sizer1.GridDefinition = "35.799522673031:False:True;48.9260143198091:False:False;11.4558472553699:False:Tr" +
    "ue;\t0.196656833824975:False:False;98.0334316617502:False:False;0.196656833824975" +
    ":False:False;";
            this.Sizer1.Location = new System.Drawing.Point(1, 42);
            this.Sizer1.Name = "Sizer1";
            this.Sizer1.Size = new System.Drawing.Size(1017, 419);
            this.Sizer1.TabIndex = 0;
            this.Sizer1.Text = "c1Sizer1";
            this.Sizer1.Click += new System.EventHandler(this.Sizer1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.TxtDato4);
            this.panel2.Controls.Add(this.TxtDato5);
            this.panel2.Controls.Add(this.TxtDato3);
            this.panel2.Controls.Add(this.TxtDato2);
            this.panel2.Controls.Add(this.TxtDato1);
            this.panel2.Location = new System.Drawing.Point(10, 367);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(997, 48);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.CmdDelTar);
            this.panel3.Controls.Add(this.CmdAddTar);
            this.panel3.Location = new System.Drawing.Point(1, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(443, 43);
            this.panel3.TabIndex = 13;
            // 
            // CmdDelTar
            // 
            this.CmdDelTar.Location = new System.Drawing.Point(121, 3);
            this.CmdDelTar.Name = "CmdDelTar";
            this.CmdDelTar.Size = new System.Drawing.Size(107, 34);
            this.CmdDelTar.TabIndex = 1;
            this.CmdDelTar.Text = "Eliminar Tarea";
            this.CmdDelTar.UseVisualStyleBackColor = true;
            // 
            // CmdAddTar
            // 
            this.CmdAddTar.Location = new System.Drawing.Point(12, 3);
            this.CmdAddTar.Name = "CmdAddTar";
            this.CmdAddTar.Size = new System.Drawing.Size(107, 34);
            this.CmdAddTar.TabIndex = 0;
            this.CmdAddTar.Text = "Agregar Tarea";
            this.CmdAddTar.UseVisualStyleBackColor = true;
            // 
            // TxtDato4
            // 
            this.TxtDato4.Location = new System.Drawing.Point(812, 2);
            this.TxtDato4.Name = "TxtDato4";
            this.TxtDato4.ReadOnly = true;
            this.TxtDato4.Size = new System.Drawing.Size(58, 20);
            this.TxtDato4.TabIndex = 11;
            this.TxtDato4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtDato5
            // 
            this.TxtDato5.Location = new System.Drawing.Point(874, 2);
            this.TxtDato5.Name = "TxtDato5";
            this.TxtDato5.ReadOnly = true;
            this.TxtDato5.Size = new System.Drawing.Size(58, 20);
            this.TxtDato5.TabIndex = 12;
            this.TxtDato5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtDato3
            // 
            this.TxtDato3.Location = new System.Drawing.Point(750, 2);
            this.TxtDato3.Name = "TxtDato3";
            this.TxtDato3.ReadOnly = true;
            this.TxtDato3.Size = new System.Drawing.Size(58, 20);
            this.TxtDato3.TabIndex = 10;
            this.TxtDato3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtDato2
            // 
            this.TxtDato2.Location = new System.Drawing.Point(633, 2);
            this.TxtDato2.Name = "TxtDato2";
            this.TxtDato2.ReadOnly = true;
            this.TxtDato2.Size = new System.Drawing.Size(58, 20);
            this.TxtDato2.TabIndex = 9;
            this.TxtDato2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtDato1
            // 
            this.TxtDato1.Location = new System.Drawing.Point(512, 2);
            this.TxtDato1.Name = "TxtDato1";
            this.TxtDato1.ReadOnly = true;
            this.TxtDato1.Size = new System.Drawing.Size(58, 20);
            this.TxtDato1.TabIndex = 8;
            this.TxtDato1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FgTarea
            // 
            this.FgTarea.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:10;}\t";
            this.FgTarea.Location = new System.Drawing.Point(10, 158);
            this.FgTarea.Name = "FgTarea";
            this.FgTarea.Rows.DefaultSize = 17;
            this.FgTarea.Size = new System.Drawing.Size(997, 205);
            this.FgTarea.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CmdCalLin);
            this.panel1.Controls.Add(this.TxtPreLin);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.TxtEfiTot);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.TxtNumOpe);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.TxtTieMax);
            this.panel1.Controls.Add(this.TxtCanPro);
            this.panel1.Controls.Add(this.CboLin);
            this.panel1.Controls.Add(this.CboPro);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(10, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(997, 150);
            this.panel1.TabIndex = 0;
            // 
            // CmdCalLin
            // 
            this.CmdCalLin.Image = ((System.Drawing.Image)(resources.GetObject("CmdCalLin.Image")));
            this.CmdCalLin.Location = new System.Drawing.Point(837, 96);
            this.CmdCalLin.Name = "CmdCalLin";
            this.CmdCalLin.Size = new System.Drawing.Size(155, 49);
            this.CmdCalLin.TabIndex = 7;
            this.CmdCalLin.Text = "Calcular Linea";
            this.CmdCalLin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdCalLin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CmdCalLin.UseVisualStyleBackColor = true;
            this.CmdCalLin.Click += new System.EventHandler(this.CmdCalLin_Click);
            // 
            // TxtPreLin
            // 
            this.TxtPreLin.Location = new System.Drawing.Point(830, 49);
            this.TxtPreLin.Name = "TxtPreLin";
            this.TxtPreLin.Size = new System.Drawing.Size(84, 20);
            this.TxtPreLin.TabIndex = 2;
            this.TxtPreLin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(773, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Precio ";
            // 
            // TxtEfiTot
            // 
            this.TxtEfiTot.Location = new System.Drawing.Point(437, 126);
            this.TxtEfiTot.Name = "TxtEfiTot";
            this.TxtEfiTot.ReadOnly = true;
            this.TxtEfiTot.Size = new System.Drawing.Size(61, 20);
            this.TxtEfiTot.TabIndex = 6;
            this.TxtEfiTot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(342, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Eficiencia Total";
            // 
            // TxtNumOpe
            // 
            this.TxtNumOpe.Location = new System.Drawing.Point(124, 126);
            this.TxtNumOpe.Name = "TxtNumOpe";
            this.TxtNumOpe.ReadOnly = true;
            this.TxtNumOpe.Size = new System.Drawing.Size(61, 20);
            this.TxtNumOpe.TabIndex = 5;
            this.TxtNumOpe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 129);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Nº Operarios";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(14, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(187, 14);
            this.label6.TabIndex = 9;
            this.label6.Text = ":: DATOS A CALCULADOS::";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(14, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = ":: DATOS DE LA LINEA";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // TxtTieMax
            // 
            this.TxtTieMax.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.TxtTieMax.Location = new System.Drawing.Point(437, 72);
            this.TxtTieMax.Name = "TxtTieMax";
            this.TxtTieMax.Size = new System.Drawing.Size(86, 20);
            this.TxtTieMax.TabIndex = 4;
            // 
            // TxtCanPro
            // 
            this.TxtCanPro.Location = new System.Drawing.Point(124, 72);
            this.TxtCanPro.Name = "TxtCanPro";
            this.TxtCanPro.Size = new System.Drawing.Size(61, 20);
            this.TxtCanPro.TabIndex = 3;
            // 
            // CboLin
            // 
            this.CboLin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboLin.FormattingEnabled = true;
            this.CboLin.Location = new System.Drawing.Point(124, 49);
            this.CboLin.Name = "CboLin";
            this.CboLin.Size = new System.Drawing.Size(576, 21);
            this.CboLin.TabIndex = 1;
            this.CboLin.SelectedValueChanged += new System.EventHandler(this.CboLin_SelectedValueChanged);
            // 
            // CboPro
            // 
            this.CboPro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboPro.FormattingEnabled = true;
            this.CboPro.Location = new System.Drawing.Point(124, 26);
            this.CboPro.Name = "CboPro";
            this.CboPro.Size = new System.Drawing.Size(576, 21);
            this.CboPro.TabIndex = 0;
            this.CboPro.SelectedValueChanged += new System.EventHandler(this.CboPro_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(300, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tiempo Max Produccion";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Can. a Producir";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Linea";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Producto";
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
            this.ToolHerramientas.Size = new System.Drawing.Size(1026, 39);
            this.ToolHerramientas.TabIndex = 37;
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
            // 
            // ToolImprimir
            // 
            this.ToolImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolImprimir.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toomenimp1});
            this.ToolImprimir.Image = ((System.Drawing.Image)(resources.GetObject("ToolImprimir.Image")));
            this.ToolImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolImprimir.Name = "ToolImprimir";
            this.ToolImprimir.Size = new System.Drawing.Size(48, 36);
            this.ToolImprimir.Text = "toolStripSplitButton1";
            // 
            // toomenimp1
            // 
            this.toomenimp1.Name = "toomenimp1";
            this.toomenimp1.Size = new System.Drawing.Size(199, 22);
            this.toomenimp1.Text = "Imprimir Estacionalidad";
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
            // FrmLineaCalculadora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 461);
            this.Controls.Add(this.ToolHerramientas);
            this.Controls.Add(this.Sizer1);
            this.Name = "FrmLineaCalculadora";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLineaCalculadora";
            this.Load += new System.EventHandler(this.FrmLineaCalculadora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Sizer1)).EndInit();
            this.Sizer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgTarea)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Sizer.C1Sizer Sizer1;
        private C1.Win.C1FlexGrid.C1FlexGrid FgTarea;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker TxtTieMax;
        private System.Windows.Forms.TextBox TxtCanPro;
        private System.Windows.Forms.ComboBox CboLin;
        private System.Windows.Forms.ComboBox CboPro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtEfiTot;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtNumOpe;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtPreLin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button CmdCalLin;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox TxtDato4;
        private System.Windows.Forms.TextBox TxtDato5;
        private System.Windows.Forms.TextBox TxtDato3;
        private System.Windows.Forms.TextBox TxtDato2;
        private System.Windows.Forms.TextBox TxtDato1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button CmdDelTar;
        private System.Windows.Forms.Button CmdAddTar;
        private System.Windows.Forms.ToolStrip ToolHerramientas;
        private System.Windows.Forms.ToolStripButton ToolNuevo;
        private System.Windows.Forms.ToolStripButton ToolModificar;
        private System.Windows.Forms.ToolStripButton ToolEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolGrabar;
        private System.Windows.Forms.ToolStripButton ToolCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSplitButton ToolImprimir;
        private System.Windows.Forms.ToolStripMenuItem toomenimp1;
        private System.Windows.Forms.ToolStripButton ToolSalir;
    }
}