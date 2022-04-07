using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using Aquality.Selenium.Core.Logging;

namespace FrameworkExtensions.Helpers
{
    public class EdgeDriverDownloader
    {
        private const string StorageUrl = @"https://msedgedriver.azureedge.net";

        public static string GetPath(string relativePath) => AppDomain.CurrentDomain.BaseDirectory + relativePath;

        public static void DownloadArchive(bool is64Os, string browserVersion)
        {
            var osBitRate = is64Os ? "64" : "32";
            var driverName = $"edgedriver_win{osBitRate}.zip";
            var requestString = $"{StorageUrl}/{browserVersion}/{driverName}";

            new WebClient().DownloadFile(new Uri(requestString), GetPath(driverName));
            Logger.Instance.Info($"Edge driver v{browserVersion} for {osBitRate} windows downloaded");

            ExtractAndRenameDriver(driverName);
        }

        private static void ExtractAndRenameDriver(string archiveName)
        {
            ZipFile.OpenRead(GetPath($"./{archiveName}")).ExtractToDirectory(GetPath("."));
            File.Move(GetPath("./msedgedriver.exe"), GetPath("./MicrosoftWebDriver.exe"));
            Logger.Instance.Info("The Edge driver extracted");
        }

    }
}