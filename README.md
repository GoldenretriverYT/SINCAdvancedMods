# SINCAdvancedMods
SINCAdvancedMods lets you easily install advanced code mods for Software Inc using [MelonLoader](https://github.com/LavaGang/MelonLoader/) - a Unity Mono & IL2Cpp mod loader. It allows for limitless access to the games internals - even with Harmony support thanks to MelonLoader. This tool is meant to streamline the MelonLoader installation and to streamline the process of installing mods.

### hi guyz how 2 instal mod?
1. Download and extract SINCAdvancedMods (from now on "SINCAm")
2. Run SINCAm and press the "Install Loader/Sync" button on the top right corner, SINCAm will prompt you to select your games installation. You can get that path by clicking on the cogwheel in your steam library and pressing "Open local path".
3. Once the loader installation is finished (you will see "Synced mods!" at the bottom status bar), you can click "Install from zip file"
4. In the opened file dialog, select a valid zipped mod (an example is provided in the release section (not actually, this is wip))
5. You are done!

### pls tell how 2 make mod
1. The general modding process is the usual MelonLoader Modding Process, you can refer to this [documentation](https://melonwiki.xyz/#/modders/quickstart).
2. Create a new folder and call it "files" - put your mod files in there.
3. Create a new file NEXT to the "files" folder and call it "meta.json"
4. Fill it with the appropriate information following the ModMeta schema.
5. Zip the files - make sure you dont have your files within a subfolder in the zip archive - they have to be at the root of the zip file.

### ModMeta Schema
```json
{
    "ModDisplayName": "string",
    "ModFolderName": "string",
    "ModType": "string enum: Mod, UserLib"
}
```

## Future Plans
Future plans are: 
  - Install directly from a Git repository
  - Install multiple from single zip, basically multiple plugins/mods/userlibs bundled within one zip file
  - Public mods registry
  - Update mods directly
  - Maybe even provide some basic libraries to make modding easier

# Notes

This is based on one of my previous projects, [Plugins for Polytoria Creator](https://github.com/GoldenretriverYT/PluginsForPolytoriaCreator) which is archived due to an upcoming change in the game engine Polytoria will use.

This repository contains built files from [UnityExplorer](https://github.com/sinai-dev/UnityExplorer) licensed under GPL-3.0; distributed files from UnityExplorer are licensed under GPL-3.0, the rest of the repository is licensed under MIT license.
