@echo off
dotnet publish "../src" -o "./../build/release/windows" --os win
dotnet publish "../src" -o "./../build/release/linux" --os linux
pause
