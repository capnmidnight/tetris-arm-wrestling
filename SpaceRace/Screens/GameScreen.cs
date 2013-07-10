using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using WinGameLib;

namespace SpaceRace.Screens
{
    class Ship
    {
        static Image[] sprite;
        static int imgW2, imgH2;
        static double P;

        static Ship()
        {
            sprite = new[]{
                Bitmap.FromFile("img/spaceship1.png"),
                   Bitmap.FromFile("img/spaceship2.png")
            };
            imgW2 = sprite[0].Width / 2;
            imgH2 = sprite[0].Height / 2;
            P = Math.PI / 180.0;
        }

        int di;
        public double x, y;
        private double dx, dy;
        double angle, dangle, accel, maxAccel;

        public Ship()
        {
            di = 0;
            angle = 0;
            maxAccel = 0.5;
            x = 100;
            y = 100;
            dx = 0;
            dy = 0;
        }

        public void Update(int sinceLast)
        {
            angle += dangle * sinceLast;

            dx += Math.Cos(angle * P) * accel;
            dy += Math.Sin(angle * P) * accel;

            dx *= 0.95;
            dy *= 0.95;

            x += dx * sinceLast;
            y += dy * sinceLast;
        }

        public void Draw(Graphics g)
        {
            var temp = g.Transform;
            g.TranslateTransform((float)x, (float)y);
            g.RotateTransform((float)angle);
            g.TranslateTransform(-imgW2, -imgH2);
//            g.DrawRectangle(Pens.Lime, 0, 0, imgW2 * 2, imgH2 * 2);
            g.DrawImage(sprite[di], 0, 0);
            g.Transform = temp;
        }


        public void Left_Depress()
        {
            dangle = -0.1;
        }
        public void Right_Depress()
        {
            dangle = 0.1;
        }
        public void Up_Depress()
        {
            accel = 0.01;
            di = 1;
        }
        public void Down_Depress()
        {
            accel = -0.01;
        }
        public void Left_Release()
        {
            dangle = 0;
        }
        public void Right_Release()
        {
            dangle = 0;
        }
        public void Up_Release()
        {
            accel = 0;
            di = 0;
        }
        public void Down_Release()
        {
            accel = 0;
        }
    }
    class GameScreen : OutputScreen
    {
        public static string[] Songs;
        static GameScreen()
        {
        }

        Dictionary<GameActions, Action[]> actions;
        Image bg;
        Ship ship;
        public GameScreen()
        {
            bg = Bitmap.FromFile("img/bg.png");
            ship = new Ship();
            actions = new Dictionary<GameActions, Action[]>();
            actions.Add(GameActions.Cancel, new Action[] { MainForm.NullAction, delegate() { NextState = AppStates.Title; } });
            actions.Add(GameActions.P1Left, new Action[] { ship.Left_Depress, ship.Left_Release });
            actions.Add(GameActions.P1Thrust, new Action[] { ship.Down_Depress, ship.Down_Release });
            actions.Add(GameActions.P1Right, new Action[] { ship.Right_Depress, ship.Right_Release });
            actions.Add(GameActions.P1Fire, new Action[] { ship.Up_Depress, ship.Up_Release });
        }



        public override void Dispose()
        {
            base.Dispose();
            bg.Dispose();
            StopSong();
        }

        public override void EnterState(AppStates previousState)
        {
            NextState = AppStates.Game;
            //PlaySong(Songs[OptionsScreen.MusicTrack + 1]);
        }

        public override void LeaveState()
        {
            //StopSong();
        }

        public override void DoKeyCommand(Keys key, int action)
        {
            var act = (from pair in OptionsScreen.KeyOptions
                       where pair.Value == key
                       select pair.Key).FirstOrDefault();
            if (actions.ContainsKey(act))
                actions[act][action]();
        }

        public override void Update(int sinceLastDraw)
        {
            ship.Update(sinceLastDraw);
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(bg, -(int)ship.x, -(int)ship.y);
            ship.Draw(g);
        }
    }
}
