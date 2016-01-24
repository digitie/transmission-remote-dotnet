There's much better RPC clients for UNIX than this with Mono, the Qt version of Transmission is my recommendation. For these reasons, nobody (including me) really wants to use this, and the effort of testing it with Mono isn't worth it.

![http://transmission-remote-dotnet.googlecode.com/svn/wiki/screenshot-3.4mono-1.small.png](http://transmission-remote-dotnet.googlecode.com/svn/wiki/screenshot-3.4mono-1.small.png)

Here's a list of issues that I've noticed on Mono:

1) Mono's winforms is often sluggish and ugly.

2) The main window needs a minor resize each time it's opened so that the whole ToolStrip is visible.

3) Minimising to tray seems to lose all the ToolStrip items on some Mono verisons.

4) Selecting multiple file items works, but right clicking causes only one to be selected.

To build for Mono (won't work anymore):

1) Do a svn checkout.

2) Open the sln file in MonoDevelop or Visual Studio (no makefile yet).

3) It may be necessary to remove the two Jayrock references and re-add them (from TransmissionClientNew/bin/Release)

4) Define the MONO symbol in project options.

5) Build and run.