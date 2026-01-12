@echo off
dotnet publish "../src" --self-contained -o "./../publish/windows" --os win
pause
