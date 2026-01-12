@echo off
dotnet build "../src" -c release -o "./../build/release/windows" --os win
dotnet build "../src" -c release -o "./../build/release/linux" --os linux
pause
