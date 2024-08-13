namespace Wpf.Ui.Appearance;
public static class ApplicationThemeColorManager {
	private static Type mType = typeof(ApplicationThemeColorManager);


	/// <summary>
	/// Gets the ApplicationBackgroundColor.
	/// </summary>
	public static ThemeColor ApplicationBackgroundColor {
		get {
			var resource = UiApplication.Current.Resources[nameof(ApplicationBackgroundColor)];
			return new ThemeColor(resource is Color color ? color : Colors.Transparent);
		}
	}

	/// <summary>
	/// Gets the TextFillColorPrimary.
	/// </summary>
	public static ThemeColor TextFillColorPrimary {
		get {
			var resource = UiApplication.Current.Resources[nameof(TextFillColorPrimary)];
			return new ThemeColor(resource is Color color ? color : Colors.Black);
		}
	}

	/// <summary>
	/// Gets the ControlStrokeColorDefault.
	/// </summary>
	public static ThemeColor ControlStrokeColorDefault {
		get {
			var resource = UiApplication.Current.Resources[nameof(ControlStrokeColorDefault)];
			return new ThemeColor(resource is Color color ? color : Colors.Transparent);
		}
	}

	/// <summary>
	/// Gets the ControlStrokeColorTertiary.
	/// </summary>
	public static ThemeColor ControlStrokeColorTertiary {
		get {
			var resource = UiApplication.Current.Resources[nameof(ControlStrokeColorTertiary)];
			return new ThemeColor(resource is Color color ? color : Colors.Transparent);
		}
	}
}