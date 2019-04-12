$startPath = "C:\projects\SmogFightClub\src\SFC.Tests\bin\debug\netcoreapp2.2"
$sqlInstance = "(local)\SQL2014"
$dbName = "SFC"

# replace the db connection with the local instance 
$config = join-path $startPath "SFC.Tests.dll.config"
$doc = (gc $config) -as [xml]
$doc.SelectSingleNode('//connectionStrings/add[@name="SFC"]').connectionString = "Data Source=$sqlInstance; Initial Catalog=$dbName; Trusted_connection=true"
$doc.Save($config)

# attach mdf to local instance
$mdfFile = join-path $startPath "SFC.mdf"
$ldfFile = join-path $startPath "SFC_log.ldf"
sqlcmd -S "$sqlInstance" -Q "Use [master]; CREATE DATABASE [$dbName]"