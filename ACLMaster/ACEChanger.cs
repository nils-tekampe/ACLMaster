using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace ACLMaster
{
    public static class ACEChanger
    {
        public static void changePermissionOfSelectedACE(ListView.SelectedListViewItemCollection collection, FileSystemRights permission, bool add)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                FileSystemAccessRule newRule;
                FileSystemAccessRule oldRule = ((FileSystemAccessRuleExtended)((OLVListItem) collection[i]).RowObject).fsar ;

                if (add)
                    newRule = new FileSystemAccessRule(oldRule.IdentityReference, oldRule.FileSystemRights | permission, oldRule.InheritanceFlags, oldRule.PropagationFlags, oldRule.AccessControlType);
                else
                    newRule = new FileSystemAccessRule(oldRule.IdentityReference, oldRule.FileSystemRights ^ permission, oldRule.InheritanceFlags, oldRule.PropagationFlags, oldRule.AccessControlType);

                ((FileSystemAccessRuleExtended)((OLVListItem) collection[i]).RowObject ).fsar = newRule;
                ((FileSystemAccessRuleExtended) ((OLVListItem) collection[i]).RowObject ).changed=true;
           


            }
        }

        public static void changeFlagsOfSelectedACE(ListView.SelectedListViewItemCollection collection, PropagationFlags flag, bool add)
        {
          
                for (int i = 0; i < collection.Count; i++)
                {
                    FileSystemAccessRule newRule;
                    FileSystemAccessRule oldRule = ((FileSystemAccessRuleExtended)((OLVListItem) collection[i]).RowObject ).fsar;
                    PropagationFlags oldFlag = oldRule.PropagationFlags;

                    PropagationFlags newFlag;
                    if (add)
                    {
                        newFlag = oldFlag | flag;
                        newRule = new FileSystemAccessRule(oldRule.IdentityReference, oldRule.FileSystemRights, oldRule.InheritanceFlags, newFlag, oldRule.AccessControlType);
                    }
                    else
                    {
                        newFlag = oldFlag ^ flag;
                        newRule = new FileSystemAccessRule(oldRule.IdentityReference, oldRule.FileSystemRights, oldRule.InheritanceFlags, newFlag, oldRule.AccessControlType);
                    }

       
                    ((FileSystemAccessRuleExtended )((OLVListItem)collection[i]).RowObject ).fsar = newRule;
                    ((FileSystemAccessRuleExtended)((OLVListItem)collection[i]).RowObject ).changed = true;
                   
                
            }
        }

        public static void changeGrantTypeOfSelectedACE(ListView.SelectedListViewItemCollection collection, AccessControlType type)
        {
           
                for (int i = 0; i < collection.Count; i++)
                {
                    FileSystemAccessRule oldRule = ((FileSystemAccessRuleExtended)  ((OLVListItem) collection[i]).RowObject ).fsar;
                    FileSystemAccessRule newRule = new FileSystemAccessRule(oldRule.IdentityReference, oldRule.FileSystemRights, oldRule.InheritanceFlags, oldRule.PropagationFlags, type);

                    ((FileSystemAccessRuleExtended) ((OLVListItem) collection[i]).RowObject ).fsar = newRule;
                    ((FileSystemAccessRuleExtended) ((OLVListItem) collection[i]).RowObject ).changed = true;

            }

           
        }


        public static void changePropagationFlagOfSelectedACE(ListView.SelectedListViewItemCollection collection, PropagationFlags  flag,bool setFlag )
        {
            
            

            for (int i = 0; i < collection.Count; i++)
            {
                FileSystemAccessRule oldRule = ((FileSystemAccessRuleExtended) ((OLVListItem)  collection[i]).RowObject ).fsar;

               

                PropagationFlags newFlags;

                if (setFlag)
                    newFlags = oldRule.PropagationFlags | flag;
                else
                {
                    newFlags = oldRule.PropagationFlags & flag;
                }

                FileSystemAccessRule newRule = new FileSystemAccessRule(oldRule.IdentityReference, oldRule.FileSystemRights, oldRule.InheritanceFlags, newFlags, oldRule.AccessControlType);

         
                ((FileSystemAccessRuleExtended)((OLVListItem)  collection[i]).RowObject).fsar = newRule;
                ((FileSystemAccessRuleExtended)((OLVListItem)  collection[i]).RowObject ).changed = true;


            }


        }


        public static void ChangeUserOfSelectedAce(ListView.SelectedListViewItemCollection collection, string _user, string _SID)
        {
           
            //TODO: hier darf nur der admin weiter und das privilege muss noch her

            string user = "";
            string sid = "";

            if ((_user == "") ^ (_SID == ""))
                MessageBox.Show("bad thing");

            if ((_user == "") && (_SID == ""))
            {
                frmSelectUser frmUser = new frmSelectUser();

                if (frmUser.ShowDialog() == DialogResult.OK)
                {
                    user = frmUser.domainChosen + "\\" + frmUser.userChosen;
                    sid = frmUser.sidChosen;

                    //we only remember the user in the list of last users if selected by the user
                    Global.shiftLastUsers(sid, user);
                }
                else
                {
                    MessageBox.Show("error in selecting user");
                    return;
                }
            }

            else
            {
                user = _user;
                sid = _SID;
            }

          
                for (int i = 0; i < collection.Count; i++)
                {
                    SecurityIdentifier id = new SecurityIdentifier(sid);
                    NTAccount nt = null;
                    FileSystemAccessRule newRule;
                    FileSystemAccessRule oldRule = ((FileSystemAccessRuleExtended)   ((OLVListItem)   collection[i]).RowObject ).fsar;

                    try
                    {
                        nt = (NTAccount)id.Translate(typeof(NTAccount));
                    }
                    catch (IdentityNotMappedException ex)
                    {
                    }

                    if (nt != null)
                    {
                        newRule = new FileSystemAccessRule(nt, oldRule.FileSystemRights, oldRule.InheritanceFlags, oldRule.PropagationFlags, oldRule.AccessControlType);
                    }
                    else
                    {
                        newRule = new FileSystemAccessRule(new SecurityIdentifier(sid), oldRule.FileSystemRights, oldRule.InheritanceFlags, oldRule.PropagationFlags, oldRule.AccessControlType);
                    }

                    ((FileSystemAccessRuleExtended) ((OLVListItem) collection[i]).RowObject ).fsar = newRule;
                    ((FileSystemAccessRuleExtended) ((OLVListItem) collection[i]).RowObject ).changed = true;

            }


        }


        //public static void applyACES(ListView.SelectedListViewItemCollection collection)
        //{
        //    //todo: check for admin

        //}
    }
}