# Get the directory where this script is located
$scriptDirectory = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent
$outputFile = Join-Path -Path $scriptDirectory -ChildPath "DirectoryContents.txt"

# Function to create a detailed representation of directory contents, excluding .git directories
function Write-DirectoryContents {
    param (
        [string]$path,
        [int]$indentLevel = 0
    )
    $indentation = " " * ($indentLevel * 4)
    $items = Get-ChildItem -Path $path -Force -Recurse | Where-Object { $_.FullName -notmatch "\\\.git($|\\)" }

    foreach ($item in $items) {
        if ($item.PSIsContainer) {
            # Write directory information
            "$indentation[Directory] $($item.FullName)" | Out-File -FilePath $outputFile -Append -Encoding UTF8
        } else {
            # Write file information
            "$indentation[File] $($item.FullName) (Size: $($item.Length) bytes, Created: $($item.CreationTime), Last Modified: $($item.LastWriteTime))" | Out-File -FilePath $outputFile -Append -Encoding UTF8
        }
    }
}

# Clear any existing content in the output file
Clear-Content -Path $outputFile -ErrorAction SilentlyContinue

# Write the header
"Contents of Directory: $scriptDirectory" | Out-File -FilePath $outputFile -Encoding UTF8
"Generated on: $(Get-Date)" | Out-File -FilePath $outputFile -Append -Encoding UTF8
"" | Out-File -FilePath $outputFile -Append -Encoding UTF8 # Add a blank line

# Call the function to write directory contents
Write-DirectoryContents -path $scriptDirectory

Write-Host "Directory contents (excluding .git directories) have been written to $outputFile with context."
