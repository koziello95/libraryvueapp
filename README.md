# libraryvueapp
Library app made as recruitment assignment
## Prerequisites:

* Microsoft Visual Studio 2019

* Extension for VS - * Vue JS 3.0 with NET 5 Web API available here https://marketplace.visualstudio.com/items?itemName=alexandredotnet.vuejsdotnetfive Please install and enable this extension

* MS SQLServer - please edit your connection string accordingly in libraryapp\libraryVueApp\appsettings.json

## Startup

1. Open solution in MS Visual Studio
2. Open View/OtherWindows/Package Manager Console
3. Run "dotnet ef database update" in Package Manager Console
4. Hit F5 or right-click on libraryVueApp -> Debug -> Start new instance:

 * all JS scripts will be build accordingly
 * web api will start running

In case you app does not work out of the box please update port application is running on in file
 \libraryapp\libraryVueApp\Properties\launchSettings.json
