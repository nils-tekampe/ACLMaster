using BrightIdeasSoftware;
using log4net;
using log4net.Config;
using log4net.Util;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace ACLMaster
{
    public delegate void OnPermissionCreated(object sender);

    public delegate void OnPermissionChangedWithDetails(object source, NotifyCollectionChangedEventWithDetailsArgs e);

    internal class Global
    {
        public static DriveList drives;
        // public static OrderedDictionary securables;

        public static bool domainInformationRetreived;

        //this is the current user
        public static WindowsIdentity currentIdentity;

        public static Securable clipboardSecurable;
        public static bool clipboardInheritance;

        public static bool adminMode = false;

        public static List<Securable> listedSecurables;

        public static TrulyObservableCollection<FileSystemAccessRuleExtended> listedPermissions;

        public static FileSystemAccessRule clipboardACE;

        public static CustomSettings settings;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public enum permissions
        {
            AppendData = FileSystemRights.AppendData,
            ChangePermissions = FileSystemRights.ChangePermissions,
            CreateDirectories = FileSystemRights.CreateDirectories,
            CreateFiles = FileSystemRights.CreateFiles,
            Delete = FileSystemRights.Delete,
            DeleteSubdirectoriesAndFiles = FileSystemRights.DeleteSubdirectoriesAndFiles,
            ExecuteFile = FileSystemRights.ExecuteFile,
            FullControl = FileSystemRights.FullControl,
            ListDirectory = FileSystemRights.ListDirectory,
            Modify = FileSystemRights.Modify,
            Read = FileSystemRights.Read,
            ReadAndExecute = FileSystemRights.ReadAndExecute,
            ReadAttributes = FileSystemRights.ReadAttributes,
            ReadData = FileSystemRights.ReadData,
            ReadExtendedAttributes = FileSystemRights.ReadExtendedAttributes,
            ReadPermissions = FileSystemRights.ReadPermissions,
            Synchronize = FileSystemRights.Synchronize,
            TakeOwnership = FileSystemRights.TakeOwnership,
            Traverse = FileSystemRights.Traverse,
            Write = FileSystemRights.Write,
            WriteAttributes = FileSystemRights.WriteAttributes,
            WriteData = FileSystemRights.WriteData,
            WriteExtendedAttributes = FileSystemRights.WriteExtendedAttributes
        }

        public static List<permissions> getPermissions()
        {
            return Global.permissions.GetValues(typeof(Global.permissions)).Cast<Global.permissions>().OrderByDescending(x => (int)x).ToList();
        }

        public enum complexPermissions
        {
            AppendData = FileSystemRights.AppendData,
            ChangePermissions = FileSystemRights.ChangePermissions,
            CreateDirectories = FileSystemRights.CreateDirectories,
            CreateFiles = FileSystemRights.CreateFiles,

            // Delete = FileSystemRights.Delete,
            DeleteSubdirectoriesAndFiles = FileSystemRights.DeleteSubdirectoriesAndFiles,

            //ExecuteFile = FileSystemRights.ExecuteFile,
            //FullControl = FileSystemRights.FullControl,
            //ListDirectory = FileSystemRights.ListDirectory,
            //Modify = FileSystemRights.Modify,
            //Read = FileSystemRights.Read,
            //ReadAndExecute = FileSystemRights.ReadAndExecute,
            ReadAttributes = FileSystemRights.ReadAttributes,

            ReadData = FileSystemRights.ReadData,
            ReadExtendedAttributes = FileSystemRights.ReadExtendedAttributes,
            ReadPermissions = FileSystemRights.ReadPermissions,
            Synchronize = FileSystemRights.Synchronize,
            TakeOwnership = FileSystemRights.TakeOwnership,
            Traverse = FileSystemRights.Traverse,

            // Write = FileSystemRights.Write,
            WriteAttributes = FileSystemRights.WriteAttributes,

            WriteData = FileSystemRights.WriteData,
            WriteExtendedAttributes = FileSystemRights.WriteExtendedAttributes
        }

        public static List<complexPermissions> getComplexPermissions()
        {
            return Global.complexPermissions.GetValues(typeof(Global.complexPermissions)).Cast<Global.complexPermissions>().OrderByDescending(x => (int)x).ToList();
        }

        public enum simplePermissions
        {
            //AppendData = FileSystemRights.AppendData,
            //ChangePermissions = FileSystemRights.ChangePermissions,
            //CreateDirectories = FileSystemRights.CreateDirectories,
            //CreateFiles = FileSystemRights.CreateFiles,
            Delete = FileSystemRights.Delete,

            //DeleteSubdirectoriesAndFiles = FileSystemRights.DeleteSubdirectoriesAndFiles,
            ExecuteFile = FileSystemRights.ExecuteFile,

            FullControl = FileSystemRights.FullControl,
            ListDirectory = FileSystemRights.ListDirectory,
            Modify = FileSystemRights.Modify,
            Read = FileSystemRights.Read,
            ReadAndExecute = FileSystemRights.ReadAndExecute,

            //ReadAttributes = FileSystemRights.ReadAttributes,
            //ReadData = FileSystemRights.ReadData,
            //ReadExtendedAttributes = FileSystemRights.ReadExtendedAttributes,
            //ReadPermissions = FileSystemRights.ReadPermissions,
            //Synchronize = FileSystemRights.Synchronize,
            //TakeOwnership = FileSystemRights.TakeOwnership,
            //Traverse = FileSystemRights.Traverse,
            Write = FileSystemRights.Write,

            //WriteAttributes = FileSystemRights.WriteAttributes,
            //WriteData = FileSystemRights.WriteData,
            //WriteExtendedAttributes = FileSystemRights.WriteExtendedAttributes
        }

        public static List<simplePermissions> getSimplePermissions()
        {
            return Global.simplePermissions.GetValues(typeof(Global.simplePermissions)).Cast<Global.simplePermissions>().OrderByDescending(x => (int)x).ToList();
        }

        public static void init()
        {
            listedSecurables = new List<Securable>();

            //now let's start logging
#if DEBUG
            LogLog.InternalDebugging = true;

#endif

            settings = new CustomSettings();
            if (System.IO.File.Exists(settings.settingsFile))
                settings = CustomSettings.read(settings.settingsFile);

            if (Global.settings.logVerbose)
                XmlConfigurator.Configure(new FileInfo("log4netVerbose.config"));
            else
                XmlConfigurator.Configure(new FileInfo("log4netStandard.config"));

            log.Debug("Starting Global.init");

            adminMode = UacHelper.IsProcessElevated;

            if (Global.settings.startEscalated)
            {
                if (!adminMode)
                {
                 GroupsAndUsers.elevatePrivileges();
                }
            }

            if (Global.settings.upgraded == false)
            {
                Global.settings.upgraded = true;
            }

            //Registering global error handling
            if (Debugger.IsAttached)
            {
            }
            else
            {
                Application.ThreadException += ApplicationThreadException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;

            }

            //init local variables

            getEnvironment();

            //read groups and users only if outdated

    
               GroupsAndUsers.getGroupsAndUsers();
          

            drives = new DriveList();

            domainInformationRetreived = false;
        }

        public static List<FileSystemAccessRule> getCommonPermissions(
             ListView.SelectedListViewItemCollection listOfSecurables)
        {
            //ListView.SelectedListViewItemCollection

            Securable base_securable = (Securable)((OLVListItem)listOfSecurables[0]).RowObject;
            List<FileSystemAccessRule> baseList = base_securable.getACLAsList();

            //the we loop thru the rest of the listOfSecurables

            for (int i = 1; i < listOfSecurables.Count; i++)
            {
                if (((Securable)((OLVListItem)listOfSecurables[i]).RowObject).ACLHash != ((Securable)((OLVListItem)listOfSecurables[i - 1]).RowObject).ACLHash)
                {
                    List<FileSystemAccessRule> mergerList = ((Securable)((OLVListItem)listOfSecurables[i]).RowObject).getACLAsList();

                    baseList = baseList.Intersect(mergerList, new FileSystemAccessRulesComparer()).ToList();
                }
            }

            return baseList;
        }

        private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
        }

        private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //ErrorInformationDialog eid = new ErrorInformationDialog(e.ExceptionObject as Exception);
            //eid.ShowDialog();
        }

        public static void getEnvironment()
        {
            log.Error("starting getEnvironment");

            //Let's get the current user
            currentIdentity = WindowsIdentity.GetCurrent();
            Global.settings.currentUser = currentIdentity.Name;

            //Then we see whether the machine is domain joined
            try
            {
                ManagementObject ComputerSystem;
                using (
                    ComputerSystem =
                        new ManagementObject(String.Format("Win32_ComputerSystem.Name='{0}'", Environment.MachineName)))
                {
                    ComputerSystem.Get();
                    object Result = ComputerSystem["PartOfDomain"];
                    Global.settings.machineIsDomainJoined = (bool)Result;

                    //let's save the domain name for further use
                    string domainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
                    Global.settings.domain = domainName;
                }
            }
            catch (ActiveDirectoryObjectNotFoundException e)
            {
                MessageBox.Show("that should never happen");
                Global.settings.machineIsDomainJoined = false;
            }
        }

       

        public static void shiftLastUsers(string _SID, string _user)
        {
            if ((Global.settings.nameLastUser1 == _user) || (Global.settings.nameLastUser2 == _user) ||
                (Global.settings.nameLastUser3 == _user) || (Global.settings.nameLastUser4 == _user) ||
                (Global.settings.nameLastUser5 == _user))
                return;

            Global.settings.nameLastUser5 = Global.settings.nameLastUser4;
            Global.settings.nameLastUser4 = Global.settings.nameLastUser3;
            Global.settings.nameLastUser3 = Global.settings.nameLastUser2;
            Global.settings.nameLastUser2 = Global.settings.nameLastUser1;

            Global.settings.sidLastUser5 = Global.settings.sidLastUser4;
            Global.settings.sidLastUser4 = Global.settings.sidLastUser3;
            Global.settings.sidLastUser3 = Global.settings.sidLastUser2;
            Global.settings.sidLastUser2 = Global.settings.sidLastUser1;

            Global.settings.nameLastUser1 = _user;
            Global.settings.sidLastUser1 = _SID;
        }

        public void updateListOfLastUsers(string SID)
        {
        }
    }

    public class FileSystemAccessRulesComparer : IEqualityComparer<FileSystemAccessRule>
    {
        public bool Equals(FileSystemAccessRule x, FileSystemAccessRule y)
        {
            return (GetHashCode(x) == GetHashCode(y));
        }

        public int GetHashCode(FileSystemAccessRule ACE)
        {
            return Securable.getACEHash(ACE).GetHashCode();
        }
    }

    [Serializable]
    public class Prcpl
    {
        public string Sid;
        public string Domain;
        public string Name;
        public string Upn;
        public bool currentUserIsMember;

        public Prcpl()
        {
        }

        public Prcpl(string _sid, string _domain, string _name, string _upn)
        {

            if (_sid == null)
                Sid = "";
            else
                Sid = _sid;

            if (_sid == null)
                Sid = "";
            else
                Sid = _sid;

            if (_name == null)
                Name = "";
            else
                Name = _name;

            if (_upn == null)
                Upn = "";
            else
                Upn = _upn;

            if (_domain == null)
                Domain = "";
            else
                Domain = _domain;

            currentUserIsMember = false;
        }

        public Prcpl(string _sid, string _domain, string _name, string _upn, bool _currentUserIsMember)
        {

            if (_sid == null)
                Sid = "";
            else
                Sid = _sid;

            if (_sid == null)
                Sid = "";
            else
                Sid = _sid;

            if (_name == null)
                Name = "";
            else
                Name = _name;

            if (_upn == null)
                Upn = "";
            else
                Upn = _upn;

            if (_domain == null)
                Domain = "";
            else
                Domain = _domain;

            currentUserIsMember = _currentUserIsMember;
        }
    }

    public static class UacHelper
    {
        private const string uacRegistryKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
        private const string uacRegistryValue = "EnableLUA";

        private static uint STANDARD_RIGHTS_READ = 0x00020000;
        private static uint TOKEN_QUERY = 0x0008;
        private static uint TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool GetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass, IntPtr TokenInformation, uint TokenInformationLength, out uint ReturnLength);

        public enum TOKEN_INFORMATION_CLASS
        {
            TokenUser = 1,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId,
            TokenGroupsAndPrivileges,
            TokenSessionReference,
            TokenSandBoxInert,
            TokenAuditPolicy,
            TokenOrigin,
            TokenElevationType,
            TokenLinkedToken,
            TokenElevation,
            TokenHasRestrictions,
            TokenAccessInformation,
            TokenVirtualizationAllowed,
            TokenVirtualizationEnabled,
            TokenIntegrityLevel,
            TokenUIAccess,
            TokenMandatoryPolicy,
            TokenLogonSid,
            MaxTokenInfoClass
        }

        public enum TOKEN_ELEVATION_TYPE
        {
            TokenElevationTypeDefault = 1,
            TokenElevationTypeFull,
            TokenElevationTypeLimited
        }

        public static bool IsUacEnabled
        {
            get
            {
                using (RegistryKey uacKey = Registry.LocalMachine.OpenSubKey(uacRegistryKey, false))
                {
                    bool result = uacKey.GetValue(uacRegistryValue).Equals(1);
                    return result;
                }
            }
        }

        public static bool IsProcessElevated
        {
            get
            {
                if (IsUacEnabled)
                {
                    IntPtr tokenHandle = IntPtr.Zero;
                    if (!OpenProcessToken(Process.GetCurrentProcess().Handle, TOKEN_READ, out tokenHandle))
                    {
                        throw new ApplicationException("Could not get process token.  Win32 Error Code: " +
                                                       Marshal.GetLastWin32Error());
                    }

                    try
                    {
                        TOKEN_ELEVATION_TYPE elevationResult = TOKEN_ELEVATION_TYPE.TokenElevationTypeDefault;

                        int elevationResultSize = Marshal.SizeOf((int)elevationResult);
                        uint returnedSize = 0;

                        IntPtr elevationTypePtr = Marshal.AllocHGlobal(elevationResultSize);
                        try
                        {
                            bool success = GetTokenInformation(tokenHandle, TOKEN_INFORMATION_CLASS.TokenElevationType,
                                                               elevationTypePtr, (uint)elevationResultSize,
                                                               out returnedSize);
                            if (success)
                            {
                                elevationResult = (TOKEN_ELEVATION_TYPE)Marshal.ReadInt32(elevationTypePtr);
                                bool isProcessAdmin = elevationResult == TOKEN_ELEVATION_TYPE.TokenElevationTypeFull;
                                return isProcessAdmin;
                            }
                            else
                            {
                                throw new ApplicationException("Unable to determine the current elevation.");
                            }
                        }
                        finally
                        {
                            if (elevationTypePtr != IntPtr.Zero)
                                Marshal.FreeHGlobal(elevationTypePtr);
                        }
                    }
                    finally
                    {
                        if (tokenHandle != IntPtr.Zero)
                            CloseHandle(tokenHandle);
                    }
                }
                else
                {
                    WindowsIdentity identity = WindowsIdentity.GetCurrent();
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    bool result = principal.IsInRole(WindowsBuiltInRole.Administrator)
                               || principal.IsInRole(0x200); //Domain Administrator
                    return result;
                }
            }
        }
    }

    public class DomainQueryWorker
    {
        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop;

        public volatile bool readCompleted = true;

        // This method will be called when the thread is started.
        public void DoWork()
        {
            //if (!readAllDomainUsers())
            //{
            //    Global.settings.readDomainInformationFailed = true;
            //    readCompleted = false;
            //}

            //if (!readAllDomainGroups())
            //{
            //    Global.settings.readDomainInformationFailed = true;
            //    readCompleted = false;
            //}

            if (!readAllLocalGroupsOfCurrentUser())
            {
                Global.settings.readDomainInformationFailed = true;
                readCompleted = false;
            }
        }

        public void RequestStop()
        {
            _shouldStop = true;
        }


        private bool readAllLocalGroupsOfCurrentUser()
        {
            string resultStr = "";

            try
            {
                foreach (IdentityReference group in WindowsIdentity.GetCurrent().Groups)
                {

                    //      Global.settings.allLocalGroupsOfCurrentUser.Add(iterGroup.Current.Sid.ToString(), new Prcpl(iterGroup.Current.Sid.ToString(), iterGroup.Current.Context.Name, iterGroup.Current.Name, iterGroup.Current.UserPrincipalName));
                    resultStr = resultStr + group.Translate(typeof(SecurityIdentifier)).Value + "\n";
                    // resultStr = resultStr + group.Translate(typeof(NTAccount)).Value + "\n";


                }
                return true;
            }
            catch (Exception ex)
            {
                //todo
            }
            return false;

        }

    }

    public class FileSystemAccessRuleExtended : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler tempHandler = PropertyChanged;
            if (tempHandler != null)
                tempHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        private FileSystemAccessRule _fsar;
        private bool constructor = false;
        public bool selected = false;

        public FileSystemAccessRule fsar
        {
            get { return _fsar; }
            set
            {
                if ((_fsar != value) && (!constructor))
                {
                    //this is a real change of the variable
                    _fsar = value;
                    OnPropertyChanged("fsar");
                }
                else if (constructor)
                {
                    //this is the init. No event fired.
                    _fsar = value;
                }
            }
        }

        public FileSystemAccessRule fsarOriginal;

        private bool _changed;

        public bool changed
        {
            get { return _changed; }
            set
            {
                if ((_changed != value) && (!constructor))
                {
                    _changed = value;
                    OnPropertyChanged("changed");
                }
                else if (constructor)
                {
                    _changed = value;
                }
            }
        }

        private bool _added;

        public bool added
        {
            get { return _added; }
            set
            {
                if ((_added != value) && (!constructor))
                {
                    _added = value;
                    OnPropertyChanged("added");
                }
                else if (constructor)
                {
                    _added = value;
                }
            }
        }

        public bool _deleted;

        public bool deleted
        {
            get { return _deleted; }
            set
            {
                if ((_deleted != value) && (!constructor))
                {
                    _deleted = value;
                    OnPropertyChanged("deleted");
                }
                else if (constructor)
                {
                    _deleted = value;
                }
            }
        }

        public FileSystemAccessRuleExtended(FileSystemAccessRule _fsar)
        {
            constructor = true;
            fsar = _fsar;
            fsarOriginal = _fsar;
            changed = false;
            added = false;
            constructor = false;
        }
    }

    public class TrulyObservableCollection<T> : ObservableCollection<T>
 where T : INotifyPropertyChanged
    {
        public event OnPermissionChangedWithDetails myChangeEvent;

        protected void MyPropertyChanged(object changedObject)
        {
            myChangeEvent(this, new NotifyCollectionChangedEventWithDetailsArgs(changedObject));

            //    NotifyCollectionChangedEventWithDetailsArgs tempHandler = myChangeEvent;
            //    if (tempHandler != null)

            //        tempHandler(new NotifyCollectionChangedEventWithDetailsArgs(changedObject));
        }

        public TrulyObservableCollection()
            : base()
        {
            CollectionChanged += new NotifyCollectionChangedEventHandler(TrulyObservableCollection_CollectionChanged);
        }

        private void TrulyObservableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Object item in e.NewItems)
                {
                    (item as INotifyPropertyChanged).PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
                }
            }
            if (e.OldItems != null)
            {
                foreach (Object item in e.OldItems)
                {
                    (item as INotifyPropertyChanged).PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
                }
            }
        }

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyCollectionChangedEventWithDetailsArgs a = new NotifyCollectionChangedEventWithDetailsArgs(NotifyCollectionChangedAction.Reset);
            // a.changedObject = sender;
            MyPropertyChanged(sender);
            //  OnCollectionChanged(a);
        }
    }

    public class NotifyCollectionChangedEventWithDetailsArgs : EventArgs
    {
        public object changedObject;

        public NotifyCollectionChangedEventWithDetailsArgs(object _changedObject)
        {
            changedObject = _changedObject;
        }
    }

    public class ComboItem : object
    {
        private string _name;
        public object _tag;

        public ComboItem(String name, object tag)
        {
            _name = name;
            _tag = tag;
        }

        public override string ToString()
        {
            return _name;
        }
    };
}