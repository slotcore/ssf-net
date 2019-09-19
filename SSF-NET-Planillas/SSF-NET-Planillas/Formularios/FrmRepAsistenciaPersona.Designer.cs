namespace SSF_NET_Planillas.Formularios
{
    partial class FrmRepAsistenciaPersona
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRepAsistenciaPersona));
            this.CboMesIni = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CboEmp = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CmdSalir = new System.Windows.Forms.Button();
            this.CmdImp = new System.Windows.Forms.Button();
            this.CboMesFin = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CboAnoTra = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtApeNom = new System.Windows.Forms.TextBox();
            this.CmdBusPer = new System.Windows.Forms.Button();
            this.lblcodpersona = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CboMesIni
            // 
            this.CboMesIni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMesIni.FormattingEnabled = true;
            this.CboMesIni.Location = new System.Drawing.Point(99, 59);
            this.CboMesIni.Name = "CboMesIni";
            this.CboMesIni.Size = new System.Drawing.Size(128, 21);
            this.CboMesIni.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Mes Inicio";
            // 
            // CboEmp
            // 
            this.CboEmp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboEmp.FormattingEnabled = true;
            this.CboEmp.Location = new System.Drawing.Point(99, 12);
            this.CboEmp.Name = "CboEmp";
            this.CboEmp.Size = new System.Drawing.Size(363, 21);
            this.CboEmp.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Empresa";
            // 
            // CmdSalir
            // 
            this.CmdSalir.Image = ((System.Drawing.Image)(resources.GetObject("CmdSalir.Image")));
            this.CmdSalir.Location = new System.Drawing.Point(238, 111);
            this.CmdSalir.Name = "CmdSalir";
            this.CmdSalir.Size = new System.Drawing.Size(125, 39);
            this.CmdSalir.TabIndex = 6;
            this.CmdSalir.Text = "&Cancelar";
            this.CmdSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CmdSalir.UseVisualStyleBackColor = true;
            this.CmdSalir.Click += new System.EventHandler(this.CmdSalir_Click);
            // 
            // CmdImp
            // 
            this.CmdImp.Image = ((System.Drawing.Image)(resources.GetObject("CmdImp.Image")));
            this.CmdImp.Location = new System.Drawing.Point(111, 111);
            this.CmdImp.Name = "CmdImp";
            this.CmdImp.Size = new System.Drawing.Size(125, 39);
            this.CmdImp.TabIndex = 5;
            this.CmdImp.Text = "&Imprimir";
            this.CmdImp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CmdImp.UseVisualStyleBackColor = true;
            this.CmdImp.Click += new System.EventHandler(this.CmdImp_Click);
            // 
            // CboMesFin
            // 
            this.CboMesFin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMesFin.FormattingEnabled = true;
            this.CboMesFin.Location = new System.Drawing.Point(334, 59);
            this.CboMesFin.Name = "CboMesFin";
            this.CboMesFin.Size = new System.Drawing.Size(128, 21);
            this.CboMesFin.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(258, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Mes Final";
            // 
            // CboAnoTra
            // 
            this.CboAnoTra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboAnoTra.FormattingEnabled = true;
            this.CboAnoTra.Location = new System.Drawing.Point(99, 36);
            this.CboAnoTra.Name = "CboAnoTra";
            this.CboAnoTra.Size = new System.Drawing.Size(128, 21);
            this.CboAnoTra.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "Año Trabajo";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 44;
            this.label5.Text = "Empleado";
            // 
            // TxtApeNom
            // 
            this.TxtApeNom.Location = new System.Drawing.Point(99, 83);
            this.TxtApeNom.Name = "TxtApeNom";
            this.TxtApeNom.ReadOnly = true;
            this.TxtApeNom.Size = new System.Drawing.Size(363, 20);
            this.TxtApeNom.TabIndex = 45;
            // 
            // CmdBusPer
            // 
            this.CmdBusPer.Image = ((System.Drawing.Image)(resources.GetObject("CmdBusPer.Image")));
            this.CmdBusPer.Location = new System.Drawing.Point(434, 84);
            this.CmdBusPer.Name = "CmdBusPer";
            this.CmdBusPer.Size = new System.Drawing.Size(27, 18);
            this.CmdBusPer.TabIndex = 46;
            this.CmdBusPer.UseVisualStyleBackColor = true;
            this.CmdBusPer.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblcodpersona
            // 
            this.lblcodpersona.AutoSize = true;
            this.lblcodpersona.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcodpersona.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblcodpersona.Location = new System.Drawing.Point(386, 110);
            this.lblcodpersona.Name = "lblcodpersona";
            this.lblcodpersona.Size = new System.Drawing.Size(86, 13);
            this.lblcodpersona.TabIndex = 47;
            this.lblcodpersona.Text = "lblcodpersona";
            this.lblcodpersona.Visible = false;
            // 
            // FrmRepAsistenciaPersona
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 158);
            this.Controls.Add(this.lblcodpersona);
            this.Controls.Add(this.CmdBusPer);
            this.Controls.Add(this.TxtApeNom);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CboAnoTra);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CboMesFin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CboMesIni);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CboEmp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CmdSalir);
            this.Controls.Add(this.CmdImp);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRepAsistenciaPersona";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmRepAsistenciaPersona";
            this.Load += new System.EventHandler(this.FrmRepAsistenciaPersona_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CboMesIni;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CboEmp;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button CmdSalir;
        internal System.Windows.Forms.Button CmdImp;
        private System.Windows.Forms.ComboBox CboMesFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CboAnoTra;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtApeNom;
        private System.Windows.Forms.Button CmdBusPer;
        private System.Windows.Forms.Label lblcodpersona;
    }
}