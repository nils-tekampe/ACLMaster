namespace ACLMaster
{
    partial class frmSettings
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxShowInternalDetails = new System.Windows.Forms.CheckBox();
            this.checkBoxGreyOut = new System.Windows.Forms.CheckBox();
            this.radioButtonOnlyChangables = new System.Windows.Forms.RadioButton();
            this.radioButtonAllReadable = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonLoggingVerbose = new System.Windows.Forms.RadioButton();
            this.radioButtonLoggingStandard = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownValidityPeriod = new System.Windows.Forms.NumericUpDown();
            this.checkBoxWorkLocal = new System.Windows.Forms.CheckBox();
            this.checkBoxStartEscalated = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValidityPeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button3);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Size = new System.Drawing.Size(274, 407);
            this.splitContainer1.SplitterDistance = 349;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxShowInternalDetails);
            this.groupBox3.Controls.Add(this.checkBoxGreyOut);
            this.groupBox3.Controls.Add(this.radioButtonOnlyChangables);
            this.groupBox3.Controls.Add(this.radioButtonAllReadable);
            this.groupBox3.Location = new System.Drawing.Point(13, 127);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(238, 127);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Show securables";
            // 
            // checkBoxShowInternalDetails
            // 
            this.checkBoxShowInternalDetails.AutoSize = true;
            this.checkBoxShowInternalDetails.Location = new System.Drawing.Point(7, 90);
            this.checkBoxShowInternalDetails.Name = "checkBoxShowInternalDetails";
            this.checkBoxShowInternalDetails.Size = new System.Drawing.Size(183, 17);
            this.checkBoxShowInternalDetails.TabIndex = 3;
            this.checkBoxShowInternalDetails.Text = "Show internal details (expert only)";
            this.checkBoxShowInternalDetails.UseVisualStyleBackColor = true;
            // 
            // checkBoxGreyOut
            // 
            this.checkBoxGreyOut.AutoSize = true;
            this.checkBoxGreyOut.Location = new System.Drawing.Point(6, 66);
            this.checkBoxGreyOut.Name = "checkBoxGreyOut";
            this.checkBoxGreyOut.Size = new System.Drawing.Size(168, 17);
            this.checkBoxGreyOut.TabIndex = 2;
            this.checkBoxGreyOut.Text = "Grey out protected securables";
            this.checkBoxGreyOut.UseVisualStyleBackColor = true;
            this.checkBoxGreyOut.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // radioButtonOnlyChangables
            // 
            this.radioButtonOnlyChangables.AutoSize = true;
            this.radioButtonOnlyChangables.Location = new System.Drawing.Point(7, 43);
            this.radioButtonOnlyChangables.Name = "radioButtonOnlyChangables";
            this.radioButtonOnlyChangables.Size = new System.Drawing.Size(153, 17);
            this.radioButtonOnlyChangables.TabIndex = 1;
            this.radioButtonOnlyChangables.TabStop = true;
            this.radioButtonOnlyChangables.Text = "Only changable securables";
            this.radioButtonOnlyChangables.UseVisualStyleBackColor = true;
            this.radioButtonOnlyChangables.CheckedChanged += new System.EventHandler(this.radioButtonOnlyChangables_CheckedChanged);
            // 
            // radioButtonAllReadable
            // 
            this.radioButtonAllReadable.AutoSize = true;
            this.radioButtonAllReadable.Location = new System.Drawing.Point(7, 20);
            this.radioButtonAllReadable.Name = "radioButtonAllReadable";
            this.radioButtonAllReadable.Size = new System.Drawing.Size(134, 17);
            this.radioButtonAllReadable.TabIndex = 0;
            this.radioButtonAllReadable.TabStop = true;
            this.radioButtonAllReadable.Text = "All readable securables";
            this.radioButtonAllReadable.UseVisualStyleBackColor = true;
            this.radioButtonAllReadable.CheckedChanged += new System.EventHandler(this.radioButtonAllReadable_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonLoggingVerbose);
            this.groupBox2.Controls.Add(this.radioButtonLoggingStandard);
            this.groupBox2.Location = new System.Drawing.Point(12, 260);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(238, 71);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logging";
            // 
            // radioButtonLoggingVerbose
            // 
            this.radioButtonLoggingVerbose.AutoSize = true;
            this.radioButtonLoggingVerbose.Location = new System.Drawing.Point(7, 43);
            this.radioButtonLoggingVerbose.Name = "radioButtonLoggingVerbose";
            this.radioButtonLoggingVerbose.Size = new System.Drawing.Size(64, 17);
            this.radioButtonLoggingVerbose.TabIndex = 1;
            this.radioButtonLoggingVerbose.TabStop = true;
            this.radioButtonLoggingVerbose.Text = "Verbose";
            this.radioButtonLoggingVerbose.UseVisualStyleBackColor = true;
            // 
            // radioButtonLoggingStandard
            // 
            this.radioButtonLoggingStandard.AutoSize = true;
            this.radioButtonLoggingStandard.Location = new System.Drawing.Point(7, 20);
            this.radioButtonLoggingStandard.Name = "radioButtonLoggingStandard";
            this.radioButtonLoggingStandard.Size = new System.Drawing.Size(68, 17);
            this.radioButtonLoggingStandard.TabIndex = 0;
            this.radioButtonLoggingStandard.TabStop = true;
            this.radioButtonLoggingStandard.Text = "Standard";
            this.radioButtonLoggingStandard.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericUpDownValidityPeriod);
            this.groupBox1.Controls.Add(this.checkBoxWorkLocal);
            this.groupBox1.Controls.Add(this.checkBoxStartEscalated);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 109);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Scan users and groups every X days";
            // 
            // numericUpDownValidityPeriod
            // 
            this.numericUpDownValidityPeriod.Location = new System.Drawing.Point(186, 64);
            this.numericUpDownValidityPeriod.Name = "numericUpDownValidityPeriod";
            this.numericUpDownValidityPeriod.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownValidityPeriod.TabIndex = 2;
            // 
            // checkBoxWorkLocal
            // 
            this.checkBoxWorkLocal.AutoSize = true;
            this.checkBoxWorkLocal.Location = new System.Drawing.Point(7, 43);
            this.checkBoxWorkLocal.Name = "checkBoxWorkLocal";
            this.checkBoxWorkLocal.Size = new System.Drawing.Size(150, 17);
            this.checkBoxWorkLocal.TabIndex = 1;
            this.checkBoxWorkLocal.Text = "Ignore AD, work local only";
            this.checkBoxWorkLocal.UseVisualStyleBackColor = true;
            // 
            // checkBoxStartEscalated
            // 
            this.checkBoxStartEscalated.AutoSize = true;
            this.checkBoxStartEscalated.Location = new System.Drawing.Point(8, 20);
            this.checkBoxStartEscalated.Name = "checkBoxStartEscalated";
            this.checkBoxStartEscalated.Size = new System.Drawing.Size(176, 17);
            this.checkBoxStartEscalated.TabIndex = 0;
            this.checkBoxStartEscalated.Text = "Start with escalated permissions";
            this.checkBoxStartEscalated.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(98, 15);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "OK";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(179, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "About";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 407);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValidityPeriod)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonLoggingVerbose;
        private System.Windows.Forms.RadioButton radioButtonLoggingStandard;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonOnlyChangables;
        private System.Windows.Forms.RadioButton radioButtonAllReadable;
        private System.Windows.Forms.CheckBox checkBoxWorkLocal;
        private System.Windows.Forms.CheckBox checkBoxStartEscalated;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxGreyOut;
        private System.Windows.Forms.CheckBox checkBoxShowInternalDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownValidityPeriod;
    }
}