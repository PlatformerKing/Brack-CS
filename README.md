## What is Brack?

Brack is a powerful language with a simplistic but useful syntax. Designed by Lockethot, it is created for both usage as a interpreted DSL creation tool and as a Markup Language. The Brack official library is released under the MIT License.

### What is a DSL?

Brack is a powerful library designed for the creation of interpreted Domain-Specific Languages (DSLs). A DSL is nothing more than a special programming language designed for usage with certain applications. DSLs are mostly useful for applications with custom scripting functionalities. For instance, video games such as Crusader Kings II by Paradox Interactive use custom DSLs to give programmers the ability to script their own game-changing modifications without altering the game's source code. Also, Interactive Fiction engines such as Inform and TADS make use of DSLs designed specifically for the genre. 

### What is a Markup Language?

Since Brack can be used to store data in files that are human-readable with any text editor, it is considered a Markup Language. Using Brack, it is possible to make data sets of any size. It is especially useful for storing nested data-sets, since Brack statements can be written inside of other statements. By using the official library, it takes little effort to convert a Brack data file into an object array, which can then be used for calculations or for the instantiation of classes. Similar markup languages include CSV, JSON, XML, XAML, and YAML.

## Would a Brack DSL be an Interpreted or Compiled Language?

It is technically possible to create a compiler for a Brack implementation that can be used to make executables. However, the official library only comes with the required tools for creating an interpreted implementation designed for usage within a specific application.

## How fast is a Brack DSL?

Languages created with Brack should typically run a bit faster than Python, a language considered the gold standard of interpreted languages. However, in order to achieve maximum runtime speed, the best method is to serialize the Brack into a data file instead of parsing the raw text data every time you run the program. Serialization can be reverse engineered easily, and all of the required tools for creating a custom serializer are present in the official Brack library.

## What languages can I include this library in?

This version of Brack (Brack-CS) is implemented in C#. However, it is possible to include the DLL output of this repository in applications made using any .Net language, which include VisualBasic, C++, C, and C#. Support for Java, Go, and Rust may be implemented in the future, but this will require separate versions of this API. Also, there will never be a Brack implementation for any interpreted language such as Python, because interpreting a language with an interpreted language would be too slow to be useful.

## How does Brack syntax work?

Brack syntax is explained in the wiki of this repository! Go [here](https://github.com/Lockethot/Brack-CS/wiki/Brack-Syntax-Rules).

## Why do you use square brackets instead of other symbols for separators in Brack?

Brack uses square brackets in its syntax due to how easy they are to type on QWERTY keyboards. Unlike parenthesis and squigly brackets, square brackets don't require you to hold down the shift key to type.

## How did you come up with the idea for Brack?

Brack is heavily inspired by MBScript, a language developed by Taleworlds Entertainment for modding early Mount and Blade games up to Warband and all of its DLC. Later games in the franchise stopped using MBScript, and instead use pure C#. If you look closely at MBScript, you may notice that it is actually a chain chain of tuples and lists in Python 2 that are fed into the program. If you want to see an example of MBScript, you can view this repository for [Persistent World Mod](https://github.com/vornne/pw_module_system).