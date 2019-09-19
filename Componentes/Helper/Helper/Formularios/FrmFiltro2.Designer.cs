namespace Helper.Formularios
{
    partial class FrmFiltro2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFiltro2));
            this.C1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.C1Sizer2 = new C1.Win.C1Sizer.C1Sizer();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.LblOrden = new System.Windows.Forms.Label();
            this.CmdEsc = new System.Windows.Forms.Button();
            this.LblNumReg = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Button2 = new System.Windows.Forms.Button();
            this.CmdAceptar = new System.Windows.Forms.Button();
            this.DgLista = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            ((System.ComponentModel.ISupportInitialize)(this.C1Sizer1)).BeginInit();
            this.C1Sizer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1Sizer2)).BeginInit();
            this.C1Sizer2.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).BeginInit();
            this.SuspendLayout();
            // 
            // C1Sizer1
            // 
            this.C1Sizer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.C1Sizer1.Border.Corners = new C1.Win.C1Sizer.Corners(1, 1, 1, 1);
            this.C1Sizer1.Border.Thickness = new System.Windows.Forms.Padding(1);
            this.C1Sizer1.Controls.Add(this.C1Sizer2);
            this.C1Sizer1.Controls.Add(this.DgLista);
            this.C1Sizer1.GridDefinition = "88.2882882882883:False:False;9.45945945945946:False:True;\t99.2395437262357:False:" +
    "False;";
            this.C1Sizer1.Location = new System.Drawing.Point(0, 0);
            this.C1Sizer1.Name = "C1Sizer1";
            this.C1Sizer1.Padding = new System.Windows.Forms.Padding(2);
            this.C1Sizer1.Size = new System.Drawing.Size(789, 444);
            this.C1Sizer1.TabIndex = 7;
            this.C1Sizer1.Text = "C1Sizer1";
            // 
            // C1Sizer2
            // 
            this.C1Sizer2.Border.Corners = new C1.Win.C1Sizer.Corners(1, 1, 1, 1);
            this.C1Sizer2.Border.Thickness = new System.Windows.Forms.Padding(1);
            this.C1Sizer2.Controls.Add(this.Panel2);
            this.C1Sizer2.Controls.Add(this.Panel1);
            this.C1Sizer2.GridDefinition = "90.4761904761905:False:False;\t67.1775223499361:False:False;32.0561941251596:False" +
    ":True;";
            this.C1Sizer2.Location = new System.Drawing.Point(3, 399);
            this.C1Sizer2.Name = "C1Sizer2";
            this.C1Sizer2.Padding = new System.Windows.Forms.Padding(1);
            this.C1Sizer2.Size = new System.Drawing.Size(783, 42);
            this.C1Sizer2.SplitterWidth = 2;
            this.C1Sizer2.TabIndex = 2;
            this.C1Sizer2.Text = "C1Sizer2";
            // 
            // Panel2
            // 
            this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this.label1);
            this.Panel2.Controls.Add(this.LblOrden);
            this.Panel2.Controls.Add(this.CmdEsc);
            this.Panel2.Controls.Add(this.LblNumReg);
            this.Panel2.Controls.Add(this.Label18);
            this.Panel2.Location = new System.Drawing.Point(2, 2);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(526, 38);
            this.Panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(220, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Ordenado Por :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblOrden
            // 
            this.LblOrden.AutoSize = true;
            this.LblOrden.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblOrden.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.LblOrden.Location = new System.Drawing.Point(314, 12);
            this.LblOrden.Name = "LblOrden";
            this.LblOrden.Size = new System.Drawing.Size(65, 13);
            this.LblOrden.TabIndex = 43;
            this.LblOrden.Text = "LblOrden";
            this.LblOrden.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CmdEsc
            // 
            this.CmdEsc.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CmdEsc.Location = new System.Drawing.Point(421, 8);
            this.CmdEsc.Name = "CmdEsc";
            this.CmdEsc.Size = new System.Drawing.Size(40, 22);
            this.CmdEsc.TabIndex = 42;
            this.CmdEsc.Text = "ESC";
            this.CmdEsc.UseVisualStyleBackColor = true;
            // 
            // LblNumReg
            // 
            this.LblNumReg.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumReg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.LblNumReg.Location = new System.Drawing.Point(98, 12);
            this.LblNumReg.Name = "LblNumReg";
            this.LblNumReg.Size = new System.Drawing.Size(89, 15);
            this.LblNumReg.TabIndex = 41;
            this.LblNumReg.Text = "Label1";
            this.LblNumReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(7, 12);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(87, 13);
            this.Label18.TabIndex = 40;
            this.Label18.Text = "Nº Registros :";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Panel1
            // 
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.Button2);
            this.Panel1.Controls.Add(this.CmdAceptar);
            this.Panel1.Location = new System.Drawing.Point(530, 2);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(251, 38);
            this.Panel1.TabIndex = 0;
            // 
            // Button2
            // 
            this.Button2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button2.Image = ((System.Drawing.Image)(resources.GetObject("Button2.Image")));
            this.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button2.Location = new System.Drawing.Point(127, 0);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(111, 39);
            this.Button2.TabIndex = 4;
            this.Button2.Text = "Cancelar  ";
            this.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // CmdAceptar
            // 
            this.CmdAceptar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdAceptar.Image = ((System.Drawing.Image)(resources.GetObject("CmdAceptar.Image")));
            this.CmdAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdAceptar.Location = new System.Drawing.Point(13, 0);
            this.CmdAceptar.Name = "CmdAceptar";
            this.CmdAceptar.Size = new System.Drawing.Size(111, 39);
            this.CmdAceptar.TabIndex = 3;
            this.CmdAceptar.Text = "Aceptar  ";
            this.CmdAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CmdAceptar.UseVisualStyleBackColor = true;
            this.CmdAceptar.Click += new System.EventHandler(this.CmdAceptar_Click);
            // 
            // DgLista
            // 
            this.DgLista.CaptionHeight = 17;
            this.DgLista.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgLista.GroupByCaption = "Drag a column header here to group by that column";
            this.DgLista.Images.Add(((System.Drawing.Image)(resources.GetObject("DgLista.Images"))));
            this.DgLista.Location = new System.Drawing.Point(3, 3);
            this.DgLista.Name = "DgLista";
            this.DgLista.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.DgLista.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.DgLista.PreviewInfo.ZoomFactor = 75D;
            this.DgLista.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("DgLista.PrintInfo.PageSettings")));
            this.DgLista.RowHeight = 15;
            this.DgLista.Size = new System.Drawing.Size(783, 392);
            this.DgLista.TabIndex = 0;
            this.DgLista.Text = "C1TrueDBGrid1";
            this.DgLista.HeadClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.DgLista_HeadClick);
            this.DgLista.ColEdit += new C1.Win.C1TrueDBGrid.ColEventHandler(this.DgLista_ColEdit);
            this.DgLista.DoubleClick += new System.EventHandler(this.DgLista_DoubleClick);
            this.DgLista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DgLista_KeyPress);
            this.DgLista.PropBag = resources.GetString("DgLista.PropBag");
            // 
            // FrmFiltro2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 465);
            this.Controls.Add(this.C1Sizer1);
            this.Name = "FrmFiltro2";
            this.Text = "FrmFiltro2";
            this.Activated += new System.EventHandler(this.FrmFiltro2_Activated);
            this.Load += new System.EventHandler(this.FrmFiltro2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.C1Sizer1)).EndInit();
            this.C1Sizer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1Sizer2)).EndInit();
            this.C1Sizer2.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgLista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal C1.Win.C1Sizer.C1Sizer C1Sizer1;
        internal C1.Win.C1Sizer.C1Sizer C1Sizer2;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label LblOrden;
        internal System.Windows.Forms.Button CmdEsc;
        internal System.Windows.Forms.Label LblNumReg;
        internal System.Windows.Forms.Label Label18;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button CmdAceptar;
        internal C1.Win.C1TrueDBGrid.C1TrueDBGrid DgLista;
    }
}