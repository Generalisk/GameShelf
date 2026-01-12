@echo off
dotnet build "../" -c debug
cd "../bin/Debug/"
GameShelf.exe
pause
