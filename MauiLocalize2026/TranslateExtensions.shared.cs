// TranslateExtensions.shared.cs

using System.Globalization;

namespace MauiLocalize2026;

/// <summary>
/// Provides extension methods for localization in MAUI applications.
/// </summary>
public static class TranslateExtensions
{
	/// <summary>
	/// Provides an extension method to set a localized binding on a bindable object.
	/// </summary>
	/// <typeparam name="T">The type of the bindable object.</typeparam>
	/// <typeparam name="TProvider">The return type of the localized resource.</typeparam>
	/// <param name="bindable">The bindable object on which to set the localized binding.</param>
	/// <param name="targetProperty">The bindable property to bind to.</param>
	/// <param name="localizationProvider">A function that provides the localized resource based on the current culture.</param>
	/// <param name="args">Optional arguments for string formatting.</param>
	/// <returns>The bindable object with the localized binding set.</returns>
	public static T Translate<T, TProvider>(this T bindable, BindableProperty targetProperty, Func<CultureInfo?, TProvider> localizationProvider, params object?[] args) where T : BindableObject
	{
		var binding = TranslateBindingBase.Create(localizationProvider, args);
		bindable.SetBinding(targetProperty, binding);
		return bindable;
	}

	/// <summary>
	/// Provides an extension method to set a localized binding on a bindable object with additional culture information.
	/// </summary>
	/// <typeparam name="T">The type of the bindable object.</typeparam>
	/// <typeparam name="TProvider">The return type of the localized resource.</typeparam>
	/// <param name="bindable">The bindable object on which to set the localized binding.</param>
	/// <param name="targetProperty">The bindable property to bind to.</param>
	/// <param name="localizationProvider">A function that provides the localized string based on the current UI culture, culture, and optional formatting arguments.</param>
	/// <param name="args">Optional arguments for string formatting.</param>
	/// <returns>The bindable object with the localized binding set.</returns>
	public static T Translate<T, TProvider>(this T bindable, BindableProperty targetProperty, Func<CultureInfo?, CultureInfo?, object?[], TProvider> localizationProvider, params object?[] args) where T : BindableObject
	{
		var binding = TranslateBindingBase.Create<TProvider>(localizationProvider, args);
		bindable.SetBinding(targetProperty, binding);
		return bindable;
	}
}
