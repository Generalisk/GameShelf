@echo off
dotnet build "../src" -c release -o "./../build/release/linux" --os linux
pause
