// App.xaml.cs

namespace MauiLocalize2026.Sample;

/// <summary>
/// MAUI application class.
/// </summary>
public partial class App : Application
{
	/// <summary>
	/// Initializes a new instance of the <see cref="App"/> class.
	/// </summary>
	public App()
	{
		InitializeComponent();
	}

	/// <summary>
	/// Creates the main window for the application.
	/// </summary>
	/// <param name="activationState">The activation state.</param>
	/// <returns>The main window.</returns>
	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}
