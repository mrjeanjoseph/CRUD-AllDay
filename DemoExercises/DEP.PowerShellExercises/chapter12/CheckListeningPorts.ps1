#Reviewing firewall configurations is crucial for maintaining security
Get-NetTCPConnection | ForEach-Object {
    if($_.OwningProcess -eq 0){
        Write-Host $_.LocalAddress $_.LocalPort $_.RemoteAddress $_.RemotePort -ForegroundColor Green;
    } else {
        Write-Host $_.LocalAddress $_.LocalPort $_.RemoteAddress $_.RemotePort -ForegroundColor Red;
    }
}