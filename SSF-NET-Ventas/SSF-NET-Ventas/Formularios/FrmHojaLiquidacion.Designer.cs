namespace SSF_NET_Ventas.Formularios
{
    partial class FrmHojaLiquidacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHojaLiquidacion));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OptDet = new System.Windows.Forms.RadioButton();
            this.OptRes = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtFchFin = new System.Windows.Forms.DateTimePicker();
            this.TxtFchIni = new System.Windows.Forms.DateTimePicker();
            this.CmdPri = new System.Windows.Forms.Button();
            this.CmdCan = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OptDet);
            this.groupBox1.Controls.Add(this.OptRes);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TxtFchFin);
            this.groupBox1.Controls.Add(this.TxtFchIni);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(21, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 123);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "..:: Datos del Reporte ::..";
            // 
            // OptDet
            // 
            this.OptDet.AutoSize = true;
            this.OptDet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptDet.Location = new System.Drawing.Point(201, 87);
            this.OptDet.Name = "OptDet";
            this.OptDet.Size = new System.Drawing.Size(108, 17);
            this.OptDet.TabIndex = 5;
            this.OptDet.TabStop = true;
            this.OptDet.Text = "Informe Detallado";
            this.OptDet.UseVisualStyleBackColor = true;
            // 
            // OptRes
            // 
            this.OptRes.AutoSize = true;
            this.OptRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptRes.Location = new System.Drawing.Point(61, 87);
            this.OptRes.Name = "OptRes";
            this.OptRes.Size = new System.Drawing.Size(110, 17);
            this.OptRes.TabIndex = 4;
            this.OptRes.TabStop = true;
            this.OptRes.Text = "Informe Resumido";
            this.OptRes.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fecha de Termino";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fecha de Inicio";
            // 
            // TxtFchFin
            // 
            this.TxtFchFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFchFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchFin.Location = new System.Drawing.Point(112, 53);
            this.TxtFchFin.Name = "TxtFchFin";
            this.TxtFchFin.Size = new System.Drawing.Size(92, 20);
            this.TxtFchFin.TabIndex = 1;
            // 
            // TxtFchIni
            // 
            this.TxtFchIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFchIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TxtFchIni.Location = new System.Drawing.Point(112, 29);
            this.TxtFchIni.Name = "TxtFchIni";
            this.TxtFchIni.Size = new System.Drawing.Size(92, 20);
            this.TxtFchIni.TabIndex = 0;
            // 
            // CmdPri
            // 
            this.CmdPri.Image = ((System.Drawing.Image)(resources.GetObject("CmdPri.Image")));
            this.CmdPri.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdPri.Location = new System.Drawing.Point(91, 147);
            this.CmdPri.Name = "CmdPri";
            this.CmdPri.Size = new System.Drawing.Size(109, 42);
            this.CmdPri.TabIndex = 1;
            this.CmdPri.Text = "Imprimir    ";
            this.CmdPri.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdPri.UseVisualStyleBackColor = true;
            this.CmdPri.Click += new System.EventHandler(this.CmdPri_Click);
            // 
            // CmdCan
            // 
            this.CmdCan.Image = ((System.Drawing.Image)(resources.GetObject("CmdCan.Image")));
            this.CmdCan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdCan.Location = new System.Drawing.Point(202, 147);
            this.CmdCan.Name = "CmdCan";
            this.CmdCan.Size = new System.Drawing.Size(109, 42);
            this.CmdCan.TabIndex = 2;
            this.CmdCan.Text = "Cancelar   ";
            this.CmdCan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdCan.UseVisualStyleBackColor = true;
            this.CmdCan.Click += new System.EventHandler(this.CmdCan_Click);
            // 
            // FrmHojaLiquidacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 197);
            this.Controls.Add(this.CmdCan);
            this.Controls.Add(this.CmdPri);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHojaLiquidacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmHojaLiquidacion";
            this.Load += new System.EventHandler(this.FrmHojaLiquidacion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton OptDet;
        private System.Windows.Forms.RadioButton OptRes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker TxtFchFin;
        private System.Windows.Forms.DateTimePicker TxtFchIni;
        private System.Windows.Forms.Button CmdPri;
        private System.Windows.Forms.Button CmdCan;
    }
}