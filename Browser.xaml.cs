using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CefSharp;
using CefSharp.Wpf;

namespace browser
{
    /// <summary>
    /// Interaction logic for Browser.xaml
    /// </summary>
    public partial class Browser : Window
    {
        private const string HomeUrl = "https://www.google.com";

        public Browser()
        {
            InitializeComponent();
            InitializeBrowser();
        }

        private void InitializeBrowser()
        {
            // Set initial URL
            BrowserControl.Address = HomeUrl;

            // Subscribe to browser events
            BrowserControl.LoadingStateChanged += BrowserControl_LoadingStateChanged;
            BrowserControl.AddressChanged += BrowserControl_AddressChanged;
            BrowserControl.TitleChanged += BrowserControl_TitleChanged;
            BrowserControl.StatusMessage += BrowserControl_StatusMessage;

            // Set focus to address bar
            Loaded += (s, e) => AddressBar.Focus();
        }

        private void BrowserControl_LoadingStateChanged(object? sender, LoadingStateChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                // Update navigation buttons
                BackButton.IsEnabled = e.CanGoBack;
                ForwardButton.IsEnabled = e.CanGoForward;

                // Update loading progress
                if (e.IsLoading)
                {
                    LoadingProgress.Visibility = Visibility.Visible;
                    LoadingProgress.IsIndeterminate = true;
                    StatusText.Text = "Loading...";
                }
                else
                {
                    LoadingProgress.Visibility = Visibility.Collapsed;
                    StatusText.Text = "Ready";
                }
            });
        }

        private void BrowserControl_AddressChanged(object? sender, DependencyPropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                AddressBar.Text = e.NewValue?.ToString() ?? string.Empty;
            });
        }

        private void BrowserControl_TitleChanged(object? sender, DependencyPropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Title = e.NewValue?.ToString() ?? "Modern Browser";
            });
        }

        private void BrowserControl_StatusMessage(object? sender, StatusMessageEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                StatusText.Text = string.IsNullOrEmpty(e.Value) ? "Ready" : e.Value;
            });
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (BrowserControl.CanGoBack)
            {
                BrowserControl.Back();
            }
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (BrowserControl.CanGoForward)
            {
                BrowserControl.Forward();
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            BrowserControl.Reload();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            BrowserControl.Load(HomeUrl);
        }

        private void AddressBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NavigateToUrl(AddressBar.Text);
            }
        }

        private void NavigateToUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return;

            // Add https:// if no protocol is specified
            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                // Check if it looks like a domain (contains a dot)
                if (url.Contains(".") && !url.Contains(" "))
                {
                    url = "https://" + url;
                }
                else
                {
                    // Otherwise, treat it as a search query
                    url = $"https://www.google.com/search?q={Uri.EscapeDataString(url)}";
                }
            }

            BrowserControl.Load(url);
        }
    }
}
