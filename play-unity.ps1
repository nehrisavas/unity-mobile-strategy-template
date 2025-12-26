Add-Type -AssemblyName System.Windows.Forms

# Find Unity window
$processes = Get-Process | Where-Object { $_.MainWindowTitle -like "*Unity*" }

if ($processes) {
    $wshell = New-Object -ComObject wscript.shell
    foreach ($proc in $processes) {
        Write-Host "Found: $($proc.MainWindowTitle)"
        $result = $wshell.AppActivate($proc.MainWindowTitle)
        if ($result) {
            Start-Sleep -Milliseconds 500
            # Ctrl+P to toggle Play mode
            $wshell.SendKeys('^p')
            Write-Host "Play mode toggled!"
            break
        }
    }
} else {
    Write-Host "Unity window not found!"
}
