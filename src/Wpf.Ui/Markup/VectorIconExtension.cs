using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

using Wpf.Ui.Controls;

namespace Wpf.Ui.Markup;

/// <summary>
/// Custom <see cref="MarkupExtension"/> which can provide <see cref="VectorIcon"/>.
/// </summary>
/// <example>
/// <code lang="xml">
/// &lt;ui:Button
///     Appearance="Primary"
///     Content="WPF UI button with font icon"
///     Icon="{ui:FontIcon '&#x1F308;'}" /&gt;
/// </code>
/// <code lang="xml">
/// &lt;ui:Button Icon="{ui:FontIcon '&amp;#x1F308;'}" /&gt;
/// </code>
/// <code lang="xml">
/// &lt;ui:HyperlinkButton Icon="{ui:FontIcon '&amp;#x1F308;'}" /&gt;
/// </code>
/// <code lang="xml">
/// &lt;ui:TitleBar Icon="{ui:FontIcon '&amp;#x1F308;'}" /&gt;
/// </code>
/// </example>
[ContentProperty(nameof(Geometry))]
[MarkupExtensionReturnType(typeof(VectorIcon))]
public class VectorIconExtension(Geometry geometry) : MarkupExtension {
	public VectorIconExtension(Geometry geometry, Geometry clipGeometry)  : this(geometry) {
		ClipGeometry = clipGeometry;
	}

	[ConstructorArgument("geometry")]
	public Geometry Geometry { get; set; } = geometry;


	[ConstructorArgument("clipGeometry")]
	public Geometry? ClipGeometry { get; set; }

	public override object ProvideValue(IServiceProvider serviceProvider) {
		var vectorIcon = new VectorIcon {
			Geometry = Geometry,
			ClipGeometry = ClipGeometry,
		};
		return vectorIcon;
	}
}
