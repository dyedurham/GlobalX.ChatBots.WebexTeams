dotnet test src/GlobalX.ChatBots.WebexTeams.Tests/GlobalX.ChatBots.WebexTeams.Tests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat="opencover"
bash <(curl -s https://codecov.io/bash) -f src/GlobalX.ChatBots.WebexTeams.Tests/coverage.opencover.xml
