# Introduction #

If you'd like to see transmission-remote-dotnet in your language you can create a translation pack for the upcoming release. You don't really need to know anything about .NET or C# and will of course be credited in the README :)

It might be a good idea to check [this issue ticket](http://code.google.com/p/transmission-remote-dotnet/issues/detail?id=29) first to see if anyone else has already translated your language.

# Details #

You need the following files
```
AboutDialog.resx
ErrorLogWindow.resx
LocalSettingsDialog.resx
MainWindow.resx
OtherStrings.resx
RemoteSettingsDialog.resx
StatsDialog.resx
TorrentPropertiesDialog.resx
UriPromptWindow.resx
```

Which you can get from here
```
http://code.google.com/p/transmission-remote-dotnet/source/browse/#svn/trunk/TransmissionClientNew
```

Or grab the whole tree with this command (or TortoiseSVN)
```
svn checkout http://transmission-remote-dotnet.googlecode.com/svn/trunk/ transmission-remote-dotnet-read-only
```

It's probably easiest to use Visual Studio 2008, which you can get the express version of for free. You could use an XML editor, but I guess you're more likely to make mistakes.

Important: **Don't translate anything with '>>' or '&gt;&gt;' in front of its name, just the English text in the value column. All (or almost) of the fields you want to edit will end in .Text or .ItemN**

Attach an archive of the .resx files with the language and the name you want credited to [this issue ticket](http://code.google.com/p/transmission-remote-dotnet/issues/detail?id=29) and I'll try to commit it ASAP.

Happy translating. :)