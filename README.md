# Raft Algorithm
A method to replicate logs across servers while accounting for node failures or data loss

## Description
This project simulates an implementation of the raft algorithm by utilizing the C# Task Parallel Library (TPL). Using the TPL allows for individual threads to be created and run simultaneously in order to simulate a group of servers on just one machine. Each thread acts as one machine which performs its own task and logging while communicating with the other machines to assure all machines retain the same state. Machine crashes, interruptions, and packet loss can be perfomed on any machine to simulate real world scenarios where machines no longer are in the same state.

## Requirements
<img src="./img/windows.png" alt="windows" width="100"/>
<img src="./img/csharp.png" alt="c#" width="100"/>
<img src="./img/net9.png" alt="c#" width="100"/>


* OS: Windows 
* C# 
* .NET9

## Usage
- Download or clone project from git repository
- Navigate in termimal from project root to inner RaftDashboard folder -> .\RaftDashboard\RaftDashboard
- Enter command -> dotnet run
- GUI should open and display the following

<img src="./img/defaultGUI.png" alt="defualt GUI" width="500"/>

