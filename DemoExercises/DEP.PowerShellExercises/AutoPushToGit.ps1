#The idea here is to implement a sort of CICD where a scheduled tasks runs this scripts 10 - 15 times per day.

function Get-GitStatus {
    param ( [string]$Path, [string]$Word )

    # Save the current location
    $currentLocation = Get-Location;

    try {
        # Navigate to the specified folder
        Set-Location -Path $Path;
        # Capture the output of the command 

        $output = Write-Output (git status);
        # Check if the output contains the specified word 

        #Run git operation if there are untracked files in the project
        if ($output[6] -match $Word) { 
            Write-Output "Keyword '$Word' = '$output[6]' found.";

            git add .
            git commit -m 'Changes found. CD to remote'
            git push

        }
        else { 
            Write-Output "The output does not contain the word '$Word'.";
        }
        # Output the Git status
        #Write-Output $gitStatus;
    }
    catch {
        Write-Error "An error occurred: $_";
    }
    finally {
        # Return to the original location
        Set-Location -Path $currentLocation;
    }
}
Get-GitStatus -Path "D:\_workspace\CRUD-AllDay" -word "Modified";
#

