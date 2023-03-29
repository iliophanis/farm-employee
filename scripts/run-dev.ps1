param (
    [switch]$obj = $false,
    [switch]$clean = $false
)
Set-Location -Path .. -PassThru
$dir = Get-Location

$paths = @(
    "server"
)

Foreach($path in $paths)
{
    $bin_path = Join-Path -path $path -ChildPath "bin"

    Remove-Item -Force -Recurse $bin_path

    if ($obj)
    {
        $obj_path = Join-Path -path $path -ChildPath "obj"

        Remove-Item -Force -Recurse $obj_path

        dotnet restore $path
    }
}

if (!$clean)
{
    Start-Process wt -ArgumentList "PowerShell.exe", 
        "-NoExit", "& {Set-Location $dir\server\; dotnet watch run --launch-profile server-Dev}",
        ";",
        "PowerShell.exe",
        "-NoExit", 
        "& {Set-Location $dir\client\; yarn dev}"
    
}

