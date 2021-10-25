#!/usr/bin/env bash

TARGET_FILE=${1:-"docs/WebGL/Build/WebGL.loader.js"}
echo -e "TARGET_FILE: ${TARGET_FILE}\n"

perl -ne  'if (/^(.*)mobile:(.*?),(.*)/) { print "JS Mobile Check: " . $2 . "\n" }' "${TARGET_FILE}"
