﻿# 8.9 - Configuring VM and storage movement

# Run on HV1

# 0. Revert VMs before starting
Stop-VM -Name HVDirect -ComputerName HV2 -Force
Remove-VM -Name HVDirect -ComputerName HV2 -Force
Start-VM -VMName HVDirect -ComputerName HV1 

# 1. View the HVDirect VM on HV1 and verify that it is running and not saved
Get-VM -Name HVDirect 

# 2. Get the VM configuration location 
(Get-VM -Name HVDirect).ConfigurationLocation 

# 3. Get Hard Drive locations
Get-VMHardDiskDrive -VMName HVDirect | 
  Format-Table -Property VMName, ControllerType, Path

# 4. Move the VMs to the C:\HVD_NEW folder
$MHT = @{
  Name                   = 'HVDirect'
  DestinationStoragePath = 'C:\HVD_NEW'
}
Move-VMStorage @MHT

# 5. View the configuration details after moving the VM's storage
(Get-VM -Name HVDirect).ConfigurationLocation
Get-VMHardDiskDrive -VMName HVDirect | 
  Format-Table -Property VMName, ControllerType, Path
  
# 6. Get the VM details for VMs from HV2
Get-VM -ComputerName HV2

# 7. Enable VM migration on both HV1 and HV2
Enable-VMMigration -ComputerName HV1, HV2

# 8. Configure VM Migration on both hosts
$SVHT = @{
  UseAnyNetworkForMigration                 = $true
  ComputerName                              = 'HV1', 'HV2'
  VirtualMachineMigrationAuthenticationType = 'Kerberos'
  VirtualMachineMigrationPerformanceOption  = 'Compression'
}
Set-VMHost @SVHT

# 9. Move the VM to HV2
$Start = Get-Date
$VMHT = @{
    Name                   = 'HVDirect'
    ComputerName           = 'HV1'
    DestinationHost        = 'HV2'
    IncludeStorage         =  $true
    DestinationStoragePath = 'C:\HVDirect' # on HV2
}
Move-VM @VMHT
$Finish = Get-Date


# 10. Display the time taken to migrate
$OS = "Migration took: [{0:n2}] minutes"
$OS -f ($($Finish-$Start).TotalMinutes)

# 11. Check the VMs on HV1
Get-VM -ComputerName HV1

# 12. Check the VMs on HV2
Get-VM -ComputerName HV2

# 13. Look at the details of the moved VM
(Get-VM -Name HVDirect -Computer HV2).ConfigurationLocation
Get-VMHardDiskDrive -VMName HVDirect -Computer HV2  |
  Format-Table -Property VMName, Path

###  Move it back (not for publication)

# 14.  Move the VM to HV1
$Start = Get-Date
$VMHT2 = @{
    Name                   = 'PSDirect'
    ComputerName           = 'HV2'
    DestinationHost        = 'HV1'
    IncludeStorage         =  $true
    DestinationStoragePath = 'C:\vm\vhds\PSDirect' # on HV1
}
Move-VM @VMHT2
$Finish = Get-Date
($Finish - $Start)

# 15. Display the time taken to migrate
$OS = "Migration took: [{0:n2}] minutes"
($os -f ($($finish-$start).TotalMinutes))
