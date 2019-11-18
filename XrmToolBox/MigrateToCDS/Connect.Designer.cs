﻿namespace CDSTools
{
    partial class Connect
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
            this.panelControls = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelDSType = new System.Windows.Forms.Panel();
            this.comboDSType = new System.Windows.Forms.ComboBox();
            this.labelDSType = new System.Windows.Forms.Label();
            this.AccessConnect = new CDSTools.UserControls.AccessConnection();
            this.SQLServerConnect = new CDSTools.UserControls.SQLServerConnection();
            this.buttonTest = new System.Windows.Forms.Button();
            this.panelControls.SuspendLayout();
            this.panelDSType.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControls
            // 
            this.panelControls.Controls.Add(this.buttonTest);
            this.panelControls.Controls.Add(this.buttonOK);
            this.panelControls.Controls.Add(this.buttonCancel);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControls.Location = new System.Drawing.Point(0, 194);
            this.panelControls.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(445, 41);
            this.panelControls.TabIndex = 2;
            // 
            // buttonOK
            // 
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(317, 8);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(53, 26);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(377, 8);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(58, 26);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelDSType
            // 
            this.panelDSType.Controls.Add(this.comboDSType);
            this.panelDSType.Controls.Add(this.labelDSType);
            this.panelDSType.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDSType.Location = new System.Drawing.Point(0, 0);
            this.panelDSType.Name = "panelDSType";
            this.panelDSType.Size = new System.Drawing.Size(445, 39);
            this.panelDSType.TabIndex = 3;
            // 
            // comboDSType
            // 
            this.comboDSType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDSType.FormattingEnabled = true;
            this.comboDSType.Location = new System.Drawing.Point(117, 10);
            this.comboDSType.Name = "comboDSType";
            this.comboDSType.Size = new System.Drawing.Size(301, 21);
            this.comboDSType.TabIndex = 1;
            this.comboDSType.SelectedIndexChanged += new System.EventHandler(this.ComboDSType_SelectedIndexChanged);
            // 
            // labelDSType
            // 
            this.labelDSType.AutoSize = true;
            this.labelDSType.Location = new System.Drawing.Point(13, 13);
            this.labelDSType.Name = "labelDSType";
            this.labelDSType.Size = new System.Drawing.Size(94, 13);
            this.labelDSType.TabIndex = 0;
            this.labelDSType.Text = "DataSource Type:";
            // 
            // AccessConnect
            // 
            this.AccessConnect.ConnectionString = null;
            this.AccessConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AccessConnect.Location = new System.Drawing.Point(0, 39);
            this.AccessConnect.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.AccessConnect.Name = "AccessConnect";
            this.AccessConnect.Size = new System.Drawing.Size(445, 196);
            this.AccessConnect.TabIndex = 1;
            this.AccessConnect.Visible = false;
            this.AccessConnect.ProviderChanged += new System.EventHandler(this.Connect_ProviderChanged);
            // 
            // SQLServerConnect
            // 
            this.SQLServerConnect.ConnectionString = null;
            this.SQLServerConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SQLServerConnect.Location = new System.Drawing.Point(0, 39);
            this.SQLServerConnect.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.SQLServerConnect.Name = "SQLServerConnect";
            this.SQLServerConnect.Size = new System.Drawing.Size(445, 196);
            this.SQLServerConnect.TabIndex = 0;
            this.SQLServerConnect.Visible = false;
            this.SQLServerConnect.ProviderChanged += new System.EventHandler(this.Connect_ProviderChanged);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(13, 8);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(94, 23);
            this.buttonTest.TabIndex = 2;
            this.buttonTest.Text = "Test Connection";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // Connect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(445, 235);
            this.ControlBox = false;
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.AccessConnect);
            this.Controls.Add(this.SQLServerConnect);
            this.Controls.Add(this.panelDSType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Connect";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connect";
            this.Load += new System.EventHandler(this.Connect_Load);
            this.panelControls.ResumeLayout(false);
            this.panelDSType.ResumeLayout(false);
            this.panelDSType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CDSTools.UserControls.SQLServerConnection SQLServerConnect;
        private CDSTools.UserControls.AccessConnection AccessConnect;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelDSType;
        private System.Windows.Forms.ComboBox comboDSType;
        private System.Windows.Forms.Label labelDSType;
        private System.Windows.Forms.Button buttonTest;
    }
}