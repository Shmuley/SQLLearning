#!/bin/bash
filePath=$1
adventureworks=AdventureWorks2019.bak

pwd

if [ -z "$filePath" ]
then
    filePath="."
fi

echo "File path is set to $filePath/$adventureworks"

if ! [ -f "$filePath/$adventureworks" ]
then
    echo "File not found, downloading"
    curl -L -o "$filePath/$adventureworks" "https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/$adventureworks"
else
    echo "File exists"
fi
