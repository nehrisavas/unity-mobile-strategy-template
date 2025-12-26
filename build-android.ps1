# Unity Android Build Script
Add-Type -AssemblyName System.Windows.Forms

$wshell = New-Object -ComObject wscript.shell

# Find Unity window
$processes = Get-Process | Where-Object { $_.MainWindowTitle -like "*Unity*" -and $_.MainWindowTitle -like "*EmpireWars*" }

if (-not $processes) {
    Write-Host "Unity penceresi bulunamadi!"
    exit 1
}

$title = $processes[0].MainWindowTitle
Write-Host "Unity bulundu: $title"

# Activate Unity
$wshell.AppActivate($title)
Start-Sleep -Milliseconds 500

# Exit play mode if running
Write-Host "Play modundan cikiliyor..."
$wshell.SendKeys('{ESCAPE}')
Start-Sleep -Milliseconds 500
$wshell.SendKeys('^p')
Start-Sleep -Seconds 3

# Wait for script compilation
Write-Host "Script derlenmesi bekleniyor (8 saniye)..."
Start-Sleep -Seconds 8

# Reactivate Unity
$wshell.AppActivate($title)
Start-Sleep -Milliseconds 500

# Execute Build Android APK using shortcut: Ctrl+Shift+Alt+A
Write-Host "Android build baslatiliyor (Ctrl+Shift+Alt+A)..."
$wshell.SendKeys('^+%a')
Start-Sleep -Milliseconds 500

Write-Host "Build komutu gonderildi!"
Write-Host "Unity Console'da build durumunu takip edin."
Write-Host "APK konumu: Builds/Android/EmpireWars.apk"
