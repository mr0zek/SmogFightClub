rem dotnet tool install -g dotnet-reportgenerator-globaltool
rem dotnet tool install -g coverlet.console
rem coverlet .\src\SFC.Tests\bin\Debug\net6.0\SFC.Tests.dll --target "dotnet" --targetargs "test --no-build"

dotnet test src\SmogFightClub.sln --collect:"XPlat Code Coverage"        
reportgenerator -reports:".\src\SFC.Tests\TestResults\*\coverage.cobertura.xml" -targetdir:"coverresults" -reporttypes:Html
start coverresults/index.html