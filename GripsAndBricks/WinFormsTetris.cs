using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;


namespace GripsAndBricks
{
    class WinFormsTetris : Puzzles.TetrisGame, IDisposable
    {
        private static Rectangle[] TileBounds;
        private static int RefCount;
        static WinFormsTetris()
        {
            TileBounds = new Rectangle[8];
            for (int i = 0; i < 8; ++i)
                TileBounds[i] = new Rectangle((i - 1) * 25, 0, 25, 25);
            RefCount = 0;
        }

        int tileSize, nextX, nextY, scoreX, scoreY, left, top;
        SoundPlayer[] clears;
        SoundPlayer drop, flip;
        private Font ScoreFont;
        private Image PlayBackground, ScoreBackground, TileBoard, GameoverLabel;
        Rectangle playBg, border, nextBgSrc, nextBgDst, next, scoreBg, scoreBorder;

        public WinFormsTetris(int x, int y, int width, int height, int tileSize)
            : base(width, height)
        {
            ScoreFont = new Font(FontFamily.GenericMonospace, 24);
            PlayBackground = Bitmap.FromFile("img/playbg3.png");
            ScoreBackground = Bitmap.FromFile("img/sbg.png");
            TileBoard = Bitmap.FromFile("img/tiles.png");
            GameoverLabel = Bitmap.FromFile("img/gameover.png");
            
            this.tileSize = tileSize;
            clears = new SoundPlayer[]{
                new SoundPlayer("snd/clear1.wav"),
                new SoundPlayer("snd/clear2.wav"),
                new SoundPlayer("snd/clear3.wav"),
                new SoundPlayer("snd/clear4.wav")
            };
            drop = new SoundPlayer("snd/drop.wav");
            flip = new SoundPlayer("snd/flip.wav");
            
            playBg = new Rectangle(x, y, width * tileSize, height * tileSize);
            border = new Rectangle(x - 1, y - 1, width * tileSize + 2, height * tileSize + 2);
            nextBgDst = new Rectangle(x + (width + 2) * tileSize, y + 2 * tileSize, 6 * tileSize, 4 * tileSize);
            nextBgSrc = new Rectangle(0, 0, tileSize * 6, tileSize * 4);
            next = new Rectangle(x + (width + 2) * tileSize, y + 2 * tileSize, 6 * tileSize, 4 * tileSize);
            nextX = x + (Width + 3) * tileSize;
            nextY = y + 3 * tileSize;
            scoreBg = new Rectangle(x + (width + 2) * tileSize, y + 9 * tileSize, 6 * tileSize, 3 * tileSize);
            scoreBorder = new Rectangle(x + (width + 2) * tileSize, y + 9 * tileSize, 6 * tileSize, 3 * tileSize);
            scoreX = x + (width + 2) * tileSize;
            scoreY = y + 10 * tileSize;
            left = x;
            top = y;
            RefCount++;
        }

        public void Dispose()
        {
            foreach (var c in clears)
                c.Dispose();
            drop.Dispose();
            flip.Dispose();
            RefCount--;
            if (RefCount == 0)
            {
                ScoreFont.Dispose();
                PlayBackground.Dispose();
                ScoreBackground.Dispose();
                TileBoard.Dispose();
                GameoverLabel.Dispose();
            }
        }


        
        public void DrawBoard(Graphics g)
        {
            g.DrawImage(PlayBackground, playBg);
            g.DrawRectangle(Pens.White, border);
            DrawPuzzle(g, this, left, top);
            DrawPuzzle(g, Current, left + CursorX * tileSize, top + CursorY * tileSize);

            g.DrawImage(PlayBackground, nextBgDst, nextBgSrc, GraphicsUnit.Pixel);
            g.DrawRectangle(Pens.White, next);
            DrawPuzzle(g, Next, nextX, nextY);

            g.DrawImage(ScoreBackground, scoreBg);
            g.DrawRectangle(Pens.White, scoreBorder);
            g.DrawString(Score.ToString(), ScoreFont, Brushes.White, scoreX, scoreY);

            if (GameOver)
            {
                g.DrawImage(GameoverLabel, left, top + Width / 2 * tileSize);
            }
        }

        void DrawPuzzle(Graphics g, PuzzleFramework.Puzzle p, int dx, int dy)
        {
            for (int y = 0; y < p.Height; ++y)
                for (int x = 0; x < p.Width; ++x)
                    DrawTile(g, p[x, y], dx + x * tileSize, dy + y * tileSize);
        }

        void DrawTile(Graphics g, int tile, int x, int y)
        {
            if (tile > 0)
                g.DrawImage(TileBoard, new Rectangle(x, y, tileSize, tileSize), TileBounds[tile], GraphicsUnit.Pixel);
        }

        protected override void PlayFlip()
        {
            flip.Play();
        }

        protected override void PlayLineClear(int numLines)
        {
            clears[numLines - 1].Play();
        }

        protected override void PlayThump()
        {
            drop.Play();
        }
    }
}
