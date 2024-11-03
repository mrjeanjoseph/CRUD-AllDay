﻿# 10.8 - Creating a Hyper-V Status Report

# Run on HV1 after creating HVDirect

# 1. Create a basic report object hash table
$ReportHT = [Ordered] @{}

# 2. Get the host details and add them to the report hash table
$HostDetails = Get-CimInstance -ClassName Win32_ComputerSystem
$ReportHT.HostName = $HostDetails.Name
$ReportHT.Maker = $HostDetails.Manufacturer
$ReportHT.Model = $HostDetails.Model

# 3. Add the PowerShell and OS version information
$ReportHT.PSVersion = $PSVersionTable.PSVersion.ToString()
# Add OS information
$OS = Get-CimInstance -Class Win32_OperatingSystem
$ReportHT.OSEdition    = $OS.Caption
$ReportHT.OSArch       = $OS.OSArchitecture
$ReportHT.OSLang       = $OS.OSLanguage
$ReportHT.LastBootTime = $OS.LastBootUpTime
$Now = Get-Date
$UTD = [float] ("{0:n3}" -f (($Now -$OS.LastBootUpTime).Totaldays))
$ReportHT.UpTimeDays = $UTD

# 4. Add a count of processors in the host
$PHT = @{
    ClassName  = 'MSvm_Processor'
    Namespace  = 'root/virtualization/v2'
}
$Proc = Get-CimInstance @PHT
$ReportHT.CPUCount = ($Proc |
    Where-Object elementname -match 'Logical Processor').count

# 5. Add the current host CPU usage
$Cname = '\processor(_total)\% processor time'
$CPU = Get-Counter -Counter $Cname
$ReportHT.HostCPUUsage = $CPU.CounterSamples.CookedValue

# 6. Add the total host physical memory
$Memory = Get-CimInstance -Class Win32_ComputerSystem
$HostMemory = [float] ( "{0:n2}" -f ($Memory.TotalPhysicalMemory/1GB))
$ReportHT.HostMemoryGB = $HostMemory

# 7. Add the memory allocated to VMs
$Sum = 0
Get-VM | Foreach-Object {$Sum += $_.MemoryAssigned}
$Sum = [float] ( "{0:N2}" -f ($Sum/1gb) )
$ReportHT.AllocatedMemoryGB = $Sum

# 8. Create the host report object
$Reportobj = New-Object -TypeName PSObject -Property $ReportHT

# 9. Create report header
$Report =  "Hyper-V Report for: $(hostname)`n"
$Report += "At: [$(Get-Date)]"

# 10. Add report object to report
$Report += $Reportobj | Out-String

# 11. Create VM details array
#     VM related objects:
$VMs = Get-VM -Name *
$VMHT = @()

# 12. Get VM details
Foreach ($VM in $VMs) {
  # Create VM Report hash table
  $VMReport = [ordered] @{}
  # Add VM's Name
  $VMReport.VMName = $VM.VMName
  # Add Status
  $VMReport.Status = $VM.Status
  # Add Uptime
  $VMReport.Uptime = $VM.Uptime
  # Add VM CPU
  $VMReport.VMCPU = $VM.ProcessorCount
  # Replication Mode/Status
  $VMReport.ReplMode = $VM.ReplicationMode
  $VMReport.ReplState = $Vm.ReplicationState
  # Create object from Hash table, add to array
  $VMR = New-Object -TypeName PSObject -Property $VMReport
  $VMHT += $VMR
}

# 13. Finish creating the report
$Report += $VMHT | Format-Table | Out-String

# 14. Display the report:
$Report
