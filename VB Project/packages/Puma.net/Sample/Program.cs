using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Puma.Net.Sample
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

            MainForm form = null;
            
            try
            {
                form = new MainForm();

            }
            catch (COMException ex)
            {
                MessageBox.Show(
                    "Error while loading Puma COM server. Check that it's registered. Inner error message: " +
                    ex.Message, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error while starting. Inner error message: " +
                    ex.Message, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            Application.Run(form);

        }
    }
}
