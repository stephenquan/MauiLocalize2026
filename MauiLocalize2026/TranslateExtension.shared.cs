// TranslateExtension.shared.cs

using System.Globalization;
using CommunityToolkit.Maui;

namespace MauiLocalize2026;

/// <summary>
/// A XAML markup extension that provides localized strings based on a specified key and optional formatting arguments.
/// </summary>
[ContentProperty(nameof(Key))]
[RequireService([typeof(IReferenceProvider), typeof(IProvideValueTarget)])]
public partial class TranslateExtension : BindableObject, IMarkupExtension<BindingBase>
{
	/// <summary>
	/// The key of the string to localize.
	/// </summary>
	[BindableProperty]
	public partial string Key { get; set; } = string.Empty;

	/// <summary>
	/// The first optional argument for string formatting.
	/// </summary>
	[BindableProperty]
	public partial object? X0 { get; set; } = null;

	/// <summary>
	/// 
	/// </summary>
	[BindableProperty]
	public partial object? X1 { get; set; } = null;

	/// <summary>
	/// Provides the value of the markup extension, which is a MultiBinding that binds to the current UI culture, culture, key, and optional formatting arguments.
	/// </summary>
	public BindingBase ProvideValue(IServiceProvider serviceProvider)
	{
		// Chain the BindingContext of the target object to this extension's BindingContext, so that the bindings can resolve correctly.
		if (!IsSet(BindingContextProperty)
			&& serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget provideValueTarget
			&& provideValueTarget.TargetObject is BindableObject targetObject)
		{
			this.SetBinding(BindingContextProperty, static (BindableObject b) => b.BindingContext, BindingMode.OneWay, source: targetObject);
		}

		// Create a MultiBinding that binds to the current UI culture, culture, key, and optional formatting arguments, and uses a converter to retrieve the localized string.
		return new MultiBinding
		{
			Bindings =
			{
				BindingBase.Create(static (LocalizationManager lm) => lm.CurrentUICulture, BindingMode.OneWay, source: LocalizationManager.Current),
				BindingBase.Create(static (LocalizationManager lm) => lm.CurrentCulture, BindingMode.OneWay, source: LocalizationManager.Current),
				BindingBase.Create(static (TranslateExtension e) => e.Key, BindingMode.OneWay, source: this),
				BindingBase.Create(static (TranslateExtension e) => e.X0,  BindingMode.OneWay, source: this),
				BindingBase.Create(static (TranslateExtension e) => e.X1,  BindingMode.OneWay, source: this),
			},
			Mode = BindingMode.OneWay,
			Converter = new TranslateExtensionMultiConverter()
		};
	}
	object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
		=> ProvideValue(serviceProvider);

	/// <summary>
	/// A multi-value converter that retrieves a localized string based on the provided key and optional formatting arguments.
	/// </summary>
	class TranslateExtensionMultiConverter : IMultiValueConverter
	{
		public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.Length >= 5
				&& values[2] is string key
				&& !string.IsNullOrEmpty(key))
			{
				CultureInfo? uiCulture = values[0] as CultureInfo;
				CultureInfo? formatCulture = values[1] as CultureInfo;
				object? x0 = values[3];
				object? x1 = values[4];
				return LocalizationManager.Current.GetString(key, x0, x1);
			}
			return null;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
