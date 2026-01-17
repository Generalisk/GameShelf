@echo off
dotnet build "../src" -c release -o "./../build/release/linux/x64" -r linux-x64
pause
