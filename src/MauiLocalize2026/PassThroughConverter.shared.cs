// PassThroughConverter.shared.cs

using System.Globalization;

namespace MauiLocalize2026;

/// <summary>
/// A converter that passes through the input values as an array without any transformation.
/// This is useful in scenarios where you want to bind multiple values to a single target property without modifying them.
/// </summary>
public class PassThroughConverter : IMultiValueConverter
{
	/// <summary>
	/// Converts the input values to an array without any transformation.
	/// </summary>
	/// <param name="values">The input values to be passed through.</param>
	/// <param name="targetType">The type of the binding target property.</param>
	/// <param name="parameter">The converter parameter to use.</param>
	/// <param name="culture">The culture to use in the converter.</param>
	/// <returns>An array containing the input values.</returns>
	public object? Convert(object?[] values, Type targetType, object? parameter, CultureInfo culture)
	{
		return values;
	}

	/// <summary>
	/// Converts back the array of values to the original input values.
	/// This method is not implemented and will throw a NotImplementedException if called.
	/// </summary>
	/// <param name="value">The value produced by the binding target.</param>
	/// <param name="targetTypes">The array of types of the binding target properties.</param>
	/// <param name="parameter">The converter parameter to use.</param>
	/// <param name="culture">The culture to use in the converter.</param>
	/// <returns>The original input values.</returns>
	/// <exception cref="NotImplementedException"></exception>
	public object[] ConvertBack(object value, Type[] targetTypes, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
