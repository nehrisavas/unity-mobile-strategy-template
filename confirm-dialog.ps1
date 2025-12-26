Add-Type -AssemblyName System.Windows.Forms

$wshell = New-Object -ComObject wscript.shell

# Find Unity window
$processes = Get-Process | Where-Object { $_.MainWindowTitle -like "*Unity*" }

if ($processes) {
    foreach ($proc in $processes) {
        Write-Host "Pencere: $($proc.MainWindowTitle)"
        $wshell.AppActivate($proc.MainWindowTitle)
        Start-Sleep -Milliseconds 300
    }

    # Press Enter to confirm any dialog
    $wshell.SendKeys('{ENTER}')
    Write-Host "Enter gonderildi"
    Start-Sleep -Milliseconds 500
    $wshell.SendKeys('{ENTER}')
    Write-Host "Enter tekrar gonderildi"
}
