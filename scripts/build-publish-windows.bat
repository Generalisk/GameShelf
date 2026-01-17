@echo off
dotnet publish "../src" --self-contained -o "./../publish/windows/x64" -r win-x64
pause
