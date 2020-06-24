# Orange Notes
Sticky notes in C# with Google Drive:
 
![image](https://user-images.githubusercontent.com/62397363/79151951-ab0fe480-7dcb-11ea-9e84-12433661bdc2.png)

## Features:
- Save notes on your desktop
- Store notes in .json file either locally or on Google Drive
- Run at startup (optional)
- Doesn't take up any space on your taskbar

## Prerequisites:
- .NET Core 3.1
- Visual Studio 2019
- (optional) "credentials.json" file to access Google Drive API (see: https://developers.google.com/drive/api/v3/about-auth):
   ```
    {
      "installed": {
        "client_id": "xxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx.apps.googleusercontent.com",
        "client_secret": "xxxxxxxxxxxxxxxxxxxxxxxx"
      }
    }
   ```

## Credits:
- Google Drive API client library by https://www.nuget.org/packages/Google.Apis.Drive.v3/ (Apache-2.0 license)
- Menu icons by https://ionicons.com/ (MIT license)
- Loading spinners by https://github.com/blackspike/Xaml-Spinners-WPF (MIT license)
