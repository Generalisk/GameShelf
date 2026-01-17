dotnet publish "../src" --self-contained -o "./../publish/windows/x64" -r win-x64
dotnet publish "../src" --self-contained -o "./../publish/linux/x64" -r linux-x64
read -p "Press any key to continue..." -n1 -s
