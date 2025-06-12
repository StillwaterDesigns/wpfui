using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

using Wpf.Ui.Controls;

namespace Wpf.Ui.Markup;

/// <summary>
/// Custom <see cref="MarkupExtension"/> which can provide <see cref="ImageIcon"/>.
/// </summary>
/// <example>
/// <code lang="xml">
/// &lt;ui:Button
///     Appearance="Primary"
///     Content="WPF UI button with font icon"
///     Icon="{ui:ImageIcon '/my-icon.png'}" /&gt;
/// </code>
/// <code lang="xml">
/// &lt;ui:Button Icon="{ui:ImageIcon 'pack://application:,,,/Assets/wpfui.png'}" /&gt;
/// </code>
/// <code lang="xml">
/// &lt;ui:HyperlinkButton Icon="{ui:ImageIcon 'pack://application:,,,/Assets/wpfui.png'}" /&gt;
/// </code>
/// <code lang="xml">
/// &lt;ui:TitleBar Icon="{ui:ImageIcon 'pack://application:,,,/Assets/wpfui.png'}" /&gt;
/// </code>
/// </example>
[ContentProperty(nameof(Source))]
[MarkupExtensionReturnType(typeof(ImageMaskIcon))]
public class ImageMaskIconExtension : MarkupExtension {
	public ImageMaskIconExtension(ImageSource? source) {
		Source = source;
	}

	[ConstructorArgument("source")]
	public ImageSource? Source { get; set; }

	public double Width { get; set; } = 16D;

	public double Height { get; set; } = 16D;

	public override object ProvideValue(IServiceProvider serviceProvider) {
		var imageIcon = new ImageMaskIcon {
			Source = Source,
			Width = Width,
			Height = Height
		};

		return imageIcon;
	}
}
