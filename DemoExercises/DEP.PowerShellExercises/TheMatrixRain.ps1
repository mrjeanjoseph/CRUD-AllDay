# Define the characters to use in the rain
$chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789$%&@#^*()_+"

# Set the size of the console window
$width = $Host.UI.RawUI.WindowSize.Width
$height = $Host.UI.RawUI.WindowSize.Height

# Function to generate a random string of characters
function Get-RandomString {
    param($length)
    $string = "";

    $ranNum = Get-Random -Minimum 1 -Maximum $chars.Length

    for ($i = 0; $i -lt $length; $i++) {
        $string += $chars[$ranNum]
    }
    return $string
}

# Loop to create the rain effect
while ($true) {
    # Generate random drops of rain
    for ($i = 0; $i -lt $width; $i++) {
        $x = Get-Random -Maximum $width
        $y = 0
        $length = Get-Random -Minimum 5 -Maximum 15
        $string = Get-RandomString $length
        Write-Host -NoNewLine " " * $x
        Write-Host -ForegroundColor Green $string
        Start-Sleep -Milliseconds 750
    }

    # Sleep to control the speed of the rain
    Start-Sleep -Milliseconds 200

    # Clear the console
    Clear-Host
}
