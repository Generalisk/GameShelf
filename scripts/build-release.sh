dotnet publish "../src" -o "./../build/release/windows" --os win
dotnet publish "../src" -o "./../build/release/linux" --os linux
read -p "Press any key to continue..." -n1 -s
