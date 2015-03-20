using ACLMaster.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ACLMaster
{
    public partial class frmSelectUser : Form
    {
        private List<ListViewItem> items = new List<ListViewItem>();

        public string userChosen;
        public string domainChosen;
        public string sidChosen;

        public frmSelectUser()
        {
            userChosen = "";
            InitializeComponent();
        }

        private void splitContainer1_Panel2_Paint(object _sender, PaintEventArgs e)
        {
        }

        private void frmSelectUser_Load(object _sender, EventArgs e)
        {
            checkBoxADUser.Enabled = Global.settings.machineIsDomainJoined;
            checkBoxADUser.Checked = Global.settings.machineIsDomainJoined;
            checkBoxADGroups.Enabled = Global.settings.machineIsDomainJoined;
            checkBoxADGroups.Checked = Global.settings.machineIsDomainJoined;

            fillPrincipals();
        }

        private void fillPrincipals()
        {
            listViewUser.Clear();

            ColumnHeader columnHeader1 = new ColumnHeader {Text = "Name"};
            ColumnHeader columnHeader2 = new ColumnHeader {Text = "Domain"};
         

            listViewUser.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });

            ListViewItem item;

            ListViewItem.ListViewSubItem[] subItems;

            if (checkBoLocalUser.Checked)
            {
                foreach (DictionaryEntry entry in Global.settings.allLocalUsers)
                {
                  


                        item = new ListViewItem(((Prcpl) entry.Value).Name)
                        {
                            ToolTipText = ((Prcpl) entry.Value).Sid,
                            Tag = ((Prcpl) entry.Value)
                        };

                        subItems = new ListViewItem.ListViewSubItem[]
                        {
                            new ListViewItem.ListViewSubItem(item, ((Prcpl) entry.Value).Domain),
                        };

                        item.SubItems.AddRange(subItems);
                        listViewUser.Items.Add(item);
                    }
                    ;
                
            }

            if (checkBoxLocalGroups.Checked)
            {
                foreach (DictionaryEntry entry in Global.settings.allLocalGroups)
                {
                    if (!checkBoxMyGroupsOnly.Checked || ((Prcpl) entry.Value).currentUserIsMember)
                    {
                        item = new ListViewItem(((Prcpl) entry.Value).Name)
                        {
                            ToolTipText = ((Prcpl) entry.Value).Sid,
                            Tag = ((Prcpl) entry.Value)
                        };
                        subItems = new ListViewItem.ListViewSubItem[]
                        {
                            new ListViewItem.ListViewSubItem(item, ((Prcpl) entry.Value).Domain),
                        };

                        item.SubItems.AddRange(subItems);
                        listViewUser.Items.Add(item);
                    }
                    else
                    {
                    }
                    ;
                }
            }

            if (checkBoxADUser.Checked)
            {
                foreach (DictionaryEntry entry in Global.settings.allDomainUsers)
                {
                    item = new ListViewItem(((Prcpl)entry.Value).Name)
                    {
                        ToolTipText = ((Prcpl)entry.Value).Sid,
                        Tag = ((Prcpl)entry.Value)
                    };
                    subItems = new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item,((Prcpl)entry.Value).Domain),
                    };

                    item.SubItems.AddRange(subItems);
                    listViewUser.Items.Add(item);
                };
            }

            if (checkBoxADGroups.Checked)
            {
                foreach (DictionaryEntry entry in Global.settings.allDomainGroups)
                {
                    if (!checkBoxMyGroupsOnly.Checked || ((Prcpl) entry.Value).currentUserIsMember)
                    {
                        item = new ListViewItem(((Prcpl) entry.Value).Name)
                        {
                            ToolTipText = ((Prcpl) entry.Value).Sid,
                            Tag = ((Prcpl) entry.Value)
                        };

                        subItems = new ListViewItem.ListViewSubItem[]
                        {
                            new ListViewItem.ListViewSubItem(item, ((Prcpl) entry.Value).Domain),
                        };

                        item.SubItems.AddRange(subItems);
                        listViewUser.Items.Add(item);
                    }
                    else
                    {
                    }
                    ;
                }
            }

            listViewUser.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            listViewUser.Items.AddRange(items.Select(c => new ListViewItem(c.Name)).ToArray());
        }

        private void button3_Click(object _sender, EventArgs e)
        {
            if (listViewUser.SelectedItems.Count != 1) return;
            userChosen = ((Prcpl)listViewUser.SelectedItems[0].Tag).Name;
            domainChosen = ((Prcpl)listViewUser.SelectedItems[0].Tag).Domain;
            sidChosen = ((Prcpl)listViewUser.SelectedItems[0].Tag).Sid;
            DialogResult = DialogResult.OK;

            Close();
        }

        private void button2_Click(object _sender, EventArgs e)
        {
            userChosen = "";
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public string getUser()
        {
            return userChosen;
        }

        private void textBox1_TextChanged(object _sender, EventArgs e)
        {
            listViewUser.Items.Clear();
            listViewUser.Items.AddRange(items.Where(i => string.IsNullOrEmpty(textBox1.Text) || i.Name.Contains(textBox1.Text))
                .Select(c => new ListViewItem(c.Name)).ToArray());
        }

        private void checkBoLocalUser_CheckedChanged(object _sender, EventArgs e)
        {
            fillPrincipals();
        }

        private void checkBoxLocalGroups_CheckStateChanged(object _sender, EventArgs e)
        {
        }

        private void checkBoxLocalGroups_CheckedChanged(object _sender, EventArgs e)
        {
            fillPrincipals();
        }

        private void checkBoxADUser_CheckedChanged(object _sender, EventArgs e)
        {
            fillPrincipals();
        }

        private void checkBoxADGroups_CheckedChanged(object _sender, EventArgs e)
        {
            fillPrincipals();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void checkBoxMyGroupsOnly_CheckedChanged(object sender, EventArgs e)
        {
            fillPrincipals();
        }
    }
}