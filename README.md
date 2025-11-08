![MRE Banner](https://raw.githubusercontent.com/JKerboeuf/Minecraft-Resource-Extractor/main/mre%20banner%20full%20512.webp)

# Minecraft Resource Extractor

The Minecraft Resource Extractor (or *MRE* for short) is a small tool for data packs and resource packs creators.  
With this tool, you can easily extract all the textures, sounds, and data files (achievements, crafting recipes, loot tables and others) from any minecraft.jar version or mod you want.

## Windows Requirements

**[Java JDK](https://www.oracle.com/java/technologies/downloads/)**, used to extract the content of .jar files

## Linux Requirements

- **Java JDK** (*openjdk*), used to extract the content of .jar files
- **jq**, a command line json parser.

You can install these with this one simple command :

```Shell
sudo apt-get update && sudo apt-get install openjdk-11-jdk-headless jq
```

## How to use on Windows

- Check and install the requirements above if needed
- Download the latest **Windows** release **[here](https://github.com/JKerboeuf/Minecraft-Resource-Extractor/releases/latest)**, it is found below "Assets" and should be named **"mre-for-windows.zip"**
- Extract the folder inside of the donwloaded .zip file and launch `MinecraftResourceExtractor.exe`
- Follow the instructions given and extract whatever you want !
- All extracted files go to the folder **"mre-output"** in the same folder as the .exe

## How to use on Linux

- Check and install the requirements above if needed
- Download the latest **Linux** release **[here](https://github.com/JKerboeuf/Minecraft-Resource-Extractor/releases/latest)**, it is found below "Assets" and should be named **"mre-for-linux.zip"**
- Extract the content of the donwloaded .zip file and launch the `mre.sh` with the path to your **.minecraft** or to a **.jar file**, you can also add a path to your Java `jar` binary (the path should be to the binary file itself, not the directory it's in).
- Follow the instructions given and extract whatever you want !
- All extracted files go to the folder **"mre-output"** in the same folder as the script

### Examples

```Shell
./mre.sh "/mnt/c/users/YOURUSER/AppData/Roaming/.minecraft"
```

```Shell
./mre.sh "/mnt/c/Users/YOURUSER/AppData/Roaming/.minecraft/mods/SomeModFile.jar"
```

```Shell
./mre.sh "/mnt/c/users/YOURUSER/AppData/Roaming/.minecraft" "/some/path/to/jar"
```

## What to extract ?

There is 3 types of files you can extract using this tool :

### version.jar -> assets

Useful for **resource pack** creators, this part contains all **textures** and **models**.

### version.jar -> data

Useful for **data pack** creators, this part contains all data files (`json` files), these include loot tables, crafting recipes, advancements, world generation and more.

### Assets

Useful for **resource pack** creators, these files are the most annoying to treat by hand as their names are "encoded", they mostly are **sound**, **music** and **language** files but they also include the **Programmer Art resource pack** and some more "default" files I'll let you explore.

## Screenshot

![screenshot of MRE](https://i.imgur.com/1pqQNQH.png)
