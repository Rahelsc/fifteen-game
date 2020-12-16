using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace movingNumbers
{
    static class Program
    {
        public static bool OpenDetailFormOnClose { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            OpenDetailFormOnClose = false;

            Application.Run(new Form1());

            while (OpenDetailFormOnClose)
            {
                Application.Run(new Form1());
            }
        }
    }
}
