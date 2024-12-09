#The idea here is to implement a sort of CICD where a scheduled tasks runs this scripts 10 - 15 times per day.
#New logic: Delete some of the content but not everything. Also add the date and time at the top

function Find-SourceLocation {
    param([string]$Path)

    $destFolderWrites = "DemoExercises\DEP.LearningPowerShell\log";
    $files = Get-ChildItem -Path "$Path\$destFolderWrites" -File;
    $fileCount = ($files).Count;    

    if ($fileCount -gt 3) {
        foreach ($file in $files) {
            Remove-Item -Path $file.FullName -Force 
        }
    }
    $num = Get-Random -Minimum 1 -Maximum 11
    Get-ChildItem $Path | ForEach-Object {
        $_ | Out-File "$Path\$destFolderWrites\SmallFiles-$num.txt" -Append;
    }
}

function Get-GitStatus {
    param ( [string]$Path, [string]$word )

    # Save the current location
    $currentLocation = Get-Location;
    Find-SourceLocation($Path);

    try {
        # Navigate to the specified folder
        Set-Location -Path $Path;
        # Capture the output of the command 

        $output = Write-Output (git status) 
        # Check if the output contains the specified word 

        #Run git operation if there are untracked files in the project
        if ($output -match $Word) { 
            Write-Output "The output contains the word '$Word'.";

            # Check Git status
            #$gitStatus = git status;
            git status
            git add .
            git commit -m 'project file update in progress'
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
Get-GitStatus -Path "C:\_workspace\CRUD-AllDay" -word "Untracked Files";


