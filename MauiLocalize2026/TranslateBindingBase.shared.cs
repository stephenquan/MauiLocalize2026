// TranslateBindingBase.shared.cs

using System.Globalization;

namespace MauiLocalize2026;

/// <summary>
/// Provides a convenient way to create bindings for localized strings in a .NET MAUI application. It allows you to bind to localized resources and format them with additional arguments, supporting both UI culture and format culture.
/// </summary>
public static class TranslateBindingBase
{
	/// <summary>
	/// Creates a binding for a localized string based on the specified function and optional arguments.
	/// </summary>
	/// <param name="localizerProvider">A function that provides the localized string based on the current culture.</param>
	/// <param name="args">Optional arguments for string formatting.</param>
	/// <returns>A BindingBase instance for the localized string.</returns>
	public static BindingBase Create<TReturn>(Func<CultureInfo?, TReturn> localizerProvider, params object?[] args)
	{
		return CreateT(new Func<CultureInfo?, CultureInfo?, object?[], TReturn?>(
			(uiCulture, formatCulture, args) =>
			{
				return localizerProvider(uiCulture);
			}),
			args);
	}

	/// <summary>
	/// Creates a binding for a localized string based on the specified function and optional arguments.
	/// </summary>
	/// <param name="localizerProvider">A function that provides the localized string based on the current UI culture.</param>
	/// <param name="args">Optional arguments for string formatting.</param>
	/// <returns>A BindingBase instance for the localized string.</returns>
	public static BindingBase Create(Func<CultureInfo?, string?> localizerProvider, params object?[] args)
	{
		return CreateT(new Func<CultureInfo?, CultureInfo?, object?[], string?>(
			(uiCulture, formatCulture, args) =>
			{
				string? localizedString = localizerProvider(uiCulture);
				if (args.Length == 0)
				{
					return localizedString;
				}

				return string.Format(formatCulture, localizedString ?? string.Empty, args);
			}),
			args);
	}

	/// <summary>
	/// Creates a binding for a localized string based on the specified function and optional arguments.
	/// </summary>
	/// <param name="fullLocalizerProvider">A function that provides the localized string based on the current UI culture and format culture.</param>
	/// <param name="args">Optional arguments for string formatting.</param>
	/// <returns>A BindingBase instance for the localized string.</returns>
	public static BindingBase CreateT<TReturn>(Func<CultureInfo?, CultureInfo?, object?[], TReturn> fullLocalizerProvider, params object?[] args)
	{
		List<BindingBase> argBindings = new();
		foreach (var arg in args)
		{
			argBindings.Add(arg is BindingBase b ? b : new Binding(".", BindingMode.OneWay, source: arg));
		}
		return new MultiBinding
		{
			Bindings =
			{
				BindingBase.Create(static (LocalizationManager lm) => lm.CurrentUICulture, BindingMode.OneWay, source: LocalizationManager.Current),
				BindingBase.Create(static (LocalizationManager lm) => lm.CurrentCulture, BindingMode.OneWay, source: LocalizationManager.Current),
				new Binding(".", BindingMode.OneWay, source: fullLocalizerProvider),
				new MultiBinding
				{
					Bindings = argBindings,
					Mode = BindingMode.OneWay,
					Converter = new PassThruMultiConverter()
				}
			},
			Mode = BindingMode.OneWay,
			Converter = new TranslateMultiConverter<TReturn>()
		};
	}

	/// <summary>
	/// A multi-value converter that passes through the input values without modification.
	/// This converter is used to aggregate multiple binding values into an array for further processing in the localization logic.
	/// </summary>
	class PassThruMultiConverter : IMultiValueConverter
	{
		public object? Convert(object?[] values, Type targetType, object? parameter, CultureInfo culture)
		{
			return values;
		}
		public object?[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// A multi-value converter that retrieves a localized string based on the provided function and arguments.
	/// </summary>
	class TranslateMultiConverter<TReturn> : IMultiValueConverter
	{
		public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.Length >= 4
				&& values[2] is Func<CultureInfo?, CultureInfo?, object?[], TReturn> fullLocalizationProvider
				&& values[3] is object?[] args)
			{
				CultureInfo? uiCulture = values[0] as CultureInfo;
				CultureInfo? formatCulture = values[1] as CultureInfo;
				return fullLocalizationProvider(uiCulture, formatCulture, args);
			}
			return default(TReturn);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
