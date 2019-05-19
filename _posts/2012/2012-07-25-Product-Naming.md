---
tags:
  - best practices
---

# Summary

MSDN (http://msdn.microsoft.com/en-us/library/ms229002.aspx) describes in detail the standards to follow in what concerns the nomenclature of namespaces, classes, structs, interfaces, assemblies, DLLs, etc.
For example, namespaces, the recommended format is:
```
< Company >.(< Product > | < Technology >)[. < Feature >][. < Subnamespace >]
```

This section aims to cover what is not in MSDN.

## Code Name

Code names are often used to clearly represent the product in development until an official name can be assigned. They are mainly:

- Simple
- Descriptive
- Static and independent of the marketing

The marketing name of a product change for several reasons but the code name can always stay the same

### Usage

Mainly, for an internal audience like in Perforce

## Product Name

The product names are the final names given to a product. The marketing name is often used for the sale of the product. They are mainly:

- Catchy & Clever
- Descriptive but subtle
- Victim to rebranding

### Usage

Mainly, for an external audience like in a public confluence