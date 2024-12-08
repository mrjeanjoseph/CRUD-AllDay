$scriptBlock = {
    $monitoringData = @()
    $cpuUsage = Get-Counter `
        -Counter "\Processor(_Total)\% Processor Time" `
        -ErrorAction SilentlyContinue
    $monitoringData += [PSCustomObject]@{
        ResourceType = "CPU Usage"
        Value        = "$($cpuUsage.CounterSamples.CookedValue)%"
    }
    $memoryUsage = Get-Counter `
        -Counter "\Memory\Available MBytes" `
        -ErrorAction SilentlyContinue
    $monitoringData += [PSCustomObject]@{
        ResourceType = "Available Memory"
        Value        = "$($memoryUsage.CounterSamples.CookedValue) MB"
    }
    $diskUsage = Get-PSDrive -PSProvider FileSystem
    foreach ($disk in $diskUsage) {
        $monitoringData += [PSCustomObject]@{
            ResourceType = "Disk Space on Drive $($disk.Name)"
            Value        = "Used - $($disk.Used / 1GB) GB, Free - $($disk.Free / 1GB) GB"
        }
    }
    $networkTraffic = Get-Counter `
        -Counter "\Network Interface(*)\Bytes Total/sec" `
        -ErrorAction SilentlyContinue
    foreach ($interface in $networkTraffic.CounterSamples) {
        $monitoringData += [PSCustomObject]@{
            ResourceType = "Network Traffic on $($interface.InstanceName)"
            Value        = "$($interface.CookedValue) Bytes/sec"
        }
    }
    $monitoringData | Export-Clixml -Path "C:\PowerShell\MonitoringData.xml"
}
$scriptPath = "C:\PowerShell\MonitoringData.ps1"
$scriptBlock | Out-File -FilePath $scriptPath
$trigger = New-ScheduledTaskTrigger `
    -AtStartup -RandomDelay (New-TimeSpan -Minutes 30)
$action = New-ScheduledTaskAction `
    -Execute "`"C:\Program Files\PowerShell\7\pwsh.exe`"" `
    -Argument "-File `"$scriptPath`""
Register-ScheduledTask `
    -TaskName "SystemPerformanceMonitoring" `
    -Trigger $trigger `
    -Action $action `
    -Description "System Performance Monitoring Task"