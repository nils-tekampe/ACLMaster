namespace ACLMaster
{
    partial class frmSelectUser
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxMyGroupsOnly = new System.Windows.Forms.CheckBox();
            this.checkBoxADGroups = new System.Windows.Forms.CheckBox();
            this.checkBoxADUser = new System.Windows.Forms.CheckBox();
            this.checkBoxLocalGroups = new System.Windows.Forms.CheckBox();
            this.checkBoLocalUser = new System.Windows.Forms.CheckBox();
            this.listViewUser = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listViewUser);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(457, 434);
            this.splitContainer1.SplitterDistance = 164;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer3.Size = new System.Drawing.Size(457, 164);
            this.splitContainer3.SplitterDistance = 88;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Location = new System.Drawing.Point(0, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(457, 82);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search for User";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button3.Location = new System.Drawing.Point(358, 22);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(62, 30);
            this.button3.TabIndex = 2;
            this.button3.Text = "OK";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(291, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 30);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(268, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxMyGroupsOnly);
            this.groupBox1.Controls.Add(this.checkBoxADGroups);
            this.groupBox1.Controls.Add(this.checkBoxADUser);
            this.groupBox1.Controls.Add(this.checkBoxLocalGroups);
            this.groupBox1.Controls.Add(this.checkBoLocalUser);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(457, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter User";
            // 
            // checkBoxMyGroupsOnly
            // 
            this.checkBoxMyGroupsOnly.AutoSize = true;
            this.checkBoxMyGroupsOnly.Location = new System.Drawing.Point(12, 43);
            this.checkBoxMyGroupsOnly.Name = "checkBoxMyGroupsOnly";
            this.checkBoxMyGroupsOnly.Size = new System.Drawing.Size(126, 17);
            this.checkBoxMyGroupsOnly.TabIndex = 4;
            this.checkBoxMyGroupsOnly.Text = "Show my groups only";
            this.checkBoxMyGroupsOnly.UseVisualStyleBackColor = true;
            this.checkBoxMyGroupsOnly.CheckedChanged += new System.EventHandler(this.checkBoxMyGroupsOnly_CheckedChanged);
            // 
            // checkBoxADGroups
            // 
            this.checkBoxADGroups.AutoSize = true;
            this.checkBoxADGroups.Location = new System.Drawing.Point(291, 18);
            this.checkBoxADGroups.Name = "checkBoxADGroups";
            this.checkBoxADGroups.Size = new System.Drawing.Size(78, 17);
            this.checkBoxADGroups.TabIndex = 3;
            this.checkBoxADGroups.Text = "AD Groups";
            this.checkBoxADGroups.UseVisualStyleBackColor = true;
            this.checkBoxADGroups.CheckedChanged += new System.EventHandler(this.checkBoxADGroups_CheckedChanged);
            // 
            // checkBoxADUser
            // 
            this.checkBoxADUser.AutoSize = true;
            this.checkBoxADUser.Location = new System.Drawing.Point(209, 18);
            this.checkBoxADUser.Name = "checkBoxADUser";
            this.checkBoxADUser.Size = new System.Drawing.Size(71, 17);
            this.checkBoxADUser.TabIndex = 2;
            this.checkBoxADUser.Text = "AD Users";
            this.checkBoxADUser.UseVisualStyleBackColor = true;
            this.checkBoxADUser.CheckedChanged += new System.EventHandler(this.checkBoxADUser_CheckedChanged);
            // 
            // checkBoxLocalGroups
            // 
            this.checkBoxLocalGroups.AutoSize = true;
            this.checkBoxLocalGroups.Checked = true;
            this.checkBoxLocalGroups.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLocalGroups.Location = new System.Drawing.Point(110, 18);
            this.checkBoxLocalGroups.Name = "checkBoxLocalGroups";
            this.checkBoxLocalGroups.Size = new System.Drawing.Size(89, 17);
            this.checkBoxLocalGroups.TabIndex = 1;
            this.checkBoxLocalGroups.Text = "Local Groups";
            this.checkBoxLocalGroups.UseVisualStyleBackColor = true;
            this.checkBoxLocalGroups.CheckedChanged += new System.EventHandler(this.checkBoxLocalGroups_CheckedChanged);
            this.checkBoxLocalGroups.CheckStateChanged += new System.EventHandler(this.checkBoxLocalGroups_CheckStateChanged);
            // 
            // checkBoLocalUser
            // 
            this.checkBoLocalUser.AutoSize = true;
            this.checkBoLocalUser.Checked = true;
            this.checkBoLocalUser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoLocalUser.Location = new System.Drawing.Point(12, 18);
            this.checkBoLocalUser.Name = "checkBoLocalUser";
            this.checkBoLocalUser.Size = new System.Drawing.Size(82, 17);
            this.checkBoLocalUser.TabIndex = 0;
            this.checkBoLocalUser.Text = "Local Users";
            this.checkBoLocalUser.UseVisualStyleBackColor = true;
            this.checkBoLocalUser.CheckedChanged += new System.EventHandler(this.checkBoLocalUser_CheckedChanged);
            // 
            // listViewUser
            // 
            this.listViewUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewUser.FullRowSelect = true;
            this.listViewUser.Location = new System.Drawing.Point(0, 0);
            this.listViewUser.Name = "listViewUser";
            this.listViewUser.Size = new System.Drawing.Size(457, 266);
            this.listViewUser.TabIndex = 1;
            this.listViewUser.UseCompatibleStateImageBehavior = false;
            this.listViewUser.View = System.Windows.Forms.View.Details;
            // 
            // frmSelectUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 434);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmSelectUser";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please select user";
            this.Load += new System.EventHandler(this.frmSelectUser_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListView listViewUser;
        private System.Windows.Forms.CheckBox checkBoxADGroups;
        private System.Windows.Forms.CheckBox checkBoxADUser;
        private System.Windows.Forms.CheckBox checkBoxLocalGroups;
        private System.Windows.Forms.CheckBox checkBoLocalUser;
        private System.Windows.Forms.CheckBox checkBoxMyGroupsOnly;
    }
}