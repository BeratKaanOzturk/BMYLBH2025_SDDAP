# Master Build Script - BMYLBH2025_SDDAP
# Builds both Backend and Frontend projects

param(
    [string]$Configuration = "Release",
    [switch]$RunTests = $true,
    [switch]$Clean = $false
)

Write-Host "======================================" -ForegroundColor Cyan
Write-Host "BMYLBH2025_SDDAP - Master Build Script" -ForegroundColor Cyan
Write-Host "======================================" -ForegroundColor Cyan
Write-Host ""

$StartTime = Get-Date
$OriginalLocation = Get-Location

try {
    # Build Backend
    Write-Host "Step 1: Building Backend API..." -ForegroundColor Magenta
    & "$PSScriptRoot/build-backend.ps1" -Configuration $Configuration -RunTests:$RunTests -Clean:$Clean
    if ($LASTEXITCODE -ne 0) {
        throw "Backend build failed"
    }
    
    Write-Host ""
    
    # Build Frontend
    Write-Host "Step 2: Building Frontend Windows Forms..." -ForegroundColor Magenta
    Set-Location $OriginalLocation
    & "$PSScriptRoot/build-frontend.ps1" -Configuration $Configuration -RunTests:$RunTests -Clean:$Clean
    if ($LASTEXITCODE -ne 0) {
        throw "Frontend build failed"
    }
    
    $EndTime = Get-Date
    $Duration = $EndTime - $StartTime
    
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Green
    Write-Host "BUILD COMPLETED SUCCESSFULLY!" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Green
    Write-Host "Total build time: $($Duration.TotalMinutes.ToString('F2')) minutes" -ForegroundColor Green
    Write-Host "Backend: Built and tested" -ForegroundColor Green
    Write-Host "Frontend: Built and tested" -ForegroundColor Green
    
} catch {
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Red
    Write-Host "BUILD FAILED!" -ForegroundColor Red
    Write-Host "========================================" -ForegroundColor Red
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
} finally {
    Set-Location $OriginalLocation
}
