// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.
//
// This Source Code is partially based on the source code provided by the .NET Foundation.
//
// TODO: Mask (with placeholder); Clipboard paste;
// TODO: Constant decimals when formatting. Although this can actually be done with NumberFormatter.
// TODO: Disable expression by default
// TODO: Lock to digit characters only by property

using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

// ReSharper disable once CheckNamespace
namespace Wpf.Ui.Controls;

/// <summary>
/// Represents a control that can be used to display and edit numbers.
/// </summary>
public class NumberBox : TextBox {
	private bool _valueUpdating;

	/// <summary>Identifies the <see cref="Value"/> dependency property.</summary>
	public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
		nameof(Value),
		typeof(double?),
		typeof(NumberBox),
		new FrameworkPropertyMetadata(
			null,
			FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
			OnValueChanged,
			null,
			false,
			UpdateSourceTrigger.PropertyChanged
		)
	);

	/// <summary>Identifies the <see cref="MaxDecimalPlaces"/> dependency property.</summary>
	public static readonly DependencyProperty MaxDecimalPlacesProperty = DependencyProperty.Register(
		nameof(MaxDecimalPlaces),
		typeof(int),
		typeof(NumberBox),
		new PropertyMetadata(6)
	);

	/// <summary>Identifies the <see cref="SmallChange"/> dependency property.</summary>
	public static readonly DependencyProperty SmallChangeProperty = DependencyProperty.Register(
		nameof(SmallChange),
		typeof(double),
		typeof(NumberBox),
		new PropertyMetadata(1.0d)
	);

	/// <summary>Identifies the <see cref="LargeChange"/> dependency property.</summary>
	public static readonly DependencyProperty LargeChangeProperty = DependencyProperty.Register(
		nameof(LargeChange),
		typeof(double),
		typeof(NumberBox),
		new PropertyMetadata(10.0d)
	);

	/// <summary>Identifies the <see cref="Maximum"/> dependency property.</summary>
	public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
		nameof(Maximum),
		typeof(double),
		typeof(NumberBox),
		new PropertyMetadata(double.MaxValue)
	);

	/// <summary>Identifies the <see cref="Minimum"/> dependency property.</summary>
	public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
		nameof(Minimum),
		typeof(double),
		typeof(NumberBox),
		new PropertyMetadata(double.MinValue)
	);

	/// <summary>Identifies the <see cref="AcceptsExpression"/> dependency property.</summary>
	public static readonly DependencyProperty AcceptsExpressionProperty = DependencyProperty.Register(
		nameof(AcceptsExpression),
		typeof(bool),
		typeof(NumberBox),
		new PropertyMetadata(true)
	);

	/// <summary>Identifies the <see cref="IncrementEnabled"/> dependency property.</summary>
	public static readonly DependencyProperty IncrementEnabledProperty = DependencyProperty.Register(
		nameof(IncrementEnabled),
		typeof(bool),
		typeof(NumberBox),
		new PropertyMetadata(true)
	);

	/// <summary>Identifies the <see cref="DecrementEnabled"/> dependency property.</summary>
	public static readonly DependencyProperty DecrementEnabledProperty = DependencyProperty.Register(
		nameof(DecrementEnabled),
		typeof(bool),
		typeof(NumberBox),
		new PropertyMetadata(true)
	);

	/// <summary>Identifies the <see cref="SpinButtonPlacementMode"/> dependency property.</summary>
	public static readonly DependencyProperty SpinButtonPlacementModeProperty = DependencyProperty.Register(
		nameof(SpinButtonPlacementMode),
		typeof(NumberBoxSpinButtonPlacementMode),
		typeof(NumberBox),
		new PropertyMetadata(NumberBoxSpinButtonPlacementMode.Inline)
	);

	/// <summary>Identifies the <see cref="ValidationMode"/> dependency property.</summary>
	public static readonly DependencyProperty ValidationModeProperty = DependencyProperty.Register(
		nameof(ValidationMode),
		typeof(NumberBoxValidationMode),
		typeof(NumberBox),
		new PropertyMetadata(NumberBoxValidationMode.InvalidInputOverwritten)
	);

	/// <summary>Identifies the <see cref="NumberFormatter"/> dependency property.</summary>
	public static readonly DependencyProperty NumberFormatterProperty = DependencyProperty.Register(
		nameof(NumberFormatter),
		typeof(INumberFormatter),
		typeof(NumberBox),
		new PropertyMetadata(null, OnNumberFormatterChanged)
	);

	/// <summary>Identifies the <see cref="ValueChanged"/> routed event.</summary>
	public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
		nameof(ValueChanged),
		RoutingStrategy.Bubble,
		typeof(RoutedEventHandler),
		typeof(NumberBox)
	);


	/// <summary>Identifies the <see cref="CoerceStepperSmChangeCallback"/> routed event.</summary>
	public static readonly DependencyProperty CoerceStepperSmChangeCallbackProperty = DependencyProperty.Register(
		nameof(CoerceStepperSmChangeCallback),
		typeof(CoerceValueCallback),
		typeof(NumberBox),
		new PropertyMetadata(null)
	);

	/// <summary>
	/// Gets or sets the numeric value of a <see cref="NumberBox"/>.
	/// </summary>
	public CoerceValueCallback? CoerceStepperSmChangeCallback {
		get => (CoerceValueCallback?)GetValue(CoerceStepperSmChangeCallbackProperty);
		set => SetValue(CoerceStepperSmChangeCallbackProperty, value);
	}

	/// <summary>
	/// Gets or sets the numeric value of a <see cref="NumberBox"/>.
	/// </summary>
	public double? Value {
		get => (double?)GetValue(ValueProperty);
		set => SetValue(ValueProperty, value);
	}

	/// <summary>
	/// Gets or sets whether the increment step button is enabled/>.
	/// </summary>
	public bool IncrementEnabled {
		get => (bool)GetValue(IncrementEnabledProperty);
		set => SetValue(IncrementEnabledProperty, value);
	}

	/// <summary>
	/// Gets or sets whether the decrement step button is enabled/>.
	/// </summary>
	public bool DecrementEnabled {
		get => (bool)GetValue(DecrementEnabledProperty);
		set => SetValue(DecrementEnabledProperty, value);
	}

	/// <summary>
	/// Gets or sets the number of decimal places to be rounded when converting from Text to Value.
	/// </summary>
	public int MaxDecimalPlaces {
		get => (int)GetValue(MaxDecimalPlacesProperty);
		set => SetValue(MaxDecimalPlacesProperty, value);
	}

	/// <summary>
	/// Gets or sets the value that is added to or subtracted from <see cref="Value"/> when a small change is made, such as with an arrow key or scrolling.
	/// </summary>
	public double SmallChange {
		get => (double)GetValue(SmallChangeProperty);
		set => SetValue(SmallChangeProperty, value);
	}

	/// <summary>
	/// Gets or sets the value that is added to or subtracted from <see cref="Value"/> when a large change is made, such as with the PageUP and PageDown keys.
	/// </summary>
	public double LargeChange {
		get => (double)GetValue(LargeChangeProperty);
		set => SetValue(LargeChangeProperty, value);
	}

	/// <summary>
	/// Gets or sets the numerical maximum for <see cref="Value"/>.
	/// </summary>
	public double Maximum {
		get => (double)GetValue(MaximumProperty);
		set => SetValue(MaximumProperty, value);
	}

	/// <summary>
	/// Gets or sets the numerical minimum for <see cref="Value"/>.
	/// </summary>
	public double Minimum {
		get => (double)GetValue(MinimumProperty);
		set => SetValue(MinimumProperty, value);
	}

	/// <summary>
	/// Gets or sets a value indicating whether the control will accept and evaluate a basic formulaic expression entered as input.
	/// </summary>
	public bool AcceptsExpression {
		get => (bool)GetValue(AcceptsExpressionProperty);
		set => SetValue(AcceptsExpressionProperty, value);
	}

	/// <summary>
	/// Gets or sets the number formatter.
	/// </summary>
	public INumberFormatter? NumberFormatter {
		get => (INumberFormatter?)GetValue(NumberFormatterProperty);
		set => SetValue(NumberFormatterProperty, value);
	}

	/// <summary>
	/// Gets or sets a value that indicates the placement of buttons used to increment or decrement the <see cref="Value"/> property.
	/// </summary>
	public NumberBoxSpinButtonPlacementMode SpinButtonPlacementMode {
		get => (NumberBoxSpinButtonPlacementMode)GetValue(SpinButtonPlacementModeProperty);
		set => SetValue(SpinButtonPlacementModeProperty, value);
	}

	/// <summary>
	/// Gets or sets the input validation behavior to invoke when invalid input is entered.
	/// </summary>
	public NumberBoxValidationMode ValidationMode {
		get => (NumberBoxValidationMode)GetValue(ValidationModeProperty);
		set => SetValue(ValidationModeProperty, value);
	}

	/// <summary>
	/// Occurs after the user triggers evaluation of new input by pressing the Enter key, clicking a spin button, or by changing focus.
	/// </summary>
	public event RoutedEventHandler ValueChanged {
		add => AddHandler(ValueChangedEvent, value);
		remove => RemoveHandler(ValueChangedEvent, value);
	}

	static NumberBox() {
		AcceptsReturnProperty.OverrideMetadata(typeof(NumberBox), new FrameworkPropertyMetadata(false));
		MaxLinesProperty.OverrideMetadata(typeof(NumberBox), new FrameworkPropertyMetadata(1));
		MinLinesProperty.OverrideMetadata(typeof(NumberBox), new FrameworkPropertyMetadata(1));
	}

	public NumberBox() : base() {
		NumberFormatter ??= GetRegionalSettingsAwareDecimalFormatter();
		DataObject.AddPastingHandler(this, OnClipboardPaste);
	}

	protected override void OnPreviewTextInput(TextCompositionEventArgs e) {
		base.OnPreviewTextInput(e);
		var decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
		var regexStr = MaxDecimalPlaces > 0 ? @$"^\d*\{decimalSeparator}?\d*$" : @"^\d*$";
		if (Minimum < 0)
			regexStr = regexStr.Insert(1, "-?");
		var isValidPattern = new Regex(regexStr);
		var isInputValid = isValidPattern.IsMatch(e.Text);
		if (!isInputValid && e.Text.Length > 0)
			e.Handled = true;
	}

	/// <inheritdoc />
	protected override void OnKeyUp(KeyEventArgs e) {
		base.OnKeyUp(e);
		if (IsReadOnly)
			return;

		switch (e.Key) {
			case Key.PageUp:
				StepValue(LargeChange);
				break;
			case Key.PageDown:
				StepValue(-LargeChange);
				break;
			case Key.Up:
				SetCurrentValue(SmallChangeProperty, (CoerceStepperSmChangeCallback?.Invoke(this, Value + SmallChange) ?? SmallChange));
				StepValue(SmallChange);
				break;
			case Key.Down:
				SetCurrentValue(SmallChangeProperty, (CoerceStepperSmChangeCallback?.Invoke(this, Value - SmallChange) ?? SmallChange));
				StepValue(-SmallChange);
				break;
			case Key.Enter:
				if (TextWrapping != TextWrapping.Wrap) {
					ValidateInput();
					MoveCaretToTextEnd();
				}

				FocusManager.SetFocusedElement(FocusManager.GetFocusScope(this), null);
				FocusManager.SetFocusedElement(FocusManager.GetFocusScope(this), this);
				break;
			default:
				break;
		}
	}

	/// <inheritdoc />
	protected override void OnTemplateButtonClick(string? parameter) {
		System.Diagnostics.Debug.WriteLine(
			$"INFO: {typeof(NumberBox)} button clicked with param: {parameter}",
			"Wpf.Ui.NumberBox"
		);

		switch (parameter) {
			case "clear":
				OnClearButtonClick();
				break;
			case "increment":
				SetCurrentValue(SmallChangeProperty,
					Convert.ToDouble(CoerceStepperSmChangeCallback?.Invoke(this, SmallChange) ?? SmallChange));
				StepValue(SmallChange);
				break;
			case "decrement":
				SetCurrentValue(SmallChangeProperty,
					Convert.ToDouble(CoerceStepperSmChangeCallback?.Invoke(this, -SmallChange) ?? SmallChange));
				StepValue(-SmallChange);
				break;
		}

		// NOTE: Focus looks and works well with mouse and Clear button. But it sucks for spin buttons
		//_ = Focus();
	}

	/// <inheritdoc />
	protected override void OnLostFocus(RoutedEventArgs e) {
		try {
			var textValue = Text;
			if(string.IsNullOrEmpty(Text))
				textValue = $"{Math.Max(Value ?? Minimum, Minimum)}";
			SetCurrentValue(TextProperty, RemoveStringFormatting(textValue));
			//SetCurrentValue(TextProperty, textValue);
			base.OnLostFocus(e);
			ValidateInput();
		} catch (FormatException fe) {
		}
	}

	protected override void OnGotFocus(RoutedEventArgs e) {
		base.OnGotFocus(e);
		SetCurrentValue(TextProperty, RemoveStringFormatting(Text));
		SelectAll();
	}

	/// <inheritdoc />
	protected override void OnTemplateChanged(ControlTemplate oldTemplate,
		ControlTemplate newTemplate) {
		base.OnTemplateChanged(oldTemplate, newTemplate);

		// If Text has been set, but Value hasn't, update Value based on Text.
		if (string.IsNullOrEmpty(Text) && Value != null)
			UpdateValueToText();
		else
			UpdateTextToValue();
	}

	/// <summary>
	/// Is called when <see cref="Value"/> in this <see cref="NumberBox"/> changes.
	/// </summary>
	protected virtual void OnValueChanged(DependencyObject d, double? oldValue) {
		if (_valueUpdating)
			return;

		_valueUpdating = true;
		var newValue = Value;
		if (newValue > Maximum)
			SetCurrentValue(ValueProperty, Maximum);
		if (newValue < Minimum)
			SetCurrentValue(ValueProperty, Minimum);

		if (!Equals(newValue, oldValue))
			RaiseEvent(new RoutedEventArgs(ValueChangedEvent));

		UpdateTextToValue();
		_valueUpdating = false;
	}

	/// <summary>
	/// Is called when something is pasted in this <see cref="NumberBox"/>.
	/// </summary>
	protected virtual void OnClipboardPaste(object sender, DataObjectPastingEventArgs e) {
		// TODO: Fix clipboard
		if (sender is not NumberBox)
			return;

		ValidateInput();
	}

	private void StepValue(double? change) {
		System.Diagnostics.Debug.WriteLine(
			$"INFO: {typeof(NumberBox)} {nameof(StepValue)} raised, change {change}",
			"Wpf.Ui.NumberBox"
		);

		// Before adjusting the value, validate the contents of the textbox so we don't override it.
		ValidateInput();
		var newValue = Value ?? 0;
		if (change is not null)
			newValue += change ?? 0d;

		SetCurrentValue(ValueProperty, newValue);
		
		MoveCaretToTextEnd();
	}

	private void UpdateTextToValue() {
		var newText = string.Empty;
		if (Value is not null && NumberFormatter is not null)
			newText = NumberFormatter.FormatDouble(Math.Round((double)Value, MaxDecimalPlaces));

		var bb = BindingOperations.GetBindingBase(this, TextProperty);
		if (bb is not null && bb.StringFormat is not null)
			newText = string.Format(bb.StringFormat, Value);
		if (newText != Text) {
			SetCurrentValue(IncrementEnabledProperty, Value < Maximum);
			SetCurrentValue(DecrementEnabledProperty, Value > Minimum);
			SetCurrentValue(TextProperty, newText);
		}
	}

	private void UpdateValueToText() {
		ValidateInput();
	}

	private void ValidateInput() {
		var text = RemoveStringFormatting(Text);
		if (string.IsNullOrEmpty(text)) {
			SetCurrentValue(ValueProperty, null);
			return;
		}

		var numberParser = NumberFormatter as INumberParser;
		var value = numberParser!.ParseDouble(text);
		if (value is null || Equals(Value, value)) {
			UpdateTextToValue();
			return;
		}

		value = Math.Max(Math.Min(value.Value, Maximum), Minimum);
		SetCurrentValue(ValueProperty, value);
		UpdateTextToValue();
	}

	private void MoveCaretToTextEnd() {
		CaretIndex = Text.Length;
	}

	private string RemoveStringFormatting(string inString) {
		var decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
		var groupSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
		var cleanStr = inString.Replace(groupSeparator, string.Empty);
		var regexIntOrDecimal = new Regex($@"(?:^|[^\w{decimalSeparator}])(\d[\d{decimalSeparator}]+)(?=\W|$)");
		var regexMatch = regexIntOrDecimal.Match(cleanStr).Value;
		regexMatch = Regex.Replace(regexMatch, "^0+(?!$)", string.Empty);
		var regexMatches = regexIntOrDecimal.Matches(cleanStr, 0);
		var regexReplace = regexIntOrDecimal.Replace(cleanStr, string.Empty);
		var resultStr = regexMatches.Count > 0 ? regexMatch : regexReplace;
		if(string.IsNullOrEmpty(resultStr))
			return resultStr;
		var numValue = double.Parse(resultStr);
		numValue = Math.Max(Math.Min(numValue, Maximum), Minimum);
		return $"{numValue}";
	}

	private static INumberFormatter GetRegionalSettingsAwareDecimalFormatter() {
		return new ValidateNumberFormatter();
	}

	private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
		if (d is not NumberBox numberBox)
			return;
		numberBox.OnValueChanged(d, (double?)e.OldValue);
	}

	private static void OnNumberFormatterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
		if (e.NewValue is not INumberParser) {
			throw new InvalidOperationException(
				$"{nameof(NumberFormatter)} must implement {typeof(INumberParser)}"
			);
		}
	}
}
