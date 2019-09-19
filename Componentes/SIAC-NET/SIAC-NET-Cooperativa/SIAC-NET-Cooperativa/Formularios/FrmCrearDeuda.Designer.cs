namespace SIAC_NET_Cooperativa.Formularios
{
    partial class FrmCrearDeuda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCrearDeuda));
            this.TxtNomSoc = new System.Windows.Forms.TextBox();
            this.TxtNumDoc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtTipSoc = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CmdCan = new System.Windows.Forms.Button();
            this.CmdAce = new System.Windows.Forms.Button();
            this.FgLista = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.CboPue = new System.Windows.Forms.ComboBox();
            this.CboMesTra = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LblDato = new System.Windows.Forms.Label();
            this.ChkActivar = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgLista)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtNomSoc
            // 
            this.TxtNomSoc.Location = new System.Drawing.Point(112, 60);
            this.TxtNomSoc.Name = "TxtNomSoc";
            this.TxtNomSoc.ReadOnly = true;
            this.TxtNomSoc.Size = new System.Drawing.Size(446, 20);
            this.TxtNomSoc.TabIndex = 1;
            // 
            // TxtNumDoc
            // 
            this.TxtNumDoc.Location = new System.Drawing.Point(112, 37);
            this.TxtNumDoc.Name = "TxtNumDoc";
            this.TxtNumDoc.ReadOnly = true;
            this.TxtNumDoc.Size = new System.Drawing.Size(81, 20);
            this.TxtNumDoc.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Socio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nº Documento";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tipo de Socio";
            // 
            // TxtTipSoc
            // 
            this.TxtTipSoc.Location = new System.Drawing.Point(112, 83);
            this.TxtTipSoc.Name = "TxtTipSoc";
            this.TxtTipSoc.ReadOnly = true;
            this.TxtTipSoc.Size = new System.Drawing.Size(169, 20);
            this.TxtTipSoc.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CmdCan);
            this.groupBox1.Controls.Add(this.CmdAce);
            this.groupBox1.Location = new System.Drawing.Point(13, 340);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 54);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // CmdCan
            // 
            this.CmdCan.Image = ((System.Drawing.Image)(resources.GetObject("CmdCan.Image")));
            this.CmdCan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdCan.Location = new System.Drawing.Point(275, 10);
            this.CmdCan.Name = "CmdCan";
            this.CmdCan.Size = new System.Drawing.Size(106, 39);
            this.CmdCan.TabIndex = 7;
            this.CmdCan.Text = "&Cancelar   ";
            this.CmdCan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdCan.UseVisualStyleBackColor = true;
            this.CmdCan.Click += new System.EventHandler(this.CmdCan_Click);
            // 
            // CmdAce
            // 
            this.CmdAce.Image = ((System.Drawing.Image)(resources.GetObject("CmdAce.Image")));
            this.CmdAce.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdAce.Location = new System.Drawing.Point(163, 10);
            this.CmdAce.Name = "CmdAce";
            this.CmdAce.Size = new System.Drawing.Size(106, 39);
            this.CmdAce.TabIndex = 6;
            this.CmdAce.Text = "&Aceptar   ";
            this.CmdAce.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdAce.UseVisualStyleBackColor = true;
            this.CmdAce.Click += new System.EventHandler(this.CmdAce_Click);
            // 
            // FgLista
            // 
            this.FgLista.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgLista.ColumnInfo = "10,1,0,0,0,85,Columns:";
            this.FgLista.Location = new System.Drawing.Point(14, 135);
            this.FgLista.Name = "FgLista";
            this.FgLista.Rows.DefaultSize = 17;
            this.FgLista.Size = new System.Drawing.Size(544, 207);
            this.FgLista.TabIndex = 5;
            this.FgLista.EnterCell += new System.EventHandler(this.FgLista_EnterCell);
            this.FgLista.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgLista_CellButtonClick);
            this.FgLista.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgLista_CellChanged);
            // 
            // CboPue
            // 
            this.CboPue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboPue.FormattingEnabled = true;
            this.CboPue.Location = new System.Drawing.Point(112, 109);
            this.CboPue.Name = "CboPue";
            this.CboPue.Size = new System.Drawing.Size(124, 21);
            this.CboPue.TabIndex = 3;
            this.CboPue.SelectedValueChanged += new System.EventHandler(this.CboPue_SelectedValueChanged);
            // 
            // CboMesTra
            // 
            this.CboMesTra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMesTra.FormattingEnabled = true;
            this.CboMesTra.Location = new System.Drawing.Point(411, 109);
            this.CboMesTra.Name = "CboMesTra";
            this.CboMesTra.Size = new System.Drawing.Size(147, 21);
            this.CboMesTra.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Nº Puesto";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(360, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Mes";
            // 
            // LblDato
            // 
            this.LblDato.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDato.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.LblDato.Location = new System.Drawing.Point(12, 9);
            this.LblDato.Name = "LblDato";
            this.LblDato.Size = new System.Drawing.Size(546, 19);
            this.LblDato.TabIndex = 14;
            this.LblDato.Text = "label6";
            // 
            // ChkActivar
            // 
            this.ChkActivar.AutoSize = true;
            this.ChkActivar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActivar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActivar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ChkActivar.Location = new System.Drawing.Point(399, 36);
            this.ChkActivar.Name = "ChkActivar";
            this.ChkActivar.Size = new System.Drawing.Size(160, 17);
            this.ChkActivar.TabIndex = 15;
            this.ChkActivar.Text = "Ver Puestos No Activos";
            this.ChkActivar.UseVisualStyleBackColor = true;
            this.ChkActivar.CheckedChanged += new System.EventHandler(this.ChkActivar_CheckedChanged);
            // 
            // FrmCrearDeuda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 395);
            this.Controls.Add(this.ChkActivar);
            this.Controls.Add(this.LblDato);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CboMesTra);
            this.Controls.Add(this.CboPue);
            this.Controls.Add(this.FgLista);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtTipSoc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtNumDoc);
            this.Controls.Add(this.TxtNomSoc);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCrearDeuda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCrearDeuda";
            this.Activated += new System.EventHandler(this.FrmCrearDeuda_Activated);
            this.Load += new System.EventHandler(this.FrmCrearDeuda_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgLista)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtNomSoc;
        private System.Windows.Forms.TextBox TxtNumDoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtTipSoc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CmdCan;
        private System.Windows.Forms.Button CmdAce;
        private C1.Win.C1FlexGrid.C1FlexGrid FgLista;
        private System.Windows.Forms.ComboBox CboPue;
        private System.Windows.Forms.ComboBox CboMesTra;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LblDato;
        private System.Windows.Forms.CheckBox ChkActivar;
    }
}