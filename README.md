# Minecraft Resource Extractor

The Minecraft Resource Extractor (or *MRE* for short) is a small tool for data packs and resource packs creators.  
With this tool, you can easily extract all the textures, sounds, and data files (achievements, crafting recipes and others) from any minecraft.jar version you have installed.

## Extraction

There is 3 types of files you can extract using this tool :

### version.jar -> assets

Useful for **resource pack** creators, this part contains all **textures** and **models** from the game.

### version.jar -> data

Useful for **data pack** creators, this part contains all data files (`json` files) from the game, these include loot tables, crafting recipes, advancements, world generation and more.

### Assets

Useful for **resource pack** creators, these files are the most annoying to treat by hand as their names are "encoded", they mostly are **sound**, **music** and **language** files but they also include the **Programmer Art resource pack** and some more "default" files I'll let you explore.

## Setup

- Download the latest release [here](https://github.com/JKerboeuf/Minecraft-Resource-Extractor/releases/latest) and put the `mre.sh` file you just downloaded anywhere you want on your computer.
- Open a Linux terminal and navigate to the directory where you put the `mre.sh` file
- Install the required programs if you dont already have them (details below about the requirements) :

```Shell
sudo apt-get update && sudo apt-get install openjdk-11-jdk-headless jq
```

- Now you can use it ! See the [Usage section](#usage) below

### Requirements

This Shell script is meant to be used within a command terminal on a Linux OS, it **will work** on Windows using the **Linux sub-system** (aka WSL).

You will need **Java** (`openjdk`) installed, any version that can extract content from a .jar file with a `jar -x [file]` command.

Finally you need **jq**, a command line json parser.

## Usage

- Launch the tool using `./mre.sh` or `sh mre.sh` with the path to your **.minecraft** folder
- The tool will display the versions existing in your .minecraft folder, you need to type the exact name of the version you want then press enter  
(if the version you want is not in the list displayed you will need to install it using the official Minecraft launcher first)
- Now you can just wait for it to finish, all the files are stored in the `mre-output` folder

### Example

```Shell
./mre.sh "/mnt/c/users/YOURUSER/AppData/Roaming/.minecraft"
```

## Planned Features

- The ability to choose what to extract
- The ability to extract assets from individual jar files (useful to extract mod assets for example)
- Usable on Windows

I am always open to any suggestions.
