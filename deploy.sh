VERSION=$1
NUGET_API_KEY=$2

dotnet pack src/GlobalX.ChatBots.WebexTeams/GlobalX.ChatBots.WebexTeams.csproj -p:PackageVersion=$VERSION -o ../../
dotnet nuget push GlobalX.ChatBots.WebexTeams.$VERSION.nupkg -k $NUGET_API_KEY -s nuget.org