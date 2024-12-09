#-------------------------------------------------------------
#Currently not working but want to make use of it.
function Rename-AllFiles {
    param ( [string]$Path, [string]$Prefix )

    try {
        # Get all files in the specified directory
        $files = Get-ChildItem -Path $Path -File

        # Rename each file by adding the prefix
        foreach ($file in $files) {
            $newName = $Prefix + $file.Name
            Rename-Item -Path $file.FullName -NewName $newName
        }

        Write-Output "All files in the directory '$Path' have been renamed with the prefix '$Prefix'."
    }
    catch {
        Write-Error "An error occurred: $_"
    }

}