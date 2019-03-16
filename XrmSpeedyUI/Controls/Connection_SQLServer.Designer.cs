namespace XrmSpeedy.Controls
{
    partial class Connection_SQLServer
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
            this.txtConnectionUsername = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.chkConnectionIntegrated = new System.Windows.Forms.CheckBox();
            this.txtConnectionDatabase = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConnectionServer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtConnectionPassword
            // 
            this.txtConnectionPassword.Enabled = false;
            this.txtConnectionPassword.Location = new System.Drawing.Point(63, 106);
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
            this.label33.Location = new System.Drawing.Point(3, 106);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(53, 13);
            this.label33.TabIndex = 31;
            this.label33.Text = "Password";
            // 
            // txtConnectionUsername
            // 
            this.txtConnectionUsername.Enabled = false;
            this.txtConnectionUsername.Location = new System.Drawing.Point(63, 74);
            this.txtConnectionUsername.MaxLength = 100;
            this.txtConnectionUsername.Name = "txtConnectionUsername";
            this.txtConnectionUsername.Size = new System.Drawing.Size(181, 20);
            this.txtConnectionUsername.TabIndex = 30;
            this.txtConnectionUsername.Validated += new System.EventHandler(this.txtConnectionUsername_Validated);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(3, 74);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(55, 13);
            this.label32.TabIndex = 29;
            this.label32.Text = "Username";
            // 
            // chkConnectionIntegrated
            // 
            this.chkConnectionIntegrated.AutoSize = true;
            this.chkConnectionIntegrated.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConnectionIntegrated.Checked = true;
            this.chkConnectionIntegrated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConnectionIntegrated.Location = new System.Drawing.Point(3, 50);
            this.chkConnectionIntegrated.Name = "chkConnectionIntegrated";
            this.chkConnectionIntegrated.Size = new System.Drawing.Size(115, 17);
            this.chkConnectionIntegrated.TabIndex = 28;
            this.chkConnectionIntegrated.Text = "Integrated Security";
            this.chkConnectionIntegrated.UseVisualStyleBackColor = true;
            this.chkConnectionIntegrated.CheckedChanged += new System.EventHandler(this.chkConnectionIntegrated_CheckedChanged);
            // 
            // txtConnectionDatabase
            // 
            this.txtConnectionDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConnectionDatabase.Location = new System.Drawing.Point(63, 25);
            this.txtConnectionDatabase.MaxLength = 100;
            this.txtConnectionDatabase.Multiline = true;
            this.txtConnectionDatabase.Name = "txtConnectionDatabase";
            this.txtConnectionDatabase.Size = new System.Drawing.Size(181, 20);
            this.txtConnectionDatabase.TabIndex = 27;
            this.txtConnectionDatabase.Validated += new System.EventHandler(this.txtConnectionDatabase_Validated);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(3, 25);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(53, 13);
            this.label31.TabIndex = 26;
            this.label31.Text = "Database";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Server";
            // 
            // txtConnectionServer
            // 
            this.txtConnectionServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConnectionServer.Location = new System.Drawing.Point(63, 0);
            this.txtConnectionServer.MaxLength = 100;
            this.txtConnectionServer.Multiline = true;
            this.txtConnectionServer.Name = "txtConnectionServer";
            this.txtConnectionServer.Size = new System.Drawing.Size(181, 20);
            this.txtConnectionServer.TabIndex = 24;
            this.txtConnectionServer.Validated += new System.EventHandler(this.txtConnectionServer_Validated);
            // 
            // Connection_SQLServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtConnectionPassword);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.txtConnectionUsername);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.chkConnectionIntegrated);
            this.Controls.Add(this.txtConnectionDatabase);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConnectionServer);
            this.Name = "Connection_SQLServer";
            this.Size = new System.Drawing.Size(266, 127);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConnectionPassword;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txtConnectionUsername;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.CheckBox chkConnectionIntegrated;
        private System.Windows.Forms.TextBox txtConnectionDatabase;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtConnectionServer;
    }
}
