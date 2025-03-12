using Guna.UI2.WinForms;
using KeyAuth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CV_IMGUI
{
    public partial class Form1 : Form
    {
        private ParticleSystem particleSystem;
        public Form1()
        {
            InitializeComponent();
            KeyAuthApp.init();
            LoadCustomFont();
            particleSystem = new ParticleSystem();

        }
        public static api KeyAuthApp = new api(
        name: "CV_PREMIUM",
        ownerid: "JOzxAPywrc",
        version: "3.9"
        );
        private void Form1_Load(object sender, EventArgs e)
        {
            USER.PlaceholderForeColor = panelcolor;
            PASS.PlaceholderForeColor = panelcolor;
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

            Font customFont = new Font(customFontCollection.Families[0], 19f);

            label1.Font = customFont;
        }
        private void SaveTextBoxContent()
        {
            try
            {
                Properties.Settings.Default.Username = USER.Text;
                Properties.Settings.Default.Password = PASS.Text;
                Properties.Settings.Default.Save();

                MessageBox.Show("Content saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving content: {ex.Message}");
            }
        }
        private void LoadTextBoxContent()
        {
            try
            {
                USER.Text = Properties.Settings.Default.Username;
                PASS.Text = Properties.Settings.Default.Password;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading content: {ex.Message}");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2Button1.BorderColor = panelcolor;
            KeyAuthApp.login(USER.Text, PASS.Text);
            if (KeyAuthApp.response.success)
            {
                HOME BRUUUH = new HOME();
                BRUUUH.Show();
                this.Hide();
            }
            else
            {
                label.Text = "Status:" + KeyAuthApp.response.message;
            }
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if (guna2CheckBox1.Checked) 
            //{
            //    SaveTextBoxContent();
            //}
        }

        private void USER_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void PASS_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            particleSystem.UpdateParticles();
            Invalidate();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label_Click(object sender, EventArgs e)
        {

        }
    }
}
