using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace WinGameLib
{
    public abstract class OutputScreen : IDisposable
    {
        public AppStates NextState;
        private SlimDXOggStreamingSample.MusicPlayer song;

        public OutputScreen()
        {
        }

        public virtual void Dispose()
        {
        }

        public abstract void Update(int sinceLastDraw);
        public abstract void DoKeyCommand(Keys key, int action);
        public abstract void Draw(Graphics g);
        public abstract void EnterState(AppStates previousState);
        public abstract void LeaveState();

        public virtual void PlaySong(string filename)
        {
            if (filename != null)
                song = new SlimDXOggStreamingSample.MusicPlayer(filename);
        }

        public void StopSong()
        {
            if (song != null)
            {
                song.Stop();
                song.Dispose();
                song = null;
            }
        }
    }
}
