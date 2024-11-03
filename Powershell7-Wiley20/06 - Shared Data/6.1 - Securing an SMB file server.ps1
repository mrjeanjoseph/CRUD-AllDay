﻿# 6.1 - Setting Up and Securing your SMB file Server

# Run on FS1

# 1. Add File Server features to FS1
Import-Module -Name ServerManager -WarningAction SilentlyContinue
$Features = 'FileAndStorage-Services',
            'File-Services',
            'FS-FileServer'
Install-WindowsFeature -Name $Features -IncludeManagementTools

# 2. Get SMB Server Settings
Get-SmbServerConfiguration

# 3. Ensure SMB1 is turned off
$CHT = @{
  EnableSMB1Protocol = $false 
  Confirm            = $false
}
Set-SmbServerConfiguration @CHT

# 4. Turn on SMB signing and encryption
$SHT1 = @{
    RequireSecuritySignature = $true
    EnableSecuritySignature  = $true
    EncryptData              = $true
    Confirm                  = $false
}
Set-SmbServerConfiguration @SHT1

# 5. Turn off default server and workstations shares 
$SHT2 = @{
    AutoShareServer       = $false
    AutoShareWorkstation  = $false
    Confirm               = $false
}
Set-SmbServerConfiguration @SHT2

# 6. Turn off server announcements
$SHT3 = @{
    ServerHidden   = $true
    AnnounceServer = $false
    Confirm        = $false
}
Set-SmbServerConfiguration @SHT3

# 7. Restart the service with the new configuration
Restart-Service LanmanServer

# 8. Review SMB Server Configuration 
Get-SmbServerConfiguration





<# undo and set back to defults

Get-SMBShare foo* | remove-SMBShare -Confirm:$False

Set-SmbServerConfiguration -EnableSMB1Protocol $true `
                           -RequireSecuritySignature $false `
                           -EnableSecuritySignature $false `
                           -EncryptData $False `
                           -AutoShareServer $true `
                           -AutoShareWorkstation $false `
                           -ServerHidden $False `
                           -AnnounceServer $True
Restart-Service lanmanserver
#>




# 6. Turn on Sserver announcements
$SHT3 = @{
    ServerHidden   = $TRUE
    AnnounceServer = $FALSE
    Confirm        = $false
    AnnounceComment = "Have A Nice Day!"
}
Set-SmbServerConfiguration @SHT3


$t1 = 56507.941
$t2 = 57229.408
$t3 = 57948.166
$T4 = 58666.928
$T4-$t3
$T3-$t2
$T2-$t1
