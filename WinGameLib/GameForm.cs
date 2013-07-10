using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinGameLib
{
    public abstract class GDIGameForm : Form
    {
        public static int ScreenHeight, ScreenWidth;
        public static Action NullAction;
        static GDIGameForm()
        {
            NullAction = delegate() { };
        }

        BufferedGraphics buf;
        Dictionary<Keys, bool> dKeys;
        Dictionary<Keys, int> keys;
        Dictionary<AppStates, OutputScreen> screens;
        AppStates currentState;
        public bool Done;

        public GDIGameForm(int width, int height)
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(width, height);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tetris42";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);

            InitializeGraphics();
            InitializeSound();
            InitializeScreens();
            LoadScreens();
            if (!screens.ContainsKey(AppStates.Title))
                throw new Exception("Whoops! You need to at least define a Title state.");
            InitializeInput();

            Done = false;
            currentState = AppStates.Title;
            screens[currentState].EnterState(AppStates.Title);
        }

        protected abstract void LoadScreens();

        protected void AddScreen(AppStates state, OutputScreen value)
        {
            screens.Add(state, value);
        }

        public void CloseupShop()
        {
            foreach (var output in screens.Values)
                output.Dispose();
            buf.Dispose();
        }

        private void InitializeGraphics()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = this.ClientSize;
            if (buf != null)
                buf.Dispose();
            buf = context.Allocate(CreateGraphics(), ClientRectangle);
        }


        private void InitializeInput()
        {
            keys = new Dictionary<Keys, int>();
            dKeys = new Dictionary<Keys, bool>();
            for (int i = 1; i < 255; ++i)
            {
                Keys key = (Keys)i;
                keys.Add(key, 1);
                dKeys.Add(key, false);
            }
        }

        private void InitializeSound()
        {
            SlimDXOggStreamingSample.DirectSoundWrapper.Initialize(this);
        }

        protected virtual void InitializeScreens()
        {
            ScreenWidth = this.ClientSize.Width;
            ScreenHeight = this.ClientSize.Height;
            screens = new Dictionary<AppStates, OutputScreen>();
        }

        public void UpdateGame(int sinceLastDraw)
        {
            List<Keys> del = new List<Keys>();
            foreach (var key in keys.Keys)
            {
                if (dKeys[key])
                {
                    if (key == Keys.F11 && keys[key] == 1)
                    {
                        if (FormBorderStyle == FormBorderStyle.None)
                        {
                            FormBorderStyle = FormBorderStyle.Fixed3D;
                            WindowState = FormWindowState.Normal;
                        }
                        else
                        {
                            FormBorderStyle = FormBorderStyle.None;
                            WindowState = FormWindowState.Maximized;
                        }
                        ScreenWidth = ClientSize.Width;
                        ScreenHeight = ClientSize.Height;
                        InitializeGraphics();
                    }
                    screens[currentState].DoKeyCommand(key, keys[key]);
                    del.Add(key);
                }
            }

            foreach (var key in del)
            {
                dKeys[key] = false;
            }
            if (screens.ContainsKey(currentState))
            {
                screens[currentState].Update(sinceLastDraw);
                if (currentState != screens[currentState].NextState)
                {
                    screens[currentState].LeaveState();
                    var previousState = currentState;
                    currentState = screens[currentState].NextState;
                    if (currentState == AppStates.Shutdown)
                    {
                        Done = true;
                    }
                    else
                    {
                        screens[currentState].EnterState(previousState);
                    }
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            KeyAct(0, e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            KeyAct(1, e);
        }

        private void KeyAct(int action, KeyEventArgs e)
        {
            if (dKeys.ContainsKey(e.KeyCode))
            {
                keys[e.KeyCode] = action;
                dKeys[e.KeyCode] = true;
            }
        }
        public void Draw()
        {
            if (screens.ContainsKey(currentState))
                screens[currentState].Draw(buf.Graphics);
            buf.Render();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Done = true;
        }

        public static void GenericMain<T>(double targetFPS) where T : GDIGameForm, new()
        {
            T form = new T();
            form.Show();
            DateTime lastDraw = DateTime.Now;
            int sinceLastDraw = 0;
            int millisPerFrame = (int)Math.Ceiling(1000.0 / targetFPS);

            SlimDX.Windows.MessagePump.Run(form, delegate()
            {
                if (form.Done)
                {
                    form.Hide();
                    form.CloseupShop();
                    form.Dispose();
                    Application.Exit();
                }
                else
                {
                    DateTime next = DateTime.Now;
                    sinceLastDraw = (int)(next - lastDraw).TotalMilliseconds;
                    if (sinceLastDraw >= millisPerFrame)
                    {
                        for (int i = 0; i < sinceLastDraw; i += millisPerFrame)
                            form.UpdateGame(millisPerFrame);
                        form.Draw();
                        sinceLastDraw %= millisPerFrame;
                        lastDraw = next;
                    }
                }
            });
        }
    }
}
