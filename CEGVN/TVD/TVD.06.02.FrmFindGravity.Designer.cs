namespace CEGVN.TVD
{
    partial class FrmFindGravity
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
            this.Rdt_Structuralframming = new System.Windows.Forms.RadioButton();
            this.Rdt_All = new System.Windows.Forms.RadioButton();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Rdt_Structuralframming
            // 
            this.Rdt_Structuralframming.AutoSize = true;
            this.Rdt_Structuralframming.Location = new System.Drawing.Point(12, 36);
            this.Rdt_Structuralframming.Name = "Rdt_Structuralframming";
            this.Rdt_Structuralframming.Size = new System.Drawing.Size(118, 17);
            this.Rdt_Structuralframming.TabIndex = 0;
            this.Rdt_Structuralframming.TabStop = true;
            this.Rdt_Structuralframming.Text = "Structural Framming";
            this.Rdt_Structuralframming.UseVisualStyleBackColor = true;
            // 
            // Rdt_All
            // 
            this.Rdt_All.AutoSize = true;
            this.Rdt_All.Location = new System.Drawing.Point(157, 36);
            this.Rdt_All.Name = "Rdt_All";
            this.Rdt_All.Size = new System.Drawing.Size(77, 17);
            this.Rdt_All.TabIndex = 1;
            this.Rdt_All.TabStop = true;
            this.Rdt_All.Text = "All Element";
            this.Rdt_All.UseVisualStyleBackColor = true;
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(157, 86);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 2;
            this.btn_Ok.Text = "OK";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // FrmFindGravity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 121);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.Rdt_All);
            this.Controls.Add(this.Rdt_Structuralframming);
            this.Name = "FrmFindGravity";
            this.Text = "FrmFindGravity";
            this.Load += new System.EventHandler(this.FrmFindGravity_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton Rdt_Structuralframming;
        private System.Windows.Forms.RadioButton Rdt_All;
        private System.Windows.Forms.Button btn_Ok;
    }
}