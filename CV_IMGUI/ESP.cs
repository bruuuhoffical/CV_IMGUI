using Guna.UI2.WinForms;
using ImGuiNET;
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
using static Guna.UI2.Native.WinApi;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CV_IMGUI
{
    public partial class ESP : Form
    {
        private ParticleSystem particleSystem;
        string on = " Enabled !";
        string off = " Disabled !";
        string wait = "Enabling";
        string wait1 = "Enabling....";
        string open = "Open Emulator";
        string nope = "Unable Able To Enable";
        private const int WM_HOTKEY = 0x0312;
        private int _nextHotkeyId = 1;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private readonly Dictionary<int, HotkeyInfo> _hotkeys = new Dictionary<int, HotkeyInfo>();
        public ESP()
        {
            InitializeComponent();
            LoadCustomFont();
            particleSystem = new ParticleSystem();
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
            transkey.Text = "?";
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDownButton1;
        }

        private void StartKeyCaptureButton2()
        {
            redlinekey.Text = "?";
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDownButton2;
        }
        private void StartKeyCaptureButton3()
        {
            chamsmocokey.Text = "?";
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDownButton3;
        }
        private void StartKeyCaptureButton4()
        {
            rgbkey.Text = "?";
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDownButton4;
        }
        private void Form1_KeyDownButton1(object sender, KeyEventArgs e)
        {
            RegisterHotKeyForButton(transkey, e.KeyCode, e.Control, e.Alt, e.Shift);
            this.KeyDown -= Form1_KeyDownButton1;
        }

        private void Form1_KeyDownButton2(object sender, KeyEventArgs e)
        {
            RegisterHotKeyForButton(redlinekey, e.KeyCode, e.Control, e.Alt, e.Shift);
            this.KeyDown -= Form1_KeyDownButton2;
        }
        private void Form1_KeyDownButton3(object sender, KeyEventArgs e)
        {
            RegisterHotKeyForButton(chamsmocokey, e.KeyCode, e.Control, e.Alt, e.Shift);
            this.KeyDown -= Form1_KeyDownButton3;
        }
        private void Form1_KeyDownButton4(object sender, KeyEventArgs e)
        {
            RegisterHotKeyForButton(rgbkey, e.KeyCode, e.Control, e.Alt, e.Shift);
            this.KeyDown -= Form1_KeyDownButton4;
        }
        private void PerformActionForButton(Guna2Button button)
        {
            if (button == transkey)
            {
                if (tran.Checked == true)
                {
                    tran.Checked = false;
                }
                else
                {
                    tran.Checked = true;
                }
            }
            else if (button == redlinekey)
            {
                if (redline.Checked == true)
                {
                    redline.Checked = false;
                }
                else
                {
                    redline.Checked = true;
                }
            }
            else if (button == chamsmocokey)
            {
                if (moco.Checked == true)
                {
                    moco.Checked = false;
                }
                else
                {
                    moco.Checked = true;
                }
            }
            else if (button == rgbkey)
            {
                if (rgbbox.Checked == true)
                {
                    rgbbox.Checked = false;
                }
                else
                {
                    rgbbox.Checked = true;
                }
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
        #region DLL INJECTION SYSTEM
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr LoadLibraryA(string lpLibFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeLibrary(IntPtr hModule);

        const uint PROCESS_CREATE_THREAD = 0x2;
        const uint PROCESS_QUERY_INFORMATION = 0x400;
        const uint PROCESS_VM_OPERATION = 0x8;
        const uint PROCESS_VM_WRITE = 0x20;
        const uint PROCESS_VM_READ = 0x10;
        const uint MEM_COMMIT = 0x1000;
        const uint PAGE_READWRITE = 4;

        private static void cvdll(string resourceName, string outputPath)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            using (Stream resourceStream = executingAssembly.GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null)
                {
                    throw new ArgumentException($"Resource '{resourceName}' not found.");
                }
                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create))
                {
                    byte[] buffer = new byte[resourceStream.Length];
                    resourceStream.Read(buffer, 0, buffer.Length);
                    fileStream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        #endregion
        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {
            guna2Panel4.BackColor = panelcolor;
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {
            guna2Panel2.BackColor = panelcolor;
        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {
            guna2Panel5.BackColor = panelcolor;
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {
            guna2Panel3.BackColor = panelcolor;
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ESP_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            guna2Panel1.BackColor = panelcolor;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            particleSystem.UpdateParticles();
            Invalidate();
        }

        private void transkey_Click(object sender, EventArgs e)
        {
            StartKeyCaptureButton1();
        }

        private void redlinekey_Click(object sender, EventArgs e)
        {
            StartKeyCaptureButton2();
        }

        private void chamsmocokey_Click(object sender, EventArgs e)
        {
            StartKeyCaptureButton3();
        }


        private void rgbkey_Click(object sender, EventArgs e)
        {
            StartKeyCaptureButton4();
        }

        private void tran_CheckedChanged(object sender, EventArgs e)
        {
            
            string processName = "HD-Player";
            string dllResourceName = "CV_IMGUI.Properties.transparent.dll";
            string tempDllPath = Path.Combine(Path.GetTempPath(), "transparent.dll");

            
            cvdll(dllResourceName, tempDllPath);

            Process[] targetProcesses = Process.GetProcessesByName(processName);
            if (targetProcesses.Length == 0)
            {
                //STATUS.Text = "Process not found";
                return;
            }

            Process targetProcess = targetProcesses[0];
            IntPtr hProcess = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, targetProcess.Id);
            if (hProcess == IntPtr.Zero)
            {
                //STATUS.Text = "Failed to open process";
                return;
            }
            if (tran.Checked)
            {
                IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                IntPtr allocMemAddress = VirtualAllocEx(hProcess, IntPtr.Zero, (IntPtr)tempDllPath.Length, MEM_COMMIT, PAGE_READWRITE);
                IntPtr bytesWritten;
                WriteProcessMemory(hProcess, allocMemAddress, System.Text.Encoding.ASCII.GetBytes(tempDllPath), (uint)tempDllPath.Length, out bytesWritten);
                IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, IntPtr.Zero, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);

                if (hThread == IntPtr.Zero)
                {
                    //STATUS.Text = "Failed to inject the DLL";
                }
                else
                {
                    //STATUS.Text = "DLL Injected successfully";
                }
            }
            else
            {
                //IntPtr hModule = GetModuleHandle("transparent.dll");
                //if (hModule == IntPtr.Zero)
                //{
                //    //STATUS.Text = "DLL not loaded in the process.";
                //    return;
                //}

                //// Get the address of FreeLibrary from kernel32
                //IntPtr freeLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "FreeLibrary");
                //if (freeLibraryAddr == IntPtr.Zero)
                //{
                //    //STATUS.Text = "Failed to get FreeLibrary address.";
                //    return;
                //}

                //// Call FreeLibrary on the DLL in the target process
                //IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, IntPtr.Zero, freeLibraryAddr, hModule, 0, IntPtr.Zero);

                //if (hThread == IntPtr.Zero)
                //{
                //    //STATUS.Text = "Failed to unload the DLL";
                //}
                //else
                //{
                //    //STATUS.Text = "DLL unloaded successfully";
                //}
                //CloseHandle(hProcess);
            }
        }

        private void redline_CheckedChanged(object sender, EventArgs e)
        {
            string processName = "HD-Player";
            string dllResourceName = "CV_IMGUI.Properties.redant.dll";
            string tempDllPath = Path.Combine(Path.GetTempPath(), "redant.dll");


            cvdll(dllResourceName, tempDllPath);

            Process[] targetProcesses = Process.GetProcessesByName(processName);
            if (targetProcesses.Length == 0)
            {
                //STATUS.Text = "Process not found";
                return;
            }

            Process targetProcess = targetProcesses[0];
            IntPtr hProcess = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, targetProcess.Id);
            if (hProcess == IntPtr.Zero)
            {
                //STATUS.Text = "Failed to open process";
                return;
            }
            if (redline.Checked)
            {
                IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                IntPtr allocMemAddress = VirtualAllocEx(hProcess, IntPtr.Zero, (IntPtr)tempDllPath.Length, MEM_COMMIT, PAGE_READWRITE);
                IntPtr bytesWritten;
                WriteProcessMemory(hProcess, allocMemAddress, System.Text.Encoding.ASCII.GetBytes(tempDllPath), (uint)tempDllPath.Length, out bytesWritten);
                IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, IntPtr.Zero, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);

                if (hThread == IntPtr.Zero)
                {
                    //STATUS.Text = "Failed to inject the DLL";
                }
                else
                {
                    //STATUS.Text = "DLL Injected successfully";
                }
            }
            else
            {
                //IntPtr hModule = GetModuleHandle("redant.dll");
                //if (hModule == IntPtr.Zero)
                //{
                //    //STATUS.Text = "DLL not loaded in the process.";
                //    return;
                //}

                //// Get the address of FreeLibrary from kernel32
                //IntPtr freeLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "FreeLibrary");
                //if (freeLibraryAddr == IntPtr.Zero)
                //{
                //    //STATUS.Text = "Failed to get FreeLibrary address.";
                //    return;
                //}

                //// Call FreeLibrary on the DLL in the target process
                //IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, IntPtr.Zero, freeLibraryAddr, hModule, 0, IntPtr.Zero);

                //if (hThread == IntPtr.Zero)
                //{
                //    //STATUS.Text = "Failed to unload the DLL";
                //}
                //else
                //{
                //    //STATUS.Text = "DLL unloaded successfully";
                //}
            }
            CloseHandle(hProcess);
        }

        private void moco_CheckedChanged(object sender, EventArgs e)
        {
            string processName = "HD-Player";
            string dllResourceName = "CV_IMGUI.Properties.moco.ll.dll";
            string tempDllPath = Path.Combine(Path.GetTempPath(), "moco.ll.dll");


            cvdll(dllResourceName, tempDllPath);

            Process[] targetProcesses = Process.GetProcessesByName(processName);
            if (targetProcesses.Length == 0)
            {
                //STATUS.Text = "Process not found";
                return;
            }

            Process targetProcess = targetProcesses[0];
            IntPtr hProcess = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, targetProcess.Id);
            if (hProcess == IntPtr.Zero)
            {
                //STATUS.Text = "Failed to open process";
                return;
            }
            if (moco.Checked)
            {
                IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                IntPtr allocMemAddress = VirtualAllocEx(hProcess, IntPtr.Zero, (IntPtr)tempDllPath.Length, MEM_COMMIT, PAGE_READWRITE);
                IntPtr bytesWritten;
                WriteProcessMemory(hProcess, allocMemAddress, System.Text.Encoding.ASCII.GetBytes(tempDllPath), (uint)tempDllPath.Length, out bytesWritten);
                IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, IntPtr.Zero, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);

                if (hThread == IntPtr.Zero)
                {
                    //STATUS.Text = "Failed to inject the DLL";
                }
                else
                {
                    //STATUS.Text = "DLL Injected successfully";
                }
            }
            else
            {
                //IntPtr hModule = GetModuleHandle("moco.ll.dll");
                //if (hModule == IntPtr.Zero)
                //{
                //    //STATUS.Text = "DLL not loaded in the process.";
                //    return;
                //}

                //// Get the address of FreeLibrary from kernel32
                //IntPtr freeLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "FreeLibrary");
                //if (freeLibraryAddr == IntPtr.Zero)
                //{
                //    //STATUS.Text = "Failed to get FreeLibrary address.";
                //    return;
                //}

                //// Call FreeLibrary on the DLL in the target process
                //IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, IntPtr.Zero, freeLibraryAddr, hModule, 0, IntPtr.Zero);

                //if (hThread == IntPtr.Zero)
                //{
                //    //STATUS.Text = "Failed to unload the DLL";
                //}
                //else
                //{
                //    //STATUS.Text = "DLL unloaded successfully";
                //}
            }
            CloseHandle(hProcess);
        }

        private void rgbbox_CheckedChanged(object sender, EventArgs e)
        {
            string processName = "HD-Player";
            string dllResourceName = "CV_IMGUI.Properties.boxrgb.dll";
            string tempDllPath = Path.Combine(Path.GetTempPath(), "boxrgb.dll");


            cvdll(dllResourceName, tempDllPath);

            Process[] targetProcesses = Process.GetProcessesByName(processName);
            if (targetProcesses.Length == 0)
            {
                //STATUS.Text = "Process not found";
                return;
            }

            Process targetProcess = targetProcesses[0];
            IntPtr hProcess = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, targetProcess.Id);
            if (hProcess == IntPtr.Zero)
            {
                //STATUS.Text = "Failed to open process";
                return;
            }
            if (rgbbox.Checked)
            {
                IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                IntPtr allocMemAddress = VirtualAllocEx(hProcess, IntPtr.Zero, (IntPtr)tempDllPath.Length, MEM_COMMIT, PAGE_READWRITE);
                IntPtr bytesWritten;
                WriteProcessMemory(hProcess, allocMemAddress, System.Text.Encoding.ASCII.GetBytes(tempDllPath), (uint)tempDllPath.Length, out bytesWritten);
                IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, IntPtr.Zero, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);

                if (hThread == IntPtr.Zero)
                {
                    //STATUS.Text = "Failed to inject the DLL";
                }
                else
                {
                    //STATUS.Text = "DLL Injected successfully";
                }
            }
            else
            {
                //IntPtr hModule = GetModuleHandle("boxrgb.dll");
                //if (hModule == IntPtr.Zero)
                //{
                //    //STATUS.Text = "DLL not loaded in the process.";
                //    return;
                //}

                //// Get the address of FreeLibrary from kernel32
                //IntPtr freeLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "FreeLibrary");
                //if (freeLibraryAddr == IntPtr.Zero)
                //{
                //    //STATUS.Text = "Failed to get FreeLibrary address.";
                //    return;
                //}

                //// Call FreeLibrary on the DLL in the target process
                //IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, IntPtr.Zero, freeLibraryAddr, hModule, 0, IntPtr.Zero);

                //if (hThread == IntPtr.Zero)
                //{
                //    //STATUS.Text = "Failed to unload the DLL";
                //}
                //else
                //{
                //    //STATUS.Text = "DLL unloaded successfully";
                //}
            }
            CloseHandle(hProcess);
        }

        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            string processName = "HD-Player";
            string dllResourceName1 = "CV_IMGUI.Properties.init.dll";
            string dllResourceName2 = "CV_IMGUI.Properties.menu.dll";
            string tempDllPath1 = Path.Combine(Path.GetTempPath(), "init.dll");
            string tempDllPath2 = Path.Combine(Path.GetTempPath(), "menu.dll");

            cvdll(dllResourceName1, tempDllPath1);
            cvdll(dllResourceName2, tempDllPath2);

            Process[] targetProcesses = Process.GetProcessesByName(processName);
            if (targetProcesses.Length == 0)
            {
                STA.Text = "Process not found";
                return;
            }

            Process targetProcess = targetProcesses[0];
            IntPtr processHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, targetProcess.Id);
            if (processHandle == IntPtr.Zero)
            {
                STA.Text = "Failed to open process";
                return;
            }

            if (guna2ToggleSwitch1.Checked)
            {
                InjectDLL(processHandle, tempDllPath1);

                InjectDLL(processHandle, tempDllPath2);
            }

            CloseHandle(processHandle);

            void InjectDLL(IntPtr targetProcessHandle, string dllPath)
            {
                IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                IntPtr allocMemAddress = VirtualAllocEx(targetProcessHandle, IntPtr.Zero, (IntPtr)dllPath.Length, MEM_COMMIT, PAGE_READWRITE);
                IntPtr bytesWritten;
                WriteProcessMemory(targetProcessHandle, allocMemAddress, System.Text.Encoding.ASCII.GetBytes(dllPath), (uint)dllPath.Length, out bytesWritten);
                IntPtr hThread = CreateRemoteThread(targetProcessHandle, IntPtr.Zero, IntPtr.Zero, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);

                if (hThread == IntPtr.Zero)
                {
                    STA.Text = $"Failed to inject the DLL";
                }
                else
                {
                    STA.Text = $"DLL Injected successfully";
                }
            }
        }

        private void guna2Panel6_Paint(object sender, PaintEventArgs e)
        {
            string processName = "HD-Player";
            string dllResourceName = "CV_IMGUI.Properties.redant.dll";
            string tempDllPath = Path.Combine(Path.GetTempPath(), "redant.dll");


            cvdll(dllResourceName, tempDllPath);

            Process[] targetProcesses = Process.GetProcessesByName(processName);
            if (targetProcesses.Length == 0)
            {
                STA.Text = "Process not found";
                return;
            }

            Process targetProcess = targetProcesses[0];
            IntPtr hProcess = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, targetProcess.Id);
            if (hProcess == IntPtr.Zero)
            {
                STA.Text = "Failed to open process";
                return;
            }
            if (redline.Checked)
            {
                IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                IntPtr allocMemAddress = VirtualAllocEx(hProcess, IntPtr.Zero, (IntPtr)tempDllPath.Length, MEM_COMMIT, PAGE_READWRITE);
                IntPtr bytesWritten;
                WriteProcessMemory(hProcess, allocMemAddress, System.Text.Encoding.ASCII.GetBytes(tempDllPath), (uint)tempDllPath.Length, out bytesWritten);
                IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, IntPtr.Zero, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);

                if (hThread == IntPtr.Zero)
                {
                    STA.Text = "Failed to inject the DLL";
                }
                else
                {
                    STA.Text = "Injected successfully";
                }
            }
            #region null
            else
            {
                //IntPtr hModule = GetModuleHandle("redant.dll");
                //if (hModule == IntPtr.Zero)
                //{
                //    //STATUS.Text = "DLL not loaded in the process.";
                //    return;
                //}

                //// Get the address of FreeLibrary from kernel32
                //IntPtr freeLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "FreeLibrary");
                //if (freeLibraryAddr == IntPtr.Zero)
                //{
                //    //STATUS.Text = "Failed to get FreeLibrary address.";
                //    return;
                //}

                //// Call FreeLibrary on the DLL in the target process
                //IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, IntPtr.Zero, freeLibraryAddr, hModule, 0, IntPtr.Zero);

                //if (hThread == IntPtr.Zero)
                //{
                //    //STATUS.Text = "Failed to unload the DLL";
                //}
                //else
                //{
                //    //STATUS.Text = "DLL unloaded successfully";
                //}
            }
            CloseHandle(hProcess);
            #endregion
        }

        private void guna2ToggleSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            string processName = "HD-Player";
            string dllResourceName1 = "CV_IMGUI.Properties.glew32.dll";
            string dllResourceName2 = "CV_IMGUI.Properties.glew64.dll";
            string tempDllPath1 = Path.Combine(Path.GetTempPath(), "glew32.dll");
            string tempDllPath2 = Path.Combine(Path.GetTempPath(), "glew64.dll");

            cvdll(dllResourceName1, tempDllPath1);
            cvdll(dllResourceName2, tempDllPath2);

            Process[] targetProcesses = Process.GetProcessesByName(processName);
            if (targetProcesses.Length == 0)
            {
                STA.Text = "Process not found";
                return;
            }

            Process targetProcess = targetProcesses[0];
            IntPtr processHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, targetProcess.Id);
            if (processHandle == IntPtr.Zero)
            {
                STA.Text = "Failed to open process";
                return;
            }

            if (guna2ToggleSwitch2.Checked)
            {
                InjectDLL(processHandle, tempDllPath1);

                InjectDLL(processHandle, tempDllPath2);
            }

            CloseHandle(processHandle);

            void InjectDLL(IntPtr targetProcessHandle, string dllPath)
            {
                IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                IntPtr allocMemAddress = VirtualAllocEx(targetProcessHandle, IntPtr.Zero, (IntPtr)dllPath.Length, MEM_COMMIT, PAGE_READWRITE);
                IntPtr bytesWritten;
                WriteProcessMemory(targetProcessHandle, allocMemAddress, System.Text.Encoding.ASCII.GetBytes(dllPath), (uint)dllPath.Length, out bytesWritten);
                IntPtr hThread = CreateRemoteThread(targetProcessHandle, IntPtr.Zero, IntPtr.Zero, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);

                if (hThread == IntPtr.Zero)
                {
                    STA.Text = $"Failed to inject the DLL";
                }
                else
                {
                    STA.Text = $"DLL Injected successfully";
                }
            }

        }











    }
}
