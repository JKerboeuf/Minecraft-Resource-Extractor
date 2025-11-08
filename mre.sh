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
█ Version 2.3 by Julien Kerboeuf █
██████████████████████████████████
'
MRE_DIR=$(pwd)
MRE_TMP_DIR="_mre_tmp"
MRE_OUTPUT_DIR="mre-output"
EXTRACTING_MC=1
EXTRACTING_CHOICE=0

function msg() {
	echo -en "${1}"
}

function checkArgs() {
	if [ "${#}" -lt 1 ] || [ "${#}" -gt 2 ]; then
		msg "${RED}Please provide the path to your .minecraft directory and optionally the path to the Java 'jar' binary.\n${YELLOW}Usage : ./mre.sh [.minecraft path] [path to jar executable]\n"
		exit 1
	fi
	if [ "${#}" -eq 2 ]; then
		JAR_CMD="${2}"
	else
		JAR_CMD="jar"
	fi
	mkdir -p "${MRE_DIR}/${MRE_OUTPUT_DIR}"
}

function checkRequirements() {
	if { ! command -v "${JAR_CMD}" &> /dev/null && [ ! -x "${JAR_CMD}" ]; } || ! command -v "jq" &> /dev/null; then
		msg "${RED}You do not have all the required programs this script needs,\nplease install ${YELLOW}Java (jar) ${RED}and ${YELLOW}jq ${RED}before using this script.\nYou can install these programs with the following command :\n${YELLOW}sudo apt-get update && sudo apt-get install openjdk-11-jdk-headless jq\n${RED}"
		exit 1
	fi
}

function checkJarFile() {
	JAR_FOLDERS=$(${JAR_CMD} -tf "${JAR_PATH}" | grep "\." | grep -v 'META-INF\|.class' | cut -d '/' -f 1 | sort | uniq | grep -v "\.")
	msg "${MAGENTA}${JAR_NAME} ${YELLOW}contains the following folders :\n${CYAN}0. ALL OF THEM\n"
	INDEX=1
	for LINE in ${JAR_FOLDERS}; do
		msg "${CYAN}${INDEX}. ${LINE}\n"
		(( INDEX++ ))
	done
	while true; do
		msg "${YELLOW}Type the number corresponding to the folder you wish to extract : ${CYAN}"
		read -n 1 -r ANSWER
		if [[ ${ANSWER} == "0" ]]; then
			extractJar "ALL"
			break
		elif [[ ${ANSWER} =~ ^[1-9]+$ && -n $(echo "${JAR_FOLDERS}" | sed "${ANSWER}q;d") ]]; then
			extractJar "$(echo "${JAR_FOLDERS}" | sed "${ANSWER}q;d")"
			break
		else
			msg "\n"
		fi
	done
}

function extractJar() {
	mkdir -p "${MRE_DIR}/${MRE_OUTPUT_DIR}/${JAR_NAME}"
	cd "${MRE_DIR}/${MRE_OUTPUT_DIR}/${JAR_NAME}" || exit 1
	if [[ "${JAR_PATH}" == *"tmp" ]]; then
		JAR_PATH="${MRE_DIR}/${MRE_TMP_DIR}/${MC_VERSION}.jar"
	fi
	if [[ "${1}" == "ALL" ]]; then
		for LINE in ${JAR_FOLDERS}; do
			FILE_TOTAL=$(${JAR_CMD} -tf "${JAR_PATH}" "${LINE}" | wc -l)
			msg "${CYAN}\nExtracting ${LINE} from ${MAGENTA}${JAR_NAME} ${CYAN}(this may take a while)...\n"
			${JAR_CMD} -xvf "${JAR_PATH}" "${LINE}" | awk -v var="${FILE_TOTAL}" 'BEGIN {ORS=" "} {print NR"/"var" files extracted\r"}'
		done
	else
		FILE_TOTAL=$(${JAR_CMD} -tf "${JAR_PATH}" "${1}" | wc -l)
		msg "${CYAN}\nExtracting ${1} from ${MAGENTA}${JAR_NAME} ${CYAN}(this may take a while)...\n"
		${JAR_CMD} -xvf "${JAR_PATH}" "${1}" | awk -v var="${FILE_TOTAL}" 'BEGIN {ORS=" "} {print NR"/"var" files extracted\r"}'
	fi

	rm -rf "META-INF"
	find . -type f \( -iname "*.class" -o -iname "*.mcassetsroot" -o -iname "gpu_warnlist.json" \) -delete
	find . -type d -empty -delete
	cd "${MRE_DIR}" || exit 1
	msg "\n${GREEN}Content from ${MAGENTA}${JAR_NAME} ${GREEN}succesfully extracted to ${MAGENTA}${MRE_OUTPUT_DIR}/${JAR_NAME}/ ${GREEN}!\n"
}

#################################################################################

function chooseVersion() {
	if [ ! -d "${1}/versions" ]; then
		msg "${RED}\nVersion directory not found, is the path correct ? Did you launch the game at least once ?\n"
		exit 1
	fi
	msg "${YELLOW}Available Minecraft versions :\n"
	VERSION_LIST=$(find "${1}/versions" -type d -not \( -iname "*forge*" -o -iname "*fabric*" -o -iname "*optifine*" -o -name "versions" \) | rev | cut -d '/' -f 1 | rev)
	msg "${CYAN}${VERSION_LIST}\n${YELLOW}Choose the version you want to extract : ${CYAN}"
	read -r MC_VERSION
	if [ ! -d "${1}/versions/${MC_VERSION}" ] || [ ! -f "${1}/versions/${MC_VERSION}/${MC_VERSION}.json" ]; then
		msg "${RED}Version \"${MC_VERSION}\" not found, please launch it with the official Minecraft launcher first.\n"
		exit 1
	fi
	mkdir -p "${MRE_DIR}/${MRE_TMP_DIR}" "${MRE_DIR}/${MRE_OUTPUT_DIR}"
}

function checkMcJar() {
	if [ ! -f "${1}/versions/${MC_VERSION}/${MC_VERSION}.jar" ]; then
		msg "${YELLOW}Version ${MAGENTA}${MC_VERSION}.jar ${YELLOW}not found, downloading it now... "
		curl -s -S "$(jq ".downloads.client.url" "${1}/versions/${MC_VERSION}/${MC_VERSION}.json" | tr -d \")" > "${MRE_DIR}/${MRE_TMP_DIR}/${MC_VERSION}.jar"
		JAR_PATH="${MRE_DIR}/${MRE_TMP_DIR}/${MC_VERSION}.jar"
		msg "${GREEN}Done !\n"
	else
		JAR_PATH="${1}/versions/${MC_VERSION}/${MC_VERSION}.jar"
	fi
	JAR_NAME="${MC_VERSION}.jar"
}

#################################################################################

function checkAssets() {
	ASSETS_VERSION="$(jq ".assetIndex.id" "${1}/versions/${MC_VERSION}/${MC_VERSION}.json" | tr -d \")"
	ASSETS_INDEX="${1}/assets/indexes/${ASSETS_VERSION}.json"
	if [ ! -f "${ASSETS_INDEX}" ]; then
		msg "${YELLOW}Needed ${ASSETS_VERSION} index not found, downloading it now... "
		curl -s -S "$(jq ".assetIndex.url" "${1}/versions/${MC_VERSION}/${MC_VERSION}.json" | tr -d \")" > "${MRE_DIR}/${MRE_TMP_DIR}/${ASSETS_VERSION}.json"
		ASSETS_INDEX="${MRE_DIR}/${MRE_TMP_DIR}/${ASSETS_VERSION}.json"
		msg "${GREEN}Done !\n${RED}Note that this could result with some files missing ! You should launch Minecraft ${MC_VERSION} with the official launcher first.\n"
	else
		msg "${CYAN}Needed ${ASSETS_VERSION} index found. "
	fi
}

function extractAssets() {
	msg "${CYAN}Starting extraction (this may take a while)...${CYAN}\n"
	jq ".objects" "${ASSETS_INDEX}" | grep ':' | grep -v "size" | tr -d ' ' > "${MRE_DIR}/${MRE_TMP_DIR}/indexes"
	FILE_TOTAL=$(wc -l "${MRE_DIR}/${MRE_TMP_DIR}/indexes" | cut -d ' ' -f 1)
	ASSET_LINE_INDEX=0
	while read -r LINE; do
		if [[ "${LINE}" == *"hash"* ]]; then
			ASSET_HASH=$(echo "${LINE}" | cut -d \" -f 4)
			mkdir -p "$(echo "${MRE_DIR}/${MRE_OUTPUT_DIR}/${ASSETS_VERSION}-assets/${ASSET_OUTPUT}" | rev | cut -d '/' -f 2- | rev)"
			ASSET_LINE_INDEX=$((ASSET_LINE_INDEX + 1))
			cp -v "${1}/assets/objects/$(echo "${ASSET_HASH}" | cut -c 1-2)/${ASSET_HASH}" "${MRE_DIR}/${MRE_OUTPUT_DIR}/${ASSETS_VERSION}-assets/${ASSET_OUTPUT}" | awk -v var="${FILE_TOTAL}" -v vari="${ASSET_LINE_INDEX}" 'BEGIN {ORS=" "} {print vari"/"var/2" files extracted\r"}'
		else
			ASSET_OUTPUT=$(echo "${LINE}" | cut -d \" -f 2)
		fi
	done < "${MRE_DIR}/${MRE_TMP_DIR}/indexes"
	msg "\n${GREEN}${ASSETS_VERSION} assets succesfully extracted to ${MAGENTA}${MRE_OUTPUT_DIR}/${ASSETS_VERSION}-assets/ ${GREEN}!\n"
}

#################################################################################

function chooseExtraction() {
	msg "${YELLOW}Please choose what you would like to extract :\n${CYAN}0. ALL OF THEM\n1. minecraft.jar files (contains textures, models and data files)\n2. Asset files (contains sounds, musics, language files and some more)\n"
	while true; do
		msg "${YELLOW}Type the number corresponding to what you wish to extract : ${CYAN}"
		read -n 1 -r ANSWER
		if [[ ${ANSWER} == "0" ]]; then
			EXTRACTING_CHOICE=0
			break
		elif [[ ${ANSWER} == "1" ]]; then
			EXTRACTING_CHOICE=1
			break
		elif [[ ${ANSWER} == "2" ]]; then
			EXTRACTING_CHOICE=2
			break
		else
			msg "\n"
		fi
	done
	msg "\n"
}

function extractMCVersion() {
	chooseVersion "${1}"
	chooseExtraction
	if [[ ${EXTRACTING_CHOICE} == "1" ]]; then
		checkMcJar "${1}"
		checkJarFile
	elif [[ ${EXTRACTING_CHOICE} == "2" ]]; then
		checkAssets "${1}"
		extractAssets "${1}"
	else
		checkMcJar "${1}"
		checkJarFile
		checkAssets "${1}"
		extractAssets "${1}"
	fi
}

msg "${CYAN}${BANNER}\n"

checkArgs "$@"
checkRequirements
if [ ${EXTRACTING_MC} -eq 1 ]; then
	extractMCVersion "${1}"
else
	if [ ! -f "${JAR_PATH}" ]; then
		msg "${RED}JAR file not found: ${JAR_PATH}\n"
		exit 1
	fi
	checkJarFile
fi

rm -rf "${MRE_DIR:?}/${MRE_TMP_DIR}"
msg "${GREEN}\nThank you for using the Minecraft Resource Extractor made by ${YELLOW}Julien Kerboeuf${GREEN},\nif you would like to support my work you can donate to me on paypal at ${YELLOW}https://paypal.me/jkerboeuf ${WHITE}\n"
