namespace SIAC_NET.Formularios
{
    partial class Form7
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form7));
            this._c1r = new C1.C1Report.C1Report();
            this.Cr7 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this._c1r)).BeginInit();
            this.SuspendLayout();
            // 
            // _c1r
            // 
            this._c1r.ReportDefinition = resources.GetString("_c1r.ReportDefinition");
            this._c1r.ReportName = "lista";
            // 
            // Cr7
            // 
            this.Cr7.ActiveViewIndex = -1;
            this.Cr7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Cr7.CachedPageNumberPerDoc = 10;
            this.Cr7.Cursor = System.Windows.Forms.Cursors.Default;
            this.Cr7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cr7.Location = new System.Drawing.Point(0, 0);
            this.Cr7.Name = "Cr7";
            this.Cr7.Size = new System.Drawing.Size(854, 533);
            this.Cr7.TabIndex = 0;
            this.Cr7.Load += new System.EventHandler(this.crystalReportViewer1_Load);
            // 
            // Form7
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 533);
            this.Controls.Add(this.Cr7);
            this.Name = "Form7";
            this.Text = "Form7";
            this.Load += new System.EventHandler(this.Form7_Load);
            ((System.ComponentModel.ISupportInitialize)(this._c1r)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.C1Report.C1Report _c1r;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer Cr7;
    }
}