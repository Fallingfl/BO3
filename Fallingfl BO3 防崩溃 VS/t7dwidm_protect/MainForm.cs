using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.ExThreads;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Refract.UI.Core.Controls;
using Refract.UI.Core.Interfaces;
using Refract.UI.Core.Singletons;
using t7dwidm_protect.Cheats;

namespace t7dwidm_protect
{
	public class MainForm : Form, IThemeableControl
	{
		private bool SpendingVials;

		private bool SpendingKeys;

		private Action UpdateProtection;

		private List<int> pidsOwned = new List<int>();

		private IContainer components;

		private CBorderedForm InnerForm;

		private Label StatusLabel;

		private Panel panel1;

		private Label label1;

		private Label label3;

		private Label label2;

		private Label label4;

		public MainForm()
		{
			NativeStealth.SetStealthMode(NativeStealthType.ManualInvoke);
			InitializeComponent();
			UIThemeManager.OnThemeChanged(this, OnThemeChanged_Implementation);
			this.SetThemeAware();
			Text = "Fallingfl BO3 TOOL";
			base.MaximizeBox = true;
			base.MinimizeBox = true;
			UpdateProtection = __updateprotection;
			new Task(delegate
			{
				PollProcesses();
			}).Start();
		}

		private void PollProcesses()
		{
			while (true)
			{
				UpdateProtection?.Invoke();
				Thread.Sleep(5000);
			}
		}

		private void __updateprotection()
		{
			try
			{
				if (BlackOps3.Game == null)
				{
					return;
				}
				BlackOps3.Game.BaseProcess.Refresh();
				if (BlackOps3.Game.BaseProcess.HasExited)
				{
					throw new Exception();
				}
				if (!pidsOwned.Contains(BlackOps3.Game.BaseProcess.Id) && BlackOps3.Game.GetValue<long>(BlackOps3.Game[378468552uL]) != 0L)
				{
					BlackOps3.SetDvar("maxvoicepacketsperframe", "0");
					string text = Path.Combine(Environment.CurrentDirectory, "t7dwidm_patch.dll");
					PointerEx pointerEx = BlackOps3.Game.QuickAlloc(text.Length + 1);
					BlackOps3.Game.SetString(pointerEx, text);
					long num = (PointerEx)Injector.GetProcAddress(Injector.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
					BlackOps3.Game.Call<long>(num, new object[1] { pointerEx });
					Thread.Sleep(100);
					ProcessThread earliestActiveThread = BlackOps3.Game.GetEarliestActiveThread();
					PointerEx pointerEx2 = NativeStealth.OpenThread(26, bInheritHandle: false, earliestActiveThread.Id);
					NativeStealth.SuspendThread(pointerEx2);
					ThreadContext64Ex threadContext64Ex = new ThreadContext64Ex(ThreadContextExFlags.All);
					if (!threadContext64Ex.GetContext(pointerEx2))
					{
						NativeStealth.ResumeThread(pointerEx2);
						return;
					}
					threadContext64Ex.SetDebug(BlackOps3.Game[21210630uL], BlackOps3.Game[20288272uL], 0);
					threadContext64Ex.SetContext(pointerEx2);
					NativeStealth.ResumeThread(pointerEx2);
					ProcessEx.CloseHandle(pointerEx2);
					StatusLabel.Text = "找到游戏进程ID: " + BlackOps3.Game.BaseProcess.Id;
					pidsOwned.Add(BlackOps3.Game.BaseProcess.Id);
					UIThemeManager.ApplyTheme(Orange());
				}
			}
			catch
			{
				StatusLabel.Text = "没有找到游戏进程";
				UIThemeManager.ApplyTheme(UIThemeInfo.Default());
			}
		}

		internal static UIThemeInfo Orange()
		{
			UIThemeInfo result = default(UIThemeInfo);
			result.BackColor = Color.FromArgb(28, 28, 28);
			result.TextColor = Color.WhiteSmoke;
			result.AccentColor = Color.DarkOrange;
			result.TitleBarColor = Color.FromArgb(36, 36, 36);
			result.ButtonFlatStyle = FlatStyle.Flat;
			result.ButtonHoverColor = Color.FromArgb(50, 50, 50);
			result.LightBackColor = Color.FromArgb(36, 36, 36);
			result.ButtonActive = Color.DarkOrange;
			result.TextInactive = Color.Gray;
			return result;
		}

		public IEnumerable<Control> GetThemedControls()
		{
			yield return InnerForm;
			yield return label1;
			yield return label2;
			yield return label3;
			yield return StatusLabel;
		}

		private void OnThemeChanged_Implementation(UIThemeInfo currentTheme)
		{
		}

		private void RPCExample3_Click(object sender, EventArgs e)
		{
		}

		private void ExampleRPC4_Click(object sender, EventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
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
			InnerForm = new Refract.UI.Core.Controls.CBorderedForm();
			label3 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			StatusLabel = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			InnerForm.ControlContents.SuspendLayout();
			SuspendLayout();
			InnerForm.BackColor = System.Drawing.Color.DodgerBlue;
			InnerForm.ControlContents.Controls.Add(label4);
			InnerForm.ControlContents.Controls.Add(label3);
			InnerForm.ControlContents.Controls.Add(label2);
			InnerForm.ControlContents.Controls.Add(label1);
			InnerForm.ControlContents.Controls.Add(panel1);
			InnerForm.ControlContents.Controls.Add(StatusLabel);
			InnerForm.ControlContents.Dock = System.Windows.Forms.DockStyle.Fill;
			InnerForm.ControlContents.Enabled = true;
			InnerForm.ControlContents.Location = new System.Drawing.Point(0, 32);
			InnerForm.ControlContents.Name = "ControlContents";
			InnerForm.ControlContents.Size = new System.Drawing.Size(396, 164);
			InnerForm.ControlContents.TabIndex = 1;
			InnerForm.ControlContents.Visible = true;
			InnerForm.Dock = System.Windows.Forms.DockStyle.Fill;
			InnerForm.Location = new System.Drawing.Point(0, 0);
			InnerForm.Name = "InnerForm";
			InnerForm.Size = new System.Drawing.Size(400, 200);
			InnerForm.TabIndex = 0;
			InnerForm.TitleBarTitle = "Fallingfl BO3 TOOL";
			InnerForm.UseTitleBar = true;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.ForeColor = System.Drawing.SystemColors.Control;
			label3.Location = new System.Drawing.Point(6, 69);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(368, 17);
			label3.TabIndex = 4;
			label3.Text = "使用此工具，您将对自己的帐户承担全部责任";
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.ForeColor = System.Drawing.SystemColors.Control;
			label2.Location = new System.Drawing.Point(6, 38);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(325, 17);
			label2.TabIndex = 3;
			label2.Text = "这是由Fallingfl做出的第三方工具，可能违反了EULA协议";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.ForeColor = System.Drawing.SystemColors.Control;
			label1.Location = new System.Drawing.Point(6, 6);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(315, 17);
			label1.TabIndex = 2;
			label1.Text = "此工具可以防止游戏发生重大错误和崩溃";
			panel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			panel1.Location = new System.Drawing.Point(0, 131);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(400, 1);
			panel1.TabIndex = 1;
			StatusLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			StatusLabel.AutoSize = true;
			StatusLabel.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			StatusLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			StatusLabel.Location = new System.Drawing.Point(5, 136);
			StatusLabel.Name = "StatusLabel";
			StatusLabel.Size = new System.Drawing.Size(179, 21);
			StatusLabel.TabIndex = 0;
			StatusLabel.Text = "没有找到游戏进程";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.ForeColor = System.Drawing.SystemColors.Control;
			label4.Location = new System.Drawing.Point(6, 102);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(244, 17);
			label4.TabIndex = 5;
			label4.Text = "游玩的时候请把工具打开";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(400, 200);
			base.Controls.Add(InnerForm);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "MainForm";
			Text = "Fallingfl BO3 MP Tool";
			InnerForm.ControlContents.ResumeLayout(false);
			InnerForm.ControlContents.PerformLayout();
			ResumeLayout(false);
		}
	}
}
