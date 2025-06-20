# BMYLBH2025_SDDAP Backend Docker Build Script
# Week 11: Containerization - Build Script

param(
    [string]$Tag = "latest",
    [string]$Registry = "",
    [switch]$Push = $false,
    [switch]$Clean = $false,
    [switch]$Verbose = $false
)

# Script configuration
$ErrorActionPreference = "Stop"
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$ProjectRoot = Split-Path -Parent (Split-Path -Parent $ScriptDir)
$DockerFile = Join-Path $ProjectRoot "Dockerfile.backend"

# Set image name
$ImageName = "bmylbh2025/backend-api"
$FullImageName = if ($Registry) { "$Registry/$ImageName" } else { $ImageName }
$FullTag = "${FullImageName}:${Tag}"

Write-Host "🐳 BMYLBH2025_SDDAP Backend Docker Build" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "📦 Building: $FullTag" -ForegroundColor Yellow

# Check Docker is available
try {
    $dockerVersion = docker --version
    Write-Host "✅ Docker: $dockerVersion" -ForegroundColor Green
}
catch {
    Write-Error "❌ Docker is not installed or not in PATH"
    exit 1
}

# Check Dockerfile exists
if (-not (Test-Path $DockerFile)) {
    Write-Error "❌ Dockerfile not found: $DockerFile"
    exit 1
}

# Build Docker image
Write-Host "🔨 Building Docker image..." -ForegroundColor Yellow
$buildStart = Get-Date

$buildArgs = @(
    "build",
    "--file", $DockerFile,
    "--tag", $FullTag,
    "--platform", "windows/amd64",
    $ProjectRoot
)

if ($Verbose) {
    $buildArgs = $buildArgs + "--progress=plain" + "--no-cache"
}

& docker @buildArgs

if ($LASTEXITCODE -ne 0) {
    Write-Error "❌ Docker build failed with exit code $LASTEXITCODE"
    exit $LASTEXITCODE
}

$buildTime = (Get-Date) - $buildStart
Write-Host "✅ Build completed in $($buildTime.TotalMinutes.ToString('F1')) minutes" -ForegroundColor Green

# Get image info
$imageInfo = docker images $FullTag --format "table {{.Repository}}\t{{.Tag}}\t{{.Size}}\t{{.CreatedAt}}"
Write-Host ""
Write-Host "📦 Image Details:" -ForegroundColor Cyan
Write-Host $imageInfo

Write-Host ""
Write-Host "🎉 Backend Docker build completed successfully!" -ForegroundColor Green
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "  • Test: docker run -p 8080:80 $FullTag" -ForegroundColor White
Write-Host "  • Health: http://localhost:8080/api/health" -ForegroundColor White
