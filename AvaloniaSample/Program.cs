﻿using System;
using Avalonia;
using Avalonia.Headless;

namespace AvaloniaSample
{
    class Program
    {
        public static void Main(string[] args) 
        {
            try
            {
                BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseSkia()
                .UseHeadless(false)
                .LogToTrace();
    }
}
