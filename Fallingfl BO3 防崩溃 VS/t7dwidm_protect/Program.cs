using System;
using System.Windows.Forms;

namespace t7dwidm_protect
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			NativeStealth.SetStealthMode(NativeStealthType.ManualInvoke);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			Application.Run(new MainForm());
		}
	}
}
