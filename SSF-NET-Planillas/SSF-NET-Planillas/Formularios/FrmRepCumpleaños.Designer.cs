namespace SSF_NET_Planillas.Formularios
{
    partial class FrmRepCumpleaños
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRepCumpleaños));
            this.CmdSalir = new System.Windows.Forms.Button();
            this.CmdImp = new System.Windows.Forms.Button();
            this.CboMes = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CboEmp = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ChkDes = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // CmdSalir
            // 
            this.CmdSalir.Image = ((System.Drawing.Image)(resources.GetObject("CmdSalir.Image")));
            this.CmdSalir.Location = new System.Drawing.Point(238, 105);
            this.CmdSalir.Name = "CmdSalir";
            this.CmdSalir.Size = new System.Drawing.Size(125, 39);
            this.CmdSalir.TabIndex = 7;
            this.CmdSalir.Text = "&Cancelar";
            this.CmdSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CmdSalir.UseVisualStyleBackColor = true;
            this.CmdSalir.Click += new System.EventHandler(this.CmdSalir_Click);
            // 
            // CmdImp
            // 
            this.CmdImp.Image = ((System.Drawing.Image)(resources.GetObject("CmdImp.Image")));
            this.CmdImp.Location = new System.Drawing.Point(111, 105);
            this.CmdImp.Name = "CmdImp";
            this.CmdImp.Size = new System.Drawing.Size(125, 39);
            this.CmdImp.TabIndex = 6;
            this.CmdImp.Text = "&Imprimir";
            this.CmdImp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CmdImp.UseVisualStyleBackColor = true;
            this.CmdImp.Click += new System.EventHandler(this.CmdImp_Click);
            // 
            // CboMes
            // 
            this.CboMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMes.FormattingEnabled = true;
            this.CboMes.Location = new System.Drawing.Point(99, 43);
            this.CboMes.Name = "CboMes";
            this.CboMes.Size = new System.Drawing.Size(147, 21);
            this.CboMes.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Mes";
            // 
            // CboEmp
            // 
            this.CboEmp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboEmp.FormattingEnabled = true;
            this.CboEmp.Location = new System.Drawing.Point(99, 16);
            this.CboEmp.Name = "CboEmp";
            this.CboEmp.Size = new System.Drawing.Size(363, 21);
            this.CboEmp.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Empresa";
            // 
            // ChkDes
            // 
            this.ChkDes.AutoSize = true;
            this.ChkDes.Location = new System.Drawing.Point(99, 75);
            this.ChkDes.Name = "ChkDes";
            this.ChkDes.Size = new System.Drawing.Size(153, 17);
            this.ChkDes.TabIndex = 35;
            this.ChkDes.Text = "Incluir Personal Destacado";
            this.ChkDes.UseVisualStyleBackColor = true;
            // 
            // FrmRepCumpleaños
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 153);
            this.Controls.Add(this.ChkDes);
            this.Controls.Add(this.CboEmp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CboMes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CmdSalir);
            this.Controls.Add(this.CmdImp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRepCumpleaños";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmRepCumpleaños";
            this.Load += new System.EventHandler(this.FrmRepCumpleaños_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button CmdSalir;
        internal System.Windows.Forms.Button CmdImp;
        private System.Windows.Forms.ComboBox CboMes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CboEmp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ChkDes;
    }
}