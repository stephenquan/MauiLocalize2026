// MainApplication.cs

using Android.App;
using Android.Runtime;

namespace MauiLocalize2026;

/// <summary>
/// 
/// </summary>
[Application]
public class MainApplication : MauiApplication
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="handle"></param>
	/// <param name="ownership"></param>
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
	}

	/// <inheritdoc/>
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
