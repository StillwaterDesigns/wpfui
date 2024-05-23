// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Wpf.Ui.Controls;

namespace Wpf.Ui.Demo.Mvvm.ViewModels;

public partial class SettingsViewModel : ObservableObject, INavigationAware {
	private bool _isInitialized = false;

	[ObservableProperty]
	private string _appVersion = string.Empty;

	[ObservableProperty]
	private Appearance.ApplicationTheme _currentApplicationTheme =
		Appearance
		.ApplicationTheme
		.Unknown;

	public void OnNavigatedTo() {
		if (!_isInitialized)
			InitializeViewModel();
	}

	public void OnNavigatedFrom() { }

	private void InitializeViewModel() {
		CurrentApplicationTheme = Appearance.ApplicationThemeManager.GetAppTheme();
		AppVersion = $"Wpf.Ui.Demo.Mvvm - {GetAssemblyVersion()}";

		_isInitialized = true;
	}

	private static string GetAssemblyVersion() {
		return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
			?? string.Empty;
	}

	[RelayCommand]
	private void OnChangeTheme(string parameter) {
		switch (parameter) {
			case "theme_light":
				if (CurrentApplicationTheme == Appearance.ApplicationTheme.Light) {
					break;
				}

				Appearance.ApplicationThemeManager.Apply(Appearance.ApplicationTheme.Light);
				CurrentApplicationTheme = Appearance.ApplicationTheme.Light;

				break;

			default:
				if (CurrentApplicationTheme == Appearance.ApplicationTheme.Dark) {
					break;
				}

				Appearance.ApplicationThemeManager.Apply(Appearance.ApplicationTheme.Dark);
				CurrentApplicationTheme = Appearance.ApplicationTheme.Dark;

				break;
		}
	}
}
