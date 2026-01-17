dotnet build "../src" -c release -o "./../build/release/windows/x64" -r win-x64
dotnet build "../src" -c release -o "./../build/release/linux/x64" -r linux-x64
read -p "Press any key to continue..." -n1 -s
