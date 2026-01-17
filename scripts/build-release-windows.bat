@echo off
dotnet build "../src" -c release -o "./../build/release/windows/x64" -r win-x64
pause
