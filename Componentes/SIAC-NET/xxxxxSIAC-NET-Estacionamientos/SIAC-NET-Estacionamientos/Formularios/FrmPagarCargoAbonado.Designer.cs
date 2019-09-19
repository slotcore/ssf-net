namespace SIAC_NET_Estacionamientos.Formularios
{
    partial class FrmPagarCargoAbonado
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
            System.Windows.Forms.TextBox TxtVuelto;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPagarCargoAbonado));
            this.TxtNumPla = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtFecha = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.CboMoneda = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.LblImpPag = new System.Windows.Forms.Label();
            this.CboCajero = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.LblTc = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.LbIdCliente = new System.Windows.Forms.Label();
            this.CmdAce = new System.Windows.Forms.Button();
            this.CmdCan = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TxtACuenta = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtImpPag = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtNumDoc = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtNumSer = new System.Windows.Forms.TextBox();
            this.CboTipDoc = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CmdBusTra = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.FgReg = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CboTipCliCli = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtApeNom = new System.Windows.Forms.TextBox();
            TxtVuelto = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgReg)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtVuelto
            // 
            TxtVuelto.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            TxtVuelto.Location = new System.Drawing.Point(121, 453);
            TxtVuelto.Name = "TxtVuelto";
            TxtVuelto.ReadOnly = true;
            TxtVuelto.Size = new System.Drawing.Size(89, 21);
            TxtVuelto.TabIndex = 93;
            TxtVuelto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtNumPla
            // 
            this.TxtNumPla.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNumPla.Location = new System.Drawing.Point(118, 28);
            this.TxtNumPla.Name = "TxtNumPla";
            this.TxtNumPla.ReadOnly = true;
            this.TxtNumPla.Size = new System.Drawing.Size(80, 21);
            this.TxtNumPla.TabIndex = 0;
            this.TxtNumPla.TextChanged += new System.EventHandler(this.TxtNumPla_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtFecha);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.CboMoneda);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.LblImpPag);
            this.groupBox1.Controls.Add(this.CboCajero);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.LblTc);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.LbIdCliente);
            this.groupBox1.Controls.Add(this.CmdAce);
            this.groupBox1.Controls.Add(this.CmdCan);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(TxtVuelto);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.TxtACuenta);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.TxtImpPag);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.TxtNumDoc);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.TxtNumSer);
            this.groupBox1.Controls.Add(this.CboTipDoc);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.CmdBusTra);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.FgReg);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CboTipCliCli);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxtApeNom);
            this.groupBox1.Controls.Add(this.TxtNumPla);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox1.Location = new System.Drawing.Point(4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(708, 480);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "...:: Cargos del Abonado ::..";
            // 
            // TxtFecha
            // 
            this.TxtFecha.Enabled = false;
            this.TxtFecha.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFecha.Location = new System.Drawing.Point(121, 308);
            this.TxtFecha.Name = "TxtFecha";
            this.TxtFecha.Size = new System.Drawing.Size(94, 21);
            this.TxtFecha.TabIndex = 106;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(5, 310);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(88, 13);
            this.label16.TabIndex = 105;
            this.label16.Text = "Fecha Emision";
            // 
            // CboMoneda
            // 
            this.CboMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMoneda.Enabled = false;
            this.CboMoneda.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboMoneda.FormattingEnabled = true;
            this.CboMoneda.Location = new System.Drawing.Point(354, 308);
            this.CboMoneda.Name = "CboMoneda";
            this.CboMoneda.Size = new System.Drawing.Size(126, 21);
            this.CboMoneda.TabIndex = 103;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(293, 310);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 13);
            this.label15.TabIndex = 104;
            this.label15.Text = "Moneda";
            // 
            // LblImpPag
            // 
            this.LblImpPag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblImpPag.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblImpPag.ForeColor = System.Drawing.Color.Black;
            this.LblImpPag.Location = new System.Drawing.Point(568, 274);
            this.LblImpPag.Name = "LblImpPag";
            this.LblImpPag.Size = new System.Drawing.Size(80, 22);
            this.LblImpPag.TabIndex = 102;
            this.LblImpPag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CboCajero
            // 
            this.CboCajero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboCajero.Enabled = false;
            this.CboCajero.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboCajero.FormattingEnabled = true;
            this.CboCajero.Location = new System.Drawing.Point(121, 332);
            this.CboCajero.Name = "CboCajero";
            this.CboCajero.Size = new System.Drawing.Size(359, 21);
            this.CboCajero.TabIndex = 100;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(3, 336);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(46, 13);
            this.label13.TabIndex = 101;
            this.label13.Text = "Cajero";
            // 
            // LblTc
            // 
            this.LblTc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTc.ForeColor = System.Drawing.Color.Blue;
            this.LblTc.Location = new System.Drawing.Point(621, 28);
            this.LblTc.Name = "LblTc";
            this.LblTc.Size = new System.Drawing.Size(80, 22);
            this.LblTc.TabIndex = 99;
            this.LblTc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(531, 32);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 13);
            this.label12.TabIndex = 98;
            this.label12.Text = "Tipo Cambio";
            // 
            // LbIdCliente
            // 
            this.LbIdCliente.AutoSize = true;
            this.LbIdCliente.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbIdCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LbIdCliente.Location = new System.Drawing.Point(295, 33);
            this.LbIdCliente.Name = "LbIdCliente";
            this.LbIdCliente.Size = new System.Drawing.Size(81, 13);
            this.LbIdCliente.TabIndex = 97;
            this.LbIdCliente.Text = "LbIdCliente";
            this.LbIdCliente.Visible = false;
            // 
            // CmdAce
            // 
            this.CmdAce.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdAce.ForeColor = System.Drawing.Color.Black;
            this.CmdAce.Image = ((System.Drawing.Image)(resources.GetObject("CmdAce.Image")));
            this.CmdAce.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdAce.Location = new System.Drawing.Point(493, 423);
            this.CmdAce.Name = "CmdAce";
            this.CmdAce.Size = new System.Drawing.Size(96, 40);
            this.CmdAce.TabIndex = 95;
            this.CmdAce.Text = "Aceptar";
            this.CmdAce.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdAce.UseVisualStyleBackColor = true;
            this.CmdAce.Click += new System.EventHandler(this.CmdAce_Click);
            // 
            // CmdCan
            // 
            this.CmdCan.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdCan.ForeColor = System.Drawing.Color.Black;
            this.CmdCan.Image = ((System.Drawing.Image)(resources.GetObject("CmdCan.Image")));
            this.CmdCan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdCan.Location = new System.Drawing.Point(590, 423);
            this.CmdCan.Name = "CmdCan";
            this.CmdCan.Size = new System.Drawing.Size(96, 40);
            this.CmdCan.TabIndex = 96;
            this.CmdCan.Text = "Cancelar";
            this.CmdCan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdCan.UseVisualStyleBackColor = true;
            this.CmdCan.Click += new System.EventHandler(this.CmdCan_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(458, 278);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 13);
            this.label11.TabIndex = 94;
            this.label11.Text = "Importe a Pagar";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(3, 457);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 92;
            this.label10.Text = "Vuelto";
            // 
            // TxtACuenta
            // 
            this.TxtACuenta.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtACuenta.Location = new System.Drawing.Point(121, 429);
            this.TxtACuenta.Name = "TxtACuenta";
            this.TxtACuenta.Size = new System.Drawing.Size(89, 21);
            this.TxtACuenta.TabIndex = 91;
            this.TxtACuenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(3, 433);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 90;
            this.label9.Text = "Acuenta";
            // 
            // TxtImpPag
            // 
            this.TxtImpPag.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtImpPag.Location = new System.Drawing.Point(121, 405);
            this.TxtImpPag.Name = "TxtImpPag";
            this.TxtImpPag.ReadOnly = true;
            this.TxtImpPag.Size = new System.Drawing.Size(89, 21);
            this.TxtImpPag.TabIndex = 89;
            this.TxtImpPag.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(3, 409);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 88;
            this.label8.Text = "Importe";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label7.Location = new System.Drawing.Point(4, 284);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(180, 14);
            this.label7.TabIndex = 87;
            this.label7.Text = "..:: Documento a Emitir ::..";
            // 
            // TxtNumDoc
            // 
            this.TxtNumDoc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNumDoc.Location = new System.Drawing.Point(174, 380);
            this.TxtNumDoc.Name = "TxtNumDoc";
            this.TxtNumDoc.ReadOnly = true;
            this.TxtNumDoc.Size = new System.Drawing.Size(104, 21);
            this.TxtNumDoc.TabIndex = 86;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(3, 383);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 85;
            this.label6.Text = "Nº Documento";
            // 
            // TxtNumSer
            // 
            this.TxtNumSer.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNumSer.Location = new System.Drawing.Point(121, 380);
            this.TxtNumSer.Name = "TxtNumSer";
            this.TxtNumSer.ReadOnly = true;
            this.TxtNumSer.Size = new System.Drawing.Size(50, 21);
            this.TxtNumSer.TabIndex = 84;
            // 
            // CboTipDoc
            // 
            this.CboTipDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTipDoc.Enabled = false;
            this.CboTipDoc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboTipDoc.FormattingEnabled = true;
            this.CboTipDoc.Location = new System.Drawing.Point(121, 355);
            this.CboTipDoc.Name = "CboTipDoc";
            this.CboTipDoc.Size = new System.Drawing.Size(359, 21);
            this.CboTipDoc.TabIndex = 82;
            this.CboTipDoc.SelectedValueChanged += new System.EventHandler(this.CboTipDoc_SelectedValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(3, 358);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 83;
            this.label5.Text = "Tipo Documento";
            // 
            // CmdBusTra
            // 
            this.CmdBusTra.BackColor = System.Drawing.Color.White;
            this.CmdBusTra.ForeColor = System.Drawing.Color.Black;
            this.CmdBusTra.Image = ((System.Drawing.Image)(resources.GetObject("CmdBusTra.Image")));
            this.CmdBusTra.Location = new System.Drawing.Point(204, 24);
            this.CmdBusTra.Name = "CmdBusTra";
            this.CmdBusTra.Size = new System.Drawing.Size(40, 28);
            this.CmdBusTra.TabIndex = 81;
            this.CmdBusTra.UseVisualStyleBackColor = false;
            this.CmdBusTra.Click += new System.EventHandler(this.CmdBusTra_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(3, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Cargos del Abonado";
            // 
            // FgReg
            // 
            this.FgReg.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgReg.ColumnInfo = "10,1,0,0,0,90,Columns:0{Width:12;}\t";
            this.FgReg.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FgReg.Location = new System.Drawing.Point(4, 127);
            this.FgReg.Name = "FgReg";
            this.FgReg.Rows.DefaultSize = 18;
            this.FgReg.Size = new System.Drawing.Size(699, 144);
            this.FgReg.StyleInfo = resources.GetString("FgReg.StyleInfo");
            this.FgReg.TabIndex = 9;
            this.FgReg.EnterCell += new System.EventHandler(this.FgReg_EnterCell);
            this.FgReg.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgReg_CellChanged);
            this.FgReg.Enter += new System.EventHandler(this.FgReg_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(3, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Nº Placa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Cliente";
            // 
            // CboTipCliCli
            // 
            this.CboTipCliCli.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTipCliCli.Enabled = false;
            this.CboTipCliCli.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboTipCliCli.FormattingEnabled = true;
            this.CboTipCliCli.Location = new System.Drawing.Point(118, 78);
            this.CboTipCliCli.Name = "CboTipCliCli";
            this.CboTipCliCli.Size = new System.Drawing.Size(359, 21);
            this.CboTipCliCli.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tipo Cliente";
            // 
            // TxtApeNom
            // 
            this.TxtApeNom.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtApeNom.Location = new System.Drawing.Point(118, 56);
            this.TxtApeNom.Name = "TxtApeNom";
            this.TxtApeNom.ReadOnly = true;
            this.TxtApeNom.Size = new System.Drawing.Size(359, 21);
            this.TxtApeNom.TabIndex = 1;
            // 
            // FrmPagarCargoAbonado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 487);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPagarCargoAbonado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmPagarCargoAbonado";
            this.Load += new System.EventHandler(this.FrmPagarCargoAbonado_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgReg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TxtNumPla;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TxtApeNom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CboTipCliCli;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1FlexGrid.C1FlexGrid FgReg;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TxtACuenta;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TxtImpPag;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtNumDoc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtNumSer;
        private System.Windows.Forms.ComboBox CboTipDoc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button CmdBusTra;
        private System.Windows.Forms.Button CmdAce;
        private System.Windows.Forms.Button CmdCan;
        private System.Windows.Forms.Label LbIdCliente;
        private System.Windows.Forms.Label LblTc;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox CboCajero;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label LblImpPag;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox CboMoneda;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker TxtFecha;
    }
}