namespace SSF_NET_Produccion.Formularios
{
    partial class FrmVerEstacionalidad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVerEstacionalidad));
            this.FgItems = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.CmdVolver = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TxtDesUniMed = new System.Windows.Forms.TextBox();
            this.TxtCodRec = new System.Windows.Forms.TextBox();
            this.TxtDesReceta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // FgItems
            // 
            this.FgItems.BackColor = System.Drawing.Color.White;
            this.FgItems.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgItems.ColumnInfo = "3,1,0,0,0,85,Columns:0{Width:9;}\t1{Width:217;Caption:\"Unidad Medida\";Style:\"TextA" +
    "lign:CenterCenter;\";StyleFixed:\"TextAlign:CenterCenter;\";}\t2{Width:30;}\t";
            this.FgItems.ForeColor = System.Drawing.Color.Black;
            this.FgItems.Location = new System.Drawing.Point(3, 82);
            this.FgItems.Name = "FgItems";
            this.FgItems.Rows.DefaultSize = 17;
            this.FgItems.Size = new System.Drawing.Size(758, 105);
            this.FgItems.StyleInfo = resources.GetString("FgItems.StyleInfo");
            this.FgItems.TabIndex = 78;
            // 
            // CmdVolver
            // 
            this.CmdVolver.Image = ((System.Drawing.Image)(resources.GetObject("CmdVolver.Image")));
            this.CmdVolver.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdVolver.Location = new System.Drawing.Point(643, 203);
            this.CmdVolver.Name = "CmdVolver";
            this.CmdVolver.Size = new System.Drawing.Size(97, 40);
            this.CmdVolver.TabIndex = 79;
            this.CmdVolver.Text = "    Volver";
            this.CmdVolver.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdVolver.UseVisualStyleBackColor = true;
            this.CmdVolver.Click += new System.EventHandler(this.CmdVolver_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.TxtDesUniMed);
            this.panel1.Controls.Add(this.TxtCodRec);
            this.panel1.Controls.Add(this.TxtDesReceta);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(758, 77);
            this.panel1.TabIndex = 80;
            // 
            // TxtDesUniMed
            // 
            this.TxtDesUniMed.BackColor = System.Drawing.Color.White;
            this.TxtDesUniMed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDesUniMed.Enabled = false;
            this.TxtDesUniMed.ForeColor = System.Drawing.Color.Black;
            this.TxtDesUniMed.Location = new System.Drawing.Point(437, 31);
            this.TxtDesUniMed.MaxLength = 4;
            this.TxtDesUniMed.Name = "TxtDesUniMed";
            this.TxtDesUniMed.ReadOnly = true;
            this.TxtDesUniMed.Size = new System.Drawing.Size(250, 20);
            this.TxtDesUniMed.TabIndex = 89;
            // 
            // TxtCodRec
            // 
            this.TxtCodRec.BackColor = System.Drawing.Color.White;
            this.TxtCodRec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCodRec.Enabled = false;
            this.TxtCodRec.ForeColor = System.Drawing.Color.Black;
            this.TxtCodRec.Location = new System.Drawing.Point(90, 31);
            this.TxtCodRec.MaxLength = 4;
            this.TxtCodRec.Name = "TxtCodRec";
            this.TxtCodRec.ReadOnly = true;
            this.TxtCodRec.Size = new System.Drawing.Size(142, 20);
            this.TxtCodRec.TabIndex = 87;
            // 
            // TxtDesReceta
            // 
            this.TxtDesReceta.BackColor = System.Drawing.Color.White;
            this.TxtDesReceta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDesReceta.Enabled = false;
            this.TxtDesReceta.ForeColor = System.Drawing.Color.Black;
            this.TxtDesReceta.Location = new System.Drawing.Point(90, 53);
            this.TxtDesReceta.MaxLength = 10;
            this.TxtDesReceta.Name = "TxtDesReceta";
            this.TxtDesReceta.Size = new System.Drawing.Size(597, 20);
            this.TxtDesReceta.TabIndex = 88;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(335, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 86;
            this.label3.Text = "Unidad Medida";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(6, 34);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(40, 13);
            this.label22.TabIndex = 85;
            this.label22.Text = "Codigo";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label12.Location = new System.Drawing.Point(3, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(184, 13);
            this.label12.TabIndex = 84;
            this.label12.Text = "..:: DATOS DE LA RECETA ::..";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(6, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 83;
            this.label6.Text = "Descripcion";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(3, 189);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(335, 67);
            this.panel2.TabIndex = 81;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(46, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 89;
            this.label1.Text = "Escaces";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(46, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 88;
            this.label7.Text = "Abundancia";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(46, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 87;
            this.label8.Text = "Regular";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Red;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(8, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 15);
            this.label4.TabIndex = 86;
            this.label4.Text = "aa";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 15);
            this.label2.TabIndex = 85;
            this.label2.Text = "Codigo";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Yellow;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(8, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 15);
            this.label5.TabIndex = 83;
            this.label5.Text = "aa";
            // 
            // FrmVerEstacionalidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 257);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CmdVolver);
            this.Controls.Add(this.FgItems);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVerEstacionalidad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmVerEstacionalidad";
            this.Load += new System.EventHandler(this.FrmVerEstacionalidad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid FgItems;
        private System.Windows.Forms.Button CmdVolver;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TxtCodRec;
        private System.Windows.Forms.TextBox TxtDesReceta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtDesUniMed;
    }
}