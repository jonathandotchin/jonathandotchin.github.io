---
tags:
  - best practices
  - tips
  - performance
---

When creating a field which will contains text, it is important to choose the right string type in order to save space and increase performance.

char(n) and nchar(n) are fixed length string that will reserve space for n bytes no matter what's the size of the actual text. For example, char(10) will use 10 bytes to store it's data even if some values contains only 5 char (SQL will pad left with 5 blank char)

nchar(n) and nvarchar(n) can contains unicode char but will use two times the necessary space (+ 2 bytes for nvarchar).

text and ntext: those are deprecated



Which one to choose?

- char(n):	IDs, GUID, phone numbers or abbreviation like studio short name
- nchar(n):	Enum value, status
- varchar(n):	URL, path, username or simple application messages
- nvarchar(n):	User input or complex messages
 

ProTips

If the text field is supposed to be often queried, don't use nvarchar (unless necessary) as this cause SQL to use high CPU for searching purpose.
If the text field is used to join two tables, both fields should be of the same type to avoid performance issue (they both use different storage strategy so SQL will try casting types)
To increase performance, char should be preferred over varchar because fixed length string will allow SQL to skip calculating the text size for each row (only if text length varies slightly, i.e. ~5 to 10 char)
