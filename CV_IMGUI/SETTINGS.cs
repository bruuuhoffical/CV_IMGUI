using DiscordRPC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CV_IMGUI
{
    public partial class SETTINGS : Form
    {
        private ParticleSystem particleSystem;
        public SETTINGS()
        {
            InitializeComponent();
            particleSystem = new ParticleSystem();
            LoadCustomFont();
        }   
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            particleSystem.DrawParticles(e.Graphics);
        }
        private PrivateFontCollection privateFontCollection = new PrivateFontCollection();
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
        private void SETTINGS_Load(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void guest_CheckedChanged(object sender, EventArgs e)
        {
            if (sound.Checked)
            {
                SoundManager.IsSoundEnabled = sound.Checked;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            particleSystem.UpdateParticles();
            Invalidate();
        }
        //public bool IsDRPCEnabled
        //{
        //    get { return drpc.Checked; }
        //}
        //public bool IsDRPCEnabled()
        //{
        //    return drpc.Checked;
        //}
        public bool IsDRPCEnabled()
        {
            return drpc.Checked;
        }
        public void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (drpc.Checked)
            {
                RPC.rpctimestamp = Timestamps.Now;
                RPC.InitializeRPC();
            }
            else
            {
                RPC.StopRPC();
            }
        }
    }
}
