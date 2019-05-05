---
tags:
  - best practices
  - tips
---

Unless you’re tracking a Text_Changed event, you don’t need ViewState enabled on TextBoxes and similar controls. Classic ASP.NET automatically repopulates TextBox.Text values upon postback, even without ViewState enabled. Turn it off on each TextBox with EnableViewState= “false” on each one. You can do this for other controls like labels, but unless you’re setting their values after the page’s load event, you won’t reduce the size of the ViewState.

[50 Ways to Avoid Find and Fix ASP.NET Performance Issues](https://www.red-gate.com/library/50-ways-to-avoid-find-and-fix-asp-net-performance-issues)