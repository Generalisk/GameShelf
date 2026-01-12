<div align="center">
  
  [![Commit Activity](https://img.shields.io/github/commit-activity/w/Generalisk/GameShelf)](https://github.com/Generalisk/GameShelf)
  [![Commit Activity](https://img.shields.io/github/commit-activity/m/Generalisk/GameShelf)](https://github.com/Generalisk/GameShelf)
  [![Commit Activity](https://img.shields.io/github/commit-activity/y/Generalisk/GameShelf)](https://github.com/Generalisk/GameShelf)
  [![Commit Activity](https://img.shields.io/github/commit-activity/t/Generalisk/GameShelf)](https://github.com/Generalisk/GameShelf)
  
  [![Version](https://img.shields.io/github/v/release/Generalisk/GameShelf)](https://github.com/Generalisk/GameShelf/releases/latest)
  [![Release Date](https://img.shields.io/github/release-date/Generalisk/GameShelf)](https://github.com/Generalisk/GameShelf/releases/latest)
  [![Commits since Latest Release](https://img.shields.io/github/commits-since/Generalisk/GameShelf/latest)](https://github.com/Generalisk/GameShelf/releases/latest)
  
  [![License](https://img.shields.io/github/license/Generalisk/GameShelf)](https://github.com/Generalisk/GameShelf/blob/main/LICENSE)
  [![Issues](https://img.shields.io/github/issues/Generalisk/GameShelf)](https://github.com/Generalisk/GameShelf/issues)
  [![File Size](https://img.shields.io/github/repo-size/Generalisk/GameShelf)](https://github.com/Generalisk/GameShelf)
  [![Last Commit](https://img.shields.io/github/last-commit/Generalisk/GameShelf)](https://github.com/Generalisk/GameShelf)
</div>

<div align="center">

  [![Windows](https://github.com/Generalisk/GameShelf/actions/workflows/build-windows.yml/badge.svg)](https://github.com/Generalisk/GameShelf/actions/workflows/build-windows.yml)
  [![Linux](https://github.com/Generalisk/GameShelf/actions/workflows/build-linux.yml/badge.svg)](https://github.com/Generalisk/GameShelf/actions/workflows/build-linux.yml)
</div>

<div align="center">
  
  # GameShelf
</div>

GameShelf is a one-stop hub for all of your games, all put together in one launcher. GameShelf gathers all the games you own across dozens of different storefronts and puts them all under one launcher, allowing you to quickly access & play all of your games in one spot.

## CONTENTS
- [Supported Storefronts](#supported-storefronts)
- [Requirements](#requirements)
- [Build Instructions](#build-instructions)
- [License](#license)

## SUPPORTED PLATFORMS
- Windows
- Linux
<!--Sorry, Mac users :( -->

> [!NOTE]
> Mac support may be added in the future, but compatibility will be very limited due to it's incompatibility with certain packages.
<!--Mainly the GameCollector package, which handles communication with the Storefronts installed on your device-->

<!--I'm not providing links to them all, go find them yourself -_- -->
## SUPPORTED STOREFRONTS
- Steam
- Itch.io Desktop App
- GOG Galaxy
- GameJolt Client
- Epic Games Store
- Xbox Game Pass (Windows only)
- Origin
- EA Desktop (Windows only)
- Blizzard Battle.net
- Ubisoft Connect
- Rockstar Games Launcher
- Riot Client
- Amazon Games
- Oculus
- Legacy Games Launcher
- Indiegala IGClient
- Paradox Launcher
- Plarium Play
- Arc
- Big Fish Game Manager
- Humble App
- RobotCache Client
- Wargaming.net Game Center
<!--NGL I didn't know half of these existed until I saw them on the GameCollector supported list-->

## REQUIREMENTS
- [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
### Windows (Alternative)
- [Microsoft Visual Studio 2022](https://visualstudio.microsoft.com/vs/older-downloads/#visual-studio-2022-and-other-products) with the following workflows & components:
  - `.NET desktop development` workflow
  - `.NET 8.0 Runtime`
  - `NuGet package manager`
  - `NuGet targets and build tasks`
<!--I don't know all the components you'll actually need, i'm just guessing :P
Feel free to correct me or add any that i'm missing-->

## BUILD INSTRUCTIONS
To build the project, simply go to the [scripts](scripts) folder & run the appropriate Batch/Shell scripts.

You can build in two modes: Debug & Release. You can use the main script to build the project for all supported platforms, or use platform-specific scripts to build them individually.

Additionally, there is also a test script, which will create a debug build and then automatically launch the program once it's finished, alongside a publish script, which will create a release build with all .NET dependencies built-in alongside additional optimizations (intended for distribution).
<!--I haven't tested the Linux scripts yet XD-->
### Using Visual Studio (Windows only)
In Visual Studio, go to the top menu & open the `Build` menu. There, you can pick on whether to build the solution or just the current project.

Alternatively, you can use the `Ctrl + Shift + B` and `Ctrl + B` shortcuts to build the solution and current project respectively.

#### Debugging using Visual Studio
To debug using Visual Studio, go an click on the green arrow with the text `GameShelf` next to it or press `F5`.

## LICENSE
This repository is licenced under the `Apache 2.0 License`, which is contained in the [LICENSE](LICENSE) file at the root of the repository.
