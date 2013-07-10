using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using WinGameLib;

namespace SpaceRace.Screens
{
    class MenuScreen : OutputScreen
    {
        static AppStates[] Transitions = new AppStates[]{
                AppStates.Game,
                AppStates.Options,
                AppStates.Shutdown
            };
        private int cursor;
        private Image[] menuItems;
        private Image bg;
        protected SoundPlayer flip;
        public MenuScreen()
        {
            bg = Bitmap.FromFile("img/bg.png");
            flip = new SoundPlayer("snd/flip.wav");
            menuItems = new Image[]{
                Bitmap.FromFile("img/menu1.png"),
                Bitmap.FromFile("img/menu2.png"),
                Bitmap.FromFile("img/menu3.png")
            };
        }
        public override void Update(int sinceLastDraw)
        {
            //nothing to do
        }
        public override void Dispose()
        {
            base.Dispose();
            foreach (var menuItem in menuItems)
                menuItem.Dispose();
            flip.Dispose();
            bg.Dispose();
            StopSong();
        }

        public override void EnterState(AppStates previousState)
        {
            NextState = AppStates.Menu;
            cursor = 0;
            PlaySong("music/menu.ogg");
        }

        public override void LeaveState()
        {
            StopSong();
        }

        public override void DoKeyCommand(Keys key, int action)
        {
            if (action == 1)
            {
                if (key == Keys.Up)
                {
                    if (cursor > 0)
                    {
                        flip.Play();
                        cursor--;
                    }
                }
                else if (key == Keys.Down)
                {
                    if (cursor < menuItems.Length - 1)
                    {
                        flip.Play();
                        cursor++;
                    }
                }
                else if (key == Keys.Enter)
                {
                    NextState = Transitions[cursor];
                }
                else if (key == Keys.Escape)
                {
                    NextState = AppStates.Title;
                }
            }
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(bg, 0, 0, MainForm.ScreenWidth, MainForm.ScreenHeight);
            g.DrawImage(menuItems[cursor], 0, 0, MainForm.ScreenWidth, MainForm.ScreenHeight);
        }
    }
}