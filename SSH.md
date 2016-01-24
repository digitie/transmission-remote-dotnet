# Introduction #

This is an experimental 'power user' feature I've added because I certainly find it useful, and I think others will too. It allows you to execute a remote command over SSH, to perform some operation on a torrents contents.

# Setup Notes #

(1) You need plink.exe (command line version of the popular PuTTy SSH client) from [here](http://www.chiark.greenend.org.uk/~sgtatham/putty/), and you need to set its path in the local settings of transmission-remote-dotnet. 'Display remote command button' needs to be checked first.

(2) Then you need to setup a session from inside the normal putty.exe client (also at link above). **The session name should be the same as the hostname transmission-remote-dotnet is connecting to**. It's optional of course, but you'll probably want to [setup key authentication](http://www.google.co.uk/search?q=putty+key+authentication) so you don't have to type a user and pass each time.

(3) Setup a command string

# Examples #

Here's a couple of example command strings. '$DATA' gets substituted with the torrent destination folder or file. The '; read' part stops the window closing after the command has finished.

ls -l $DATA; read

scp -r $DATA you@home.machine.com:

$HOME/unpack.pl $DATA

I use it with a small perl script called [unpack.pl](http://eth0.org.uk/~alan/unpack.pl) (use at your own risk) which I wrote for extracting multipart RAR archives.

(4) You should now see a terminal button in transmission-remote-dotnet that you can click when one torrent is selected. If the window closes quickly and doesn't do what you expect, it's most likely your command that's wrong.

Let me know if this feature is useful or has any issues. :)