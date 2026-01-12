@echo off
dotnet build "../src" -c release -o "./../build/release/windows" --os win
pause
