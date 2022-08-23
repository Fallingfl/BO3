using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Refract.UI.Core.Controls
{
	internal class CBorderedFormDesigner : ParentControlDesigner
	{
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			Panel controlContents = ((CBorderedForm)Control).ControlContents;
			EnableDesignMode(controlContents, "ControlContents");
		}

		public override bool CanParent(Control control)
		{
			return false;
		}

		protected override void OnDragOver(DragEventArgs de)
		{
			de.Effect = DragDropEffects.None;
		}

		protected override IComponent[] CreateToolCore(ToolboxItem tool, int x, int y, int width, int height, bool hasLocation, bool hasSize)
		{
			return null;
		}
	}
}
