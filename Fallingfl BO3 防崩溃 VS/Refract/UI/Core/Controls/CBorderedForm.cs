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
	[Designer(typeof(CBorderedFormDesigner))]
	public class CBorderedForm : UserControl, IThemeableControl
	{
		private bool __useTitleBar = true;

		public const int WM_NCLBUTTONDOWN = 161;

		public const int HT_CAPTION = 2;

		private IContainer components;

		private Panel MainPanel;

		private CTitleBar TitleBar;

		private Panel DesignerContents;

		[Category("Title Bar")]
		[Description("Determines if a title bar should be rendered.")]
		[Browsable(true)]
		public bool UseTitleBar
		{
			get
			{
				return __useTitleBar;
			}
			set
			{
				__useTitleBar = value;
				TitleBar.Visible = value;
				Invalidate();
			}
		}

		[Category("Title Bar")]
		[Description("Determines the title bar's text")]
		[Browsable(true)]
		public string TitleBarTitle
		{
			get
			{
				return TitleBar.TitleLabel.Text;
			}
			set
			{
				TitleBar.TitleLabel.Text = value;
				Invalidate();
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Panel ControlContents => DesignerContents;

		public CBorderedForm()
		{
			InitializeComponent();
			base.MouseDown += MouseDown_Drag;
			MainPanel.MouseDown += MouseDown_Drag;
			UIThemeManager.RegisterCustomThemeHandler(typeof(CBorderedForm), ApplyThemeCustom_Implementation);
			TypeDescriptor.AddAttributes(DesignerContents, new DesignerAttribute(typeof(CBFInnerPanelDesigner)));
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
			BackColor = themeData.AccentColor;
			MainPanel.BackColor = themeData.BackColor;
		}

		public IEnumerable<Control> GetThemedControls()
		{
			yield return TitleBar;
			yield return MainPanel;
			yield return DesignerContents;
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.DesignerContents = new System.Windows.Forms.Panel();
            this.TitleBar = new Refract.UI.Core.Controls.CTitleBar();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.MainPanel.Controls.Add(this.DesignerContents);
            this.MainPanel.Controls.Add(this.TitleBar);
            this.MainPanel.Location = new System.Drawing.Point(2, 2);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(832, 481);
            this.MainPanel.TabIndex = 0;
            // 
            // DesignerContents
            // 
            this.DesignerContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DesignerContents.Location = new System.Drawing.Point(0, 30);
            this.DesignerContents.Name = "DesignerContents";
            this.DesignerContents.Size = new System.Drawing.Size(832, 451);
            this.DesignerContents.TabIndex = 1;
            // 
            // TitleBar
            // 
            this.TitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.TitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleBar.Location = new System.Drawing.Point(0, 0);
            this.TitleBar.Name = "TitleBar";
            this.TitleBar.Size = new System.Drawing.Size(832, 30);
            this.TitleBar.TabIndex = 0;
            // 
            // CBorderedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.Controls.Add(this.MainPanel);
            this.Name = "CBorderedForm";
            this.Size = new System.Drawing.Size(836, 485);
            this.MainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

		}
	}
}
