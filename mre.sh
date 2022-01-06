#!/bin/bash

GREEN='\033[1;32m'
CYAN='\033[1;36m'
RED='\033[0;31m'
YELLOW='\033[1;33m'
MAGENTA='\033[1;35m'
WHITE='\033[1;0m'

if [ ! $# -eq 1 ]; then
	echo -e "${RED}Please provide only the path to your .minecraft directory."
	echo -e "${YELLOW}Usage : ./mre.sh [.minecraft path]"
	exit
fi

MRETPM="_mre_tmp"
MREOUT="mre-output"

#####	VERSION
echo -e "${CYAN}= Minecraft Ressource Extractor ="
if [ ! -d "$1/versions" ]; then
    echo -e "${RED}Version directory not found, is the path correct ? Did you launch the game at least once ?"
	exit
fi
echo -e "${WHITE}Available versions :"
VERLIST=$(find "$1/versions" -type d -not \( -iname "*forge*" -o -iname "*fabric*" -o -iname "*optifine*" -o -name "versions" \) | rev | cut -d '/' -f 1 | rev)
echo -en "${WHITE}$VERLIST\n${YELLOW}Choose the version you want to extract : ${MAGENTA}"
read -r MCVER
if [ ! -d "$1/versions/$MCVER" ] || [ ! -f "$1/versions/$MCVER/$MCVER.json" ]; then
    echo -e "${RED}Version \"$MCVER\" not found, please launch it with the official Minecraft launcher first."
	exit
fi
mkdir -p $MRETPM $MREOUT

#####	JAR
if [ ! -f "$1/versions/$MCVER/$MCVER.jar" ]; then
    echo -e "${YELLOW}Version $MCVER.jar not found, downloading it now..."
	curl -s -S "$(jq ".downloads.client.url" "$1/versions/$MCVER/$MCVER.json" | tr -d \")" > "$MRETPM/$MCVER.jar"
	JARPATH="../../$MRETPM/$MCVER.jar"
else
    echo -en "${CYAN}$MCVER.jar found. "
	JARPATH="$1/versions/$MCVER/$MCVER.jar"
fi
echo -e "${CYAN}Starting extraction (this may take a while)..."
mkdir -p "$MREOUT/$MCVER.jar"
cd "$MREOUT/$MCVER.jar" || exit
echo -e "${CYAN}Extracting assets from $MCVER.jar...${MAGENTA}"
FILENB=$(jar -tf "$JARPATH" assets | wc -l)
jar -xvf "$JARPATH" assets | awk -v var="$FILENB" 'BEGIN {ORS=" "} {print NR"/"var" files extracted\r"}'
echo -e "\r${CYAN}Extracting data from $MCVER.jar...${MAGENTA}"
FILENB=$(jar -tf "$JARPATH" data | wc -l)
jar -xvf "$JARPATH" data | awk -v var="$FILENB" 'BEGIN {ORS=" "} {print NR"/"var" files extracted\r"}'
rm -f assets/.mcassetsroot data/.mcassetsroot assets/minecraft/gpu_warnlist.json ../../$MRETPM/$MCVER.jar
cd ../..
echo -e "\r${GREEN}Content from $MCVER.jar succesfully extracted to $MREOUT/$MCVER.jar !"

#####	ASSETS
ASSVER="$(jq ".assetIndex.id" "$1/versions/$MCVER/$MCVER.json" | tr -d \")"
ASSINDEX="$1/assets/indexes/$ASSVER.json"
if [ ! -f "$ASSINDEX" ]; then
    echo -e "${YELLOW}Needed $ASSVER index not found, downloading it now..."
    echo -e "${RED}Note that this could result with some files missing ! You should launch Minecraft $MCVER with the official launcher first."
	curl -s -S "$(jq ".assetIndex.url" "$1/versions/$MCVER/$MCVER.json" | tr -d \")" > "$MRETPM/$ASSVER.json"
	ASSINDEX="$MRETPM/$ASSVER.json"
else
    echo -en "${CYAN}Needed $ASSVER index found. "
fi
echo -e "${CYAN}Starting extraction (this may take a while)...${MAGENTA}"
jq ".objects" "$ASSINDEX" | grep ':' | grep -v "size" | tr -d ' ' > "$MRETPM/indexes"
FILENB=$(cat "$MRETPM/indexes" | wc -l)
I=0
while read LINE; do
	if [[ "$LINE" == *"hash"* ]]; then
		ASSHASH=$(echo "$LINE" | cut -d \" -f 4)
		mkdir -p "$(echo "$MREOUT/$ASSVER-assets/$ASSOUT" | rev | cut -d '/' -f 2- | rev)"
		I=$((I+1))
		cp -v "$1/assets/objects/$(echo "$ASSHASH" | cut -c 1-2)/$ASSHASH" "$MREOUT/$ASSVER-assets/$ASSOUT" | awk -v var="$FILENB" -v vari="$I" 'BEGIN {ORS=" "} {print vari"/"var/2" files extracted\r"}'
	else
		ASSOUT=$(echo "$LINE" | cut -d \" -f 2)
	fi
done < "$MRETPM/indexes"
echo -e "\r${GREEN}$ASSVER assets succesfully extracted to $MREOUT/$ASSVER-assets !${WHITE}"
rm -rf "$MRETPM"
