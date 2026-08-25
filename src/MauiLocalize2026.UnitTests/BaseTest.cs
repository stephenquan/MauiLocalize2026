// BaseTest.cs

using System.Globalization;
using MauiLocalize2026.UnitTests.Mocks;
using Microsoft.Maui.Dispatching;

namespace MauiLocalize2026.UnitTests;

/// <summary>
/// Provides shared .NET MAUI and localization state for unit tests.
/// </summary>
public abstract class BaseTest : IDisposable
{
	readonly Func<string, CultureInfo, string?>? originalLocalizationProvider;
	readonly CultureInfo originalCurrentCulture;
	readonly CultureInfo originalCurrentUICulture;
	readonly MockDispatcherProvider dispatcherProvider;

	bool isDisposed;

	/// <summary>
	/// Initializes the MAUI dispatcher and captures shared localization state.
	/// </summary>
	protected BaseTest()
	{
		originalLocalizationProvider = LocalizationManager.Current.LocalizationProvider;
		originalCurrentCulture = CultureInfo.CurrentCulture;
		originalCurrentUICulture = CultureInfo.CurrentUICulture;

		DispatcherProvider.SetCurrent(dispatcherProvider = new MockDispatcherProvider());
	}

	/// <summary>
	/// Restores shared state after each test.
	/// </summary>
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Restores shared state and releases the mock dispatcher provider.
	/// </summary>
	/// <param name="disposing">Whether managed resources should be released.</param>
	protected virtual void Dispose(bool disposing)
	{
		if (isDisposed)
		{
			return;
		}

		if (disposing)
		{
			LocalizationManager.Current.LocalizationProvider = originalLocalizationProvider;
			CultureInfo.CurrentCulture = originalCurrentCulture;
			CultureInfo.CurrentUICulture = originalCurrentUICulture;
			DispatcherProvider.SetCurrent(null);
			dispatcherProvider.Dispose();
		}

		isDisposed = true;
	}
}
