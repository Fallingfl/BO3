using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Refract.UI.Core.Interfaces;

namespace Refract.UI.Core.Singletons
{
	internal static class UIThemeManager
	{
		private static HashSet<Control> ThemedControls;

		private static Dictionary<Type, ThemeChangedCallback> CustomTypeHandlers;

		private static Dictionary<Control, ThemeChangedCallback> CustomControlHandlers;

		public static UIThemeInfo CurrentTheme { get; private set; }

		static UIThemeManager()
		{
			ThemedControls = new HashSet<Control>();
			CustomTypeHandlers = new Dictionary<Type, ThemeChangedCallback>();
			CustomControlHandlers = new Dictionary<Control, ThemeChangedCallback>();
			CurrentTheme = UIThemeInfo.Default();
		}

		internal static void SetThemeAware(this IThemeableControl control)
		{
			Control control2 = control as Control;
			if (control2 == null)
			{
				throw new InvalidOperationException($"Cannot theme control of type '{control.GetType()}' because it is not derived from Control");
			}
			foreach (Control themedControl in control.GetThemedControls())
			{
				IThemeableControl themeableControl = themedControl as IThemeableControl;
				if (themeableControl != null)
				{
					themeableControl.SetThemeAware();
				}
				else
				{
					RegisterAndThemeControl(themedControl);
				}
			}
			RegisterAndThemeControl(control2);
		}

		private static void ThemedControlDisposed(object sender, EventArgs e)
		{
			ThemedControls.Remove(sender as Control);
		}

		internal static void ApplyTheme(UIThemeInfo theme)
		{
			CurrentTheme = theme;
			foreach (Control themedControl in ThemedControls)
			{
				ThemeSpecificControl(themedControl);
			}
		}

		public static void RegisterCustomThemeHandler(Type type, ThemeChangedCallback callback)
		{
			if (callback == null)
			{
				CustomTypeHandlers.Remove(type);
			}
			else
			{
				CustomTypeHandlers[type] = callback;
			}
		}

		public static void OnThemeChanged(Control control, ThemeChangedCallback callback)
		{
			if (control != null)
			{
				if (callback == null)
				{
					CustomControlHandlers.Remove(control);
					return;
				}
				CustomControlHandlers[control] = callback;
				control.Disposed += CustomThemeCallback_ControlDisposed;
			}
		}

		private static void CustomThemeCallback_ControlDisposed(object sender, EventArgs e)
		{
			CustomControlHandlers.Remove(sender as Control);
		}

		private static void ThemeSpecificControl(Control control)
		{
			if (CustomTypeHandlers.ContainsKey(control.GetType()))
			{
				CustomTypeHandlers[control.GetType()]?.Invoke(CurrentTheme);
			}
			else
			{
				Form form = control as Form;
				if (form == null)
				{
					Button button = control as Button;
					if (button == null)
					{
						Label label = control as Label;
						if (label == null)
						{
							Panel panel = control as Panel;
							if (panel == null)
							{
								throw new NotImplementedException($"Theming procedure for control type: '{control.GetType()}' has not been implemented.");
							}
							panel.BackColor = CurrentTheme.BackColor;
						}
						else
						{
							label.ForeColor = CurrentTheme.TextColor;
						}
					}
					else
					{
						button.BackColor = CurrentTheme.BackColor;
						button.FlatAppearance.BorderColor = CurrentTheme.AccentColor;
						button.FlatStyle = CurrentTheme.ButtonFlatStyle;
						button.ForeColor = CurrentTheme.TextColor;
						button.FlatAppearance.MouseOverBackColor = CurrentTheme.ButtonHoverColor;
					}
				}
				else
				{
					form.BackColor = CurrentTheme.BackColor;
					form.ForeColor = CurrentTheme.TextColor;
				}
			}
			if (CustomControlHandlers.ContainsKey(control))
			{
				CustomControlHandlers[control](CurrentTheme);
			}
		}

		private static void RegisterAndThemeControl(Control control)
		{
			control.Disposed += ThemedControlDisposed;
			ThemedControls.Add(control);
			ThemeSpecificControl(control);
		}
	}
}
