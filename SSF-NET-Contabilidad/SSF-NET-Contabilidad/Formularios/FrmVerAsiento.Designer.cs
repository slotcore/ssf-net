namespace SSF_NET_Contabilidad.Formularios
{
    partial class FrmVerAsiento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVerAsiento));
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.FgItems = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.TxtLibro = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtNumReg = new System.Windows.Forms.TextBox();
            this.CmdSal = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtTot1 = new System.Windows.Forms.TextBox();
            this.TxtTot2 = new System.Windows.Forms.TextBox();
            this.TxtTot3 = new System.Windows.Forms.TextBox();
            this.TxtTot4 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
            this.c1Sizer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.Border.Thickness = new System.Windows.Forms.Padding(1);
            this.c1Sizer1.Controls.Add(this.panel1);
            this.c1Sizer1.Controls.Add(this.FgItems);
            this.c1Sizer1.GridDefinition = "23.5059760956175:False:False;74.5019920318725:False:False;\t99.4673768308922:False" +
    ":False;";
            this.c1Sizer1.Location = new System.Drawing.Point(2, 2);
            this.c1Sizer1.Margin = new System.Windows.Forms.Padding(1);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Padding = new System.Windows.Forms.Padding(1);
            this.c1Sizer1.Size = new System.Drawing.Size(751, 251);
            this.c1Sizer1.SplitterWidth = 1;
            this.c1Sizer1.TabIndex = 12;
            this.c1Sizer1.Text = "c1Sizer1";
            // 
            // FgItems
            // 
            this.FgItems.BackColor = System.Drawing.Color.White;
            this.FgItems.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgItems.ColumnInfo = "3,1,0,0,0,85,Columns:0{Width:9;}\t1{Width:217;Caption:\"Unidad Medida\";Style:\"TextA" +
    "lign:CenterCenter;\";StyleFixed:\"TextAlign:CenterCenter;\";}\t2{Width:30;}\t";
            this.FgItems.ForeColor = System.Drawing.Color.Black;
            this.FgItems.Location = new System.Drawing.Point(2, 62);
            this.FgItems.Name = "FgItems";
            this.FgItems.Rows.DefaultSize = 17;
            this.FgItems.Size = new System.Drawing.Size(747, 187);
            this.FgItems.StyleInfo = resources.GetString("FgItems.StyleInfo");
            this.FgItems.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CmdSal);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TxtNumReg);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.TxtLibro);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(747, 59);
            this.panel1.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(9, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 116;
            this.label10.Text = "Libro";
            // 
            // TxtLibro
            // 
            this.TxtLibro.Enabled = false;
            this.TxtLibro.Location = new System.Drawing.Point(92, 6);
            this.TxtLibro.MaxLength = 20;
            this.TxtLibro.Name = "TxtLibro";
            this.TxtLibro.Size = new System.Drawing.Size(314, 20);
            this.TxtLibro.TabIndex = 115;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(9, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 118;
            this.label1.Text = "Nº Registro";
            // 
            // TxtNumReg
            // 
            this.TxtNumReg.Enabled = false;
            this.TxtNumReg.Location = new System.Drawing.Point(92, 30);
            this.TxtNumReg.MaxLength = 20;
            this.TxtNumReg.Name = "TxtNumReg";
            this.TxtNumReg.Size = new System.Drawing.Size(108, 20);
            this.TxtNumReg.TabIndex = 117;
            // 
            // CmdSal
            // 
            this.CmdSal.Image = ((System.Drawing.Image)(resources.GetObject("CmdSal.Image")));
            this.CmdSal.Location = new System.Drawing.Point(681, 6);
            this.CmdSal.Name = "CmdSal";
            this.CmdSal.Size = new System.Drawing.Size(49, 45);
            this.CmdSal.TabIndex = 119;
            this.CmdSal.UseVisualStyleBackColor = true;
            this.CmdSal.Click += new System.EventHandler(this.CmdSal_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(340, 261);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 14);
            this.label2.TabIndex = 120;
            this.label2.Text = "TOTAL==>";
            // 
            // TxtTot1
            // 
            this.TxtTot1.Enabled = false;
            this.TxtTot1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTot1.ForeColor = System.Drawing.Color.Navy;
            this.TxtTot1.Location = new System.Drawing.Point(464, 258);
            this.TxtTot1.MaxLength = 20;
            this.TxtTot1.Name = "TxtTot1";
            this.TxtTot1.Size = new System.Drawing.Size(66, 20);
            this.TxtTot1.TabIndex = 119;
            this.TxtTot1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtTot2
            // 
            this.TxtTot2.Enabled = false;
            this.TxtTot2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTot2.ForeColor = System.Drawing.Color.Navy;
            this.TxtTot2.Location = new System.Drawing.Point(531, 258);
            this.TxtTot2.MaxLength = 20;
            this.TxtTot2.Name = "TxtTot2";
            this.TxtTot2.Size = new System.Drawing.Size(66, 20);
            this.TxtTot2.TabIndex = 121;
            this.TxtTot2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtTot3
            // 
            this.TxtTot3.Enabled = false;
            this.TxtTot3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTot3.ForeColor = System.Drawing.Color.Navy;
            this.TxtTot3.Location = new System.Drawing.Point(598, 258);
            this.TxtTot3.MaxLength = 20;
            this.TxtTot3.Name = "TxtTot3";
            this.TxtTot3.Size = new System.Drawing.Size(66, 20);
            this.TxtTot3.TabIndex = 122;
            this.TxtTot3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtTot4
            // 
            this.TxtTot4.Enabled = false;
            this.TxtTot4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTot4.ForeColor = System.Drawing.Color.Navy;
            this.TxtTot4.Location = new System.Drawing.Point(665, 258);
            this.TxtTot4.MaxLength = 20;
            this.TxtTot4.Name = "TxtTot4";
            this.TxtTot4.Size = new System.Drawing.Size(66, 20);
            this.TxtTot4.TabIndex = 123;
            this.TxtTot4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmVerAsiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 284);
            this.Controls.Add(this.TxtTot4);
            this.Controls.Add(this.TxtTot3);
            this.Controls.Add(this.TxtTot2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtTot1);
            this.Controls.Add(this.c1Sizer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVerAsiento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmVerAsiento";
            this.Load += new System.EventHandler(this.FrmVerAsiento_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Sizer.C1Sizer c1Sizer1;
        private System.Windows.Forms.Panel panel1;
        private C1.Win.C1FlexGrid.C1FlexGrid FgItems;
        private System.Windows.Forms.Button CmdSal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtNumReg;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TxtLibro;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtTot1;
        private System.Windows.Forms.TextBox TxtTot2;
        private System.Windows.Forms.TextBox TxtTot3;
        private System.Windows.Forms.TextBox TxtTot4;
    }
}