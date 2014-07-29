using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MiceliMatrix;

namespace MiceliMatrix
{
    static class Program
    {
        public static readonly string KEY = "Software\\MiceliMatrix";  

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0 && args[0].ToLower().Contains("/p"))
                return;

            if(args.Length > 0 && args[0].ToLower().Contains("/c"))
                Application.Run(new PreferencesForm());
            else
                Application.Run(new ScreensaverForm());
        }
    }
}
