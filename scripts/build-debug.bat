@echo off
dotnet build "../src" -c debug -o "./../build/debug/windows/x64" -r win-x64
dotnet build "../src" -c debug -o "./../build/debug/linux/x64" -r linux-x64
pause
