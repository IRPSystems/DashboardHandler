using Controls.Views;
using ControlzEx.Theming;
using DashboardHandler.Views;
using Services.Services;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace DashboardHandler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		public App()
		{
			Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
				 "MzM3MDg2M0AzMjM0MmUzMDJlMzBCT2dsKzBPUW9HbXFrM1J3aWxQR2k5UDVOZXNDdE4zdGJCSjI5N2lpWGlJPQ==");

			this.DispatcherUnhandledException += App_DispatcherUnhandledException;


		}

		protected override void OnStartup(StartupEventArgs e)
		{
			System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

			base.OnStartup(e);

			SplashView splash = new SplashView();
			splash.AppName = "Dashboard Handler";
			splash.Show();

			bool isAppRunning = IsAppRunning();
			if (isAppRunning)
			{
				MessageBox.Show("The application is already running");
				Current.Shutdown();
				return;
			}

			// Right now I'm showing main window right after splash screen but I will eventually wait until splash screen closes.
			MainWindow = new DashboardHandlerMainView();
			MainWindow.Show();
			splash.Close();
		}

		private bool IsAppRunning()
		{
			Process current = Process.GetCurrentProcess();
			Process[] processes = Process.GetProcessesByName(current.ProcessName);

			//Loop through the running processes in with the same name 
			foreach (Process process in processes)
			{
				//Ignore the current process 
				if (process.Id != current.Id)
				{
					return true;
				}
			}

			return false;
		}

		public static void ChangeDarkLight(bool isLightTheme)
		{
			if (Current == null)
				return;

			if (isLightTheme)
				ThemeManager.Current.ChangeTheme(Current, "Light.Cobalt");
			else
				ThemeManager.Current.ChangeTheme(Current, "Dark.Cobalt");
		}



		private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			LoggerService.Error("Un-handled exception caught", "Error", e.Exception);
			e.Handled = true;
		}
	}

}
