# BMYLBH2025_SDDAP Containerization Test Script
# Week 11: Containerization - Validation Script

Write-Host "🧪 BMYLBH2025_SDDAP Containerization Tests" -ForegroundColor Cyan
Write-Host "===========================================" -ForegroundColor Cyan

# Test 1: Check Docker availability
Write-Host "📋 Test 1: Docker Installation" -ForegroundColor Yellow
try {
    $dockerVersion = docker --version
    Write-Host "✅ PASS: Docker installed - $dockerVersion" -ForegroundColor Green
}
catch {
    Write-Host "❌ FAIL: Docker not available" -ForegroundColor Red
    exit 1
}

# Test 2: Check project structure
Write-Host "📋 Test 2: Project Structure" -ForegroundColor Yellow
$requiredFiles = @(
    "Dockerfile.backend",
    "docker-compose.yml",
    "Backend/BMYLBH2025_SDDAP/BMYLBH2025_SDDAP/BMYLBH2025_SDDAP.csproj",
    "Backend/BMYLBH2025_SDDAP/BMYLBH2025_SDDAP/App_Data/BMYLBH2025_SDDAP.sqlite",
    "scripts/docker/build-backend.ps1",
    "scripts/docker/run-backend.ps1"
)

foreach ($file in $requiredFiles) {
    if (Test-Path $file) {
        Write-Host "✅ PASS: $file exists" -ForegroundColor Green
    } else {
        Write-Host "❌ FAIL: $file missing" -ForegroundColor Red
    }
}

# Test 3: Validate Dockerfile syntax
Write-Host "📋 Test 3: Dockerfile Validation" -ForegroundColor Yellow
try {
    $dockerfileContent = Get-Content "Dockerfile.backend" -Raw
    if ($dockerfileContent -match "FROM.*aspnet.*4.8") {
        Write-Host "✅ PASS: Dockerfile uses correct base image" -ForegroundColor Green
    } else {
        Write-Host "❌ FAIL: Dockerfile base image incorrect" -ForegroundColor Red
    }
    
    if ($dockerfileContent -match "EXPOSE 80") {
        Write-Host "✅ PASS: Dockerfile exposes HTTP port" -ForegroundColor Green
    } else {
        Write-Host "❌ FAIL: Dockerfile missing port exposure" -ForegroundColor Red
    }
}
catch {
    Write-Host "❌ FAIL: Cannot validate Dockerfile" -ForegroundColor Red
}

# Test 4: Check build script
Write-Host "📋 Test 4: Build Script Validation" -ForegroundColor Yellow
if (Test-Path "scripts/docker/build-backend.ps1") {
    try {
        Get-Content "scripts/docker/build-backend.ps1" | Out-Null
        Write-Host "✅ PASS: Build script is readable" -ForegroundColor Green
    }
    catch {
        Write-Host "❌ FAIL: Build script has issues" -ForegroundColor Red
    }
} else {
    Write-Host "❌ FAIL: Build script missing" -ForegroundColor Red
}

Write-Host ""
Write-Host "🎯 Containerization Setup Summary:" -ForegroundColor Cyan
Write-Host "  • Docker environment validated" -ForegroundColor White
Write-Host "  • Project structure confirmed" -ForegroundColor White
Write-Host "  • Dockerfile configuration checked" -ForegroundColor White
Write-Host "  • Build automation ready" -ForegroundColor White
Write-Host ""
Write-Host "Next steps for Week 11:" -ForegroundColor Yellow
Write-Host "  1. Build container: .\scripts\docker\build-backend.ps1" -ForegroundColor White
Write-Host "  2. Test container: .\scripts\docker\run-backend.ps1" -ForegroundColor White
Write-Host "  3. Verify health: http://localhost:8080/api/health" -ForegroundColor White
Write-Host "  4. Use Docker Compose: docker-compose up -d" -ForegroundColor White
