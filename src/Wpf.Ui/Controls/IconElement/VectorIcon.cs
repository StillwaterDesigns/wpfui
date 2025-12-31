using System.Windows.Shapes;

using Wpf.Ui.Markup;

namespace Wpf.Ui.Controls;

/// <summary>
/// Displays vector path data as an IconElement.
/// </summary>
public class VectorIcon : IconElement {
	private Path? _path;

	public VectorIcon() { }

	public VectorIcon(string pathData, Brush? brush = null) : this() {
		Data = Geometry.Parse(pathData);
		Foreground = brush ?? (Brush)UiApplication.Current.Resources[ThemeResource.TextFillColorPrimaryBrush];
	}

	/// <summary>
	/// The vector path geometry being rendered.
	/// </summary>
	[TypeConverter(typeof(GeometryConverter))]
	public Geometry Data {
		get => (Geometry)GetValue(DataProperty);
		set => SetValue(DataProperty, value);
	}

	public static readonly DependencyProperty DataProperty =
		DependencyProperty.Register(
			nameof(Data),
			typeof(Geometry),
			typeof(VectorIcon),
			new FrameworkPropertyMetadata(
				Geometry.Empty,
				FrameworkPropertyMetadataOptions.AffectsRender,
				OnDataChanged
			)
		);

	/// <summary>
	/// Determines how the icon scales inside the available bounds.
	/// </summary>
	public Stretch Stretch {
		get => (Stretch)GetValue(StretchProperty);
		set => SetValue(StretchProperty, value);
	}

	public static readonly DependencyProperty StretchProperty =
		DependencyProperty.Register(
			nameof(Stretch),
			typeof(Stretch),
			typeof(VectorIcon),
			new FrameworkPropertyMetadata(
				Stretch.Uniform,
				FrameworkPropertyMetadataOptions.AffectsRender
			)
		);

	// Called when Foreground changes
	protected override void OnForegroundChanged(DependencyPropertyChangedEventArgs args) {
		if (_path is not null)
			_path.SetCurrentValue(Shape.FillProperty, Foreground);
	}

	private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
		if (d is VectorIcon icon && icon._path is not null)
			icon._path.SetCurrentValue(Path.DataProperty, (Geometry)e.NewValue);
	}

	/// <summary>
	/// Called once to create the inner visual.
	/// </summary>
	protected override UIElement InitializeChildren() {
		_path = new Path {
			Stretch = Stretch,
			Fill = Foreground,
			Data = Data,
			SnapsToDevicePixels = true
		};

		return _path;
	}
}
//public class VectorIcon: IconElement {
//	private static readonly Type mType = typeof(VectorIcon);


//	/// <summary>
//	/// Gets or sets the character code that identifies the icon glyph.
//	/// </summary>
//	/// <returns>The hexadecimal character code for the icon glyph.</returns>
//	public Geometry Geometry {
//		get => (Geometry)GetValue(GeometryProperty);
//		set => SetValue(GeometryProperty, value);
//	}
//	/// <summary>
//	/// Gets or sets the character code that identifies the icon glyph.
//	/// </summary>
//	/// <returns>The hexadecimal character code for the icon glyph.</returns>
//	public Geometry? ClipGeometry {
//		get => (Geometry?)GetValue(ClipGeometryProperty);
//		set => SetValue(ClipGeometryProperty, value);
//	}
//	/// <summary>Identifies the <see cref="Geometry"/> dependency property.</summary>
//	public static readonly DependencyProperty GeometryProperty = DependencyProperty.Register(
//		nameof(Geometry),
//		typeof(Geometry),
//		typeof(VectorIcon),
//		new FrameworkPropertyMetadata(Geometry.Empty)
//	);
//	/// <summary>Identifies the <see cref="ClipGeometry"/> dependency property.</summary>
//	public static readonly DependencyProperty ClipGeometryProperty = DependencyProperty.Register(
//		nameof(ClipGeometry),
//		typeof(Geometry),
//		typeof(VectorIcon),
//		new FrameworkPropertyMetadata(Geometry.Empty)
//	);

//	protected Image? Image { get; set; }
//	protected GeometryDrawing? GeometryDrawing { get; set; }

//	protected override void OnForegroundChanged(DependencyPropertyChangedEventArgs args) {
//		base.OnForegroundChanged(args);
//		GeometryDrawing?.SetCurrentValue(GeometryDrawing.BrushProperty, Foreground);
//	}

//	protected override UIElement InitializeChildren() {
//		GeometryDrawing = new GeometryDrawing {
//			Brush = Foreground,
//			Geometry = Geometry,
//		};
//		var drawingGroup = new DrawingGroup {
//			ClipGeometry = ClipGeometry
//		};
//		drawingGroup.Children.Add(GeometryDrawing);

//		Image = new Image {
//			Source = new DrawingImage(drawingGroup)
//		};

//		SetCurrentValue(FocusableProperty, false);
//		return Image;
//	}
//}