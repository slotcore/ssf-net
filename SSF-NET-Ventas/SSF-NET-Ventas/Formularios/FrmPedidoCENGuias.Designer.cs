namespace SSF_NET_Ventas.Formularios
{
    partial class FrmPedidoCENGuias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPedidoCENGuias));
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.FgItems = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtPunEnt = new System.Windows.Forms.TextBox();
            this.TxtPunVen = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CmdGen = new System.Windows.Forms.Button();
            this.CmdSal = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CboPunPar = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
            this.c1Sizer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.Controls.Add(this.FgItems);
            this.c1Sizer1.GridDefinition = "97.667638483965:False:False;\t99.1709844559585:False:False;";
            this.c1Sizer1.Location = new System.Drawing.Point(4, 60);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Size = new System.Drawing.Size(965, 343);
            this.c1Sizer1.TabIndex = 0;
            this.c1Sizer1.Text = "c1Sizer1";
            // 
            // FgItems
            // 
            this.FgItems.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgItems.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:10;}\t";
            this.FgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FgItems.Location = new System.Drawing.Point(0, 0);
            this.FgItems.Name = "FgItems";
            this.FgItems.Rows.DefaultSize = 17;
            this.FgItems.Size = new System.Drawing.Size(965, 343);
            this.FgItems.TabIndex = 0;
            this.FgItems.RowColChange += new System.EventHandler(this.FgItems_RowColChange);
            this.FgItems.EnterCell += new System.EventHandler(this.FgItems_EnterCell);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(-2, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Emision de Guias";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtPunEnt);
            this.groupBox1.Controls.Add(this.TxtPunVen);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.CmdGen);
            this.groupBox1.Controls.Add(this.CmdSal);
            this.groupBox1.Location = new System.Drawing.Point(4, 401);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(964, 55);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // TxtPunEnt
            // 
            this.TxtPunEnt.Location = new System.Drawing.Point(97, 32);
            this.TxtPunEnt.Name = "TxtPunEnt";
            this.TxtPunEnt.Size = new System.Drawing.Size(353, 20);
            this.TxtPunEnt.TabIndex = 7;
            // 
            // TxtPunVen
            // 
            this.TxtPunVen.Location = new System.Drawing.Point(97, 8);
            this.TxtPunVen.Name = "TxtPunVen";
            this.TxtPunVen.Size = new System.Drawing.Size(353, 20);
            this.TxtPunVen.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Punto Entrega";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Punto Venta";
            // 
            // CmdGen
            // 
            this.CmdGen.Image = ((System.Drawing.Image)(resources.GetObject("CmdGen.Image")));
            this.CmdGen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdGen.Location = new System.Drawing.Point(639, 10);
            this.CmdGen.Name = "CmdGen";
            this.CmdGen.Size = new System.Drawing.Size(116, 42);
            this.CmdGen.TabIndex = 1;
            this.CmdGen.Text = "Generar Guia  ";
            this.CmdGen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdGen.UseVisualStyleBackColor = true;
            this.CmdGen.Click += new System.EventHandler(this.CmdGen_Click);
            // 
            // CmdSal
            // 
            this.CmdSal.Image = ((System.Drawing.Image)(resources.GetObject("CmdSal.Image")));
            this.CmdSal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdSal.Location = new System.Drawing.Point(758, 10);
            this.CmdSal.Name = "CmdSal";
            this.CmdSal.Size = new System.Drawing.Size(116, 42);
            this.CmdSal.TabIndex = 0;
            this.CmdSal.Text = "Salir         ";
            this.CmdSal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdSal.UseVisualStyleBackColor = true;
            this.CmdSal.Click += new System.EventHandler(this.CmdSal_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Punto de Partida";
            // 
            // CboPunPar
            // 
            this.CboPunPar.BackColor = System.Drawing.Color.White;
            this.CboPunPar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboPunPar.ForeColor = System.Drawing.Color.Black;
            this.CboPunPar.FormattingEnabled = true;
            this.CboPunPar.Location = new System.Drawing.Point(107, 35);
            this.CboPunPar.Name = "CboPunPar";
            this.CboPunPar.Size = new System.Drawing.Size(359, 21);
            this.CboPunPar.TabIndex = 5;
            // 
            // FrmPedidoCENGuias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 458);
            this.Controls.Add(this.CboPunPar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.c1Sizer1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPedidoCENGuias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmPedidoCENGuias";
            this.Load += new System.EventHandler(this.FrmPedidoCENGuias_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Sizer.C1Sizer c1Sizer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CmdGen;
        private System.Windows.Forms.Button CmdSal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CboPunPar;
        private System.Windows.Forms.TextBox TxtPunEnt;
        private System.Windows.Forms.TextBox TxtPunVen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private C1.Win.C1FlexGrid.C1FlexGrid FgItems;
    }
}