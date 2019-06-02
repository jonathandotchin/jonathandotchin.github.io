---
tags:
  - python
  - tutorial
---

A track on gaining quick insights on Python.

# Learning Python

## For Developers

### Tutorials

#### Basic

http://learnpythonthehardway.org/

An excellent tutorial to start learning Python and it is free.

#### Advanced

http://docs.python-guide.org/en/latest/

Once the basic are mastered, this guide give a key view on the best practices of Python.

## For Non Developers

http://code.tutsplus.com/articles/the-best-way-to-learn-python--net-26288

# Environment

## Windows Development
 
### Proxy Setup

In some cases, it might be necessary to setup the proxy on the machine. This can be done in Internet Explorer.

### Tools

The following are the minimal tools that should be installed on the developer's Windows machine to enable development using Python in Visual Studio.

1.	Python 2.6.6
2.	Setup Tools	3.5.1	
3.	PIP	1.5.5	
4.	Python Tools for Visual Studio	2.1	
5.	Visual Studio 2012 Update 4

### Libraries

The following represents the libraries that should be installed on the client machine. It presents the assumed running environment until we can figure out how to use the virtual environment of Python, which would be ideal.

To install these libraries, you must go to the directly where pip is installed. Usually, C:\Python26\Scripts

1. mock 1.0.1	(The mocking framework used for our tests)

Python Tools for Visual Studio is capable of executing Python tests, which is why nose is not required

### Configurations

#### Python Tools for Visual Studio

Some features of Python Tools for Visual Studio requires to run as administrator.

In Visual Studio 2012, go to Tools --> Options --> Python Tools and check "Always run pip as administrator" and "Always run easy_install as administrator" if it is not already checked.

#### Resharper

In order to fully take advantage of Python Tools for Visual Studio, it is necessary to disable Resharper.

In Visual Studio 2012, go to Tools --> Options --> Resharper and click on "Suspend Now"

## CentOS Go Agent
 
### Proxy Setup

In some cases, it might be necessary to setup the proxy on the machine.

### Tools

1. Python	2.6.6
2. PIP 1.3.1
 	 	 	 	 	 	 	 	 
### Libraries

The following represents the libraries that should be installed on the client machine. It presents the assumed running environment until we can figure out how to use the virtual environment of Python, which would be ideal.

1. mock	1.0.1
2. nose	1.3.3

# Source Code

## Structure

The solution is divided into several package for better manageability. Currently, the test packages are in the same folder level as the application package but they can all exists under the application package as well.

### package

The __init__.py files are required to make Python treat the directories as containing packages.

### application

Represents the application.

It can contain a __main__.py if you want to run the Python application using the package (directory) name instead of the file name.

### unit_tests

Represents the unit tests

### integration_tests

Represents the integration_tests

### acceptance_tests

Represents the acceptance_tests