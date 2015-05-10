using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using Tulpep.ActiveDirectoryObjectPicker;

namespace ACLMaster
{
    internal class GroupsAndUsers
    {
        /// <summary>
        ///     Reads all the groups and users.
        ///     Here is what it does in detail:
        /// </summary>
        public static void getGroupsAndUsers()
        {
            //if ((Global.settings.validityPeriodGroupsAndUsers != 0) && ((DateTime.Now - Global.settings.dateOfLastScan).TotalDays < Global.settings.validityPeriodGroupsAndUsers))
            //{
            //    return;
            //}

            if (MessageBox.Show("We need to acquire information about the groups and users on the local machine and (potentially) the domain that the machine belongs to. This may take a while.", "Acquiring group and user information", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {

                return;
            }


            //initialize the dictionaries
            //Global.settings.allLocalUsers = new OrderedDictionary();
            //lobal.settings.allDomainUsers = new OrderedDictionary();
            //Global.settings.allLocalGroups = new OrderedDictionary();
           // Global.settings.allDomainGroups = new OrderedDictionary();
            Global.settings.allLocalGroupsOfCurrentUser = new OrderedDictionary();
            Global.settings.allDomainGroupsOfCurrentUser = new OrderedDictionary();
            bool domainSuccess = true;

          //  readAllLocalUsers();
           // readAllLocalGroups();


            ReadAllLocalGroupsOfCurrentUser();
            readAllDomainGroupsOfCurrentUser();

            if (Global.settings.machineIsDomainJoined && !Global.settings.localOnly)
            {
                //Here we are in the domain context because the machine id domain joined.
                //This means that we can safely try to read all groups and users from thd PDC.
                try
                {
                   // readAllDomainUsers();
                   // readAllDomainGroups();
                }
                catch (PrincipalServerDownException ex)
                {
                    if (MessageBox.Show("The domain server seems to be down. Shall we work locally?", "Server down", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        domainSuccess = false;
                    }
                }
            }

            ////Next wer are interested in the groups of the user. So let's see whether it is a local one or one from the domain.
            //if (isCurrentUserLocalUser()|| !domainSuccess)
            //{
            //    ReadAllLocalGroupsOfCurrentUser();
            //}
            //else
            //{
            //    //case domain user. We need to get the local groups as well as the AD groups
            //    try
            //    {
            //      //  readAllLocalGroupsOfCurrentDomainUser();
            //       // readAllDomainGroupsOfCurrentDomainUser();
            //    }
            //    catch (PrincipalServerDownException ex)
            //    {
            //        if (MessageBox.Show("The domain server seems to be down. Shall we work locally?", "Server down", MessageBoxButtons.YesNo) == DialogResult.No)
            //        {
            //            Application.Exit();
            //        }
            //        else
            //        {
            //            domainSuccess = false;
            //        }
            //    }
            //}

       
            //for convenience we will mark the groups that the current user is a member of

          //  MarkGroups();

            if (domainSuccess)
            {
                Global.settings.dateOfLastScan = DateTime.Now;
            }


            CustomSettings.save(Global.settings, Global.settings.settingsFile);
        }

        public static void elevatePrivileges()
        {
            var proc = new ProcessStartInfo();
            proc.UseShellExecute = true;
            proc.WorkingDirectory = Environment.CurrentDirectory;
            proc.FileName = Application.ExecutablePath;

            proc.Verb = "runas";

            try
            {
                Process.Start(proc);
            }
            catch
            {
                // The user refused to allow privileges elevation.
                // Do nothing and return directly ...
                MessageBox.Show("Could not obtain permission to start with escalated privileges. Will start with standard permissions");
                return;
            }

            Environment.Exit(0);
        }

        //private static void MarkGroups()
        //{
        //    foreach (DictionaryEntry de in Global.settings.allLocalGroups)
        //    {
        //        if (Global.settings.allLocalGroupsOfCurrentUser.Contains(de.Key))
        //            ((Prcpl)de.Value).currentUserIsMember = true;
        //    }

        //    //todo: domain groups
        //}

        /// <summary>
        ///     Reads all groups of current local user.
        /// </summary>
        private static void ReadAllLocalGroupsOfCurrentUser()
        {

            var context = new PrincipalContext(ContextType.Machine, Environment.MachineName);
            UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, Environment.UserName);
           
            foreach (GroupPrincipal group in userPrincipal.GetAuthorizationGroups())
            {
                
                 Global.settings.allLocalGroupsOfCurrentUser.Add(@group.Sid.ToString(), new Prcpl(@group.Sid.ToString(), @group.Context.Name, @group.Name, @group.UserPrincipalName));

        
           
            }

//            MessageBox.Show(resultStr);
            string test;
            test="";



            //var context = new PrincipalContext(ContextType.Machine, Environment.MachineName);
            //UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, Environment.UserName);

            //foreach (GroupPrincipal group in userPrincipal.GetAuthorizationGroups())
            //{
            //    Global.settings.allLocalGroupsOfCurrentUser.Add(@group.Sid.ToString(), new Prcpl(@group.Sid.ToString(), @group.Context.Name, @group.Name, @group.UserPrincipalName));
            //}
        }

        //private static void readAllLocalGroupsOfCurrentDomainUser()
        //{
        //    var root = new DirectoryEntry(String.Format("WinNT://{0},Computer", Environment.MachineName), null, null, AuthenticationTypes.Secure);

        //    foreach (DirectoryEntry groupDirectoryEntry in root.Children)
        //    {
        //        if (groupDirectoryEntry.SchemaClassName != "Group")
        //            continue;

        //        string groupName = groupDirectoryEntry.Name;
        //        Console.WriteLine("Checking: {0}", groupName);

        //        if (IsUserMemberOfGroup(groupDirectoryEntry, String.Format("WinNT://{0}/{1}", "tuvit", Environment.UserName)))
        //        {
        //            MessageBox.Show(groupName);
        //        }
        //    }
        //}

        private static void readAllDomainGroupsOfCurrentUser()
        {
            //(gute Basis)


            string resultStr = "";
            // Objekt für AD-Abfrage erzeugen
            using (DirectorySearcher searcher = new DirectorySearcher(new DirectoryEntry(string.Empty)))
            {
                // nach Kriterium filtern - hier nach Gruppe mit einem best. Namen (Inhalt von 'username')
                searcher.Filter = string.Concat(string.Format(@"(&(ObjectClass=user)(sAMAccountName={0}))", Environment.UserName));

                // Anfrage mit gesetzteen Filter ausführen und Ergebnisse durch iterieren
                foreach (SearchResult result in searcher.FindAll())
                {
                    // Eigenschaft 'MemberOf' des AD-Knotenpunktes 'result' durch iterieren
                    foreach (var group in result.Properties["MemberOf"])
                    {
                        // cast von 'group' zum Datentyp 'string' sollte nicht möglich sein, wird 'groupResult' 'null'
                        string groupResult = group as string;

                        if (groupResult != null)
                        {
                            // CN aus dem Pfad extrahieren und zur Liste hinzufügen
                            resultStr = resultStr + groupResult.Substring(3, groupResult.IndexOf(',') - 3) + "\n";

                            //  groups.Add(groupResult.Substring(3, groupResult.IndexOf(',') - 3));
                        }
                    }
                }
            }


          














            ////todo: not yet working: http://stackoverflow.com/questions/4460558/how-to-get-all-the-ad-groups-for-a-particular-user

            //UserPrincipal user = UserPrincipal.FindByIdentity(new PrincipalContext(ContextType.Domain, Environment.UserDomainName), IdentityType.SamAccountName, Global.currentIdentity.Name);

            //foreach (GroupPrincipal groupPrincipal in user.GetGroups())
            //{
            //    Prcpl prcpl = new Prcpl(groupPrincipal.Sid.ToString(), groupPrincipal.Context.Name, groupPrincipal.Name, groupPrincipal.UserPrincipalName);
            //    Global.settings.allDomainGroupsOfCurrentUser.Add(groupPrincipal.Sid.ToString(), prcpl);
            //}


        }

        private static bool IsUserMemberOfGroup(DirectoryEntry group, string userPath)
        {
            return (bool)@group.Invoke("IsMember", new object[] { userPath });
        }

        private static void readAllLocalGroups()
        {
            var AD = new PrincipalContext(ContextType.Machine, Environment.MachineName);
            var group = new GroupPrincipal(AD);
            var searchgroup = new PrincipalSearcher(@group);

            using (PrincipalSearchResult<Principal> allPrincipals = searchgroup.FindAll())

                foreach (GroupPrincipal groupPrincipal in allPrincipals.OfType<GroupPrincipal>())

                //Fixme: the following should also als the new dictionalry for the domain user if ready
                {
                    Prcpl prcpl = null;
                    if (Global.settings.allLocalGroupsOfCurrentUser.Contains(groupPrincipal.Sid.ToString()) || Global.settings.allLocalGroupsOfCurrentUser.Contains(groupPrincipal.Sid.ToString()))
                    {
                        //mark the principal so that the current user is a member of it
                        prcpl = new Prcpl(groupPrincipal.Sid.ToString(), groupPrincipal.Context.Name, groupPrincipal.Name, groupPrincipal.UserPrincipalName, true);
                    }
                    else
                    {
                        //create the principal without having the user a member of it.
                        prcpl = new Prcpl(groupPrincipal.Sid.ToString(), groupPrincipal.Context.Name, groupPrincipal.Name, groupPrincipal.UserPrincipalName);
                    }

                    Global.settings.allLocalGroups.Add(groupPrincipal.Sid.ToString(), prcpl);
                }
        }

        /// <summary>
        ///     Reads all domain groups.
        /// </summary>
        private static void readAllDomainGroups()
        {
            //todo: if the other solution works, it may also be used here

            var AD = new PrincipalContext(ContextType.Domain);
            var group = new GroupPrincipal(AD);
            var searchgroup = new PrincipalSearcher(@group);

            using (PrincipalSearchResult<Principal> allPrincipals = searchgroup.FindAll())

                foreach (GroupPrincipal groupPrincipal in allPrincipals.OfType<GroupPrincipal>())
                {
                    Global.settings.allDomainGroups.Add(groupPrincipal.Sid.ToString(), new Prcpl(groupPrincipal.Sid.ToString(), groupPrincipal.Context.Name, groupPrincipal.Name, groupPrincipal.UserPrincipalName));
                }
        }

        //private static void readAllLocalUsers()
        //{
        //    var AD = new PrincipalContext(ContextType.Machine, Environment.MachineName);
        //    var u = new UserPrincipal(AD);
        //    var search = new PrincipalSearcher(u);

        //    foreach (UserPrincipal result in search.FindAll())
        //    {
        //        Global.settings.allLocalUsers.Add(result.Sid.ToString(), new Prcpl(result.Sid.ToString(), result.Context.Name, result.Name, result.UserPrincipalName));

        //        Console.WriteLine(result.Name);
        //    }

        //    CustomSettings.save(Global.settings, Global.settings.settingsFile);
        //}

        private static void readAllDomainUsers()
        {
            var AD = new PrincipalContext(ContextType.Domain);
            var u = new UserPrincipal(AD);
            var search = new PrincipalSearcher(u);

            foreach (UserPrincipal result in search.FindAll())
            {
                Global.settings.allDomainUsers.Add(result.Sid.ToString(), new Prcpl(result.Sid.ToString(), result.Context.Name, result.Name, result.UserPrincipalName));

                Console.WriteLine(result.Name);
            }

            CustomSettings.save(Global.settings, Global.settings.settingsFile);
        }

        public static bool isCurrentUserLocalUser()
        {
            return Environment.MachineName.ToLower() == Environment.UserDomainName.ToLower();
        }


        public static Prcpl pickUser(bool userOnly)
        {

            Prcpl ret = new Prcpl();

            Tulpep.ActiveDirectoryObjectPicker.DirectoryObjectPickerDialog picker = new DirectoryObjectPickerDialog()
            {
                AllowedObjectTypes = ObjectTypes.All   ,
                DefaultObjectTypes = ObjectTypes.None  ,
                AllowedLocations = Locations.All,
                DefaultLocations = Locations.JoinedDomain  ,
                MultiSelect = false,
                ShowAdvancedView = true
            };



             picker.DefaultObjectTypes  |= ObjectTypes.Users ;
            picker.DefaultObjectTypes |= ObjectTypes.WellKnownPrincipals ;

            if (!userOnly)
            {
          
                picker.DefaultObjectTypes  |= ObjectTypes.BuiltInGroups;
                picker.DefaultObjectTypes |= ObjectTypes.Groups;
            }

            picker.Providers = ADsPathsProviders.WinNT;
            picker.AttributesToFetch.Add("objectSid");

            if (picker.ShowDialog() == DialogResult.OK)
            {
   
                ret.Name = picker.SelectedObject.Name;


                string[] split = picker.SelectedObject.Path.Split('/');

                ret.Domain = split[split.Length - 2];

                MessageBox.Show(picker.SelectedObject.Name + "\n" + picker.SelectedObject.Path);

                if (picker.SelectedObject.FetchedAttributes[0] == null)
                {
                    //   sb.Append("(not present)");
                    //fixme: SID anders ermitteln
                }
                else if (picker.SelectedObject.FetchedAttributes[0] is byte[])
                {
                    byte[] bytes = (byte[])picker.SelectedObject.FetchedAttributes[0];
                    ret.Sid  = BytesToString(bytes);
                }

                return ret;

            
            }
            else
            {
                MessageBox.Show("error in selecting user");
                return ret ;
            }

        }

        private static string BytesToString(byte[] bytes)
        {
            try
            {
                Guid guid = new Guid(bytes);
                return guid.ToString("D");
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }

            try
            {
                SecurityIdentifier sid = new SecurityIdentifier(bytes, 0);
                return sid.ToString();
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }

            return "0x" + BitConverter.ToString(bytes).Replace('-', ' ');
        }
    }
}