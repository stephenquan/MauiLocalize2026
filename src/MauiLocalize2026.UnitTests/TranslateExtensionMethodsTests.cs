// TranslateExtensionMethodsTests.cs

using System.Globalization;
using MauiLocalize2026.UnitTests.Resources.Strings;

namespace MauiLocalize2026.UnitTests;

/// <summary>
/// Tests for <see cref="TranslateExtensionMethods"/>.
/// </summary>
public sealed class TranslateExtensionMethodsTests : BaseTest
{
	/// <summary>
	/// Verifies the UI-culture provider overload translates a MAUI label.
	/// </summary>
	/// <param name="cultureName">The UI and formatting culture.</param>
	/// <param name="expected">The expected label text.</param>
	[Theory]
	[InlineData("en-US", "Salutations!")]
	[InlineData("fr-FR", "Salutations !")]
	[InlineData("de-DE", "Grüße!")]
	[InlineData("ja-JP", "挨拶！")]
	public void Translate_UICultureProvider_SetsLabelText(
		string cultureName, string expected)
	{
		// Arrange
		var culture = new CultureInfo(cultureName);
		LocalizationManager.Current.CurrentUICulture = culture;
		LocalizationManager.Current.CurrentCulture = culture;
		var label = new Label();

		// Act
		var result = label.Translate(
			Label.TextProperty,
			(currentUiCulture) => TestStrings.LBL_SALUTATIONS);

		// Assert
		Assert.Same(label, result);
		Assert.Equal(expected, label.Text);
	}

	/// <summary>
	/// Verifies the UI-culture provider overload with arguments translates a MAUI label.
	/// </summary>
	/// <param name="cultureName">The UI and formatting culture.</param>
	/// <param name="expected">The expected label text.</param>
	[Theory]
	[InlineData("en-US", "Clicked 3 times!")]
	[InlineData("fr-FR", "J'ai cliqué 3 fois !")]
	[InlineData("de-DE", "3 Mal angeklickt!")]
	[InlineData("ja-JP", "3 回クリックされました!")]
	public void Translate_UICultureProviderWithArgs_SetsButtonText(
		string cultureName, string expected)
	{
		// Arrange
		var culture = new CultureInfo(cultureName);
		LocalizationManager.Current.CurrentUICulture = culture;
		LocalizationManager.Current.CurrentCulture = culture;
		var button = new Button();

		// Act
		var result = button.Translate(
			Button.TextProperty,
			(currentUiCulture) => TestStrings.BTN_CLICKED_N_TIMES,
			3);

		// Assert
		Assert.Same(button, result);
		Assert.Equal(expected, button.Text);
	}
}
