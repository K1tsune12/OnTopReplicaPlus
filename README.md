# OnTopReplicaPlus

**A real-time always-on-top “replica” of a window of your choice, for Windows Vista, 7, 8, 10 or 11.**

> **OnTopReplicaPlus** is a fork of [**OnTopReplica**](https://github.com/LorenzCK/OnTopReplica) by
> Lorenz Cuno Klopfenstein. All credit for the original application goes to the original author and
> contributors. This fork adds persistent, portable settings and a few quality-of-life fixes (see
> [What's different in this fork](#whats-different-in-this-fork)).

This simple utility application shows a blank always-on-top window by default.
Users can pick any other window of the system to have an always up-to-date clone of the target window shown always-on-top.
Very useful for monitoring background processes, wrangling with complex multi-window games or tools, watching Youtube videos while working, and so on.

**📢 Features:**

* Clone any of your windows and keep it *always-on-top* while working with other windows,
* Select a subregion of the cloned window, which:
  * Can be stored for future use,
  * Can use relative coordinates from the target window’s borders.
* Auto-resizing (fit the original window, half, quarter and fullscreen mode),
* Position lock on any corner of your screen,
* Adjustable opacity,
* “Click forwarding”: allows to interact with the cloned window,
* “Click-through”: makes the replica ignore any mouse interaction (turns it into an overlay if set together with partial opacity),
* “Group switch”-mode automatically switches through a group of windows while you use them.

## What's different in this fork

* **Persistent, portable settings.** Settings are now stored in a single, human-readable JSON
  file at `%LOCALAPPDATA%\OnTopReplicaPlus\settings.json` instead of the framework's
  `user.config`. The file is easy to inspect, back up or move between machines.
* **Settings actually persist between sessions by default.** Window position/size and the last
  cloned window are now restored automatically (the relevant toggles default to *on*), and the
  current opacity is remembered across launches. The very first run still opens with a sensible
  default-sized window.
* **Click-through fix.** Click-through is no longer silently disabled when the window is brought
  back from the tray or fullscreen (ports [upstream PR #139](https://github.com/LorenzCK/OnTopReplica/pull/139), issue #105).

## Requirements

* Microsoft Windows Vista or greater (the application makes use of native DWM&nbsp;Thumbnails to create replicas),
* Microsoft .NET Framework 4.7,
* Desktop Composition (a.k.a. Windows *Aero*) enabled.

## Building

Open `src/OnTopReplica.sln` in **Visual Studio 2019/2022** (Community edition is fine) with the
*.NET desktop development* workload and the **.NET Framework 4.7 targeting pack** installed, then
build in `Release`. NuGet will restore the `Windows-Forms-Aero` package automatically.

From the command line:

```
msbuild src\OnTopReplica.sln /p:Configuration=Release /p:Platform="Any CPU"
```

The resulting executable is `src/OnTopReplica/bin/Release/OnTopReplicaPlus.exe`.

The optional Windows installer is built from `Installer/script.nsi` using [NSIS](https://nsis.sourceforge.io/).

## License

**OnTopReplicaPlus** is, like the original, licensed under the
[MS-RL (Microsoft Reciprocal License)](LICENSE). The original copyright and attribution notices
are retained as required by the license. This fork is not affiliated with or endorsed by the
original author.
