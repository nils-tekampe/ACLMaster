using log4net;
using System;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace ACLMaster
{
    internal sealed class File : Securable
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private FileSecurity _fs;

        public File(string _link)
            : base(_link)
        {
            isFile = true;

            readAttributesFromDisk();
            effectiveRightsForCurrentUser = getEffectiveRightsForCurrentUser();
            locked = !hasUserThisRight(FileSystemRights.ChangePermissions);
        }

        /// <summary>
        /// This function applies the whole security descriptor back to the file on disk. This is the only function that really changes permissions.
        /// </summary>
        public override bool applyACL()
        {
            log.Debug("Starting applyACL for file " + fullName);
            try
            {
                System.IO.File.SetAccessControl(fullName, _fs);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error while applyingACL to file. Error: " + ex.ToString());
                return false;
            }
        }

        public override void changeOwnerInSD(string _sid)
        {
            IdentityReference newUser = new SecurityIdentifier(_sid);

            _fs.SetOwner(newUser);
        }

        public override void updateACEInSD(FileSystemAccessRule _newACE, FileSystemAccessRule _oldACE)
        {
            _fs.RemoveAccessRule(_oldACE);
            _fs.AddAccessRule(_newACE);
        }

        public override void addACEToSD(FileSystemAccessRule _ACE)
        {
            if (_ACE.IsInherited)
                MessageBox.Show("Trying to add an inherited ACE");
            else
                _fs.AddAccessRule(_ACE);
        }

        public override void removeACEFromSD(FileSystemAccessRule _ACE)
        {
            if (_ACE.IsInherited)
                MessageBox.Show("Trying to remove an inherited ACE");
            else

                _fs.RemoveAccessRule(_ACE);
        }

        public override void setInheritance(bool _inheritance, bool _copy)
        {
            _fs.SetAccessRuleProtection(!_inheritance, _copy);

            inheritsParentPermissions = _inheritance;
        }

        public override AuthorizationRuleCollection getACL()
        {
            return _fs.GetAccessRules(true, true, typeof(SecurityIdentifier));
        }

        public override void readAttributesFromDisk()
        {
            name = Path.GetFileName(fullName);
            lastUpdate = System.IO.File.GetLastWriteTime(fullName);

            try
            {
                _fs = System.IO.File.GetAccessControl(fullName);
                inheritsParentPermissions = !_fs.AreAccessRulesProtected;

                ownerSID = System.IO.File.GetAccessControl(fullName).GetOwner(typeof(SecurityIdentifier)).ToString();

                owner = _fs.GetOwner(typeof(NTAccount)).ToString();

                knownOwner = true;
                updateHash();
            }

            catch (UnauthorizedAccessException e)
            {
                log.Debug("Unauthorized Access Exception in Constructor for file: ", e);
                readError = true;
            }
            catch (IdentityNotMappedException ex)
            {
                owner = "";
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