using System;
using System.Windows.Forms;
using ACLMaster.Properties;

namespace ACLMaster
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        //private void tabPage3_Click(object sender, EventArgs e)
        //{
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //some consistency checks

            if ((radioButtonAllReadable.Checked == false) && (radioButtonOnlyChangables.Checked == false))
            {
                MessageBox.Show("Please select one option which files should be shown");
                return;
            }

           

            //set settings accordingly

            Global.settings.startEscalated = checkBoxStartEscalated.Checked;
            Global.settings.localOnly = checkBoxWorkLocal.Checked;

            Global.settings.showOnlyReadableFolders = radioButtonAllReadable.Checked;
            Global.settings.showOnlyChangableFolders = radioButtonOnlyChangables.Checked;

            Global.settings.logVerbose = radioButtonLoggingVerbose.Checked;

            Global.settings.greyOutProtectedSecurables = checkBoxGreyOut.Checked;

            Global.settings.showInternalDetails = checkBoxShowInternalDetails.Checked;
            //save settings
            CustomSettings.save(Global.settings,Global.settings.settingsFile );

            Global.settings.validityPeriodGroupsAndUsers = decimal.ToInt32(numericUpDownValidityPeriod.Value);
           

            //  Properties.Global.settings.WriteSettings();

            //close form

            Close();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            checkBoxStartEscalated.Checked = Global.settings.startEscalated;
            checkBoxWorkLocal.Checked = Global.settings.localOnly;

            radioButtonAllReadable.Checked = Global.settings.showOnlyReadableFolders;
            radioButtonOnlyChangables.Checked = Global.settings.showOnlyChangableFolders;

            radioButtonLoggingStandard.Checked = !Global.settings.logVerbose;
            radioButtonLoggingVerbose.Checked = Global.settings.logVerbose;

            checkBoxGreyOut.Checked = Global.settings.greyOutProtectedSecurables;
            checkBoxShowInternalDetails.Checked = Global.settings.showInternalDetails;

            numericUpDownValidityPeriod.Value = Global.settings.validityPeriodGroupsAndUsers;
            
        }

        private void radioButtonAllReadable_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAllReadable.Checked == true)
                checkBoxGreyOut.Enabled = true;
            else
                checkBoxGreyOut.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButtonOnlyChangables_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOnlyChangables.Checked == true)
                checkBoxGreyOut.Enabled = false;
            else
                checkBoxGreyOut.Enabled = true;
        }
    }
}