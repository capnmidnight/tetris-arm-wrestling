using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using WinGameLib;

namespace GripsAndBricks.Screens
{
    class OptionsScreen : OutputScreen
    {
        public static int MusicTrack;
        public static bool SoundOn;
        public static Dictionary<GameActions, Keys> KeyOptions;
        public static Dictionary<Keys, string> Keynames;
        private static int[] OptY;
        private static GameActions[] UnderCursor;
        static OptionsScreen()
        {
            MusicTrack = 0;
            SoundOn = true;
            Keynames = new Dictionary<Keys, string>();
            Keynames.Add(Keys.A, "a");
            Keynames.Add(Keys.B, "b");
            Keynames.Add(Keys.C, "c");
            Keynames.Add(Keys.D, "d");
            Keynames.Add(Keys.E, "e");
            Keynames.Add(Keys.F, "f");
            Keynames.Add(Keys.G, "g");
            Keynames.Add(Keys.H, "h");
            Keynames.Add(Keys.I, "i");
            Keynames.Add(Keys.J, "j");
            Keynames.Add(Keys.K, "k");
            Keynames.Add(Keys.L, "l");
            Keynames.Add(Keys.M, "m");
            Keynames.Add(Keys.N, "n");
            Keynames.Add(Keys.O, "o");
            Keynames.Add(Keys.P, "p");
            Keynames.Add(Keys.Q, "q");
            Keynames.Add(Keys.R, "r");
            Keynames.Add(Keys.S, "s");
            Keynames.Add(Keys.T, "t");
            Keynames.Add(Keys.U, "u");
            Keynames.Add(Keys.V, "v");
            Keynames.Add(Keys.W, "w");
            Keynames.Add(Keys.X, "x");
            Keynames.Add(Keys.Y, "y");
            Keynames.Add(Keys.Z, "z");
            Keynames.Add(Keys.D0, "0");
            Keynames.Add(Keys.D1, "1");
            Keynames.Add(Keys.D2, "2");
            Keynames.Add(Keys.D3, "3");
            Keynames.Add(Keys.D4, "4");
            Keynames.Add(Keys.D5, "5");
            Keynames.Add(Keys.D6, "6");
            Keynames.Add(Keys.D7, "7");
            Keynames.Add(Keys.D8, "8");
            Keynames.Add(Keys.D9, "9");
            Keynames.Add(Keys.Left, "left");
            Keynames.Add(Keys.Right, "right");
            Keynames.Add(Keys.Up, "up");
            Keynames.Add(Keys.Down, "down");
            Keynames.Add(Keys.Space, "space");
            Keynames.Add(Keys.Enter, "enter");
            Keynames.Add(Keys.Escape, "escape");

            KeyOptions = new Dictionary<GameActions, Keys>();
            KeyOptions.Add(GameActions.Cancel, Keys.Escape);
            KeyOptions.Add(GameActions.MenuDown, Keys.Down);
            KeyOptions.Add(GameActions.MenuUp, Keys.Up);
            KeyOptions.Add(GameActions.P1Drop, Keys.S);
            KeyOptions.Add(GameActions.P1Flip, Keys.W);
            KeyOptions.Add(GameActions.P1Left, Keys.A);
            KeyOptions.Add(GameActions.P1Right, Keys.D);
            KeyOptions.Add(GameActions.P2Drop, Keys.K);
            KeyOptions.Add(GameActions.P2Flip, Keys.I);
            KeyOptions.Add(GameActions.P2Left, Keys.J);
            KeyOptions.Add(GameActions.P2Right, Keys.L);
            KeyOptions.Add(GameActions.Select, Keys.Enter);

            OptY = new int[]{
                45,
                100,
                210,
                265,
                320,
                375,
                485,
                540,
                595,
                650
            };

            UnderCursor = new GameActions[]{
                GameActions.Cancel,
                GameActions.Cancel,
                GameActions.P1Left,
                GameActions.P1Right,
                GameActions.P1Flip,
                GameActions.P1Drop,
                GameActions.P2Left,
                GameActions.P2Right,
                GameActions.P2Flip,
                GameActions.P2Drop
            };

        }

        private int cursor;
        private Dictionary<Keys, Image> OptionImages;
        private Image foreground, on, off, cursorImage;
        private bool waitingForKey;
        protected SoundPlayer flip;
        Image bg;
        public OptionsScreen()
        {
            bg = Bitmap.FromFile("img/bg.png");
            flip = new SoundPlayer("snd/flip.wav");
            foreground = Bitmap.FromFile("img/options.png");
            on = Bitmap.FromFile("img/opt/on.png");
            off = Bitmap.FromFile("img/opt/off.png");
            cursorImage = Bitmap.FromFile("img/cursor.png");
            OptionImages = new Dictionary<Keys, Image>();
            foreach (var key in Keynames)
                OptionImages.Add(key.Key, Bitmap.FromFile(string.Format("img/opt/{0}.png", key.Value)));
        }
        public override void Update(int sinceLastDraw)
        {
            // nothing to do
        }
        public override void EnterState(AppStates previousState)
        {
            NextState = AppStates.Options;
            cursor = 0;
            waitingForKey = false;
            PlaySong(GameScreen.Songs[MusicTrack + 1]);
        }

        public override void LeaveState()
        {
            StopSong();
        }

        public override void Dispose()
        {
            base.Dispose();
            foreach (var optImage in OptionImages.Values)
                optImage.Dispose();
            foreground.Dispose();
            on.Dispose();
            off.Dispose();
            flip.Dispose();
            cursorImage.Dispose();
            bg.Dispose();
        }

        public override void DoKeyCommand(Keys key, int action)
        {
            if (action == 1)
            {
                if (waitingForKey)
                {
                    waitingForKey = false;
                    if (!KeyOptions.Values.Contains(key))
                        KeyOptions[UnderCursor[cursor]] = key;
                }
                else if (key == Keys.Up)
                {
                    if (cursor > 0)
                    {
                        flip.Play();
                        cursor--;
                    }
                }
                else if (key == Keys.Down)
                {
                    if (cursor < 9)
                    {
                        flip.Play();
                        cursor++;
                    }
                }
                else if (key == Keys.Enter)
                {
                    ChangeOption();
                }
                else if (key == Keys.Escape)
                {
                    NextState = AppStates.Menu;
                }
            }
        }

        private void ChangeOption()
        {
            switch (cursor)
            {
                case 0:
                    SoundOn = !SoundOn;
                    break;
                case 1:
                    MusicTrack++;
                    if (MusicTrack == 2)
                    {
                        MusicTrack = -1;
                    }

                    StopSong();
                    PlaySong(GameScreen.Songs[MusicTrack + 1]);

                    break;
                default:
                    waitingForKey = true;
                    break;
            }
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(bg, 0, 0, MainForm.ScreenWidth, MainForm.ScreenHeight);
            g.DrawImage(foreground, 0, 0, MainForm.ScreenWidth, MainForm.ScreenHeight);
            DrawSoundOpt(g);
            DrawMusicOpt(g);
            DrawKeyOptions(g);
            g.DrawImageUnscaled(cursorImage, 635 - (waitingForKey ? 10 : 0), OptY[cursor]);
        }

        private void DrawSoundOpt(Graphics g)
        {
            Image soundToDraw = SoundOn ? on : off;
            g.DrawImage(soundToDraw, 250, 45, soundToDraw.Width, soundToDraw.Height);
        }

        private void DrawMusicOpt(Graphics g)
        {
            Image musicToDraw = off;
            if (MusicTrack == 0) musicToDraw = OptionImages[Keys.A];
            else if (MusicTrack == 1) musicToDraw = OptionImages[Keys.B];
            g.DrawImage(musicToDraw, 250, 100, musicToDraw.Width, musicToDraw.Height);
        }

        private void DrawKeyOptions(Graphics g)
        {
            for (int i = 2; i < UnderCursor.Length; ++i)
            {
                var img = OptionImages[KeyOptions[UnderCursor[i]]];
                var y = 100 + (i + (i - 2) / 4) * 55;
                g.DrawImage(img, 525, y, img.Width, img.Height);
            }
        }
    }
}