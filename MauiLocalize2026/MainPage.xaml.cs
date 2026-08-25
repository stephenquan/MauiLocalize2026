// MainPage.xaml.cs

using System.Globalization;
using CommunityToolkit.Maui;
using MauiLocalize2026.Resources.Strings;

namespace MauiLocalize2026;

/// <summary>
/// Demos localization applied to the .NET MAUI starter application template.
/// </summary>
public partial class MainPage : ContentPage
{
	/// <summary>
	/// Gets or sets the count of button clicks.
	/// </summary>
	[BindableProperty]
	public partial int Count { get; set; } = 0;

	List<CultureInfo> uiCultures = new List<CultureInfo>
	{
		new CultureInfo("en-US"),
		new CultureInfo("fr-FR"),
		new CultureInfo("de-DE"),
		new CultureInfo("zh-CN"),
		new CultureInfo("ar-SA"),
	};
	int cultureIndex = 0;

	/// <summary>
	/// Initializes a new instance of the <see cref="MainPage"/> class.
	/// </summary>
	public MainPage()
	{
		LocalizationManager.Current.CurrentUICulture = uiCultures[cultureIndex];

		BindingContext = this;

		InitializeComponent();

		// Binding to Count argument in C# works too
		//CounterBtn.Translate(
		//	Button.TextProperty,
		//	_ => AppStrings.BUTTON_CLICKED_N_TIMES,
		//	BindingBase.Create(static (MainPage p) => p.Count, BindingMode.OneWay));

		CounterBtn.SetBinding(
			Button.TextProperty,
			TranslateBindingBase.Create(
				_ => AppStrings.BUTTON_CLICKED_N_TIMES,
				BindingBase.Create(static (MainPage p) => p.Count, BindingMode.OneWay)));

		this.Translate(
			FlowDirectionProperty,
			uiCulture => uiCulture?.TextInfo.IsRightToLeft == true ? FlowDirection.RightToLeft : FlowDirection.LeftToRight);

		DotNetBot.Translate(
			Image.ScaleXProperty,
			uiCulture => uiCulture?.TextInfo.IsRightToLeft == true ? 1 : -1);
	}

	/// <summary>
	/// Increments the click count and updates the button text based on the number of clicks.
	/// </summary>
	/// <param name="sender">The button that was clicked.</param>
	/// <param name="e">The event data.</param>
	void OnCounterClicked(object? sender, EventArgs e)
	{
		Count++;

		if (Count == 1)
		{
			CounterBtn.Translate(Button.TextProperty, _ => AppStrings.BUTTON_CLICKED_1_TIME);
		}
		else
		{
			CounterBtn.Translate(Button.TextProperty, _ => AppStrings.BUTTON_CLICKED_N_TIMES, Count);
		}

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	/// <summary>
	/// Cycles through the available UI cultures and updates the application's UI culture when the culture button is clicked.
	/// </summary>
	/// <param name="sender">The button that was clicked.</param>
	/// <param name="e">The event data.</param>
	void OnToggleCultureClicked(object sender, EventArgs e)
	{
		cultureIndex = (cultureIndex + 1) % uiCultures.Count;
		LocalizationManager.Current.CurrentUICulture = uiCultures[cultureIndex];
	}
}
