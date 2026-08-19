// LocalizationManager.shared.cs

using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiLocalize2026;

/// <summary>
/// Manages localization for the application.
/// 
/// It provides a thin observable wrapper for CultureInfo.CurrentUICulture and CultureInfo.CurrentCulture,
/// allowing for binding to localized strings and responding to culture changes in the application.
/// </summary>
public partial class LocalizationManager : ObservableObject
{
	/// <summary>
	/// Gets the current instance of the LocalizationManager.
	/// </summary>
	public static LocalizationManager Current { get; } = new();

	/// <summary>
	/// Gets or sets the function used to retrieve localized strings based on the current culture.
	/// </summary>
	public Func<string, CultureInfo, string?>? Localizer { get; set; }
	//Func<string, CultureInfo, object?[], string?>? Localizer { get; set; }

	/// <summary>
	/// Retrieves a localized string based on the specified culture and optional formatting arguments.
	/// </summary>
	/// <param name="s">The key of the string to localize.</param>
	/// <param name="culture">The culture to use for localization.</param>
	/// <param name="args">Optional arguments for string formatting.</param>
	/// <returns>The localized string.</returns>
	public string? GetString(CultureInfo? culture, string s, params object?[] args)
	{
		if (Localizer is not null)
		{
			if (args.Length == 0)
			{
				return Localizer(s, culture ?? CultureInfo.CurrentUICulture);
			}
			return string.Format(CultureInfo.CurrentCulture, Localizer(s, culture ?? CultureInfo.CurrentUICulture) ?? string.Empty, args);
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
	public CultureInfo UICulture
	{
		get => CultureInfo.CurrentUICulture;
		set
		{
			CultureInfo.CurrentUICulture = value;
			OnPropertyChanged(nameof(UICulture));
		}
	}

	/// <summary>
	/// Gets or sets the current culture (for formatting of numbers, dates, etc.).
	/// </summary>
	public CultureInfo Culture
	{
		get => CultureInfo.CurrentCulture;
		set
		{
			CultureInfo.CurrentCulture = value;
			OnPropertyChanged(nameof(Culture));
		}
	}
}
