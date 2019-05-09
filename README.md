# p5template
A simple tool that makes it easy to setup a p5.js sketch

## Build Instructions

\* Before you follow the build instructions, make sure you have the [Mono C# compiler](https://www.mono-project.com/). Also, if you are using Windows, you can just use a binary from the [releases](https://github.com/greenmancode/p5template/releases) page.

1. Clone the repository

`git clone https://github.com/greenmancode/p5template.git`

2. Navigate to where the source files are located

`cd p5template/p5template/`

3. Compile `Program.cs` using Mono (mcs)

`mcs Program.cs`

4. Run the program using Mono

`mono Program.exe`

## Usage

Either compile the program yourself or download the latest binary from the [releases](https://github.com/greenmancode/p5template/releases) page.

To use this program, copy or move `p5template.exe` into the directory you want to create your sketch in, and run the program.

## Issues

~~Because this is created in Batch, there is no user input error handling so be cautious when entering data.~~ (Fixed)

No issues were found while I tested the program. Please report any issues you have so I can fix them.

## Planned Changes
* ~~Grabbing the latest CDN links for the libraries~~ (Added in v1.2)
* Library Browser (to find other useful p5.js-related libraries)
* ~~Error Handling (so typing the wrong thing doesn't break the program)~~ (Added in v1.1)
* More templates (ex: p5.js sketch with title)
* Ability to run the program using command line arguments

[Changelog](https://github.com/greenmancode/p5template/blob/master/changelog.md)
