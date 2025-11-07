# ContextMenuSizeChanger

A [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader) mod for [Resonite](https://resonite.com/) that allows you to customize the size of context menus in-game.

This mod patches the ContextMenu's OnChanges method to replace the hardcoded size multiplier (0.85) with a configurable value, allowing players to adjust context menu sizes between 0.1 and 2.0x of the default size.

## Configuration

The mod provides a configuration option `MenuSize` that controls the context menu size multiplier:
- **Default**: 0.85
- **Range**: 0.1 to 2.0
- Any values outside this range will be automatically clamped to valid limits

## Installation
1. Install [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader).
1. Place [ContextMenuSizeChanger.dll](https://github.com/nalathethird/ContextMenuSizeChanger/releases/latest/download/ContextMenuSizeChanger.dll) into your `rml_mods` folder. This folder should be at `C:\Program Files (x86)\Steam\steamapps\common\Resonite\rml_mods` for a default install. You can create it if it's missing, or if you launch the game once with ResoniteModLoader installed it will create this folder for you.
1. Start the game. If you want to verify that the mod is working you can check your Resonite logs.
