namespace Helper.Formularios
{
    partial class FrmVerImpresion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVerImpresion));
            this.Cr7 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // Cr7
            // 
            this.Cr7.ActiveViewIndex = -1;
            this.Cr7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Cr7.Cursor = System.Windows.Forms.Cursors.Default;
            this.Cr7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cr7.Location = new System.Drawing.Point(0, 0);
            this.Cr7.Name = "Cr7";
            this.Cr7.Size = new System.Drawing.Size(923, 624);
            this.Cr7.TabIndex = 0;
            this.Cr7.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.Cr7.Load += new System.EventHandler(this.crystalReportViewer1_Load);
            // 
            // FrmVerImpresion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 624);
            this.Controls.Add(this.Cr7);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmVerImpresion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Activated += new System.EventHandler(this.FrmVerImpresion_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer Cr7;
    }
}