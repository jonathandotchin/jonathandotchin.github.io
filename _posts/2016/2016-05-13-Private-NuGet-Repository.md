---
tags:
  - poc
  - nuget
  - package
---

In this post, we will examine our endeavor in the creation of a private NuGet repository.

## Objective

### Version Freeze

When using the official NuGet repository, we are always accessing the latest and greatest version of the libraries. Although, it presents us many advantages such as embracing the latest features and bug fixes, it can also create a very heterogeneous ecosystem when we are not careful. For instance, we could easily end up with multiple version of Entity Framework in Product Suite. The newest version of NuGet (3.0) allows us to lock a solution wise version but it is not yet release and it is only solution bound. Version freezing has the advantage of maintaining an homogenous ecosystem and to upgrade to another version of the library only when the developers are ready and when the library has been tested.

### Internal Libraries

As we create more and more internal libraries, we would require a place to host and share them among the different applications. Candidates to be placed in the repository include:

-   Generic library such as a FileSystem wrapper that is unit testable and mockable
-   Product specific library

### Fallback

A lesser but important characteristic of a private NuGet repository is that it would act as a fallback should the main one becomes offline.

## Features

### Primary

The following are the essential features identified for the proof of concept. The inability of a solution to provide for these features will seriously impact our decision.

##### Ease of Use

If it is too hard to use, we are not going to use it.

##### NuGet

NuGet compatibility will allows us to host our own NuGet package and to host the version we want to proliferate throughout our applications.

##### Vertical Scalability

Although the private repository is only for our team, it still needs to handle several hundred packages. If it scales beyond that, it would be a bonus.

##### Ease In Adding Own Libraries

Adding our own libraries via an API would be ideal. In essence, our build system could create a NuGet package and it would be added directly to the repository.

##### Ease In Adding Third Party Libraries

Ideally, we would have a command where we can select a NuGet library from the official gallery and add it to the repository.

##### Support

The solution needs to be actively maintained by their creators and by the community.

### Nice To Have

The following are the nice to have features identified for the proof of concept. The inability of a solution to provide for these features will have no serious impact on our decision. However, should multiple solutions fulfill the primary features, these secondary features can play a role in our decision.

##### Chocolatey

The ability to host Chocolatey package (which is in essence very similar to NuGet package) would allow us to use Chocolatey to perform apt-get like install in Windows environment.

##### PyPI

PyPI is basically the NuGet equivalent in Python. Since our Linux development language of choice is Python, this would be helpful

##### Horizontal Scalability

Should the solution become extremely popular, the ability to load balance on multiple machine and to scale horizontally would be helpful.

##### Advanced Enterprise Feature

Several nice to have features proper to enterprise applications are

-   Artifacts promotion, demotion and cleanup
-   Security checks
-   License checks
-   Quality checks
-   LDAP Authentication

## Candidates

Since we are in cost cutting mode, we will be focusing on free solution for this Proof of Concept. If the solution are inadequate, we can reconsider.

### NuGet Server

[http://nugetserver.net/](http://nugetserver.net/)

### NuGet Gallery

[https://github.com/NuGet/NuGetGallery](https://github.com/NuGet/NuGetGallery)

### Klondike

[https://github.com/themotleyfool/Klondike](https://github.com/themotleyfool/Klondike)

### Artifactory OSS

[http://www.jfrog.com/open-source/](http://www.jfrog.com/open-source/)

### Sonatype Nexus OSS

[http://www.sonatype.com/nexus/compare-repos](http://www.sonatype.com/nexus/compare-repos)

### ProGet Free

[http://inedo.com/proget](http://inedo.com/proget)

## Comparison

### Essential

|                      | NuGet Server                                                 | NuGet Gallery                                                | Klondike                                                     | Artifactory OSS                           | Sonatype Nexus OSS                                           | ProGet Free                                                  |
| -------------------- | ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ | ----------------------------------------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| NuGet Compatibility  | GREEN                                                        | GREEN                                                        | GREEN                                                        | RED<br />Not supported in the OSS version | YELLOW<br />It is functional<br />NuGet although functional is not treated as a first class citizen in the UI. (i.e. In NuGet Gallery, we have commands and tag, which is not present here) | GREEN                                                        |
| Ease of Use          | GREEN                                                        | RED<br />I couldn't get the code on GitHub to compile, let alone install an instance of it. | YELLOW<br />Some initial trouble compiling in the beginning but once it was rolling, it was rolling | GREY                                      | YELLOW<br />Simple single package<br />Installation via batch file<br /><br />UI is far more metallic and geared towards advanced users<br />The search is also not very functional on the WebUI but it works fine via Visual Studio | GREEN<br />Simple single package installation<br />Administration is done through a responsive Web UI |
| Vertical Scalability | RED<br />It does not behaves well after 100 packages. This is due to a design flaw where the packages are badly indexed and cached | GREY                                                         | GREEN<br />The indexing system seem to make a difference in handling large number of packages | GREY                                      | GREEN<br />A mature product                                  | GREEN<br />So far I added hundreds of package at the same time without any signs of slowdown |
| Custom Libraries     | GREY                                                         | GREY                                                         | GREEN                                                        | GREY                                      | GREEN<br />Ability to add package by command line which works very well with Go | GREEN<br />Ability to add package by command line which works very well with Go |
| Official Libraries   | GREY                                                         | GREY                                                         | GREEN                                                        | GREY                                      | GREEN<br />It works but<br />We cannot pull from the official gallery nor can we import<br />We can create a proxy but it doesn't help us freeze version<br />Nevertheless, there is no real blocker | GREEN<br />Ability to pull install package from another repository such as the official NuGet Gallery<br />Ability to bulk import |
| Support              | GREY                                                         | GREY                                                         | RED<br />It doesn't look very active with only 2 contributors. It looks like someone pet project. | GREY                                      | GREEN<br />A veteran in the field<br />Community looks strong | YELLOW<br />A rather young company and young product. For instance, on stackoverflow the oldest question is from 2012.<br />There were a couple of bugs but once I raised them, they were actually fixed.<br />Based on my interaction by email, it looks like they are quite aggressive in seeking customers. |
| Summary              | RED<br />We got a showstopper                                | RED<br />We got a showstopper                                | RED<br />Lack of tracking for this project                   | RED<br />We got a showstopper             | GREEN<br />Solid and proven solution<br />Tons of features<br />Perhaps beyond our use case<br />UI is rather an old industrial java design<br />NuGet is not treated as a first class citizen. It works but in a generic way. | GREEN<br />Solid solution provided by a rather young product and company. |


### Nice to Have

|                        |                         NuGet Server                         |                        NuGet Gallery                         |                           Klondike                           |                       Artifactory OSS                        |                      Sonatype Nexus OSS                      |                         ProGet Free                          |
| :--------------------: | :----------------------------------------------------------: | :----------------------------------------------------------: | :----------------------------------------------------------: | :----------------------------------------------------------: | :----------------------------------------------------------: | :----------------------------------------------------------: |
|       Chocolatey       |                             GREY                             |                             GREY                             |                             GREY                             |                             GREY                             | YELLOW<br />Since chocolatey is basically a NuGet repository, it works<br />However, it makes no difference between NuGet packages and Chocolatey packages |         GREEN<br />Supported as first class citizen          |
|          PyPI          |                             GREY                             |                             GREY                             |                             GREY                             |                             GREY                             |  YELLOW<br />Not supported but it is plan for the year 2015  | YELLOW<br />Not supported but included in the roadmap when i asked for it |
| Horizontal Scalability |                             GREY                             |                             GREY                             |                             GREY                             |                             GREY                             | GREEN<br />Horizontal Scalability is provided through the means of a proxy | RED<br />Real load balancing requires the purchase of the enterprise edition |
|  Enterprise Features   |                             GREY                             |                             GREY                             |                             GREY                             |                             GREY                             | YELLOW<br />The OSS version contains some features such as LDAP authentication. However, you will need to go PRO to take advantage of advanced feature such as security and license checks. | RED<br />The free version is aimed at small team and is stripped of any enterprise features. Even the PRO version doesn't contain any of the enterprise feature in Nexus OSS for instance |
|        Summary         | GREY<br />We got a showstopper in the essential so we did not investigate further | GREY<br />We got a showstopper in the essential so we did not investigate further | GREY<br />We got a showstopper in the essential so we did not investigate further | GREY<br />We got a showstopper in the essential so we did not investigate further | YELLOW<br />It is clear the the OSS version is good enough.  | RED<br />If we ever want to go above and beyond a private and internal NuGet, this solution is not viable. |

