using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace XrmSpeedyUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            try
            { 
                MessageBox.Show(ex.Message, "An Error Has Occurred");
            }
            finally
            {
                LogError(ex);
                Application.Exit();
            }
        }

        public static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                result = MessageBox.Show(e.Exception.Message, "An Error Has Occurred",
                  MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop);
            }
            finally
            {
                if (result == DialogResult.Cancel)
                {
                    LogError(e.Exception);
                    Application.Exit();
                }
            }
        }

        private static void LogError(Exception ex)
        {
            StreamWriter log;
            if (!File.Exists(Application.StartupPath + "\\XRMSpeedy_Error.log"))
                log = new StreamWriter(Application.StartupPath + "\\XRMSpeedy_Error.log");
            else
                log = File.AppendText(Application.StartupPath + "\\XRMSpeedy_Error.log");

            log.WriteLine(DateTime.Now);
            log.WriteLine("Exception:");
            log.WriteLine(ex.Message);
            log.WriteLine(ex.StackTrace);
            if (ex.InnerException != null)
            {
                log.WriteLine("Inner Exception:");
                log.WriteLine(ex.InnerException.Message);
                log.WriteLine(ex.InnerException.StackTrace);
            }
            log.WriteLine();
            log.Close();
        }
    }
}
