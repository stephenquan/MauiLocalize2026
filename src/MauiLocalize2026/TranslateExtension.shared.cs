// TranslateExtension.shared.cs

using CommunityToolkit.Maui;

namespace MauiLocalize2026;

/// <summary>
/// A XAML markup extension that provides localized strings based on a specified key and optional formatting arguments.
/// </summary>
[ContentProperty(nameof(Key))]
[RequireService([typeof(IReferenceProvider), typeof(IProvideValueTarget)])]
public partial class TranslateExtension : BindableObject, IMarkupExtension<BindingBase>
{
	/// <summary>The key of the string to localize.</summary>
	[BindableProperty]
	public partial string Key { get; set; } = string.Empty;

	/// <summary>The {0} argument for string formatting.</summary>
	[BindableProperty]
	public partial object? X0 { get; set; } = null;

	/// <summary>The {1} argument for string formatting.</summary>
	[BindableProperty]
	public partial object? X1 { get; set; } = null;

	/// <summary>The {2} argument for string formatting.</summary>
	[BindableProperty]
	public partial object? X2 { get; set; } = null;

	/// <summary>The {3} argument for string formatting.</summary>
	[BindableProperty]
	public partial object? X3 { get; set; } = null;

	/// <summary>The {4} argument for string formatting.</summary>
	[BindableProperty]
	public partial object? X4 { get; set; } = null;

	/// <summary>The {5} argument for string formatting.</summary>
	[BindableProperty]
	public partial object? X5 { get; set; } = null;

	/// <summary>The {6} argument for string formatting.</summary>
	[BindableProperty]
	public partial object? X6 { get; set; } = null;

	/// <summary>The {7} argument for string formatting.</summary>
	[BindableProperty]
	public partial object? X7 { get; set; } = null;

	/// <summary>The {8} argument for string formatting.</summary>
	[BindableProperty]
	public partial object? X8 { get; set; } = null;

	/// <summary>The {9} argument for string formatting.</summary>
	[BindableProperty]
	public partial object? X9 { get; set; } = null;

	/// <summary>
	/// Provides the value of the markup extension, which is a MultiBinding that binds to the current UI culture, culture, key, and optional formatting arguments.
	/// </summary>
	public BindingBase ProvideValue(IServiceProvider serviceProvider)
	{
		// Chain the BindingContext of the target object to this extension's BindingContext, so that the bindings can resolve correctly.
		this.PropagateBindingContext(serviceProvider);

		// Create a MultiBinding that binds to the current UI culture, culture, key, and optional formatting arguments, and uses a converter to retrieve the localized string.
		return new MultiBinding
		{
			Bindings =
			{
				BindingBase.Create(static (LocalizationManager lm) => lm.CurrentUICulture, BindingMode.OneWay, source: LocalizationManager.Current),
				BindingBase.Create(static (LocalizationManager lm) => lm.CurrentCulture, BindingMode.OneWay, source: LocalizationManager.Current),
				BindingBase.Create(static (TranslateExtension e) => e.Key, BindingMode.OneWay, source: this),
				new MultiBinding
				{
					Bindings = new(BindableProperty targetProperty, Func<BindingBase> makeBinding)[]
					{
						(X0Property, () => BindingBase.Create(static (TranslateExtension ctx) => ctx.X0, BindingMode.OneWay, source: this)),
						(X1Property, () => BindingBase.Create(static (TranslateExtension ctx) => ctx.X1, BindingMode.OneWay, source: this)),
						(X2Property, () => BindingBase.Create(static (TranslateExtension ctx) => ctx.X2, BindingMode.OneWay, source: this)),
						(X3Property, () => BindingBase.Create(static (TranslateExtension ctx) => ctx.X3, BindingMode.OneWay, source: this)),
						(X4Property, () => BindingBase.Create(static (TranslateExtension ctx) => ctx.X4, BindingMode.OneWay, source: this)),
						(X5Property, () => BindingBase.Create(static (TranslateExtension ctx) => ctx.X5, BindingMode.OneWay, source: this)),
						(X6Property, () => BindingBase.Create(static (TranslateExtension ctx) => ctx.X6, BindingMode.OneWay, source: this)),
						(X7Property, () => BindingBase.Create(static (TranslateExtension ctx) => ctx.X7, BindingMode.OneWay, source: this)),
						(X8Property, () => BindingBase.Create(static (TranslateExtension ctx) => ctx.X8, BindingMode.OneWay, source: this)),
						(X9Property, () => BindingBase.Create(static (TranslateExtension ctx) => ctx.X9, BindingMode.OneWay, source: this))
					}
					.TakeWhile(arg => IsSet(arg.targetProperty))
					.Select(arg => arg.makeBinding())
					.ToList(),
					Mode = BindingMode.OneWay,
					Converter = passThroughConverter
				}
			},
			Mode = BindingMode.OneWay,
			Converter = translateExtensionConverter
		};
	}

	object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
		=> ProvideValue(serviceProvider);

	static TranslateExtensionConverter translateExtensionConverter = new();
	static PassThroughConverter passThroughConverter = new();
}
