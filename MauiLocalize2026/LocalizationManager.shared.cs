// LocalizationManager.shared.cs

using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace MauiLocalize2026;

/// <summary>
/// Manages localization for the application.
/// 
/// It provides a thin observable wrapper for CultureInfo.CurrentUICulture and CultureInfo.CurrentCulture,
/// allowing for binding to localized strings and responding to culture changes in the application.
/// </summary>
public partial class LocalizationManager : INotifyPropertyChanged
{
	/// <summary>
	/// Gets the current instance of the LocalizationManager.
	/// </summary>
	public static LocalizationManager Current { get; } = new();

	/// <summary>
	/// Gets or sets the function used to retrieve localized strings based on the current culture.
	/// </summary>
	public Func<string, CultureInfo, string?>? LocalizationProvider { get; set; }

	/// <summary>
	/// Retrieves a localized string based on the specified culture and optional formatting arguments.
	/// </summary>
	/// <param name="s">The key of the string to localize.</param>
	/// <param name="currentUICulture">The UI culture to use for localization.</param>
	/// <param name="args">Optional arguments for string formatting.</param>
	/// <returns>The localized string.</returns>
	public string? GetString(CultureInfo? currentUICulture, string s, params object?[] args)
		=> GetString(currentUICulture, CultureInfo.CurrentCulture, s, args);

	/// <summary>
	/// Retrieves a localized string based on the specified UI culture, format culture, and optional formatting arguments.
	/// </summary>
	/// <param name="currentUICulture">The UI culture to use for localization.</param>
	/// <param name="currentCulture">The culture to use for formatting.</param>
	/// <param name="s">The key of the string to localize.</param>
	/// <param name="args">Optional arguments for string formatting.</param>
	/// <returns></returns>
	public string? GetString(CultureInfo? currentUICulture, CultureInfo? currentCulture, string s, params object?[] args)
	{
		if (LocalizationProvider is not null)
		{
			var localizedString = LocalizationProvider(s, currentUICulture ?? CultureInfo.CurrentUICulture);
			if (args.Length == 0)
			{
				return localizedString;
			}
			return string.Format(currentCulture ?? CultureInfo.CurrentCulture, localizedString ?? string.Empty, args);
		}
		return null;
	}

	/// <summary>
	/// Retrieves a localized string based on the current UI culture and optional formatting arguments.
	/// </summary>
	/// <param name="s">The key of the string to localize.</param>
	/// <param name="args">Optional arguments for string formatting.</param>
	/// <returns>The localized string.</returns>
	public string? GetString(string s, params object?[] args)
		=> GetString(null, s, args);

	/// <summary>
	/// Gets or sets the current UI culture (for localized strings).
	/// </summary>
	public CultureInfo CurrentUICulture
	{
		get => CultureInfo.CurrentUICulture;
		set => SetProperty(CultureInfo.CurrentUICulture, value, static (c) => CultureInfo.CurrentUICulture = c);
	}

	/// <summary>
	/// Gets or sets the current culture (for formatting of numbers, dates, etc.).
	/// </summary>
	public CultureInfo CurrentCulture
	{
		get => CultureInfo.CurrentCulture;
		set => SetProperty(CultureInfo.CurrentCulture, value, static (c) => CultureInfo.CurrentCulture = c);
	}

	/// <summary>
	/// Sets the property value and raises the PropertyChanged event if the value has changed.
	/// </summary>
	/// <typeparam name="T">The type of the property.</typeparam>
	/// <param name="oldValue">The property's current value.</param>
	/// <param name="value">The new value to set.</param>
	/// <param name="applyValue">An action to apply the new value.</param>
	/// <param name="propertyName">The name of the property.</param>
	/// <returns>True if the value was changed; otherwise, false.</returns>
	bool SetProperty<T>(T oldValue, T value, Action<T> applyValue, [CallerMemberName] string? propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(oldValue, value))
		{
			return false;
		}

		applyValue(value);
		OnPropertyChanged(propertyName);
		return true;
	}

	/// <summary>
	/// Raises the PropertyChanged event for the specified property name.
	/// </summary>
	/// <param name="propertyName"></param>
	public void OnPropertyChanged([CallerMemberName] string? propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	/// <summary>
	/// Raises the PropertyChanged event for the specified property name.
	/// </summary>
	public event PropertyChangedEventHandler? PropertyChanged;
}
