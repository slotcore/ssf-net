namespace SSF_NET_Produccion.Formularios
{
    partial class FrmVerRecetaInsumos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVerRecetaInsumos));
            this.Eo1 = new C1.Win.C1Sizer.C1Sizer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CmdCan = new System.Windows.Forms.Button();
            this.CmdAce = new System.Windows.Forms.Button();
            this.CmdVolver = new System.Windows.Forms.Button();
            this.CmdDelIns = new System.Windows.Forms.Button();
            this.CmdAddIns = new System.Windows.Forms.Button();
            this.FgRec = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtObs = new System.Windows.Forms.TextBox();
            this.TxtCan = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CboUniMed = new System.Windows.Forms.ComboBox();
            this.TxtCodRec = new System.Windows.Forms.TextBox();
            this.TxtDesReceta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Eo1)).BeginInit();
            this.Eo1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgRec)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Eo1
            // 
            this.Eo1.Border.Thickness = new System.Windows.Forms.Padding(1);
            this.Eo1.Controls.Add(this.panel2);
            this.Eo1.Controls.Add(this.FgRec);
            this.Eo1.Controls.Add(this.panel1);
            this.Eo1.GridDefinition = "36.3408521303258:False:True;46.6165413533835:False:False;12.531328320802:False:Tr" +
    "ue;\t1.06044538706257:False:True;95.9703075291622:False:False;1.06044538706257:Fa" +
    "lse:True;";
            this.Eo1.Location = new System.Drawing.Point(1, 12);
            this.Eo1.Name = "Eo1";
            this.Eo1.Size = new System.Drawing.Size(943, 399);
            this.Eo1.TabIndex = 0;
            this.Eo1.Text = "c1Sizer1";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.CmdCan);
            this.panel2.Controls.Add(this.CmdAce);
            this.panel2.Controls.Add(this.CmdVolver);
            this.panel2.Controls.Add(this.CmdDelIns);
            this.panel2.Controls.Add(this.CmdAddIns);
            this.panel2.Location = new System.Drawing.Point(19, 344);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(905, 50);
            this.panel2.TabIndex = 10;
            // 
            // CmdCan
            // 
            this.CmdCan.Image = ((System.Drawing.Image)(resources.GetObject("CmdCan.Image")));
            this.CmdCan.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdCan.Location = new System.Drawing.Point(484, 3);
            this.CmdCan.Name = "CmdCan";
            this.CmdCan.Size = new System.Drawing.Size(97, 40);
            this.CmdCan.TabIndex = 21;
            this.CmdCan.Text = "  Cancelar";
            this.CmdCan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdCan.UseVisualStyleBackColor = true;
            this.CmdCan.Visible = false;
            this.CmdCan.Click += new System.EventHandler(this.CmdCan_Click);
            // 
            // CmdAce
            // 
            this.CmdAce.Image = ((System.Drawing.Image)(resources.GetObject("CmdAce.Image")));
            this.CmdAce.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdAce.Location = new System.Drawing.Point(384, 3);
            this.CmdAce.Name = "CmdAce";
            this.CmdAce.Size = new System.Drawing.Size(97, 40);
            this.CmdAce.TabIndex = 20;
            this.CmdAce.Text = "   Aceptar";
            this.CmdAce.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdAce.UseVisualStyleBackColor = true;
            this.CmdAce.Visible = false;
            this.CmdAce.Click += new System.EventHandler(this.CmdAce_Click);
            // 
            // CmdVolver
            // 
            this.CmdVolver.Image = ((System.Drawing.Image)(resources.GetObject("CmdVolver.Image")));
            this.CmdVolver.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdVolver.Location = new System.Drawing.Point(259, 3);
            this.CmdVolver.Name = "CmdVolver";
            this.CmdVolver.Size = new System.Drawing.Size(97, 40);
            this.CmdVolver.TabIndex = 19;
            this.CmdVolver.Text = "    Volver";
            this.CmdVolver.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdVolver.UseVisualStyleBackColor = true;
            this.CmdVolver.Click += new System.EventHandler(this.CmdVolver_Click);
            // 
            // CmdDelIns
            // 
            this.CmdDelIns.Enabled = false;
            this.CmdDelIns.Location = new System.Drawing.Point(135, 3);
            this.CmdDelIns.Name = "CmdDelIns";
            this.CmdDelIns.Size = new System.Drawing.Size(97, 40);
            this.CmdDelIns.TabIndex = 1;
            this.CmdDelIns.Text = "Eliminar Insumo";
            this.CmdDelIns.UseVisualStyleBackColor = true;
            this.CmdDelIns.Click += new System.EventHandler(this.CmdDelIns_Click);
            // 
            // CmdAddIns
            // 
            this.CmdAddIns.Enabled = false;
            this.CmdAddIns.Location = new System.Drawing.Point(29, 3);
            this.CmdAddIns.Name = "CmdAddIns";
            this.CmdAddIns.Size = new System.Drawing.Size(97, 40);
            this.CmdAddIns.TabIndex = 0;
            this.CmdAddIns.Text = "Agregar Insumo";
            this.CmdAddIns.UseVisualStyleBackColor = true;
            this.CmdAddIns.Click += new System.EventHandler(this.CmdAddIns_Click);
            // 
            // FgRec
            // 
            this.FgRec.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.FgRec.ColumnInfo = "4,1,0,0,0,85,Columns:0{Width:10;}\t";
            this.FgRec.Location = new System.Drawing.Point(19, 154);
            this.FgRec.Name = "FgRec";
            this.FgRec.Rows.DefaultSize = 17;
            this.FgRec.Size = new System.Drawing.Size(905, 186);
            this.FgRec.TabIndex = 9;
            this.FgRec.EnterCell += new System.EventHandler(this.FgRec_EnterCell);
            this.FgRec.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgRec_CellButtonClick);
            this.FgRec.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.FgRec_KeyPressEdit);
            this.FgRec.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.FgRec_CellChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TxtObs);
            this.panel1.Controls.Add(this.TxtCan);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.CboUniMed);
            this.panel1.Controls.Add(this.TxtCodRec);
            this.panel1.Controls.Add(this.TxtDesReceta);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(19, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(905, 145);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 93;
            this.label2.Text = "Observaciones";
            // 
            // TxtObs
            // 
            this.TxtObs.BackColor = System.Drawing.Color.White;
            this.TxtObs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtObs.Enabled = false;
            this.TxtObs.ForeColor = System.Drawing.Color.Black;
            this.TxtObs.Location = new System.Drawing.Point(99, 99);
            this.TxtObs.MaxLength = 10;
            this.TxtObs.Multiline = true;
            this.TxtObs.Name = "TxtObs";
            this.TxtObs.Size = new System.Drawing.Size(748, 41);
            this.TxtObs.TabIndex = 92;
            // 
            // TxtCan
            // 
            this.TxtCan.BackColor = System.Drawing.Color.White;
            this.TxtCan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCan.Enabled = false;
            this.TxtCan.ForeColor = System.Drawing.Color.Black;
            this.TxtCan.Location = new System.Drawing.Point(535, 75);
            this.TxtCan.MaxLength = 4;
            this.TxtCan.Name = "TxtCan";
            this.TxtCan.ReadOnly = true;
            this.TxtCan.Size = new System.Drawing.Size(84, 20);
            this.TxtCan.TabIndex = 91;
            this.TxtCan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(469, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 90;
            this.label1.Text = "Cantidad";
            // 
            // CboUniMed
            // 
            this.CboUniMed.Enabled = false;
            this.CboUniMed.FormattingEnabled = true;
            this.CboUniMed.Location = new System.Drawing.Point(99, 75);
            this.CboUniMed.Name = "CboUniMed";
            this.CboUniMed.Size = new System.Drawing.Size(262, 21);
            this.CboUniMed.TabIndex = 89;
            // 
            // TxtCodRec
            // 
            this.TxtCodRec.BackColor = System.Drawing.Color.White;
            this.TxtCodRec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCodRec.Enabled = false;
            this.TxtCodRec.ForeColor = System.Drawing.Color.Black;
            this.TxtCodRec.Location = new System.Drawing.Point(99, 31);
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
            this.TxtDesReceta.Location = new System.Drawing.Point(99, 53);
            this.TxtDesReceta.MaxLength = 10;
            this.TxtDesReceta.Name = "TxtDesReceta";
            this.TxtDesReceta.Size = new System.Drawing.Size(748, 20);
            this.TxtDesReceta.TabIndex = 88;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 78);
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
            this.label22.Location = new System.Drawing.Point(6, 33);
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
            // FrmVerRecetaInsumos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 422);
            this.Controls.Add(this.Eo1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVerRecetaInsumos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmVerRecetaInsumos";
            this.Activated += new System.EventHandler(this.FrmVerRecetaInsumos_Activated);
            this.Load += new System.EventHandler(this.FrmVerRecetaInsumos_Load);
            this.Resize += new System.EventHandler(this.FrmVerRecetaInsumos_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.Eo1)).EndInit();
            this.Eo1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgRec)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Sizer.C1Sizer Eo1;
        private System.Windows.Forms.Panel panel1;
        private C1.Win.C1FlexGrid.C1FlexGrid FgRec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox CboUniMed;
        private System.Windows.Forms.TextBox TxtCodRec;
        private System.Windows.Forms.TextBox TxtDesReceta;
        private System.Windows.Forms.Button CmdDelIns;
        private System.Windows.Forms.Button CmdAddIns;
        private System.Windows.Forms.TextBox TxtCan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtObs;
        private System.Windows.Forms.Button CmdVolver;
        private System.Windows.Forms.Button CmdCan;
        private System.Windows.Forms.Button CmdAce;
    }
}