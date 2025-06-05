using System.Windows.Controls;
using System.Windows.Shapes;

namespace Wpf.Ui.Controls;
public class VectorIcon: IconElement {
	private static readonly Type mType = typeof(VectorIcon);


	/// <summary>
	/// Gets or sets the character code that identifies the icon glyph.
	/// </summary>
	/// <returns>The hexadecimal character code for the icon glyph.</returns>
	public Geometry Geometry {
		get => (Geometry)GetValue(GeometryProperty);
		set => SetValue(GeometryProperty, value);
	}
	/// <summary>
	/// Gets or sets the character code that identifies the icon glyph.
	/// </summary>
	/// <returns>The hexadecimal character code for the icon glyph.</returns>
	public Geometry? ClipGeometry {
		get => (Geometry?)GetValue(ClipGeometryProperty);
		set => SetValue(ClipGeometryProperty, value);
	}
	/// <summary>Identifies the <see cref="Geometry"/> dependency property.</summary>
	public static readonly DependencyProperty GeometryProperty = DependencyProperty.Register(
		nameof(Geometry),
		typeof(Geometry),
		typeof(VectorIcon),
		new FrameworkPropertyMetadata(Geometry.Empty)
	);
	/// <summary>Identifies the <see cref="Geometry"/> dependency property.</summary>
	public static readonly DependencyProperty ClipGeometryProperty = DependencyProperty.Register(
		nameof(ClipGeometry),
		typeof(Geometry),
		typeof(VectorIcon),
		new FrameworkPropertyMetadata(Geometry.Empty)
	);

	protected Image? Image { get; set; }
	protected GeometryDrawing? GeometryDrawing { get; set; }

	protected override void OnForegroundChanged(DependencyPropertyChangedEventArgs args) {
		base.OnForegroundChanged(args);
		GeometryDrawing?.SetCurrentValue(GeometryDrawing.BrushProperty, Foreground);
	}

	protected override UIElement InitializeChildren() {
		GeometryDrawing = new GeometryDrawing {
			Brush = Foreground,
			Geometry = Geometry,
		};
		var drawingGroup = new DrawingGroup {
			ClipGeometry = ClipGeometry
		};
		drawingGroup.Children.Add(GeometryDrawing);

		Image = new Image {
			Source = new DrawingImage(drawingGroup)
		};

		SetCurrentValue(FocusableProperty, false);
		return Image;
	}
}