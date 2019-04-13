$startPath = "C:\projects\SmogFightClub\src\SFC.Tests\bin\debug\netcoreapp2.2"
$sqlInstance = "(local)\SQL2014"
$dbName = "SFC"

# replace the db connection with the local instance 
$filePath = join-path $startPath "appSettings.json"

$connectionString = "Data Source=$sqlInstance; Initial Catalog=$dbName; Trusted_connection=true"
$JSON = (Get-Content $filePath | ConvertFrom-Json)
$JSON.ConnectionStrings.DefaultConnection = $connectionString 
$JSON | ConvertTo-Json -depth 100 | Set-Content $filePath 

# attach mdf to local instance
$mdfFile = join-path $startPath "SFC.mdf"
$ldfFile = join-path $startPath "SFC_log.ldf"
sqlcmd -S "$sqlInstance" -Q "Use [master]; CREATE DATABASE [$dbName]"

cd $startPath

dotnet test