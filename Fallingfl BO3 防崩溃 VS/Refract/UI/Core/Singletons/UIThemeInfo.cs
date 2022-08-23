using System.Drawing;
using System.Windows.Forms;

namespace Refract.UI.Core.Singletons
{
	internal struct UIThemeInfo
	{
		public Color BackColor;

		public Color AccentColor;

		public Color TextColor;

		public Color TitleBarColor;

		public FlatStyle ButtonFlatStyle;

		public Color ButtonHoverColor;

		public Color LightBackColor;

		public Color ButtonActive;

		public Color TextInactive;

		public static UIThemeInfo Default()
		{
			UIThemeInfo result = default(UIThemeInfo);
			result.BackColor = Color.FromArgb(28, 28, 28);
			result.TextColor = Color.WhiteSmoke;
			result.AccentColor = Color.DodgerBlue;
			result.TitleBarColor = Color.FromArgb(36, 36, 36);
			result.ButtonFlatStyle = FlatStyle.Flat;
			result.ButtonHoverColor = Color.FromArgb(50, 50, 50);
			result.LightBackColor = Color.FromArgb(36, 36, 36);
			result.ButtonActive = Color.DodgerBlue;
			result.TextInactive = Color.Gray;
			return result;
		}
	}
}
