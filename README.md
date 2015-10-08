# Xpressional
A regular expression parser for Automata

## Steps to Install
1. Copy the contents of this repository to your local machine.
2. Open up Visual Studio or Xamarin Studio
3. Open the project named 'Xpressional' at the root of the directory and wait for everything to load.
4. You should now be able to build the application with default settings on the project.

## Steps to Test
Specify on the command line (either with Mono or otherwise) the name of the file to open up. This can be a full path or relative to the working bin directory.

An example call would be: `Xpressional.exe "test_expressions.txt"` or `mono ./Xpressional.exe "/MyDirectory/MySpecialFile.txt"`

If you choose to run this using the debugger, it will default to the [test file](Xpressional/test_expressions.txt) that was included in this repository.