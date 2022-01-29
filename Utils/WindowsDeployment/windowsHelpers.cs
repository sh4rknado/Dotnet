using System.IO;

namespace WindowsHelpers {

  public static class WindowsImager {


    #region WindowsHelper

    public static void GetAvailableVersions(string esdPath) {

      CheckPath(string esdPath);
      (string output, string errors) results = ExecutePowerShell(String.Format("DISM /Get-WimInfo /wimfile:{0}", esdPath));

    }

    public static void GetWindowsWimFromEsd(string esdPath, int idx, string destFile) {

          CheckPath(string esdPath);
          (string output, string errors) results = ExecutePowerShell(String.Format("DISM /export-image /sourceimagefile:{0} /sourceindex:{1} /destinationimagefile:{2} /compress:max /checkintegrity", esdPath, idx, destFile));
    }

    private static (string output, string errors) ExecutePowerShell(string cmd) {

      ProcessStartInfo startInfo = new ProcessStartInfo() {
          FileName = @"powershell.exe";
          Arguments = cmd;
          RedirectStandardOutput = true;
          RedirectStandardError = true;
          UseShellExecute = false;
          CreateNoWindow = true;
      }

      Process process = new Process(startInfo);
      // process.StartInfo = startInfo;
      process.Start();

      string output = process.StandardOutput.ReadToEnd();
      string errors = process.StandardError.ReadToEnd();

      return (output,errors);

    }

    #endregion


    #region IsoHelper

    private static string MountIso(string isoPath) {

        using (var ps = PowerShell.Create())
        {

            //Mount ISO Image
            var command = ps.AddCommand("Mount-DiskImage");
            command.AddParameter("ImagePath", isoPath);
            command.Invoke();
            ps.Commands.Clear();

            //Get Drive Letter ISO Image Was Mounted To
            var runSpace = ps.Runspace;
            var pipeLine = runSpace.CreatePipeline();
            var getImageCommand = new Command("Get-DiskImage");
            getImageCommand.Parameters.Add("ImagePath", isoPath);
            pipeLine.Commands.Add(getImageCommand);
            pipeLine.Commands.Add("Get-Volume");

            string driveLetter = null;
            foreach (PSObject psObject in pipeLine.Invoke())
            {
                driveLetter = psObject.Members["DriveLetter"].Value.ToString();
                Console.WriteLine("Mounted On Drive: " + driveLetter);
            }
            pipeLine.Commands.Clear();

            return driveLetter;
        }
    }

    private static void UnmountIsoFromDrive(string driveLetter) {
      using (var ps = PowerShell.Create())
      {
                  //Alternate Unmount Via Drive Letter
                  ps.AddScript("$ShellApplication = New-Object -ComObject Shell.Application;" +
                      "$ShellApplication.Namespace(17).ParseName(\"" + driveLetter + ":\").InvokeVerb(\"Eject\")");
                  ps.Invoke();
                  ps.Commands.Clear();
      }
    }

    private static void UnmountIsoFromIsoPath(string isoPath) {
      using (var ps = PowerShell.Create())
      {
                  //Unmount Via Image File Path
                  command = ps.AddCommand("Dismount-DiskImage");
                  command.AddParameter("ImagePath", isoPath);
                  ps.Invoke();
                  ps.Commands.Clear();
      }
    }

    private static void GenerateWindowsIso(string isoPath) {
      using (var ps = PowerShell.Create())
      {
                  //Unmount Via Image File Path
                  command = ps.AddCommand("Dismount-DiskImage");
                  command.AddParameter("ImagePath", isoPath);
                  ps.Invoke();
                  ps.Commands.Clear();
      }
    }

    #endregion


    #region Files

        private static void CheckPath(string path) {

          if(!File.Exists(esdPath))
            throw new Exception(String.Fomat("File not found : {0}", esdPath));

        }

        void CopyFiles(string sourceDir, string targetDir) {

            Directory.CreateDirectory(targetDir);

            foreach(var file in Directory.GetFiles(sourceDir))
                File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)));

            foreach(var directory in Directory.GetDirectories(sourceDir))
                Copy(directory, Path.Combine(targetDir, Path.GetFileName(directory)));
        }

    #endregion



  }

}
