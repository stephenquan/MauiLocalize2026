// App.xaml.cs

namespace MauiLocalize2026;

/// <summary>
/// 
/// </summary>
public partial class App : Application
{
	/// <summary>
	/// 
	/// </summary>
	public App()
	{
		InitializeComponent();
	}

	/// <inheritdoc />
	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}