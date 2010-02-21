using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using AQWE.Data;

namespace AQWE.Core
{
    /// <summary>
    /// Provides various for logging events to the main form and optionally to the disk.
    /// </summary>
    class Logging
    {
        #region Declares
        /// <summary>
        /// A reference to the main form.
        /// </summary>
        public static frmMain mainForm;

        /// <summary>
        /// Specifies if to save logs to disk.
        /// </summary>
        public static bool saveLogsToDisk = true;

        /// <summary>
        /// Specifies if to log info messages.
        /// </summary>
        public static bool logInfoMessages = true;

        /// <summary>
        /// Specifies if to log warnings.
        /// </summary>
        public static bool logWarnings = true;

        /// <summary>
        /// Specifies if to log errors.
        /// </summary>
        public static bool logErrors = true;

        /// <summary>
        /// Specifies if to log client messages.
        /// </summary>
        public static bool logClientMessages = true;

        /// <summary>
        /// Specifies if to log server messages.
        /// </summary>
        public static bool logServerMessages = true;
        #endregion

        public static void logHolyInfo(string Text)
        {
            System.Windows.Forms.Application.DoEvents();
            mainForm.logInfo(Text);
        }

        /// <summary>
        /// Logs a plain line of text with a >, wich is never written to a log file.
        /// </summary>
        /// <param name="Text">The text to log</param>
        public static void logInfo(string Text)
        {
            if (logInfoMessages)
                mainForm.logInfo("» " + Text);
        }

        /// <summary>
        /// Logs a warning message with timestamp.
        /// </summary>
        /// <param name="Text">Text to log</param>
        public static void logWarning(string Text)
        {
            if (logWarnings)
                mainForm.logWarning(Text);
        }

        /// <summary>
        /// Logs a error message with timestamp and assembly name.
        /// </summary>
        /// <param name="Text"></param>
        public static void logError(string Text)
        {
            if (logErrors)
                mainForm.logError(Text);
        }

        public static void logClientMessage(int connectionID, string Message)
        {
            if (logClientMessages)
                mainForm.logClientMessage(connectionID, Message);
        }
        public static void logServerMessage(int connectionID, string Message)
        {
            if (logServerMessages)
                mainForm.logServerMessage(connectionID, Message);
        }

        /// <summary>
        /// Creates a TextWriter instance and writes a string to the current log file in the /logs/ folder of the application.
        /// </summary>
        /// <param name="s">The string to write.</param>
        public static void writeToLogFile(string s)
        {
            try
            {
                TextWriter Writer = new StreamWriter(Filesystem.startupPath + "/logs/" + DateTime.Now.ToShortDateString() + ".txt", true);
                Writer.Write(s);
                Writer.Flush();
                Writer.Close();
                Writer.Dispose();
            }
            catch { }
        }
    }
}
