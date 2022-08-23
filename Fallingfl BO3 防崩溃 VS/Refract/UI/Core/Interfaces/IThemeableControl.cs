using System.Collections.Generic;
using System.Windows.Forms;

namespace Refract.UI.Core.Interfaces
{
	internal interface IThemeableControl
	{
		IEnumerable<Control> GetThemedControls();
	}
}
