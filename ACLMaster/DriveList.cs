using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ACLMaster
{
    internal class DriveList
    {
        private List<Drive> drives;

        public DriveList()
        {
            drives = new List<Drive>();
        }

        public List<Drive> getDriveList()
        {
            return drives;
        }

        public void addDrive(Drive _drive)
        {
            drives.Add(_drive);
        }

        public void readDrives()
        {
            drives.Clear();
            foreach (DriveInfo drive in DriveInfo.GetDrives().Where(drive => drive.IsReady))
            {
                addDrive(new Drive(drive));
            }
        }

        public Drive getDrive(int _index)
        {
            return drives[_index];
        }
    }
}