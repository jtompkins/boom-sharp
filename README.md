# BOOM#

## About

Boom# is a command line key-value store for Windows. It's a C# port of Zach Holman's [boom](https://github.com/holman/boom). It's evolved a bit since then, but the core of the code still matches Zach's Ruby pretty closely.

*"Motherfucking TEXT SNIPPETS! On the COMMAND LINE!"*

## Installation

Installation is a little lame for now. Download the source and build in your favorite .NET IDE (Visual Studio or MonoDevelop). Boom-sharp uses a few .NET 4.0 features, so you'll need the latest version of MonoDevelop or Visual Studio.  Unfortunately I can't seem to get the clipboard copy functionality working under Mono on OS X, so Boom# is Windows-only for now.

(Do you know how to get Mono to allow clipboard interaction from a command line app? Let me know on this [StackOverflow question](http://stackoverflow.com/questions/7367603/how-can-i-copy-text-to-the-clipboard-from-a-mono-c-console-application-on-os-x/7372530#comment8913512_7372530).

## Usage

	boom-sharp help

	   - boom-sharp: help ---------------------------------------------------

    boom-sharp                          display high-level overview
    boom-sharp all                      show all items in all lists
    boom-sharp help                     this help text
    boom-sharp storage                  shows which storage backend you're using
    boom-sharp switch <storage>         switches to a different storage backend
    boom-sharp import <storage>         imports items from a different storage backend

    boom-sharp <list>                   create a new list
    boom-sharp <list>                   show items for a list
    boom-sharp <list> delete            deletes a list

    boom-sharp <list> <name> <value>    create a new list item
    boom-sharp <name>                   copy item's value to clipboard
    boom-sharp <list> <name>            copy item's value to clipboard
    boom-sharp open <list>              open the urls of all items in a list
    boom-sharp open <name>              open item's url in browser
    boom-sharp open <list> <name>       open item's url in browser for a list
    boom-sharp random                   open a random item's url in browser
    boom-sharp random <list>            open a random item's url for a list in browser
    boom-sharp echo <name>              echo the item's value without copying
    boom-sharp echo <list> <name>       echo the item's value without copying
    boom-sharp <list> <name> delete     deletes an item

    boom-sharp speak <room> <name>      speaks an item's value in a campfire room

Boom#'s key-value store (if you're using the JSON backend, which is the only one actually working right now) is called `.boom` and is stored in your home directory.  It's 100% compatible with the store used by Boom's JSON backend, so you can move it back and forth between your Mac and your PC.

Boom#'s config file is called `.boom-config` and is *also* stored in your home directory.  You shouldn't need to modify it unless you want to use Boom#'s [Campfire](http://campfirenow.com/) features.

## Contributions

Want to contribute?  I'd love to hear from you. Clone this repo, write some code, send me a pull request. I'll accept just about any code, I promise!

## Hey There

[Josh Tompkins](http://www.joshtompkins.com) made this. Want to reach me? The best place is on Twitter (I'm [@iioshius](http://www.twitter.com/iioshius)).
