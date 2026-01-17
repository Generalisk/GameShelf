@echo off
dotnet build "../src" -c debug -o "./../build/debug/windows/x64" -r win-x64
pause
