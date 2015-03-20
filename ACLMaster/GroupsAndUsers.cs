using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Windows.Forms;

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
            if ((Global.settings.validityPeriodGroupsAndUsers != 0) && ((DateTime.Now - Global.settings.dateOfLastScan).TotalDays < Global.settings.validityPeriodGroupsAndUsers))
            {
                return;
            }

            if (MessageBox.Show("We need to acquire information about the groups and users on the local machine and (potentially) the domain that the machine belongs to. This may take a while.", "Acquiring group and user information", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {

                return;
            }


            //initialize the dictionaries
            Global.settings.allLocalUsers = new OrderedDictionary();
            Global.settings.allDomainUsers = new OrderedDictionary();
            Global.settings.allLocalGroups = new OrderedDictionary();
            Global.settings.allDomainGroups = new OrderedDictionary();
            Global.settings.allLocalGroupsOfCurrentUser = new OrderedDictionary();
            Global.settings.allDomainGroupsOfCurrentUser = new OrderedDictionary();
            bool domainSuccess = true;

            readAllLocalUsers();
            readAllLocalGroups();

         
     
            if (Global.settings.machineIsDomainJoined && !Global.settings.localOnly)
            {
                //Here we are in the domain context because the machine id domain joined.
                //This means that we can safely try to read all groups and users from thd PDC.
                try
                {
                    readAllDomainUsers();
                    readAllDomainGroups();
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

            //Next wer are interested in the groups of the user. So let's see whether it is a local one or one from the domain.
            if (isCurrentUserLocalUser()|| !domainSuccess)
            {
                ReadAllGroupsOfCurrentLocalUser();
            }
            else
            {
                //case domain user. We need to get the local groups as well as the AD groups
                try
                {
                    readAllLocalGroupsOfCurrentDomainUser();
                    readAllDomainGroupsOfCurrentDomainUser();
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

       
            //for convenience we will mark the groups that the current user is a member of

            MarkGroups();

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

        private static void MarkGroups()
        {
            foreach (DictionaryEntry de in Global.settings.allLocalGroups)
            {
                if (Global.settings.allLocalGroupsOfCurrentUser.Contains(de.Key))
                    ((Prcpl)de.Value).currentUserIsMember = true;
            }

            //todo: domain groups
        }

        /// <summary>
        ///     Reads all groups of current local user.
        /// </summary>
        private static void ReadAllGroupsOfCurrentLocalUser()
        {
            var context = new PrincipalContext(ContextType.Machine, Environment.MachineName);
            UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, Environment.UserName);

            foreach (GroupPrincipal group in userPrincipal.GetAuthorizationGroups())
            {
                Global.settings.allLocalGroupsOfCurrentUser.Add(@group.Sid.ToString(), new Prcpl(@group.Sid.ToString(), @group.Context.Name, @group.Name, @group.UserPrincipalName));
            }
        }

        private static void readAllLocalGroupsOfCurrentDomainUser()
        {
            var root = new DirectoryEntry(String.Format("WinNT://{0},Computer", Environment.MachineName), null, null, AuthenticationTypes.Secure);

            foreach (DirectoryEntry groupDirectoryEntry in root.Children)
            {
                if (groupDirectoryEntry.SchemaClassName != "Group")
                    continue;

                string groupName = groupDirectoryEntry.Name;
                Console.WriteLine("Checking: {0}", groupName);

                if (IsUserMemberOfGroup(groupDirectoryEntry, String.Format("WinNT://{0}/{1}", "tuvit", Environment.UserName)))
                {
                    MessageBox.Show(groupName);
                }
            }
        }

        private static void readAllDomainGroupsOfCurrentDomainUser()
        {
            //todo: not yet working: http://stackoverflow.com/questions/4460558/how-to-get-all-the-ad-groups-for-a-particular-user

            UserPrincipal user = UserPrincipal.FindByIdentity(new PrincipalContext(ContextType.Domain, Environment.UserDomainName), IdentityType.SamAccountName, Global.currentIdentity.Name);

            foreach (GroupPrincipal groupPrincipal in user.GetGroups())
            {
                Prcpl prcpl = new Prcpl(groupPrincipal.Sid.ToString(), groupPrincipal.Context.Name, groupPrincipal.Name, groupPrincipal.UserPrincipalName);
                Global.settings.allDomainGroupsOfCurrentUser.Add(groupPrincipal.Sid.ToString(), prcpl);
            }

            //static void Main(string[] args)
            ////{
            //    DirectoryEntry de = new DirectoryEntry("LDAP://"+Environment.UserDomainName);
            //    DirectorySearcher searcher = new DirectorySearcher(de);
            //searcher.Filter = "(&(ObjectClass=group))";
            //    searcher.PropertiesToLoad.Add("distinguishedName");
            //    searcher.PropertiesToLoad.Add("sAMAccountName");
            //    searcher.PropertiesToLoad.Add("name");
            ///   searcher.PropertiesToLoad.Add("objectSid");
            ////    SearchResultCollection results = searcher.FindAll();
            ////    int i = 1;
            //    foreach (SearchResult res in results)
            //    {
            //        Console.WriteLine("Result" + Convert.ToString(i++));
            //        DisplayProperties("distinguishedName", res);
            //        DisplayProperties("sAMAccouontName", res);
            //        DisplayProperties("name", res);
            //        DisplayProperties("objectSid", res);
            //        Console.WriteLine();
            //    }

            //    Console.ReadKey();
            //}

            //private static void DisplayProperties(string property, SearchResult res)
            //{
            //    Console.WriteLine("\t" + property);
            //    ResultPropertyValueCollection col = res.Properties[property];
            //    foreach (object o in col)
            //    {
            //        Console.WriteLine("\t\t" + o.ToString());
            //    }
            //}

            //search.PropertiesToLoad.Add("memberOf");
            //        StringBuilder groupNames = new StringBuilder(); //stuff them in | delimited

            //            SearchResult result = search.FindOne();
            //            int propertyCount = result.Properties["memberOf"].Count;
            //            String dn;
            //            int equalsIndex, commaIndex;

            //            for (int propertyCounter = 0; propertyCounter < propertyCount;
            //                propertyCounter++)
            //            {
            //                dn = (String)result.Properties["memberOf"][propertyCounter];

            //                equalsIndex = dn.IndexOf("=", 1);
            //                commaIndex = dn.IndexOf(",", 1);
            //                if (-1 == equalsIndex)
            //                {
            //                    return null;
            //                }
            //                groupNames.Append(dn.Substring((equalsIndex + 1),
            //                            (commaIndex - equalsIndex) - 1));
            //                groupNames.Append("|");
            //            }

            //        return groupNames.ToString();

            //--alte lösung
            //DirectoryEntry root = new DirectoryEntry(String.Format("WinNT://{0},Computer", Environment), null, null, AuthenticationTypes.Secure);

            //foreach (DirectoryEntry groupDirectoryEntry in root.Children)
            //{
            //    if (groupDirectoryEntry.SchemaClassName != "Group")
            //        continue;

            //    string groupName = groupDirectoryEntry.Name;
            //    Console.WriteLine("Checking: {0}", groupName);

            //    if (IsUserMemberOfGroup(groupDirectoryEntry, String.Format("WinNT://{0}/{1}", "tuvit", Environment.UserName)))
            //    {
            //        MessageBox.Show(groupName);
            //    }
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

        private static void readAllLocalUsers()
        {
            var AD = new PrincipalContext(ContextType.Machine, Environment.MachineName);
            var u = new UserPrincipal(AD);
            var search = new PrincipalSearcher(u);

            foreach (UserPrincipal result in search.FindAll())
            {
                Global.settings.allLocalUsers.Add(result.Sid.ToString(), new Prcpl(result.Sid.ToString(), result.Context.Name, result.Name, result.UserPrincipalName));

                Console.WriteLine(result.Name);
            }

            CustomSettings.save(Global.settings, Global.settings.settingsFile);
        }

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
    }
}