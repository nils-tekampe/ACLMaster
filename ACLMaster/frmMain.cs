
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
using ACLMaster.Properties;
using BrightIdeasSoftware;
using log4net;

namespace ACLMaster
{
    public partial class frmMain : Form
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly List<FileSystemAccessRule> deletedRules;
        public bool _canAllSeucurablesBeRead;

        //private List<ListViewItem> tmpListViewItems;

        private bool _canPermissionsBeChanged;

        private bool _closeSpecialPermissionMenu;

        private bool _isCopySecurablePossible;
        public bool _isDeleteACEPossible;
        public bool _isDeleteACEPossibleBasedOnSecurable;
        private bool _isModifyPermissionPossible;
        private bool _isPasteSecurablePossible;
        public bool _isTakeOwnershipPossible;
        private bool _unifomSecurableType;
        private bool _uniformType;

        private ListViewColumnSorter lvwColumnSorter;
        public bool uncomittedChanges;

        private PropagationFlags unifiedFlags;
        private FileSystemRights unifiedRights;

        public frmMain()
        {
            log.Error("Initializing fromMain()");

            InitializeComponent();

            Global.init();

            prepareContextMenu();

            deletedRules = new List<FileSystemAccessRule>();

            //Startup Options

            if (Global.adminMode)
            {
                Text = "ACL Master - Managing Windows (R) Permissions made simple - Admin mode";
            }
            else
            {
                Text = "ACL Master - Managing Windows (R) Permissions made simple";
            }

            populateDriveBox();

            string[] args = Environment.GetCommandLineArgs();

            if (Global.drives.getDriveList().Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }

            _closeSpecialPermissionMenu = false;
            uncomittedChanges = false;


            updateButtons();

            prepareListViewSecurables();
            prepareListViewPermissons();

            //       Global.listedPermissions.CollectionChanged += new NotifyCollectionChangedEventHandler(PermissionsChanged);
        }

        //private void SinglePermissionsChanged()
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        ///     Verarbeitet eine Befehlstaste.
        /// </summary>
        /// <param name="msg">
        ///     Eine als Verweis übergebene <see cref="T:System.Windows.Forms.Message" />, die die zu verarbeitende
        ///     Win32-Meldung darstellt.
        /// </param>
        /// <param name="keyData">
        ///     Einer der <see cref="T:System.Windows.Forms.Keys" />-Werte, die die zu verarbeitende Taste
        ///     darstellen.
        /// </param>
        /// <returns>
        ///     true, wenn der Tastaturanschlag verarbeitet und auf das Steuerelement angewendet wurde, andernfalls false, um eine
        ///     weitere Verarbeitung zu ermöglichen.
        ///     In my implementation it handles all the specific shortcuts. No other functions elsewhere should be used for
        ///     shortcuts
        /// </returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (ActiveControl == splitContainerForm)
            {
                if (splitContainerForm.ActiveControl == splitContainerRight)

                {
                    if (splitContainerRight.ActiveControl == splitContainerTopRight)
                    {
                        //here is to handle all the shortcuts for securables  
                    }
                    else if (splitContainerRight.ActiveControl == splitContainerBottomRight)
                    {
//here is to handle all the shortcuts for permissions   }
                    }
                    else if (splitContainerForm.ActiveControl == splitContainerLeft)
                    {
                        //here is only the F5 case
                    }
                }
            }
            //if ((e.KeyValue == 46) && _isDeleteACEPossibleBasedOnSecurable)
            //    deleteSelectedPermissions();


            //if (keyData == (Keys.Control  | Keys.C))
            //{
            //    // Alt+F pressed
            //    MessageBox.Show("asder");
            //    return true;
            //}

            //if (e.Control && (e.KeyCode == Keys.C))
            //{
            //    MessageBox.Show("copy");

            //    if (_isCopySecurablePossible)
            //        copyACL();


            //}
            //else if (e.Control && e.KeyCode == Keys.V)
            //{
            //    if (_isPasteSecurablePossible)
            //        PasteAcl();
            //}
            //else if (e.Control && e.KeyCode == Keys.M)
            //{
            //    CopyPermissionsAsMarkdown();
            //}
            //else if (e.Control && e.KeyCode == Keys.T)
            //{
            //    CopyPermissionsAsText();
            //}
            //if (e.KeyCode == Keys.F5)
            //{
            //    updateSecurableViewCompletely();
            //}
            //else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.R)
            //{
            //    //reread Drives

            //    populateDriveBox();
            //    if (Global.drives.getDriveList().Count > 0)
            //    {
            //        comboBox1.SelectedIndex = 0;
            //    }
//        }
//            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.E)
//            {
//                if (Directory.Exists(((DirectoryInfo)treeViewFolder.SelectedNode.Tag).FullName))
//                    Process.Start("explorer.exe", ((DirectoryInfo)treeViewFolder.SelectedNode.Tag).FullName);
//            }
//            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.H)
//            {
//                Global.elevatePrivileges();
//            }
//            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.P)
//            {
//                OpenSettings();
//            }
//            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.O)
//            {
//                OpenFolder();
//            }
//            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.G)
//            {
//                listViewSecurables.ShowGroups = !listViewSecurables.ShowGroups;
//            }

//#if DEBUG
//            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D)
//                Debugger.Launch();
//#endif


            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void refreshPermissions(object sender, EventArgs e)
        {
            foreach (FileSystemAccessRuleExtended fsarext in Global.listedPermissions)
            {
                fsarext.selected = false;
            }

            foreach (OLVListItem item in listViewPermissions.SelectedItems)
            {
                ((FileSystemAccessRuleExtended) item.RowObject).selected = true;
            }
            listViewPermissions.SetObjects(Global.listedPermissions);

            UpdateCommitButton();

            foreach (OLVListItem item in listViewPermissions.Items)
            {
                if (((FileSystemAccessRuleExtended) item.RowObject).selected)
                    item.Selected = true;
            }
        }

        private void PermissionsChanged(object sender, NotifyCollectionChangedEventWithDetailsArgs e)
        {
            listViewPermissions.RefreshObject(e.changedObject);
            UpdateCommitButton();
        }

        private bool isDeleteACEPossibleBasedOnSecurable()
        {
            //optmizized

            if (!_isModifyPermissionPossible)
                return false;

            foreach (OLVListItem olvi in listViewSecurables.SelectedItems)
            {
                if (((Securable) (olvi.RowObject)).inheritsParentPermissions)
                    return false;
            }

            return true;
        }

        private bool isDeleteACEPossibleBasedOnACE()
        {
            if (listViewPermissions.SelectedItems.Count == 0)
                return false;

            if (!_isModifyPermissionPossible)
                return false;

            foreach (OLVListItem olvi in listViewPermissions.SelectedItems)
            {
                var rule = (FileSystemAccessRuleExtended) olvi.RowObject;
                if ((rule.fsar.IsInherited) && (!rule.added))
                    return false;
            }
            return true;
        }

        private void updateButtons()
        {
            _isModifyPermissionPossible = isModifyPermissionPossible();
            _isCopySecurablePossible = (listViewSecurables.SelectedItems.Count == 1);
            _isPasteSecurablePossible = isPasteSecurablePossible();
            _isDeleteACEPossibleBasedOnSecurable = isDeleteACEPossibleBasedOnSecurable();
            _isDeleteACEPossible = isDeleteACEPossibleBasedOnACE();
            _isTakeOwnershipPossible = isTakeOwnershipPossible();
            _canAllSeucurablesBeRead = canAllSeucurablesBeRead();

            toolStripRightButtonCopy.Enabled = _isCopySecurablePossible;
            toolStripRightButtonPaste.Enabled = _isPasteSecurablePossible;

            toolStripDropDownButtonUser.Enabled = toolStripDropDownButtonUserTO.Enabled = toolStripDropDownButtonUserCO.Enabled = _isTakeOwnershipPossible;

            copyFilePermissionAsTextToolStripMenuItem.Enabled = _canAllSeucurablesBeRead;
            copyPermissionsAsMarkupToolStripMenuItem.Enabled = _canAllSeucurablesBeRead;
            toolStripDropDownButtonTools.Enabled = copyFilePermissionAsTextToolStripMenuItem.Enabled || copyPermissionsAsMarkupToolStripMenuItem.Enabled;

            checkBoxInheritance.Enabled = _isModifyPermissionPossible;

            if (listViewSecurables.SelectedItems.Count > 0)
            {
                copyFilePermissionAsTextToolStripMenuItem.Enabled = true;
                copyPermissionsAsMarkupToolStripMenuItem.Enabled = true;
                toolStripDropDownButtonTools.Enabled = true;
            }
            else
            {
                copyFilePermissionAsTextToolStripMenuItem.Enabled = false;
                copyPermissionsAsMarkupToolStripMenuItem.Enabled = false;
                toolStripDropDownButtonTools.Enabled = false;
            }
          
            if (listViewSecurables.Items.Count  > 0)
            {
                toolStripRightButtonRefresh.Enabled = true;
            }
            else
            {
                toolStripRightButtonRefresh.Enabled = false;
            }

            //and the two buttons for ACEs
            toolStripBottomButtonAddRule.Enabled = _isModifyPermissionPossible;
            toolStripBottomButtonDelete.Enabled = _isDeleteACEPossible;
            checkBoxInheritance.Enabled = _isModifyPermissionPossible;

            //   updateCommitButton();
        }

        private bool canAllSeucurablesBeRead()
        {
            bool returnValue = true;

            if (listViewSecurables.SelectedItems.Count > 0)
            {
                foreach (OLVListItem olvi in listViewSecurables.SelectedItems)
                {
                    if (!(((Securable) (olvi.RowObject)).hasUserThisRight(FileSystemRights.ReadPermissions)))
                        return false;
                }
            }
            else
            {
                return false;
            }

            return returnValue;
        }

        private bool isTakeOwnershipPossible()
        {
            if (listViewSecurables.SelectedItems.Count > 0)
            {
                foreach (OLVListItem olvi in listViewSecurables.SelectedItems)
                {
                    if (!(((Securable) (olvi.RowObject)).canOwnerBeChanged()))
                        return false;
                }

                return true;
            }
            return false;
        }

        private bool isModifyPermissionPossible()
        {
            if (listViewSecurables.SelectedItems.Count > 0)
            {
                foreach (OLVListItem olvi in listViewSecurables.SelectedItems)
                {
                    if (((Securable) (olvi.RowObject)).locked)
                        return false;
                }

                return true;
            }
            return false;
        }

        private void populateDriveBox()
        {
            Global.drives.readDrives();

            try
            {
                comboBox1.Items.Clear();

                comboBox1.DrawMode = DrawMode.OwnerDrawFixed;

                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox1.DrawItem += comboBox1_DrawItem;

                foreach (Drive drive in Global.drives.getDriveList())
                {
                    comboBox1.Items.Add(drive.displayName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in populateDriveBox " + ex.Message);
            }
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;

            e.DrawBackground();

            e.Graphics.DrawImage(Global.drives.getDrive(e.Index).bitmap, 3, e.Bounds.Top, 16, 16);

            e.Graphics.DrawString(comboBox1.Items[e.Index].ToString(), comboBox1.Font, Brushes.Black, 16 + 2, e.Bounds.Top);
            e.DrawFocusRectangle();
        }

        private void populateTreeView(string _path)
        {
            try
            {
                treeViewFolder.Nodes.Clear();

                var info = new DirectoryInfo(_path);
                

                var rootnode = new TreeNode(_path) {Tag = info, ImageIndex = 4};
                treeViewFolder.Nodes.Add(rootnode);

                FillChildNodes(rootnode);
                treeViewFolder.Nodes[0].Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in populate tree view " + ex.Message);
            }
        }

        private void FillChildNodes(TreeNode node)
        {
            try
            {
                var dirs = new DirectoryInfo(node.FullPath);
                foreach (DirectoryInfo dir in dirs.GetDirectories())
                {
                    //the following line will check that the current user has the permission to at least view the ACL

                    var tmpFolder = new Folder(dir.FullName);

                    if ((tmpFolder.shouldSecurableBeShown()))
                    {
                        //the following line makes sure that no reparse pointe like links are shown
                        if ((dir.Attributes & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
                        {
                            var newnode = new TreeNode(dir.Name);
                            newnode.Tag = dir;
                            node.Nodes.Add(newnode);
                            newnode.ImageIndex = 4; //folder image
                            newnode.Nodes.Add("*");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in FillChildNodes " + ex );
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "*")
            {
                e.Node.Nodes.Clear();
                FillChildNodes(e.Node);
            }
        }

        private void treeView1_NodeMouseClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeViewClickNode(e.Node);
        }

        private void treeViewClickNode(TreeNode node)
        {
            if (uncomittedChanges)
            {
                if (MessageBox.Show("You have uncomitted changes for files. Changing the folder will dispose those changes. Do you want to proceed? Please press OK to proceed and dispose the changes, otherwise press cancel", "Uncommited Changes", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;
            }


            //The rest is important to get all the ACLs cleared
            deletedRules.Clear();
            Global.listedPermissions.Clear();
            

            TreeNode newSelected = node;
            treeViewFolder.SelectedNode = newSelected;

            updateSecurableViewCompletely();
            buildPermissionView();
            UpdateCommitButton();
        }

        private void updateSecurableViewCompletely()
        {
            var _nodeDirInfo = (DirectoryInfo) treeViewFolder.SelectedNode.Tag;

            fillSecurableList(_nodeDirInfo);

            updateButtons();
            UpdateCommitButton();
        }

        private void fillSecurableList(DirectoryInfo nodeDirInfo)
        {
            Global.listedSecurables.Clear();
            var watch = new Stopwatch();
            watch.Start();

            //we put the folder itself into the list
            var tmp2 = new Folder(nodeDirInfo.FullName, ".");
            Global.listedSecurables.Add(tmp2);
            //createListViewItemBasedOnSecurable(tmp2);

            //and the parent
            if (nodeDirInfo.Parent != null)
            {
                tmp2 = new Folder(nodeDirInfo.Parent.FullName, "..");
                Global.listedSecurables.Add(tmp2);

                //   createSecurable(tmp2);

                // Global.securables.Add(nodeDirInfo.Parent.FullName, tmp2);
            }

            foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
            {
                try
                {
                    var tmpFolder = new Folder(dir.FullName);

                    if (tmpFolder.shouldSecurableBeShown())
                    {
                        Global.listedSecurables.Add(tmpFolder);

                        //   createSecurable(tmp);
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show("this should never happen");

                    //This exception is ignored
                }
            }

            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                try
                {
                    var tmpFile = new File(file.FullName);

                    if (tmpFile.shouldSecurableBeShown())
                    {
                        Global.listedSecurables.Add(tmpFile);

                        //createSecurable(tmp);
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show("this should never happen");

                    //This exception is ignored
                }
            }

            listViewSecurables.SetObjects(Global.listedSecurables);
        }

        private void prepareListViewSecurables()
        {
            listViewSecurables.ShowGroups = true;
            columnName.GroupKeyGetter = delegate(object rowObject) { return ((Securable) rowObject).ACLHash.ToString(); };

            columnName.GroupKeyToTitleConverter = delegate { return "tbd"; };

            listViewSecurables.AboutToCreateGroups += (s, args) =>
            {
                foreach (OLVGroup group in args.Groups)
                {
                    group.Header = group.Items[0].Text;
                }
            };

            columnName.AspectGetter = delegate(object sec)
            {
                var securable = (Securable) sec;

                if (!securable.isFile)
                    if (((Folder) securable).displayName == "")
                        return securable.name;
                    else
                        return ((Folder) securable).displayName;
                return securable.name;
            };

            columnSecurablesIcon.AspectGetter = delegate { return ""; };

            columnSecurablesIcon.AspectToStringConverter = delegate { return String.Empty; };

            columnSecurablesIcon.ImageGetter = delegate(object x)
            {
                if (x.GetType() == typeof (Folder))
                {
                    return "folder.png";
                }
                return "file.png";
            };

            Global.listedSecurables = new List<Securable>();
        }

        private void listViewPermissions_FormatRow(object sender, FormatRowEventArgs e)
        {
            //   Customer customer = (Customer)e.Model;
            // if (customer.Credit < 0)

            var fsarext = (FileSystemAccessRuleExtended) e.Item.RowObject;

            if (fsarext.added || fsarext.changed)
            {
                e.Item.BackColor = Color.LightGray;
            }
        }

        private void prepareListViewPermissons()
        {
            //   this.listViewPermissions.UseCellFormatEvents = true;

            //this.listViewPermissions.FormatRow += new Ce

            columnPrincipal.AspectGetter = delegate(object obj)
            {
                FileSystemAccessRule rule = ((FileSystemAccessRuleExtended) obj).fsar;
                ;

                string username;
                try
                {
                    username = rule.IdentityReference.Translate(typeof (NTAccount)).ToString();
                }
                catch (Exception e)
                {
                    username = rule.IdentityReference.ToString();
                }

                return username;
            };

            columnGrantStatus.AspectGetter = delegate(object obj)
            {
                FileSystemAccessRule rule = ((FileSystemAccessRuleExtended) obj).fsar;

                return rule.AccessControlType.ToString();
            };

            columnPermission.AspectGetter = delegate(object obj)
            {
                FileSystemAccessRule rule = ((FileSystemAccessRuleExtended) obj).fsar;

                return rule.FileSystemRights.ToString();
            };

            columnInherited.AspectGetter = delegate(object obj)
            {
                FileSystemAccessRule rule = ((FileSystemAccessRuleExtended) obj).fsar;
                return rule.IsInherited;
                // return rule.IsInherited.ToString();
            };
         

            columnPropagates.AspectGetter = delegate(object obj)
            {
                FileSystemAccessRule rule = ((FileSystemAccessRuleExtended) obj).fsar;

                return rule.PropagationFlags.ToString();
            };

            columnInheritanceFlags.AspectGetter = delegate(object obj)
            {
                FileSystemAccessRule rule = ((FileSystemAccessRuleExtended) obj).fsar;

                return rule.InheritanceFlags.ToString();
            };

            columnPermissionImage.AspectGetter = delegate { return ""; };

            columnPermissionImage.AspectToStringConverter = delegate { return String.Empty; };

            columnPermissionImage.ImageGetter = delegate(object x)
            {
                var fsarext = (FileSystemAccessRuleExtended) x;

                if (fsarext.added || fsarext.changed)
                    return "exclamation.png";
                if (!_canPermissionsBeChanged)
                    return "lock.png";
                return "OK.png";
            };

            listViewPermissions.CellEditStarting += HandleCellEditStarting;
            listViewPermissions.CellEditFinishing += HandleCellEditFinishing;

            Global.listedPermissions = new TrulyObservableCollection<FileSystemAccessRuleExtended>();
            listViewPermissions.SetObjects(Global.listedPermissions);

            Global.listedPermissions.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    refreshPermissions(sender, e);
                }

                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    refreshPermissions(sender, e);
                }
            };

            Global.listedPermissions.myChangeEvent += PermissionsChanged;
        }

        private void HandleCellEditStarting(object sender, CellEditEventArgs e)
        {
            if (!_isModifyPermissionPossible)
                return;
            if (e.Column.Index < 2) //case Principal or icon colum
            {
            }
            if (e.Column.Index == 2) //case permission
            {
                var cb = new ComboBox();
                cb.Bounds = e.CellBounds;
                cb.Font = ((ObjectListView) sender).Font;
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                cb.Items.Add(new ComboItem("Full Control", FileSystemRights.FullControl));
                cb.Items.Add(new ComboItem("Modify", FileSystemRights.Modify));
                cb.Items.Add(new ComboItem("Read and Execute", FileSystemRights.ReadAndExecute));
                cb.Items.Add(new ComboItem("List Directory", FileSystemRights.ListDirectory));
                cb.Items.Add(new ComboItem("Read", FileSystemRights.Read));
                cb.Items.Add(new ComboItem("Write", FileSystemRights.Write));
                cb.SelectedIndex = 0; // should select the entry that reflects the current value
                e.Control = cb;
            }
            else if (e.Column.Index == 3) //grant status
            {
                var cb = new ComboBox();
                cb.Bounds = e.CellBounds;
                cb.Font = ((ObjectListView) sender).Font;
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                cb.Items.Add(new ComboItem("Grant", AccessControlType.Allow));
                cb.Items.Add(new ComboItem("Deny", AccessControlType.Deny));
                cb.SelectedIndex = 0; // should select the entry that reflects the current value
                e.Control = cb;
            }
            else if (e.Column.Index == 5) //Propagates
            {
                var cb = new ComboBox();
                cb.Bounds = e.CellBounds;
                cb.Font = ((ObjectListView) sender).Font;
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                cb.Items.Add(new ComboItem("None", PropagationFlags.None));
                cb.Items.Add(new ComboItem("Inherit Only", PropagationFlags.InheritOnly));
                cb.Items.Add(new ComboItem("No Propagate Inherit", PropagationFlags.NoPropagateInherit));
                cb.SelectedIndex = 0; // should select the entry that reflects the current value
                e.Control = cb;
            }
            else if (e.Column.Index == 6) //inheritance flags
            {
                var cb = new ComboBox();
                cb.Bounds = e.CellBounds;
                cb.Font = ((ObjectListView) sender).Font;
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                cb.Items.Add(new ComboItem("None hier", InheritanceFlags.None));
                cb.Items.Add(new ComboItem("Container Inherit", InheritanceFlags.ContainerInherit));
                cb.Items.Add(new ComboItem("Object Inherit", InheritanceFlags.ObjectInherit));
                cb.SelectedIndex = 0; // should select the entry that reflects the current value
                e.Control = cb;
            }
        }

        private void HandleCellEditFinishing(object sender, CellEditEventArgs e)
        {
            if (e.Control is ComboBox)
            {
                // string value = ((ComboBox) e.Control).SelectedItem.ToString();
                var rule = (FileSystemAccessRuleExtended) e.RowObject;

                if (!_isModifyPermissionPossible)
                    return;
                FileSystemAccessRule oldRule = rule.fsar;
                FileSystemAccessRule newRule = oldRule;

                if (e.Column.Index < 2) //case Principal or icon colum
                {
                    return;
                }
                if (e.Column.Index == 2) //case permission
                {
                    var newFileSystemRights = (FileSystemRights) ((ComboItem) ((ComboBox) (e.Control)).SelectedItem)._tag;

                    if (newFileSystemRights != oldRule.FileSystemRights)
                    {
                        newRule = new FileSystemAccessRule(oldRule.IdentityReference, newFileSystemRights, oldRule.InheritanceFlags, oldRule.PropagationFlags, oldRule.AccessControlType);
                    }
                }
                else if (e.Column.Index == 3) //grant status
                {
                    var newAccessControlType = (AccessControlType) ((ComboItem) ((ComboBox) (e.Control)).SelectedItem)._tag;

                    if (newAccessControlType != oldRule.AccessControlType)
                    {
                        newRule = new FileSystemAccessRule(oldRule.IdentityReference, oldRule.FileSystemRights, oldRule.InheritanceFlags, oldRule.PropagationFlags, newAccessControlType);
                    }
                }
                else if (e.Column.Index == 4) //inheritance
                {
                    MessageBox.Show("The inheritance status of a rule cannot be changed. Please change the characteristics of the file");
                    e.Cancel = true;
                }
                else if (e.Column.Index == 5) //Propagates
                {
                    var newPropagationFlags = (PropagationFlags) ((ComboItem) ((ComboBox) (e.Control)).SelectedItem)._tag;

                    if (newPropagationFlags != oldRule.PropagationFlags)
                    {
                        newRule = new FileSystemAccessRule(oldRule.IdentityReference, oldRule.FileSystemRights, oldRule.InheritanceFlags, newPropagationFlags, oldRule.AccessControlType);
                    }
                }
                else if (e.Column.Index == 6) //inheritance flags
                {
                    var newInheritanceFlags = (InheritanceFlags) ((ComboItem) ((ComboBox) (e.Control)).SelectedItem)._tag;

                    if (newInheritanceFlags != oldRule.InheritanceFlags)
                    {
                        newRule = new FileSystemAccessRule(oldRule.IdentityReference, oldRule.FileSystemRights, newInheritanceFlags, oldRule.PropagationFlags, oldRule.AccessControlType);
                    }
                }

                //only update if the rule really changed
                if (Securable.getACEHash(rule.fsar) != Securable.getACEHash(newRule))
                {
                
                rule.fsar = newRule;
                rule.changed = true;
            }
        }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateButtons();
            Global.listedPermissions.Clear();
            if (listViewSecurables.SelectedItems.Count > 0)
            {
                _unifomSecurableType = true;

                for (int i = 1; i < listViewSecurables.SelectedItems.Count; i++)
                {
                    if (((OLVListItem) listViewSecurables.SelectedItems[i]).RowObject.GetType() != ((OLVListItem) listViewSecurables.SelectedItems[i - 1]).RowObject.GetType())
                        _unifomSecurableType = false;
                }

                foreach (FileSystemAccessRule fsar in Global.getCommonPermissions(listViewSecurables.SelectedItems))
                {
                    var fsarext = new FileSystemAccessRuleExtended(fsar);

                    Global.listedPermissions.Add(fsarext);
                }

                buildPermissionView();
            }
        }

        private bool isPasteSecurablePossible()
        {
            if (!((listViewSecurables.SelectedItems.Count > 0) && (Global.clipboardSecurable != null)))
            {
                return false;
            }

            //if more than one target is selected they all shall be of the same type and they shall be of the same type as the clipboard securable
            bool homogenous = true;
            if (listViewSecurables.SelectedItems.Count > 1)
            {
      
                for (int i = 1; i < listViewSecurables.SelectedItems.Count; i++)
                {
                    var current = (Securable) ((OLVListItem) listViewSecurables.SelectedItems[i]).RowObject;
                    var previous = (Securable) ((OLVListItem) listViewSecurables.SelectedItems[i - 1]).RowObject;

                    if (!(current.GetType() == previous.GetType()))
                    {
                        homogenous = false;
                    }
                    if (!(current.GetType() == previous.GetType()))
                    {
                        homogenous = false;
                    }
                }
            }

            return homogenous && isModifyPermissionPossible();
        }

        private void buildPermissionView()
        {
            analyzeSelectedSecurables();
            listInheritance();
            listViewPermissions.SetObjects(Global.listedPermissions);
        }

        private void analyzeSelectedSecurables()
        {
            int numberOfFiles = 0;
            _canPermissionsBeChanged = true;

            foreach (OLVListItem olvi in listViewSecurables.SelectedItems)
            {
                if (((Securable) olvi.RowObject).isFile)
                    numberOfFiles++;

                if (((Securable) olvi.RowObject).locked)
                    _canPermissionsBeChanged = false;
            }
        }

        private void listInheritance()
        {
            if (listViewSecurables.SelectedItems.Count > 0)
            {
                checkBoxInheritance.Visible = true;
                //First we set the inheritance status of the first object
                checkBoxInheritance.Checked = ((Securable) ((OLVListItem) listViewSecurables.SelectedItems[0]).RowObject).inheritsParentPermissions;
                checkBoxInheritance.Enabled = _canPermissionsBeChanged;

                checkBoxInheritance.ForeColor = Color.Black;

                foreach (OLVListItem olvi in listViewSecurables.SelectedItems)
                {
                    if (((Securable) olvi.RowObject).inheritsParentPermissions != checkBoxInheritance.Checked)
                    {
                        //this is the case where we have no common permission inheritance status
                        checkBoxInheritance.Checked = false;

                        return;
                    }
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            toolStripRightButtonCopy.Enabled = false;
            toolStripRightButtonPaste.Enabled = false;
            //updateGroupButton();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            listViewSecurables.ShowGroups = !listViewSecurables.ShowGroups;
        }

        private void toolStripButton1_Click_2(object sender, EventArgs e)
        {
            populateDriveBox();

            if (Global.drives.getDriveList().Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void contextMenuPermissions_Opening(object sender, CancelEventArgs e)
        {
            //if no ACE is selected we cancel opening

            if (listViewPermissions.SelectedItems.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            if (!_canPermissionsBeChanged)
            {
                e.Cancel = true;
                return;
            }

            foreach (ToolStripControlHost host in ((ToolStripMenuItem) contextMenuPermissions.Items["CP"]).DropDown.Items)
            {
                if (unifiedRights.hasRights((FileSystemRights) host.Control.Tag))
                {
                    ((CheckBox) host.Control).Checked = true;
                }
                else
                {
                    ((CheckBox) host.Control).Checked = false;
                }
            }

            foreach (ToolStripControlHost host in ((ToolStripMenuItem) contextMenuPermissions.Items["SP"]).DropDown.Items)
            {
                if (unifiedRights.hasRights((FileSystemRights) host.Control.Tag))
                {
                    ((CheckBox) host.Control).Checked = true;
                }
                else
                {
                    ((CheckBox) host.Control).Checked = false;
                }
            }
        }

        private void permissionsChangeUserClick(object sender, EventArgs e)
        {
            var _sender = (ToolStripMenuItem) sender;
            switch (_sender.Name)
            {
                case "itemSelectUser":

                    ACEChanger.ChangeUserOfSelectedAce(listViewPermissions.SelectedItems, "", "");
                    break;

                case "itemLastUser1":
                    ACEChanger.ChangeUserOfSelectedAce(listViewPermissions.SelectedItems, Global.settings.nameLastUser1, Global.settings.sidLastUser1);
                    break;

                case "itemLastUser2":
                    ACEChanger.ChangeUserOfSelectedAce(listViewPermissions.SelectedItems, Global.settings.nameLastUser2, Global.settings.sidLastUser2);
                    break;

                case "itemLastUser3":
                    ACEChanger.ChangeUserOfSelectedAce(listViewPermissions.SelectedItems, Global.settings.nameLastUser3, Global.settings.sidLastUser3);
                    break;

                case "itemLastUser4":
                    ACEChanger.ChangeUserOfSelectedAce(listViewPermissions.SelectedItems, Global.settings.nameLastUser4, Global.settings.sidLastUser4);
                    break;

                case "itemLastUser5":
                    ACEChanger.ChangeUserOfSelectedAce(listViewPermissions.SelectedItems, Global.settings.nameLastUser5, Global.settings.sidLastUser5);
                    break;
            }
        }

        private void permissionsChangeFlagsClick(object sender, EventArgs e)
        {
            ACEChanger.changeFlagsOfSelectedACE(listViewPermissions.SelectedItems, (PropagationFlags) ((ToolStripMenuItem) sender).Tag, !((ToolStripMenuItem) sender).Checked);

            _closeSpecialPermissionMenu = false;
        }

        private void permissionsChangePermissionClick(object sender, EventArgs e)
        {
            ACEChanger.changePermissionOfSelectedACE(listViewPermissions.SelectedItems, (FileSystemRights) ((ToolStripMenuItem) sender).Tag, ((ToolStripMenuItem) sender).Checked);

            _closeSpecialPermissionMenu = false;
        }

        private void permissionsChangeGrantTypeClick(object sender, EventArgs e)
        {
            ACEChanger.changeGrantTypeOfSelectedACE(listViewPermissions.SelectedItems, (AccessControlType) ((ToolStripMenuItem) sender).Tag);
        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
        }

        public void prepareContextMenu()
        {
            //let's disable the margins

            ((ToolStripDropDownMenu) ((ToolStripMenuItem) contextMenuPermissions.Items["CP"]).DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu) ((ToolStripMenuItem) contextMenuPermissions.Items["CP"]).DropDown).ShowImageMargin = false;

            ((ToolStripDropDownMenu) ((ToolStripMenuItem) contextMenuPermissions.Items["SP"]).DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu) ((ToolStripMenuItem) contextMenuPermissions.Items["SP"]).DropDown).ShowImageMargin = false;

            ((ToolStripDropDownMenu) ((ToolStripMenuItem) contextMenuPermissions.Items["PF"]).DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu) ((ToolStripMenuItem) contextMenuPermissions.Items["PF"]).DropDown).ShowImageMargin = false;

            ((ToolStripDropDownMenu) ((ToolStripMenuItem) contextMenuPermissions.Items["CO"]).DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu) ((ToolStripMenuItem) contextMenuPermissions.Items["CO"]).DropDown).ShowImageMargin = false;

            ((ToolStripDropDownMenu) ((ToolStripMenuItem) contextMenuPermissions.Items["CG"]).DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu) ((ToolStripMenuItem) contextMenuPermissions.Items["CG"]).DropDown).ShowImageMargin = false;

            //*********************************************
            //Building the  permission menu
            //*********************************************

            foreach (FileSystemRights entry in Global.getSimplePermissions())
            {
                var Cb = new CheckBox();
                Cb.Text = entry.ToString();

                Cb.Tag = entry;
                Cb.CheckedChanged += Cb_CheckedChanged;
                Cb.Click += Cb_Click;
                var Ch = new ToolStripControlHost(Cb);
                Ch.BackColor = Color.Transparent;
                ((ToolStripMenuItem) contextMenuPermissions.Items["CP"]).DropDown.Items.Add(Ch);

                //click handler registrkieren
            }
            //The following is necessary as the text is chopped else. Don't ask me why ;-)
            var rb2 = new RadioButton();
            rb2.Text = "";
            var Ch2 = new ToolStripControlHost(rb2);
            Ch2.AutoSize = true;
            ((ToolStripMenuItem) contextMenuPermissions.Items["CP"]).DropDown.Items.Add(Ch2);
            ((ToolStripMenuItem) contextMenuPermissions.Items["CP"]).DropDown.Items.Remove(Ch2);

            //*********************************************
            //Building the special permission menu
            //*********************************************

            foreach (FileSystemRights entry in Global.getComplexPermissions())
            {
                var Cb = new CheckBox();
                Cb.Text = entry.ToString();

                Cb.Tag = entry;
                Cb.CheckedChanged += Cb_CheckedChanged;
                Cb.Click += Cb_Click; //Fixme

                var Ch = new ToolStripControlHost(Cb);
                Ch.BackColor = Color.Transparent;

                ((ToolStripMenuItem) contextMenuPermissions.Items["SP"]).DropDown.Items.Add(Ch);

                //click handler registrkieren
            }

            //The following is necessary as the text is chopped else. Don't ask me why ;-)
            ((ToolStripMenuItem) contextMenuPermissions.Items["SP"]).DropDown.Items.Add(Ch2);
            ((ToolStripMenuItem) contextMenuPermissions.Items["SP"]).DropDown.Items.Remove(Ch2);

            //*********************************************
            //Building the progagation menu
            //*********************************************
            foreach (PropagationFlags entry in Enum.GetValues(typeof (PropagationFlags)))
            {
                var rb = new RadioButton();
                rb.Text = entry.ToString();

                rb.Tag = entry;
                rb.AutoSize = true;

                rb.Click += rb_Click; //Fixme

                var Ch = new ToolStripControlHost(rb);
                Ch.AutoSize = true;
                Ch.BackColor = Color.Transparent;

                ((ToolStripMenuItem) contextMenuPermissions.Items["PF"]).DropDown.Items.Add(Ch);
            }

            //The following is necessary as the text is chopped else. Don't ask me why ;-)
            ((ToolStripMenuItem) contextMenuPermissions.Items["PF"]).DropDown.Items.Add(Ch2);
            ((ToolStripMenuItem) contextMenuPermissions.Items["PF"]).DropDown.Items.Remove(Ch2);

            //*********************************************
            //Building the grant type
            //*********************************************
            foreach (AccessControlType entry in Enum.GetValues(typeof (AccessControlType)))
            {
                var rb = new RadioButton();
                rb.Text = entry.ToString();
                rb.Tag = entry;
                rb.AutoSize = true;

                rb.Click += rb_Click; //Fixme

                var Ch = new ToolStripControlHost(rb);
                Ch.AutoSize = true;
                Ch.BackColor = Color.Transparent;

                ((ToolStripMenuItem) contextMenuPermissions.Items["CG"]).DropDown.Items.Add(Ch);
            }

            //The following is necessary as the text is chopped else. Don't ask me why ;-)

            ((ToolStripMenuItem) contextMenuPermissions.Items["CG"]).DropDown.Items.Add(Ch2);
            ((ToolStripMenuItem) contextMenuPermissions.Items["CG"]).DropDown.Items.Remove(Ch2);

            //add Rule menu in bottom toolstrip
            ((ToolStripMenuItem) toolStripBottomButtonAddRule.DropDown.Items[0]).DropDownItems["fullControlToolStripMenuItem"].Tag = FileSystemRights.FullControl;
            ((ToolStripMenuItem) toolStripBottomButtonAddRule.DropDown.Items[0]).DropDownItems["modifyToolStripMenuItem"].Tag = FileSystemRights.Modify;
            ((ToolStripMenuItem) toolStripBottomButtonAddRule.DropDown.Items[0]).DropDownItems["readAndExecuteToolStripMenuItem"].Tag = FileSystemRights.ReadAndExecute;
            ((ToolStripMenuItem) toolStripBottomButtonAddRule.DropDown.Items[0]).DropDownItems["listToolStripMenuItem"].Tag = FileSystemRights.ListDirectory;
            ((ToolStripMenuItem) toolStripBottomButtonAddRule.DropDown.Items[0]).DropDownItems["readToolStripMenuItem"].Tag = FileSystemRights.Read;
            ((ToolStripMenuItem) toolStripBottomButtonAddRule.DropDown.Items[0]).DropDownItems["writeToolStripMenuItem"].Tag = FileSystemRights.Write;

            ((ToolStripMenuItem) toolStripBottomButtonAddRule.DropDown.Items[1]).DropDownItems["fullControlToolStripMenuItem1"].Tag = FileSystemRights.FullControl;
            ((ToolStripMenuItem) toolStripBottomButtonAddRule.DropDown.Items[1]).DropDownItems["modifyToolStripMenuItem1"].Tag = FileSystemRights.Modify;
            ((ToolStripMenuItem) toolStripBottomButtonAddRule.DropDown.Items[1]).DropDownItems["readAndExecuteToolStripMenuItem1"].Tag = FileSystemRights.ReadAndExecute;
            ((ToolStripMenuItem) toolStripBottomButtonAddRule.DropDown.Items[1]).DropDownItems["listToolStripMenuItem1"].Tag = FileSystemRights.ListDirectory;
            ((ToolStripMenuItem) toolStripBottomButtonAddRule.DropDown.Items[1]).DropDownItems["readToolStripMenuItem1"].Tag = FileSystemRights.Read;
            ((ToolStripMenuItem) toolStripBottomButtonAddRule.DropDown.Items[1]).DropDownItems["writeToolStripMenuItem1"].Tag = FileSystemRights.Write;
        }

        private void rb_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(((RadioButton)sender).Text);
            //throw new NotImplementedException();
        }

        private void Cb_Click(object sender, EventArgs e)
        {
            //User clicks to change the permission of the currently

            //let's change the selected rules
            var cb = (CheckBox) sender;

            ACEChanger.changePermissionOfSelectedACE(listViewPermissions.SelectedItems, (FileSystemRights) cb.Tag, cb.Checked);
            //and update the displayed permissions
            buildUnifiedPermissions();

            foreach (ToolStripControlHost host in ((ToolStripMenuItem) contextMenuPermissions.Items["CP"]).DropDown.Items)
            {
                if (unifiedRights.hasRights((FileSystemRights) host.Control.Tag))
                {
                    ((CheckBox) host.Control).Checked = true;
                }
                else
                {
                    ((CheckBox) host.Control).Checked = false;
                }
            }

            foreach (ToolStripControlHost host in ((ToolStripMenuItem) contextMenuPermissions.Items["SP"]).DropDown.Items)
            {
                if (unifiedRights.hasRights((FileSystemRights) host.Control.Tag))
                {
                    ((CheckBox) host.Control).Checked = true;
                }
                else
                {
                    ((CheckBox) host.Control).Checked = false;
                }
            }
        }

        private void Cb_CheckedChanged(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void Ch_GotFocus(object sender, EventArgs e)
        {
            //ToolStripControlHost host = (ToolStripControlHost)sender;
            //((CheckBox)host.Control).Checked = true;
            throw new NotImplementedException();
        }

        private void Ch_Paint(object sender, PaintEventArgs e)
        {
            //ToolStripControlHost host = (ToolStripControlHost)sender;
            //((CheckBox)host.Control).Checked = true;

            throw new NotImplementedException();
        }


        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            copyACL();
        }

        private void copyACL()
        {
            if (listViewSecurables.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please select exactly one file or folder to copy permissions");

                return;
            }

            Securable sec = ((Securable) ((OLVListItem) listViewSecurables.SelectedItems[0]).RowObject);

            Global.clipboardSecurable = sec;
            Global.clipboardInheritance = sec.inheritsParentPermissions;
        }


        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }


      

        private void menuEventFilesCpClick(object sender, EventArgs e)
        {
            //copy permissions
            copyACL();
        }

        private void menuEventFilesPaClick(object sender, EventArgs e)
        {
            //paste permissions
            PasteAcl();
        }

        private void menuEventFilesToClick(object sender, EventArgs e)
        {
            //Take the ownership for the securable
            changeOwnerOfSelectedFiles(Global.currentIdentity.Name, Global.currentIdentity.User.ToString());
        }

        private void menuEventFilesChangeUser(object sender, EventArgs e)
        {
            var _sender = (ToolStripMenuItem) sender;
            switch (_sender.Name)
            {
                case "itemSelectUser":
                    changeOwnerOfSelectedFiles("", "");

                    //ACEChanger.changeUserOfSelectedACE(listViewPermissions.SelectedItems,"", "");
                    break;

                case "itemLastUser1":
                    changeOwnerOfSelectedFiles(Global.settings.nameLastUser1, Global.settings.sidLastUser1);
                    break;

                case "itemLastUser2":
                    changeOwnerOfSelectedFiles(Global.settings.nameLastUser2, Global.settings.sidLastUser2);
                    break;

                case "itemLastUser3":
                    changeOwnerOfSelectedFiles(Global.settings.nameLastUser3, Global.settings.sidLastUser3);
                    break;

                case "itemLastUser4":
                    changeOwnerOfSelectedFiles(Global.settings.nameLastUser4, Global.settings.sidLastUser4);
                    break;

                case "itemLastUser5":
                    changeOwnerOfSelectedFiles(Global.settings.nameLastUser5, Global.settings.sidLastUser5);
                    break;
            }
        }

        private void changeOwnerOfSelectedFiles(string _user, string _SID)
        {
            //Todo: hier darf nur der admin weiter und das privilege muss noch her

            string user = "";
            string SID = "";

            if ((_user == "") ^ (_SID == ""))
                MessageBox.Show("bad thing");

            if ((_user == "") && (_SID == ""))
            {
                var frmUser = new frmSelectUser();

                if (frmUser.ShowDialog() == DialogResult.OK)
                {
                    user = frmUser.domainChosen + "\\" + frmUser.userChosen;
                    SID = frmUser.sidChosen;

                    //we only remember the user in the list of last users if selected by the user
                    Global.shiftLastUsers(SID, user);
                }
                else
                {
                    MessageBox.Show("error in selecting user");
                }
            }
            else
            {
                user = _user;
                SID = _SID;
            }

            if (listViewSecurables.SelectedItems.Count > 0)
            {
                foreach (OLVListItem olvi in listViewSecurables.SelectedItems)
                {
                    ((Securable) olvi.RowObject).changeOwner(SID);
                }
            }

            updateSecurableViewCompletely();
        }


        private void UpdateCommitButton()
        {
            uncomittedChanges = false;

            int addedRules = 0;

            int changedRules = 0;

            foreach (OLVListItem olvi in listViewPermissions.Items)
            {
                var fsarext = (FileSystemAccessRuleExtended) olvi.RowObject;

                if (fsarext.changed)
                    changedRules++;
                if (fsarext.added)
                    addedRules++;
            }

            switch (changedRules + addedRules + deletedRules.Count)
            {
                case 0:
                    toolStripBottomButtonApply.Text = "No change pending";
                    toolStripBottomButtonApply.ToolTipText = "There are no changes pending";
                    toolStripBottomButtonApply.Image = new Bitmap(Resources.OK);
                    toolStripBottomButtonApply.Enabled = false;

                    break;

                case 1:
                    toolStripBottomButtonApply.Text = "1 file change pending";
                    toolStripBottomButtonApply.ToolTipText = "There is 1 change pending";
                    toolStripBottomButtonApply.Image = new Bitmap(Resources.exclamation);
                    uncomittedChanges = true;
                    toolStripBottomButtonApply.Enabled = true;
                    break;

                default:
                    toolStripBottomButtonApply.Text = (changedRules + addedRules + deletedRules.Count) + " file changes pending";
                    toolStripBottomButtonApply.ToolTipText = changedRules + " changed rules, " + addedRules + " added rules, " + deletedRules.Count + " are waiting";
                    toolStripBottomButtonApply.Image = new Bitmap(Resources.exclamation);
                    uncomittedChanges = true;
                    toolStripBottomButtonApply.Enabled = true;
                    break;
            }
        }

        private void toolStripRightButtonPaste_Click(object sender, EventArgs e)
        {
            PasteAcl();
            updateSecurableViewCompletely();
        }

        private void PasteAcl()
        {
            if (listViewSecurables.SelectedItems.Count == 0)
            {
                MessageBox.Show("tbd");
            }
            bool canBeUpdated = true;

            foreach (OLVListItem olvi in listViewSecurables.SelectedItems)
            {
                var sec = (Securable) olvi.RowObject;

                if (sec.locked)
                {
                    canBeUpdated = false;
                }
            }

            if (!canBeUpdated)
            {
                MessageBox.Show("tbd");
            }
            foreach (OLVListItem olvi in listViewSecurables.SelectedItems)
            {
                var sec = (Securable) olvi.RowObject;

                sec.replaceAllSecuritySettings(Global.clipboardSecurable);
                listViewSecurables.RefreshObject(sec);
            }
            listInheritance();
        }


        private void toolStripBottomButtonDelete_Click(object sender, EventArgs e)
        {
            deleteSelectedPermissions();
        }

        private void deleteSelectedPermissions()
        {
            bool inheritance = false;

            var itemsToRemove = new List<FileSystemAccessRuleExtended>();

            var sec = ((Securable) ((OLVListItem) listViewSecurables.SelectedItems[0]).RowObject);

            foreach (OLVListItem olvi in listViewPermissions.SelectedItems)
            {
                if (((FileSystemAccessRuleExtended) olvi.RowObject).fsar.IsInherited)
                    inheritance = true;
            }
            if (inheritance)
            {
                MessageBox.Show("The rule you are trying to remove has been inherited from a parent object. Please either remove the rule on the parent object or remove the inheritance");
                return;
            }

            foreach (OLVListItem olvi in listViewPermissions.SelectedItems)
            {
                if (((FileSystemAccessRuleExtended) olvi.RowObject).added)
                {
                    itemsToRemove.Add((FileSystemAccessRuleExtended) olvi.RowObject);
                }
                else
                {
                    itemsToRemove.Add((FileSystemAccessRuleExtended) olvi.RowObject);
                    deletedRules.Add(((FileSystemAccessRuleExtended) olvi.RowObject).fsar);
                }
            }

            foreach (FileSystemAccessRuleExtended item in itemsToRemove)
            {
                Global.listedPermissions.Remove(item);
            }

            UpdateCommitButton();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBoxInheritance_Click(object sender, EventArgs e)
        {
            //Todo: the view is not updated after applying this operation. But that's a problem of the list view 
            if (listViewSecurables.SelectedItems.Count == 0)
            {
                MessageBox.Show("tbd");

                return;
            }

            if (checkBoxInheritance.Checked)
            {
                if (MessageBox.Show("You are about to inherit all parent permissions to the current securable(s). Are you sure you want to continue?", "Changing inheritance", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (OLVListItem olvi in listViewSecurables.SelectedItems)
                    {
                        ((Securable) olvi.RowObject).setInheritance(true, false);

                        if (!((Securable) olvi.RowObject).applyACL())
                            MessageBox.Show("this shall not happpen");

                        ((Securable)olvi.RowObject).readAttributesFromDisk();
                        listViewSecurables.RefreshObject(olvi);
                    }
                    buildPermissionView();
                }
            }
            else
            {
                if (MessageBox.Show("You are about to protect the current securable against inheriting the parent permissions. Are you sure you want to continue", "Changing inheritance", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (MessageBox.Show("Do you want to keep a copy of the previously inherited permissions as explicit permissions?", "Changing inheritance", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (OLVListItem olvi in listViewSecurables.SelectedItems)
                        {
                            ((Securable) olvi.RowObject).setInheritance(false, true);
                            if (!((Securable) olvi.RowObject).applyACL())
                                MessageBox.Show("this shall not happpen");

                            ((Securable) olvi.RowObject).readAttributesFromDisk();
                            listViewSecurables.RefreshObject(olvi);
                        }

                        buildPermissionView();
                    }
                    else
                    {
                        foreach (OLVListItem olvi in listViewSecurables.SelectedItems)
                        {
                            ((Securable) olvi.RowObject).setInheritance(false, false);

                            if (!((Securable) olvi.RowObject).applyACL())
                                MessageBox.Show("this shall not happpen");
                           
                            ((Securable)olvi.RowObject).readAttributesFromDisk();
                            listViewSecurables.RefreshObject(olvi);
                        }

                        buildPermissionView();
                    }
                }
                else
                    return;
            }
        }

        private void toolStripRightButtonRefresh_Click(object sender, EventArgs e)
        {
           

            updateSecurableViewCompletely();
        }

        private void treeView1_AfterSelect_3(object sender, TreeViewEventArgs e)
        {
        }

        private void listViewSecurables_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void AnalyzeListedPermissions()
        {
            _uniformType = true;

            bool firstRun = true;

            if (listViewPermissions.SelectedItems.Count < 2)
                return;

            FileSystemAccessRule tmpRule = null;

            foreach (OLVListItem olvi in listViewPermissions.SelectedItems)
            {
                FileSystemAccessRule ACE = ((FileSystemAccessRuleExtended) olvi.RowObject).fsar;
                if (firstRun)
                {
                    tmpRule = ACE;
                    firstRun = false;
                }
                else
                {
                    if (!ACE.AccessControlType.Equals(tmpRule.AccessControlType))
                    {
                        _uniformType = false;
                    }
                }
            }
        }

        private void contextMenuFiles_Opening(object sender, CancelEventArgs e)
        {
        }

        private void CopyPermissionsAsText()
        {
            string text = "";

            foreach (OLVListItem olvi in listViewSecurables.SelectedItems)
            {
                text = text + ((Securable) olvi.RowObject).getSecurableAsText();
                text = text + "************************************************************************************************" + Environment.NewLine;
            }

            Clipboard.SetText(text);
        }

        private void CopyPermissionsAsMarkdown()
        {
            string text = "";

            foreach (OLVListItem olvi in listViewSecurables.SelectedItems)
            {
                text = text + ((Securable) olvi.RowObject).getSecurableAsMarkdown();
            }

            Clipboard.SetText(text);
        }

        private void copyFilePermissionAsTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyPermissionsAsText();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Drive test;
            test = Global.drives.getDrive(comboBox1.SelectedIndex);

            

            if (test.info.DriveFormat == "NTFS")
            {
                DirectoryInfo info = Global.drives.getDrive(comboBox1.SelectedIndex).rootDirectory;
                populateTreeView(info.Name);
            }
            else
            {
                MessageBox.Show("This tool currently only supports NTFS file systems");
            }
        }

        private void toolStripDropDownButtonSettings_Click(object sender, EventArgs e)
        {
        }

        private void rerteiveDomainInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Fixme: der hier muss dann immer ausgeführt werden
            GroupsAndUsers.getGroupsAndUsers();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            GroupsAndUsers.elevatePrivileges();
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private static void OpenSettings()
        {
            var frm = new frmSettings();
            frm.Show();
        }

        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            OpenFolder();
        }

        private void OpenFolder()
        {
            folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowDialog();
            string folder = folderBrowserDialog1.SelectedPath;

            if (Directory.Exists(folder))
            {

                DriveInfo di = new DriveInfo(Directory.GetDirectoryRoot(folder));

                if (di.DriveFormat == "NTFS")
                {

                    populateTreeView(folder);
                }
       
            else
            {
                 MessageBox.Show("This software only supports NTFS formatted drives");
            }
        }
            else
            {
                MessageBox.Show("The selected path doesn't seem to exist");
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void toolStripDropDownButtonUser_Click(object sender, EventArgs e)
        {
        }

        private void toolStripDropDownButtonUserCO_Click(object sender, EventArgs e)
        {
            changeOwnerOfSelectedFiles("", "");
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            CustomSettings.save(Global.settings, Global.settings.settingsFile);
        }

        private void toolStripBottomButtonApply_Click(object sender, EventArgs e)
        {
            //time to apply the ACEs
            ListView.SelectedListViewItemCollection selectedSecurables = listViewSecurables.SelectedItems;

            foreach (OLVListItem olvi in listViewSecurables.SelectedItems)
            {
                {
                    var securable = ((Securable) olvi.RowObject);

                    //first we add the new ACEs and the updated ones

                    foreach (OLVListItem item in listViewPermissions.Items)
                    {
                        if (((FileSystemAccessRuleExtended) item.RowObject).added)
                        {
                            securable.addACE(((FileSystemAccessRuleExtended) item.RowObject).fsar);
                            ((FileSystemAccessRuleExtended) item.RowObject).added = false;
                        }

                        if (((FileSystemAccessRuleExtended) item.RowObject).changed)
                        {
                            securable.removeACE(((FileSystemAccessRuleExtended) item.RowObject).fsarOriginal);
                            securable.addACE(((FileSystemAccessRuleExtended) item.RowObject).fsar);
                            ((FileSystemAccessRuleExtended) item.RowObject).changed = false;
                        }
                    }

                    //then we delete the deleted ones
                    foreach (FileSystemAccessRule rule in deletedRules)
                    {
                        securable.removeACE(rule);
                    }

                    deletedRules.Clear();

                    if (!securable.applyACL())
                        if (MessageBox.Show("Error when applying changes to Do you want to continue or cancel for the rest of the files?", "Error Appyling ACL", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                            break;
                }

                updateSecurableViewCompletely();
            }
        }

        private void toolStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void listViewPermissions_MouseDown(object sender, MouseEventArgs e)
        {
            if (listViewPermissions.FocusedItem == null)
                return;

            if (listViewPermissions.FocusedItem.Selected == false)
                listViewPermissions.FocusedItem.Selected = true;
        }

        private void toolStripBottomButtonAddRule_Click(object sender, EventArgs e)
        {
        }

        private void toolStripBottomButtonAddRule_DropDownOpening(object sender, EventArgs e)
        {
            toolStripBottomButtonAddRule.DropDown.Items.Clear();

            var itemAllow = new ToolStripMenuItem("Allow");
            itemAllow.Name = "itemSelectUser";
            itemAllow.Tag = AccessControlType.Allow;
            toolStripBottomButtonAddRule.DropDown.Items.Add(itemAllow);

            var itemDeny = new ToolStripMenuItem("Deny");
            itemDeny.Name = "itemDenyUser";
            itemDeny.Tag = AccessControlType.Deny;
            toolStripBottomButtonAddRule.DropDown.Items.Add(itemDeny);

            var permissions = new ToolStripMenuItem[Enum.GetValues(typeof (Global.simplePermissions)).Length];

            foreach (ToolStripMenuItem baseitem in (toolStripBottomButtonAddRule).DropDown.Items)
            {
                int i = 0;

                foreach (FileSystemRights entry in Global.getSimplePermissions())
                {
                    permissions[i] = new ToolStripMenuItem();
                    permissions[i].Name = entry.ToString();
                    permissions[i].Text = entry.ToString();
                    permissions[i].Tag = entry;

                    var itemSelectUser = new ToolStripMenuItem("Select User");
                    itemSelectUser.Name = "itemSelectUser";
                    itemSelectUser.Click += addACEClick;
                    permissions[i].DropDown.Items.Add(itemSelectUser);

                    if (Global.settings.nameLastUser1 != "")
                    {
                        var itemLastUser1 = new ToolStripMenuItem(Global.settings.nameLastUser1);
                        itemLastUser1.Name = "itemLastUser1";
                        itemLastUser1.Click += addACEClick;
                        permissions[i].DropDown.Items.Add(itemLastUser1);
                    }

                    if (Global.settings.nameLastUser2 != "")
                    {
                        var itemLastUser2 = new ToolStripMenuItem(Global.settings.nameLastUser2);
                        itemLastUser2.Name = "itemLastUser2";
                        itemLastUser2.Click += addACEClick;
                        permissions[i].DropDown.Items.Add(itemLastUser2);
                    }

                    if (Global.settings.nameLastUser3 != "")
                    {
                        var itemLastUser3 = new ToolStripMenuItem(Global.settings.nameLastUser3);
                        itemLastUser3.Name = "itemLastUser3";
                        itemLastUser3.Click += addACEClick;
                        permissions[i].DropDown.Items.Add(itemLastUser3);
                    }

                    if (Global.settings.nameLastUser4 != "")
                    {
                        var itemLastUser4 = new ToolStripMenuItem(Global.settings.nameLastUser4);
                        itemLastUser4.Name = "itemLastUser4";
                        itemLastUser4.Click += addACEClick;
                        permissions[i].DropDown.Items.Add(itemLastUser4);
                    }

                    if (Global.settings.nameLastUser5 != "")
                    {
                        var itemLastUser5 = new ToolStripMenuItem(Global.settings.nameLastUser5);
                        itemLastUser5.Name = "itemLastUser5";
                        itemLastUser5.Click += addACEClick;
                        permissions[i].DropDown.Items.Add(itemLastUser5);
                    }

                    i++;
                }
                baseitem.DropDownItems.AddRange(permissions);
            }
        }

        private void addACEClick(object sender, EventArgs e)
        {
            var clickedMenuItem = (ToolStripMenuItem) sender;
            string menuText = clickedMenuItem.Text;

            var accessControlType = new AccessControlType();
            FileSystemRights fileSystemRights;
            string userSID = "";

            accessControlType = (AccessControlType) clickedMenuItem.OwnerItem.OwnerItem.Tag;
            fileSystemRights = (FileSystemRights) clickedMenuItem.OwnerItem.Tag;

            switch (clickedMenuItem.Name)
            {
                case "itemSelectUser":
                    var frmUser = new frmSelectUser();
                    if (frmUser.ShowDialog() == DialogResult.OK)
                    {
                        string user = frmUser.domainChosen + "\\" + frmUser.userChosen;
                        userSID = frmUser.sidChosen;

                        //we only remember the user in the list of last users if selected by the user
                        Global.shiftLastUsers(userSID, user);
                    }
                    else
                    {
                        MessageBox.Show("error in selecting user");
                    }
                    break;

                case "itemLastUser1":
                    userSID = Global.settings.sidLastUser1;
                    break;

                case "itemLastUser2":
                    userSID = Global.settings.sidLastUser2;
                    break;

                case "itemLastUser3":
                    userSID = Global.settings.sidLastUser3;
                    break;

                case "itemLastUser4":
                    userSID = Global.settings.sidLastUser4;
                    break;

                case "itemLastUser5":
                    userSID = Global.settings.sidLastUser5;

                    break;
            }

            var fsarext = new FileSystemAccessRuleExtended(new FileSystemAccessRule(new SecurityIdentifier(userSID), fileSystemRights, accessControlType));
            fsarext.added = true;
            Global.listedPermissions.Add(fsarext);
        }

        //private static void autoResizeListView(ListView lv)
        //{
        //    lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        //    ListView.ColumnHeaderCollection cc = lv.Columns;
        //    for (int i = 0; i < cc.Count; i++)
        //    {
        //        int colWidth = TextRenderer.MeasureText(cc[i].Text, lv.Font).Width + 10;
        //        if (colWidth > cc[i].Width)
        //        {
        //            cc[i].Width = colWidth;
        //        }
        //    }
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //}

        private void listViewSecurables_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //if the item on focus is not yet selected, let's do it.
                if (listViewSecurables.FocusedItem == null)
                    return;
                if (listViewSecurables.FocusedItem.Selected == false)
                    listViewSecurables.FocusedItem.Selected = true;

                var sec = (Securable) ((OLVListItem) listViewSecurables.SelectedItems[0]).RowObject;

                contextMenuFiles.Items.Clear();
                contextMenuFiles.ShowCheckMargin = false;
                contextMenuFiles.ShowImageMargin = true;

                //**********************************
                //entry for copy
                //**********************************

                var cp = new ToolStripMenuItem("Copy Permissions");

                cp.Enabled = _isCopySecurablePossible;
                cp.Image = Resources.copy;

                cp.Click += menuEventFilesCpClick;

                contextMenuFiles.Items.Add(cp);

                //**********************************
                //entry for paste
                //**********************************

                var pa = new ToolStripMenuItem("Paste Permissions");
                pa.Enabled = _isPasteSecurablePossible;
                pa.Image = Resources.paste;
                pa.Click += menuEventFilesPaClick;

                contextMenuFiles.Items.Add(pa);

                //**********************************
                //entry for take ownership
                //**********************************

                var to = new ToolStripMenuItem("Take Ownership");
                to.Click += menuEventFilesToClick;
                to.Image = Resources.user_own;
                to.Enabled = isTakeOwnershipPossible();
                to.Click += menuEventFilesToClick;
                contextMenuFiles.Items.Add(to);

                //**********************************
                //entries for changes ownership
                //**********************************
                var CO = new ToolStripMenuItem("Change Owner");

                CO.DropDownItems.Clear();
                CO.Image = Resources.user_select;

                var itemSelectUser = new ToolStripMenuItem("Select User");
                itemSelectUser.Name = "itemSelectUser";
                itemSelectUser.Click += menuEventFilesChangeUser;

                CO.DropDownItems.Add(itemSelectUser);

                if (Global.settings.nameLastUser1 != "")
                {
                    var itemLastUser1 = new ToolStripMenuItem(Global.settings.nameLastUser1);
                    itemLastUser1.Name = "itemLastUser1";
                    itemLastUser1.Click += menuEventFilesChangeUser;
                    CO.DropDownItems.Add(itemLastUser1);
                }
                if (Global.settings.nameLastUser2 != "")
                {
                    var itemLastUser2 = new ToolStripMenuItem(Global.settings.nameLastUser2);
                    itemLastUser2.Name = "itemLastUser2";
                    itemLastUser2.Click += menuEventFilesChangeUser;
                    CO.DropDownItems.Add(itemLastUser2);
                }
                if (Global.settings.nameLastUser3 != "")
                {
                    var itemLastUser3 = new ToolStripMenuItem(Global.settings.nameLastUser3);
                    itemLastUser3.Name = "itemLastUser3";
                    itemLastUser3.Click += menuEventFilesChangeUser;
                    CO.DropDownItems.Add(itemLastUser3);
                }
                if (Global.settings.nameLastUser4 != "")
                {
                    var itemLastUser4 = new ToolStripMenuItem(Global.settings.nameLastUser4);
                    itemLastUser4.Name = "itemLastUser4";
                    itemLastUser4.Click += menuEventFilesChangeUser;
                    CO.DropDownItems.Add(itemLastUser4);
                }
                if (Global.settings.nameLastUser5 != "")
                {
                    var itemLastUser5 = new ToolStripMenuItem(Global.settings.nameLastUser5);
                    itemLastUser5.Name = "itemLastUse5";
                    itemLastUser5.Click += menuEventFilesChangeUser;
                    CO.DropDownItems.Add(itemLastUser5);
                }

                CO.Enabled = isTakeOwnershipPossible();

                contextMenuFiles.Items.Add(CO);
            }
        }

        private void listViewSecurables_DoubleClick(object sender, EventArgs e)
        {
        }

        private void listViewPermissions_SelectedIndexChanged(object sender, EventArgs e)
        {
            //the only GUI element that needs a potential update is the delete ACE button
            toolStripBottomButtonDelete.Enabled = isDeleteACEPossibleBasedOnACE();
            AnalyzeListedPermissions();
            buildUnifiedPermissions();
        }

        private void buildUnifiedPermissions()
        {
            //**********************************
            //building the unified permission for all selected ACEs
            //**********************************

            unifiedRights = new FileSystemRights();
            unifiedRights = FileSystemRights.FullControl;

            unifiedFlags = new PropagationFlags();

            unifiedFlags = unifiedFlags | PropagationFlags.InheritOnly | PropagationFlags.NoPropagateInherit;

            foreach (OLVListItem olvi in listViewPermissions.SelectedItems)
            {
                unifiedRights = unifiedRights & ((FileSystemAccessRuleExtended) olvi.RowObject).fsar.FileSystemRights;
                unifiedFlags = unifiedFlags & ((FileSystemAccessRuleExtended) olvi.RowObject).fsar.PropagationFlags;
            }
        }

        private void listViewPermissions_KeyDown(object sender, KeyEventArgs e)
        {
        }

        public void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //This will get called when the property of an object inside the collection changes - note you must make it a 'reset' - dunno why
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
        }

        private void fullControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void listViewPermissions_FormatCell(object sender, FormatCellEventArgs e)
        {
            string test = "";
        }

        private void contextMenuPermissions_Paint(object sender, PaintEventArgs e)
        {
        }

        private void CP_DropDownOpening(object sender, EventArgs e)
        {
            foreach (ToolStripControlHost host in CP.DropDown.Items)
            {
                if (unifiedRights.hasRights((FileSystemRights) host.Control.Tag))
                {
                    ((CheckBox) host.Control).Checked = true;
                }
                else
                {
                    ((CheckBox) host.Control).Checked = false;
                }
            }
        }

        private void CO_DropDownOpening(object sender, EventArgs e)
        {
            //**********************************
            //entry for change user
            //**********************************

            var item = (ToolStripMenuItem) sender;

            CO.DropDownItems.Clear();

            var itemSelectUser = new ToolStripMenuItem("Select User");
            itemSelectUser.Name = "itemSelectUser";
            itemSelectUser.Click += permissionsChangeUserClick;
            CO.DropDownItems.Add(itemSelectUser);

            if (Global.settings.nameLastUser1 != "")
            {
                var itemLastUser1 = new ToolStripMenuItem(Global.settings.nameLastUser1);
                itemLastUser1.Name = "itemLastUser1";
                itemLastUser1.Click += permissionsChangeUserClick;
                CO.DropDownItems.Add(itemLastUser1);
            }
            if (Global.settings.nameLastUser2 != "")
            {
                var itemLastUser2 = new ToolStripMenuItem(Global.settings.nameLastUser2);

                itemLastUser2.Name = "itemLastUser2";
                itemLastUser2.Click += permissionsChangeUserClick;
                CO.DropDownItems.Add(itemLastUser2);
            }
            if (Global.settings.nameLastUser3 != "")
            {
                var itemLastUser3 = new ToolStripMenuItem(Global.settings.nameLastUser3);
                itemLastUser3.Name = "itemLastUser3";
                itemLastUser3.Click += permissionsChangeUserClick;
                CO.DropDownItems.Add(itemLastUser3);
            }
            if (Global.settings.nameLastUser4 != "")
            {
                var itemLastUser4 = new ToolStripMenuItem(Global.settings.nameLastUser4);
                itemLastUser4.Name = "itemLastUser4";
                itemLastUser4.Click += permissionsChangeUserClick;
                CO.DropDownItems.Add(itemLastUser4);
            }
            if (Global.settings.nameLastUser5 != "")
            {
                var itemLastUser5 = new ToolStripMenuItem(Global.settings.nameLastUser5);
                itemLastUser5.Name = "itemLastUser5";
                itemLastUser5.Click += permissionsChangeUserClick;
                CO.DropDownItems.Add(itemLastUser5);
            }
        }

        private void CP_Click(object sender, EventArgs e)
        {
        }

        //private void listViewPermissions_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //}

        private void listViewSecurables_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listViewSecurables.HitTest(e.X, e.Y);

            ListViewItem item = info.Item;

            if (item != null)
            {
                var securable = (Securable) ((OLVListItem) item).RowObject;
                if (!securable.isFile)
                {
                    if (((Folder) securable).displayName == "..")
                    {
                        treeViewClickNode(treeViewFolder.SelectedNode.Parent);
                    }
                    if (((Folder) securable).displayName == ".")
                    {
                    }
                    else
                    {
                        treeViewFolder.SelectedNode.Expand();

                        foreach (TreeNode node in treeViewFolder.SelectedNode.Nodes)
                        {
                            var dir = (DirectoryInfo) node.Tag;

                            if (dir.Name != securable.name)
                                continue;
                            treeViewClickNode(node);
                            return;
                        }
                    }
                }
            }
        }

        private void copyPermissionsAsMarkupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyPermissionsAsMarkdown();
        }

        private void toolStripButton1_DisplayStyleChanged(object sender, EventArgs e)
        {

        }
    }
}