using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordRPC;
using ImGuiNET;

namespace CV_IMGUI
{
    public partial class HOME : Form
    {
        private string imguiIniPath;
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_QUIT = 1;
        private const int HOTKEY_STOP = 4;
        Color panelcolor = Color.FromArgb(13, 15, 15);
        public HOME()
        {
            InitializeComponent();
            //LoadCustomFont();
            //ApplyCustomFont();
            imguiIniPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "imgui.ini");
            //LoadImGuiIni();
            //stopwatch = new Stopwatch();
            //frameCount = 0;

            //fpsTimer = new Timer();
            //fpsTimer.Interval = 1000 / 60;
            //fpsTimer.Tick += (s, e) => UpdateFPS(FPS);
            //fpsTimer.Start();

            //stopwatch.Start();
        }
        private Stopwatch stopwatch;
        private int frameCount;
        private Timer fpsTimer;
        private PrivateFontCollection privateFontCollection = new PrivateFontCollection();
        private void LoadCustomFont()
        {
            string resourceName = /*"C:\\Users\\BRUUUH CHEATS\\Documents\\MY PANELS\\CV_IMGUI\\CV_IMGUI\\bin\\Debug\\fontlogo.ttf";*/"CV_IMGUI.fontlogo.ttf";

            using (Stream fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (fontStream != null)
                {
                    byte[] fontData = new byte[fontStream.Length];
                    fontStream.Read(fontData, 0, (int)fontStream.Length);

                    IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
                    Marshal.Copy(fontData, 0, fontPtr, fontData.Length);

                    privateFontCollection.AddMemoryFont(fontPtr, fontData.Length);

                    Marshal.FreeCoTaskMem(fontPtr);

                    if (privateFontCollection.Families.Length > 0)
                    {
                        Font customFont = new Font(privateFontCollection.Families[0], 12f);

                        foreach (Control control in this.Controls)
                        {
                            control.Font = customFont;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No font families loaded.");
                    }
                }
                else
                {
                    MessageBox.Show("Font resource not found.");
                }
            }
        }
        private void ApplyCustomFont()
        {
            if (privateFontCollection.Families.Length > 0)
            {
                FontFamily customFontFamily = privateFontCollection.Families[0];

                Font customFont = new Font(customFontFamily, 16.0f, FontStyle.Bold);

                //label1.Font = customFont;
            }
        }
        private void SaveImGuiIni()
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(exePath, "imgui.ini");

            int posX = this.Location.X;
            int posY = this.Location.Y;
            int width = this.Size.Width;
            int height = this.Size.Height;

            string iniContent = $"[Window]\nPos={posX},{posY}\nSize={width},{height}\n" +
                                $"[Settings]";
            File.WriteAllText(imguiIniPath, iniContent);
            RegisterHotKey(this.Handle, HOTKEY_QUIT, 0x0000, (int)Keys.Delete);
            RegisterHotKey(this.Handle, HOTKEY_STOP, 0x0001, (int)Keys.F8);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80; 
                return cp;
            }
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //int id = m.WParam.ToInt32();

            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_QUIT)
            {
                Application.Exit();
            }
            //else if (id == 4)
            //{
            //    //RPC.StopRPC();
            //    //RPC.StopRPC();
            //}

            base.WndProc(ref m);
        }
        //private void UpdateFPS(Label label)
        //{
        //    frameCount++;

        //    if (stopwatch.ElapsedMilliseconds >= 1000)
        //    {
        //        double fps = (double)frameCount / (stopwatch.ElapsedMilliseconds / 1000.0);
        //        frameCount = 0;
        //        stopwatch.Restart();

        //        label.Text = $"{fps:F2}";
        //    }
        //}
        private void LoadImGuiIni()
        {
            if (File.Exists(imguiIniPath))
            {
                string[] iniLines = File.ReadAllLines(imguiIniPath);

                foreach (string line in iniLines)
                {
                    if (line.StartsWith("Pos="))
                    {
                        var pos = line.Replace("Pos=", "").Split(',');
                        this.Location = new System.Drawing.Point(int.Parse(pos[0]), int.Parse(pos[1]));
                    }
                    else if (line.StartsWith("Size="))
                    {
                        var size = line.Replace("Size=", "").Split(',');
                        this.Size = new System.Drawing.Size(int.Parse(size[0]), int.Parse(size[1]));
                    }
                    //else if (line.StartsWith("Percentage="))
                    //{
                    //    percentageValue = int.Parse(line.Replace("Percentage=", ""));
                    //    Console.WriteLine($"Loaded Percentage: {percentageValue}%");
                    //}
                }
            }
        }
        private void HOME_Move(object sender, EventArgs e)
        {
            SaveImGuiIni();
        }

        private void HOME_Resize(object sender, EventArgs e)
        {
            SaveImGuiIni();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2Button2.Checked = false;
            guna2Button3.Checked = false;
            guna2Button4.Checked = false;
            guna2Button5.Checked = false;
            if (guna2Button1.Checked)
            {
                AIMBOT BRUUUH = new AIMBOT();
                BRUUUH.Show();
            }
            else
            {
                AIMBOT BRUUUH = new AIMBOT();
                BRUUUH.Hide();
            }        
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2Button2.Checked = false;
            guna2Button1.Checked = false;
            guna2Button4.Checked = false;
            guna2Button5.Checked = false;
            SNIPER BRUUUH = new SNIPER();
            BRUUUH.Show();
        }
        private SETTINGS settingsForm;
        private void HOME_Load(object sender, EventArgs e)
        {
            LoadImGuiIni();
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;

            int x = workingArea.Left + 20;
            int y = workingArea.Top + 50;

            Location = new Point(x, y);
            if (SoundManager.IsSoundEnabled)
            {
                SoundManager.PlaySound(Properties.Resources.welcome);
            }
            //RPC.rpctimestamp = Timestamps.Now;
            //RPC.InitializeRPC();
        }

        private void HOME_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, HOTKEY_QUIT);
            UnregisterHotKey(this.Handle, HOTKEY_STOP);
        }

        private void HOME_FormClosed(object sender, FormClosedEventArgs e)
        {
            //UnregisterHotKey(this.Handle, HOTKEY_ID);
            //base.OnFormClosed(e);
        }

        private void FPS_Click(object sender, EventArgs e)
        {
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2Button1.Checked = false;
            guna2Button3.Checked = false;
            guna2Button4.Checked = false;
            guna2Button5.Checked = false;
            ESP BRUUUH = new ESP();
            BRUUUH.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            guna2Button2.Checked = false;
            guna2Button3.Checked = false;
            guna2Button1.Checked = false;
            guna2Button5.Checked = false;
            SETTINGS BRUUUH = new SETTINGS();
            BRUUUH.Show();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            guna2Button2.Checked = false;
            guna2Button3.Checked = false;
            guna2Button4.Checked = false;
            guna2Button1.Checked = false;
            PROFILE BRUUUH = new PROFILE();
            BRUUUH.Show();
        }
    }
}
