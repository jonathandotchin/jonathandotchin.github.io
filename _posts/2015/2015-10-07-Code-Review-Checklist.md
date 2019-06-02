---
tags:
  - best practices
  - code review
  - tips
  - wip
---

The following is a simple checklist for code review. 

- Why was the code changed?
    - It is important to understand why the code changed in order to review it
- Have the tests been modified?
    - If the task is a new feature, there should be new unit tests
    - If the task is about bug fixing, there should be changes in unit tests
    - If the task is about refactoring, there shouldn't be changes in unit tests
- Have the documentation been updated?
    - Internal or external documents needs to reflect the new changes
- Are any code analysis suppression been justified?
    - Usually code analysis rules are consistent throughout the project, any exception should be documented
- Are TODO comments followed by JIRA items?
    - Any technical debt needs to be documented in JIRA for future planning
- Are there any useless comments?
    - Avoid any confusion
- Are there any dead code?
    - Avoid any confusion
- Can I tell which developer wrote the code?
    - If you can tell who wrote the code, then it is most likely not standard
- Are there any quality issues or bugs with code?
    - Does it work or not?
- Is there a simpler way to solve the problem?
    - Is there a better way of doing it?