using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using WinGameLib;

namespace GripsAndBricks.Screens
{
    class GameScreen : OutputScreen
    {
        static int BoardWidth, BoardHeight;
        public static string[] Songs;
        static GameScreen()
        {
            BoardWidth = 10;
            BoardHeight = 20;
            Songs = new string[]{
                null,
                "music/game1.ogg",
                "music/game2.ogg"
            };
        }

        int tileSize, sinceGameOver;
        WinFormsTetris board1, board2;
        Dictionary<GameActions, Action[]> actions;
        Image bg;
        bool justEntered;
        public GameScreen()
        {
            bg = Bitmap.FromFile("img/bg.png");
            tileSize = (int)Math.Min((MainForm.ScreenWidth * 3) / (10 * BoardWidth), MainForm.ScreenHeight / (BoardHeight + 10));
            int top = (MainForm.ScreenHeight - BoardHeight * tileSize) / 2;

            board1 = new WinFormsTetris(MainForm.ScreenWidth / 2 - tileSize * (BoardWidth + 9), top, BoardWidth, BoardHeight, tileSize);
            board2 = new WinFormsTetris(MainForm.ScreenWidth / 2 + tileSize, top, BoardWidth, BoardHeight, tileSize);

            actions = new Dictionary<GameActions, Action[]>();
            actions.Add(GameActions.Cancel, new Action[] { MainForm.NullAction, delegate() { NextState = AppStates.Menu; } });
            actions.Add(GameActions.P1Left, new Action[] { board1.Left_Depress, board1.Left_Release });
            actions.Add(GameActions.P1Drop, new Action[] { board1.Down_Depress, board1.Down_Release });
            actions.Add(GameActions.P1Right, new Action[] { board1.Right_Depress, board1.Right_Release });
            actions.Add(GameActions.P1Flip, new Action[] { board1.Up_Depress, board1.Up_Release });
            actions.Add(GameActions.P2Left, new Action[] { board2.Left_Depress, board2.Left_Release });
            actions.Add(GameActions.P2Drop, new Action[] { board2.Down_Depress, board2.Down_Release });
            actions.Add(GameActions.P2Right, new Action[] { board2.Right_Depress, board2.Right_Release });
            actions.Add(GameActions.P2Flip, new Action[] { board2.Up_Depress, board2.Up_Release });
        }

        public override void Dispose()
        {
            base.Dispose();
            board1.Dispose();
            board2.Dispose();
            bg.Dispose();
            StopSong();
        }

        public override void EnterState(AppStates previousState)
        {
            justEntered = true;
            NextState = AppStates.Game;
            sinceGameOver = 0;
            board1.Reset();
            board2.Reset();
            PlaySong(Songs[OptionsScreen.MusicTrack + 1]);
        }

        public override void LeaveState()
        {
            StopSong();
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
            if (board1.GameOver || board2.GameOver)
            {
                if (sinceGameOver == 0)
                {
                    StopSong();
                    if(board2.GameOver)
                        PlaySong("music/player1win.ogg");
                    else if (board1.GameOver)
                        PlaySong("music/player2win.ogg");
                }
                sinceGameOver += sinceLastDraw;
                if (sinceGameOver > 5000)
                    NextState = AppStates.Menu;
            }
            else
            {
                board1.Update(sinceLastDraw);
                board2.Update(sinceLastDraw);
            }
        }

        public override void Draw(Graphics g)
        {
            if (justEntered || board1.GameOver || board2.GameOver)
            {
                //blank the screen once
                g.DrawImage(bg, 0, 0, MainForm.ScreenWidth, MainForm.ScreenHeight);
                justEntered = false;
                //but only draw the updated parts thereafter
            }
            board1.DrawBoard(g);
            board2.DrawBoard(g);
        }
    }
}
