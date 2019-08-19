namespace CDSTools.UserControls
{
    partial class AccessConnection
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
            this.components = new System.ComponentModel.Container();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.chkPassword = new System.Windows.Forms.CheckBox();
            this.labelDBFile = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblRuntime = new System.Windows.Forms.Label();
            this.errorMgr = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorMgr)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(222, 127);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(6);
            this.txtPassword.MaxLength = 100;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(442, 29);
            this.txtPassword.TabIndex = 32;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(13, 131);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(98, 25);
            this.labelPassword.TabIndex = 31;
            this.labelPassword.Text = "Password";
            // 
            // chkPassword
            // 
            this.chkPassword.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPassword.Location = new System.Drawing.Point(13, 72);
            this.chkPassword.Margin = new System.Windows.Forms.Padding(6);
            this.chkPassword.Name = "chkPassword";
            this.chkPassword.Size = new System.Drawing.Size(232, 33);
            this.chkPassword.TabIndex = 28;
            this.chkPassword.Text = "Password required?";
            this.chkPassword.UseVisualStyleBackColor = true;
            this.chkPassword.CheckedChanged += new System.EventHandler(this.chkPassword_CheckedChanged);
            // 
            // labelDBFile
            // 
            this.labelDBFile.AutoSize = true;
            this.labelDBFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDBFile.Location = new System.Drawing.Point(13, 20);
            this.labelDBFile.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelDBFile.Name = "labelDBFile";
            this.labelDBFile.Size = new System.Drawing.Size(86, 25);
            this.labelDBFile.TabIndex = 25;
            this.labelDBFile.Text = "Location";
            // 
            // txtLocation
            // 
            this.txtLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocation.Location = new System.Drawing.Point(222, 17);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(6);
            this.txtLocation.MaxLength = 1000;
            this.txtLocation.Multiline = true;
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(442, 34);
            this.txtLocation.TabIndex = 24;
            this.txtLocation.WordWrap = false;
            // 
            // openFileDlg
            // 
            this.openFileDlg.FileName = "*.accdb";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(677, 13);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(6);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(44, 44);
            this.btnBrowse.TabIndex = 33;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblRuntime
            // 
            this.lblRuntime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRuntime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRuntime.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblRuntime.Location = new System.Drawing.Point(178, 207);
            this.lblRuntime.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblRuntime.Name = "lblRuntime";
            this.lblRuntime.Size = new System.Drawing.Size(370, 26);
            this.lblRuntime.TabIndex = 34;
            this.lblRuntime.Text = "Access 2007 Download: Access Runtime";
            this.lblRuntime.Click += new System.EventHandler(this.lblRuntime_Click);
            // 
            // errorMgr
            // 
            this.errorMgr.ContainerControl = this;
            // 
            // AccessConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.lblRuntime);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.chkPassword);
            this.Controls.Add(this.labelDBFile);
            this.Controls.Add(this.txtLocation);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "AccessConnection";
            this.Size = new System.Drawing.Size(735, 264);
            this.EnabledChanged += new System.EventHandler(this.AccessConnection_EnabledChanged);
            ((System.ComponentModel.ISupportInitialize)(this.errorMgr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.CheckBox chkPassword;
        private System.Windows.Forms.Label labelDBFile;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
        private System.Windows.Forms.Label lblRuntime;
        private System.Windows.Forms.ErrorProvider errorMgr;
    }
}
