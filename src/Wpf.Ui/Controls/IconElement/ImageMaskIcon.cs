// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

// ReSharper disable once CheckNamespace
using System.Windows.Controls;

namespace Wpf.Ui.Controls;

/// <summary>
/// Represents an icon that uses an <see cref="System.Windows.Controls.Image"/> as its content.
/// </summary>
public class ImageMaskIcon : IconElement {
	/// <summary>Identifies the <see cref="Source"/> dependency property.</summary>
	public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
		nameof(Source),
		typeof(ImageSource),
		typeof(ImageMaskIcon),
		new FrameworkPropertyMetadata(
			null,
			FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
			OnSourceChanged
		)
	);

	/// <summary>
	/// Gets or sets the Source on this Image.
	/// </summary>
	public ImageSource? Source {
		get => (ImageSource?)GetValue(SourceProperty);
		set => SetValue(SourceProperty, value);
	}

	protected System.Windows.Controls.Image? Image { get; set; }
	private Grid? LayoutMask { get; set; }

	protected override UIElement InitializeChildren() {
		Image = new System.Windows.Controls.Image() { Source = Source, Stretch = Stretch.UniformToFill };
		if(Foreground == Brushes.Transparent)
			SetCurrentValue(ForegroundProperty, SystemColors.ControlTextBrush);
		LayoutMask = new Grid { Background = Foreground, SnapsToDevicePixels = true, Margin = this.Margin };
		LayoutMask.SetCurrentValue(OpacityMaskProperty, new VisualBrush(Image));
		return LayoutMask;
	}

	protected override void OnForegroundChanged(DependencyPropertyChangedEventArgs args) {
		base.OnForegroundChanged(args);
		LayoutMask?.SetCurrentValue(Panel.BackgroundProperty, Foreground);
	}

	private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
		ImageMaskIcon self = (ImageMaskIcon)d;
		if (self.Image is null)
			return;

		self.Image.SetCurrentValue(System.Windows.Controls.Image.SourceProperty, (ImageSource?)e.NewValue);
	}
}
