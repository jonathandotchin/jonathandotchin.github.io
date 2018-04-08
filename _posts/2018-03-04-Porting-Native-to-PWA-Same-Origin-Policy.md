---
tags:
  - pwa
  - mobile
  - app
---

This post examine a potential issue when porting a native app to PWA.

# Background

One of the first thing we want to do when porting a native app to PWA is to obtain our data for our app. Typically, the way of doing this is via a AJAX call to the remote resource. However, when performing this call, we stumble an error similar to "No ‘Access-Control-Allow-Origin’ header is present on the requested resource.". We basically encountered the "Same-Origin Policy".

# Same-Origin Policy

Same-Origin Policy is a security construct whereas the web browser restrict access to resources to document and scripts from the same origin. This is a very good thing and you can read more about it [here](https://en.wikipedia.org/wiki/Same-origin_policy). But what we want would be how to workaround it.

# Workaround

## Cross-Origin-Resource Sharing

Using Cross-Origin-Resource Sharing (CORS) is the proper way of solving this problem but it requires modification to the remote resources being accessed. In essence, CORS provides a mean to tell the browser that requesting a resource in domain A from domain B is allowed. This is done by including a new HTTP header "Access-Control-Allow-Origin" that that would contain a list of accepted domain such that when the browser receives a response, it would check the content of the header to see if the current origin matches the one listed in the header. If there is a match, then the browser allows access to the response. For example, if we have a script hosted on "https://www.siteA.com" and we would like to access another script at "https://www.siteB.com", then the response from htts://www.siteB.com should include a header "Access-Control-Allow-Origin" with the value "https://www.siteA.com". It is also possible to include a '*' to allow any origin access.

This is by no mean an exhaustive overview of CORS. A more detailed writeout can be found on the [Mozilla Developer Network](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS)

## Proxy Server

The Same-Origin Policy, being enforced by the browser, can be overcome by using a proxy server located in the same origin as your web application. In essence, we could our own web service that would simply forward call to the actual resource. Since it is a web service making the call to the remote resource, it is not constrained by the Same-Origin Policy. In this case, the web service will simply return the requested resource. If the proxy server is not at the same origin as your web application, you can simply include the CORS header.

This does have limitations such as authentication.

### YQL

If you cannot build a proxy server yourself, you can leverage [YQL](https://developer.yahoo.com/yql/) to this effect. In essence, the YQL query would be as follow:

```YQL
select * from json where url = yoururl
```
which becomes https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20json%20where%20url%20%3D%20%22yoururl%22&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys.

Or for XML
```YQL
select * from xml where url = yoururl
```
which becomes https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20xml%20where%20url%20%3D%20%22yoururl%22&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys.

Thereafter, you simply parse the output in consequence.

## JSONP

Another solution would rely on the fact that ```<script>``` tags can have sources of different origins; thus, when a browser parses a ```<script>``` tag, it will GET the content and execute it regardless of its origin. JSONP takes advantages of this situation. One of the easiest way to leverage this would be with ```JQuery```.

``` javascript
$.ajax({
    url: "https://remote.resources.com",
    dataType: "jsonp",
    cache: false,
    success: function (json) {
        // use resource
    }
})
```

# Further Information

Unlike native or hybrid app like those built with Cordova, PWA runs directly from a full screen browser. They are therefore restricted by the same security model of any normal web pages. Same-Origin Policy is just one of them.