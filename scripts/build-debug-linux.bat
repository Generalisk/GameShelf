@echo off
dotnet build "../src" -c debug -o "./../build/debug/linux/x64" -r linux-x64
pause
