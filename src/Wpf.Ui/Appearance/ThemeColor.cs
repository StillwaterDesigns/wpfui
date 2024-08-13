namespace Wpf.Ui.Appearance;
public struct ThemeColor {
	public ThemeColor(Color color) : this() {
		Color = color;
		Brush = new SolidColorBrush(Color);
	}

	public Color Color { get; }
	public Brush Brush { get; }
}