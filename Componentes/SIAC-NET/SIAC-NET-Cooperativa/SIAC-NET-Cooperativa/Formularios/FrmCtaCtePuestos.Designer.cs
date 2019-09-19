namespace SIAC_NET_Cooperativa.Formularios
{
    partial class FrmCtaCtePuestos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCtaCtePuestos));
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
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.c1Sizer2 = new C1.Win.C1Sizer.C1Sizer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TxtTotal = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.FgDeuda = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CmdExtPag = new System.Windows.Forms.Button();
            this.CmdGenPag = new System.Windows.Forms.Button();
            this.LblIdSoc = new System.Windows.Forms.Label();
            this.LblIdPuesto = new System.Windows.Forms.Label();
            this.CmdMostrarDeuda = new System.Windows.Forms.Button();
            this.OptTod = new System.Windows.Forms.RadioButton();
            this.OptSolDeu = new System.Windows.Forms.RadioButton();
            this.CmdProPen = new System.Windows.Forms.Button();
            this.TxtFchIng = new System.Windows.Forms.DateTimePicker();
            this.TxtSer = new System.Windows.Forms.TextBox();
            this.TxtNomSoc = new System.Windows.Forms.TextBox();
            this.TxtCodPue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ToolHerramientas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
            this.c1Sizer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer2)).BeginInit();
            this.c1Sizer2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgDeuda)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.ToolHerramientas.Size = new System.Drawing.Size(938, 39);
            this.ToolHerramientas.TabIndex = 30;
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
            this.ToolNuevo.ToolTipText = "Generar Cargo al Asociado";
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
            // c1Sizer1
            // 
            this.c1Sizer1.Controls.Add(this.c1Sizer2);
            this.c1Sizer1.Controls.Add(this.panel1);
            this.c1Sizer1.GridDefinition = "16.7755991285403:False:True;82.5708061002179:False:False;\t99.786552828175:False:F" +
    "alse;";
            this.c1Sizer1.Location = new System.Drawing.Point(0, 42);
            this.c1Sizer1.Margin = new System.Windows.Forms.Padding(1);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Padding = new System.Windows.Forms.Padding(1);
            this.c1Sizer1.Size = new System.Drawing.Size(937, 459);
            this.c1Sizer1.SplitterWidth = 1;
            this.c1Sizer1.TabIndex = 31;
            this.c1Sizer1.Text = "c1Sizer1";
            // 
            // c1Sizer2
            // 
            this.c1Sizer2.Controls.Add(this.panel2);
            this.c1Sizer2.Controls.Add(this.FgDeuda);
            this.c1Sizer2.GridDefinition = "89.1820580474934:False:False;10.0263852242744:False:True;\t99.7860962566845:False:" +
    "False;";
            this.c1Sizer2.Location = new System.Drawing.Point(1, 79);
            this.c1Sizer2.Margin = new System.Windows.Forms.Padding(1);
            this.c1Sizer2.Name = "c1Sizer2";
            this.c1Sizer2.Padding = new System.Windows.Forms.Padding(1);
            this.c1Sizer2.Size = new System.Drawing.Size(935, 379);
            this.c1Sizer2.SplitterWidth = 1;
            this.c1Sizer2.TabIndex = 1;
            this.c1Sizer2.Text = "c1Sizer2";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.TxtTotal);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(1, 340);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(933, 38);
            this.panel2.TabIndex = 2;
            // 
            // TxtTotal
            // 
            this.TxtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotal.Location = new System.Drawing.Point(595, 8);
            this.TxtTotal.Name = "TxtTotal";
            this.TxtTotal.Size = new System.Drawing.Size(82, 20);
            this.TxtTotal.TabIndex = 9;
            this.TxtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(508, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Total ==>";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(251, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Documentos Pendientes";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Red;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(208, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "label8";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(51, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Documentos Cancelados";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Blue;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(10, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "labels";
            // 
            // FgDeuda
            // 
            this.FgDeuda.ColumnInfo = "10,1,0,0,0,85,Columns:";
            this.FgDeuda.Location = new System.Drawing.Point(1, 1);
            this.FgDeuda.Name = "FgDeuda";
            this.FgDeuda.Rows.DefaultSize = 17;
            this.FgDeuda.Size = new System.Drawing.Size(933, 338);
            this.FgDeuda.TabIndex = 8;
            this.FgDeuda.EnterCell += new System.EventHandler(this.FgDeuda_EnterCell);
            this.FgDeuda.CellChecked += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgDeuda_CellChecked);
            this.FgDeuda.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgDeuda_CellChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CmdExtPag);
            this.panel1.Controls.Add(this.CmdGenPag);
            this.panel1.Controls.Add(this.LblIdSoc);
            this.panel1.Controls.Add(this.LblIdPuesto);
            this.panel1.Controls.Add(this.CmdMostrarDeuda);
            this.panel1.Controls.Add(this.OptTod);
            this.panel1.Controls.Add(this.OptSolDeu);
            this.panel1.Controls.Add(this.CmdProPen);
            this.panel1.Controls.Add(this.TxtFchIng);
            this.panel1.Controls.Add(this.TxtSer);
            this.panel1.Controls.Add(this.TxtNomSoc);
            this.panel1.Controls.Add(this.TxtCodPue);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(935, 77);
            this.panel1.TabIndex = 0;
            // 
            // CmdExtPag
            // 
            this.CmdExtPag.Image = ((System.Drawing.Image)(resources.GetObject("CmdExtPag.Image")));
            this.CmdExtPag.Location = new System.Drawing.Point(862, 12);
            this.CmdExtPag.Name = "CmdExtPag";
            this.CmdExtPag.Size = new System.Drawing.Size(62, 55);
            this.CmdExtPag.TabIndex = 135;
            this.CmdExtPag.UseVisualStyleBackColor = true;
            this.CmdExtPag.Click += new System.EventHandler(this.CmdExtPag_Click);
            // 
            // CmdGenPag
            // 
            this.CmdGenPag.Image = ((System.Drawing.Image)(resources.GetObject("CmdGenPag.Image")));
            this.CmdGenPag.Location = new System.Drawing.Point(800, 12);
            this.CmdGenPag.Name = "CmdGenPag";
            this.CmdGenPag.Size = new System.Drawing.Size(62, 55);
            this.CmdGenPag.TabIndex = 2;
            this.CmdGenPag.UseVisualStyleBackColor = true;
            this.CmdGenPag.Click += new System.EventHandler(this.CmdGenPag_Click);
            // 
            // LblIdSoc
            // 
            this.LblIdSoc.AutoSize = true;
            this.LblIdSoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIdSoc.ForeColor = System.Drawing.Color.Red;
            this.LblIdSoc.Location = new System.Drawing.Point(463, 11);
            this.LblIdSoc.Name = "LblIdSoc";
            this.LblIdSoc.Size = new System.Drawing.Size(67, 13);
            this.LblIdSoc.TabIndex = 134;
            this.LblIdSoc.Text = "LblIdSocio";
            this.LblIdSoc.Visible = false;
            // 
            // LblIdPuesto
            // 
            this.LblIdPuesto.AutoSize = true;
            this.LblIdPuesto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIdPuesto.ForeColor = System.Drawing.Color.Red;
            this.LblIdPuesto.Location = new System.Drawing.Point(361, 11);
            this.LblIdPuesto.Name = "LblIdPuesto";
            this.LblIdPuesto.Size = new System.Drawing.Size(74, 13);
            this.LblIdPuesto.TabIndex = 133;
            this.LblIdPuesto.Text = "LblIdPuesto";
            this.LblIdPuesto.Visible = false;
            // 
            // CmdMostrarDeuda
            // 
            this.CmdMostrarDeuda.Image = ((System.Drawing.Image)(resources.GetObject("CmdMostrarDeuda.Image")));
            this.CmdMostrarDeuda.Location = new System.Drawing.Point(738, 12);
            this.CmdMostrarDeuda.Name = "CmdMostrarDeuda";
            this.CmdMostrarDeuda.Size = new System.Drawing.Size(62, 55);
            this.CmdMostrarDeuda.TabIndex = 1;
            this.CmdMostrarDeuda.UseVisualStyleBackColor = true;
            this.CmdMostrarDeuda.Click += new System.EventHandler(this.CmdMostrarDeuda_Click);
            // 
            // OptTod
            // 
            this.OptTod.AutoSize = true;
            this.OptTod.Location = new System.Drawing.Point(582, 43);
            this.OptTod.Name = "OptTod";
            this.OptTod.Size = new System.Drawing.Size(143, 17);
            this.OptTod.TabIndex = 7;
            this.OptTod.TabStop = true;
            this.OptTod.Text = "Mostrar Toda la Cta. Cte.";
            this.OptTod.UseVisualStyleBackColor = true;
            // 
            // OptSolDeu
            // 
            this.OptSolDeu.AutoSize = true;
            this.OptSolDeu.Location = new System.Drawing.Point(582, 19);
            this.OptSolDeu.Name = "OptSolDeu";
            this.OptSolDeu.Size = new System.Drawing.Size(119, 17);
            this.OptSolDeu.TabIndex = 6;
            this.OptSolDeu.TabStop = true;
            this.OptSolDeu.Text = "Mostrar Solo Deuda";
            this.OptSolDeu.UseVisualStyleBackColor = true;
            // 
            // CmdProPen
            // 
            this.CmdProPen.Image = ((System.Drawing.Image)(resources.GetObject("CmdProPen.Image")));
            this.CmdProPen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdProPen.Location = new System.Drawing.Point(203, 3);
            this.CmdProPen.Name = "CmdProPen";
            this.CmdProPen.Size = new System.Drawing.Size(108, 24);
            this.CmdProPen.TabIndex = 129;
            this.CmdProPen.Text = "Buscar Puesto";
            this.CmdProPen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdProPen.UseVisualStyleBackColor = true;
            this.CmdProPen.Click += new System.EventHandler(this.CmdProPen_Click);
            // 
            // TxtFchIng
            // 
            this.TxtFchIng.Enabled = false;
            this.TxtFchIng.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchIng.Location = new System.Drawing.Point(466, 51);
            this.TxtFchIng.Name = "TxtFchIng";
            this.TxtFchIng.Size = new System.Drawing.Size(97, 20);
            this.TxtFchIng.TabIndex = 5;
            this.TxtFchIng.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dateTimePicker1_KeyPress);
            // 
            // TxtSer
            // 
            this.TxtSer.Enabled = false;
            this.TxtSer.Location = new System.Drawing.Point(113, 51);
            this.TxtSer.Name = "TxtSer";
            this.TxtSer.Size = new System.Drawing.Size(198, 20);
            this.TxtSer.TabIndex = 4;
            this.TxtSer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            // 
            // TxtNomSoc
            // 
            this.TxtNomSoc.Enabled = false;
            this.TxtNomSoc.Location = new System.Drawing.Point(113, 28);
            this.TxtNomSoc.Name = "TxtNomSoc";
            this.TxtNomSoc.Size = new System.Drawing.Size(450, 20);
            this.TxtNomSoc.TabIndex = 3;
            this.TxtNomSoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // TxtCodPue
            // 
            this.TxtCodPue.Location = new System.Drawing.Point(113, 5);
            this.TxtCodPue.MaxLength = 10;
            this.TxtCodPue.Name = "TxtCodPue";
            this.TxtCodPue.Size = new System.Drawing.Size(84, 20);
            this.TxtCodPue.TabIndex = 0;
            this.TxtCodPue.TextChanged += new System.EventHandler(this.TxtCodPue_TextChanged);
            this.TxtCodPue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCodPue_KeyPress);
            this.TxtCodPue.Validated += new System.EventHandler(this.TxtCodPue_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(387, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Fch. Ingreso";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tipo Socio";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre Socio";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nº Puesto";
            // 
            // FrmCtaCtePuestos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 516);
            this.Controls.Add(this.c1Sizer1);
            this.Controls.Add(this.ToolHerramientas);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCtaCtePuestos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCtaCtePuestos";
            this.Activated += new System.EventHandler(this.FrmCtaCtePuestos_Activated);
            this.Load += new System.EventHandler(this.FrmCtaCtePuestos_Load);
            this.Resize += new System.EventHandler(this.FrmCtaCtePuestos_Resize);
            this.ToolHerramientas.ResumeLayout(false);
            this.ToolHerramientas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer2)).EndInit();
            this.c1Sizer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgDeuda)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private C1.Win.C1FlexGrid.C1FlexGrid FgDeuda;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker TxtFchIng;
        private System.Windows.Forms.TextBox TxtSer;
        private System.Windows.Forms.TextBox TxtNomSoc;
        private System.Windows.Forms.TextBox TxtCodPue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button CmdMostrarDeuda;
        private System.Windows.Forms.RadioButton OptTod;
        private System.Windows.Forms.RadioButton OptSolDeu;
        private System.Windows.Forms.Button CmdProPen;
        private System.Windows.Forms.Label LblIdPuesto;
        private C1.Win.C1Sizer.C1Sizer c1Sizer2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox TxtTotal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LblIdSoc;
        private System.Windows.Forms.Button CmdGenPag;
        private System.Windows.Forms.Button CmdExtPag;
    }
}