// MockDispatcherProvider.cs

using Microsoft.Maui.Dispatching;

namespace MauiLocalize2026.UnitTests.Mocks;

sealed class MockDispatcherProvider : IDispatcherProvider, IDisposable
{
	readonly ThreadLocal<IDispatcher> dispatcherInstance = new(static () => new MockDispatcher());

	public IDispatcher GetForCurrentThread()
		=> dispatcherInstance.Value ?? throw new InvalidOperationException();

	public void Dispose()
		=> dispatcherInstance.Dispose();
}
