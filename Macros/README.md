Revit Macros
========

Each of the files in the folders is a single Macro. These Macros are added from the Macro VSTA in Revit.

Macros are small pieces of code that users can run to do anything the Revit API can do short of complex interfaces or installation options. However the code can be written, debugged and tested then ported over to a Visual Studio project if you wish to build consistent functionality for all your users.

SETUP
========

To add a Macro to your Revit you need to go to the Manage tab then under the Macros manager and then Macros panel then Macros Manager.

Once in the Macro manager you need to select the Applications TAB, then create a new C# Module, then you can create a number of Macro's under each C# Module.

Each of the Macros in these examples are C# however we may add Python, vb or others depending on the contributions setup.

These macros have been setup to run as Application Macros ONLY and your results may vary when running them as Application projects.

Each .cs file contains a single Macro and is setup as follows

README - A short description of what the Macro does as a Comment block, and any additional instructions that are outside of the following readme comments.

Using References :- each of the using References that will be required to execute this macro that are not part of the initial Macro setup environment, you need to copy and paste these to the other "Using" references at the top of the Macro editing Environment.

public void ExampleName()\n
{\n
  MACRO CODE IS HERE COPY/PASTE THE STUFF IN BETWEEN THESE BRACKETS ONLY!!!\n
}\n

The second part of the file will be the Macro code itself, you need to copy/paste the code from the code block into your own named Macro. Once this is done you should be able to close and run macro as you need to.

Any issues please use the issues register.
