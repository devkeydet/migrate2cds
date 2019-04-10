﻿namespace CDSTools.UserControls
{
    partial class SQLServerConnection
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
            this.txtConnectionPassword = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.txtConnectionUsername = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.chkConnectionIntegrated = new System.Windows.Forms.CheckBox();
            this.txtConnectionDatabase = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConnectionServer = new System.Windows.Forms.TextBox();
            this.errorMgr = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorMgr)).BeginInit();
            this.SuspendLayout();
            // 
            // txtConnectionPassword
            // 
            this.txtConnectionPassword.Enabled = false;
            this.txtConnectionPassword.Location = new System.Drawing.Point(116, 105);
            this.txtConnectionPassword.MaxLength = 100;
            this.txtConnectionPassword.Name = "txtConnectionPassword";
            this.txtConnectionPassword.PasswordChar = '*';
            this.txtConnectionPassword.Size = new System.Drawing.Size(265, 20);
            this.txtConnectionPassword.TabIndex = 32;
            // 
            // label33
            // 
            this.label33.Location = new System.Drawing.Point(7, 108);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(85, 19);
            this.label33.TabIndex = 31;
            this.label33.Text = "Password";
            // 
            // txtConnectionUsername
            // 
            this.txtConnectionUsername.Enabled = false;
            this.txtConnectionUsername.Location = new System.Drawing.Point(116, 81);
            this.txtConnectionUsername.MaxLength = 100;
            this.txtConnectionUsername.Name = "txtConnectionUsername";
            this.txtConnectionUsername.Size = new System.Drawing.Size(265, 20);
            this.txtConnectionUsername.TabIndex = 30;
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(7, 84);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(85, 19);
            this.label32.TabIndex = 29;
            this.label32.Text = "Username";
            // 
            // chkConnectionIntegrated
            // 
            this.chkConnectionIntegrated.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConnectionIntegrated.Checked = true;
            this.chkConnectionIntegrated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConnectionIntegrated.Location = new System.Drawing.Point(7, 58);
            this.chkConnectionIntegrated.Name = "chkConnectionIntegrated";
            this.chkConnectionIntegrated.Size = new System.Drawing.Size(123, 19);
            this.chkConnectionIntegrated.TabIndex = 28;
            this.chkConnectionIntegrated.Text = "Integrated Security";
            this.chkConnectionIntegrated.UseVisualStyleBackColor = true;
            this.chkConnectionIntegrated.CheckedChanged += new System.EventHandler(this.chkConnectionIntegrated_CheckedChanged);
            // 
            // txtConnectionDatabase
            // 
            this.txtConnectionDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConnectionDatabase.Location = new System.Drawing.Point(116, 34);
            this.txtConnectionDatabase.MaxLength = 100;
            this.txtConnectionDatabase.Name = "txtConnectionDatabase";
            this.txtConnectionDatabase.Size = new System.Drawing.Size(265, 20);
            this.txtConnectionDatabase.TabIndex = 27;
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(7, 37);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(85, 19);
            this.label31.TabIndex = 26;
            this.label31.Text = "Database";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 19);
            this.label2.TabIndex = 25;
            this.label2.Text = "Server";
            // 
            // txtConnectionServer
            // 
            this.txtConnectionServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConnectionServer.Location = new System.Drawing.Point(116, 8);
            this.txtConnectionServer.MaxLength = 100;
            this.txtConnectionServer.Name = "txtConnectionServer";
            this.txtConnectionServer.Size = new System.Drawing.Size(265, 20);
            this.txtConnectionServer.TabIndex = 24;
            // 
            // errorMgr
            // 
            this.errorMgr.ContainerControl = this;
            // 
            // SQLServerConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkConnectionIntegrated);
            this.Controls.Add(this.txtConnectionPassword);
            this.Controls.Add(this.txtConnectionServer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConnectionDatabase);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.txtConnectionUsername);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label32);
            this.Name = "SQLServerConnection";
            this.Size = new System.Drawing.Size(399, 143);
            this.EnabledChanged += new System.EventHandler(this.SQLServerConnect_EnabledChanged);
            ((System.ComponentModel.ISupportInitialize)(this.errorMgr)).EndInit();
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
        private System.Windows.Forms.ErrorProvider errorMgr;
    }
}
