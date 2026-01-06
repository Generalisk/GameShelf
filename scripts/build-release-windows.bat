@echo off
dotnet publish "../src" -o "./../build/release/windows" --os win
pause
