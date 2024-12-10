
# Display the last 24 hours of entries from the system event log
Get-WinEvent -FilterHashtable @{LogName="System";StartTime=(Get-Date).AddDays(-1)}


# Filter by event type such as errors or warning
Get-WinEvent -FilterHashtable @{LogName="System";Level=1;}
Get-WinEvent -FilterHashtable @{LogName="System";ID=7001;}

#We can combine the above two queries but will have to use -FilterXml
$filterXml = @'
<QueryList>
    <Query Id="0" Path="System">
        <Select Path="System">
            *[System[(Level=2) or (EventID=0000)]]
        </Select>
    </Query>
</QueryList>
'@
Get-WinEvent -FilterXml $filterXml

#Examine message content of events
Get-WinEvent -LogName Application -MaxEvents 10 | Select-Object TimeCreated, Message

# Using Hashtable to filter the system log for errors
Get-WinEvent -FilterHashtable @{LogName="System";Level=2;StartTime=(Get-Date).AddDays(-1)}

Get-WinEvent `
    -FilterHashtable @{
        LogName="System";
        Level=2;
        StartTime=(Get-Date).AddDays(-1)
    }