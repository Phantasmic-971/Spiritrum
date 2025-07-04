$sourceRoot = $PSScriptRoot + "\MagnoliaAddon"

# Function to process files recursively
function Process-Files {
    param (
        [string]$directory
    )
    
    # Get all .cs files in the current directory
    $files = Get-ChildItem -Path $directory -Filter "*.cs"
    
    foreach ($file in $files) {
        $fullPath = $file.FullName
        Write-Host "Processing file: $fullPath"
        
        # Read the content
        $content = Get-Content -Path $fullPath -Raw
        
        # Replace "namespace Spiritrum.MagnoliaAddon.Content." with "namespace Spiritrum.MagnoliaAddon."
        $updatedContent = $content -replace "namespace Spiritrum\.MagnoliaAddon\.Content\.", "namespace Spiritrum.MagnoliaAddon."
        
        # Replace "using Spiritrum.MagnoliaAddon.Content." with "using Spiritrum.MagnoliaAddon."
        $updatedContent = $updatedContent -replace "using Spiritrum\.MagnoliaAddon\.Content\.", "using Spiritrum.MagnoliaAddon."
        
        # Write the updated content back if changes were made
        if ($content -ne $updatedContent) {
            Set-Content -Path $fullPath -Value $updatedContent
            Write-Host "Updated namespace in: $fullPath"
        }
        else {
            Write-Host "No changes needed in: $fullPath"
        }
    }
    
    # Process subdirectories
    $subdirs = Get-ChildItem -Path $directory -Directory
    foreach ($subdir in $subdirs) {
        Process-Files -directory $subdir.FullName
    }
}

# Start processing from the root directory
Process-Files -directory $sourceRoot

Write-Host "All files have been processed successfully!"
