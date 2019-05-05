---
tags:
  - best practices
---

This post details the versioning strategy we are using for our products. We are talking about products and not libraries, web services or APIs.

# Background

Our current strategy for versioning our product is simply based on time. In essence, if it is the first release of the year, we would version as "2010.1", the second version would be "2010.2" and so on until we reach another year where it becomes "2011.1". It was fairly simple for us but our customers were confused. They expected that, between "2010.3" and "2011.1", there would be big changes because we are jumping year where it is not always the case. In order cases, some of our products were small and fairly stable. It is not uncommon for them to not get a new version for over a year so their version would jump from "2010.1" to "2011.1". So we wanted to have a new versioning scheme that provide meaning, that provide information on the product.

# Semantic Versioning

Currently, the versioning strategy of choice for APIs is based on [semantic versioning](http://semver.org/). The technique sums up to:

>Given a version number MAJOR.MINOR.PATCH, increment the:
>
> 1. MAJOR version when you make incompatible API changes,
> 2. MINOR version when you add functionality in a backwards-compatible manner, and
> 3. PATCH version when you make backwards-compatible bug fixes.
>
>Additional labels for pre-release and build metadata are available as extensions to the MAJOR.MINOR.PATCH format.

For our products, we decided to try a similiar approach.

# Products Versioning 

## First Attempt

Initially, we came up with the following scheme which an almost copy paste from the semantic versioning.

Given a version number MAJOR.MINOR.PATCH, increment the:

1. MAJOR version when you make incompatible changes to the product,
2. MINOR version when you add functionality in a backwards-compatible manner, and
3. PATCH version when you make backwards-compatible bug fixes.

After using this strategy for a while, we realized a few things:

1. We didn't increase the PATCH version for the simple reason that we bundle those fixes with a release that contains added functionalities. In consequence, we would always increment the MINOR version and reset the PATCH version to 0.
2. We didn't increase the MAJOR version since all our changes are expected to be backwards-compatible. Even if features are deprecated, it is expected that we provide a smooth transition, often transparent to the user.

## Second Attempt

In the second attempt, we decided to examine what kind of information we wanted to convey when changing version numbers. We decided to give the following a try.

Recall that our products are always backward compatible and bug fixes are bundle together with other changes.

Given a version number MAJOR.MINOR, increment the:

1. MAJOR version when you make changes that changes the scope of the product, hence you want to make marketing splash,
2. MINOR version when you make any other changes to the products

# Conclusion

It has been around 1 year now for our products with that versioning scheme. So far the comments from our customers were positives. They find it easier to understand than the previous scheme.