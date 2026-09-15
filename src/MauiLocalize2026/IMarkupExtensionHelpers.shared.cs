// IMarkupExtensionHelpers.shared.cs

namespace MauiLocalize2026;

/// <summary>
/// Provides helper methods for working with XAML markup extensions, specifically for propagating the BindingContext from the target object to bindable objects.
/// </summary>
public static class IMarkupExtensionHelpers
{
	/// <summary>
	/// Propagates the BindingContext from the target object to the specified bindable object if it is not already set.
	/// This allows the bindable object to inherit the BindingContext of its parent, enabling proper data binding in XAML.
	/// </summary>
	/// <typeparam name="T">The type of the bindable object.</typeparam>
	/// <param name="bindable">The bindable object to propagate the BindingContext to.</param>
	/// <param name="serviceProvider">The service provider used to retrieve the target object.</param>
	/// <returns>The bindable object with the propagated BindingContext.</returns>
	public static T PropagateBindingContext<T>(this T bindable, IServiceProvider serviceProvider) where T : BindableObject
	{
		if (!bindable.IsSet(BindableObject.BindingContextProperty)
			&& serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget provideValueTarget
			&& provideValueTarget.TargetObject is BindableObject targetObject)
		{
			bindable.SetBinding(BindableObject.BindingContextProperty, static (BindableObject b) => b.BindingContext, BindingMode.OneWay, source: targetObject);
		}
		return bindable;
	}
}
