// AppDelegate.cs

using Foundation;

namespace MauiLocalize2026.Sample;

/// <summary>
/// Represents the application delegate for the Mac Catalyst platform.
/// </summary>
[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	/// <summary>
	/// Creates the MAUI application for the Mac Catalyst platform.
	/// </summary>
	/// <returns>The created MAUI application.</returns>
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
