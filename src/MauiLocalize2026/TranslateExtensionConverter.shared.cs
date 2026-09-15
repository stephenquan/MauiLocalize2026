// TranslateExtensionConverter.shared.cs

using System.Globalization;

namespace MauiLocalize2026;

/// <summary>
/// A multi-value converter that retrieves a localized string based on the provided key and optional formatting arguments.
/// </summary>
class TranslateExtensionConverter : IMultiValueConverter
{
	public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		if (values.Length == 4
			&& values[2] is string key
			&& !string.IsNullOrEmpty(key)
			&& values[3] is object?[] args)
		{
			CultureInfo? uiCulture = values[0] as CultureInfo;
			CultureInfo? formatCulture = values[1] as CultureInfo;
			return LocalizationManager.Current.GetString(key, args);
		}
		return null;
	}

	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
