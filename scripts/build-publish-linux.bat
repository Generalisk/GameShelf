@echo off
dotnet publish "../src" --self-contained -o "./../publish/linux/x64" -r linux-x64
pause
