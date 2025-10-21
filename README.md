# Modern Browser - C# WPF with CefSharp

A modern, lightweight browser built with C# WPF and CefSharp (Chromium Embedded Framework).

## Features

- **Modern UI Design**: Clean interface with rounded corners and smooth interactions

- **Navigation Controls**: 
  - Back/Forward buttons with state management
  - Refresh button
  - Home button (defaults to Google)
- **Smart Address Bar**: 
  - Automatically adds `https://` protocol when needed
  - Searches Google for non-URL queries
  - Displays current URL with lock icon
- **Status Bar**: Shows loading state and status messages
- **Loading Indicator**: Visual feedback during page loads
- **Tab Management**: Window title updates with current page title

## Requirements

- Windows OS (WPF is Windows-only)
- .NET 8.0 or higher
- Visual Studio 2022 (recommended) or .NET SDK

## Getting Started

### Build & Run

1. Open the solution file `browser.sln` in Visual Studio
2. Restore NuGet packages (should happen automatically)
3. Build the solution (Ctrl+Shift+B)
4. Run the application (F5)

### Using Command Line

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

### Change Home Page

Edit `Browser.xaml.cs`:
```csharp
private const string HomeUrl = "https://url.com";
```

### Modify UI Colors

Edit the hex color values in `Browser.xaml` for borders, backgrounds, and button colors.

## License

This is an educational project. Feel free to use and modify as needed.
