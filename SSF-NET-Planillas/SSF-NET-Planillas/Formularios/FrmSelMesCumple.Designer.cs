namespace SSF_NET_Planillas.Formularios
{
    partial class FrmSelMesCumple
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
            this.CmdAce = new System.Windows.Forms.Button();
            this.CmdCan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CboMeses = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CboMeses);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(281, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // CmdAce
            // 
            this.CmdAce.Location = new System.Drawing.Point(58, 73);
            this.CmdAce.Name = "CmdAce";
            this.CmdAce.Size = new System.Drawing.Size(86, 33);
            this.CmdAce.TabIndex = 1;
            this.CmdAce.Text = "Aceptar";
            this.CmdAce.UseVisualStyleBackColor = true;
            this.CmdAce.Click += new System.EventHandler(this.CmdAce_Click);
            // 
            // CmdCan
            // 
            this.CmdCan.Location = new System.Drawing.Point(147, 73);
            this.CmdCan.Name = "CmdCan";
            this.CmdCan.Size = new System.Drawing.Size(86, 33);
            this.CmdCan.TabIndex = 2;
            this.CmdCan.Text = "Cancelar";
            this.CmdCan.UseVisualStyleBackColor = true;
            this.CmdCan.Click += new System.EventHandler(this.CmdCan_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mes";
            // 
            // CboMeses
            // 
            this.CboMeses.FormattingEnabled = true;
            this.CboMeses.Location = new System.Drawing.Point(72, 25);
            this.CboMeses.Name = "CboMeses";
            this.CboMeses.Size = new System.Drawing.Size(193, 21);
            this.CboMeses.TabIndex = 1;
            // 
            // FrmSelMesCumple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 113);
            this.Controls.Add(this.CmdCan);
            this.Controls.Add(this.CmdAce);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelMesCumple";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccione Mes";
            this.Activated += new System.EventHandler(this.FrmSelMesCumple_Activated);
            this.Load += new System.EventHandler(this.FrmSelMesCumple_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CboMeses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CmdAce;
        private System.Windows.Forms.Button CmdCan;
    }
}