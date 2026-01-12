dotnet build "../src" -c release -o "./../build/release/windows" --os win
dotnet build "../src" -c release -o "./../build/release/linux" --os linux
read -p "Press any key to continue..." -n1 -s
