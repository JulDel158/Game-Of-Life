namespace GOL
{
    partial class TimerMD
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
            this.Ok_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.MillSecUpDown = new System.Windows.Forms.NumericUpDown();
            this.MillSecLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MillSecUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // Ok_Button
            // 
            this.Ok_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Ok_Button.Location = new System.Drawing.Point(141, 201);
            this.Ok_Button.Name = "Ok_Button";
            this.Ok_Button.Size = new System.Drawing.Size(75, 23);
            this.Ok_Button.TabIndex = 0;
            this.Ok_Button.Text = "Ok";
            this.Ok_Button.UseVisualStyleBackColor = true;
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Location = new System.Drawing.Point(222, 201);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_Button.TabIndex = 1;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            // 
            // MillSecUpDown
            // 
            this.MillSecUpDown.Location = new System.Drawing.Point(129, 93);
            this.MillSecUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.MillSecUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MillSecUpDown.Name = "MillSecUpDown";
            this.MillSecUpDown.Size = new System.Drawing.Size(120, 20);
            this.MillSecUpDown.TabIndex = 2;
            this.MillSecUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // MillSecLabel
            // 
            this.MillSecLabel.AutoSize = true;
            this.MillSecLabel.Location = new System.Drawing.Point(59, 95);
            this.MillSecLabel.Name = "MillSecLabel";
            this.MillSecLabel.Size = new System.Drawing.Size(64, 13);
            this.MillSecLabel.TabIndex = 3;
            this.MillSecLabel.Text = "Milliseconds";
            // 
            // TimerMD
            // 
            this.AcceptButton = this.Ok_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_Button;
            this.ClientSize = new System.Drawing.Size(309, 236);
            this.Controls.Add(this.MillSecLabel);
            this.Controls.Add(this.MillSecUpDown);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.Ok_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimerMD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Timer";
            ((System.ComponentModel.ISupportInitialize)(this.MillSecUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ok_Button;
        private System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.NumericUpDown MillSecUpDown;
        private System.Windows.Forms.Label MillSecLabel;
    }
}