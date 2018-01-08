---
tags:
  - best practices
  - asp.net
  - tips
  - performance
---

In this post, we will look at why we should avoid large view state when possible.

# Background

In essence, ASP.NET uses the view state in order to keep track of the changes made to the state of a Web Form (i.e. the value in a textbox if it is a server side control) across postbacks. ASP.NET automatically stores and retrieves these values.

# Consequences

The view state is basically a hidden that looks alot like the following

``` html
<input type=”hidden” name=”__VIEWSTATE” id=”__VIEWSTATE” value=”/E9uY2xpY2s9J3dpbmRvdyJFcXVpcG1lbnQ5hc3B4P1MjAxMC1UZXJl…
( continues for 20000 characters)…lVTIaWR0D=” />
```

As the quantity of fields to keep track increase, so does the size of the view state. This could bring problems

1. It increases the amount of data going back and forth. Hence, a page will take longer to download.

2. The extra data can also be subject to corruption, leading to view state exception.

3. ASP.NET needs to serialize and deserialize the view state each time. This lead to longer load time.

# Conclusion

Reducing the number of server controls and relying on simple HTML controls when possible will go a long way in reducing the size of the view state and making your site more responsive.

[50 Ways to Avoid Find and Fix ASP.NET Performance Issues](https://www.red-gate.com/library/50-ways-to-avoid-find-and-fix-asp-net-performance-issues)

