// Program.cs

using UIKit;

namespace MauiLocalize2026.Sample;

/// <summary>
/// The main entry point of the application for the iOS platform.
/// </summary>
public class Program
{
	// This is the main entry point of the application.
	static void Main(string[] args)
	{
		// if you want to use a different Application Delegate class from "AppDelegate"
		// you can specify it here.
		UIApplication.Main(args, null, typeof(AppDelegate));
	}
}
