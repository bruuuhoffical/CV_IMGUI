namespace CV_IMGUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.USER = new Guna.UI2.WinForms.Guna2TextBox();
            this.PASS = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DRAG1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.DRAG2 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.DRAG3 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label = new System.Windows.Forms.Label();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // USER
            // 
            this.USER.Animated = true;
            this.USER.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.USER.BorderRadius = 5;
            this.USER.BorderThickness = 2;
            this.USER.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.USER.DefaultText = "";
            this.USER.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.USER.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.USER.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.USER.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.USER.FillColor = System.Drawing.Color.Transparent;
            this.USER.FocusedState.BorderColor = System.Drawing.Color.White;
            this.USER.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.USER.ForeColor = System.Drawing.Color.White;
            this.USER.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.USER.Location = new System.Drawing.Point(24, 99);
            this.USER.Name = "USER";
            this.USER.PasswordChar = '\0';
            this.USER.PlaceholderForeColor = System.Drawing.Color.White;
            this.USER.PlaceholderText = "Username";
            this.USER.SelectedText = "";
            this.USER.ShadowDecoration.Color = System.Drawing.Color.DimGray;
            this.USER.Size = new System.Drawing.Size(270, 38);
            this.USER.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.USER.TabIndex = 0;
            this.USER.TextChanged += new System.EventHandler(this.USER_TextChanged);
            // 
            // PASS
            // 
            this.PASS.Animated = true;
            this.PASS.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.PASS.BorderRadius = 5;
            this.PASS.BorderThickness = 2;
            this.PASS.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PASS.DefaultText = "";
            this.PASS.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.PASS.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PASS.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PASS.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PASS.FillColor = System.Drawing.Color.Transparent;
            this.PASS.FocusedState.BorderColor = System.Drawing.Color.White;
            this.PASS.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.PASS.ForeColor = System.Drawing.Color.White;
            this.PASS.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.PASS.Location = new System.Drawing.Point(24, 163);
            this.PASS.Name = "PASS";
            this.PASS.PasswordChar = '\0';
            this.PASS.PlaceholderForeColor = System.Drawing.Color.White;
            this.PASS.PlaceholderText = "Password";
            this.PASS.SelectedText = "";
            this.PASS.Size = new System.Drawing.Size(270, 38);
            this.PASS.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.PASS.TabIndex = 1;
            this.PASS.TextChanged += new System.EventHandler(this.PASS_TextChanged);
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.guna2Button1.BorderRadius = 4;
            this.guna2Button1.BorderThickness = 1;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(56, 235);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(206, 34);
            this.guna2Button1.TabIndex = 2;
            this.guna2Button1.Text = "Proceed";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.guna2ControlBox1);
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.guna2Panel1.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.guna2Panel1.Location = new System.Drawing.Point(0, 2);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(318, 40);
            this.guna2Panel1.TabIndex = 10;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.CustomClick = true;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(289, 8);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.PressedDepth = 60;
            this.guna2ControlBox1.Size = new System.Drawing.Size(20, 21);
            this.guna2ControlBox1.TabIndex = 2;
            this.guna2ControlBox1.Click += new System.EventHandler(this.guna2ControlBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(64, -3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "BRUUUH CHEATS";
            // 
            // DRAG1
            // 
            this.DRAG1.DockIndicatorTransparencyValue = 0.6D;
            this.DRAG1.TargetControl = this;
            this.DRAG1.UseTransparentDrag = true;
            // 
            // DRAG2
            // 
            this.DRAG2.DockIndicatorTransparencyValue = 0.6D;
            this.DRAG2.TargetControl = this.guna2Panel1;
            this.DRAG2.UseTransparentDrag = true;
            // 
            // DRAG3
            // 
            this.DRAG3.DockIndicatorTransparencyValue = 0.6D;
            this.DRAG3.TargetControl = this.label1;
            this.DRAG3.UseTransparentDrag = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label.Location = new System.Drawing.Point(8, 323);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(19, 16);
            this.label.TabIndex = 11;
            this.label.Text = "...";
            this.label.Click += new System.EventHandler(this.label_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(318, 348);
            this.Controls.Add(this.label);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.PASS);
            this.Controls.Add(this.USER);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "LOGIN";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox USER;
        private Guna.UI2.WinForms.Guna2TextBox PASS;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2DragControl DRAG1;
        private Guna.UI2.WinForms.Guna2DragControl DRAG2;
        private Guna.UI2.WinForms.Guna2DragControl DRAG3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label;
    }
}

