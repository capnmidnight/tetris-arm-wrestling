using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SpaceRace
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WinGameLib.GDIGameForm.GenericMain<MainForm>(30.0);
        }
    }
}
