// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

// ReSharper disable once CheckNamespace
namespace Wpf.Ui.Controls;

/// <summary>
/// Represents a callback that determines the effective step for a <see cref="NumberBox"/> operation.
/// </summary>
/// <param name="sender">The number box performing the step.</param>
/// <param name="currentValue">The current value before the step is applied.</param>
/// <param name="requestedStep">The requested signed step.</param>
/// <returns>The signed step that will be applied.</returns>
public delegate double NumberBoxStepCoercionCallback(NumberBox sender, double currentValue, double requestedStep);
