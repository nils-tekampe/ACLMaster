using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace ACLMaster
{
    /// <summary>
    /// This is the master class for all securables. I.e. for files and folders
    /// </summary>
    internal class Securable
    {
        public string fullName;

        public string name { get; set; }

        // ReSharper disable once InconsistentNaming
        public ArrayList ACLText;

        // ReSharper disable once InconsistentNaming
        public int ACLHash { get; set; }

        public bool locked;
        public bool readError;

        public DateTime lastUpdate;
        public string owner;

        // ReSharper disable once InconsistentNaming
        public string ownerSID;

        public bool knownOwner;
        public bool inConstructor = false;

        public bool isFile;
        public bool inheritsParentPermissions;

        //The following array is for the effective permissions
        public FileSystemRights effectiveRightsForCurrentUser;

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="Securable"/> class.
        /// </summary>
        /// <param name="link">The link to the securable.</param>
        public Securable(string link)
        {
            try
            {
                ACLHash = 0;
                fullName = link;
                ACLText = new ArrayList();
                readError = false;


            }
            catch (Exception ex)
            {
                Log.Error("Error in Constructor of securable " + ex);
            }
        }

        /// <summary>
        /// Function to add an ACE to a securable
        /// </summary>
        /// <param name="fsar">The _ FSAR.</param>
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once InconsistentNaming
        public void addACE(FileSystemAccessRule fsar)
        {
            if (locked)
            {
                Log.Error("Trying to add a ACE to a locke securable. Securable:" + fullName);
#if DEBUG
                MessageBox.Show("Trying to add a ACE to a locke securable. This shall never happen");
#endif
            }

            Log.Debug("Adding an ACE to " + fullName);
            try
            {
                addACEToSD(fsar);
            }
            catch (Exception ex)
            {
                Log.Error("Error in addACE of Securable " + ex);
            }
            updateHash();
        }

        /// <summary>
        /// Determines whether the owner of the securable can be changed by the current user
        /// </summary>
        /// <returns></returns>
        public bool canOwnerBeChanged()
        {
            if (!UacHelper.IsProcessElevated)
            {
                return false;
            }

            if (hasUserThisRight(FileSystemRights.TakeOwnership))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Checks whether the ACE is existing in the current securable
        /// </summary>
        /// <param name="ace">The ace.</param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public bool doesACEExist(FileSystemAccessRule ace)
        {
            bool returnValue = false;

            foreach (FileSystemAccessRule rule in getACL())
            {
                if (getACEHash(rule) == getACEHash(ace)) { returnValue = true; }
            }
            return returnValue;
        }

        public void removeACE(FileSystemAccessRule _ACE)
        {
            Log.Debug("Removing an ACE from " + fullName);
            try
            {
                if (doesACEExist(_ACE))
                {
                    removeACEFromSD(_ACE);
                }
                else
                    Log.Debug("Tried to remove an ACE from securable that was not existing. Securable: " + fullName);

                updateHash();
            }
            catch (Exception ex)
            {
                Log.Error("Error in removeACE of Securable " + ex.ToString());
            }
        }

        public void updateACE(FileSystemAccessRule _newACE, FileSystemAccessRule _oldACE)
        {
            if (locked)
            {
                Log.Error("Trying to update an ACE to a locke securable");
#if DEBUG
                MessageBox.Show("Trying to update an ACE to a locke securable");

#endif
            }

            Log.Debug("Updating an ACE in " + fullName);
            try
            {
                if (doesACEExist(_oldACE))
                {
                    updateACEInSD(_oldACE, _newACE);
                }
                else
                    Log.Debug("Tried to update an ACE from securable that was not existing. Securable: " + fullName);

                updateHash();
            }
            catch (Exception ex)
            {
                Log.Error("Error in updateACE of Securable " + ex.ToString());
            }
        }

        /// <summary>
        /// This function replaces the complete ACL of the securable. This is done for each and every rule by first removing all old rules and then adding the new rules
        /// </summary>
        /// <param name="_newACL">The new ACL is passed to the function by ref. It is only read..</param>
        public void replaceACL(AuthorizationRuleCollection _newACL)
        {
            if (locked)
            {
                Log.Error("Trying to replace ACL of a locked securable");
#if DEBUG
                MessageBox.Show("Trying to replace ACL of a locked securable");

#endif
            }

            //This onyl works on explicit permissions!

            //We make a copy of the current permissions

            List<FileSystemAccessRule> allCurrentPermissions = new List<FileSystemAccessRule>();
            allCurrentPermissions = getACLAsList();

            //first we remove all explicit permissions
            foreach (FileSystemAccessRule rule in allCurrentPermissions)
            {
                if (!rule.IsInherited)
                {
                    removeACE(rule);
                }
            }

            //then we add the new explicit permissions
            foreach (FileSystemAccessRule rule in _newACL)
            {
                if (!rule.IsInherited)
                {
                    addACE(rule);
                }
            }
        }

        /// <summary>
        /// This function computes and returns the proprietary hash value for an ACE
        /// </summary>
        /// <param name="_ace">The ACE.</param>
        /// <returns></returns>
        public static int getACEHash(FileSystemAccessRule _ace)
        {
            //hier kein logging da problematisch für die Performanz!!!

            int hash = 17;
            hash = hash * 23 + _ace.IdentityReference.GetHashCode();
            hash = hash * 23 + _ace.AccessControlType.GetHashCode();
            hash = hash * 23 + _ace.FileSystemRights.GetHashCode();
            hash = hash * 23 + _ace.InheritanceFlags.GetHashCode();
            hash = hash * 23 + _ace.PropagationFlags.GetHashCode();
            hash = hash * 23 + _ace.IsInherited.GetHashCode();
            hash = hash * 23 + _ace.FileSystemRights.GetHashCode();

            return hash;
        }

        /// <summary>
        /// This function computes and returns the ACE in a readable form as text
        /// </summary>
        /// <param name="ACE">The ACE.</param>
        /// <returns></returns>
        public static string getACEString(FileSystemAccessRule ACE)
        {
            Log.Debug("Starting getACEString for securable");

            string desc = "";
            string user = "";
            try
            {
                user = ACE.IdentityReference.Translate(typeof(NTAccount)).ToString();
            }
            catch (IdentityNotMappedException exception)
            {
                user = "unknown";
            }

            desc = "Identity (SID): " + ACE.IdentityReference.ToString() + ";" + "User ID: " + user + ";" + "Rule type: " + ACE.AccessControlType.ToString() + ";" + "Permission: " + ACE.FileSystemRights.ToString() + ";" + "InheritanceFlags:" + ACE.InheritanceFlags.ToString() + ";" + "Propagation Flags:" + ACE.PropagationFlags.ToString() + ";" + "IsInherited:" + ACE.IsInherited.ToString() + ";" + "FileSystemRights:" + ACE.FileSystemRights.ToString() + Environment.NewLine;
            return desc;
        }

        public static string getACESMarkdownString(FileSystemAccessRule ACE)
        {
            Log.Debug("Starting getACEString for securable");

            string desc = "";
            string user = "";
            try
            {
                user = ACE.IdentityReference.Translate(typeof(NTAccount)).ToString();
            }
            catch (IdentityNotMappedException exception)
            {
                user = "unknown";
            }

            desc = "* **Identity (SID):** " + ACE.IdentityReference.ToString() + ";" + "**User ID:** " + user + ";" + "**Rule type:** " + ACE.AccessControlType.ToString() + ";" + "**Permission:** " + ACE.FileSystemRights.ToString() + ";" + "**InheritanceFlags:** " + ACE.InheritanceFlags.ToString() + ";" + "**Propagation Flags:** " + ACE.PropagationFlags.ToString() + ";" + "**IsInherited:** " + ACE.IsInherited.ToString() + ";" + "**FileSystemRights:** " + ACE.FileSystemRights.ToString() + Environment.NewLine;
            return desc;
        }

        /// <summary>
        /// This function changes the owner of the securable
        /// </summary>
        /// <param name="_SID">The new owner.</param>
        public void changeOwner(string _SID)

            //hier weiter eher SID
        {
            Log.Debug("Starting changeOwner for securable " + fullName);

            if (Privileges.EnablePrivilege(SecurityEntity.SE_RESTORE_NAME))
            {
                owner = _SID;
                changeOwnerInSD(_SID);
                applyACL();
            }
            else
            {
                MessageBox.Show("Error freeing permission SE_RESTORE_NAME");
            }
        }

        public void updateHash()
        {
            ACLHash = 17;
            ACLHash = ACLHash * 23 + owner.GetHashCode();
            ACLHash = ACLHash * 23 + inheritsParentPermissions.GetHashCode();

            foreach (FileSystemAccessRule rule in getACL())
            {
                ACLHash = ACLHash * 23 + getACEHash(rule);
            }

            //todo: achtung. Eigentlich sollte die reihenfolg der ACE keinen Einfluss
        }

        public void replaceAllSecuritySettings(Securable _securable)
        {
            if (this.ownerSID != _securable.ownerSID)
                if (Global.adminMode)
                    changeOwner(_securable.ownerSID);
                else
                {
                    MessageBox.Show("Changing the owner of the securable requires admin permissions");
                    return;
                }
            setInheritance(_securable.inheritsParentPermissions, false);
            replaceACL(_securable.getACL());
            applyACL();
            readAttributesFromDisk();
        }

        public List<FileSystemAccessRule> getACLAsList()
        {
            FileSystemAccessRule[] array = new FileSystemAccessRule[getACL().Count];
            getACL().CopyTo(array, 0);

            //then we make it an arraylist to use linq
            List<FileSystemAccessRule> return_value = new List<FileSystemAccessRule>(array);

            return return_value;
        }

        public string getSecurableAsText()
        {
            string text = "";
            text = "Permission report for securable: " + fullName + Environment.NewLine;
            text = text + "Owner: " + owner + Environment.NewLine;
            text = text + "Inherits parent permissions: " + inheritsParentPermissions.ToString() + Environment.NewLine;
            text = text + "Permission list:" + Environment.NewLine;

            text = getACL().Cast<FileSystemAccessRule>().Aggregate(text, (current, rule) => current + getACEString(rule));

            return text;
        }

        public string getSecurableAsMarkdown()
        {
            string text = "";
            text = "Permission report for securable: " + fullName + Environment.NewLine;
            text = text + "====" + Environment.NewLine;
            text = text + "Owner: " + owner + Environment.NewLine;
            text = text + "----" + Environment.NewLine;
            text = text + "Inherits parent permissions: " + inheritsParentPermissions.ToString() + Environment.NewLine;
            text = text + "----" + Environment.NewLine;
            text = text + "Permission list:" + Environment.NewLine;
            text = text + "----" + Environment.NewLine;

            text = getACL().Cast<FileSystemAccessRule>().Aggregate(text, (current, rule) => current + getACESMarkdownString(rule));

            return text;
        }

        //********************************************************
        //Virtual Functions
        //********************************************************

        public virtual void updateACEInSD(FileSystemAccessRule _newACE, FileSystemAccessRule _oldACE)
        {
            MessageBox.Show("This shall never happen!!!!");
        }

        public virtual void addACEToSD(FileSystemAccessRule _ACE)
        {
            MessageBox.Show("This shall never happen!!!!");
        }

        public virtual void removeACEFromSD(FileSystemAccessRule _ACE)
        {
            MessageBox.Show("This shall never happen!!!!");
        }

        public virtual bool applyACL()
        {
            MessageBox.Show("This shall never happen!!!!");
            return true;
        }

        public virtual void changeOwnerInSD(string _sid)
        {
            MessageBox.Show("This shall never happen!!!!");
        }

        public virtual void setInheritance(bool _inheritance, bool _copy)
        {
            MessageBox.Show("This shall never happen!!!!");
        }

        public virtual AuthorizationRuleCollection getACL()
        {
            MessageBox.Show("This shall never happen!!!!");
            return null;
        }

        public virtual void readAttributesFromDisk()
        {
            MessageBox.Show("This shall never happen!!!!");
        }

        //********************************************************
        //Following are the functions for effective permissions
        //********************************************************
        internal FileSystemRights getEffectiveRightsForCurrentUser()
        {
            if (string.IsNullOrEmpty(Environment.UserName))
            {
                throw new ArgumentException("userName");
            }

            FileSystemAccessRule[] accessRules = GetAccessRulesArray(Environment.UserName, fullName);

            FileSystemRights denyRights = 0;
            FileSystemRights allowRights = 0;

            if (accessRules == null) return allowRights;

            for (int index = 0, total = accessRules.Length; index < total; index++)
            {
                FileSystemAccessRule rule = accessRules[index];

                if (rule.AccessControlType == AccessControlType.Deny)
                {
                    denyRights |= rule.FileSystemRights;
                }
                else
                {
                    allowRights |= rule.FileSystemRights;
                }
            }

            return (allowRights | denyRights) ^ denyRights;
        }

        private FileSystemAccessRule[] GetAccessRulesArray(string userName, string path)
        {
            List<FileSystemAccessRule> rules = new List<FileSystemAccessRule>();

            try
            {
                //First all rules for the securable are obtained

                //AuthorizationRuleCollection authorizationRules = (new FileInfo(path)).GetAccessControl().GetAccessRules(true, true, typeof(SecurityIdentifier));
                //  _ds = _di.GetAccessControl(AccessControlSections.Access);
                AuthorizationRuleCollection authorizationRules = getACL();

                SecurityIdentifier adminGroup = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);

                //then we loop thru them and see whether they match a group of the user
                foreach (FileSystemAccessRule rule in authorizationRules)
                {
                    //filter admin mode
                    if ((rule.IdentityReference == adminGroup) && (!Global.adminMode))
                    {
                        //if we are not in admin mode and this rule is about an admin, we ignore it
                       
                    }
                    else
                    {
                        //It the rule is dedicated to the user, we add the rule
                        if (rule.IdentityReference.ToString() == Global.currentIdentity.Owner.ToString())
                        { rules.Add(rule); }

                        //if the rule is for a group that the user belongs to, we do the same
                        if (Global.settings.allLocalGroupsOfCurrentUser.Contains(rule.IdentityReference.Value))
                            rules.Add(rule);

                        //fixme: what about domain gropus

                    }
                }

                return rules.ToArray();
            }
            catch (UnauthorizedAccessException ex)
            {
                return null;
            }
        }

        public bool shouldSecurableBeShown()
        {
            //this function is used to evaluate whether a file/folder should be shown at all.

            bool ret = false;

            if (readError)
                return false;

            if (Global.settings.showOnlyReadableFolders)
            {
                if (GetType() == typeof(Folder))
                {
                    //We are looking at a folder
                    ret = hasUserThisRight(FileSystemRights.ReadPermissions) && hasUserThisRight(FileSystemRights.Read) && hasUserThisRight(FileSystemRights.ListDirectory);
                }
                else
                {
                    //we are looking at a file

                    ret = hasUserThisRight(FileSystemRights.ReadPermissions) && hasUserThisRight(FileSystemRights.Read);
                }
            }
            else if (Global.settings.showOnlyChangableFolders)
            {
                ret = hasUserThisRight(FileSystemRights.ReadPermissions) && hasUserThisRight(FileSystemRights.ChangePermissions);
            }
            return ret;

        }

        public bool hasUserThisRight(FileSystemRights _right)
        {
            //Special case: Current user is owner

            if (Global.currentIdentity.Name == this.owner)
                return true;

            try
            {
                return effectiveRightsForCurrentUser.hasRights(_right);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in hasUserThisRight " + ex.Message);
            }
            return false;
        }
    }

    public static class FileSystemRightsEx
    {
        public static bool hasRights(this FileSystemRights rights, FileSystemRights testRights)
        {
            return (rights & testRights) == testRights;
        }
    }
}