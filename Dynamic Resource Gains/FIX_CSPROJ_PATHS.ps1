param(
    [string]$ProjectPath = (Join-Path $PSScriptRoot 'Dynamic Resource Gains.csproj')
)

$ErrorActionPreference = 'Stop'

if (-not (Test-Path -LiteralPath $ProjectPath)) {
    throw "Nie znaleziono pliku csproj: $ProjectPath"
}

$projectDir = Split-Path -Parent $ProjectPath
$backupPath = "$ProjectPath.bak_$(Get-Date -Format 'yyyyMMdd_HHmmss')"
Copy-Item -LiteralPath $ProjectPath -Destination $backupPath -Force

[xml]$xml = Get-Content -LiteralPath $ProjectPath -Raw
$ns = New-Object System.Xml.XmlNamespaceManager($xml.NameTable)
$ns.AddNamespace('msb', 'http://schemas.microsoft.com/developer/msbuild/2003')

$managedDir = 'C:\Program Files\Epic Games\KerbalSpaceProgram\KerbalSpaceProgram\KSP_x64_Data\Managed'
$localLibDir = Join-Path $projectDir 'libs'

function Set-HintPath {
    param(
        [string]$ReferenceName,
        [string]$NewPath
    )

    $refNode = $xml.SelectSingleNode("//msb:Reference[contains(@Include,'$ReferenceName')]", $ns)
    if ($refNode -ne $null) {
        $hint = $refNode.SelectSingleNode('msb:HintPath', $ns)
        if ($hint -eq $null) {
            $hint = $xml.CreateElement('HintPath', 'http://schemas.microsoft.com/developer/msbuild/2003')
            [void]$refNode.AppendChild($hint)
        }
        $hint.InnerText = $NewPath
    }
}

function Add-ReferenceIfMissing {
    param(
        [string]$Include,
        [string]$HintPath
    )

    $existing = $xml.SelectSingleNode("//msb:Reference[contains(@Include,'$Include')]", $ns)
    if ($existing -ne $null) {
        return
    }

    $itemGroup = $xml.SelectSingleNode('//msb:Project/msb:ItemGroup[msb:Reference]', $ns)
    if ($itemGroup -eq $null) {
        $itemGroup = $xml.CreateElement('ItemGroup', 'http://schemas.microsoft.com/developer/msbuild/2003')
        [void]$xml.Project.AppendChild($itemGroup)
    }

    $reference = $xml.CreateElement('Reference', 'http://schemas.microsoft.com/developer/msbuild/2003')
    $includeAttr = $xml.CreateAttribute('Include')
    $includeAttr.Value = $Include
    [void]$reference.Attributes.Append($includeAttr)

    $specificVersion = $xml.CreateElement('SpecificVersion', 'http://schemas.microsoft.com/developer/msbuild/2003')
    $specificVersion.InnerText = 'False'
    [void]$reference.AppendChild($specificVersion)

    $hint = $xml.CreateElement('HintPath', 'http://schemas.microsoft.com/developer/msbuild/2003')
    $hint.InnerText = $HintPath
    [void]$reference.AppendChild($hint)

    $private = $xml.CreateElement('Private', 'http://schemas.microsoft.com/developer/msbuild/2003')
    $private.InnerText = 'False'
    [void]$reference.AppendChild($private)

    [void]$itemGroup.AppendChild($reference)
}

if (Test-Path -LiteralPath $localLibDir) {
    Set-HintPath -ReferenceName 'Assembly-CSharp' -NewPath '..\libs\Assembly-CSharp.dll'
    Set-HintPath -ReferenceName 'UnityEngine.UI' -NewPath '..\libs\UnityEngine.UI.dll'
    Set-HintPath -ReferenceName 'UnityEngine' -NewPath '..\libs\UnityEngine.dll'
    Add-ReferenceIfMissing -Include 'UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' -HintPath '..\libs\UnityEngine.CoreModule.dll'
    Add-ReferenceIfMissing -Include 'UnityEngine.IMGUIModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' -HintPath '..\libs\UnityEngine.IMGUIModule.dll'
    Add-ReferenceIfMissing -Include 'UnityEngine.AnimationModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' -HintPath '..\libs\UnityEngine.AnimationModule.dll'
    Add-ReferenceIfMissing -Include 'UnityEngine.TextRenderingModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' -HintPath '..\libs\UnityEngine.TextRenderingModule.dll'
} else {
    Set-HintPath -ReferenceName 'Assembly-CSharp' -NewPath "$managedDir\Assembly-CSharp.dll"
    Set-HintPath -ReferenceName 'UnityEngine.UI' -NewPath "$managedDir\UnityEngine.UI.dll"
    Set-HintPath -ReferenceName 'UnityEngine' -NewPath "$managedDir\UnityEngine.dll"
    Add-ReferenceIfMissing -Include 'UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' -HintPath "$managedDir\UnityEngine.CoreModule.dll"
    Add-ReferenceIfMissing -Include 'UnityEngine.IMGUIModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' -HintPath "$managedDir\UnityEngine.IMGUIModule.dll"
    Add-ReferenceIfMissing -Include 'UnityEngine.AnimationModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' -HintPath "$managedDir\UnityEngine.AnimationModule.dll"
    Add-ReferenceIfMissing -Include 'UnityEngine.TextRenderingModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' -HintPath "$managedDir\UnityEngine.TextRenderingModule.dll"
}

$xml.Save($ProjectPath)
Write-Host "Zaktualizowano csproj: $ProjectPath"
Write-Host "Backup: $backupPath"