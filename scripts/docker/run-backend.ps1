# BMYLBH2025_SDDAP Backend Docker Run Script
# Week 11: Containerization - Run Script

param(
    [string]$Tag = "latest",
    [string]$Port = "8080",
    [string]$ContainerName = "bmylbh2025-backend",
    [switch]$Detached = $true,
    [switch]$Remove = $true
)

$ImageName = "bmylbh2025/backend-api:$Tag"

Write-Host "🚀 Starting BMYLBH2025_SDDAP Backend Container" -ForegroundColor Cyan
Write-Host "===============================================" -ForegroundColor Cyan

# Stop existing container if running
$existingContainer = docker ps -a --filter "name=$ContainerName" --format "{{.ID}}"
if ($existingContainer) {
    Write-Host "🛑 Stopping existing container: $ContainerName" -ForegroundColor Yellow
    docker stop $ContainerName | Out-Null
    docker rm $ContainerName | Out-Null
}

# Prepare run arguments
$runArgs = @(
    "run"
)

if ($Detached) { $runArgs += "-d" }
if ($Remove) { $runArgs += "--rm" }

$runArgs += @(
    "--name", $ContainerName,
    "-p", "${Port}:80",
    "--env", "ASPNETCORE_ENVIRONMENT=Production",
    "--env", "IsContainerMode=true",
    $ImageName
)

Write-Host "🐳 Running: docker $($runArgs -join ' ')" -ForegroundColor Gray

# Run container
& docker @runArgs

if ($LASTEXITCODE -ne 0) {
    Write-Error "❌ Failed to start container"
    exit $LASTEXITCODE
}

Write-Host "✅ Container started successfully!" -ForegroundColor Green
Write-Host "📍 Backend API: http://localhost:$Port" -ForegroundColor Cyan
Write-Host "🏥 Health Check: http://localhost:$Port/api/health" -ForegroundColor Cyan

if ($Detached) {
    Write-Host ""
    Write-Host "Container commands:" -ForegroundColor Yellow
    Write-Host "  • View logs: docker logs $ContainerName" -ForegroundColor White
    Write-Host "  • Stop: docker stop $ContainerName" -ForegroundColor White
    Write-Host "  • Shell: docker exec -it $ContainerName powershell" -ForegroundColor White
}
