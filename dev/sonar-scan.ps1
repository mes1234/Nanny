dotnet sonarscanner begin /k:"nanny" /d:sonar.host.url="$env:SONAR_ADDRESS" /d:sonar.login="$env:SONAR_TOKEN" /d:sonar.coverageReportPaths=".\sonarqubecoverage\SonarQube.xml" /d:sonar.cs.xunit.reportsPaths="./*.Tests/TestResults/TestResults.xml"

dotnet build

dotnet test --no-build --collect:"XPlat Code Coverage"  --logger:xunit

reportgenerator "-reports:*\TestResults\*\coverage.cobertura.xml" "-targetdir:sonarqubecoverage" "-reporttypes:SonarQube"
 

dotnet sonarscanner end /d:sonar.login="$env:SONAR_TOKEN"