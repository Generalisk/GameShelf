@echo off
dotnet build "../src" -c debug -o "./../build/debug/windows" --os win
dotnet build "../src" -c debug -o "./../build/debug/linux" --os linux
pause
