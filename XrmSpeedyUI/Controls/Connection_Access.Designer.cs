namespace XrmSpeedy.Controls
{
    partial class Connection_Access
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtConnectionPassword = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.chkPassword = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblRuntime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtConnectionPassword
            // 
            this.txtConnectionPassword.Enabled = false;
            this.txtConnectionPassword.Location = new System.Drawing.Point(63, 55);
            this.txtConnectionPassword.MaxLength = 100;
            this.txtConnectionPassword.Name = "txtConnectionPassword";
            this.txtConnectionPassword.PasswordChar = '*';
            this.txtConnectionPassword.Size = new System.Drawing.Size(181, 20);
            this.txtConnectionPassword.TabIndex = 32;
            this.txtConnectionPassword.Validated += new System.EventHandler(this.txtConnectionPassword_Validated);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(3, 55);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(53, 13);
            this.label33.TabIndex = 31;
            this.label33.Text = "Password";
            // 
            // chkPassword
            // 
            this.chkPassword.AutoSize = true;
            this.chkPassword.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPassword.Location = new System.Drawing.Point(3, 26);
            this.chkPassword.Name = "chkPassword";
            this.chkPassword.Size = new System.Drawing.Size(121, 17);
            this.chkPassword.TabIndex = 28;
            this.chkPassword.Text = "Database Password";
            this.chkPassword.UseVisualStyleBackColor = true;
            this.chkPassword.CheckedChanged += new System.EventHandler(this.chkPassword_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Location";
            // 
            // txtLocation
            // 
            this.txtLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocation.Location = new System.Drawing.Point(64, 0);
            this.txtLocation.MaxLength = 1000;
            this.txtLocation.Multiline = true;
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(181, 20);
            this.txtLocation.TabIndex = 24;
            this.txtLocation.WordWrap = false;
            this.txtLocation.Validated += new System.EventHandler(this.txtLocationServer_Validated);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(169, 26);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 33;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblRuntime
            // 
            this.lblRuntime.AutoSize = true;
            this.lblRuntime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRuntime.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblRuntime.Location = new System.Drawing.Point(3, 91);
            this.lblRuntime.Name = "lblRuntime";
            this.lblRuntime.Size = new System.Drawing.Size(203, 13);
            this.lblRuntime.TabIndex = 34;
            this.lblRuntime.Text = "Access 2007 Download: Access Runtime";
            this.lblRuntime.Click += new System.EventHandler(this.lblRuntime_Click);
            // 
            // Connection_Access
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblRuntime);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtConnectionPassword);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.chkPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLocation);
            this.Name = "Connection_Access";
            this.Size = new System.Drawing.Size(266, 127);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConnectionPassword;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.CheckBox chkPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblRuntime;
    }
}
