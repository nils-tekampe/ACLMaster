namespace ACLMaster
{
    partial class frmMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.splitContainerForm = new System.Windows.Forms.SplitContainer();
            this.splitContainerLeft = new System.Windows.Forms.SplitContainer();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.toolStripLeft = new System.Windows.Forms.ToolStrip();
            this.toolStripLeftButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.treeViewFolder = new System.Windows.Forms.TreeView();
            this.splitContainerRight = new System.Windows.Forms.SplitContainer();
            this.splitContainerTopRight = new System.Windows.Forms.SplitContainer();
            this.toolStripRight = new System.Windows.Forms.ToolStrip();
            this.toolStripRightButtonCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripRightButtonPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripRightButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripRightButtonGroup = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButtonUser = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButtonUserTO = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButtonUserCO = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButtonTools = new System.Windows.Forms.ToolStripDropDownButton();
            this.copyFilePermissionAsTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPermissionsAsMarkupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButtonSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.elevatePermissionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rerteiveDomainInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewSecurables = new BrightIdeasSoftware.ObjectListView();
            this.columnSecurablesIcon = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnLastUpdate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnOwner = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnACL = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.splitContainerBottomRight = new System.Windows.Forms.SplitContainer();
            this.checkBoxInheritance = new System.Windows.Forms.CheckBox();
            this.toolStripBottom = new System.Windows.Forms.ToolStrip();
            this.toolStripBottomButtonAddRule = new System.Windows.Forms.ToolStripDropDownButton();
            this.denyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readAndExecuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullControlToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.readAndExecuteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.readToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.writeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripBottomButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripBottomButtonApply = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listViewPermissions = new BrightIdeasSoftware.ObjectListView();
            this.columnPermissionImage = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnPrincipal = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnPermission = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnGrantStatus = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnInherited = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnPropagates = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnInheritanceFlags = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuPermissions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CO = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CP = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SP = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.CG = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.PF = new System.Windows.Forms.ToolStripMenuItem();
            this.columnACLHash = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.baseRenderer1 = new BrightIdeasSoftware.BaseRenderer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerForm)).BeginInit();
            this.splitContainerForm.Panel1.SuspendLayout();
            this.splitContainerForm.Panel2.SuspendLayout();
            this.splitContainerForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeft)).BeginInit();
            this.splitContainerLeft.Panel1.SuspendLayout();
            this.splitContainerLeft.Panel2.SuspendLayout();
            this.splitContainerLeft.SuspendLayout();
            this.toolStripLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).BeginInit();
            this.splitContainerRight.Panel1.SuspendLayout();
            this.splitContainerRight.Panel2.SuspendLayout();
            this.splitContainerRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTopRight)).BeginInit();
            this.splitContainerTopRight.Panel1.SuspendLayout();
            this.splitContainerTopRight.Panel2.SuspendLayout();
            this.splitContainerTopRight.SuspendLayout();
            this.toolStripRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listViewSecurables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBottomRight)).BeginInit();
            this.splitContainerBottomRight.Panel1.SuspendLayout();
            this.splitContainerBottomRight.Panel2.SuspendLayout();
            this.splitContainerBottomRight.SuspendLayout();
            this.toolStripBottom.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listViewPermissions)).BeginInit();
            this.contextMenuPermissions.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerForm
            // 
            this.splitContainerForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerForm.Location = new System.Drawing.Point(0, 0);
            this.splitContainerForm.Name = "splitContainerForm";
            // 
            // splitContainerForm.Panel1
            // 
            this.splitContainerForm.Panel1.Controls.Add(this.splitContainerLeft);
            this.splitContainerForm.Panel1.Margin = new System.Windows.Forms.Padding(4);
            // 
            // splitContainerForm.Panel2
            // 
            this.splitContainerForm.Panel2.Controls.Add(this.splitContainerRight);
            this.splitContainerForm.Size = new System.Drawing.Size(1112, 730);
            this.splitContainerForm.SplitterDistance = 350;
            this.splitContainerForm.TabIndex = 0;
            // 
            // splitContainerLeft
            // 
            this.splitContainerLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLeft.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerLeft.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLeft.Name = "splitContainerLeft";
            this.splitContainerLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLeft.Panel1
            // 
            this.splitContainerLeft.Panel1.Controls.Add(this.comboBox1);
            this.splitContainerLeft.Panel1.Controls.Add(this.toolStripLeft);
            this.splitContainerLeft.Panel1MinSize = 48;
            // 
            // splitContainerLeft.Panel2
            // 
            this.splitContainerLeft.Panel2.Controls.Add(this.treeViewFolder);
            this.splitContainerLeft.Panel2MinSize = 0;
            this.splitContainerLeft.Size = new System.Drawing.Size(350, 730);
            this.splitContainerLeft.SplitterDistance = 48;
            this.splitContainerLeft.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(115, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // toolStripLeft
            // 
            this.toolStripLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripLeft.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripLeft.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripLeft.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLeftButtonRefresh,
            this.toolStripButton5});
            this.toolStripLeft.Location = new System.Drawing.Point(0, 0);
            this.toolStripLeft.Name = "toolStripLeft";
            this.toolStripLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripLeft.Size = new System.Drawing.Size(350, 48);
            this.toolStripLeft.TabIndex = 0;
            this.toolStripLeft.Text = "toolStrip1";
            this.toolStripLeft.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolStripLeftButtonRefresh
            // 
            this.toolStripLeftButtonRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripLeftButtonRefresh.Image = global::ACLMaster.Properties.Resources.refresh;
            this.toolStripLeftButtonRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripLeftButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLeftButtonRefresh.Margin = new System.Windows.Forms.Padding(135, 1, 10, 2);
            this.toolStripLeftButtonRefresh.Name = "toolStripLeftButtonRefresh";
            this.toolStripLeftButtonRefresh.Size = new System.Drawing.Size(102, 45);
            this.toolStripLeftButtonRefresh.Text = "Reload Drives";
            this.toolStripLeftButtonRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripLeftButtonRefresh.ToolTipText = "Reload Drives (Strg + R)";
            this.toolStripLeftButtonRefresh.Click += new System.EventHandler(this.toolStripButton1_Click_2);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = global::ACLMaster.Properties.Resources.add_folder;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(94, 45);
            this.toolStripButton5.Text = "Open folder";
            this.toolStripButton5.ToolTipText = "Open (network) folder (Strg + O)";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // treeViewFolder
            // 
            this.treeViewFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFolder.HideSelection = false;
            this.treeViewFolder.Location = new System.Drawing.Point(0, 0);
            this.treeViewFolder.Name = "treeViewFolder";
            this.treeViewFolder.Size = new System.Drawing.Size(350, 678);
            this.treeViewFolder.TabIndex = 0;
            this.treeViewFolder.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeViewFolder.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFolder_AfterSelect);
            this.treeViewFolder.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick_1);
            // 
            // splitContainerRight
            // 
            this.splitContainerRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRight.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRight.Name = "splitContainerRight";
            this.splitContainerRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRight.Panel1
            // 
            this.splitContainerRight.Panel1.Controls.Add(this.splitContainerTopRight);
            // 
            // splitContainerRight.Panel2
            // 
            this.splitContainerRight.Panel2.Controls.Add(this.splitContainerBottomRight);
            this.splitContainerRight.Size = new System.Drawing.Size(758, 730);
            this.splitContainerRight.SplitterDistance = 431;
            this.splitContainerRight.TabIndex = 0;
            // 
            // splitContainerTopRight
            // 
            this.splitContainerTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTopRight.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerTopRight.Location = new System.Drawing.Point(0, 0);
            this.splitContainerTopRight.Name = "splitContainerTopRight";
            this.splitContainerTopRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerTopRight.Panel1
            // 
            this.splitContainerTopRight.Panel1.Controls.Add(this.toolStripRight);
            // 
            // splitContainerTopRight.Panel2
            // 
            this.splitContainerTopRight.Panel2.Controls.Add(this.listViewSecurables);
            this.splitContainerTopRight.Size = new System.Drawing.Size(758, 431);
            this.splitContainerTopRight.SplitterDistance = 48;
            this.splitContainerTopRight.TabIndex = 0;
            // 
            // toolStripRight
            // 
            this.toolStripRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripRight.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripRight.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripRightButtonCopy,
            this.toolStripRightButtonPaste,
            this.toolStripButton2,
            this.toolStripRightButtonRefresh,
            this.toolStripRightButtonGroup,
            this.toolStripButton1,
            this.toolStripDropDownButtonUser,
            this.toolStripButton7,
            this.toolStripDropDownButtonTools,
            this.toolStripButton4,
            this.toolStripDropDownButtonSettings});
            this.toolStripRight.Location = new System.Drawing.Point(0, 0);
            this.toolStripRight.Name = "toolStripRight";
            this.toolStripRight.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripRight.Size = new System.Drawing.Size(758, 48);
            this.toolStripRight.TabIndex = 0;
            this.toolStripRight.Text = "toolStrip2";
            // 
            // toolStripRightButtonCopy
            // 
            this.toolStripRightButtonCopy.Image = global::ACLMaster.Properties.Resources.copy;
            this.toolStripRightButtonCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripRightButtonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRightButtonCopy.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.toolStripRightButtonCopy.Name = "toolStripRightButtonCopy";
            this.toolStripRightButtonCopy.Size = new System.Drawing.Size(125, 45);
            this.toolStripRightButtonCopy.Text = "Copy Permissions";
            this.toolStripRightButtonCopy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripRightButtonCopy.ToolTipText = "Copy Permissions (Strg + C)";
            this.toolStripRightButtonCopy.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripRightButtonPaste
            // 
            this.toolStripRightButtonPaste.Image = global::ACLMaster.Properties.Resources.paste;
            this.toolStripRightButtonPaste.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripRightButtonPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRightButtonPaste.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.toolStripRightButtonPaste.Name = "toolStripRightButtonPaste";
            this.toolStripRightButtonPaste.Size = new System.Drawing.Size(125, 45);
            this.toolStripRightButtonPaste.Text = "Paste Permissions";
            this.toolStripRightButtonPaste.ToolTipText = "Paste Permissions (Strg + V)";
            this.toolStripRightButtonPaste.Click += new System.EventHandler(this.toolStripRightButtonPaste_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 45);
            // 
            // toolStripRightButtonRefresh
            // 
            this.toolStripRightButtonRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripRightButtonRefresh.Image = global::ACLMaster.Properties.Resources.refresh;
            this.toolStripRightButtonRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripRightButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRightButtonRefresh.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.toolStripRightButtonRefresh.Name = "toolStripRightButtonRefresh";
            this.toolStripRightButtonRefresh.Size = new System.Drawing.Size(93, 45);
            this.toolStripRightButtonRefresh.Text = "Reload Files";
            this.toolStripRightButtonRefresh.ToolTipText = "Reload Files (F5)";
            this.toolStripRightButtonRefresh.Click += new System.EventHandler(this.toolStripRightButtonRefresh_Click);
            // 
            // toolStripRightButtonGroup
            // 
            this.toolStripRightButtonGroup.Image = global::ACLMaster.Properties.Resources.sort;
            this.toolStripRightButtonGroup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripRightButtonGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRightButtonGroup.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.toolStripRightButtonGroup.Name = "toolStripRightButtonGroup";
            this.toolStripRightButtonGroup.Size = new System.Drawing.Size(64, 45);
            this.toolStripRightButtonGroup.Text = "Group";
            this.toolStripRightButtonGroup.ToolTipText = "Group (Strg + G)";
            this.toolStripRightButtonGroup.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Enabled = false;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 45);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.DisplayStyleChanged += new System.EventHandler(this.toolStripButton1_DisplayStyleChanged);
            // 
            // toolStripDropDownButtonUser
            // 
            this.toolStripDropDownButtonUser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonUserTO,
            this.toolStripDropDownButtonUserCO});
            this.toolStripDropDownButtonUser.Enabled = false;
            this.toolStripDropDownButtonUser.Image = global::ACLMaster.Properties.Resources.user;
            this.toolStripDropDownButtonUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripDropDownButtonUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonUser.Name = "toolStripDropDownButtonUser";
            this.toolStripDropDownButtonUser.Size = new System.Drawing.Size(75, 45);
            this.toolStripDropDownButtonUser.Text = "Owner";
            this.toolStripDropDownButtonUser.ToolTipText = "Owner";
            this.toolStripDropDownButtonUser.Click += new System.EventHandler(this.toolStripDropDownButtonUser_Click);
            // 
            // toolStripDropDownButtonUserTO
            // 
            this.toolStripDropDownButtonUserTO.Enabled = false;
            this.toolStripDropDownButtonUserTO.Image = global::ACLMaster.Properties.Resources.user_own;
            this.toolStripDropDownButtonUserTO.Name = "toolStripDropDownButtonUserTO";
            this.toolStripDropDownButtonUserTO.Size = new System.Drawing.Size(159, 22);
            this.toolStripDropDownButtonUserTO.Text = "Take Ownership";
            // 
            // toolStripDropDownButtonUserCO
            // 
            this.toolStripDropDownButtonUserCO.Enabled = false;
            this.toolStripDropDownButtonUserCO.Image = global::ACLMaster.Properties.Resources.user_select;
            this.toolStripDropDownButtonUserCO.Name = "toolStripDropDownButtonUserCO";
            this.toolStripDropDownButtonUserCO.Size = new System.Drawing.Size(159, 22);
            this.toolStripDropDownButtonUserCO.Text = "Select Owner";
            this.toolStripDropDownButtonUserCO.Click += new System.EventHandler(this.toolStripDropDownButtonUserCO_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Enabled = false;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(23, 45);
            this.toolStripButton7.Text = "toolStripButton7";
            this.toolStripButton7.Visible = false;
            // 
            // toolStripDropDownButtonTools
            // 
            this.toolStripDropDownButtonTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyFilePermissionAsTextToolStripMenuItem,
            this.copyPermissionsAsMarkupToolStripMenuItem});
            this.toolStripDropDownButtonTools.Enabled = false;
            this.toolStripDropDownButtonTools.Image = global::ACLMaster.Properties.Resources.tool;
            this.toolStripDropDownButtonTools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonTools.Name = "toolStripDropDownButtonTools";
            this.toolStripDropDownButtonTools.Size = new System.Drawing.Size(69, 45);
            this.toolStripDropDownButtonTools.Text = "Tools";
            // 
            // copyFilePermissionAsTextToolStripMenuItem
            // 
            this.copyFilePermissionAsTextToolStripMenuItem.Enabled = false;
            this.copyFilePermissionAsTextToolStripMenuItem.Image = global::ACLMaster.Properties.Resources.text;
            this.copyFilePermissionAsTextToolStripMenuItem.Name = "copyFilePermissionAsTextToolStripMenuItem";
            this.copyFilePermissionAsTextToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.copyFilePermissionAsTextToolStripMenuItem.Text = "Copy permissions as text";
            this.copyFilePermissionAsTextToolStripMenuItem.Click += new System.EventHandler(this.copyFilePermissionAsTextToolStripMenuItem_Click);
            // 
            // copyPermissionsAsMarkupToolStripMenuItem
            // 
            this.copyPermissionsAsMarkupToolStripMenuItem.Enabled = false;
            this.copyPermissionsAsMarkupToolStripMenuItem.Image = global::ACLMaster.Properties.Resources.markup;
            this.copyPermissionsAsMarkupToolStripMenuItem.Name = "copyPermissionsAsMarkupToolStripMenuItem";
            this.copyPermissionsAsMarkupToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.copyPermissionsAsMarkupToolStripMenuItem.Text = "Copy permissions as markdown";
            this.copyPermissionsAsMarkupToolStripMenuItem.Click += new System.EventHandler(this.copyPermissionsAsMarkupToolStripMenuItem_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Enabled = false;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 45);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStripDropDownButtonSettings
            // 
            this.toolStripDropDownButtonSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.elevatePermissionsToolStripMenuItem,
            this.rerteiveDomainInformationToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.toolStripDropDownButtonSettings.Image = global::ACLMaster.Properties.Resources.settings;
            this.toolStripDropDownButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonSettings.Name = "toolStripDropDownButtonSettings";
            this.toolStripDropDownButtonSettings.Size = new System.Drawing.Size(82, 45);
            this.toolStripDropDownButtonSettings.Text = "Settings";
            this.toolStripDropDownButtonSettings.Click += new System.EventHandler(this.toolStripDropDownButtonSettings_Click);
            // 
            // elevatePermissionsToolStripMenuItem
            // 
            this.elevatePermissionsToolStripMenuItem.Image = global::ACLMaster.Properties.Resources.up;
            this.elevatePermissionsToolStripMenuItem.Name = "elevatePermissionsToolStripMenuItem";
            this.elevatePermissionsToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.elevatePermissionsToolStripMenuItem.Text = "Elevate permissions";
            this.elevatePermissionsToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // rerteiveDomainInformationToolStripMenuItem
            // 
            this.rerteiveDomainInformationToolStripMenuItem.Image = global::ACLMaster.Properties.Resources.AD;
            this.rerteiveDomainInformationToolStripMenuItem.Name = "rerteiveDomainInformationToolStripMenuItem";
            this.rerteiveDomainInformationToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.rerteiveDomainInformationToolStripMenuItem.Text = "Retreive Groups and Users";
            this.rerteiveDomainInformationToolStripMenuItem.Click += new System.EventHandler(this.rerteiveDomainInformationToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::ACLMaster.Properties.Resources.settings;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem1_Click);
            // 
            // listViewSecurables
            // 
            this.listViewSecurables.AllColumns.Add(this.columnSecurablesIcon);
            this.listViewSecurables.AllColumns.Add(this.columnName);
            this.listViewSecurables.AllColumns.Add(this.columnLastUpdate);
            this.listViewSecurables.AllColumns.Add(this.columnOwner);
            this.listViewSecurables.AllColumns.Add(this.columnACL);
            this.listViewSecurables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnSecurablesIcon,
            this.columnName,
            this.columnLastUpdate,
            this.columnOwner});
            this.listViewSecurables.ContextMenuStrip = this.contextMenuFiles;
            this.listViewSecurables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewSecurables.FullRowSelect = true;
            this.listViewSecurables.HideSelection = false;
            this.listViewSecurables.LargeImageList = this.imageList;
            this.listViewSecurables.Location = new System.Drawing.Point(0, 0);
            this.listViewSecurables.Name = "listViewSecurables";
            this.listViewSecurables.Size = new System.Drawing.Size(758, 379);
            this.listViewSecurables.SmallImageList = this.imageList;
            this.listViewSecurables.TabIndex = 0;
            this.listViewSecurables.UseCompatibleStateImageBehavior = false;
            this.listViewSecurables.View = System.Windows.Forms.View.Details;
            this.listViewSecurables.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listViewSecurables.DoubleClick += new System.EventHandler(this.listViewSecurables_DoubleClick);
            this.listViewSecurables.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewSecurables_KeyDown);
            this.listViewSecurables.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewSecurables_MouseDoubleClick);
            this.listViewSecurables.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewSecurables_MouseDown);
            // 
            // columnSecurablesIcon
            // 
            this.columnSecurablesIcon.CellPadding = null;
            this.columnSecurablesIcon.MaximumWidth = 20;
            this.columnSecurablesIcon.MinimumWidth = 20;
            this.columnSecurablesIcon.Sortable = false;
            this.columnSecurablesIcon.Text = "";
            this.columnSecurablesIcon.Width = 20;
            // 
            // columnName
            // 
            this.columnName.AspectName = "name";
            this.columnName.CellPadding = null;
            this.columnName.FillsFreeSpace = true;
            this.columnName.Text = "Name";
            // 
            // columnLastUpdate
            // 
            this.columnLastUpdate.AspectName = "lastUpdate";
            this.columnLastUpdate.CellPadding = null;
            this.columnLastUpdate.MinimumWidth = 40;
            this.columnLastUpdate.Text = "Last Update";
            this.columnLastUpdate.Width = 130;
            // 
            // columnOwner
            // 
            this.columnOwner.AspectName = "owner";
            this.columnOwner.CellPadding = null;
            this.columnOwner.FillsFreeSpace = true;
            this.columnOwner.Text = "Owner";
            // 
            // columnACL
            // 
            this.columnACL.CellPadding = null;
            this.columnACL.DisplayIndex = 3;
            this.columnACL.IsVisible = false;
            // 
            // contextMenuFiles
            // 
            this.contextMenuFiles.Name = "contextMenuFiles";
            this.contextMenuFiles.Size = new System.Drawing.Size(61, 4);
            this.contextMenuFiles.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuFiles_Opening);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "lock.png");
            this.imageList.Images.SetKeyName(1, "OK.png");
            this.imageList.Images.SetKeyName(2, "exclamation.png");
            this.imageList.Images.SetKeyName(3, "file.png");
            this.imageList.Images.SetKeyName(4, "folder.png");
            this.imageList.Images.SetKeyName(5, "hdd.png");
            this.imageList.Images.SetKeyName(6, "USB.png");
            this.imageList.Images.SetKeyName(7, "CD.png");
            this.imageList.Images.SetKeyName(8, "network.png");
            this.imageList.Images.SetKeyName(9, "folder_open.png");
            this.imageList.Images.SetKeyName(10, "file_grey.png");
            this.imageList.Images.SetKeyName(11, "folder_grey.png");
            // 
            // splitContainerBottomRight
            // 
            this.splitContainerBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerBottomRight.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerBottomRight.Location = new System.Drawing.Point(0, 0);
            this.splitContainerBottomRight.Name = "splitContainerBottomRight";
            this.splitContainerBottomRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerBottomRight.Panel1
            // 
            this.splitContainerBottomRight.Panel1.Controls.Add(this.checkBoxInheritance);
            this.splitContainerBottomRight.Panel1.Controls.Add(this.toolStripBottom);
            // 
            // splitContainerBottomRight.Panel2
            // 
            this.splitContainerBottomRight.Panel2.Controls.Add(this.tabControl1);
            this.splitContainerBottomRight.Size = new System.Drawing.Size(758, 295);
            this.splitContainerBottomRight.TabIndex = 1;
            // 
            // checkBoxInheritance
            // 
            this.checkBoxInheritance.AutoSize = true;
            this.checkBoxInheritance.BackColor = System.Drawing.SystemColors.Control;
            this.checkBoxInheritance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxInheritance.Location = new System.Drawing.Point(389, 17);
            this.checkBoxInheritance.Name = "checkBoxInheritance";
            this.checkBoxInheritance.Size = new System.Drawing.Size(167, 17);
            this.checkBoxInheritance.TabIndex = 1;
            this.checkBoxInheritance.Text = "Securable inherits permissions";
            this.checkBoxInheritance.UseVisualStyleBackColor = false;
            this.checkBoxInheritance.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.checkBoxInheritance.Click += new System.EventHandler(this.checkBoxInheritance_Click);
            // 
            // toolStripBottom
            // 
            this.toolStripBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripBottom.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripBottom.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBottomButtonAddRule,
            this.toolStripButton8,
            this.toolStripBottomButtonDelete,
            this.toolStripButton3,
            this.toolStripBottomButtonApply,
            this.toolStripButton6});
            this.toolStripBottom.Location = new System.Drawing.Point(0, 0);
            this.toolStripBottom.Name = "toolStripBottom";
            this.toolStripBottom.Size = new System.Drawing.Size(758, 50);
            this.toolStripBottom.TabIndex = 0;
            this.toolStripBottom.Text = "toolStrip1";
            this.toolStripBottom.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked_1);
            // 
            // toolStripBottomButtonAddRule
            // 
            this.toolStripBottomButtonAddRule.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.denyToolStripMenuItem,
            this.allowToolStripMenuItem});
            this.toolStripBottomButtonAddRule.Image = global::ACLMaster.Properties.Resources.add;
            this.toolStripBottomButtonAddRule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBottomButtonAddRule.Name = "toolStripBottomButtonAddRule";
            this.toolStripBottomButtonAddRule.Size = new System.Drawing.Size(88, 47);
            this.toolStripBottomButtonAddRule.Text = "Add Rule";
            this.toolStripBottomButtonAddRule.DropDownOpening += new System.EventHandler(this.toolStripBottomButtonAddRule_DropDownOpening);
            this.toolStripBottomButtonAddRule.Click += new System.EventHandler(this.toolStripBottomButtonAddRule_Click);
            // 
            // denyToolStripMenuItem
            // 
            this.denyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullControlToolStripMenuItem,
            this.modifyToolStripMenuItem,
            this.readAndExecuteToolStripMenuItem,
            this.listToolStripMenuItem,
            this.readToolStripMenuItem,
            this.writeToolStripMenuItem});
            this.denyToolStripMenuItem.Name = "denyToolStripMenuItem";
            this.denyToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.denyToolStripMenuItem.Text = "Deny";
            // 
            // fullControlToolStripMenuItem
            // 
            this.fullControlToolStripMenuItem.Name = "fullControlToolStripMenuItem";
            this.fullControlToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.fullControlToolStripMenuItem.Text = "Full Control";
            this.fullControlToolStripMenuItem.Click += new System.EventHandler(this.fullControlToolStripMenuItem_Click);
            // 
            // modifyToolStripMenuItem
            // 
            this.modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
            this.modifyToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.modifyToolStripMenuItem.Text = "Modify";
            // 
            // readAndExecuteToolStripMenuItem
            // 
            this.readAndExecuteToolStripMenuItem.Name = "readAndExecuteToolStripMenuItem";
            this.readAndExecuteToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.readAndExecuteToolStripMenuItem.Text = "Read and Execute";
            // 
            // listToolStripMenuItem
            // 
            this.listToolStripMenuItem.Name = "listToolStripMenuItem";
            this.listToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.listToolStripMenuItem.Text = "List";
            // 
            // readToolStripMenuItem
            // 
            this.readToolStripMenuItem.Name = "readToolStripMenuItem";
            this.readToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.readToolStripMenuItem.Text = "Read";
            // 
            // writeToolStripMenuItem
            // 
            this.writeToolStripMenuItem.Name = "writeToolStripMenuItem";
            this.writeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.writeToolStripMenuItem.Text = "Write";
            // 
            // allowToolStripMenuItem
            // 
            this.allowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullControlToolStripMenuItem1,
            this.modifyToolStripMenuItem1,
            this.readAndExecuteToolStripMenuItem1,
            this.listToolStripMenuItem1,
            this.readToolStripMenuItem1,
            this.writeToolStripMenuItem1});
            this.allowToolStripMenuItem.Name = "allowToolStripMenuItem";
            this.allowToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.allowToolStripMenuItem.Text = "Allow";
            // 
            // fullControlToolStripMenuItem1
            // 
            this.fullControlToolStripMenuItem1.Name = "fullControlToolStripMenuItem1";
            this.fullControlToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.fullControlToolStripMenuItem1.Text = "Full Control";
            // 
            // modifyToolStripMenuItem1
            // 
            this.modifyToolStripMenuItem1.Name = "modifyToolStripMenuItem1";
            this.modifyToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.modifyToolStripMenuItem1.Text = "Modify";
            // 
            // readAndExecuteToolStripMenuItem1
            // 
            this.readAndExecuteToolStripMenuItem1.Name = "readAndExecuteToolStripMenuItem1";
            this.readAndExecuteToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.readAndExecuteToolStripMenuItem1.Text = "Read and Execute";
            // 
            // listToolStripMenuItem1
            // 
            this.listToolStripMenuItem1.Name = "listToolStripMenuItem1";
            this.listToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.listToolStripMenuItem1.Text = "List";
            // 
            // readToolStripMenuItem1
            // 
            this.readToolStripMenuItem1.Name = "readToolStripMenuItem1";
            this.readToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.readToolStripMenuItem1.Text = "Read ";
            // 
            // writeToolStripMenuItem1
            // 
            this.writeToolStripMenuItem1.Name = "writeToolStripMenuItem1";
            this.writeToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.writeToolStripMenuItem1.Text = "Write";
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Enabled = false;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 47);
            this.toolStripButton8.Text = "toolStripButton3";
            // 
            // toolStripBottomButtonDelete
            // 
            this.toolStripBottomButtonDelete.Image = global::ACLMaster.Properties.Resources.delete;
            this.toolStripBottomButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBottomButtonDelete.Name = "toolStripBottomButtonDelete";
            this.toolStripBottomButtonDelete.Size = new System.Drawing.Size(90, 47);
            this.toolStripBottomButtonDelete.Text = "Delete Rule";
            this.toolStripBottomButtonDelete.Click += new System.EventHandler(this.toolStripBottomButtonDelete_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Enabled = false;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 47);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // toolStripBottomButtonApply
            // 
            this.toolStripBottomButtonApply.Image = global::ACLMaster.Properties.Resources.OK;
            this.toolStripBottomButtonApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripBottomButtonApply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBottomButtonApply.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.toolStripBottomButtonApply.Name = "toolStripBottomButtonApply";
            this.toolStripBottomButtonApply.Size = new System.Drawing.Size(133, 47);
            this.toolStripBottomButtonApply.Text = "0 Changes Pending";
            this.toolStripBottomButtonApply.Click += new System.EventHandler(this.toolStripBottomButtonApply_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(24, 47);
            this.toolStripButton6.Text = "toolStripButton6";
            this.toolStripButton6.Visible = false;
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click_1);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(758, 241);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listViewPermissions);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(750, 215);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Show all permissions";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listViewPermissions
            // 
            this.listViewPermissions.AllColumns.Add(this.columnPermissionImage);
            this.listViewPermissions.AllColumns.Add(this.columnPrincipal);
            this.listViewPermissions.AllColumns.Add(this.columnPermission);
            this.listViewPermissions.AllColumns.Add(this.columnGrantStatus);
            this.listViewPermissions.AllColumns.Add(this.columnInherited);
            this.listViewPermissions.AllColumns.Add(this.columnPropagates);
            this.listViewPermissions.AllColumns.Add(this.columnInheritanceFlags);
            this.listViewPermissions.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClick;
            this.listViewPermissions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPermissionImage,
            this.columnPrincipal,
            this.columnPermission,
            this.columnGrantStatus,
            this.columnInherited,
            this.columnPropagates,
            this.columnInheritanceFlags});
            this.listViewPermissions.ContextMenuStrip = this.contextMenuPermissions;
            this.listViewPermissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPermissions.EmptyListMsg = "No Access Control Rules to show";
            this.listViewPermissions.FullRowSelect = true;
            this.listViewPermissions.HeaderWordWrap = true;
            this.listViewPermissions.HideSelection = false;
            this.listViewPermissions.LargeImageList = this.imageList;
            this.listViewPermissions.Location = new System.Drawing.Point(3, 3);
            this.listViewPermissions.Name = "listViewPermissions";
            this.listViewPermissions.ShowImagesOnSubItems = true;
            this.listViewPermissions.Size = new System.Drawing.Size(744, 209);
            this.listViewPermissions.SmallImageList = this.imageList;
            this.listViewPermissions.TabIndex = 0;
            this.listViewPermissions.UseCellFormatEvents = true;
            this.listViewPermissions.UseCompatibleStateImageBehavior = false;
            this.listViewPermissions.UseSubItemCheckBoxes = true;
            this.listViewPermissions.View = System.Windows.Forms.View.Details;
            this.listViewPermissions.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.listViewPermissions_FormatCell);
            this.listViewPermissions.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.listViewPermissions_FormatRow);
            this.listViewPermissions.SelectedIndexChanged += new System.EventHandler(this.listViewPermissions_SelectedIndexChanged);
            this.listViewPermissions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewPermissions_KeyDown);
            this.listViewPermissions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewPermissions_MouseDown);
            // 
            // columnPermissionImage
            // 
            this.columnPermissionImage.CellPadding = null;
            this.columnPermissionImage.MaximumWidth = 20;
            this.columnPermissionImage.MinimumWidth = 20;
            this.columnPermissionImage.Sortable = false;
            this.columnPermissionImage.Text = "";
            this.columnPermissionImage.Width = 20;
            // 
            // columnPrincipal
            // 
            this.columnPrincipal.AspectName = "IdentityReference";
            this.columnPrincipal.CellPadding = null;
            this.columnPrincipal.FillsFreeSpace = true;
            this.columnPrincipal.IsEditable = false;
            this.columnPrincipal.MinimumWidth = 250;
            this.columnPrincipal.Text = "Principal";
            this.columnPrincipal.Width = 250;
            // 
            // columnPermission
            // 
            this.columnPermission.CellPadding = null;
            this.columnPermission.FillsFreeSpace = true;
            this.columnPermission.Text = "Permission";
            this.columnPermission.Width = 92;
            // 
            // columnGrantStatus
            // 
            this.columnGrantStatus.CellPadding = null;
            this.columnGrantStatus.MaximumWidth = 80;
            this.columnGrantStatus.MinimumWidth = 80;
            this.columnGrantStatus.Text = "Grant Status";
            this.columnGrantStatus.Width = 80;
            // 
            // columnInherited
            // 
            this.columnInherited.CellPadding = null;
            this.columnInherited.IsEditable = false;
            this.columnInherited.Text = "Inherited";
            // 
            // columnPropagates
            // 
            this.columnPropagates.CellPadding = null;
            this.columnPropagates.MaximumWidth = 80;
            this.columnPropagates.MinimumWidth = 80;
            this.columnPropagates.Text = "Propagates";
            this.columnPropagates.Width = 80;
            // 
            // columnInheritanceFlags
            // 
            this.columnInheritanceFlags.CellPadding = null;
            this.columnInheritanceFlags.MaximumWidth = 100;
            this.columnInheritanceFlags.MinimumWidth = 100;
            this.columnInheritanceFlags.Text = "Inheritance Flags";
            this.columnInheritanceFlags.Width = 100;
            // 
            // contextMenuPermissions
            // 
            this.contextMenuPermissions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CO,
            this.toolStripSeparator2,
            this.CP,
            this.toolStripSeparator1,
            this.SP,
            this.toolStripSeparator3,
            this.CG,
            this.toolStripSeparator4,
            this.PF});
            this.contextMenuPermissions.Name = "contextMenuStrip1";
            this.contextMenuPermissions.ShowImageMargin = false;
            this.contextMenuPermissions.Size = new System.Drawing.Size(153, 138);
            this.contextMenuPermissions.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuPermissions_Opening);
            this.contextMenuPermissions.Paint += new System.Windows.Forms.PaintEventHandler(this.contextMenuPermissions_Paint);
            // 
            // CO
            // 
            this.CO.Name = "CO";
            this.CO.Size = new System.Drawing.Size(152, 22);
            this.CO.Text = "Change User";
            this.CO.DropDownOpening += new System.EventHandler(this.CO_DropDownOpening);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // CP
            // 
            this.CP.Name = "CP";
            this.CP.Size = new System.Drawing.Size(152, 22);
            this.CP.Text = "Change Permission";
            this.CP.DropDownOpening += new System.EventHandler(this.CP_DropDownOpening);
            this.CP.Click += new System.EventHandler(this.CP_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // SP
            // 
            this.SP.Name = "SP";
            this.SP.Size = new System.Drawing.Size(152, 22);
            this.SP.Text = "Special Permissions";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // CG
            // 
            this.CG.Name = "CG";
            this.CG.Size = new System.Drawing.Size(152, 22);
            this.CG.Text = "Change Grant Type";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // PF
            // 
            this.PF.Name = "PF";
            this.PF.Size = new System.Drawing.Size(152, 22);
            this.PF.Text = "Propagation Flags";
            // 
            // columnACLHash
            // 
            this.columnACLHash.AspectName = "ACLHash";
            this.columnACLHash.CellPadding = null;
            this.columnACLHash.FillsFreeSpace = true;
            this.columnACLHash.IsVisible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1112, 730);
            this.Controls.Add(this.splitContainerForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ACL Master - Managing Windows (R) Permissions made simple";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.splitContainerForm.Panel1.ResumeLayout(false);
            this.splitContainerForm.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerForm)).EndInit();
            this.splitContainerForm.ResumeLayout(false);
            this.splitContainerLeft.Panel1.ResumeLayout(false);
            this.splitContainerLeft.Panel1.PerformLayout();
            this.splitContainerLeft.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeft)).EndInit();
            this.splitContainerLeft.ResumeLayout(false);
            this.toolStripLeft.ResumeLayout(false);
            this.toolStripLeft.PerformLayout();
            this.splitContainerRight.Panel1.ResumeLayout(false);
            this.splitContainerRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).EndInit();
            this.splitContainerRight.ResumeLayout(false);
            this.splitContainerTopRight.Panel1.ResumeLayout(false);
            this.splitContainerTopRight.Panel1.PerformLayout();
            this.splitContainerTopRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTopRight)).EndInit();
            this.splitContainerTopRight.ResumeLayout(false);
            this.toolStripRight.ResumeLayout(false);
            this.toolStripRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listViewSecurables)).EndInit();
            this.splitContainerBottomRight.Panel1.ResumeLayout(false);
            this.splitContainerBottomRight.Panel1.PerformLayout();
            this.splitContainerBottomRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBottomRight)).EndInit();
            this.splitContainerBottomRight.ResumeLayout(false);
            this.toolStripBottom.ResumeLayout(false);
            this.toolStripBottom.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listViewPermissions)).EndInit();
            this.contextMenuPermissions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerForm;
        private System.Windows.Forms.SplitContainer splitContainerRight;
        private System.Windows.Forms.SplitContainer splitContainerTopRight;
        private System.Windows.Forms.ToolStrip toolStripRight;
        private System.Windows.Forms.ToolStripButton toolStripRightButtonRefresh;
        private System.Windows.Forms.ToolStripButton toolStripRightButtonCopy;
        private System.Windows.Forms.SplitContainer splitContainerLeft;
        private System.Windows.Forms.TreeView treeViewFolder;
        private System.Windows.Forms.ToolStripButton toolStripRightButtonGroup;
        private System.Windows.Forms.SplitContainer splitContainerBottomRight;
        private System.Windows.Forms.ContextMenuStrip contextMenuPermissions;
        private System.Windows.Forms.ToolStripButton toolStripRightButtonPaste;
        private System.Windows.Forms.ToolStrip toolStripBottom;
        private System.Windows.Forms.ToolStrip toolStripLeft;
        private System.Windows.Forms.ToolStripButton toolStripLeftButtonRefresh;
        private System.Windows.Forms.ContextMenuStrip contextMenuFiles;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripBottomButtonDelete;
        private System.Windows.Forms.CheckBox checkBoxInheritance;
        private System.Windows.Forms.ToolStripButton toolStripBottomButtonApply;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonUser;
        private System.Windows.Forms.ToolStripMenuItem toolStripDropDownButtonUserTO;
        private System.Windows.Forms.ToolStripMenuItem toolStripDropDownButtonUserCO;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonTools;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem copyFilePermissionAsTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPermissionsAsMarkupToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonSettings;
        private System.Windows.Forms.ToolStripMenuItem elevatePermissionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rerteiveDomainInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        public System.Windows.Forms.ToolStripMenuItem CP;
        private System.Windows.Forms.ToolStripMenuItem SP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem CO;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem CG;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem PF;
        private System.Windows.Forms.ToolStripDropDownButton toolStripBottomButtonAddRule;
        private System.Windows.Forms.ToolStripMenuItem denyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullControlToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem writeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readAndExecuteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem readAndExecuteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private BrightIdeasSoftware.OLVColumn columnACLHash;
        private BrightIdeasSoftware.ObjectListView listViewSecurables;
        private BrightIdeasSoftware.OLVColumn columnName;
        private BrightIdeasSoftware.OLVColumn columnLastUpdate;
        private BrightIdeasSoftware.OLVColumn columnOwner;
        private BrightIdeasSoftware.OLVColumn columnACL;
        private BrightIdeasSoftware.ObjectListView listViewPermissions;
        private BrightIdeasSoftware.OLVColumn columnPrincipal;
        private BrightIdeasSoftware.OLVColumn columnPermission;
        private BrightIdeasSoftware.OLVColumn columnGrantStatus;
        private BrightIdeasSoftware.OLVColumn columnInherited;
        private BrightIdeasSoftware.OLVColumn columnPropagates;
        private BrightIdeasSoftware.OLVColumn columnInheritanceFlags;
        private System.Windows.Forms.ImageList imageList;
        private BrightIdeasSoftware.OLVColumn columnPermissionImage;
        private BrightIdeasSoftware.OLVColumn columnSecurablesIcon;
        private BrightIdeasSoftware.BaseRenderer baseRenderer1;


    }
}

