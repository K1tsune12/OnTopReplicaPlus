# OnTopReplicaPlus

A real-time, always-on-top clone of any window you choose. For Windows Vista, 7, 8, 10 and 11.

A fork of [OnTopReplica](https://github.com/LorenzCK/OnTopReplica) by Lorenz Cuno Klopfenstein. All credit for the original app goes to its author. This fork adds saved settings, a dark theme, and some quality-of-life fixes.

## What it does
Pick any window and OnTopReplicaPlus shows a live, always-on-top copy of it. Handy for watching a video or a background task while you work.

## Features
- Clone any window and keep it always on top.
- Crop to a region, and save regions for later.
- Auto-resize and lock to any screen corner.
- Adjustable opacity.
- Click-forwarding (interact with the clone) and click-through (use it as an overlay).
- Group-switch mode that cycles through a set of windows.

## New in this fork
- Settings saved automatically and kept between sessions.
- Light and dark theme.
- Optional system tray icon.
- Keeps cloned browser videos playing instead of freezing when you switch windows.
- Cycle saved regions with a hotkey (Ctrl+Shift+R), double-click to jump to the source window.
- Launch on a chosen monitor with /screen=N.
- Full Brazilian Portuguese translation.

## Install
Download the latest release from the [Releases page](https://github.com/K1tsune12/OnTopReplicaPlus/releases):
- Setup.zip - run the installer.
- portable.zip - extract and run the .exe, no installation needed.

## Build
Open `src/OnTopReplicaPlus.sln` in Visual Studio 2019 or 2022 (Community is fine) with the ".NET desktop development" workload, then build in Release.

## License
Licensed under the [MS-RL](LICENSE), like the original. Not affiliated with or endorsed by the original author.
