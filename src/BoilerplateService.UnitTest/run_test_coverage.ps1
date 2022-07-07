# Parameters - Minumum code coverage rate
$Threshold = 80
# Parameters - Solution
$SolutionFileName = "BoilerplateService.sln"
$CoverageCoverletDir = ".coverage"
$CoverageReportDir = ".coverage-report"

# Parameters - Coverlet
$DataCollectorFormat = "cobertura"
$CoverletOutputFormat = "cobertura"
$CoverletOutputExtension = ".xml"
$Collect = "XPlat Code Coverage"
$CoverageFileName = "coverage.$CoverletOutputFormat$CoverletOutputExtension"

# Parameters - Report Generator
$ReportTypes = "HTML;cobertura;"
$HtmlReportIndexFileName = "index.html"

# Calculated Parameters - Project
$ProjectDir = (Get-Item $PSScriptRoot).FullName

# Calculated Parameters - Solution
$SolutionDir = (Get-Item $PSScriptRoot).Parent.FullName
$Solution = [IO.Path]::Combine($SolutionDir, $SolutionFileName)

# Calculated Parameters - Coverlet
$CoverletOutput = [IO.Path]::Combine($ProjectDir, $CoverageCoverletDir)

# Delete previous test run results
Remove-Item -Recurse -Force $CoverletOutput/

# Build and Test
dotnet.exe test $Solution --collect:"$Collect" --results-directory:"$CoverletOutput" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format="$DataCollectorFormat"

# Check Test results
if ($LastExitCode -ne 0) {
    exit 1
}

# Delete previous test run reports
Remove-Item -Recurse -Force [IO.Path]::Combine($ProjectDir, $CoverageReportDir)/

# Calculated Parameters - Report Generator
$TargetDir = [IO.Path]::Combine($ProjectDir, $CoverageReportDir)
$ReportHtmlFile = [IO.Path]::Combine($ProjectDir, $CoverageReportDir, $HtmlReportIndexFileName)
$TestResultsDirs = Join-Path $CoverletOutput -ChildPath **\*
$CoverageFile = (Split-Path $TestResultsDirs -Resolve | ForEach-Object -Process { [IO.Path]::Combine($_, $CoverageFileName) }) -join ";"

# Extracte code coverage rates
[XML]$report = Get-Content $CoverageFile
[decimal]$LineRate = $report.coverage.'line-rate'
$LineRate = [math]::Round($LineRate * 100, 2)

# Generate Report
reportgenerator.exe "-reports:$CoverageFile" "-targetdir:$TargetDir" "-reporttypes:$ReportTypes"

if ($LineRate -lt $Threshold) {
    Write-Host "Test coverage failed! Covered: $LineRate%, expected: $Threshold%" -ForegroundColor red
    Invoke-Item $ReportHtmlFile
    exit 1
} else {
    Write-Host "Test coverage passed! Covered: $LineRate%, expected: $Threshold%" -ForegroundColor green
    exit 0
}