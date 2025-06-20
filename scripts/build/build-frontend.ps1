# Build Frontend Windows Forms - BMYLBH2025_SDDAP (Fixed with full MSBuild path)
param(
    [string]$Configuration = "Release",
    [string]$Platform = "Any CPU",
    [switch]$RunTests = $false,
    [switch]$Clean = $false
)

Write-Host "=== BMYLBH2025_SDDAP Frontend Build Script ===" -ForegroundColor Green
Write-Host "Configuration: $Configuration" -ForegroundColor Yellow

# MSBuild path
$MSBuildPath = "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe"

try {
    # Navigate to frontend directory
    $FrontendPath = "Frontend/BMYLBH2025_SDDAP"
    Set-Location $FrontendPath
    
    # Clean if requested
    if ($Clean) {
        Write-Host "Cleaning solution..." -ForegroundColor Yellow
        & $MSBuildPath BMYLBH2025_SDDAP.sln /t:Clean /p:Configuration=$Configuration
    }
    
    # Restore using MSBuild
    Write-Host "Restoring packages..." -ForegroundColor Yellow
    & $MSBuildPath BMYLBH2025_SDDAP.sln /t:Restore
    
    # Build the solution
    Write-Host "Building frontend solution..." -ForegroundColor Yellow
    & $MSBuildPath BMYLBH2025_SDDAP.sln /p:Configuration=$Configuration /p:Platform="$Platform"
    if ($LASTEXITCODE -ne 0) {
        throw "Build failed with exit code: $LASTEXITCODE"
    }
    
    # Run tests if requested
    if ($RunTests) {
        Write-Host "Running frontend unit tests..." -ForegroundColor Yellow
        & $MSBuildPath BMYLBH2025_SDDAP.Tests/BMYLBH2025_SDDAP.Tests.csproj /p:Configuration=$Configuration
        if ($LASTEXITCODE -ne 0) {
            throw "Tests failed with exit code: $LASTEXITCODE"
        }
    }
    
    Write-Host "Frontend build completed successfully!" -ForegroundColor Green
    
} catch {
    Write-Host "Frontend Build Failed: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}
