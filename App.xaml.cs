using System.Configuration;
using System.Data;
using System.Windows;
using CefSharp;
using CefSharp.Wpf;
using System.IO;
using System;

namespace browser;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        var settings = new CefSettings()
        {
            CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BrowserCache"),
 
        };

        // Perform dependency check to make sure all relevant resources are in our output directory
        Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Cef.Shutdown();
        base.OnExit(e);
    }
}

