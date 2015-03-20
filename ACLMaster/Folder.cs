using log4net;
using System;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace ACLMaster
{
    /// <summary>
    /// This class represents a folder.
    /// </summary>
    internal sealed class Folder : Securable
    {
        private DirectoryInfo _di;
        private DirectorySecurity _ds;
        public string displayName;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Folder(string _link)
            : base(_link)
        {
            displayName = "";
            isFile = false;

            readAttributesFromDisk();
            if (!readError)
            {
                effectiveRightsForCurrentUser = getEffectiveRightsForCurrentUser();
                locked = !hasUserThisRight(FileSystemRights.ChangePermissions);
            }
        }

        public Folder(string _link, string _displayName)
            : base(_link)
        {
            displayName = _displayName;
        }

        public override bool applyACL()
        {
            try
            {
                Directory.SetAccessControl(fullName, _ds);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error while applyingACL to folder. Error: " + ex.ToString());
                return false;
            }
        }

        public override void updateACEInSD(FileSystemAccessRule _newACE, FileSystemAccessRule _oldACE)
        {
            _ds.RemoveAccessRule(_oldACE);
            _ds.AddAccessRule(_newACE);
        }

        public override void addACEToSD(FileSystemAccessRule _ACE)
        {
            if (_ACE.IsInherited)
                MessageBox.Show("Trying to add an inherited ACE");
            else
                _ds.AddAccessRule(_ACE);
        }

        public override void removeACEFromSD(FileSystemAccessRule _ACE)
        {
            if (_ACE.IsInherited)
                MessageBox.Show("Trying to remove an inherited ACE");
            else

                _ds.RemoveAccessRule(_ACE);
        }

        public override void changeOwnerInSD(string _sid)
        {
            try
            {
                IdentityReference newUser = new SecurityIdentifier(_sid);
                _ds.SetOwner(newUser);
            }
            catch (Exception ex)
            {
                log.Error("Error while changing owner of securable. Error: " + ex.ToString());
            }
        }

        public override void setInheritance(bool _inheritance, bool _copy)
        {
            _ds.SetAccessRuleProtection(!_inheritance, _copy);
            inheritsParentPermissions = _inheritance;
        }

        public override AuthorizationRuleCollection getACL()
        {
            try
            {
                return _ds.GetAccessRules(true, true, typeof(SecurityIdentifier));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()+this.fullName );
                return null;
            }
       
        }

        public override void readAttributesFromDisk()
        {
            name = Path.GetFileName(fullName);

            _di = new DirectoryInfo(fullName);
            lastUpdate = _di.LastWriteTime;

            try
            {
                ownerSID = System.IO.File.GetAccessControl(fullName).GetOwner(typeof(SecurityIdentifier)).ToString();
                owner = _di.GetAccessControl().GetOwner(typeof(NTAccount)).ToString();
                _ds = _di.GetAccessControl(AccessControlSections.Access);

                knownOwner = true;
                inheritsParentPermissions = !_ds.AreAccessRulesProtected;
                updateHash();
            }

            catch (UnauthorizedAccessException e)
            {
                log.Debug("Unauthorized Access Exception in Constructor for folder: ", e);
                readError = true;
            }
            catch (IdentityNotMappedException ex)
            {
                ownerSID = System.IO.File.GetAccessControl(fullName).GetOwner(typeof(SecurityIdentifier)).ToString();
                knownOwner = false;
            }
            catch (Exception e)
            {
                log.Debug("  Exception in Constructor for folder: ", e);
                readError = true;
            }
        }
    }
}