# Deploy BMYLBH2025_SDDAP Application (Fixed paths)
param(
    [Parameter(Mandatory=$true)]
    [ValidateSet("Staging", "Production")]
    [string]$Environment,
    
    [string]$Configuration = "Release",
    [switch]$SkipTests = $false
)

Write-Host "=== BMYLBH2025_SDDAP Deployment Script ===" -ForegroundColor Green
Write-Host "Environment: $Environment" -ForegroundColor Yellow
Write-Host "Configuration: $Configuration" -ForegroundColor Yellow

try {
    # Pre-deployment checks
    if (-not $SkipTests) {
        Write-Host "Running pre-deployment tests..." -ForegroundColor Yellow
        & "$PSScriptRoot/../test/run-all-tests.ps1" -Configuration $Configuration
        if ($LASTEXITCODE -ne 0) {
            throw "Pre-deployment tests failed"
        }
    }
    
    # Create deployment directory
    $DeploymentPath = "deployment/$Environment"
    if (Test-Path $DeploymentPath) {
        Remove-Item $DeploymentPath -Recurse -Force
    }
    New-Item -ItemType Directory -Path $DeploymentPath -Force | Out-Null
    
    # Deploy Backend
    Write-Host "Deploying Backend API..." -ForegroundColor Yellow
    $BackendSource = "Backend/BMYLBH2025_SDDAP/BMYLBH2025_SDDAP/bin"
    $BackendDest = "$DeploymentPath/backend"
    
    if (Test-Path $BackendSource) {
        Copy-Item $BackendSource $BackendDest -Recurse -Force
        Write-Host "Backend deployed to: $BackendDest" -ForegroundColor Green
    } else {
        throw "Backend build artifacts not found: $BackendSource"
    }
    
    # Deploy Frontend
    Write-Host "Deploying Frontend Windows Forms..." -ForegroundColor Yellow
    $FrontendSource = "Frontend/BMYLBH2025_SDDAP/BMYLBH2025_SDDAP/bin/$Configuration"
    $FrontendDest = "$DeploymentPath/frontend"
    
    if (Test-Path $FrontendSource) {
        Copy-Item $FrontendSource $FrontendDest -Recurse -Force
        Write-Host "Frontend deployed to: $FrontendDest" -ForegroundColor Green
    } else {
        throw "Frontend build artifacts not found: $FrontendSource"
    }
    
    # Create deployment manifest
    $Manifest = @{
        DeploymentDate = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
        Environment = $Environment
        Configuration = $Configuration
        BackendPath = $BackendDest
        FrontendPath = $FrontendDest
        Version = "1.0.0"
    }
    
    $Manifest | ConvertTo-Json | Out-File "$DeploymentPath/deployment-manifest.json" -Encoding UTF8
    
    Write-Host ""
    Write-Host "=== Deployment Completed Successfully ===" -ForegroundColor Green
    Write-Host "Environment: $Environment" -ForegroundColor Green
    Write-Host "Deployment Path: $DeploymentPath" -ForegroundColor Green
    Write-Host "Backend: $BackendDest" -ForegroundColor Green
    Write-Host "Frontend: $FrontendDest" -ForegroundColor Green
    
} catch {
    Write-Host "Deployment Failed: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}
