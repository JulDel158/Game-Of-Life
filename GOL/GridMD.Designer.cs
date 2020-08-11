namespace GOL
{
    partial class GridMD
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
            this.Apply_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.xAxisNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.X_Label = new System.Windows.Forms.Label();
            this.yAxisNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.yLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.xAxisNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yAxisNumUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // Apply_Button
            // 
            this.Apply_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Apply_Button.Location = new System.Drawing.Point(62, 200);
            this.Apply_Button.Name = "Apply_Button";
            this.Apply_Button.Size = new System.Drawing.Size(75, 23);
            this.Apply_Button.TabIndex = 0;
            this.Apply_Button.Text = "Apply";
            this.Apply_Button.UseVisualStyleBackColor = true;
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Location = new System.Drawing.Point(143, 200);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_Button.TabIndex = 1;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            // 
            // xAxisNumUpDown
            // 
            this.xAxisNumUpDown.Location = new System.Drawing.Point(54, 37);
            this.xAxisNumUpDown.Name = "xAxisNumUpDown";
            this.xAxisNumUpDown.Size = new System.Drawing.Size(120, 20);
            this.xAxisNumUpDown.TabIndex = 2;
            // 
            // X_Label
            // 
            this.X_Label.AutoSize = true;
            this.X_Label.Location = new System.Drawing.Point(12, 39);
            this.X_Label.Name = "X_Label";
            this.X_Label.Size = new System.Drawing.Size(35, 13);
            this.X_Label.TabIndex = 3;
            this.X_Label.Text = "Width";
            // 
            // yAxisNumUpDown
            // 
            this.yAxisNumUpDown.Location = new System.Drawing.Point(54, 96);
            this.yAxisNumUpDown.Name = "yAxisNumUpDown";
            this.yAxisNumUpDown.Size = new System.Drawing.Size(120, 20);
            this.yAxisNumUpDown.TabIndex = 4;
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(12, 98);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(38, 13);
            this.yLabel.TabIndex = 5;
            this.yLabel.Text = "Height";
            // 
            // GridMD
            // 
            this.AcceptButton = this.Apply_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_Button;
            this.ClientSize = new System.Drawing.Size(230, 235);
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.yAxisNumUpDown);
            this.Controls.Add(this.X_Label);
            this.Controls.Add(this.xAxisNumUpDown);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.Apply_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GridMD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grid";
            ((System.ComponentModel.ISupportInitialize)(this.xAxisNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yAxisNumUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Apply_Button;
        private System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.NumericUpDown xAxisNumUpDown;
        private System.Windows.Forms.Label X_Label;
        private System.Windows.Forms.NumericUpDown yAxisNumUpDown;
        private System.Windows.Forms.Label yLabel;
    }
}