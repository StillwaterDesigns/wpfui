// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Runtime.InteropServices;

using Wpf.Ui.Input;
using Wpf.Ui.Interop;
using Wpf.Ui.Interop.WinDef;

using Size = System.Windows.Size;

// ReSharper disable once CheckNamespace
namespace Wpf.Ui.Controls;

/// <summary>
/// Customized window for notifications.
/// </summary>
public class MessageBox : Window {
    /// <summary>Identifies the <see cref="ShowTitle"/> dependency property.</summary>
    public static readonly DependencyProperty ShowTitleProperty = DependencyProperty.Register(
        nameof(ShowTitle),
        typeof(bool),
        typeof(MessageBox),
        new PropertyMetadata(true)
    );

    /// <summary>Identifies the <see cref="PrimaryButtonText"/> dependency property.</summary>
    public static readonly DependencyProperty PrimaryButtonTextProperty = DependencyProperty.Register(
        nameof(PrimaryButtonText),
        typeof(string),
        typeof(MessageBox),
        new PropertyMetadata(string.Empty)
    );

    /// <summary>Identifies the <see cref="SecondaryButtonText"/> dependency property.</summary>
    public static readonly DependencyProperty SecondaryButtonTextProperty = DependencyProperty.Register(
        nameof(SecondaryButtonText),
        typeof(string),
        typeof(MessageBox),
        new PropertyMetadata(string.Empty)
    );

    /// <summary>Identifies the <see cref="CloseButtonText"/> dependency property.</summary>
    public static readonly DependencyProperty CloseButtonTextProperty = DependencyProperty.Register(
        nameof(CloseButtonText),
        typeof(string),
        typeof(MessageBox),
        new PropertyMetadata("Close")
    );

    /// <summary>Identifies the <see cref="PrimaryButtonIcon"/> dependency property.</summary>
    public static readonly DependencyProperty PrimaryButtonIconProperty = DependencyProperty.Register(
        nameof(PrimaryButtonIcon),
        typeof(SymbolRegular),
        typeof(MessageBox),
        new PropertyMetadata(SymbolRegular.Empty)
    );

    /// <summary>Identifies the <see cref="SecondaryButtonIcon"/> dependency property.</summary>
    public static readonly DependencyProperty SecondaryButtonIconProperty = DependencyProperty.Register(
        nameof(SecondaryButtonIcon),
        typeof(SymbolRegular),
        typeof(MessageBox),
        new PropertyMetadata(SymbolRegular.Empty)
    );

    /// <summary>Identifies the <see cref="CloseButtonIcon"/> dependency property.</summary>
    public static readonly DependencyProperty CloseButtonIconProperty = DependencyProperty.Register(
        nameof(CloseButtonIcon),
        typeof(SymbolRegular),
        typeof(MessageBox),
        new PropertyMetadata(SymbolRegular.Empty)
    );

    /// <summary>Identifies the <see cref="PrimaryButtonAppearance"/> dependency property.</summary>
    public static readonly DependencyProperty PrimaryButtonAppearanceProperty = DependencyProperty.Register(
        nameof(PrimaryButtonAppearance),
        typeof(ControlAppearance),
        typeof(MessageBox),
        new PropertyMetadata(ControlAppearance.Primary)
    );

    /// <summary>Identifies the <see cref="SecondaryButtonAppearance"/> dependency property.</summary>
    public static readonly DependencyProperty SecondaryButtonAppearanceProperty = DependencyProperty.Register(
        nameof(SecondaryButtonAppearance),
        typeof(ControlAppearance),
        typeof(MessageBox),
        new PropertyMetadata(ControlAppearance.Secondary)
    );

    /// <summary>Identifies the <see cref="CloseButtonAppearance"/> dependency property.</summary>
    public static readonly DependencyProperty CloseButtonAppearanceProperty = DependencyProperty.Register(
        nameof(CloseButtonAppearance),
        typeof(ControlAppearance),
        typeof(MessageBox),
        new PropertyMetadata(ControlAppearance.Secondary)
    );

    /// <summary>Identifies the <see cref="IsPrimaryButtonEnabled"/> dependency property.</summary>
    public static readonly DependencyProperty IsPrimaryButtonEnabledProperty = DependencyProperty.Register(
        nameof(IsPrimaryButtonEnabled),
        typeof(bool),
        typeof(MessageBox),
        new PropertyMetadata(true)
    );

    /// <summary>Identifies the <see cref="IsSecondaryButtonEnabled"/> dependency property.</summary>
    public static readonly DependencyProperty IsSecondaryButtonEnabledProperty = DependencyProperty.Register(
        nameof(IsSecondaryButtonEnabled),
        typeof(bool),
        typeof(MessageBox),
        new PropertyMetadata(true)
    );

    /// <summary>Identifies the <see cref="TemplateButtonCommand"/> dependency property.</summary>
    public static readonly DependencyProperty TemplateButtonCommandProperty = DependencyProperty.Register(
        nameof(TemplateButtonCommand),
        typeof(IRelayCommand),
        typeof(MessageBox),
        new PropertyMetadata(null)
    );

	/// <summary>
	/// Gets or sets a value indicating whether to show the <see cref="Window.Title"/> in <see cref="TitleBar"/>.
	/// </summary>
	public bool ShowTitle
    {
        get => (bool)GetValue(ShowTitleProperty);
        set => SetValue(ShowTitleProperty, value);
    }

    /// <summary>
    /// Gets or sets the text to display on the primary button.
    /// </summary>
    public string PrimaryButtonText
    {
        get => (string)GetValue(PrimaryButtonTextProperty);
        set => SetValue(PrimaryButtonTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the text to be displayed on the secondary button.
    /// </summary>
    public string SecondaryButtonText
    {
        get => (string)GetValue(SecondaryButtonTextProperty);
        set => SetValue(SecondaryButtonTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the text to display on the close button.
    /// </summary>
    public string CloseButtonText
    {
        get => (string)GetValue(CloseButtonTextProperty);
        set => SetValue(CloseButtonTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="SymbolRegular"/> on the primary button
    /// </summary>
    public SymbolRegular PrimaryButtonIcon
    {
        get => (SymbolRegular)GetValue(PrimaryButtonIconProperty);
        set => SetValue(PrimaryButtonIconProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="SymbolRegular"/> on the secondary button
    /// </summary>
    public SymbolRegular SecondaryButtonIcon
    {
        get => (SymbolRegular)GetValue(SecondaryButtonIconProperty);
        set => SetValue(SecondaryButtonIconProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="SymbolRegular"/> on the close button
    /// </summary>
    public SymbolRegular CloseButtonIcon
    {
        get => (SymbolRegular)GetValue(CloseButtonIconProperty);
        set => SetValue(CloseButtonIconProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="ControlAppearance"/> on the primary button
    /// </summary>
    public ControlAppearance PrimaryButtonAppearance
    {
        get => (ControlAppearance)GetValue(PrimaryButtonAppearanceProperty);
        set => SetValue(PrimaryButtonAppearanceProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="ControlAppearance"/> on the secondary button
    /// </summary>
    public ControlAppearance SecondaryButtonAppearance
    {
        get => (ControlAppearance)GetValue(SecondaryButtonAppearanceProperty);
        set => SetValue(SecondaryButtonAppearanceProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="ControlAppearance"/> on the close button
    /// </summary>
    public ControlAppearance CloseButtonAppearance
    {
        get => (ControlAppearance)GetValue(CloseButtonAppearanceProperty);
        set => SetValue(CloseButtonAppearanceProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="MessageBox"/> primary button is enabled.
    /// </summary>
    public bool IsSecondaryButtonEnabled
    {
        get => (bool)GetValue(IsSecondaryButtonEnabledProperty);
        set => SetValue(IsSecondaryButtonEnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="MessageBox"/> secondary button is enabled.
    /// </summary>
    public bool IsPrimaryButtonEnabled
    {
        get => (bool)GetValue(IsPrimaryButtonEnabledProperty);
        set => SetValue(IsPrimaryButtonEnabledProperty, value);
    }

    /// <summary>
    /// Gets the command triggered after clicking the button on the Footer.
    /// </summary>
    public IRelayCommand TemplateButtonCommand => (IRelayCommand)GetValue(TemplateButtonCommandProperty);

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageBox"/> class.
    /// </summary>
    public MessageBox()
    {
        Topmost = true;
        SetValue(TemplateButtonCommandProperty, new RelayCommand<MessageBoxButton>(OnButtonClick));

        PreviewMouseDoubleClick += static (_, args) => args.Handled = true;

        Loaded += static (sender, _) =>
        {
            var self = (MessageBox)sender;
            self.OnLoaded();
        };
    }

    protected TaskCompletionSource<MessageBoxResult>? Tcs { get; set; }

    [Obsolete($"Use {nameof(ShowDialogAsync)} instead")]
    public new void Show()
    {
        throw new InvalidOperationException($"Use {nameof(ShowDialogAsync)} instead");
    }

    [Obsolete($"Use {nameof(ShowDialogAsync)} instead")]
    public new bool? ShowDialog()
    {
        throw new InvalidOperationException($"Use {nameof(ShowDialogAsync)} instead");
    }

    [Obsolete($"Use {nameof(Close)} with MessageBoxResult instead")]
    public new void Close()
    {
        throw new InvalidOperationException($"Use {nameof(Close)} with MessageBoxResult instead");
    }

    /// <summary>
    /// Displays a message box
    /// </summary>
    /// <returns><see cref="MessageBoxResult"/></returns>
    /// <exception cref="TaskCanceledException">Thrown if the operation is canceled.</exception>
    public async Task<MessageBoxResult> ShowDialogAsync(
        bool showAsDialog = true,
        CancellationToken cancellationToken = default
    )
    {
        Tcs = new TaskCompletionSource<MessageBoxResult>();
        CancellationTokenRegistration tokenRegistration = cancellationToken.Register(
            o => Tcs.TrySetCanceled((CancellationToken)o!),
            cancellationToken
        );

        try
        {
            RemoveTitleBarAndApplyMica();

            if (showAsDialog)
            {
                base.ShowDialog();
            }
            else
            {
                base.Show();
            }

            return await Tcs.Task;
        }
        finally
        {
#if NET6_0_OR_GREATER
            await tokenRegistration.DisposeAsync();
#else
            tokenRegistration.Dispose();
#endif
        }
    }

    /// <summary>
    /// Occurs after Loading event
    /// </summary>
    protected virtual void OnLoaded()
    {
        var rootElement = (UIElement)GetVisualChild(0)!;

        ResizeToContentSize(rootElement);
		ApplyWindowStartupLocation();
    }

    /// <summary>
    /// Resizes the MessageBox to fit the content's size, including margins.
    /// </summary>
    /// <param name="rootElement">The root element of the MessageBox</param>
    protected virtual void ResizeToContentSize(UIElement rootElement)
    {
        Size desiredSize = rootElement.DesiredSize;

        // left and right margin
        const double margin = 12.0 * 2;

        SetCurrentValue(WidthProperty, desiredSize.Width + margin);
        SetCurrentValue(HeightProperty, desiredSize.Height);

        ResizeWidth(rootElement);
        ResizeHeight(rootElement);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);

        if (e.Cancel)
        {
            return;
        }

        _ = Tcs?.TrySetResult(MessageBoxResult.None);
    }

	/// <summary>
	/// Positions the MessageBox according to <see cref="Window.WindowStartupLocation"/>.
	/// </summary>
	protected virtual void ApplyWindowStartupLocation() {
		switch (WindowStartupLocation) {
			case WindowStartupLocation.CenterOwner:
				CenterWindowOnOwner();
				break;
			case WindowStartupLocation.CenterScreen:
				CenterWindowOnScreen();
				break;
			case WindowStartupLocation.Manual:
				break;
		}
	}
	protected virtual void CenterWindowOnOwner() {
		if (Owner is null)
			return;

		var ownerHandle = new WindowInteropHelper(Owner).Handle;
		if (ownerHandle == IntPtr.Zero || !GetWindowRect(ownerHandle, out var rect))
			return;

		var source = PresentationSource.FromVisual(Owner);
		if (source?.CompositionTarget is null)
			return;

		var topLeft = source.CompositionTarget.TransformFromDevice.Transform(
			new Point(rect.Left, rect.Top)
		);

		var bottomRight = source.CompositionTarget.TransformFromDevice.Transform(
			new Point(rect.Right, rect.Bottom)
		);

		double ownerWidth = bottomRight.X - topLeft.X;
		double ownerHeight = bottomRight.Y - topLeft.Y;

		SetCurrentValue(LeftProperty, topLeft.X + ((ownerWidth - Width) / 2));
		SetCurrentValue(TopProperty, topLeft.Y + ((ownerHeight - Height) / 2));
	}

	protected virtual void CenterWindowOnScreen() {
		nint monitor;

		if (Owner is not null) {
			var ownerHandle = new WindowInteropHelper(Owner).Handle;

			monitor = MonitorFromWindow(
				ownerHandle,
				MonitorOptions.MONITOR_DEFAULTTONEAREST
			);
		} else if (GetCursorPos(out var cursorPosition)) {
			monitor = MonitorFromPoint(
				cursorPosition,
				MonitorOptions.MONITOR_DEFAULTTONEAREST
			);
		} else {
			return;
		}

		var monitorInfo = new MONITORINFO {
			cbSize = (uint)Marshal.SizeOf<MONITORINFO>()
		};

		if (!GetMonitorInfo(monitor, ref monitorInfo))
			return;

		var source = PresentationSource.FromVisual(this);
		if (source?.CompositionTarget is null)
			return;

		var topLeft = source.CompositionTarget.TransformFromDevice.Transform(
			new Point(monitorInfo.rcWork.Left, monitorInfo.rcWork.Top)
		);

		var bottomRight = source.CompositionTarget.TransformFromDevice.Transform(
			new Point(monitorInfo.rcWork.Right, monitorInfo.rcWork.Bottom)
		);

		double workAreaWidth = bottomRight.X - topLeft.X;
		double workAreaHeight = bottomRight.Y - topLeft.Y;

		SetCurrentValue(LeftProperty, topLeft.X + ((workAreaWidth - Width) / 2));
		SetCurrentValue(TopProperty, topLeft.Y + ((workAreaHeight - Height) / 2));
	}

	/// <summary>
	/// Occurs after the <see cref="MessageBoxButton"/> is clicked
	/// </summary>
	/// <param name="button">The MessageBox button</param>
	protected virtual void OnButtonClick(MessageBoxButton button)
    {
        MessageBoxResult result = button switch
        {
            MessageBoxButton.Primary => MessageBoxResult.Primary,
            MessageBoxButton.Secondary => MessageBoxResult.Secondary,
            _ => MessageBoxResult.None
        };

        _ = Tcs?.TrySetResult(result);
        base.Close();
    }

    private void RemoveTitleBarAndApplyMica()
    {
        _ = UnsafeNativeMethods.RemoveWindowTitlebarContents(this);
        _ = WindowBackdrop.ApplyBackdrop(this, WindowBackdropType.Mica);
    }

    private void ResizeWidth(UIElement element)
    {
        if (Width <= MaxWidth)
        {
            return;
        }

        SetCurrentValue(WidthProperty, MaxWidth);
        element.UpdateLayout();

        SetCurrentValue(HeightProperty, element.DesiredSize.Height);

        if (Height > MaxHeight)
        {
            SetCurrentValue(MaxHeightProperty, Height);
        }
    }

    private void ResizeHeight(UIElement element)
    {
        if (Height <= MaxHeight)
        {
            return;
        }

        SetCurrentValue(HeightProperty, MaxHeight);
        element.UpdateLayout();

        SetCurrentValue(WidthProperty, element.DesiredSize.Width);

        if (Width > MaxWidth)
        {
            SetCurrentValue(MaxWidthProperty, Width);
        }
    }

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

	[StructLayout(LayoutKind.Sequential)]
	private struct RECT {
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;
	}
	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetCursorPos(out POINT lpPoint);

	[DllImport("user32.dll")]
	internal static extern nint MonitorFromWindow(nint hwnd, MonitorOptions dwFlags);

	[DllImport("user32.dll")]
	private static extern nint MonitorFromPoint(POINT pt, MonitorOptions dwFlags);

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetMonitorInfo(nint hMonitor, ref MONITORINFO lpmi);
	[StructLayout(LayoutKind.Sequential)]
	private struct POINT {
		public int X;
		public int Y;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	private struct MONITORINFO {
		public uint cbSize;
		public RECT rcMonitor;
		public RECT rcWork;
		public uint dwFlags;
	}

	internal enum MonitorOptions : uint {
		MONITOR_DEFAULTTONULL = 0x00000000,
		MONITOR_DEFAULTTOPRIMARY = 0x00000001,
		MONITOR_DEFAULTTONEAREST = 0x00000002
	}
}
