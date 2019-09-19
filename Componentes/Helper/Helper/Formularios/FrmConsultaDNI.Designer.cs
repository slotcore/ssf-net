namespace Helper.Formularios
{
    partial class FrmConsultaDNI
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtClave = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtNumDni = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PicImagen = new System.Windows.Forms.PictureBox();
            this.CmdCon = new System.Windows.Forms.Button();
            this.CmdCle = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CmdEnv = new System.Windows.Forms.Button();
            this.CmdSal = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtNom = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtApeMat = new System.Windows.Forms.TextBox();
            this.TxtApePat = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicImagen)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtClave);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxtNumDni);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.PicImagen);
            this.groupBox1.Controls.Add(this.CmdCon);
            this.groupBox1.Controls.Add(this.CmdCle);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox1.Location = new System.Drawing.Point(5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(498, 82);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "..:: DATOS A CONSULTAR ::..";
            // 
            // TxtClave
            // 
            this.TxtClave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.TxtClave.Location = new System.Drawing.Point(121, 53);
            this.TxtClave.MaxLength = 4;
            this.TxtClave.Name = "TxtClave";
            this.TxtClave.Size = new System.Drawing.Size(64, 21);
            this.TxtClave.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(14, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Codigo Capcha";
            // 
            // TxtNumDni
            // 
            this.TxtNumDni.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNumDni.Location = new System.Drawing.Point(121, 27);
            this.TxtNumDni.MaxLength = 11;
            this.TxtNumDni.Name = "TxtNumDni";
            this.TxtNumDni.Size = new System.Drawing.Size(126, 21);
            this.TxtNumDni.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(14, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Nº D.N.I.";
            // 
            // PicImagen
            // 
            this.PicImagen.Location = new System.Drawing.Point(290, 19);
            this.PicImagen.Name = "PicImagen";
            this.PicImagen.Size = new System.Drawing.Size(101, 54);
            this.PicImagen.TabIndex = 21;
            this.PicImagen.TabStop = false;
            // 
            // CmdCon
            // 
            this.CmdCon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.CmdCon.ForeColor = System.Drawing.Color.Black;
            this.CmdCon.Location = new System.Drawing.Point(398, 18);
            this.CmdCon.Name = "CmdCon";
            this.CmdCon.Size = new System.Drawing.Size(94, 28);
            this.CmdCon.TabIndex = 2;
            this.CmdCon.Text = "Consultar";
            this.CmdCon.UseVisualStyleBackColor = true;
            this.CmdCon.Click += new System.EventHandler(this.CmdCon_Click);
            // 
            // CmdCle
            // 
            this.CmdCle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.CmdCle.ForeColor = System.Drawing.Color.Black;
            this.CmdCle.Location = new System.Drawing.Point(398, 46);
            this.CmdCle.Name = "CmdCle";
            this.CmdCle.Size = new System.Drawing.Size(94, 28);
            this.CmdCle.TabIndex = 3;
            this.CmdCle.Text = "Limpiar";
            this.CmdCle.UseVisualStyleBackColor = true;
            this.CmdCle.Click += new System.EventHandler(this.CmdCle_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CmdEnv);
            this.groupBox2.Controls.Add(this.CmdSal);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.TxtNom);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.TxtApeMat);
            this.groupBox2.Controls.Add(this.TxtApePat);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox2.Location = new System.Drawing.Point(5, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(498, 114);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "..:: DATOS DEL CIUDADANO ::..";
            // 
            // CmdEnv
            // 
            this.CmdEnv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.CmdEnv.ForeColor = System.Drawing.Color.Black;
            this.CmdEnv.Location = new System.Drawing.Point(398, 50);
            this.CmdEnv.Name = "CmdEnv";
            this.CmdEnv.Size = new System.Drawing.Size(94, 28);
            this.CmdEnv.TabIndex = 42;
            this.CmdEnv.Text = "Enviar";
            this.CmdEnv.UseVisualStyleBackColor = true;
            this.CmdEnv.Click += new System.EventHandler(this.CmdEnv_Click);
            // 
            // CmdSal
            // 
            this.CmdSal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.CmdSal.ForeColor = System.Drawing.Color.Black;
            this.CmdSal.Location = new System.Drawing.Point(398, 78);
            this.CmdSal.Name = "CmdSal";
            this.CmdSal.Size = new System.Drawing.Size(94, 28);
            this.CmdSal.TabIndex = 43;
            this.CmdSal.Text = "Salir";
            this.CmdSal.UseVisualStyleBackColor = true;
            this.CmdSal.Click += new System.EventHandler(this.CmdSal_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(12, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "Nombres";
            // 
            // TxtNom
            // 
            this.TxtNom.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.TxtNom.Location = new System.Drawing.Point(148, 84);
            this.TxtNom.Name = "TxtNom";
            this.TxtNom.ReadOnly = true;
            this.TxtNom.Size = new System.Drawing.Size(157, 21);
            this.TxtNom.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(12, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Apellido Materno";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(12, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Apellido Paterno";
            // 
            // TxtApeMat
            // 
            this.TxtApeMat.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.TxtApeMat.Location = new System.Drawing.Point(148, 59);
            this.TxtApeMat.Name = "TxtApeMat";
            this.TxtApeMat.ReadOnly = true;
            this.TxtApeMat.Size = new System.Drawing.Size(157, 21);
            this.TxtApeMat.TabIndex = 29;
            // 
            // TxtApePat
            // 
            this.TxtApePat.Location = new System.Drawing.Point(148, 33);
            this.TxtApePat.Name = "TxtApePat";
            this.TxtApePat.ReadOnly = true;
            this.TxtApePat.Size = new System.Drawing.Size(157, 23);
            this.TxtApePat.TabIndex = 28;
            // 
            // FrmConsultaDNI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 210);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConsultaDNI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmConsultaDNI";
            this.Load += new System.EventHandler(this.FrmConsultaDNI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicImagen)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TxtClave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtNumDni;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PicImagen;
        private System.Windows.Forms.Button CmdCon;
        private System.Windows.Forms.Button CmdCle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button CmdEnv;
        private System.Windows.Forms.Button CmdSal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtNom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtApeMat;
        private System.Windows.Forms.TextBox TxtApePat;
    }
}