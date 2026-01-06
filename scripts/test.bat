@echo off
dotnet build "../"
cd "../bin/Debug/"
GameShelf.exe
pause
