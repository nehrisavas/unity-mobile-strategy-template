Add-Type -AssemblyName System.Windows.Forms

$wshell = New-Object -ComObject wscript.shell

# Find Unity processes
$unityProcs = Get-Process | Where-Object { $_.ProcessName -like "*Unity*" -and $_.MainWindowTitle -like "*EmpireWars*" }

if ($unityProcs) {
    Write-Host "Unity kapatiliyor..."

    foreach ($proc in $unityProcs) {
        Write-Host "Kapatiliyor: $($proc.MainWindowTitle)"

        # Activate and send Ctrl+Q (quit)
        $wshell.AppActivate($proc.MainWindowTitle)
        Start-Sleep -Milliseconds 500

        # Close any dialogs first
        $wshell.SendKeys('{ESCAPE}')
        Start-Sleep -Milliseconds 300
        $wshell.SendKeys('{ESCAPE}')
        Start-Sleep -Milliseconds 300

        # Quit Unity
        $wshell.SendKeys('^q')
        Start-Sleep -Seconds 2

        # If save dialog appears, click Don't Save (usually 'd' key or Tab+Enter)
        $wshell.SendKeys('d')
        Start-Sleep -Milliseconds 500
    }

    # Wait for Unity to close
    Start-Sleep -Seconds 5

    # Check if closed
    $stillRunning = Get-Process | Where-Object { $_.ProcessName -like "*Unity*" -and $_.MainWindowTitle -like "*EmpireWars*" }
    if ($stillRunning) {
        Write-Host "Unity hala calisiyor, zorla kapatiliyor..."
        $stillRunning | Stop-Process -Force
    }

    Write-Host "Unity kapatildi."
} else {
    Write-Host "Unity zaten kapali."
}
