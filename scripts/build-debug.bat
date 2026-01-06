@echo off
dotnet build "../src" -o "./../build/debug/windows" --os win
dotnet build "../src" -o "./../build/debug/linux" --os linux
pause
