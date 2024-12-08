

function Get-performanceData {
    
    $counterSet = @(
        "\Processor(_Total)\% Processor Time",
        "\Memory\% Committed Bytes In Use",
        "\PhysicalDisk(_Total)\Disk Read Bytes/sec")

    $performanceData = Get-Counter `
        -Counter $counterSet `
        -SampleInterval 2`
    -MaxSamples 60
    $currentLocation = Get-Location;
    $performanceData | Export-Clixml -Path "$currentLocation\log\baseline-perfom.xml"
}

Get-performanceData