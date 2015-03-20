using System.Drawing;
using System.IO;
using ACLMaster.Properties;

namespace ACLMaster
{
    internal class Drive
    {
        public string name;
        public string volumeLabel;
        public string displayName;
        public Bitmap bitmap;
        public DirectoryInfo rootDirectory;
        public DriveInfo info;
         

        public Drive(DriveInfo _info)
        {
            name = _info.Name;
            volumeLabel = _info.VolumeLabel;
            displayName = _info.Name + " - " + _info.VolumeLabel;
            rootDirectory = _info.RootDirectory ;
            info = _info;

            switch (_info.DriveType )
            {
                    
                case DriveType.CDRom:
                    bitmap = Resources.CD;
                    break;

                case DriveType.Fixed:
                    bitmap = Resources.hdd;
                    break;

                case DriveType.Removable:
                    bitmap = Resources.USB;
                    break;

                case DriveType.Network:
                    bitmap = Resources.network;
                    break;
            }
        }

   
    }
}