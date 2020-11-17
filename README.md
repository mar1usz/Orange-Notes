# Orange Notes
Sticky notes using [Google Drive API](https://developers.google.com/drive) in C# WPF .NET Core:<br/>![image](https://user-images.githubusercontent.com/62397363/87773147-04e8d600-c823-11ea-99ea-fb0ab7e21323.png)

## Features:
- Save notes on your desktop
- Store notes in .json file either locally or on Google Drive
- Run at startup (optional)
- Doesn't take up any space on your taskbar

## Prerequisites:
- .NET Core 3.1
- Visual Studio 2019
- (optional) credentials.json to access Google Drive API (see: https://developers.google.com/drive/api/v3/about-auth):
```
{
    "installed": {
        "client_id": "xxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx.apps.googleusercontent.com",
        "client_secret": "xxxxxxxxxxxxxxxxxxxxxxxx"
    }
}
```

## Credits:
- DeskNotes by https://github.com/dabager/DeskNotes
- Google APIs Client Library for working with Drive v3 by https://www.nuget.org/packages/Google.Apis.Drive.v3 (Apache-2.0 license)
- Ionicons by https://github.com/ionic-team/ionicons (MIT license)
- Free WPF loading spinners by https://github.com/blackspike/Xaml-Spinners-WPF (MIT license)
