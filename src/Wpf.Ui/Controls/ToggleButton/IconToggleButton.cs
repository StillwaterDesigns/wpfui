using System.Windows.Controls;

// ReSharper disable once CheckNamespace
namespace Wpf.Ui.Controls;

/// <summary>
/// Inherited from the <see cref="System.Windows.Controls.Primitives.ToggleButton"/>, adding <see cref="SymbolRegular"/>.
/// </summary>
/// <example>
/// <code lang="xml">
/// &lt;ui:ToggleButton
///     Appearance="Primary"
///     Content="WPF UI toggle button with font icon"
///     Icon="{ui:SymbolIcon Symbol=Fluent24}" /&gt;
/// </code>
/// <code lang="xml">
/// &lt;ui:ToggleButton
///     Appearance="Primary"
///     Content="WPF UI toggle button with font icon"
///     Icon="{ui:FontIcon '&#x1F308;'}" /&gt;
/// </code>
/// </example>
/// <remarks>
/// The <see cref="IconToggleButton"/> class inherits from the base <see cref="System.Windows.Controls.Primitives.ToggleButton"/> class.
/// </remarks>
public class IconToggleButton : System.Windows.Controls.Primitives.ToggleButton, IAppearanceControl, IIconControl {
	/// <summary>Identifies the <see cref="Icon"/> dependency property.</summary>
	public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
		nameof(Icon),
		typeof(IconElement),
		typeof(IconToggleButton),
		new PropertyMetadata(null, null, IconElement.Coerce)
	);

	/// <summary>Identifies the <see cref="Appearance"/> dependency property.</summary>
	public static readonly DependencyProperty AppearanceProperty = DependencyProperty.Register(
		nameof(Appearance),
		typeof(ControlAppearance),
		typeof(IconToggleButton),
		new PropertyMetadata(ControlAppearance.Primary)
	);

	/// <summary>Identifies the <see cref="MouseOverBackground"/> dependency property.</summary>
	public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register(
		nameof(MouseOverBackground),
		typeof(Brush),
		typeof(IconToggleButton),
		new PropertyMetadata(Border.BackgroundProperty.DefaultMetadata.DefaultValue)
	);

	/// <summary>Identifies the <see cref="MouseOverBorderBrush"/> dependency property.</summary>
	public static readonly DependencyProperty MouseOverBorderBrushProperty = DependencyProperty.Register(
		nameof(MouseOverBorderBrush),
		typeof(Brush),
		typeof(IconToggleButton),
		new PropertyMetadata(Border.BorderBrushProperty.DefaultMetadata.DefaultValue)
	);

	/// <summary>Identifies the <see cref="PressedForeground"/> dependency property.</summary>
	public static readonly DependencyProperty PressedForegroundProperty = DependencyProperty.Register(
		nameof(PressedForeground),
		typeof(Brush),
		typeof(IconToggleButton),
		new FrameworkPropertyMetadata(
			SystemColors.ControlTextBrush,
			FrameworkPropertyMetadataOptions.Inherits
		)
	);

	/// <summary>Identifies the <see cref="PressedBackground"/> dependency property.</summary>
	public static readonly DependencyProperty PressedBackgroundProperty = DependencyProperty.Register(
		nameof(PressedBackground),
		typeof(Brush),
		typeof(IconToggleButton),
		new PropertyMetadata(Border.BackgroundProperty.DefaultMetadata.DefaultValue)
	);

	/// <summary>Identifies the <see cref="PressedBorderBrush"/> dependency property.</summary>
	public static readonly DependencyProperty PressedBorderBrushProperty = DependencyProperty.Register(
		nameof(PressedBorderBrush),
		typeof(Brush),
		typeof(IconToggleButton),
		new PropertyMetadata(Border.BorderBrushProperty.DefaultMetadata.DefaultValue)
	);

	/// <summary>Identifies the <see cref="CornerRadius"/> dependency property.</summary>
	public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
		nameof(CornerRadius),
		typeof(CornerRadius),
		typeof(IconToggleButton),
		new FrameworkPropertyMetadata(
			default(CornerRadius),
			FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender
		)
	);

	/// <summary>
	/// Gets or sets displayed <see cref="IconElement"/>.
	/// </summary>
	[Bindable(true)]
	[Category("Appearance")]
	public IconElement? Icon {
		get => (IconElement?)GetValue(IconProperty);
		set => SetValue(IconProperty, value);
	}

	/// <inheritdoc />
	[Bindable(true)]
	[Category("Appearance")]
	public ControlAppearance Appearance {
		get => (ControlAppearance)GetValue(AppearanceProperty);
		set => SetValue(AppearanceProperty, value);
	}

	/// <summary>
	/// Gets or sets background <see cref="Brush"/>.
	/// </summary>
	[Bindable(true)]
	[Category("Appearance")]
	public Brush MouseOverBackground {
		get => (Brush)GetValue(MouseOverBackgroundProperty);
		set => SetValue(MouseOverBackgroundProperty, value);
	}

	/// <summary>
	/// Gets or sets border <see cref="Brush"/> when the user mouses over the button.
	/// </summary>
	[Bindable(true)]
	[Category("Appearance")]
	public Brush MouseOverBorderBrush {
		get => (Brush)GetValue(MouseOverBorderBrushProperty);
		set => SetValue(MouseOverBorderBrushProperty, value);
	}

	/// <summary>
	/// Gets or sets the foreground <see cref="Brush"/> when the user clicks the button.
	/// </summary>
	[Bindable(true)]
	[Category("Appearance")]
	public Brush PressedForeground {
		get => (Brush)GetValue(PressedForegroundProperty);
		set => SetValue(PressedForegroundProperty, value);
	}

	/// <summary>
	/// Gets or sets background <see cref="Brush"/> when the user clicks the button.
	/// </summary>
	[Bindable(true)]
	[Category("Appearance")]
	public Brush PressedBackground {
		get => (Brush)GetValue(PressedBackgroundProperty);
		set => SetValue(PressedBackgroundProperty, value);
	}

	/// <summary>
	/// Gets or sets border <see cref="Brush"/> when the user clicks the button.
	/// </summary>
	[Bindable(true)]
	[Category("Appearance")]
	public Brush PressedBorderBrush {
		get => (Brush)GetValue(PressedBorderBrushProperty);
		set => SetValue(PressedBorderBrushProperty, value);
	}

	/// <summary>
	/// Gets or sets a value that represents the degree to which the corners of a <see cref="T:System.Windows.Controls.Border" /> are rounded.
	/// </summary>
	public CornerRadius CornerRadius {
		get => (CornerRadius)GetValue(CornerRadiusProperty);
		set => SetValue(CornerRadiusProperty, (object)value);
	}

	/// <summary>
	/// Gets or sets displayed checked <see cref="IconElement"/>.
	/// </summary>
	[Bindable(true)]
	[Category("Appearance")]
	public IconElement? CheckedIcon {
		get => (IconElement?)GetValue(CheckedIconProperty);
		set => SetValue(CheckedIconProperty, value);
	}
	/// <summary>Identifies the <see cref="CheckedIcon"/> dependency property.</summary>
	public static readonly DependencyProperty CheckedIconProperty = DependencyProperty.Register(nameof(CheckedIcon),
		typeof(IconElement),
		typeof(IconToggleButton));

	/// <summary>
	/// Gets or sets displayed unchecked <see cref="IconElement"/>.
	/// </summary>
	[Bindable(true)]
	[Category("Appearance")]
	public IconElement? UncheckedIcon {
		get => (IconElement?)GetValue(UncheckedIconProperty);
		set => SetValue(UncheckedIconProperty, value);
	}
	/// <summary>Identifies the <see cref="UncheckedIcon"/> dependency property.</summary>
	public static readonly DependencyProperty UncheckedIconProperty = DependencyProperty.Register(nameof(UncheckedIcon),
		typeof(IconElement), typeof(IconToggleButton));

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e) {
		base.OnPropertyChanged(e);
		switch (e.Property.Name) {
			case nameof(IsChecked):
				if (CheckedIcon is not null && UncheckedIcon is not null)
					SetCurrentValue(IconProperty, IsChecked is true ? CheckedIcon : UncheckedIcon);
				break;
			case nameof(CheckedIcon):
				if(IsChecked is true)
					SetCurrentValue(IconProperty, CheckedIcon);
				break;
			case nameof(UncheckedIcon):
				if (IsChecked is false)
					SetCurrentValue(IconProperty, UncheckedIcon);
				break;
			default:
				break;
		}
	}

	public IconToggleButton() : base() {
		if(CheckedIcon is not null && UncheckedIcon is not null)
			Icon = IsChecked is true ? CheckedIcon : UncheckedIcon;
	}
}

