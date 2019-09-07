---
tags:
  - best practices
  - software development
  - summary
---

This is a very high level summary of the essence of the book Agile Principles, Patterns, and Practices in C#.

# Design Smells

## Rigidity

A design is rigid if a single change causes a cascade of subsequent changes in dependent modules.

### Example

Using entity object as data contract causes changes to cascade from model to client

## Fragility

Fragility is the tendency of a program to break in many places when a single change is made.

### Example

Changing an api can break anything that depends on it.

## Immobility

A design is immobile when it contains parts that could be useful in other systems, but the effort and risk involved with separating those parts from the original system are too great.

### Example

Modules used in a monolith design.

## Viscosity

When the design-preserving methods are more difficult to use than the hacks, the viscosity of the design is high.

### Example

Method1, Method2, Method3, etc versus service and data contract versioning

## Needless Complexity

A design smells of needless complexity when it contains elements that aren't currently useful.

### Example

Columns that can move in a table on a web page before coding the actual display of data.

## Needless Repetition

When there is redundant code in the system, the job of changing the system can become arduous. Bugs found in such a repeating unit have to be fixed in every repetition. However, since each repetition is slightly different from every other, the fix is not always the same.

### Example

2 app that does exactly the same thing except they are used in 2 different geographic location.

## Opacity

Opacity is the tendency of a module to be difficult to understand.

### Example

Implementing a custom search engine in each app.

# Trump Principles

## YAGNI - You aren't gonna need it

Always implement what you need, only what you need and not what you think you will need. Must be used in conjunction with continuous refactoring, testing and integration.

Otherwise,
    - Too much time spent adding unnecessary features
    - Difficult to test such features since we don't even know what they do
    - Code is bloated 

## KISS - Keep it simple, stupid

Avoid creating a burden inside your code with the creation of over complicated algorithm or solution.

Otherwise,
    - Difficult to maintain
    - Code require specific talent to look at 

## DRY - Don't repeat yourself

Avoid copy and pasting code whether it is within the same project or outside the project. It is side that if you copy and paste more than twice, you must refactor.

Otherwise,
    - Change in one section must be manually track into the second section

# SOLID Principles

## The Single-Responsibility Principle

An class should have only one reason to change.

Why was it important to separate these two responsibilities into separate classes? The reason is that each responsibility is an axis of change. When the requirements change, that change will be manifest through a change in responsibility among the classes. If a class assumes more than one responsibility, that class will have more than one reason to change.

In the context of the SRP, we define a responsibility to be a reason for change. If you can think of more than one motive for changing a class, that class has more than one responsibility.

An axis of change is an axis of change only if the changes occur. It is not wise to apply this principle or any other principle, for that matter, if there is no symptom.

This is applicable on different scale: a function, a class, an assembly, an application and the axis of change scale as well.

### Example

Business logic and persistence rules should be separated.

## The Open/Closed Principle

Software entities (classes, modules, functions, etc.) should be open for extension but closed for modification.

They are open for extension. This means that the behavior of the module can be extended. As the requirements of the application change, we can extend the module with new behaviors that satisfy those changes. In other words, we are able to change what the module does.

They are closed for modification. Extending the behavior of a module does not result in changes to the source, or binary, code of the module. The binary executable version of the module whether in a linkable library, a DLL, or a .EXE file remains untouched.

In C# or any other object-oriented programming language (OOPL), it is possible to create abstractions that are fixed and yet represent an unbounded group of possible behaviors. The abstractions are abstract base classes, and the unbounded group of possible behaviors are represented by all the possible derivative classes.

The strategy pattern and the template method pattern are the most common ways of satisfying OCP. They represent a clear separation of generic functionality from the detailed implementation of that functionality. But take care of resisting premature abstraction as it is as important as abstraction itself.

### Example

Build Agents

## The Liskov Substitution Principle

If for each object o1 of type S there is an object o2 of type T such that for all programs P defined in terms of T, the behavior of P is unchanged when o1 is substituted for o2 then S is a subtype of T.

The importance of this principle becomes obvious when you consider the consequences of violating it. Presume that we have a function f that takes as its argument a reference to some base class B. Presume also that when passed to f in the guise of B, some derivative D of B causes f to misbehave. Then D violates LSP. Clearly, D is fragile in the presence of f.

The authors of f will be tempted to put in some kind of test for D so that f can behave properly when a D is passed to it. This test violates OCP because now, f is not closed to all the various derivatives of B. Such tests are a code smell that are the result of inexperienced developers or, what's worse, developers in a hurry reacting to LSP violations.

This principle is just an extension of the Open Close Principle and it means that we must make sure that new derived classes are extending the base classes without changing their behavior.

One way to achieve this is to design by contract. To achieve it further is to make the contract immutable; such that you don't have anything that can modify the behavior of say contract.

If it looks like a duck, quacks like a duck but needs batteries, then you have the wrong abstraction.

### Example

Enterprise Library Logging to mail, event log, database, etc.

## The Interface Segregation Principle

Classes whose interfaces are not cohesive have "fat" interfaces. In other words, the interfaces of the class can be broken up into groups of methods. Each group serves a different set of clients. Thus, some clients use one group of methods, and other clients use the other groups.

ISP acknowledges that there are objects that require non-cohesive interfaces; however, it suggests that clients should not know about them as a single class. Instead, clients should know about abstract base classes that have cohesive interfaces.

Separate clients means separate interface. Clients should not be forced to depend on methods they do not use. If the client does need both, then they can use multiple inheritance. Consider an application UI where there is an command line UI and a visual UI. The application UI interface should not have something for drag and drop. It should contains only things that are common to both clients. The interface should be about common behaviors.

### Example

.NET Framework

## The Dependency-Inversion Principle

High-level modules should not depend on low-level modules. Both should depend on abstractions.

Abstractions should not depend upon details. Details should depend upon abstractions.

To provide a summary, the Dependency Inversion Principle is primarily about reversing the conventional direction of dependencies from "higher level" components to "lower level" components such that "lower level" components are dependent upon the interfaces owned by the "higher level" components. (Note: "higher level" component here refers to the component requiring external dependencies/services, not necessarily its conceptual position within a layered architecture.) In doing so, coupling isn't reduced so much as it is shifted from components that are theoretically less valuable for reuse to components which are theoretically more valuable for reuse.

What the Dependency Inversion Principle does not refer to is the simple practice of abstracting dependencies through the use of interfaces (e.g. MyService → [ILogger ⇐ Logger]). While this decouples a component from the specific implementation detail of the dependency, it does not invert the relationship between the consumer and dependency (e.g. [MyService → IMyServiceLogger] ⇐ Logger or [MyService] → [IMyServiceLogger] ⇐ Logger.

Dependency injection is another way of seeing this.

### Example

C++ API that defines the system.h class

# Reference

Agile Software Development, Principles, Patterns, and Practices. Robert C. Martin.  

http://msdn.microsoft.com/en-us/magazine/cc546578.aspx