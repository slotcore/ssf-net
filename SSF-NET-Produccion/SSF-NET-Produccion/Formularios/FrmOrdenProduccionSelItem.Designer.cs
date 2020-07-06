namespace SSF_NET_Planillas.Formularios
{
    partial class FrmOrdenProduccionSelItem
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
            this.CboMeses = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CmdAce = new System.Windows.Forms.Button();
            this.CmdCan = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CboMeses);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(530, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // CboMeses
            // 
            this.CboMeses.FormattingEnabled = true;
            this.CboMeses.Location = new System.Drawing.Point(110, 33);
            this.CboMeses.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CboMeses.Name = "CboMeses";
            this.CboMeses.Size = new System.Drawing.Size(412, 24);
            this.CboMeses.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Producto";
            // 
            // CmdAce
            // 
            this.CmdAce.Location = new System.Drawing.Point(149, 89);
            this.CmdAce.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CmdAce.Name = "CmdAce";
            this.CmdAce.Size = new System.Drawing.Size(115, 41);
            this.CmdAce.TabIndex = 1;
            this.CmdAce.Text = "Aceptar";
            this.CmdAce.UseVisualStyleBackColor = true;
            this.CmdAce.Click += new System.EventHandler(this.CmdAce_Click);
            // 
            // CmdCan
            // 
            this.CmdCan.Location = new System.Drawing.Point(268, 89);
            this.CmdCan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CmdCan.Name = "CmdCan";
            this.CmdCan.Size = new System.Drawing.Size(115, 41);
            this.CmdCan.TabIndex = 2;
            this.CmdCan.Text = "Cancelar";
            this.CmdCan.UseVisualStyleBackColor = true;
            this.CmdCan.Click += new System.EventHandler(this.CmdCan_Click);
            // 
            // FrmOrdenProduccionSelItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 139);
            this.Controls.Add(this.CmdCan);
            this.Controls.Add(this.CmdAce);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOrdenProduccionSelItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccionar Producto";
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