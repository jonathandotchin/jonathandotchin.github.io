---
tags:
  - test
  - automation
  - tdd
---

# Definition

![Pyramid]({{site.url}}/resources/2016-04-28-Test-Automation\images/Pyramid.PNG "Pyramid"){: .align-center}
 
## Unit Tests

At the base, we have the most predominant type of tests. We would test the smallest possible piece of software in the application in isolation to determine whether they behave as expected. In our case, the smallest piece is could be an entire interface such as a class and the functions of say class. The main contributors for these tests are the developers.

### Functional Tests

In the middle, we want to test the different functions or services provided by the application. If we were testing a calculator and the calculator offers addition, subtraction, multiplication and division, we would test these services separately from the user interaction. Whereas, the bottom layer would be about doing things right, this layer would be about doing the right things.

The use of stubbing and mocking are debatable here.

- Arguments can be made such that if the persistence is an intrinsic part of your system, you should not stub or mock it.
    - You wrote your own SQL statement.
- What is clear is that external services for which we should have no control should be mock.
    - You are using an third party DNS resolution from Azure.

The main contributors for these tests are the developers.

### System Tests

At the very top of the pyramid, we have the least common type of tests. Typically, we would test the system as a whole. If we were testing a calculator, we would literally hold a calculator and punch in operations. There is no mocking or stubbing involved. Although these can be automated, they are often difficult to do so. Hence, we want to do the least amount as possible.

The contributors for these tests are the developers and third parties like operations, testers, clients. Consequently, it is critical for the developer teams to provide the necessary tools to build these tests.