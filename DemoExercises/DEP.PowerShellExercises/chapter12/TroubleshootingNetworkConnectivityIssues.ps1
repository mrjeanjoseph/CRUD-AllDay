
#excludes all IP version 6 addresses and returns IP version 4 addresses plus the current DNS Server entries:
Get-NetAdapter | `
Select-Object `
-Property Name, LinkSpeed, Status,
@{ 
    Name = "IPv4 Address";
    Expression = { 
        (Get-NetIPAddress -InterfaceIndex $_.ifIndex -AddressFamily IPv4).IPAddress
    }
}, @{ 
    Name = "IPv4 DNS Servers";
    Expression = { 
        (Get-DnsClientServerAddress -InterfaceIndex $_.ifIndex -AddressFamily IPv4).ServerAddresses
    }
} | Format-Table -AutoSize


#send a network request (like a ping) to test a route’s functionality to the destination:
$routeEntries = Get-NetRoute | Where-Object { $_.DestinationPrefix -notmatch "^127\.0\.0\." -and $_.AddressFamily -eq 2 }
foreach ($route in $routeEntries) {
    $destination = ($route.DestinationPrefix -split '/')[0]
    if ($destination -eq "0.0.0.0" -or $destination -eq $route.NextHop) { continue }
    Write-Host "Testing route to destination: $destination"
    $result = Test-Connection -ComputerName $destination -Count 1 -Quiet
    if ($result) {
        Write-Host "Route to $destination is working." -ForegroundColor Green
    }
    else {
        Write-Host "Route to $destination failed." -ForegroundColor Red
    }
}
# These routes are working: 
#       192.168.1.79
#       172.28.96.1
#       172.26.192.1

#For bandwidth issues, we can test network speed with Test-Connection cmdlet:
$computerName = "172.28.96.1" # Or we can use "JeanPC"
$port = 80
$tcpTestResult = Test-NetConnection `
	-ComputerName $computerName `
	-Port $port `
	-InformationLevel Detailed
$pingTestResult = Test-Connection `
	-ComputerName $computerName -Count 1
$result = [PSCustomObject]@{
	ComputerName = $tcpTestResult.ComputerName
	RemoteAddress = $tcpTestResult.RemoteAddress
	RemotePort = $tcpTestResult.RemotePort
	InterfaceAlias = $tcpTestResult.InterfaceAlias
	InterfaceDescription = $tcpTestResult.InterfaceDescription
	TcpTestSucceeded = $tcpTestResult.TcpTestSucceeded
	Latency = $pingTestResult.Latency
	PingSucceeded = $pingTestResult.StatusCode -eq 0
	PingReplyDetails = $pingTestResult
}
$result | Format-Table -AutoSize
#==================================================================

# A script to automate diagnostics and run it regularly or when issues are detected.
$scriptBlock = {    
    $outputFile = "C:\LogResults\diagnostics-results.txt";

    Test-Connection -ComputerName "JeanPC" | Out-File -FilePath $outputFile -Append
    Test-NetConnection -ComputerName "JeanPC" -Port 80 | Out-File -FilePath $outputFile -Append
    Get-NetIPAddress | Out-File -FilePath $outputFile -Append
    Resolve-DnsName "JeanPC" | Out-File -FilePath $outputFile -Append
    Get-NetRoute | Out-File -FilePath $outputFile -Append
    Get-NetAdapter | Select-Object Name, Status, LinkSpeed | Out-File -FilePath $outputFile -Append
    Get-Counter -Counter "\Network Interface(*)\Bytes Total/sec" | Out-File -FilePath $outputFile -Append
} 
& $scriptBlock

