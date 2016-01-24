# Introduction #

If you want to use the current trunk source, you can compile it.
You need to [Microsoft .Net Framework 3.x](http://www.microsoft.com/downloads/details.aspx?FamilyId=333325FD-AE52-4E35-B531-508D977D32A6) installed.

# Details #

Run the following command:
```
%Windir%\Microsoft.NET\Framework\v3.5\MSBuild.exe /t:Rebuild /p:Configuration=Release
```

If you want a portable build (use the settings.json file as LocalSettingsStore):
```
%Windir%\Microsoft.NET\Framework\v3.5\MSBuild.exe /t:Rebuild /p:Configuration=Release /p:DefineConstants=PORTABLE
```