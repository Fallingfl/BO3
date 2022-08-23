using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Refract.UI.Core.Controls
{
	internal class CBFInnerPanelDesigner : ParentControlDesigner
	{
		public override SelectionRules SelectionRules => base.SelectionRules & ~SelectionRules.AllSizeable;

		protected override void PostFilterAttributes(IDictionary attributes)
		{
			base.PostFilterAttributes(attributes);
			attributes[typeof(DockingAttribute)] = new DockingAttribute(DockingBehavior.Never);
		}

		protected override void PostFilterProperties(IDictionary properties)
		{
			base.PostFilterProperties(properties);
			string[] array = new string[12]
			{
				"Dock", "Anchor", "Size", "Location", "Width", "Height", "MinimumSize", "MaximumSize", "AutoSize", "AutoSizeMode",
				"Visible", "Enabled"
			};
			foreach (string key in array)
			{
				if (properties.Contains(key))
				{
					properties[key] = TypeDescriptor.CreateProperty(base.Component.GetType(), (PropertyDescriptor)properties[key], new BrowsableAttribute(browsable: false));
				}
			}
		}
	}
}
