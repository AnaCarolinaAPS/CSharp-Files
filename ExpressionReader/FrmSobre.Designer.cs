namespace ExpressionReader
{
    partial class FrmSobre
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblText = new System.Windows.Forms.Label();
            this.btnDicas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(153, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sobre o SAAPC";
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText.Location = new System.Drawing.Point(12, 56);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(41, 15);
            this.lblText.TabIndex = 1;
            this.lblText.Text = "label2";
            this.lblText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnDicas
            // 
            this.btnDicas.Location = new System.Drawing.Point(167, 113);
            this.btnDicas.Name = "btnDicas";
            this.btnDicas.Size = new System.Drawing.Size(75, 23);
            this.btnDicas.TabIndex = 2;
            this.btnDicas.Text = "Dicas";
            this.btnDicas.UseVisualStyleBackColor = true;
            this.btnDicas.Click += new System.EventHandler(this.btnDicas_Click);
            // 
            // FrmSobre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 211);
            this.Controls.Add(this.btnDicas);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximumSize = new System.Drawing.Size(425, 380);
            this.MinimumSize = new System.Drawing.Size(425, 250);
            this.Name = "FrmSobre";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sobre";
            this.Load += new System.EventHandler(this.FrmSobre_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Button btnDicas;
    }
}