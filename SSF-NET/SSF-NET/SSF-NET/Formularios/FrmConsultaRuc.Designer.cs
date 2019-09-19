namespace SSF_NET.Formularios
{
    partial class FrmConsultaRuc
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
            this.PicImagen = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtNUmRuc = new System.Windows.Forms.TextBox();
            this.TxtClave = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CmdCon = new System.Windows.Forms.Button();
            this.CmdCle = new System.Windows.Forms.Button();
            this.TxtNom = new System.Windows.Forms.TextBox();
            this.TxtDir = new System.Windows.Forms.TextBox();
            this.TxtActEco = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtConCon = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtFchIni = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtTipCon = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtEst = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.PicImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // PicImagen
            // 
            this.PicImagen.Location = new System.Drawing.Point(434, 12);
            this.PicImagen.Name = "PicImagen";
            this.PicImagen.Size = new System.Drawing.Size(94, 48);
            this.PicImagen.TabIndex = 0;
            this.PicImagen.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nº R.U.C.";
            // 
            // TxtNUmRuc
            // 
            this.TxtNUmRuc.Location = new System.Drawing.Point(99, 33);
            this.TxtNUmRuc.Name = "TxtNUmRuc";
            this.TxtNUmRuc.Size = new System.Drawing.Size(126, 20);
            this.TxtNUmRuc.TabIndex = 2;
            // 
            // TxtClave
            // 
            this.TxtClave.Location = new System.Drawing.Point(99, 59);
            this.TxtClave.Name = "TxtClave";
            this.TxtClave.Size = new System.Drawing.Size(64, 20);
            this.TxtClave.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Codigo Capcha";
            // 
            // CmdCon
            // 
            this.CmdCon.Location = new System.Drawing.Point(434, 61);
            this.CmdCon.Name = "CmdCon";
            this.CmdCon.Size = new System.Drawing.Size(94, 28);
            this.CmdCon.TabIndex = 5;
            this.CmdCon.Text = "Consultar";
            this.CmdCon.UseVisualStyleBackColor = true;
            this.CmdCon.Click += new System.EventHandler(this.CmdCon_Click);
            // 
            // CmdCle
            // 
            this.CmdCle.Location = new System.Drawing.Point(434, 88);
            this.CmdCle.Name = "CmdCle";
            this.CmdCle.Size = new System.Drawing.Size(94, 28);
            this.CmdCle.TabIndex = 6;
            this.CmdCle.Text = "Limpiar";
            this.CmdCle.UseVisualStyleBackColor = true;
            this.CmdCle.Click += new System.EventHandler(this.CmdCle_Click);
            // 
            // TxtNom
            // 
            this.TxtNom.Location = new System.Drawing.Point(141, 113);
            this.TxtNom.Name = "TxtNom";
            this.TxtNom.Size = new System.Drawing.Size(259, 20);
            this.TxtNom.TabIndex = 7;
            // 
            // TxtDir
            // 
            this.TxtDir.Location = new System.Drawing.Point(141, 139);
            this.TxtDir.Name = "TxtDir";
            this.TxtDir.Size = new System.Drawing.Size(259, 20);
            this.TxtDir.TabIndex = 8;
            // 
            // TxtActEco
            // 
            this.TxtActEco.Location = new System.Drawing.Point(141, 212);
            this.TxtActEco.Name = "TxtActEco";
            this.TxtActEco.Size = new System.Drawing.Size(259, 20);
            this.TxtActEco.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Razon Social";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Direccion";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Actividad Economica";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Condicion Contribuyente";
            // 
            // TxtConCon
            // 
            this.TxtConCon.Location = new System.Drawing.Point(141, 238);
            this.TxtConCon.Name = "TxtConCon";
            this.TxtConCon.Size = new System.Drawing.Size(259, 20);
            this.TxtConCon.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Fch. Inicio Actividades";
            // 
            // TxtFchIni
            // 
            this.TxtFchIni.Location = new System.Drawing.Point(141, 162);
            this.TxtFchIni.Name = "TxtFchIni";
            this.TxtFchIni.Size = new System.Drawing.Size(259, 20);
            this.TxtFchIni.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 191);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Tipo Controbuyente";
            // 
            // TxtTipCon
            // 
            this.TxtTipCon.Location = new System.Drawing.Point(141, 188);
            this.TxtTipCon.Name = "TxtTipCon";
            this.TxtTipCon.Size = new System.Drawing.Size(259, 20);
            this.TxtTipCon.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 267);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Estado";
            // 
            // TxtEst
            // 
            this.TxtEst.Location = new System.Drawing.Point(141, 264);
            this.TxtEst.Name = "TxtEst";
            this.TxtEst.Size = new System.Drawing.Size(259, 20);
            this.TxtEst.TabIndex = 19;
            // 
            // FrmConsultaRuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 356);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.TxtEst);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TxtTipCon);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TxtFchIni);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TxtConCon);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtActEco);
            this.Controls.Add(this.TxtDir);
            this.Controls.Add(this.TxtNom);
            this.Controls.Add(this.CmdCle);
            this.Controls.Add(this.CmdCon);
            this.Controls.Add(this.TxtClave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtNUmRuc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PicImagen);
            this.Name = "FrmConsultaRuc";
            this.Text = "FrmConsultaRuc";
            this.Load += new System.EventHandler(this.FrmConsultaRuc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PicImagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PicImagen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtNUmRuc;
        private System.Windows.Forms.TextBox TxtClave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CmdCon;
        private System.Windows.Forms.Button CmdCle;
        private System.Windows.Forms.TextBox TxtNom;
        private System.Windows.Forms.TextBox TxtDir;
        private System.Windows.Forms.TextBox TxtActEco;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtConCon;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtFchIni;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtTipCon;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TxtEst;
    }
}