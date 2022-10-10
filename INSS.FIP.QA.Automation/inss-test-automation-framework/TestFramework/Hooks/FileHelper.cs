﻿using System;
using System.IO;
using TestFramework.Hooks;

namespace TestFramework.Hooks
{
    public class FileHelper
    {
        private static string ValidateFolder(string folder)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(folder)) folder = Path.GetTempPath();
                Console.WriteLine($"Download path:{folder}");
                return folder;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static string GetDownloadFolder()
        {
            var downloadFolder = WebDriverFactory.Config["DownloadFolder"];
            return ValidateFolder(downloadFolder);
        }
    }
}