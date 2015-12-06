$location = Get-Location
$parent = Split-Path $location -Parent
$appName = $location.ToString().Replace($parent.ToString(),'').Replace('\','')
$extensions = @('.cs','.xaml','.appxmanifest')
$items = Get-ChildItem -Recurse

foreach($item in $items){
    $isContains = $FALSE
    foreach($ex in $extensions){
        if($ex -eq $item.Extension){
            $isContains = $TRUE
        }
    }
    if($isContains -eq $TRUE){
        $reader = New-Object IO.StreamReader($item.FullName, [Text.Encoding]::GetEncoding('UTF-8'))
        
        $lines = @()
        while(!$reader.EndOfStream){
            $lines += $reader.ReadLine().Replace($appName,'$safeprojectname$')
        }
        $reader.Close();
        $writer = New-Object IO.StreamWriter($item.FullName, $false,[Text.Encoding]::GetEncoding('UTF-8'))
        foreach($line in $lines){
            $writer.WriteLine($line)
        }

        $writer.Close();
    }
    if($item.Extension -eq '.vstemplate'){
        $reader = New-Object IO.StreamReader($item.FullName, [Text.Encoding]::GetEncoding('UTF-8'))
        
        $lines = @()
        while(!$reader.EndOfStream){
            $lines += $reader.ReadLine().Replace('<ProjectItem ReplaceParameters="false" TargetFileName="Package.appxmanifest">','<ProjectItem ReplaceParameters="true" TargetFileName="Package.appxmanifest">')
        }
        $reader.Close();
        $writer = New-Object IO.StreamWriter($item.FullName, $false,[Text.Encoding]::GetEncoding('UTF-8'))
        foreach($line in $lines){
            $writer.WriteLine($line)
        }

        $writer.Close();
    }
    
}