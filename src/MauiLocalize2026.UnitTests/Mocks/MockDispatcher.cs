// MockDispatcher.cs

using Microsoft.Maui.Dispatching;

namespace MauiLocalize2026.UnitTests.Mocks;

sealed class MockDispatcher : IDispatcher
{
	public bool IsDispatchRequired => false;

	public IDispatcherTimer CreateTimer()
		=> new MockDispatcherTimer(this);

	public bool Dispatch(Action action)
	{
		action();
		return true;
	}

	public bool DispatchDelayed(TimeSpan delay, Action action)
	{
		action();
		return true;
	}

	sealed class MockDispatcherTimer(IDispatcher dispatcher) : IDispatcherTimer, IDisposable
	{
		Timer? timer;

		public TimeSpan Interval { get; set; }

		public bool IsRepeating { get; set; }

		public bool IsRunning => timer is not null;

		public event EventHandler? Tick;

		public void Start()
		{
			timer = new Timer(
				_ => dispatcher.Dispatch(() => Tick?.Invoke(this, EventArgs.Empty)),
				null,
				Interval,
				IsRepeating ? Interval : Timeout.InfiniteTimeSpan);
		}

		public void Stop()
			=> Dispose();

		public void Dispose()
		{
			timer?.Dispose();
			timer = null;
		}
	}
}
