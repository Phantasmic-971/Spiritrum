$sourceRoot = $PSScriptRoot + "\MagnoliaAddon"

# Files that need fixing due to missing RewinderPlaced reference
$filesToFix = @(
    "Items\Placeables\AncientCopperOre.cs",
    "Items\Placeables\AncientGoldOre.cs",
    "Items\Placeables\AncientIronOre.cs",
    "Items\Placeables\AncientSilverOre.cs",
    "Items\Placeables\AncientStoneBlock.cs",
    "Items\Placeables\AncientWood.cs"
)

foreach ($file in $filesToFix) {
    $fullPath = Join-Path -Path $sourceRoot -ChildPath $file
    
    if (Test-Path $fullPath) {
        Write-Host "Fixing file: $fullPath"
        
        # Read the content
        $content = Get-Content -Path $fullPath -Raw
        
        # Replace the AddTile reference to RewinderPlaced
        $content = $content -replace "recipe\.AddTile<RewinderPlaced>\(\);", "recipe.AddTile(TileID.WorkBenches);"
        
        # Write the updated content back
        Set-Content -Path $fullPath -Value $content
        
        Write-Host "File updated: $fullPath"
    } else {
        Write-Host "Warning: File not found: $fullPath"
    }
}

# Files with special placeable tiles
$specialFiles = @{
    "Items\Placeables\AncientFurnace.cs" = @{
        oldText = "Item.DefaultToPlaceableTile\(ModContent.TileType<AncientFurnacePlaced>\(\)\);";
        newText = "Item.createTile = TileID.Furnaces;";
    };
    "Items\Placeables\AncientHellforge.cs" = @{
        oldText = "Item.DefaultToPlaceableTile\(ModContent.TileType<AncientHellforgePlaced>\(\)\);";
        newText = "Item.createTile = TileID.Hellforge;";
    };
}

foreach ($file in $specialFiles.Keys) {
    $fullPath = Join-Path -Path $sourceRoot -ChildPath $file
    
    if (Test-Path $fullPath) {
        Write-Host "Fixing special file: $fullPath"
        
        # Read the content
        $content = Get-Content -Path $fullPath -Raw
        
        # Replace the DefaultToPlaceableTile reference
        $content = $content -replace $specialFiles[$file].oldText, $specialFiles[$file].newText
        
        # Also fix the AddTile reference to RewinderPlaced
        $content = $content -replace "recipe\.AddTile<RewinderPlaced>\(\);", "recipe.AddTile(TileID.WorkBenches);"
        
        # Write the updated content back
        Set-Content -Path $fullPath -Value $content
        
        Write-Host "Special file updated: $fullPath"
    } else {
        Write-Host "Warning: Special file not found: $fullPath"
    }
}

Write-Host "All file updates complete!"
