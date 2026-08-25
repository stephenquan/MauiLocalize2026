// LocalizationManagerTests.cs

using System.Globalization;
using System.Text.Json;
using MauiLocalize2026.UnitTests.Resources.Strings;

namespace MauiLocalize2026.UnitTests;

/// <summary>
/// Tests for <see cref="LocalizationManager"/>.
/// </summary>
public sealed class LocalizationManagerTests : BaseTest
{
	const string localizationJson = """
		{
			"json:LABEL_HELLO": "Hello",
			"json:LABEL_HELLO.fr-FR": "Bonjour",
			"json:LABEL_HELLO.de-DE": "Hallo",
			"json:BUTTON_CLICKED_N_TIMES": "Button clicked {0} times",
			"json:BUTTON_CLICKED_N_TIMES.fr-FR": "Bouton cliqué {0} fois",
			"json:BUTTON_CLICKED_N_TIMES.de-DE": "Schaltfläche {0} Mal angeklickt",
			"json:LABEL_SALES_TAX": "Sales Tax: {0:C}",
			"json:LABEL_SALES_TAX.fr-FR": "Taxe de vente : {0:C}",
			"json:LABEL_SALES_TAX.de-DE": "Umsatzsteuer: {0:C}"
		}
		""";

	/// <summary>
	/// Verifies a JSON-backed provider returns and formats culture-specific values.
	/// </summary>
	/// <param name="json">The JSON localization dictionary.</param>
	/// <param name="key">The localization key.</param>
	/// <param name="jsonArgs">The JSON array of arguments for formatting.</param>
	/// <param name="cultureName">The UI and formatting culture.</param>
	/// <param name="expected">The expected localized and formatted value.</param>
	[Theory]
	[InlineData(localizationJson, "json:LABEL_HELLO", "[]", "en-US", "Hello")]
	[InlineData(localizationJson, "json:LABEL_HELLO", "[]", "fr-FR", "Bonjour")]
	[InlineData(localizationJson, "json:LABEL_HELLO", "[]", "de-DE", "Hallo")]
	[InlineData(localizationJson, "json:BUTTON_CLICKED_N_TIMES", "[3]", "en-US", "Button clicked 3 times")]
	[InlineData(localizationJson, "json:BUTTON_CLICKED_N_TIMES", "[3]", "fr-FR", "Bouton cliqué 3 fois")]
	[InlineData(localizationJson, "json:BUTTON_CLICKED_N_TIMES", "[3]", "de-DE", "Schaltfläche 3 Mal angeklickt")]
	public void GetString_JsonLocalizationProvider_ReturnsCultureSpecificText(
		string json, string key, string jsonArgs, string cultureName, string expected)
	{
		// Arrange
		var localizedStrings = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
		var args = JsonSerializer.Deserialize<object?[]>(jsonArgs);
		Assert.NotNull(localizedStrings);
		Assert.NotNull(args);

		LocalizationManager.Current.LocalizationProvider = (key, culture) =>
			localizedStrings.GetValueOrDefault($"{key}.{culture.Name}")
			?? localizedStrings.GetValueOrDefault(key);
		var culture = CultureInfo.GetCultureInfo(cultureName);

		// Act
		var result = LocalizationManager.Current.GetString(culture, culture, key, args);

		// Assert
		Assert.Equal(expected, result);
	}

	/// <summary>
	/// Verifies a JSON-backed provider returns and formats culture-specific currency values.
	/// </summary>
	/// <param name="json">The JSON localization dictionary.</param>
	/// <param name="key">The localization key.</param>
	/// <param name="tax">The currency value to format.</param>
	/// <param name="cultureName">The UI and formatting culture.</param>
	/// <param name="expected">The expected localized and formatted value.</param>
	[Theory]
	[InlineData(localizationJson, "json:LABEL_SALES_TAX", 12.5, "en-US", "Sales Tax: $12.50")]
	[InlineData(localizationJson, "json:LABEL_SALES_TAX", 12.5, "en-GB", "Sales Tax: £12.50")]
	[InlineData(localizationJson, "json:LABEL_SALES_TAX", 12.5, "en-HK", "Sales Tax: HK$12.50")]
	[InlineData(localizationJson, "json:LABEL_SALES_TAX", 12.5, "fr-FR", "Taxe de vente : 12,50 €")]
	[InlineData(localizationJson, "json:LABEL_SALES_TAX", 12.5, "de-DE", "Umsatzsteuer: 12,50 €")]
	[InlineData(localizationJson, "json:LABEL_SALES_TAX", 12.5, "ja-JP", "Sales Tax: ￥13")]
	public void GetString_JsonLocalizationProvider_ReturnsCultureSpecificCurrencyText(
		string json, string key, decimal tax, string cultureName, string expected)
	{
		// Arrange
		var localizedStrings = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
		Assert.NotNull(localizedStrings);
		LocalizationManager.Current.LocalizationProvider = (key, culture) =>
			localizedStrings.GetValueOrDefault($"{key}.{culture.Name}")
			?? localizedStrings.GetValueOrDefault(key);
		var culture = CultureInfo.GetCultureInfo(cultureName);

		// Act
		var result = LocalizationManager.Current.GetString(culture, culture, key, tax);

		// Assert
		Assert.Equal(expected, result);
	}

	/// <summary>
	/// Verifies a resource-backed provider returns and formats culture-specific values.
	/// </summary>
	/// <param name="key">The localization key.</param>
	/// <param name="jsonArgs">The JSON array of arguments for formatting.</param>
	/// <param name="cultureName">The UI and formatting culture.</param>
	/// <param name="expected">The expected localized and formatted value.</param>
	[Theory]
	[InlineData("LBL_SALUTATIONS", "[]", "en-US", "Salutations!")]
	[InlineData("LBL_SALUTATIONS", "[]", "fr-FR", "Salutations !")]
	[InlineData("LBL_SALUTATIONS", "[]", "de-DE", "Grüße!")]
	[InlineData("LBL_SALUTATIONS", "[]", "ja-JP", "挨拶！")]
	public void GetString_TestStringsLocalizationProvider_ReturnsCultureSpecificText(
		string key, string jsonArgs, string cultureName, string expected)
	{
		// Arrange
		var args = JsonSerializer.Deserialize<object?[]>(jsonArgs);
		Assert.NotNull(args);

		LocalizationManager.Current.LocalizationProvider = TestStrings.ResourceManager.GetString;
		var culture = CultureInfo.GetCultureInfo(cultureName);

		// Act
		var result = LocalizationManager.Current.GetString(culture, culture, key, args);

		// Assert
		Assert.Equal(expected, result);
	}

	/// <summary>
	/// Verifies an unconfigured provider produces no localized value.
	/// </summary>
	[Fact]
	public void GetString_NullLocalizationProvider_ReturnsNull()
	{
		// Arrange
		LocalizationManager.Current.LocalizationProvider = null;

		// Act
		var result = LocalizationManager.Current.GetString("Missing");

		// Assert
		Assert.Null(result);
	}
}
