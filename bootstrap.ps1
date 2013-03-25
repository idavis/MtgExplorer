param(
  [Parameter( Position = 0, Mandatory = 0 )]
  [string] $path = ((Resolve-Path "$(Split-Path -parent $MyInvocation.MyCommand.Definition)").Path),
  [Parameter( Position = 1, Mandatory = 0 )]
  [string] $install_to = 'lib'
)

function script:FileExistsInPath {
  param (
    [Parameter(Position=0,Mandatory=$true)]
    [string] $fileName = $null
  )

  $path = Get-Childitem Env:Path
  $found = $false
  foreach ($folder in $path.Value.Split(";")) { if (Test-Path "$folder\$fileName") { $found = $true; break } }
  return $found
}

function Resolve-NuGet {
  $nuGetIsInPath = (FileExistsInPath "NuGet.exe") -or (FileExistsInPath "NuGet.bat")
  $nuget = "NuGet"
  if($nuGetIsInPath) {
    $nuget = (@(get-command nuget) | % {$_.Definition} | ? { (Test-Path $_) } | Select-Object -First 1)
  } else {  
    $nugets = @(Get-ChildItem "..\*" -recurse -include NuGet.exe)
    if ($nugets.Length -le 0) { 
      Write-Output "No NuGet executables found."
	  Write-Output "Downloading latest Nuget.exe."
	  $webClient = New-Object System.Net.WebClient
      $webClient.DownloadFile("https://nuget.org/nuget.exe", "$(Join-Path $path "NuGet.exe")");
      return Resolve-NuGet
    }
    $nuget = (Resolve-Path $nugets[0]).Path
    $env:Path = $env:Path + ";" + (Split-Path $nuget)
  }
  return $nuget
}

Write-Output "Loading Nuget Dependencies"
$nuget = Resolve-NuGet
Write-Output "Nuget = $nuget"
$package_files = Get-ChildItem . -recurse -include packages.config
$package_files | % { & $nuget i $_ -OutputDirectory $install_to }