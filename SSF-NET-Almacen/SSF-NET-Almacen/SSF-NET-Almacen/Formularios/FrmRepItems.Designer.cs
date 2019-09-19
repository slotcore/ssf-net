namespace SSF_NET_Almacen.Formularios
{
    partial class FrmRepItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRepItems));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CmdSalir = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.Opt1 = new System.Windows.Forms.RadioButton();
            this.Opt2 = new System.Windows.Forms.RadioButton();
            this.Opt3 = new System.Windows.Forms.RadioButton();
            this.Opt4 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.CboTipExis = new System.Windows.Forms.ComboBox();
            this.OptAct = new System.Windows.Forms.RadioButton();
            this.OptNoAct = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Opt4);
            this.groupBox1.Controls.Add(this.Opt3);
            this.groupBox1.Controls.Add(this.Opt2);
            this.groupBox1.Controls.Add(this.Opt1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 139);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "[ Opcones de Impresion]";
            // 
            // CmdSalir
            // 
            this.CmdSalir.Image = ((System.Drawing.Image)(resources.GetObject("CmdSalir.Image")));
            this.CmdSalir.Location = new System.Drawing.Point(193, 206);
            this.CmdSalir.Name = "CmdSalir";
            this.CmdSalir.Size = new System.Drawing.Size(125, 39);
            this.CmdSalir.TabIndex = 16;
            this.CmdSalir.Text = "&Cancelar";
            this.CmdSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CmdSalir.UseVisualStyleBackColor = true;
            this.CmdSalir.Click += new System.EventHandler(this.CmdSalir_Click);
            // 
            // Button1
            // 
            this.Button1.Image = ((System.Drawing.Image)(resources.GetObject("Button1.Image")));
            this.Button1.Location = new System.Drawing.Point(66, 206);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(125, 39);
            this.Button1.TabIndex = 15;
            this.Button1.Text = "&Imprimir";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Opt1
            // 
            this.Opt1.AutoSize = true;
            this.Opt1.Location = new System.Drawing.Point(28, 29);
            this.Opt1.Name = "Opt1";
            this.Opt1.Size = new System.Drawing.Size(90, 17);
            this.Opt1.TabIndex = 3;
            this.Opt1.TabStop = true;
            this.Opt1.Text = "Lista de Items";
            this.Opt1.UseVisualStyleBackColor = true;
            // 
            // Opt2
            // 
            this.Opt2.AutoSize = true;
            this.Opt2.Location = new System.Drawing.Point(28, 52);
            this.Opt2.Name = "Opt2";
            this.Opt2.Size = new System.Drawing.Size(117, 17);
            this.Opt2.TabIndex = 4;
            this.Opt2.TabStop = true;
            this.Opt2.Text = "Toma de Inventario";
            this.Opt2.UseVisualStyleBackColor = true;
            // 
            // Opt3
            // 
            this.Opt3.AutoSize = true;
            this.Opt3.Location = new System.Drawing.Point(28, 75);
            this.Opt3.Name = "Opt3";
            this.Opt3.Size = new System.Drawing.Size(138, 17);
            this.Opt3.TabIndex = 5;
            this.Opt3.TabStop = true;
            this.Opt3.Text = "Items con Stock Minimo";
            this.Opt3.UseVisualStyleBackColor = true;
            // 
            // Opt4
            // 
            this.Opt4.AutoSize = true;
            this.Opt4.Location = new System.Drawing.Point(28, 98);
            this.Opt4.Name = "Opt4";
            this.Opt4.Size = new System.Drawing.Size(141, 17);
            this.Opt4.TabIndex = 6;
            this.Opt4.TabStop = true;
            this.Opt4.Text = "Items con Stock Maximo";
            this.Opt4.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Tipo Existencia";
            // 
            // CboTipExis
            // 
            this.CboTipExis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTipExis.FormattingEnabled = true;
            this.CboTipExis.Location = new System.Drawing.Point(118, 8);
            this.CboTipExis.Name = "CboTipExis";
            this.CboTipExis.Size = new System.Drawing.Size(254, 21);
            this.CboTipExis.TabIndex = 0;
            // 
            // OptAct
            // 
            this.OptAct.AutoSize = true;
            this.OptAct.Location = new System.Drawing.Point(119, 39);
            this.OptAct.Name = "OptAct";
            this.OptAct.Size = new System.Drawing.Size(55, 17);
            this.OptAct.TabIndex = 1;
            this.OptAct.TabStop = true;
            this.OptAct.Text = "Activo";
            this.OptAct.UseVisualStyleBackColor = true;
            // 
            // OptNoAct
            // 
            this.OptNoAct.AutoSize = true;
            this.OptNoAct.Location = new System.Drawing.Point(196, 39);
            this.OptNoAct.Name = "OptNoAct";
            this.OptNoAct.Size = new System.Drawing.Size(72, 17);
            this.OptNoAct.TabIndex = 2;
            this.OptNoAct.TabStop = true;
            this.OptNoAct.Text = "No Activo";
            this.OptNoAct.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Estado Item";
            // 
            // FrmRepItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 251);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OptNoAct);
            this.Controls.Add(this.OptAct);
            this.Controls.Add(this.CboTipExis);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CmdSalir);
            this.Controls.Add(this.Button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRepItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmRepItems";
            this.Load += new System.EventHandler(this.FrmRepItems_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.Button CmdSalir;
        internal System.Windows.Forms.Button Button1;
        private System.Windows.Forms.RadioButton Opt4;
        private System.Windows.Forms.RadioButton Opt3;
        private System.Windows.Forms.RadioButton Opt2;
        private System.Windows.Forms.RadioButton Opt1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CboTipExis;
        private System.Windows.Forms.RadioButton OptAct;
        private System.Windows.Forms.RadioButton OptNoAct;
        private System.Windows.Forms.Label label2;
    }
}