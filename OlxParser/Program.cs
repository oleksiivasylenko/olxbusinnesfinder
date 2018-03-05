using System;
using System.Windows.Forms;

namespace OlxParser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new mForm());
            }
            catch (Exception ex)
            {
                var a = 1;
            }
        }
    }
}
