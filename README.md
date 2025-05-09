API Wallpaper Changer

Simple C# console program that fetches wallpapers from any specified API and sets it as your Windows desktop wallpaper at specified intervals. It resizes images to your display resolution and ignores portrait-sized images by default. Ideal for automating wallpaper updating from dynamic sources like Unsplash, Pexels, or even your personal APIs.





Features

Automatically fetches and sets wallpaper from any configurable API.

Scales images to match your screen resolution.

Skips portrait images for a good desktop fit.

Runs continuously in a loop, refreshing every 30 seconds (configurable).








Prerequisites

Windows 10 or later.

.NET 6.0 SDK or later.

Visual Studio 2022 (ideal) or Visual Studio Code.

Internet connection.







Installation

Clone this repository:
git clone https://github.com/yourusername/ApiWallpaperChanger.git
cd ApiWallpaperChanger

Install required NuGet packages:
dotnet add package Newtonsoft.Json









Configuration

Replace "Api Url Here" in the Program.cs file with the real URL of the API serving image data. Make sure the API returns a JSON object containing an url field.

Building and Running (Visual Studio)

Open the project folder in Visual Studio.

Restore NuGet packages if prompted.

Build the project (Ctrl+Shift+B) to make sure everything is set up correctly.

Run the project (F5) or build and run using the command:

dotnet run







Customization

Adjust the update interval by modifying the Task.Delay(30000) line in the Main method.

Add error logging or exception handling for more robustness.

Consider adding multiple screen support or custom cropping as needed.

Contributing

Feel free to report issues, fork the project, and submit pull requests to improve this utility.

License

This project is licensed under the MIT License. See the LICENSE file for details.
