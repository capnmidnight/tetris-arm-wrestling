using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinGameLib;

namespace SpaceRace.Screens
{
    class TitleScreen : OutputScreen
    {
        private Image title;

        public TitleScreen()
        {
            title = Bitmap.FromFile("img/Title.png");
        }

        public override void Dispose()
        {
            base.Dispose();
            title.Dispose();
            StopSong();
        }

        public override void EnterState(AppStates previousState)
        {
            NextState = AppStates.Title;
            //PlaySong("music/title.ogg");
        }

        public override void LeaveState()
        {
            StopSong();
        }

        public override void DoKeyCommand(Keys key, int action)
        {
            if (action == 1)
            {
                if (key == Keys.Enter)
                {
                    NextState = AppStates.Game;
                }
                else if (key == Keys.Escape)
                {
                    NextState = AppStates.Shutdown;
                }
            }
        }

        public override void Update(int sinceLastDraw)
        {
            //throw new NotImplementedException();
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(title, 0, 0, MainForm.ScreenWidth, MainForm.ScreenHeight);
        }
    }
}