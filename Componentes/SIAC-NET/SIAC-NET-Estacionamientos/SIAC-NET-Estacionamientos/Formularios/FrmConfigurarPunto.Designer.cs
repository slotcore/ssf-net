namespace SIAC_NET_Estacionamientos.Formularios
{
    partial class FrmConfigurarPunto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigurarPunto));
            this.CboLocal = new System.Windows.Forms.ComboBox();
            this.CboCajero = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CmdAce = new System.Windows.Forms.Button();
            this.CmdCan = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CboLocal
            // 
            this.CboLocal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboLocal.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboLocal.FormattingEnabled = true;
            this.CboLocal.Location = new System.Drawing.Point(103, 31);
            this.CboLocal.Name = "CboLocal";
            this.CboLocal.Size = new System.Drawing.Size(602, 21);
            this.CboLocal.TabIndex = 5;
            this.CboLocal.SelectedIndexChanged += new System.EventHandler(this.CboLocal_SelectedIndexChanged);
            // 
            // CboCajero
            // 
            this.CboCajero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboCajero.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboCajero.FormattingEnabled = true;
            this.CboCajero.Location = new System.Drawing.Point(103, 54);
            this.CboCajero.Name = "CboCajero";
            this.CboCajero.Size = new System.Drawing.Size(312, 21);
            this.CboCajero.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(13, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Local";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(13, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Cajero";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CboLocal);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.CboCajero);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(723, 112);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "..:: Configurar Punto de Venta ::..";
            // 
            // CmdAce
            // 
            this.CmdAce.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdAce.Image = ((System.Drawing.Image)(resources.GetObject("CmdAce.Image")));
            this.CmdAce.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdAce.Location = new System.Drawing.Point(270, 122);
            this.CmdAce.Name = "CmdAce";
            this.CmdAce.Size = new System.Drawing.Size(96, 40);
            this.CmdAce.TabIndex = 11;
            this.CmdAce.Text = "Aceptar";
            this.CmdAce.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdAce.UseVisualStyleBackColor = true;
            this.CmdAce.Click += new System.EventHandler(this.CmdAce_Click);
            // 
            // CmdCan
            // 
            this.CmdCan.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdCan.Image = ((System.Drawing.Image)(resources.GetObject("CmdCan.Image")));
            this.CmdCan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdCan.Location = new System.Drawing.Point(367, 122);
            this.CmdCan.Name = "CmdCan";
            this.CmdCan.Size = new System.Drawing.Size(96, 40);
            this.CmdCan.TabIndex = 12;
            this.CmdCan.Text = "Cancelar";
            this.CmdCan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdCan.UseVisualStyleBackColor = true;
            this.CmdCan.Click += new System.EventHandler(this.CmdCan_Click);
            // 
            // FrmConfigurarPunto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 166);
            this.Controls.Add(this.CmdAce);
            this.Controls.Add(this.CmdCan);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConfigurarPunto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmConfigurarPunto";
            this.Load += new System.EventHandler(this.FrmConfigurarPunto_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox CboLocal;
        private System.Windows.Forms.ComboBox CboCajero;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CmdAce;
        private System.Windows.Forms.Button CmdCan;
    }
}