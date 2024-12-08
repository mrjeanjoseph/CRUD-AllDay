# Run the arp command and capture the output
$arpOutput = arp -a

# Parse the output to extract IP addresses and MAC addresses
$devices = $arpOutput | Select-String -Pattern "(\d{1,3}\.){3}\d{1,3}" | ForEach-Object {
    $line = $_.Line
    $ip = $line -match "(\d{1,3}\.){3}\d{1,3}" | Out-Null; $matches[0]
    $mac = $line -match "([0-9A-F]{2}-){5}[0-9A-F]{2}" | Out-Null; $matches[0]
    [PSCustomObject]@{
        IPAddress = $ip
        MACAddress = $mac
    }
}

# Output the list of devices
$devices | Format-Table -AutoSize
