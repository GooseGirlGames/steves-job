# Create Build

Step-by-step guide on how to do a release.

1.  Probably quit Unity Editor first, just to be sure.
2.  Check that the manual (doc/Manual.md) is correct and up-to-date. It
    will be included in the zips.
3.  Create the relase on GitHub [here][release].  Choose a version
    number, and hit *Publish Release*
4.  In the `steves-job` directory: Run `git pull` to pull in the new
    tag.
5.  Run `make prep-dirs`.  This creates the `build/` directories.
6.  Open Unity Editor.
7.  Hit *File -> Build Settings*, choose *Linux* as target platform.
8.  Hit *Build* and choose the `build/steves-job-linux` directory, and
    `steves-job.x86_64` as the file name ([screenshot][lindir]).
9.  Hit *Save* and wait for Unity to build the game.
10. Re-Open *File -> Build Settings*, choose *Windows* as target
    platform.
11.  Hit *Build* and choose the `build/steves-job-windows` directory,
     and `steves-job.exe` as the file name ([screenshot][windir]).
12.  Hit *Save* and wait for Unity to build the game.
13.  In the `steves-job` directory, run `make zip` to create the zips.
14.  Go to the relase page on GitHub, hit *Edit Release* and upload the
     zips.
15.  Hit *Update Release* and you're good to go!

[release]: https://github.com/finnmito/steves-job/releases/new
[lindir]: https://user-images.githubusercontent.com/26824491/124400632-24bf2580-dd24-11eb-98fd-27df1b14ddfa.png
[windir]: https://user-images.githubusercontent.com/26824491/124400666-867f8f80-dd24-11eb-8aae-f818d3832af9.png
