namespace IRCTC
{
    partial class FormDetailsAutomation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDetailsAutomation));
            this.btn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnPaymentDetails = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn
            // 
            this.btn.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btn.Location = new System.Drawing.Point(30, 26);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(101, 23);
            this.btn.TabIndex = 0;
            this.btn.Text = "TravelDetails";
            this.btn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn.UseVisualStyleBackColor = false;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.button1.Location = new System.Drawing.Point(30, 74);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "PassengerDetails";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnPaymentDetails
            // 
            this.btnPaymentDetails.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnPaymentDetails.Location = new System.Drawing.Point(30, 119);
            this.btnPaymentDetails.Name = "btnPaymentDetails";
            this.btnPaymentDetails.Size = new System.Drawing.Size(101, 23);
            this.btnPaymentDetails.TabIndex = 2;
            this.btnPaymentDetails.Text = "PaymentDetails";
            this.btnPaymentDetails.UseVisualStyleBackColor = false;
            this.btnPaymentDetails.Click += new System.EventHandler(this.btnPaymentDetails_Click);
            // 
            // FormDetailsAutomation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(159, 166);
            this.Controls.Add(this.btnPaymentDetails);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDetailsAutomation";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormDetailsAutomation";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnPaymentDetails;
    }
}