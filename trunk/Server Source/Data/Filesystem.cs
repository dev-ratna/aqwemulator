using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AQWE.Data
{
    /// <summary>
    /// Provides filesystem functions and properties.
    /// </summary>
    class Filesystem
    {
        #region Kernel
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        #endregion

        #region Properties
        /// <summary>
        /// Returns the startup path of the application.
        /// </summary>
        public static string startupPath
        {
            get
            {
                return Application.StartupPath;
            }
        }
        #endregion

        #region File and folder functions
        /// <summary>
        /// Returns a boolean that indicates if a certain file exists on disk.
        /// </summary>
        /// <param name="Location">The full path to the file to check</param>
        public static bool fileExists(string Location)
        {
            return File.Exists(Location);
        }

        /// <summary>
        /// Returns a boolean that indicates if a certain folder exists on disk.
        /// </summary>
        /// <param name="Location">The fill path to the directory to check</param>
        public static bool folderExists(string Location)
        {
            return Directory.Exists(Location);
        }

        /// <summary>
        /// Creates a folder on disk at a given location.
        /// </summary>
        /// <param name="Location">The full destination path</param>
        public static void createFolder(string Location)
        {
            try { Directory.CreateDirectory(Location); }
            catch { }
        }
        #endregion

        #region INI functions
        /// <summary>
        /// Returns the value of a private profile string in a textfile as a string. The max length is 255 characters.
        /// </summary>
        /// <param name="iniSection">The section where the value is located in</param>
        /// <param name="iniKey">The key of the value</param>
        /// <param name="iniLocation">The location of the textfile</param>
        public static string readINI(string iniSection, string iniKey, string iniLocation)
        {
            StringBuilder sb = new StringBuilder(255);

            try
            {
                int i = GetPrivateProfileString(iniSection, iniKey, "", sb, 255, iniLocation);
                return sb.ToString();
            }
            catch { return ""; }
        }

        /// <summary>
        /// Updates the value of a key in a textfile using WritePrivateProfileString.
        /// </summary>
        /// <param name="iniSection">The section where the key to update is located</param>
        /// <param name="iniKey">The key to update</param>
        /// <param name="iniValue">The new value to the key</param>
        /// <param name="iniLocation">The location of the textfile</param>
        public static void writeINI(string iniSection, string iniKey, string iniValue, string iniLocation)
        {
            WritePrivateProfileString(iniSection, iniKey, iniValue, iniLocation);
        }
        #endregion
    }
}
