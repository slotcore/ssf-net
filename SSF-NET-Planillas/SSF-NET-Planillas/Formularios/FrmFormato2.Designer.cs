namespace SSF_NET_Planillas.Formularios
{
    partial class FrmFormato2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFormato2));
            this.TxtFchFin = new System.Windows.Forms.DateTimePicker();
            this.TxtFchIni = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CmdSalir = new System.Windows.Forms.Button();
            this.CmdImp = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtPor = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TxtFchFin
            // 
            this.TxtFchFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchFin.Location = new System.Drawing.Point(105, 39);
            this.TxtFchFin.Name = "TxtFchFin";
            this.TxtFchFin.Size = new System.Drawing.Size(96, 20);
            this.TxtFchFin.TabIndex = 1;
            this.TxtFchFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtFchFin_KeyPress);
            // 
            // TxtFchIni
            // 
            this.TxtFchIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchIni.Location = new System.Drawing.Point(105, 16);
            this.TxtFchIni.Name = "TxtFchIni";
            this.TxtFchIni.Size = new System.Drawing.Size(96, 20);
            this.TxtFchIni.TabIndex = 0;
            this.TxtFchIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtFchIni_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(24, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 84;
            this.label4.Text = "Fch. Termino";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(24, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 85;
            this.label1.Text = "Fch. Inicio";
            // 
            // CmdSalir
            // 
            this.CmdSalir.Image = ((System.Drawing.Image)(resources.GetObject("CmdSalir.Image")));
            this.CmdSalir.Location = new System.Drawing.Point(170, 94);
            this.CmdSalir.Name = "CmdSalir";
            this.CmdSalir.Size = new System.Drawing.Size(125, 39);
            this.CmdSalir.TabIndex = 87;
            this.CmdSalir.Text = "&Cancelar";
            this.CmdSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CmdSalir.UseVisualStyleBackColor = true;
            this.CmdSalir.Click += new System.EventHandler(this.CmdSalir_Click);
            // 
            // CmdImp
            // 
            this.CmdImp.Image = ((System.Drawing.Image)(resources.GetObject("CmdImp.Image")));
            this.CmdImp.Location = new System.Drawing.Point(43, 94);
            this.CmdImp.Name = "CmdImp";
            this.CmdImp.Size = new System.Drawing.Size(125, 39);
            this.CmdImp.TabIndex = 86;
            this.CmdImp.Text = "&Imprimir";
            this.CmdImp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CmdImp.UseVisualStyleBackColor = true;
            this.CmdImp.Click += new System.EventHandler(this.CmdImp_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(24, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 88;
            this.label2.Text = "Fch. Inicio";
            // 
            // TxtPor
            // 
            this.TxtPor.Location = new System.Drawing.Point(105, 64);
            this.TxtPor.MaxLength = 4;
            this.TxtPor.Name = "TxtPor";
            this.TxtPor.Size = new System.Drawing.Size(52, 20);
            this.TxtPor.TabIndex = 2;
            this.TxtPor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtPor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPor_KeyPress);
            // 
            // FrmFormato2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 143);
            this.Controls.Add(this.TxtPor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CmdSalir);
            this.Controls.Add(this.CmdImp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TxtFchFin);
            this.Controls.Add(this.TxtFchIni);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFormato2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmFormato2";
            this.Load += new System.EventHandler(this.FrmFormato2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker TxtFchFin;
        private System.Windows.Forms.DateTimePicker TxtFchIni;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button CmdSalir;
        internal System.Windows.Forms.Button CmdImp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtPor;
    }
}