using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using KeyAuth;

namespace CV_IMGUI
{
    public partial class PROFILE : Form
    {
        private ParticleSystem particleSystem;
        public PROFILE()
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
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            particleSystem.UpdateParticles();
            Invalidate();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/hDTF97EauR");
        }
        #region PCINFO
        private string GetHWID()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor"))
            {
                foreach (var item in searcher.Get())
                {
                    return item["ProcessorId"].ToString();
                }
            }
            return "Unknown HWID";
        }
        private async void GetIPAddress()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string publicIp = await client.GetStringAsync("https://api.ipify.org");
                    ip.Text = publicIp;
                }
                catch
                {
                    ip.Text = "Unable to fetch IP";
                }
            }
        }
        public DateTime UnixTimeToDateTime(long unixtime)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            try
            {
                dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
            }
            catch
            {
                dtDateTime = DateTime.MaxValue;
            }
            return dtDateTime;
        }
        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public string expirydaysleft()
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(long.Parse(Form1.KeyAuthApp.user_data.subscriptions[0].expiry)).ToLocalTime();
            TimeSpan difference = dtDateTime - DateTime.Now;
            return Convert.ToString(difference.Days + " Days " + difference.Hours + " Hours Left");
        }
        #endregion;
        private void PROFILE_Load(object sender, EventArgs e)
        {
            pcname.Text = Environment.MachineName;
            hwid.Text = GetHWID();
            GetIPAddress();
            var keyAuthApp = Form1.KeyAuthApp;

            if (keyAuthApp.response.success)
            {
                //.Text = keyAuthApp.user_data.sub;exp
                label3.Text = expirydaysleft();
            }
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://bruuhcheat.netlify.app/");
        }
    }
}
