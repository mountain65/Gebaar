Gebaar
======

A simple commandline viewer for Dutch Sign Language on the Mac

## Introduction
This viewer works with all the Gebaren DVDs you bought at the [Gebarencentrum webshop](https://www.gebarencentrum.nl/winkel/gebaren-dvd-roms/). 

## Build
If you have MonoDevelop or Xamarin Studio, open the solution file and build. Otherwise download the  executable.

## Installation
Copy the contents of your DVD to your disk. Than copy the Gebaar.exe to the directory where you copied the DVD to. 
This should be a directory with a file called "begrip_ok.ini" in it.
If needed, install Mono from [Mono Project](http://www.mono-project.com/Main_Page).

## Usage
Open a Terminal window and "cd" into the directory where the exe is. 
Type "mono Gebaar.exe -all" to get a list of all the available signs. 
Type "mono Gebaar.exe [sign-name]" to view one or more short films in QuickTime. All the signs that start with the given name are matched.


