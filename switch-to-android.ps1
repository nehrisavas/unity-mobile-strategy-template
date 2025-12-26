Add-Type -AssemblyName System.Windows.Forms

$wshell = New-Object -ComObject wscript.shell

# Find Unity window
$processes = Get-Process | Where-Object { $_.MainWindowTitle -like "*Unity*" -and $_.MainWindowTitle -like "*EmpireWars*" }

if (-not $processes) {
    Write-Host "Unity bulunamadi!"
    exit 1
}

$title = $processes[0].MainWindowTitle
Write-Host "Unity: $title"

# Activate Unity
$wshell.AppActivate($title)
Start-Sleep -Milliseconds 500

# Open Build Settings with Ctrl+Shift+B
Write-Host "Build Settings aciliyor (Ctrl+Shift+B)..."
$wshell.SendKeys('^+b')
Start-Sleep -Seconds 2

# In Build Settings, navigate to Android platform
# Press Tab to go to platform list, then arrow keys to find Android
Write-Host "Android platform seciliyor..."
$wshell.SendKeys('{TAB}')
Start-Sleep -Milliseconds 200
$wshell.SendKeys('{TAB}')
Start-Sleep -Milliseconds 200
$wshell.SendKeys('{TAB}')
Start-Sleep -Milliseconds 200

# Type 'a' to jump to Android in the list
$wshell.SendKeys('a')
Start-Sleep -Milliseconds 500

# Click Switch Platform button (Alt+S usually)
Write-Host "Switch Platform butonuna basiliyor..."
$wshell.SendKeys('%s')
Start-Sleep -Milliseconds 500

Write-Host "Platform degistirme komutu gonderildi!"
Write-Host "Bu islem birkaç dakika surebilir..."
