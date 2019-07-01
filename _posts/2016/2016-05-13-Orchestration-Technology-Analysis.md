---
tags:
  - poc
  - orchestration
  - analysis
---

## Summary

As applications move towards a service oriented architecture, there is a growth of independent and loosely coupled set of services with a decoupled orchestration engine that is responsible for maintain a flow of actions in order to address business needs. Examples of such orchestrations / workflow includes:

- Asset Store Cleanup
- Map Reduce Starter

The following analysis is to help identify which technology should be considered in order to implement this orchestration engine in order to fulfill the needs of our applications and whether we can use the same technology across multiple teams.

## Scope

If you have a command line like application that seems out of place that is performing a workflow based several web services depending on theirs outputs, then it can be a candidate for this.

- Orchestration Engine
  - has no internal business logic except for the workflow itself
  - is based on input/output of web services
  - executes a directed graphs
  - groups disjointed services together to fulfill a need

## Desired Features

### Sequential and Parallel Execution

- Executes sequential tasks and/or parallel tasks

### GUI and API

- Provides visualization of the workflow in a GUI as a directed acyclic graph
- Allows execution of the workflow via an API

### Notification

- Provides notification via email and/or zabbix

### Dry Run

- Allows a sanity check on the execution of the workflow
- Provides an execution plan of the workflow

### Loop with Source Array

- Provides the ability to execute the workflow given an array of inputs

### Template

- Workflow can take into account parameters and environment arguments to customize the execution

### Extensible

- Actions executed by the workflow should via scripts
- Workflows execution can be triggered via API

### Progress Tracking

- Provides progress during the execution of a workflow

### Telemetry

- Provides instrumentation during the execution of a workflow

### Dispatching and Tagging

- Is capable of dispatching specific tasks to specific workers

### Open to Operationalization

- Should provide the necessary infrastructure to be plugged into MainFrame or myUPS
- Closely related to an API

### Logging (Verbosity)

- Provides visibility in its execution via different level of logs

### Ability to Cancel

- Provides an ability to cancel the execution of a workflow
- Activities will need to handle the cancel order

### Textual Configuration

- Configuration of workflow should be in text such that they can be version controlled

### Support

- Maintained and documented

### Price

- Justifiable

# First Class Candidates

The following are candidates that based on their documentations and of our knowledge of the tools seems to be able to covered the requirements enumerated. However, a more detailed PoC is needed to validate this assertion.

### Apache Airflow

Airflow is a platform to programmatically author, schedule and monitor workflows.

- http://pythonhosted.org/airflow/index.html

### Pinball

Pinball is a scalable workflow manager.

- https://github.com/pinterest/pinball

### Spiff

Spiff Workflow is a library implementing a framework for workflows. It is based on [http://www.workflowpatterns.com](http://www.workflowpatterns.com/) and implemented in pure Python.

In addition, Spiff Workflow provides a parser and workflow emulation layer that can be used to create executable Spiff Workflow specifications from Business Process Model and Notation (BPMN) documents.

- https://github.com/knipknap/SpiffWorkflow

### Luigi

Luigi is a Python (2.7, 3.3, 3.4, 3.5) package that helps you build complex pipelines of batch jobs. It handles dependency resolution, workflow management, visualization, handling failures, command line integration, and much more.

- https://github.com/spotify/luigi

### Ansible

Ansible is a radically simple IT automation platform that makes your applications and systems easier to deploy. Avoid writing scripts or custom code to deploy and update your applications— automate in a language that approaches plain English, using SSH, with no agents to install on remote systems.

- https://github.com/ansible/ansible

### Dray

Dray allows users to separate a workflow into discrete steps each to be handled by a single container. This isolation makes efficient use of compute resources and also allows workflows to be easily changed, extended or re-composed via this loosely coupled architecture.

- http://dray.it/

### CloudSlang

CloudSlang is a flow-based orchestration tool for managing deployed applications. It allows you to rapidly automate your DevOps and everyday IT operations use cases using ready-made workflows or create custom workflows using a YAML-based DSL.

- http://cloudslang-docs.readthedocs.org/en/v0.9.50/index.html

### ActionChain

ActionChain is a no-frills linear workflow, a simple chain of action invocations. On completion of a constituent action the choice between on-success and on-failure is evaluated to pick the next action. This implementation allows for passing of data between actions and finally publishes the result of each of the constituent actions. From perspective of StackStorm an ActionChain is itself an action, therefore all the operations and features of an action like definition, registration, execution from cli, usage in Rules etc. are the same.

- https://docs.stackstorm.com/actionchain.html

### Mistral

Mistral is a workflow service. Most business processes consist of multiple distinct interconnected steps that need to be executed in a particular order in a distributed environment. One can describe such process as a set of tasks and task relations and upload such description to Mistral so that it takes care of state management, correct execution order, parallelism, synchronization and high availability. Mistral also provides flexible task scheduling so that we can run a process according to a specified schedule (i.e. every Sunday at 4.00pm) instead of running it immediately. We call such set of tasks and relations between them a workflow.

- https://wiki.openstack.org/wiki/Mistral

## Other Candidates

### Jenkins

Jenkins is an open source automation server with an unparalleled plugin ecosystem to support practically every tool as part of your delivery pipelines. Whether your goal is [continuous integration](https://en.wikipedia.org/wiki/Continuous_integration), [continuous delivery](https://en.wikipedia.org/wiki/Continuous_delivery) or something else entirely, Jenkins can help automate it.

- https://jenkins.io/2.0/

### Go

Open source continuous delivery server specializing in advanced workflow modeling and visualization

- https://www.go.cd/

### Custom Made Solution

- Workflow individually coded
- Cross cutting concerns such as notifications, error reporting, loggings, scheduling, etc leverage shared and existent or custom made technologies
  - For example, although each workflow could be an individual windows services, they will all use the same error reporting service even if the error reporting is custom made

## Comparison

### Legend

- **GOOD** The technology fully fulfills the desired feature.
- **OK** The desired feature is partially fulfilled. There are some identified caveats.
- **BAD** The desired feature is not present and it could require a significant amount of work to provide the feature.
- **N/A** It is not applicable for this solution

### Matrix

|              Feature              |                Apache Airflow                |                           Pinball                            |                        Spiff                         |                            Luigi                             |                           Ansible                            |                  Dray                  |               CloudSlang               |              ActionChain               |                Mistral                 |
| :-------------------------------: | :------------------------------------------: | :----------------------------------------------------------: | :--------------------------------------------------: | :----------------------------------------------------------: | :----------------------------------------------------------: | :------------------------------------: | :------------------------------------: | :------------------------------------: | :------------------------------------: |
| Sequential and Parallel Execution |                   **GOOD**                   |                                                              |                                                      |                                                              |                                                              | **BAD**<br />Sequential execution only |                                        | **BAD**<br />Sequential execution only |                                        |
|            GUI and API            | **OK**<br />There's no API but there's a CLI |                                                              |                                                      |         **OK**<br />There's no API but there's a CLI         | **BAD**<br />Support YAML<br />Need to pay for Ansible Tower |                                        | **BAD**<br />Sublime textual authoring |                                        | **BAD**<br />Sublime textual authoring |
|           Notification            |                   **GOOD**                   |                                                              |                                                      |                                                              |                                                              |                                        |                                        |                                        |                                        |
|              Dry Run              |                   **GOOD**                   |                                                              |                                                      |                                                              |                                                              |                                        |                                        |                                        |                                        |
|      Loop with Source Array       |                   **GOOD**                   |                                                              |                                                      |                                                              |                                                              |                                        |                                        |                                        |                                        |
|             Template              |                   **GOOD**                   |                                                              |                                                      |                                                              |                                                              |                                        |                                        |                                        |                                        |
|            Extensible             |                   **GOOD**                   |                                                              |                                                      |                                                              |                                                              |                                        |                                        |                                        |                                        |
|         Progress Tracking         |                   **GOOD**                   |                                                              |                                                      |                                                              |                                                              |                                        |                                        |                                        |                                        |
|             Telemetry             |                   **GOOD**                   |                                                              |                                                      |                                                              |                                                              |                                        |                                        |                                        |                                        |
|      Dispatching and Tagging      |                   **GOOD**                   |                                                              |                                                      | **OK**<br />Focus on batch processing with each task as sizable chunk of work<br />Does not support distributed execution<br />This could affect scalability<br />http://luigi.readthedocs.org/en/stable/execution_model.html |                                                              |                                        |                                        |                                        |                                        |
|    Open to Operationalization     |                                              |                                                              |                                                      |                                                              |                                                              |                                        |                                        |                                        |                                        |
|        Logging (Verbosity)        |                                              |                                                              |                                                      |                                                              |                                                              |                                        |                                        |                                        |                                        |
|         Ability to Cancel         |                   **GOOD**                   |                                                              |                                                      |                                                              |                                                              |                                        |                                        |                                        |                                        |
|       Textual Configuration       |                   **GOOD**                   | **BAD**<br />No DSL (domain specific language)I question how they can build a solid UI without DSL |                                                      |                                                              |                                                              |                                        |                                        |                                        |                                        |
|              Support              |                   **GOOD**                   |                                                              | **BAD**<br />2 wiki pages<br />Inactive mailing list |                                                              |                                                              |                                        |                                        |                                        |                                        |
|               Price               |                   **GOOD**                   |                                                              |                                                      |                                                              |                                                              |                                        |                                        |                                        |                                        |
