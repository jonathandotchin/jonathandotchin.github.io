---
tags:
  - best practices
  - software development
  - test
---

# Defined

## What Is TDD?

From Wikipedia: "Test-driven development (TDD) is a software development process that relies on the repetition of a very short development cycle: first the developer writes an (initially failing) automated test case that defines a desired improvement or new function, then produces the minimum amount of code to pass that test, and finally refactors the new code to acceptable standards."

A very short process in development
 - KISS: Keep it simple stupid
 - YAGNI: You ain't gonna need it

Test are written first
    - Initially, the test cases are failing

Minimum amount of code is written
    - Only the code necessary to make the test pass
    - Fake it until you need it

Refactor the code until it meets acceptable standards
    - The tests are there to guard and validate the changes

## Benefits

Drive modular design
    - Created testable code through Design By Contract
    - High cohesion, loose coupling, cleaner interfaces

Small incremental steps
    - Only necessary code is written
    - Avoid over-engineering
    - The necessary code is covered by test

Provides a safety net when refactoring
    - Changes made inside a method can be validated right away

## Shortcomings

False sense of security
    - Full functional/non-functional tests are still necessary
    - User Interface, database, network, etc.
    - Automated tests can be written but it is not TDD (TDD focuses mainly on unit tests)
    - Developer’s blind spot is still present

Additional maintenance
    - There is actually more “stuff” to maintain

Management support is essential
    - The benefits are often seen in the long term.

## Practice Versus Theory

Legacy without automated tests.
    - We often need to add them afterward.
    - The tasks are boring to do and looks like burden.
    - It is not TDD but it will allow us to use it in the future.
    - A necessary evil for the greater good.

# Best Practices

Red, Green, Refactor

![red-green-refactor]({{site.url}}/resources/2014-01-20-Test-Driven-Development\images/red-green-refactor.png "red-green-refactor"){: .align-center}

    - Red Green Refactor is missing a key component
    - Before writing a test, think about the feature
    - Breakdown the feature as much as possible

## Libraries and Tools

.NET Projects
    - Unit Test Framework
        - NUnit
    - Mocking Framework
        - Moq
    - Test Coverage
        - TestDriven.Net with NCover
    - Test Runner
        - TestDriven.Net
C++ Projects (TBD)

## Naming and Structure

### Naming

    - Be consistent : We should use the same naming convention
    - Be descriptive : We should know exactly what the test is doing and what to expect.
    - Be readable : We should be able to read it either in code or in the test runner.


### Project Structure

The structure of your test project should closely resemble the structure of your production code. It is, however, better to keep a single test project. But there should be an one to one link between the namespace and the classes.

### Test Structure

Based on NuGet and Haacked

http://haacked.com/archive/2012/01/02/structuring-unit-tests.aspx

Consider

```
public class Titleizer
{
    public string Titleize(string name)
    {
        if (String.IsNullOrEmpty(name))
            return "Your name is now Phil the Foolish";
        return name + " the awesome hearted";
    }
 
    public string Knightify(string name, bool male)
    {
        if (String.IsNullOrEmpty(name))
            return "Your name is now Sir Jester";
        return (male ? "Sir" : "Dame") + " " + name;
    }
}
```

The tests would look like

```
public class TitleizerTests
{
    [TestFixture]
    public class TitleizerMethod
    {
        [Test]
        public void ReturnDefaultTitleForNullName()
        {
            // Test code
        }
 
        [Test]
        public void AppendTitleToName()
        {
            // Test code
        }
    }
 
    [TestFixture]
    public class KnightifyMethod
    {
        [Test]
        public void ReturnDefaultTitleForNullName()
        {
            // Test code
        }
 
        [Test]
        public void AppendSirToMaleNames()
        {
            // Test code
        }
 
        [Test]
        public void AppendDameToFemaleNames()
        {
            // Test code
        }
    }
}
```

## Arrange, Act, Assert

Each unit test should have 3 sections:

### Arrange

In this part of the test, we create and initialize the class under test, the mock classes and any variables needed for the test.

### Act

In this part, we call the method under test. Usually, this section only contains 1 statement.

### Assert

In this final part, we assert the state of our class or the result of the operation. We should only assert the things that are under test.
Be careful not to assert too much since this can become a maintainability issue in the long run.

An assertion applies for your test. There should be no condition attached to it.

## Coverage

### Guideline

    - Help identify cases that are tested
    - Not perfect

### Warning
    - High Coverage =/= Good Test
    - What if b = 0 in the divide in the example below

### Remember
    - Think!


## 3 types of methods

When coding a method, the developer should consider what type of method he is creating. A method should ideally be only 1 of the following types:

    - Workflow: Method which decides, based on conditions, which action to perform.
    - Data Manipulation: Method which receives data, fetches data or transforms data and returns a result.
    - External Communication: Method which communicates with external programs, components, service, etc.

## What makes a good unit test

A good unit test must absolutely follow these rules:

    - Must be environment independent: This means that the test should be capable of running on any computer, with any user account. This is important because if your colleagues can't run your test, the whole endeavor is useless.
    - Must be FAST: A full execution of all your unit tests should take less than a minute. For 600 unit tests, that means 0.1 second per test. Why ? Well if your tests take 5-10 minutes to run, people will stop executing them. The whole TDD process will fail.
    - Must test only 1 method of 1 class: This is why we call it a unit test. If you test too much code units, then the coupling of your unit test increases and this can lead to more work when interfaces or code changes. Keep your test simple.
    - Must only test public units: Private units should be the result of refactoring public units which are already unit tested. Testing private units will increase the coupling of your unit test and this can lead to more work when code changes.

## Mock external dependencies

    - File System
    - Threads
    - Web Srvices
    - Database
    - Environment (Date and Time, Hardware, etc.)

## Do not create test helpers in your production code.
Doing so will simply pollute your production code. If you need to have helpers in your code to make your test work, it is a clear sign of design problem in your code. Consider reviewing:

    - Interfaces
    - Public vs Private

# References
http://artofunittesting.com/