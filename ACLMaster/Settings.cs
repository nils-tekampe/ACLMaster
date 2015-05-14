using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Xml.Serialization;

namespace ACLMaster
{
    /// <summary>
    /// Class used for custom settingsToSave [x]
    /// </summary>
    [Serializable]
    public class CustomSettings
    {
        public string settingsFile;

        //now following all the parameters of the settingsToSave
        public bool upgraded = false;

        public DateTime dateOfLastScan = new DateTime(1978, 04, 20);
        public int validityPeriodGroupsAndUsers = 14;
        public bool showOnlyReadableFolders = true;
        public bool showOnlyChangableFolders = false;
        public bool machineIsDomainJoined = false;
        public string domain = "";
        public bool readDomainInformationFailed = false;
        public bool startEscalated = false;
        public string currentUser = "";
        public bool logVerbose = false;
        public bool greyOutProtectedSecurables = true;
        public string nameLastUser1 = "";
        public string nameLastUser2 = "";
        public string nameLastUser3 = "";
        public string nameLastUser4 = "";
        public string nameLastUser5 = "";
        public string sidLastUser1 = "";
        public string sidLastUser2 = "";
        public string sidLastUser3 = "";
        public string sidLastUser4 = "";
        public string sidLastUser5 = "";
        public bool localOnly = false;
        public bool showInternalDetails = false;

        /// <summary>
        /// All local users
        /// </summary>
        [XmlIgnore]
        public OrderedDictionary allLocalUsers = new OrderedDictionary();

        /// <summary>
        /// All domain users
        /// </summary>
        [XmlIgnore]
        public OrderedDictionary allDomainUsers = new OrderedDictionary();

        /// <summary>
        /// All local groups
        /// </summary>
        [XmlIgnore]
        public OrderedDictionary allLocalGroups = new OrderedDictionary();

        /// <summary>
        /// All domain groups
        /// </summary>
        [XmlIgnore]
        public OrderedDictionary allDomainGroups = new OrderedDictionary();

        /// <summary>
        /// All local groups of current user that could be obtained regardless whether the current user is a domain user or local user
        /// </summary>
        [XmlIgnore]
        public OrderedDictionary allGroupsOfCurrentUser = new OrderedDictionary();

        ///// <summary>
        ///// All local groups of current user that could be obtained regardless whether the current user is a domain user or local user
        ///// </summary>
        //[XmlIgnore]
        //public OrderedDictionary allDomainGroupsOfCurrentUser = new OrderedDictionary();

        /// <summary>
        /// Internal List to manage allLocalUsers. Must be public to get serialized.
        /// </summary>
        //[XmlArray("PersonenArray")]
        //[XmlArrayItem("PersonObjekt")]
        //// ReSharper disable once InconsistentNaming
        //public List<Prcpl> _allLocalUsers = new List<Prcpl>();

        /// <summary>
        /// Internal List to manage allDomainUsers. Must be public to get serialized.
        ///// </summary>
        //[XmlArray("PersonenArray2")]
        //[XmlArrayItem("PersonObjekt2")]
        //// ReSharper disable once InconsistentNaming
        //public List<Prcpl> _allDomainUsers = new List<Prcpl>();

        /// <summary>
        /// Internal List to manage allLocalGroups. Must be public to get serialized.
        ///// </summary>
        //[XmlArray("PersonenArray3")]
        //[XmlArrayItem("PersonObjekt3")]
        //// ReSharper disable once InconsistentNaming
        //public List<Prcpl> _allLocalGroups = new List<Prcpl>();

        /// <summary>
        /// Internal List to manage allDomainGroups. Must be public to get serialized.
        ///// </summary>
        //[XmlArray("PersonenArray4")]
        //[XmlArrayItem("PersonObjekt4")]
        //// ReSharper disable once InconsistentNaming
        //public List<Prcpl> _allDomainGroups = new List<Prcpl>();

        /// <summary>
        /// Internal List to manage allLocalGroupsOfCurrentser. Must be public to get serialized.
        /// </summary>
        [XmlArray("PersonenArray5")] 
        [XmlArrayItem("PersonObjekt5")] 
        // ReSharper disable once InconsistentNaming
        public List<Prcpl> _allGroupsOfCurrentUser = new List<Prcpl>();

        ///// <summary>
        ///// Internal List to manage allLocalGroupsOfCurrentser. Must be public to get serialized.
        ///// </summary>
        //[XmlArray("PersonenArray6")]
        //[XmlArrayItem("PersonObjekt6")]
        //// ReSharper disable once InconsistentNaming
        //public List<Prcpl> _allDomainGroupsOfCurrentUser = new List<Prcpl>();

        public CustomSettings()
        {
            settingsFile = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\config.xml";
        }

        /// <summary>
        /// This function reads all the relevant settingsToSave from the settingsFile
        /// </summary>
        /// <param name="settingsFile">The settings file to be read.</param>
        /// <returns></returns>
        public static CustomSettings read(string settingsFile)
        {
            if (settingsFile == null)
                throw new ArgumentNullException("settingsFile");

            XmlSerializer xs = new XmlSerializer(typeof(CustomSettings));

            StreamReader srdr = new StreamReader(settingsFile);
            CustomSettings settings = (CustomSettings)xs.Deserialize(srdr);

            //dirty workaround as my ordereddictionaries are not serializable
            //settings.allLocalUsers.Clear();
            //settings.allDomainUsers.Clear();
            //settings.allLocalGroups.Clear();
            //settings.allDomainGroups.Clear();
            settings.allGroupsOfCurrentUser.Clear();

            //foreach (Prcpl prcpl in settings._allLocalUsers)
            //{
            //    settings.allLocalUsers.Add(prcpl.Sid, prcpl);
            //}

            //foreach (Prcpl prcpl in settings._allDomainUsers)
            //{
            //    settings.allDomainUsers.Add(prcpl.Sid, prcpl);
            //}

            //foreach (Prcpl prcpl in settings._allLocalGroups)
            //{
            //    settings.allLocalGroups.Add(prcpl.Sid, prcpl);
            //}

            //foreach (Prcpl prcpl in settings._allDomainGroups)
            //{
            //    settings.allDomainGroups.Add(prcpl.Sid, prcpl);
            //}

            foreach (Prcpl prcpl in settings._allGroupsOfCurrentUser)
            {
                settings.allGroupsOfCurrentUser.Add(prcpl.Sid, prcpl);
            }

            //foreach (Prcpl prcpl in settings._allDomainGroupsOfCurrentUser)
            //{
            //    settings.allDomainGroupsOfCurrentUser.Add(prcpl.Sid, prcpl);
            //}

            srdr.Close();
            return settings;
        }

        /// <summary>
        /// This functions saves all the specified settingsToSave into a the settingsFile
        /// </summary>
        /// <param name="settingsToSave">The settings object that should be saved.</param>
        /// <param name="settingsFile">The _settings file to save toharaigoshi78$
        /// .</param>
        public static void save(CustomSettings settingsToSave, string settingsFile)
        {
            if (settingsFile == null)
                throw new ArgumentNullException("settingsFile");

            XmlSerializer xs = new XmlSerializer(typeof(CustomSettings));
            TextWriter writer = new StreamWriter(settingsFile, false);

            //dirty workaround as my ordereddictionaries are not serializable I transfer the content to Lists first
            //settingsToSave._allLocalUsers.Clear();
            //settingsToSave._allDomainUsers.Clear();
            //settingsToSave._allLocalGroups.Clear();
            //settingsToSave._allDomainGroups.Clear();
            settingsToSave._allGroupsOfCurrentUser.Clear();

            //foreach (Prcpl prcpl in settingsToSave.allLocalUsers.Values)
            //{
            //    settingsToSave._allLocalUsers.Add(prcpl);
            //}

            //foreach (Prcpl prcpl in settingsToSave.allDomainUsers.Values)
            //{
            //    settingsToSave._allDomainUsers.Add(prcpl);
            //}

            //foreach (Prcpl prcpl in settingsToSave.allLocalGroups.Values)
            //{
            //    settingsToSave._allLocalGroups.Add(prcpl);
            //}

            //foreach (Prcpl prcpl in settingsToSave.allDomainGroups.Values)
            //{
            //    settingsToSave._allDomainGroups.Add(prcpl);
            //}

            //foreach (Prcpl prcpl in settingsToSave.allLocalGroupsOfCurrentUser.Values)
            //{
            //    settingsToSave._allLocalGroupsOfCurrentUser.Add(prcpl);
            //}

            foreach (Prcpl prcpl in settingsToSave.allGroupsOfCurrentUser.Values)
            {
                settingsToSave._allGroupsOfCurrentUser.Add(prcpl);
            }

            xs.Serialize(writer, settingsToSave);

            writer.Close();
        }
    }
}