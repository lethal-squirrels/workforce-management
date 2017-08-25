# workforce-management

# INSTRUCTIONS FOR WORKING ON THIS PROJECT 

#### Rule number one of Bangazon Workforce Manager - Nobody but Chaz can update migrations
### Rule number two pf Bangazon Workforce Manager - Nobdy but Chaz can update migrations

Due to some awesome stuff with visual studio - if multiple people add migrations and try and push it to github - were gonna have a bad time. 
If you or anyone you know find any problems/or find something you need to add let Chaz know. He will make the change and push up a new master forthwith. 
This will avoid any migration issues moving forward. 

### Working remotely

If anybody is going to be working on anything remotely this week, PLEASE make your PRs _as detailed as possible_. 
We wont be around each other to quickly get feedback. 
Please leave detailed message about _what_ you did and _why you did it_ 
ALSO - please give a description of the details we should expect when testing your code. "Run it and make sure it works" is not a good response and I will lay the ban hammer down on those PRs. 

If you have any other questions - we're all on slack. If you get stuck or dont know where to go on something let everyone know. 

## Using this application 

As of August 22, 2017 - this application is not hosted. _However_ you can still take advantage of the functionality locally on your machine. 
There are a few things you will have to have installed on your machine first to ensure you can run the project. 

### Installation

1. Make sure you are on a machine that has the capibility of running Windows. 
1. Download and install [Visual Studio Community Edition](https://www.visualstudio.com/)
1. Clone this repo on your machine. 
1. `cd` into the directory and open the `.sln` file. This will open in Visual Studio and prompt you to install all the packages you will need. 
1. Once the Solution has been loaded in Visual Stuio press `ctrl + f5` to build the project. It will automatically open in your default browser. 

### Navigating the Menus

#### Home Screen
The splash page will display a list of the last 5 added employees as well as a list of upcomming training programs. 
This is intented to be a breif overview of the recent activity in the company

#### Employees Tab
Clicking on the `Employees` tab takes you to an overview list of all the employees. You will see their full name and department they are in. 
Clicking on `Create New` will allow you to add a new employee 
Clicking on `Details` will give you more information including name, department, start date, currently assigned  computer and any training programs they have attended or plan to attend. 
Cicking on	`Edit` will allow you to change an employees last name, their department, assigned computer, and add them to a training program

#### Department Tab  
The Department tab presents you with a list of the companies departments. 
Clicking on `Create New	` will allow you to add a new Department.
Clicking on `Details` will show you the list of employees inside that department. 

#### Computers
The Computers tab presents you with a list of all the computers the company owns
Clicking on `Create New` will allow you to add a new Computer.
Clicking on `Delete` will remove the computer from the database, but _only if it has not been assigned to an employee_ 
If a computer has ever been assigned to an employee, it cannot be removed. 

#### Training Programs
Clicking on the `Training Programs` tab will present you with a list of training programs that have not yet started. 
Clicking on the `Details` tab will let you see the name, a breif description, the start and end dates, max attendees and which employees are signed up to attend. 

