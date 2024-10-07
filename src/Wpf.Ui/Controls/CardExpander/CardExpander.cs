// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

// ReSharper disable once CheckNamespace
using System.Windows.Controls;

namespace Wpf.Ui.Controls;

/// <summary>
/// Inherited from the <see cref="System.Windows.Controls.Expander"/> control which can hide the collapsible content.
/// </summary>
public class CardExpander : System.Windows.Controls.Expander {
	/// <summary>Identifies the <see cref="ToggleButtonTemplate"/> dependency property.</summary>
	public static readonly DependencyProperty ToggleButtonTemplateProperty = DependencyProperty.Register(
		nameof(ToggleButtonTemplate),
		typeof(ControlTemplate),
		typeof(CardExpander)
	);

	/// <summary>Identifies the <see cref="Icon"/> dependency property.</summary>
	public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(IconElement),
        typeof(CardExpander),
        new PropertyMetadata(null, null, IconElement.Coerce)
    );

    /// <summary>Identifies the <see cref="CornerRadius"/> dependency property.</summary>
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius),
        typeof(CornerRadius),
        typeof(CardExpander),
        new PropertyMetadata(new CornerRadius(4))
    );

    /// <summary>Identifies the <see cref="ContentPadding"/> dependency property.</summary>
    public static readonly DependencyProperty ContentPaddingProperty = DependencyProperty.Register(
        nameof(ContentPadding),
        typeof(Thickness),
        typeof(CardExpander),
        new FrameworkPropertyMetadata(
            default(Thickness),
            FrameworkPropertyMetadataOptions.AffectsParentMeasure
        )
    );

	/// <summary>Identifies the <see cref="HeaderOpacityMask"/> dependency property.</summary>
	public static readonly DependencyProperty HeaderOpacityMaskProperty = DependencyProperty.Register(
		nameof(HeaderOpacityMask),
		typeof(Brush),
		typeof(CardExpander),
		new FrameworkPropertyMetadata(
			default(Brush)
		)
	);

	/// <summary>Identifies the <see cref="BackgroundTint"/> dependency property.</summary>
	public static readonly DependencyProperty BackgroundTintProperty = DependencyProperty.Register(
		nameof(BackgroundTint),
		typeof(Brush),
		typeof(CardExpander),
		new FrameworkPropertyMetadata(
			default(Brush)
		)
	);
	/// <summary>
	/// Gets or sets toggle button template <see cref="ControlTemplate"/>.
	/// </summary>
	[Bindable(true)]
	public ControlTemplate? ToggleButtonTemplate {
		get => (ControlTemplate?)GetValue(ToggleButtonTemplateProperty);
		set => SetValue(ToggleButtonTemplateProperty, value);
	}

	/// <summary>
	/// Gets or sets displayed <see cref="IconElement"/>.
	/// </summary>
	[Bindable(true)]
    [Category("Appearance")]
    public IconElement? Icon
    {
        get => (IconElement?)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Gets or sets displayed <see cref="IconElement"/>.
    /// </summary>
    [Bindable(true)]
    [Category("Appearance")]
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets content padding Property
    /// </summary>
    [Bindable(true)]
    [Category("Layout")]
    public Thickness ContentPadding
    {
        get { return (Thickness)GetValue(ContentPaddingProperty); }
        set { SetValue(ContentPaddingProperty, value); }
	}

	/// <summary>
	/// Gets or sets header opacity mask
	/// </summary>
	[Bindable(true)]
	public Brush? HeaderOpacityMask {
		get => (Brush?)GetValue(HeaderOpacityMaskProperty);
		set => SetValue(HeaderOpacityMaskProperty, value);
	}

	/// <summary>
	/// Gets or sets header background tint
	/// </summary>
	[Bindable(true)]
	public Brush? BackgroundTint {
		get => (Brush?)GetValue(BackgroundTintProperty);
		set => SetValue(BackgroundTintProperty, value);
	}
}
