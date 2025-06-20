# Run All Tests - BMYLBH2025_SDDAP (Fixed with MSBuild path)
param(
    [string]$Configuration = "Release",
    [switch]$GenerateReport = $true
)

Write-Host "=== BMYLBH2025_SDDAP Test Runner ===" -ForegroundColor Green

# MSBuild path
$MSBuildPath = "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe"

$TestResults = @()
$StartTime = Get-Date

try {
    # Run Backend Tests
    Write-Host "Running Backend API Tests..." -ForegroundColor Yellow
    Set-Location "Backend/BMYLBH2025_SDDAP"
    
    dotnet test BMYLBH2025_SDDAP.Tests/BMYLBH2025_SDDAP.Tests.csproj --configuration $Configuration --logger "trx;LogFileName=backend-test-results.trx" --results-directory "TestResults/" --no-build
    $TestResults += @{
        Project = "Backend API"
        Success = ($LASTEXITCODE -eq 0)
        ExitCode = $LASTEXITCODE
    }
    
    Set-Location "../.."
    
    # Run Frontend Tests
    Write-Host "Running Frontend Windows Forms Tests..." -ForegroundColor Yellow
    Set-Location "Frontend/BMYLBH2025_SDDAP"
    
    & $MSBuildPath BMYLBH2025_SDDAP.Tests/BMYLBH2025_SDDAP.Tests.csproj /p:Configuration=$Configuration
    
    $TestResults += @{
        Project = "Frontend Windows Forms"
        Success = ($LASTEXITCODE -eq 0)
        ExitCode = $LASTEXITCODE
    }
    
    Set-Location "../.."
    
    # Display Results
    Write-Host ""
    Write-Host "=== Test Results Summary ===" -ForegroundColor Cyan
    foreach ($result in $TestResults) {
        $status = if ($result.Success) { "PASSED" } else { "FAILED" }
        $color = if ($result.Success) { "Green" } else { "Red" }
        Write-Host "$($result.Project): $status" -ForegroundColor $color
    }
    
    $EndTime = Get-Date
    $Duration = $EndTime - $StartTime
    Write-Host "Total test time: $($Duration.TotalSeconds.ToString('F2')) seconds" -ForegroundColor Cyan
    
    # Return success if all tests passed
    $allPassed = ($TestResults | Where-Object { -not $_.Success }).Count -eq 0
    if (-not $allPassed) {
        exit 1
    }
    
} catch {
    Write-Host "Test execution failed: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}
