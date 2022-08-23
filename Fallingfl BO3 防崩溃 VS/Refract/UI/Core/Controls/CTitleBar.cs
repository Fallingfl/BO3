using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Refract.UI.Core.Interfaces;
using Refract.UI.Core.Singletons;

namespace Refract.UI.Core.Controls
{
	public class CTitleBar : UserControl, IThemeableControl
	{
		public const int WM_NCLBUTTONDOWN = 161;

		public const int HT_CAPTION = 2;

		private IContainer components;

		private Button ExitButton;

		internal Label TitleLabel;

		public CTitleBar()
		{
			InitializeComponent();
			base.MouseDown += MouseDown_Drag;
			TitleLabel.MouseDown += MouseDown_Drag;
			UIThemeManager.RegisterCustomThemeHandler(typeof(CTitleBar), ApplyThemeCustom_Implementation);
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			base.ParentForm?.Close();
		}

		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		private void MouseDown_Drag(object sender, MouseEventArgs e)
		{
			if (base.ParentForm != null && e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(base.ParentForm.Handle, 161, 2, 0);
			}
		}

		private void ApplyThemeCustom_Implementation(UIThemeInfo themeData)
		{
			ExitButton.BackColor = themeData.LightBackColor;
		}

		public IEnumerable<Control> GetThemedControls()
		{
			yield return ExitButton;
			yield return TitleLabel;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.TitleLabel = new System.Windows.Forms.Label();
            this.ExitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.TitleLabel.Location = new System.Drawing.Point(4, 4);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(39, 21);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Title";
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExitButton.FlatAppearance.BorderSize = 0;
            this.ExitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.ExitButton.Location = new System.Drawing.Point(461, 0);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(0);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(32, 52);
            this.ExitButton.TabIndex = 1;
            this.ExitButton.Text = "x";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // CTitleBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.TitleLabel);
            this.Name = "CTitleBar";
            this.Size = new System.Drawing.Size(493, 52);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
