# Xpressional
A .NET based console application that parses input regular expressions from a file and outputs a state table for each expression in the file.

## Authors
Tyler Vanderhoef - tyler.vanderhoef@wsu.edu

GitHub repo can be found [here](https://github.com/tvand7093/Xpressional).

## Environment
You must be using one of the following IDE's:
* Visual Studio
* Xamarin Studio

## Steps to Install
1. Copy the contents of this repository to your local machine.
2. Open the newly downloaded/unziped folder and double click on `Xpressional.sln`
3. If Visual Studio prompts about unsafe code, click continue until you can see the whole solution structure.
4. Now you should be able to simply press the 'Play' button to build the application and it will place the contents in `Xpressional/bin/Debug` as `Xpressional.exe`.

## Steps to Test
Navigate to the `Xpressional/bin/Debug` folder after building the application.
Specify on the command line (either with Mono or otherwise) the name of the file to open up. This can be a full path or relative to the working bin directory.

An example call would be: `Xpressional.exe "test_expressions.txt"` or `mono ./Xpressional.exe "/MyDirectory/MySpecialFile.txt"`

If you choose to run this using the debugger, it will default to the [test file](Xpressional/test_expressions.txt) that was included in this repository.

## Files
Here is a list of the files and what each one does/represents.
* Xpressional.sln - The solution file for the whole project.
* .gitignore - The ignore file for the GitHub repository.
* Xpressional - The console application folder
	* Properties (Ignore the contents)
	* Xpressional.csproj - The project file for the console application
	* ConsoleHelper.cs - The helper class for console output
	* Program.cs - The main application
	* test_expressions.txt - A set of test expressions
* Xpressional.Data - The folder for the majority of application operations.
	* Exceptions
		* InvalidLanguageException.cs - A custom exception for invalid inputs.
	* Graphs
		* Graph.cs - The main class for working with the graphs. This provides the union, concat, and kleen operations.
		* GraphState.cs - Represents a state node in the Graph.
		* GraphStateConnection.cs - Represents how two states are connected and by what type of Word.
	* Interfaces
		* IConsole.cs - An interface for working with the console.
		* IMapping.cs - An interface for mapping a letter or symbol to a given Operation or Word.
	* Managers
		* InputManager.cs - Used for taking input and making a graph out of it.
		* OutputManager.cs - Used for outputing a graph to the console.
	* Models
		* Language.cs - A list of Words that represent valid input for the system.
		* Operation.cs - Used to denote what letters go to what type of graph operation.
		* OperationType.cs - An enum contianing values for valid operation types.
		* Word.cs - Used when creating connections between two state nodes. Represents an a letter.
	* Properties (Ignore the contents)
* Xpressional.Tests (not included in solution because Visual Studio 2010 doesn't like NuGet)
	* A suite of tests used when writing this application. For the most part, the files in this folder can be ignored.
		
	