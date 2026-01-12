@echo off
dotnet publish "../src" --self-contained -o "./../publish/linux" --os linux
pause
