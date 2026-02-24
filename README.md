# Task Tracker Console Application
A console-based task tracker application built using C# and .NET. This allows the user to create and manage their own task checklist through a structured menu-driven console interface.

## Overview
This project demonstrates:
- Object-oriented programming in C#
- JSON-based data persistence
- File I/O operations
- Console-based interaction
- State management

### Features
- view current list items
- create a new task or habit item
- edit or delete existing items
- mark tasks as complete
- increment habit completion count
- save and load data from a JSON file
- reset all stored data

#### Try Me!
Prerequisites:
- .NET SDK 6.0 or later
- Check your version: dotnet --version

Setup:
- Navigate to the directory Task Tracker\App: cd App
- Run application: dotnet run
- Application should open on the Menu in your console

Usage:
View List (1) - displays all current tasks and habits
Add Item (2) - create a new task or habit item
Manage Item (3) - edit or delete an existing item
Mark Complete (4) - mark a task as complete or increment completion of a habit item
Save Data (5) - saves the current list to the JSON file
Load Data (6) - loads the previously saved list
Exit (7) - terminates the program
Wipe List & Save File (8) - clears all stored data and overwrites the JSON file