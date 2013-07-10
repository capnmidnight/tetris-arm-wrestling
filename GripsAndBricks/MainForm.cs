using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinGameLib;
using GripsAndBricks.Screens;

namespace GripsAndBricks
{
    public partial class MainForm : GDIGameForm
    {
        public MainForm()
            : base(1024, 768)
        {
        }

        protected override void LoadScreens()
        {
            AddScreen(AppStates.Title, new TitleScreen());
            AddScreen(AppStates.Menu, new MenuScreen());
            AddScreen(AppStates.Game, new GameScreen());
            AddScreen(AppStates.Options, new OptionsScreen());
        }
    }
}
