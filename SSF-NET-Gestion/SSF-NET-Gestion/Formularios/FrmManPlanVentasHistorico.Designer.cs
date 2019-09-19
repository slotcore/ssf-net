namespace SSF_NET_Gestion.Formularios
{
    partial class FrmManPlanVentasHistorico
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManPlanVentasHistorico));
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CmdAddPla = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.FgDato = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CmdAddDevStd = new System.Windows.Forms.Button();
            this.CmdAddProMen = new System.Windows.Forms.Button();
            this.TxtPor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.CmdSalir = new System.Windows.Forms.Button();
            this.TxtUniMed = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtCodPro = new System.Windows.Forms.TextBox();
            this.CmdAddItem = new System.Windows.Forms.Button();
            this.CmdDelItem = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtDes = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.FgItems = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
            this.c1Sizer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgDato)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).BeginInit();
            this.SuspendLayout();
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.Controls.Add(this.panel2);
            this.c1Sizer1.Controls.Add(this.FgDato);
            this.c1Sizer1.Controls.Add(this.panel1);
            this.c1Sizer1.Controls.Add(this.FgItems);
            this.c1Sizer1.ForeColor = System.Drawing.Color.Black;
            this.c1Sizer1.GridDefinition = resources.GetString("c1Sizer1.GridDefinition");
            this.c1Sizer1.Location = new System.Drawing.Point(1, 0);
            this.c1Sizer1.Margin = new System.Windows.Forms.Padding(1);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Padding = new System.Windows.Forms.Padding(2);
            this.c1Sizer1.Size = new System.Drawing.Size(1103, 347);
            this.c1Sizer1.SplitterWidth = 1;
            this.c1Sizer1.TabIndex = 11;
            this.c1Sizer1.Text = "c1Sizer1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.CmdAddPla);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(4, 225);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1095, 30);
            this.panel2.TabIndex = 15;
            // 
            // CmdAddPla
            // 
            this.CmdAddPla.BackColor = System.Drawing.Color.White;
            this.CmdAddPla.ForeColor = System.Drawing.Color.Black;
            this.CmdAddPla.Location = new System.Drawing.Point(757, 3);
            this.CmdAddPla.Name = "CmdAddPla";
            this.CmdAddPla.Size = new System.Drawing.Size(132, 24);
            this.CmdAddPla.TabIndex = 39;
            this.CmdAddPla.Text = "Agregar al Plan";
            this.CmdAddPla.UseVisualStyleBackColor = false;
            this.CmdAddPla.Click += new System.EventHandler(this.CmdAddPla_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(7, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 14);
            this.label2.TabIndex = 38;
            this.label2.Text = "..:: Datos a Procesar ::..";
            // 
            // FgDato
            // 
            this.FgDato.BackColor = System.Drawing.Color.White;
            this.FgDato.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgDato.ColumnInfo = "3,1,0,0,0,85,Columns:0{Width:9;}\t1{Width:217;Caption:\"Unidad Medida\";Style:\"TextA" +
    "lign:CenterCenter;\";StyleFixed:\"TextAlign:CenterCenter;\";}\t2{Width:30;}\t";
            this.FgDato.ForeColor = System.Drawing.Color.Black;
            this.FgDato.Location = new System.Drawing.Point(4, 256);
            this.FgDato.Name = "FgDato";
            this.FgDato.Rows.DefaultSize = 17;
            this.FgDato.Size = new System.Drawing.Size(1097, 89);
            this.FgDato.StyleInfo = resources.GetString("FgDato.StyleInfo");
            this.FgDato.TabIndex = 14;
            this.FgDato.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.FgDato_KeyPressEdit);
            this.FgDato.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgDato_CellChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.CmdAddDevStd);
            this.panel1.Controls.Add(this.CmdAddProMen);
            this.panel1.Controls.Add(this.TxtPor);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.CmdSalir);
            this.panel1.Controls.Add(this.TxtUniMed);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.TxtCodPro);
            this.panel1.Controls.Add(this.CmdAddItem);
            this.panel1.Controls.Add(this.CmdDelItem);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.TxtDes);
            this.panel1.Controls.Add(this.label9);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(4, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1095, 72);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // CmdAddDevStd
            // 
            this.CmdAddDevStd.BackColor = System.Drawing.Color.White;
            this.CmdAddDevStd.ForeColor = System.Drawing.Color.Black;
            this.CmdAddDevStd.Location = new System.Drawing.Point(806, 2);
            this.CmdAddDevStd.Name = "CmdAddDevStd";
            this.CmdAddDevStd.Size = new System.Drawing.Size(132, 24);
            this.CmdAddDevStd.TabIndex = 47;
            this.CmdAddDevStd.Text = "Agregar Desviacion Std.";
            this.CmdAddDevStd.UseVisualStyleBackColor = false;
            this.CmdAddDevStd.Click += new System.EventHandler(this.CmdAddDevStd_Click);
            // 
            // CmdAddProMen
            // 
            this.CmdAddProMen.BackColor = System.Drawing.Color.White;
            this.CmdAddProMen.ForeColor = System.Drawing.Color.Black;
            this.CmdAddProMen.Location = new System.Drawing.Point(806, 25);
            this.CmdAddProMen.Name = "CmdAddProMen";
            this.CmdAddProMen.Size = new System.Drawing.Size(132, 24);
            this.CmdAddProMen.TabIndex = 48;
            this.CmdAddProMen.Text = "Agregar Promedio Mensual";
            this.CmdAddProMen.UseVisualStyleBackColor = false;
            this.CmdAddProMen.Click += new System.EventHandler(this.CmdAddProMen_Click);
            // 
            // TxtPor
            // 
            this.TxtPor.BackColor = System.Drawing.Color.White;
            this.TxtPor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtPor.ForeColor = System.Drawing.Color.Black;
            this.TxtPor.Location = new System.Drawing.Point(96, 49);
            this.TxtPor.Multiline = true;
            this.TxtPor.Name = "TxtPor";
            this.TxtPor.Size = new System.Drawing.Size(74, 21);
            this.TxtPor.TabIndex = 46;
            this.TxtPor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPor_KeyPress);
            this.TxtPor.Validated += new System.EventHandler(this.TxtPor_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(1, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "% Incremento";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(668, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 24);
            this.button1.TabIndex = 44;
            this.button1.Text = "Agregar Total";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CmdSalir
            // 
            this.CmdSalir.Image = ((System.Drawing.Image)(resources.GetObject("CmdSalir.Image")));
            this.CmdSalir.Location = new System.Drawing.Point(957, 11);
            this.CmdSalir.Name = "CmdSalir";
            this.CmdSalir.Size = new System.Drawing.Size(55, 51);
            this.CmdSalir.TabIndex = 43;
            this.CmdSalir.UseVisualStyleBackColor = true;
            this.CmdSalir.Click += new System.EventHandler(this.CmdSalir_Click);
            // 
            // TxtUniMed
            // 
            this.TxtUniMed.BackColor = System.Drawing.Color.White;
            this.TxtUniMed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUniMed.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtUniMed.Enabled = false;
            this.TxtUniMed.ForeColor = System.Drawing.Color.Black;
            this.TxtUniMed.Location = new System.Drawing.Point(564, 3);
            this.TxtUniMed.Multiline = true;
            this.TxtUniMed.Name = "TxtUniMed";
            this.TxtUniMed.Size = new System.Drawing.Size(68, 21);
            this.TxtUniMed.TabIndex = 42;
            this.TxtUniMed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtUniMed_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(469, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "Unidad Medida";
            // 
            // TxtCodPro
            // 
            this.TxtCodPro.BackColor = System.Drawing.Color.White;
            this.TxtCodPro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCodPro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtCodPro.Enabled = false;
            this.TxtCodPro.ForeColor = System.Drawing.Color.Black;
            this.TxtCodPro.Location = new System.Drawing.Point(96, 3);
            this.TxtCodPro.Multiline = true;
            this.TxtCodPro.Name = "TxtCodPro";
            this.TxtCodPro.Size = new System.Drawing.Size(154, 21);
            this.TxtCodPro.TabIndex = 40;
            this.TxtCodPro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCodPro_KeyPress);
            // 
            // CmdAddItem
            // 
            this.CmdAddItem.BackColor = System.Drawing.Color.White;
            this.CmdAddItem.ForeColor = System.Drawing.Color.Black;
            this.CmdAddItem.Location = new System.Drawing.Point(668, 2);
            this.CmdAddItem.Name = "CmdAddItem";
            this.CmdAddItem.Size = new System.Drawing.Size(132, 24);
            this.CmdAddItem.TabIndex = 14;
            this.CmdAddItem.Text = "Agregar Año Actual";
            this.CmdAddItem.UseVisualStyleBackColor = false;
            this.CmdAddItem.Click += new System.EventHandler(this.CmdAddItem_Click);
            // 
            // CmdDelItem
            // 
            this.CmdDelItem.BackColor = System.Drawing.Color.White;
            this.CmdDelItem.ForeColor = System.Drawing.Color.Black;
            this.CmdDelItem.Location = new System.Drawing.Point(668, 25);
            this.CmdDelItem.Name = "CmdDelItem";
            this.CmdDelItem.Size = new System.Drawing.Size(132, 24);
            this.CmdDelItem.TabIndex = 15;
            this.CmdDelItem.Text = "Agregar Promedio Anual";
            this.CmdDelItem.UseVisualStyleBackColor = false;
            this.CmdDelItem.Click += new System.EventHandler(this.CmdDelItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(1, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Codigo";
            // 
            // TxtDes
            // 
            this.TxtDes.BackColor = System.Drawing.Color.White;
            this.TxtDes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDes.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtDes.Enabled = false;
            this.TxtDes.ForeColor = System.Drawing.Color.Black;
            this.TxtDes.Location = new System.Drawing.Point(96, 26);
            this.TxtDes.Multiline = true;
            this.TxtDes.Name = "TxtDes";
            this.TxtDes.Size = new System.Drawing.Size(536, 21);
            this.TxtDes.TabIndex = 12;
            this.TxtDes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDes_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(1, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 37;
            this.label9.Text = "Descripcion";
            // 
            // FgItems
            // 
            this.FgItems.BackColor = System.Drawing.Color.White;
            this.FgItems.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgItems.ColumnInfo = "3,1,0,0,0,85,Columns:0{Width:9;}\t1{Width:217;Caption:\"Unidad Medida\";Style:\"TextA" +
    "lign:CenterCenter;\";StyleFixed:\"TextAlign:CenterCenter;\";}\t2{Width:30;}\t";
            this.FgItems.ForeColor = System.Drawing.Color.Black;
            this.FgItems.Location = new System.Drawing.Point(4, 75);
            this.FgItems.Name = "FgItems";
            this.FgItems.Rows.DefaultSize = 17;
            this.FgItems.Size = new System.Drawing.Size(1095, 149);
            this.FgItems.StyleInfo = resources.GetString("FgItems.StyleInfo");
            this.FgItems.TabIndex = 13;
            this.FgItems.Click += new System.EventHandler(this.FgItems_Click);
            // 
            // FrmManPlanVentasHistorico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1103, 347);
            this.Controls.Add(this.c1Sizer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmManPlanVentasHistorico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmManPlanVentasHistorico";
            this.Activated += new System.EventHandler(this.FrmManPlanVentasHistorico_Activated);
            this.Load += new System.EventHandler(this.FrmManPlanVentasHistorico_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgDato)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Sizer.C1Sizer c1Sizer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TxtUniMed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtCodPro;
        private System.Windows.Forms.Button CmdAddItem;
        private System.Windows.Forms.Button CmdDelItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtDes;
        private System.Windows.Forms.Label label9;
        private C1.Win.C1FlexGrid.C1FlexGrid FgItems;
        private System.Windows.Forms.Button CmdSalir;
        private C1.Win.C1FlexGrid.C1FlexGrid FgDato;
        private System.Windows.Forms.TextBox TxtPor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button CmdAddPla;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CmdAddDevStd;
        private System.Windows.Forms.Button CmdAddProMen;

    }
}