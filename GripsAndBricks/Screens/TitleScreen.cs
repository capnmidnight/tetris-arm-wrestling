using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinGameLib;

namespace GripsAndBricks.Screens
{
    class TitleScreen : OutputScreen
    {
        private Image title, bg;
        private int x, dx;

        public TitleScreen()
        {
            bg = Bitmap.FromFile("img/bg.png");
            title = Bitmap.FromFile("img/title.png");
        }

        public override void Update(int sinceLastDraw)
        {
            x += dx * sinceLastDraw;
            if (x >= 0)
            {
                x = 0;
                dx = 0;
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            title.Dispose();
            bg.Dispose();
            StopSong();
        }

        public override void EnterState(AppStates previousState)
        {
            x = -MainForm.ScreenWidth;
            dx = 1;
            NextState = AppStates.Title;
            PlaySong("music/title.ogg");
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
                    NextState = AppStates.Menu;
                }
                else if (key == Keys.Escape)
                {
                    NextState = AppStates.Shutdown;
                }
            }
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(bg, 0, 0, MainForm.ScreenWidth, MainForm.ScreenHeight);
            g.DrawImage(title, x, 0, MainForm.ScreenWidth, MainForm.ScreenHeight);
        }
    }
}