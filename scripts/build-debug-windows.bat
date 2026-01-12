@echo off
dotnet build "../src" -c debug -o "./../build/debug/windows" --os win
pause
