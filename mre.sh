#!/bin/bash

GREEN='\033[1;32m'
CYAN='\033[1;36m'
RED='\033[1;31m'
YELLOW='\033[1;33m'
MAGENTA='\033[1;35m'
WHITE='\033[1;0m'
BANNER='
 ███    ███    ██████     ███████
 ████  ████    ██   ██    ██     
 ██ ████ ██    ██████     █████  
 ██  ██  ██    ██   ██    ██     
 ██      ██    ██   ██    ███████

██████████████████████████████████
█  Minecraft Resource Extractor  █
█ Version 1.0 by Julien Kerboeuf █
██████████████████████████████████
'
MRE_TMP_DIR="_mre_tmp"
MRE_OUTPUT_DIR="mre-output"

function checkVersions() {
	echo -en "${CYAN}Searching Minecraft versions... "
	if [ ! -d "$1/versions" ]; then
		echo -e "${RED}\nVersion directory not found, is the path correct ? Did you launch the game at least once ?"
		exit
	fi
	echo -e "${GREEN}Done !${WHITE}"
}

function chooseVersion() {
	echo -e "${YELLOW}Available versions :"
	VERSION_LIST=$(find "$1/versions" -type d -not \( -iname "*forge*" -o -iname "*fabric*" -o -iname "*optifine*" -o -name "versions" \) | rev | cut -d '/' -f 1 | rev)
	echo -en "${MAGENTA}$VERSION_LIST\n${YELLOW}Choose the version you want to extract : ${MAGENTA}"
	read -r MC_VERSION
	if [ ! -d "$1/versions/$MC_VERSION" ] || [ ! -f "$1/versions/$MC_VERSION/$MC_VERSION.json" ]; then
		echo -e "${RED}Version \"$MC_VERSION\" not found, please launch it with the official Minecraft launcher first."
		exit
	fi
	mkdir -p $MRE_TMP_DIR $MRE_OUTPUT_DIR
}

function checkJar() {
	if [ ! -f "$1/versions/$MC_VERSION/$MC_VERSION.jar" ]; then
		echo -en "${YELLOW}Version $MC_VERSION.jar not found, downloading it now... "
		curl -s -S "$(jq ".downloads.client.url" "$1/versions/$MC_VERSION/$MC_VERSION.json" | tr -d \")" > "$MRE_TMP_DIR/$MC_VERSION.jar"
		JAR_PATH="../../$MRE_TMP_DIR/$MC_VERSION.jar"
		echo -e "${GREEN}Done !"
	else
		echo -en "${CYAN}$MC_VERSION.jar found. "
		JAR_PATH="$1/versions/$MC_VERSION/$MC_VERSION.jar"
	fi
}

function extractJar() {
	echo -e "${CYAN}Starting extraction (this may take a while)..."
	mkdir -p "$MRE_OUTPUT_DIR/$MC_VERSION.jar"
	cd "$MRE_OUTPUT_DIR/$MC_VERSION.jar" || exit
	extractJarAssets
	extractJarData
	rm -f "assets/.mcassetsroot" "data/.mcassetsroot" "assets/minecraft/gpu_warnlist.json" "../../$MRE_TMP_DIR/$MC_VERSION.jar"
	cd ../..
	echo -e "\r${GREEN}Content from $MC_VERSION.jar succesfully extracted to ${YELLOW}$MRE_OUTPUT_DIR/$MC_VERSION.jar/ ${GREEN}!"
}

function extractJarAssets() {
	FILE_TOTAL=$(jar -tf "$JAR_PATH" assets | wc -l)
	echo -e "${CYAN}Extracting assets from $MC_VERSION.jar...${MAGENTA}"
	jar -xvf "$JAR_PATH" assets | awk -v var="$FILE_TOTAL" 'BEGIN {ORS=" "} {print NR"/"var" files extracted\r"}'
}

function extractJarData() {
	FILE_TOTAL=$(jar -tf "$JAR_PATH" data | wc -l)
	if [ ! "$FILE_TOTAL" -eq 0 ]; then
		echo -e "\r${CYAN}Extracting data from $MC_VERSION.jar...${MAGENTA}"
		jar -xvf "$JAR_PATH" data | awk -v var="$FILE_TOTAL" 'BEGIN {ORS=" "} {print NR"/"var" files extracted\r"}'
	else
		echo -e "${YELLOW}Minecraft $MC_VERSION doesn't have a data directory, the data files are in the assets directory from $MC_VERSION.jar"
	fi
}

checkAssets() {
	ASSETS_VERSION="$(jq ".assetIndex.id" "$1/versions/$MC_VERSION/$MC_VERSION.json" | tr -d \")"
	ASSETS_INDEX="$1/assets/indexes/$ASSETS_VERSION.json"
	if [ ! -f "$ASSETS_INDEX" ]; then
		echo -en "${YELLOW}Needed $ASSETS_VERSION index not found, downloading it now... "
		curl -s -S "$(jq ".assetIndex.url" "$1/versions/$MC_VERSION/$MC_VERSION.json" | tr -d \")" > "$MRE_TMP_DIR/$ASSETS_VERSION.json"
		ASSETS_INDEX="$MRE_TMP_DIR/$ASSETS_VERSION.json"
		echo -e "${GREEN}Done !\n${RED}Note that this could result with some files missing ! You should launch Minecraft $MC_VERSION with the official launcher first."
	else
		echo -en "${CYAN}Needed $ASSETS_VERSION index found. "
	fi
}

function extractAssets() {
	echo -e "${CYAN}Starting extraction (this may take a while)...${MAGENTA}"
	jq ".objects" "$ASSETS_INDEX" | grep ':' | grep -v "size" | tr -d ' ' > "$MRE_TMP_DIR/indexes"
	FILE_TOTAL=$(wc -l "$MRE_TMP_DIR/indexes" | cut -d ' ' -f 1)
	ASSET_LINE_INDEX=0
	while read -r LINE; do
		if [[ "$LINE" == *"hash"* ]]; then
			ASSET_HASH=$(echo "$LINE" | cut -d \" -f 4)
			mkdir -p "$(echo "$MRE_OUTPUT_DIR/$ASSETS_VERSION-assets/$ASSET_OUTPUT" | rev | cut -d '/' -f 2- | rev)"
			ASSET_LINE_INDEX=$((ASSET_LINE_INDEX+1))
			cp -v "$1/assets/objects/$(echo "$ASSET_HASH" | cut -c 1-2)/$ASSET_HASH" "$MRE_OUTPUT_DIR/$ASSETS_VERSION-assets/$ASSET_OUTPUT" | awk -v var="$FILE_TOTAL" -v vari="$ASSET_LINE_INDEX" 'BEGIN {ORS=" "} {print vari"/"var/2" files extracted\r"}'
		else
			ASSET_OUTPUT=$(echo "$LINE" | cut -d \" -f 2)
		fi
	done < "$MRE_TMP_DIR/indexes"
	echo -e "\r${GREEN}$ASSETS_VERSION assets succesfully extracted to ${YELLOW}$MRE_OUTPUT_DIR/$ASSETS_VERSION-assets/ ${GREEN}!"
}

if [ ! $# -eq 1 ]; then
	echo -e "${RED}Please provide the path to your .minecraft directory and nothing else.\n${YELLOW}Usage : ./mre.sh [.minecraft path]"
	exit
fi

echo -e "${CYAN}${BANNER}"

checkVersions "$1"
chooseVersion "$1"
checkJar "$1"
extractJar "$1"
checkAssets "$1"
extractAssets "$1"

echo -en "${CYAN}Cleaning up temporary files... "
rm -rf "$MRE_TMP_DIR"
echo -e "${GREEN}Done !\n"

echo -e "${GREEN}Thank you for using the Minecraft Resource Extractor made by ${MAGENTA}Julien Kerboeuf${GREEN},\nif you would like to support my work you can donate to me on paypal at ${MAGENTA}https://paypal.me/jkerboeuf ${WHITE}"
