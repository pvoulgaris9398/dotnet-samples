#!/bin/bash

set -e

# Find and display directories to be deleted
find . \( -iname "bin" -o -iname "obj" \) -type d -print

# Prompt user for confirmation
read -p "The above folders are going to be deleted, are you sure? [Y/N]: " -n 1 -r
echo    # Move to a new line

# Delete directories if confirmed
if [[ $REPLY =~ ^[Yy]$ ]]; then
    find . \( -iname "bin" -o -iname "obj" \) -type d -exec rm -rfv {} +
    echo "Done"
else
    echo "No"
    exit 1
fi