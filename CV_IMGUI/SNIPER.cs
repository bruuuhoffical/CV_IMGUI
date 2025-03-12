using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;
using Bruuuh;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;
using System.Timers;

namespace CV_IMGUI
{
    public partial class SNIPER : Form
    {
        private ParticleSystem particleSystem;
        private const int WM_HOTKEY = 0x0312;
        private int _nextHotkeyId = 1;
        Evelyn bruuuh = new Evelyn();
        string on = " Enabled !";
        string off = " Disabled !";
        string wait = "Enabling";
        string wait1 = "Enabling....";
        string open = "Open Emulator !";
        string nope = "Unable Able To Enable !";
        string soon = "Under Update Coming Soon !";
        
        string speedtimersearch = "01 00 00 00 02 2B 07 3D";
        string speedtimerreplace = "01 00 00 00 92 E4 78 3D";
        string wallsearch = "3F AE 47 81 3F 00 1A B7 EE DC 3A 9F ED 30 00 4F E2 43 2A B0 EE EF 0A 60 F4 43 6A F0 EE 1C 00 8A E2 43 5A F0 EE 8F 0A 48 F4 43 2A F0 EE 43 7A B0";
        string wallreplace = "BF AE 47 81 3F 00 1A B7 EE DC 3A 9F ED 30 00 4F E2 43 2A B0 EE EF 0A 60 F4 43 6A F0 EE 1C 00 8A E2 43 5A F0 EE 8F 0A 48 F4 43 2A F0 EE 43 7A B0";
        string glicths = "3f ae 47 81 3f 00 1a b7 ee dc 3a 9f ed 30";
        string glitchr = "bf ae 47 81 3f 00 1a b7 ee dc 3a 9f ed 30";
        string cameraleftsearch = "00 00 00 81 95 E3 3F 00 00 80 3F 00 00 80 3F 0A D7 A3 3D 00 00 00 00 00 00 5C 43 00 00 90 42 00 00 B4 42 96 00 00 00 00 00 00 00 00 00 00 2B 00 00 80 2B 00 00 00 00 04 00 00 00 00 00 80 3F 00";
        string cameraleftreplace = "22 8E C3 40 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 80 7F 00 00 80 7F 00 00 80 7F 00 00 80 FF";
        List<long> addresses = new List<long>();

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private readonly Dictionary<int, HotkeyInfo> _hotkeys = new Dictionary<int, HotkeyInfo>();
        public SNIPER()
        {
            InitializeComponent();
            LoadCustomFont();
            particleSystem = new ParticleSystem();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            particleSystem.DrawParticles(e.Graphics);
        }
        private PrivateFontCollection privateFontCollection = new PrivateFontCollection();
        Color panelcolor = Color.FromArgb(13, 15, 15);
        private void LoadCustomFont()
        {
            PrivateFontCollection customFontCollection = new PrivateFontCollection();
            byte[] fontData = Properties.Resources.fontlogo;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
            Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            customFontCollection.AddMemoryFont(fontPtr, fontData.Length);
            Marshal.FreeCoTaskMem(fontPtr);

            Font customFont = new Font(customFontCollection.Families[0], 15f);

            label1.Font = customFont;
        }
        private void SNIPER_Load(object sender, EventArgs e)
        {

        }
        #region HOTKEY
        private class HotkeyInfo
        {
            public Guna2Button Button { get; }
            public Keys Key { get; }
            public uint Modifiers { get; }

            public HotkeyInfo(Guna2Button button, Keys key, uint modifiers)
            {
                Button = button;
                Key = key;
                Modifiers = modifiers;
            }
        }
        private void RegisterHotKeyForButton(Guna2Button button, Keys key, bool control, bool alt, bool shift)
        {
            int id = _nextHotkeyId++;
            button.Tag = id;

            uint modifiers = (control ? (uint)KeyModifiers.Control : 0) |
                             (alt ? (uint)KeyModifiers.Alt : 0) |
                             (shift ? (uint)KeyModifiers.Shift : 0);

            if (RegisterHotKey(this.Handle, id, modifiers, (uint)key))
            {
                _hotkeys[id] = new HotkeyInfo(button, key, modifiers);
                button.Text = $"{key}";
            }
            else
            {
                MessageBox.Show($"Failed to register hotkey: {key}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                int id = m.WParam.ToInt32();
                if (_hotkeys.TryGetValue(id, out var hotkeyInfo))
                {
                    PerformActionForButton(hotkeyInfo.Button);
                }
            }
            base.WndProc(ref m);
        }
        private void StartKeyCaptureButton3()
        {
            speedon.Text = "?";
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDownButton3;
        }
        private void StartKeyCaptureButton4()
        {
            speedoff.Text = "?";
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDownButton4;
        }
        private void StartKeyCaptureButton5()
        {
            wallon.Text = "?";
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDownButton5;
        }
        private void StartKeyCaptureButton6()
        {
            walloff.Text = "?";
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDownButton6;
        }
        private void StartKeyCaptureButton7()
        {
            camon.Text = "?";
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDownButton7;
        }
        private void StartKeyCaptureButton8()
        {
            camoff.Text = "?";
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDownButton8;
        }
        private void Form1_KeyDownButton3(object sender, KeyEventArgs e)
        {
            RegisterHotKeyForButton(speedon, e.KeyCode, e.Control, e.Alt, e.Shift);
            this.KeyDown -= Form1_KeyDownButton3;
            
        }

        private void Form1_KeyDownButton4(object sender, KeyEventArgs e)
        {
            RegisterHotKeyForButton(speedoff, e.KeyCode, e.Control, e.Alt, e.Shift);
            this.KeyDown -= Form1_KeyDownButton4;
        }
        private void Form1_KeyDownButton5(object sender, KeyEventArgs e)
        {
            RegisterHotKeyForButton(wallon, e.KeyCode, e.Control, e.Alt, e.Shift);
            this.KeyDown -= Form1_KeyDownButton5;
        }
        private void Form1_KeyDownButton6(object sender, KeyEventArgs e)
        {
            RegisterHotKeyForButton(walloff, e.KeyCode, e.Control, e.Alt, e.Shift);
            this.KeyDown -= Form1_KeyDownButton6;
        }
        private void Form1_KeyDownButton7(object sender, KeyEventArgs e)
        {
            RegisterHotKeyForButton(camon, e.KeyCode, e.Control, e.Alt, e.Shift);
            this.KeyDown -= Form1_KeyDownButton7;
        }
        private void Form1_KeyDownButton8(object sender, KeyEventArgs e)
        {
            RegisterHotKeyForButton(camoff, e.KeyCode, e.Control, e.Alt, e.Shift);
            this.KeyDown -= Form1_KeyDownButton8;
        }
        private void PerformActionForButton(Guna2Button button)
        {
            if (button == speedon)
            {
                guna2ToggleSwitch1.Checked = true;
            }
            else if (button == speedoff)
            {
                guna2ToggleSwitch2.Checked = true;
            }
            else if (button == wallon)
            {
                wallhack.Checked = true;
            }
            else if (button == walloff)
            {
                wallhack.Checked = false;
            }
            else if (button == camon)
            {
                cameraup.Checked = true;
            }
            else if (button == camoff)
            {
                cameraup.Checked = false;
            }
        }
        private void InitializeButtons()
        {
        }

        private void UnregisterAllHotkeys()
        {
            foreach (var id in _hotkeys.Keys)
            {
                UnregisterHotKey(this.Handle, id);
            }
            _hotkeys.Clear();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            foreach (var id in _hotkeys.Keys.ToList())
            {
                UnregisterHotKey(this.Handle, id);
            }
            base.OnFormClosing(e);
        }

        [Flags]
        private enum KeyModifiers : uint
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }
        #endregion
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            guna2Panel1.BackColor = panelcolor;
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {
            guna2Panel2.BackColor = panelcolor;
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {
            guna2Panel3.BackColor = panelcolor;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            particleSystem.UpdateParticles();
            Invalidate();
        }

        private void ssckey_Click(object sender, EventArgs e)
        {
            
        }

        private void sskey_Click(object sender, EventArgs e)
        {
            
        }

        private async void sniperscope_CheckedChanged(object sender, EventArgs e)
        {
            if (sniperscope.Checked)
            {   
                string search = "8C 3F 8F C2 F5 3C CD CC CC 3D 06 00 00 00 00 00 00 00 00 00 00 00 00 00 F0 41 00 00 48 42 00 00 00 3F 33 33 13 40 00 00 B0 3F 00 00 80 3F 01";
                string replace = "8C 3F 8F C2 F5 3C CD CC CC 3D 06 00 00 00 00 00 FF FF 00 00 00 00 00 00 F0 41 00 00 48 42 00 00 00 3F 33 33 13 40 00 00 B0 3F 00 00 80 3F 01";
                bool k = false;

                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    STA.Text = $"{open}";
                }
                else
                {
                    bruuuh.OpenProcess("HD-Player");
                    STA.Text = $"{wait} Sniper Scope";
                    int i2 = 22000000;
                    IEnumerable<long> wl = await bruuuh.AoBScan(search, writable: true);
                    string u = "0x" + wl.FirstOrDefault().ToString("X");
                    if (wl.Count() != 0)
                    {
                        for (int i = 0; i < wl.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl.ElementAt(i).ToString("X"), "bytes", replace);
                        }
                        k = true;
                    }

                    if (k == true)
                    {
                        STA.Text = $"Sniper Scope {on}";
                    }
                    else
                    {
                        STA.Text = $"{nope} Sniper Scope";
                        SoundManager.PlaySound(Properties.Resources.welcome);
                    }
                }
            }
            else
            {

            }
        }

        private async void sniperswitch_CheckedChanged(object sender, EventArgs e)
        {
            if (sniperswitch.Checked)
            {
                string search = "00 00 00 81 95 E3 3F 00 00 80 3F 00 00 80 3F 0A D7 A3 3D 00 00 00 00 00 00 5C 43 00 00 90 42 00 00 B4 42 96 00 00 00 00 00 00 00 00 00 00 3F 00 00 80 3E 00 00 00 00 04 00 00 00 00 00 80 3F 00";
                string replace = "00 00 00 81 95 E3 3F 00 00 80 3F 00 00 80 3F 0A D7 A3 3D 00 00 00 00 00 00 5C 43 00 00 90 42 00 00 B4 42 96 00 00 00 00 00 00 00 00 00 00 2B 00 00 80 2B 00 00 00 00 04 00 00 00 00 00 80 3F 00";
                bool k = false;

                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    STA.Text = $"{open}";
                }
                else
                {
                    bruuuh.OpenProcess("HD-Player");
                    STA.Text = $"{wait} Sniper Switch";
                    int i2 = 22000000;
                    IEnumerable<long> wl = await bruuuh.AoBScan(search, writable: true);
                    string u = "0x" + wl.FirstOrDefault().ToString("X");
                    if (wl.Count() != 0)
                    {
                        for (int i = 0; i < wl.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl.ElementAt(i).ToString("X"), "bytes", replace);
                        }
                        k = true;
                    }

                    if (k == true)
                    {
                        STA.Text = $"Sniper Switch {on}";
                        SoundManager.PlaySound(Properties.Resources.welcome);
                    }
                    else
                    {
                        STA.Text = $"{nope} Switch Switch";
                    }
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private async void guest_CheckedChanged(object sender, EventArgs e)
        {
            if (guest.Checked)
            {
                string search = "90 8D E5 28 90 8D E5 24 90 8D E5 20 90 8D E5 1C 90 8D E5 18 90 8D E5 10 90 8D E5 08 60 8D E5 82 00 8D E8 10 17 02 E3 3C FF 2F E1 DC A0 94 E5 00 00 5A E3 0F 00 00 1A 00 00 58 E3 01 00 00 1A 00";
                string replace = "90 8D E5 28 90 8D E5 24 90 8D E5 20 90 8D E5 1C 90 8D E5 18 90 8D E5 10 90 8D E5 08 60 8D E5 82 00 8D AE 90 17 44 3F EF F0 3F E1 DC A0 94 E5 00 00 5A E3 0F 00 00 1A 00 00 58 E3 01 00 00 1A 00";
                bool k = false;

                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    STA.Text = $"{open}";
                }
                else
                {
                    bruuuh.OpenProcess("HD-Player");
                    STA.Text = $"Bypassing Wallhack";
                    int i2 = 22000000;
                    IEnumerable<long> wl = await bruuuh.AoBScan(search, writable: true);
                    string u = "0x" + wl.FirstOrDefault().ToString("X");
                    if (wl.Count() != 0)
                    {
                        for (int i = 0; i < wl.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl.ElementAt(i).ToString("X"), "bytes", replace);
                        }
                        k = true;
                    }

                    if (k == true)
                    {
                        STA.Text = $"Bypassing Wallhack Success";
                    }
                    else
                    {
                        STA.Text = $"Failed To Bypass";
                    }
                }
            }
            else
            {

            }
        }
        private void ExecuteCommand(string command)
        {
            ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process proc = new Process
            {
                StartInfo = procStartInfo
            };
            proc.Start();
            proc.WaitForExit();
            //ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            //processInfo.CreateNoWindow = true;
            //processInfo.UseShellExecute = false;
            //processInfo.RedirectStandardOutput = true;
            //processInfo.RedirectStandardError = true;

            //Process process = Process.Start(processInfo);
            //process.WaitForExit();

            //string output = process.StandardOutput.ReadToEnd();
            //string error = process.StandardError.ReadToEnd();

            //if (!string.IsNullOrEmpty(output))
            //    Console.WriteLine(output);

            //if (!string.IsNullOrEmpty(error))
            //    Console.WriteLine(error);
        }
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int NtSuspendProcess(IntPtr processHandle);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int NtResumeProcess(IntPtr processHandle);

        private string processName = "HD-Player";
        private void fakelag_CheckedChanged(object sender, EventArgs e)
        {

        }
        public void SuspendProcess(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            foreach (var process in processes)
            {
                NtSuspendProcess(process.Handle);
            }
        }

        public void ResumeProcess(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            foreach (var process in processes)
            {
                NtResumeProcess(process.Handle);
            }
        }
        private void fakelagtime_Scroll(object sender, ScrollEventArgs e)
        {
           
        }

        private void fakelagkey_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ResumeProcess(processName);
            timer2.Stop();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch1.Checked)
            {
                if (addresses.Count > 0)
                {
                    foreach (var address in addresses)
                    {
                        bruuuh.WriteMemory(address.ToString("X"), "bytes", speedtimerreplace);
                    }

                    STA.Text = $"Speed Timer Enabled";
                    SoundManager.PlaySound(Properties.Resources.welcome);
                }
                else
                {
                    STA.Text = $"No addresses to replace. Please scan first.";
                }
            }
            else
            {
                if (addresses.Count > 0)
                {
                    foreach (var address in addresses)
                    {
                        bruuuh.WriteMemory(address.ToString("X"), "bytes", speedtimersearch);
                    }

                    STA.Text = $"Speed Timer Disabled";
                    SoundManager.PlaySound(Properties.Resources.welcome);
                }
                else
                {
                    STA.Text = $"No addresses to replace. Please scan first.";
                }
            }
        }

        private void STA_Click(object sender, EventArgs e)
        {

        }

        private async void guna2ToggleSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch2.Checked)
            {
                string search = "FF FF FF FF 08 00 00 00 00 00 60 40 CD CC 8C 3F 8F C2 F5 3C CD CC CC 3D 06 00 00 00 00 00 00 00 00 00 00 00 00 00 F0 41 00 00 48 42 00 00 00 3F 33 33 13 40 00 00 B0 3F 00 00 80 3F 01 00 00 3F ?? ?? ?? ??";
                string replace = "FF FF FF FF 08 00 00 00 00 00 60 40 E0 B1 FF FF E0 B1 FF FF E0 B1 FF FF E0 B1 FF FF E0 B1 FF FF 00 00 00 00 00 00 F0 41 00 00 48 42 00 00 00 3F 33 33 13 40 00 00 B0 3F 00 00 29 5C 01 00 00 ?? ?? ?? ?? ??";
                bool k = false;

                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    STA.Text = $"{open}";
                }
                else
                {
                    bruuuh.OpenProcess("HD-Player");
                    STA.Text = $"Enabling Sniper Scope Tracking";
                    int i2 = 22000000;
                    IEnumerable<long> wl = await bruuuh.AoBScan(search, writable: true);
                    string u = "0x" + wl.FirstOrDefault().ToString("X");
                    if (wl.Count() != 0)
                    {
                        for (int i = 0; i < wl.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl.ElementAt(i).ToString("X"), "bytes", replace);
                        }
                        k = true;
                    }

                    if (k == true)
                    {
                        STA.Text = $"Enabled Sniper Scope Tracking\r\n";
                        SoundManager.PlaySound(Properties.Resources.welcome);
                    }
                    else
                    {
                        STA.Text = $"Failed To Enable";
                    }
                }
                //if (addresses.Count > 0)
                //{
                //    foreach (var address in addresses)
                //    {
                //        bruuuh.WriteMemory(address.ToString("X"), "bytes", speedtimersearch);
                //    }

                //    STA.Text = $"Speed Timer Disabled";
                //    Console.Beep(400, 300);
                //}
                //else
                //{
                //    STA.Text = $"No modified addresses to revert.";
                //}
            }
        }

        private async void guna2Button1_Click_1(object sender, EventArgs e)
        {
            string search = "82 00 8D E8 10 17 02 E3 3C FF 2F E2";
            string replace = "82 00 8D E8 10 17 02 E3 00 F0 20 E3";
            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                STA.Text = $"{open}";
            }
            else
            {
                bruuuh.OpenProcess("HD-Player");
                STA.Text = $"Enabling Bypass Wall Hack";
                int i2 = 22000000;
                IEnumerable<long> wl = await bruuuh.AoBScan(search, writable: true);
                string u = "0x" + wl.FirstOrDefault().ToString("X");
                if (wl.Count() != 0)
                {
                    for (int i = 0; i < wl.Count(); i++)
                    {
                        i2++;
                        bruuuh.WriteMemory(wl.ElementAt(i).ToString("X"), "bytes", replace);
                    }
                    k = true;
                }

                if (k == true)
                {
                    STA.Text = $"Bypass Wall Hack Enabled";
                    SoundManager.PlaySound(Properties.Resources.welcome);
                }
                else
                {
                    STA.Text = $"Failed To Enable";
                }
            }
        }

        private async void aimbotkey_Click(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                STA.Text = $"{open}";
                return;
            }

            bruuuh.OpenProcess("HD-Player");
            STA.Text = $"Scanning for Speed Timer...";
            addresses.Clear();

            IEnumerable<long> wl = await bruuuh.AoBScan(speedtimersearch, writable: true);
            addresses.AddRange(wl);

            if (addresses.Count > 0)
            {
                STA.Text = $"Scan completed ! {addresses.Count}";
                //Console.Beep(500, 300);
            }
            else
            {
                STA.Text = $"Failed to find addresses for Speed Timer.";
            }
        }

        private void speedon_Click(object sender, EventArgs e)
        {
            StartKeyCaptureButton3();
        }

        private void speedoff_Click(object sender, EventArgs e)
        {
            StartKeyCaptureButton4();
        }

        private void wallon_Click(object sender, EventArgs e)
        {
            StartKeyCaptureButton5();
        }

        private void walloff_Click(object sender, EventArgs e)
        {
            StartKeyCaptureButton6();
        }

        private async void wallhack_CheckedChanged(object sender, EventArgs e)
        {
            if (wallhack.Checked)
            {
                
                if (addresses.Count > 0)
                {
                    foreach (var address in addresses)
                    {
                        bruuuh.WriteMemory(address.ToString("X"), "bytes", wallreplace);
                    }

                    STA.Text = $"Wallhack Enabled";
                    SoundManager.PlaySound(Properties.Resources.welcome);
                }
                else
                {
                    STA.Text = $"No addresses to replace. Please scan first.";
                }
            }
            else
            {
                if (addresses.Count > 0)
                {
                    foreach (var address in addresses)
                    {
                        bruuuh.WriteMemory(address.ToString("X"), "bytes", wallsearch);
                    }

                    STA.Text = $"Wall Hack Disabled";
                    Console.Beep(400, 300);
                }
                else
                {
                    STA.Text = $"No modified addresses to revert.";
                }
            }
        }

        private async void wallhackscan_Click(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                STA.Text = $"{open}";
                return;
            }

            bruuuh.OpenProcess("HD-Player");
            STA.Text = $"Scanning for Wallhack...";
            addresses.Clear();

            IEnumerable<long> wl = await bruuuh.AoBScan(wallsearch, writable: true);
            addresses.AddRange(wl);

            if (addresses.Count > 0)
            {
                STA.Text = $"Scan completed ! {addresses.Count}";
                //Console.Beep(500, 300);
            }
            else
            {
                STA.Text = $"Failed to find addresses for Wallhack.";
            }
        }

        private async void bypass_CheckedChanged(object sender, EventArgs e)
        {
            if (bypass.Checked)
            {
                string search = "01 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 CB 00 00 00 ?? ?? ?? ?? 00 00 00 00 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 00 ?? 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ";
                string replace = "00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 CB 00 00 00 F7 19 00 00 F8 19 FF FF FF FF FF FF FF FF 3F CD CC 4C 00 0 00 00 00 00 00 00 00 00 00 00 00 00 00";
                bool k = false;

                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    STA.Text = $"{open}";
                }
                else
                {
                    bruuuh.OpenProcess("HD-Player");
                    STA.Text = $"{wait} Aim Sniper";
                    int i2 = 22000000;
                    IEnumerable<long> wl = await bruuuh.AoBScan(search, writable: true);
                    string u = "0x" + wl.FirstOrDefault().ToString("X");
                    if (wl.Count() != 0)
                    {
                        for (int i = 0; i < wl.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl.ElementAt(i).ToString("X"), "bytes", replace);
                        }
                        k = true;
                    }

                    if (k == true)
                    {
                        STA.Text = $"Aim Sniper {on}";
                    }
                    else
                    {
                        STA.Text = $"{nope} Aim Sniper";
                        SoundManager.PlaySound(Properties.Resources.welcome);
                    }
                }
            }
            else
            {

            }
        }

        private async void resetguest_CheckedChanged(object sender, EventArgs e)
        {
            if (resetguest.Checked)
            {
                string search = "10 4C 2D E9 08 B0 8D E2 0C 01 9F E5 00 00 8F E0 00";
                string replace = "01 00 A0 E3 1E FF 2F E1";
                bool k = false;

                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    STA.Text = $"{open}";
                }
                else
                {
                    bruuuh.OpenProcess("HD-Player");
                    STA.Text = $"Reseting Guest Account";
                    int i2 = 22000000;
                    IEnumerable<long> wl = await bruuuh.AoBScan(search, writable: true);
                    string u = "0x" + wl.FirstOrDefault().ToString("X");
                    if (wl.Count() != 0)
                    {
                        for (int i = 0; i < wl.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl.ElementAt(i).ToString("X"), "bytes", replace);
                        }
                        k = true;
                    }

                    if (k == true)
                    {
                        STA.Text = $"Reset Guest Account Success";
                    }
                    else
                    {
                        STA.Text = $"Failed To Reset";
                    }
                }
            }
            else
            {

            }
        }

        private async void guna2ToggleSwitch9_CheckedChanged(object sender, EventArgs e)
        {
            if (norecoil.Checked)
            {
                string search = "00 0A 81 EE 10 0A 10 EE 10 8C BD E8 00 00 7A 44 F0 48 2D E9 10 B0 8D E2 02 8B 2D ED 08 D0 4D E2 00 50 A0 E1 10 1A 08 EE 08 40 95 E5  00 00 54 E3";
                string replace = "00 0A 81 EE 10 0A 10 EE 10 8C BD E8 F0 48 2D E9 10 B0 8D E2 02 8B 2D ED 08 D0 4D E2 00 50 A0 E1 10 1A 08 EE 08 40 95 E5 00 00 54 E3";
                bool k = false;

                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    STA.Text = $"{open}";
                }
                else
                {
                    bruuuh.OpenProcess("HD-Player");
                    STA.Text = $"{wait} No Recoil";
                    int i2 = 22000000;
                    IEnumerable<long> wl = await bruuuh.AoBScan(search, writable: true);
                    string u = "0x" + wl.FirstOrDefault().ToString("X");
                    if (wl.Count() != 0)
                    {
                        for (int i = 0; i < wl.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl.ElementAt(i).ToString("X"), "bytes", replace);
                        }
                        k = true;
                    }

                    if (k == true)
                    {
                        STA.Text = $"No Recoil {on}";
                    }
                    else
                    {
                        STA.Text = $"{nope} No Recoil";
                        SoundManager.PlaySound(Properties.Resources.welcome);
                    }
                }
            }
            else
            {

            }
        }

        private async void guna2ToggleSwitch8_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch8.Checked)
            {
                string search = "6D 00 00 EB 00 0A B7 EE 10 0A 01 EE 00 0A 31 EE 10 5A 01 EE 00 0A 21 EE 10 0A 10 EE 30 88 BD E8";
                string replace = "FF 02 44 E3 00 0A B7 EE 10 0A";
                bool k = false;

                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    STA.Text = $"{open}";
                }
                else
                {
                    bruuuh.OpenProcess("HD-Player");
                    STA.Text = $"{wait} Fast Reload";
                    int i2 = 22000000;
                    IEnumerable<long> wl = await bruuuh.AoBScan(search, writable: true);
                    string u = "0x" + wl.FirstOrDefault().ToString("X");
                    if (wl.Count() != 0)
                    {
                        for (int i = 0; i < wl.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl.ElementAt(i).ToString("X"), "bytes", replace);
                        }
                        k = true;
                    }

                    if (k == true)
                    {
                        STA.Text = $"Fast Reload {on}";
                    }
                    else
                    {
                        STA.Text = $"{nope} Fast Reload";
                        SoundManager.PlaySound(Properties.Resources.welcome);
                    }
                }
            }
            else
            {

            }
        }

        private async void guna2ToggleSwitch6_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch6.Checked)
            {
                string search = "AC C5 27 37 30 48 2D E9 01 40 A0 E1 20 10 9F E5 00 50 A0 E1";
                string replace = "AC E9 90 3F 30 48 2D E9 01 40 A0 E1 20 10 9F E5 00 50 A0 E1";
                bool k = false;

                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    STA.Text = $"{open}";
                }
                else
                {
                    bruuuh.OpenProcess("HD-Player");
                    STA.Text = $"{wait} Magic Bullet";
                    int i2 = 22000000;
                    IEnumerable<long> wl = await bruuuh.AoBScan(search, writable: true);
                    string u = "0x" + wl.FirstOrDefault().ToString("X");
                    if (wl.Count() != 0)
                    {
                        for (int i = 0; i < wl.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl.ElementAt(i).ToString("X"), "bytes", replace);
                        }
                        k = true;
                    }

                    if (k == true)
                    {
                        STA.Text = $"Magic Bullet {on}";
                    }
                    else
                    {
                        STA.Text = $"{nope} Magic Bullet";
                        //SoundManager.PlaySound(Properties.Resources.welcome);
                    }
                }
            }
            else
            {

            }
        }

        private async void night_CheckedChanged(object sender, EventArgs e)
        {
            if (night.Checked)
            {
                string search = "A4 70 7D 3F 3A CD 13 3F 0A D7 23 3C BD 37 86 35 00 00 51 E3 04 10 91 15";
                string replace = "A4 70 7D 3F 3A CD 13 3F 0A D7 23 3C 00 00 80 BF 00 20 A0 E3 04 10 91 15";
                bool k = false;

                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    STA.Text = $"{open}";
                }
                else
                {
                    bruuuh.OpenProcess("HD-Player");
                    STA.Text = $"{wait} Night Mode";
                    int i2 = 22000000;
                    IEnumerable<long> wl = await bruuuh.AoBScan(search, writable: true);
                    string u = "0x" + wl.FirstOrDefault().ToString("X");
                    if (wl.Count() != 0)
                    {
                        for (int i = 0; i < wl.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl.ElementAt(i).ToString("X"), "bytes", replace);
                        }
                        k = true;
                    }

                    if (k == true)
                    {
                        STA.Text = $"Night Mode {on}";
                    }
                    else
                    {
                        STA.Text = $"{nope} Night Mode";
                        SoundManager.PlaySound(Properties.Resources.welcome);
                    }
                }
            }
            else
            {

            }
        }

        private async void guna2ToggleSwitch3_CheckedChanged(object sender, EventArgs e)
        {
            if (glitch.Checked)
            {
                string search = "00 00 C0 3F 01 00 00 00 00 00 34 42 9A 99 99 3E 0A D7 A3 3D 9A 99 99 3E 00 00 00 3F 8F C2 F5 3E";
                string replace = "00 00 00 00 01 00 00 00 00";
                bool k = false;

                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    STA.Text = $"{open}";
                }
                else
                {
                    bruuuh.OpenProcess("HD-Player");
                    STA.Text = $"Enabling Glitch Fire";
                    int i2 = 22000000;
                    IEnumerable<long> wl = await bruuuh.AoBScan(search, writable: true);
                    string u = "0x" + wl.FirstOrDefault().ToString("X");
                    if (wl.Count() != 0)
                    {
                        for (int i = 0; i < wl.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl.ElementAt(i).ToString("X"), "bytes", replace);
                        }
                        k = true;
                    }

                    if (k == true)
                    {
                        STA.Text = $"Glitch Fire Enabled";
                    }
                    else
                    {
                        STA.Text = $"Failed To Enable";
                    }
                }
            }
            else
            {

            }
        }

        private async void cameraup_CheckedChanged(object sender, EventArgs e)
        {
            if (cameraup.Checked)
            {
                if (addresses.Count > 0)
                {
                    foreach (var address in addresses)
                    {
                        bruuuh.WriteMemory(address.ToString("X"), "bytes", cameraleftreplace);
                    }

                    STA.Text = $"Camera Right Enabled";
                    SoundManager.PlaySound(Properties.Resources.welcome);
                }
                else
                {
                    STA.Text = $"No addresses to replace. Please scan first.";
                }
            }
            else
            {
                if (addresses.Count > 0)
                {
                    foreach (var address in addresses)
                    {
                        bruuuh.WriteMemory(address.ToString("X"), "bytes", cameraleftsearch);
                    }

                    STA.Text = $"Camera Right Disabled";
                    SoundManager.PlaySound(Properties.Resources.welcome);
                }
                else
                {
                    STA.Text = $"No addresses to replace. Please scan first.";
                }
            }
        }

        private void guna2Panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click_2(object sender, EventArgs e)
        {
            StartKeyCaptureButton8();
        }

        private void camon_Click(object sender, EventArgs e)
        {
            StartKeyCaptureButton7();
        }

        private async  void guna2Button1_Click_3(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                STA.Text = $"{open}";
                return;
            }

            bruuuh.OpenProcess("HD-Player");
            STA.Text = $"Scanning for Camera...";
            addresses.Clear();

            IEnumerable<long> wl = await bruuuh.AoBScan(cameraleftsearch, writable: true);
            addresses.AddRange(wl);

            if (addresses.Count > 0)
            {
                STA.Text = $"Scan completed ! {addresses.Count}";
                //Console.Beep(500, 300);
            }
            else
            {
                STA.Text = $"Failed to find addresses for Camera.";
            }
        }

        private void guna2ToggleSwitch4_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
