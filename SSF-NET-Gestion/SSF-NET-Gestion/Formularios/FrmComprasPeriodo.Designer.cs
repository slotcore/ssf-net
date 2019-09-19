namespace SSF_NET_Gestion.Formularios
{
    partial class FrmComprasPeriodo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmComprasPeriodo));
            this.FgUni = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.FgImp = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.TxtUniMed = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtCodPro = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtDes = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CmdSalir = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.FgCanReq = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.FgUni)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgImp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgCanReq)).BeginInit();
            this.SuspendLayout();
            // 
            // FgUni
            // 
            this.FgUni.BackColor = System.Drawing.Color.White;
            this.FgUni.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgUni.ColumnInfo = "3,1,0,0,0,85,Columns:0{Width:9;}\t1{Width:217;Caption:\"Unidad Medida\";Style:\"TextA" +
    "lign:CenterCenter;\";StyleFixed:\"TextAlign:CenterCenter;\";}\t2{Width:30;}\t";
            this.FgUni.ForeColor = System.Drawing.Color.Black;
            this.FgUni.Location = new System.Drawing.Point(3, 161);
            this.FgUni.Name = "FgUni";
            this.FgUni.Rows.DefaultSize = 17;
            this.FgUni.Size = new System.Drawing.Size(903, 61);
            this.FgUni.StyleInfo = resources.GetString("FgUni.StyleInfo");
            this.FgUni.TabIndex = 14;
            // 
            // FgImp
            // 
            this.FgImp.BackColor = System.Drawing.Color.White;
            this.FgImp.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgImp.ColumnInfo = "3,1,0,0,0,85,Columns:0{Width:9;}\t1{Width:217;Caption:\"Unidad Medida\";Style:\"TextA" +
    "lign:CenterCenter;\";StyleFixed:\"TextAlign:CenterCenter;\";}\t2{Width:30;}\t";
            this.FgImp.ForeColor = System.Drawing.Color.Black;
            this.FgImp.Location = new System.Drawing.Point(3, 246);
            this.FgImp.Name = "FgImp";
            this.FgImp.Rows.DefaultSize = 17;
            this.FgImp.Size = new System.Drawing.Size(903, 61);
            this.FgImp.StyleInfo = resources.GetString("FgImp.StyleInfo");
            this.FgImp.TabIndex = 15;
            // 
            // TxtUniMed
            // 
            this.TxtUniMed.BackColor = System.Drawing.Color.White;
            this.TxtUniMed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUniMed.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtUniMed.Enabled = false;
            this.TxtUniMed.ForeColor = System.Drawing.Color.Black;
            this.TxtUniMed.Location = new System.Drawing.Point(569, 6);
            this.TxtUniMed.Multiline = true;
            this.TxtUniMed.Name = "TxtUniMed";
            this.TxtUniMed.Size = new System.Drawing.Size(68, 21);
            this.TxtUniMed.TabIndex = 48;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(474, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "Unidad Medida";
            // 
            // TxtCodPro
            // 
            this.TxtCodPro.BackColor = System.Drawing.Color.White;
            this.TxtCodPro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCodPro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtCodPro.Enabled = false;
            this.TxtCodPro.ForeColor = System.Drawing.Color.Black;
            this.TxtCodPro.Location = new System.Drawing.Point(101, 6);
            this.TxtCodPro.Multiline = true;
            this.TxtCodPro.Name = "TxtCodPro";
            this.TxtCodPro.Size = new System.Drawing.Size(154, 21);
            this.TxtCodPro.TabIndex = 46;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Codigo";
            // 
            // TxtDes
            // 
            this.TxtDes.BackColor = System.Drawing.Color.White;
            this.TxtDes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDes.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtDes.Enabled = false;
            this.TxtDes.ForeColor = System.Drawing.Color.Black;
            this.TxtDes.Location = new System.Drawing.Point(101, 29);
            this.TxtDes.Multiline = true;
            this.TxtDes.Name = "TxtDes";
            this.TxtDes.Size = new System.Drawing.Size(536, 21);
            this.TxtDes.TabIndex = 43;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(6, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 44;
            this.label9.Text = "Descripcion";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(3, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 14);
            this.label1.TabIndex = 49;
            this.label1.Text = "..:: Compras en Unidades ::..";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(3, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 14);
            this.label2.TabIndex = 50;
            this.label2.Text = "..:: Compras en Importes ::..";
            // 
            // CmdSalir
            // 
            this.CmdSalir.Image = ((System.Drawing.Image)(resources.GetObject("CmdSalir.Image")));
            this.CmdSalir.Location = new System.Drawing.Point(846, 4);
            this.CmdSalir.Name = "CmdSalir";
            this.CmdSalir.Size = new System.Drawing.Size(59, 49);
            this.CmdSalir.TabIndex = 51;
            this.CmdSalir.UseVisualStyleBackColor = true;
            this.CmdSalir.Click += new System.EventHandler(this.CmdSalir_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label5.Location = new System.Drawing.Point(3, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(176, 14);
            this.label5.TabIndex = 53;
            this.label5.Text = "..:: Cantdad Requerida ::..";
            // 
            // FgCanReq
            // 
            this.FgCanReq.BackColor = System.Drawing.Color.White;
            this.FgCanReq.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgCanReq.ColumnInfo = "3,1,0,0,0,85,Columns:0{Width:9;}\t1{Width:217;Caption:\"Unidad Medida\";Style:\"TextA" +
    "lign:CenterCenter;\";StyleFixed:\"TextAlign:CenterCenter;\";}\t2{Width:30;}\t";
            this.FgCanReq.ForeColor = System.Drawing.Color.Black;
            this.FgCanReq.Location = new System.Drawing.Point(3, 77);
            this.FgCanReq.Name = "FgCanReq";
            this.FgCanReq.Rows.DefaultSize = 17;
            this.FgCanReq.Size = new System.Drawing.Size(903, 61);
            this.FgCanReq.StyleInfo = resources.GetString("FgCanReq.StyleInfo");
            this.FgCanReq.TabIndex = 52;
            // 
            // FrmComprasPeriodo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 310);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.FgCanReq);
            this.Controls.Add(this.CmdSalir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtUniMed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TxtCodPro);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtDes);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.FgImp);
            this.Controls.Add(this.FgUni);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmComprasPeriodo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmComprasPeriodo";
            this.Activated += new System.EventHandler(this.FrmComprasPeriodo_Activated);
            this.Load += new System.EventHandler(this.FrmComprasPeriodo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FgUni)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgImp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgCanReq)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid FgUni;
        private C1.Win.C1FlexGrid.C1FlexGrid FgImp;
        private System.Windows.Forms.TextBox TxtUniMed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtCodPro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtDes;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CmdSalir;
        private System.Windows.Forms.Label label5;
        private C1.Win.C1FlexGrid.C1FlexGrid FgCanReq;
    }
}