// MauiProgram.cs

using CommunityToolkit.Maui;
using MauiLocalize2026.Sample.Resources.Strings;
using Microsoft.Extensions.Logging;

namespace MauiLocalize2026.Sample;

/// <summary>
/// Represents the Maui program class that configures and creates the MAUI application.
/// </summary>
public static class MauiProgram
{
	/// <summary>
	/// Creates and configures the MAUI application.
	/// </summary>
	/// <returns>The configured MAUI application.</returns>
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
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
