#!/usr/bin/env bash

TARGET_FILE=${1:-"docs/WebGL/Build/WebGL.loader.js"}
echo "TARGET_FILE: ${TARGET_FILE}"

# Very Fragile
sed -i -e 's_mobile:/Mobile|Android|iP(ad|hone)/.test(navigator.appVersion)_mobile:false_g' "${TARGET_FILE}"
