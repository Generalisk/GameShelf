@echo off
dotnet build "../src" -c release -o "./../build/release/windows/x64" -r win-x64
dotnet build "../src" -c release -o "./../build/release/linux/x64" -r linux-x64
pause
