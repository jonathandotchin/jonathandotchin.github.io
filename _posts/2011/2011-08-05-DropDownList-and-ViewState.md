---
tags:
  - best practices
  - tips
---

You may think it’s impossible to turn off ViewState on a DropDownList, even if you re-bind it on every postback. But with a tiny bit of elbow grease you can keep ViewState enabled and avoid passing all your option values back and forth. This is particularly worthwhile for DropDownLists with a big ListItem collection. One way is to turn off ViewState and bind the select value manually to the actual posted value, like so:
```
string selectedId = Request[Countries. UniqueID];
if (selectedId != null)
    Countries.SelectedValue = selectedId;
```
However, you may prefer something I came across more recently. Instead of binding your DropDown- List in the typical Page_Load or Page_Init, bind it in the control’s Init event:
```
<asp:DropDownList ID=”Countries” ... OnInit=”CountryListInit” />
protected void CountryListInit(object sender, EventArgs e)
{
    Countries.DataSource = // get datafrom database
    Countries.DataBind();
}
```
[50 Ways to Avoid Find and Fix ASP.NET Performance Issues](https://www.red-gate.com/library/50-ways-to-avoid-find-and-fix-asp-net-performance-issues)