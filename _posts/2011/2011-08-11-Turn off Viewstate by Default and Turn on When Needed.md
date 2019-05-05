---
tags:
  - best practices
  - tips
---

Make it your habit to turn off ViewState on every control by default, and only turn it on when you need it. If a page doesn’t need ViewState anywhere, turn it off at the page level. You do all that work to reduce requests, combine and compress static references, and make sure your code is as clean as possible - don’t ruin it with a ViewState monster! If you’re anal, you can completely remove all traces of ViewState from pages that don’t need it by inheriting from a BasePage such as this:

```
/// <summary>
/// BasePage providing cross-site functionality for pages that should not have ViewState enabled.
/// </summary>
public class BasePageNoViewState : Page // Or of course, inherit from your standard BasePage, which in turn inherits from Page
{
    protected override void SavePageStateToPersistenceMedium(object viewState)
    {
    }
 
    protected override object LoadPageStateFromPersistenceMedium()
    {
        return null;
    }
 
    protected override void OnPreInit(EventArgs e)
    {
        // Ensure that ViewState is turned off for every page inheriting this BasePage
        base.EnableViewState = false;
        base.OnPreInit(e);
    }
}
```

[50 Ways to Avoid Find and Fix ASP.NET Performance Issues](https://www.red-gate.com/library/50-ways-to-avoid-find-and-fix-asp-net-performance-issues)