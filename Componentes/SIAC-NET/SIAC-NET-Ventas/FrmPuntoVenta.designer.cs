using MetroFramework.Forms;

namespace SIAC_NET_Ventas.Formularios
{
    partial class FrmPuntoVenta : MetroForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPuntoVenta));
            this.panel3 = new System.Windows.Forms.Panel();
            this.LblTipCam = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LblIdCliente = new System.Windows.Forms.Label();
            this.TxtDireccion = new System.Windows.Forms.TextBox();
            this.TxtNomCli = new System.Windows.Forms.TextBox();
            this.TxtNumRuc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LblTipDocumento = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TooNuevo = new System.Windows.Forms.ToolStripButton();
            this.ToolCliRapido = new System.Windows.Forms.ToolStripButton();
            this.ToolTraeCoti = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolGrabar = new System.Windows.Forms.ToolStripButton();
            this.C1Sizer2 = new C1.Win.C1Sizer.C1Sizer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CboTipDocumento = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.CboMoneda = new System.Windows.Forms.ComboBox();
            this.CmdCamcNumeracion = new System.Windows.Forms.Button();
            this.CmdTraerCotiza = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.LblIGVTasa = new System.Windows.Forms.Label();
            this.LblIgv = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LblTotal = new System.Windows.Forms.Label();
            this.CmdEsc = new System.Windows.Forms.Button();
            this.LblImpBru = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.FgDetalle = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.LblNumDoc = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.C1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.LblSerDoc = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1Sizer2)).BeginInit();
            this.C1Sizer2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgDetalle)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1Sizer1)).BeginInit();
            this.C1Sizer1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.LblTipCam);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.LblIdCliente);
            this.panel3.Controls.Add(this.TxtDireccion);
            this.panel3.Controls.Add(this.TxtNomCli);
            this.panel3.Controls.Add(this.TxtNumRuc);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(3, 77);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(798, 91);
            this.panel3.TabIndex = 8;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint_1);
            // 
            // LblTipCam
            // 
            this.LblTipCam.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblTipCam.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTipCam.ForeColor = System.Drawing.Color.Navy;
            this.LblTipCam.Location = new System.Drawing.Point(620, 2);
            this.LblTipCam.Name = "LblTipCam";
            this.LblTipCam.Size = new System.Drawing.Size(117, 26);
            this.LblTipCam.TabIndex = 52;
            this.LblTipCam.Text = "LblTipCam";
            this.LblTipCam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label6.Location = new System.Drawing.Point(514, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 51;
            this.label6.Text = "Tipo de Cambio";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // LblIdCliente
            // 
            this.LblIdCliente.AutoSize = true;
            this.LblIdCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIdCliente.ForeColor = System.Drawing.Color.Red;
            this.LblIdCliente.Location = new System.Drawing.Point(294, 10);
            this.LblIdCliente.Name = "LblIdCliente";
            this.LblIdCliente.Size = new System.Drawing.Size(74, 13);
            this.LblIdCliente.TabIndex = 50;
            this.LblIdCliente.Text = "LblIdCliente";
            this.LblIdCliente.Visible = false;
            // 
            // TxtDireccion
            // 
            this.TxtDireccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDireccion.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDireccion.Location = new System.Drawing.Point(142, 60);
            this.TxtDireccion.Multiline = true;
            this.TxtDireccion.Name = "TxtDireccion";
            this.TxtDireccion.Size = new System.Drawing.Size(595, 26);
            this.TxtDireccion.TabIndex = 2;
            this.TxtDireccion.Text = "TxtDireccion";
            this.TxtDireccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDireccion_KeyPress);
            // 
            // TxtNomCli
            // 
            this.TxtNomCli.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNomCli.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNomCli.Location = new System.Drawing.Point(142, 32);
            this.TxtNomCli.Multiline = true;
            this.TxtNomCli.Name = "TxtNomCli";
            this.TxtNomCli.Size = new System.Drawing.Size(595, 26);
            this.TxtNomCli.TabIndex = 1;
            this.TxtNomCli.Text = "TxtNomCli";
            this.TxtNomCli.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNomCli_KeyPress);
            // 
            // TxtNumRuc
            // 
            this.TxtNumRuc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNumRuc.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNumRuc.Location = new System.Drawing.Point(142, 4);
            this.TxtNumRuc.MaxLength = 11;
            this.TxtNumRuc.Multiline = true;
            this.TxtNumRuc.Name = "TxtNumRuc";
            this.TxtNumRuc.Size = new System.Drawing.Size(146, 26);
            this.TxtNumRuc.TabIndex = 0;
            this.TxtNumRuc.Text = "TxtNumRuc";
            this.TxtNumRuc.TextChanged += new System.EventHandler(this.TxtNumRuc_TextChanged);
            this.TxtNumRuc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumRuc_KeyPress);
            this.TxtNumRuc.Validated += new System.EventHandler(this.TxtNumRuc_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "Direccion";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "Nombre/Razon Social";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 47;
            this.label5.Text = "Nº  R.U.C.";
            // 
            // LblTipDocumento
            // 
            this.LblTipDocumento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblTipDocumento.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTipDocumento.Location = new System.Drawing.Point(273, 40);
            this.LblTipDocumento.Name = "LblTipDocumento";
            this.LblTipDocumento.Size = new System.Drawing.Size(180, 24);
            this.LblTipDocumento.TabIndex = 7;
            this.LblTipDocumento.Text = "LblTipDocumento";
            this.LblTipDocumento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TooNuevo,
            this.ToolCliRapido,
            this.ToolTraeCoti,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.ToolGrabar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(796, 39);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TooNuevo
            // 
            this.TooNuevo.Image = global::SIAC_NET_Ventas.Properties.Resources.Nuevo;
            this.TooNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TooNuevo.Name = "TooNuevo";
            this.TooNuevo.Size = new System.Drawing.Size(110, 36);
            this.TooNuevo.Text = "Nueva Venta";
            this.TooNuevo.Click += new System.EventHandler(this.TooNuevo_Click);
            // 
            // ToolCliRapido
            // 
            this.ToolCliRapido.Image = global::SIAC_NET_Ventas.Properties.Resources.clienteAnonimo;
            this.ToolCliRapido.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolCliRapido.Name = "ToolCliRapido";
            this.ToolCliRapido.Size = new System.Drawing.Size(120, 36);
            this.ToolCliRapido.Text = "Cliente Rapido";
            this.ToolCliRapido.Click += new System.EventHandler(this.ToolCliRapido_Click);
            // 
            // ToolTraeCoti
            // 
            this.ToolTraeCoti.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolTraeCoti.Name = "ToolTraeCoti";
            this.ToolTraeCoti.Size = new System.Drawing.Size(97, 36);
            this.ToolTraeCoti.Text = "Traer Cotizacion";
            this.ToolTraeCoti.Click += new System.EventHandler(this.ToolTraeCoti_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::SIAC_NET_Ventas.Properties.Resources.sonbrero;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(157, 36);
            this.toolStripButton2.Text = "Cambiar Numeracion";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // ToolGrabar
            // 
            this.ToolGrabar.Image = global::SIAC_NET_Ventas.Properties.Resources.Grabar;
            this.ToolGrabar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolGrabar.Name = "ToolGrabar";
            this.ToolGrabar.Size = new System.Drawing.Size(111, 36);
            this.ToolGrabar.Text = "Grabar Venta";
            this.ToolGrabar.Click += new System.EventHandler(this.ToolGrabar_Click);
            // 
            // C1Sizer2
            // 
            this.C1Sizer2.Border.Corners = new C1.Win.C1Sizer.Corners(1, 1, 1, 1);
            this.C1Sizer2.Border.Thickness = new System.Windows.Forms.Padding(1);
            this.C1Sizer2.Controls.Add(this.panel2);
            this.C1Sizer2.Controls.Add(this.panel5);
            this.C1Sizer2.GridDefinition = "95.5555555555556:False:False;\t67.2932330827068:False:False;31.9548872180451:False" +
    ":True;";
            this.C1Sizer2.Location = new System.Drawing.Point(3, 401);
            this.C1Sizer2.Name = "C1Sizer2";
            this.C1Sizer2.Padding = new System.Windows.Forms.Padding(1);
            this.C1Sizer2.Size = new System.Drawing.Size(798, 90);
            this.C1Sizer2.SplitterWidth = 2;
            this.C1Sizer2.TabIndex = 2;
            this.C1Sizer2.Text = "C1Sizer2";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.CboTipDocumento);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.CboMoneda);
            this.panel2.Controls.Add(this.CmdCamcNumeracion);
            this.panel2.Controls.Add(this.CmdTraerCotiza);
            this.panel2.Location = new System.Drawing.Point(2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(537, 86);
            this.panel2.TabIndex = 0;
            // 
            // CboTipDocumento
            // 
            this.CboTipDocumento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTipDocumento.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboTipDocumento.FormattingEnabled = true;
            this.CboTipDocumento.Location = new System.Drawing.Point(102, 46);
            this.CboTipDocumento.Name = "CboTipDocumento";
            this.CboTipDocumento.Size = new System.Drawing.Size(190, 22);
            this.CboTipDocumento.TabIndex = 50;
            this.CboTipDocumento.SelectedIndexChanged += new System.EventHandler(this.CboTipDocumento_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 49;
            this.label9.Text = "Comprobante";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 48;
            this.label8.Text = "Moneda";
            // 
            // CboMoneda
            // 
            this.CboMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMoneda.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboMoneda.FormattingEnabled = true;
            this.CboMoneda.Location = new System.Drawing.Point(102, 19);
            this.CboMoneda.Name = "CboMoneda";
            this.CboMoneda.Size = new System.Drawing.Size(190, 22);
            this.CboMoneda.TabIndex = 4;
            // 
            // CmdCamcNumeracion
            // 
            this.CmdCamcNumeracion.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdCamcNumeracion.Location = new System.Drawing.Point(312, 10);
            this.CmdCamcNumeracion.Name = "CmdCamcNumeracion";
            this.CmdCamcNumeracion.Size = new System.Drawing.Size(111, 50);
            this.CmdCamcNumeracion.TabIndex = 4;
            this.CmdCamcNumeracion.Text = "Cambiar Numeracion";
            this.CmdCamcNumeracion.UseVisualStyleBackColor = true;
            this.CmdCamcNumeracion.Visible = false;
            this.CmdCamcNumeracion.Click += new System.EventHandler(this.CmdCamcNumeracion_Click);
            // 
            // CmdTraerCotiza
            // 
            this.CmdTraerCotiza.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdTraerCotiza.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdTraerCotiza.Location = new System.Drawing.Point(410, 10);
            this.CmdTraerCotiza.Name = "CmdTraerCotiza";
            this.CmdTraerCotiza.Size = new System.Drawing.Size(111, 50);
            this.CmdTraerCotiza.TabIndex = 3;
            this.CmdTraerCotiza.Text = "Traer Cotizacion";
            this.CmdTraerCotiza.UseVisualStyleBackColor = true;
            this.CmdTraerCotiza.Visible = false;
            this.CmdTraerCotiza.Click += new System.EventHandler(this.CmdTraerCotiza_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.LblIGVTasa);
            this.panel5.Controls.Add(this.LblIgv);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.LblTotal);
            this.panel5.Controls.Add(this.CmdEsc);
            this.panel5.Controls.Add(this.LblImpBru);
            this.panel5.Controls.Add(this.Label18);
            this.panel5.Location = new System.Drawing.Point(541, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(255, 86);
            this.panel5.TabIndex = 1;
            // 
            // LblIGVTasa
            // 
            this.LblIGVTasa.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIGVTasa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.LblIGVTasa.Location = new System.Drawing.Point(51, 34);
            this.LblIGVTasa.Name = "LblIGVTasa";
            this.LblIGVTasa.Size = new System.Drawing.Size(41, 18);
            this.LblIGVTasa.TabIndex = 47;
            this.LblIGVTasa.Text = "LblIGVTasa";
            this.LblIGVTasa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblIgv
            // 
            this.LblIgv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblIgv.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIgv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.LblIgv.Location = new System.Drawing.Point(141, 30);
            this.LblIgv.Name = "LblIgv";
            this.LblIgv.Size = new System.Drawing.Size(97, 26);
            this.LblIgv.TabIndex = 43;
            this.LblIgv.Text = "LblIgv";
            this.LblIgv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "Total";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "I.G.V. (          )";
            // 
            // LblTotal
            // 
            this.LblTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotal.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.LblTotal.Location = new System.Drawing.Point(141, 57);
            this.LblTotal.Name = "LblTotal";
            this.LblTotal.Size = new System.Drawing.Size(97, 26);
            this.LblTotal.TabIndex = 44;
            this.LblTotal.Text = "LblTotal";
            this.LblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CmdEsc
            // 
            this.CmdEsc.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CmdEsc.Location = new System.Drawing.Point(341, 9);
            this.CmdEsc.Name = "CmdEsc";
            this.CmdEsc.Size = new System.Drawing.Size(40, 22);
            this.CmdEsc.TabIndex = 42;
            this.CmdEsc.Text = "ESC";
            this.CmdEsc.UseVisualStyleBackColor = true;
            // 
            // LblImpBru
            // 
            this.LblImpBru.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblImpBru.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblImpBru.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.LblImpBru.Location = new System.Drawing.Point(141, 3);
            this.LblImpBru.Name = "LblImpBru";
            this.LblImpBru.Size = new System.Drawing.Size(97, 26);
            this.LblImpBru.TabIndex = 41;
            this.LblImpBru.Text = "LblImpBru";
            this.LblImpBru.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(5, 10);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(69, 13);
            this.Label18.TabIndex = 40;
            this.Label18.Text = "Imp. Bruto";
            // 
            // label7
            // 
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(459, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 24);
            this.label7.TabIndex = 3;
            this.label7.Text = "Nº";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FgDetalle
            // 
            this.FgDetalle.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.FgDetalle.BackColor = System.Drawing.Color.White;
            this.FgDetalle.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgDetalle.ColumnInfo = resources.GetString("FgDetalle.ColumnInfo");
            this.FgDetalle.ForeColor = System.Drawing.Color.Black;
            this.FgDetalle.Location = new System.Drawing.Point(3, 172);
            this.FgDetalle.Name = "FgDetalle";
            this.FgDetalle.Rows.DefaultSize = 18;
            this.FgDetalle.Rows.MaxSize = 50;
            this.FgDetalle.Rows.MinSize = 24;
            this.FgDetalle.Size = new System.Drawing.Size(798, 225);
            this.FgDetalle.StyleInfo = resources.GetString("FgDetalle.StyleInfo");
            this.FgDetalle.TabIndex = 71;
            this.FgDetalle.RowColChange += new System.EventHandler(this.FgDetalle_RowColChange);
            this.FgDetalle.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgDetalle_CellChanged);
            this.FgDetalle.Click += new System.EventHandler(this.FgDetalle_Click);
            // 
            // LblNumDoc
            // 
            this.LblNumDoc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblNumDoc.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumDoc.ForeColor = System.Drawing.Color.Navy;
            this.LblNumDoc.Location = new System.Drawing.Point(614, 39);
            this.LblNumDoc.Name = "LblNumDoc";
            this.LblNumDoc.Size = new System.Drawing.Size(173, 25);
            this.LblNumDoc.TabIndex = 2;
            this.LblNumDoc.Text = "LblNumDoc";
            this.LblNumDoc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.C1Sizer1);
            this.panel1.Location = new System.Drawing.Point(7, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(826, 508);
            this.panel1.TabIndex = 34;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint_1);
            // 
            // C1Sizer1
            // 
            this.C1Sizer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.C1Sizer1.BackColor = System.Drawing.Color.White;
            this.C1Sizer1.Border.Corners = new C1.Win.C1Sizer.Corners(1, 1, 1, 1);
            this.C1Sizer1.Border.Thickness = new System.Windows.Forms.Padding(1);
            this.C1Sizer1.Controls.Add(this.panel4);
            this.C1Sizer1.Controls.Add(this.panel3);
            this.C1Sizer1.Controls.Add(this.C1Sizer2);
            this.C1Sizer1.Controls.Add(this.FgDetalle);
            this.C1Sizer1.GridDefinition = "14.17004048583:False:True;18.4210526315789:False:True;45.5465587044534:False:Fals" +
    "e;18.2186234817814:False:True;\t99.2537313432836:False:False;";
            this.C1Sizer1.Location = new System.Drawing.Point(11, 7);
            this.C1Sizer1.Name = "C1Sizer1";
            this.C1Sizer1.Padding = new System.Windows.Forms.Padding(2);
            this.C1Sizer1.Size = new System.Drawing.Size(804, 494);
            this.C1Sizer1.TabIndex = 25;
            this.C1Sizer1.Text = "C1Sizer1";
            this.C1Sizer1.Click += new System.EventHandler(this.C1Sizer1_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.LblTipDocumento);
            this.panel4.Controls.Add(this.toolStrip1);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.LblNumDoc);
            this.panel4.Controls.Add(this.LblSerDoc);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(798, 70);
            this.panel4.TabIndex = 72;
            // 
            // LblSerDoc
            // 
            this.LblSerDoc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblSerDoc.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSerDoc.ForeColor = System.Drawing.Color.Navy;
            this.LblSerDoc.Location = new System.Drawing.Point(517, 39);
            this.LblSerDoc.Name = "LblSerDoc";
            this.LblSerDoc.Size = new System.Drawing.Size(84, 25);
            this.LblSerDoc.TabIndex = 1;
            this.LblSerDoc.Text = "LblSerDoc";
            this.LblSerDoc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmPuntoVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 564);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmPuntoVenta";
            this.Text = "FrmPuntoVenta";
            this.Load += new System.EventHandler(this.FrmPuntoVenta_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1Sizer2)).EndInit();
            this.C1Sizer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgDetalle)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1Sizer1)).EndInit();
            this.C1Sizer1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label LblTipCam;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LblIdCliente;
        private System.Windows.Forms.TextBox TxtDireccion;
        private System.Windows.Forms.TextBox TxtNomCli;
        private System.Windows.Forms.TextBox TxtNumRuc;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LblTipDocumento;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TooNuevo;
        private System.Windows.Forms.ToolStripButton ToolCliRapido;
        private System.Windows.Forms.ToolStripButton ToolTraeCoti;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolGrabar;
        internal C1.Win.C1Sizer.C1Sizer C1Sizer2;
        internal System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox CboTipDocumento;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox CboMoneda;
        internal System.Windows.Forms.Button CmdCamcNumeracion;
        internal System.Windows.Forms.Button CmdTraerCotiza;
        internal System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label LblIGVTasa;
        internal System.Windows.Forms.Label LblIgv;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label LblTotal;
        internal System.Windows.Forms.Button CmdEsc;
        internal System.Windows.Forms.Label LblImpBru;
        internal System.Windows.Forms.Label Label18;
        private System.Windows.Forms.Label label7;
        internal C1.Win.C1FlexGrid.C1FlexGrid FgDetalle;
        private System.Windows.Forms.Label LblNumDoc;
        private System.Windows.Forms.Panel panel1;
        internal C1.Win.C1Sizer.C1Sizer C1Sizer1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label LblSerDoc;
    }
}