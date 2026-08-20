// MauiProgram.cs

using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using MauiLocalize2026.Resources.Strings;
using Microsoft.Extensions.Logging;

namespace MauiLocalize2026;

/// <summary>
/// 
/// </summary>
public static class MauiProgram
{
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseMauiCommunityToolkitMarkup()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		// Provide the localization method to use for the i18n:Localize markup extension.
		LocalizationManager.Current.LocalizationProvider = AppStrings.ResourceManager.GetString;

		return builder.Build();
	}
}
