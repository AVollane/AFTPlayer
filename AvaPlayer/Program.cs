using AFTClient;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace AvaPlayer
{
    class Program
    {
        private static Receiver receiver;
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args) 
        {
            string? ftpHostIP = ConfigurationManager.AppSettings.Get("FTPHostIP");
            string? ftpUsername = ConfigurationManager.AppSettings.Get("FTPUsername");
            string? ftpPassword = ConfigurationManager.AppSettings.Get("FTPPassword");
            receiver = new Receiver(ftpHostIP, ftpUsername, ftpPassword);

            int recurrences;
            string? recurrencesStr = ConfigurationManager.AppSettings.Get("ConnectionsRecurrences");
            if(Int32.TryParse(recurrencesStr, out recurrences))
            {
                recurrences = 5;
            }

            int millisecondsDelay;
            string? millisecondsDelayStr = ConfigurationManager.AppSettings.Get("ConnectionMillisecondsDelay");
            if(Int32.TryParse(millisecondsDelayStr, out millisecondsDelay))
            {
                millisecondsDelay = 5000;
            }
            TryConnect(recurrences, millisecondsDelay);

            if (receiver.IsConnected)
            {
                string? remoteDirectory = ConfigurationManager.AppSettings.Get("RemoteDirectory");
                string? localDirectory = ConfigurationManager.AppSettings.Get("LocalDirectory");
                RemoveOldFiles(localDirectory);
                receiver.DownloadFiles(remoteDirectory, localDirectory);
            }

            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        private static void TryConnect(int recurrences, int millisecondsDelay)
        {
            if (recurrences == 0)
                recurrences = 5;
            if (millisecondsDelay == 0)
                millisecondsDelay = 5000;

            for (int i = 0; i < recurrences; i++)
            {
                try
                {
                    receiver.Connect();
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(millisecondsDelay);
                }
            }
        }

        private static void RemoveOldFiles(string? localDirectory)
        {
            try
            {
                string[] filesInLocalDir = Directory.GetFiles(localDirectory);
                foreach (string file in filesInLocalDir)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .With(new SkiaOptions { MaxGpuResourceSizeBytes = 8096000})
                .LogToTrace()
                .UseSkia()
                .UseReactiveUI();
    }
}
