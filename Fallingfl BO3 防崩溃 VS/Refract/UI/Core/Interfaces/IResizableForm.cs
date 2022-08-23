using System.Windows.Forms;

namespace Refract.UI.Core.Interfaces
{
	internal interface IResizableForm
	{
		void WndProc_Implementation(ref Message m);
	}
}
