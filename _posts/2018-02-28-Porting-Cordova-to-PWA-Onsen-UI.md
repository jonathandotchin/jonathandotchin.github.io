---
tags:
  - pwa
  - mobile
  - app
---

This post examines porting a Cordova app built with Onsen UI to PWA.

# Background

I wanted to experiment with PWA for a while and one of my app was built using Cordova and Onsen UI. I figured that it would be a good starting point to port it to PWA. However, I ran into a couple of issues that I would need to keep in mind for future work.

# Performance

App based on Onsen UI, whether it is based on Vanilla JS or Vue, seems to be performing much slower than apps built with Polymer. In my informal test runs with lighthouse, sample Onsen UI app would score in the 50s whereas sample apps built with Polymer would score in the 90s. These tests are by no means scientific since it is very well possible that the Polymer sample has been optimized specifically for lighthouse.

[Onsen Vanilla JS Lighthouse Report]({{site.url}}/resources/2018-02-28-Porting-Cordova-to-PWA-Onsen-UI/Documents/onsen-vanilla-js-lighthouse-report.pdf "Onsen Vanilla JS Lighthouse Report"){: .align-center}

[Onsen Vue Lighthouse Report]({{site.url}}/resources/2018-02-28-Porting-Cordova-to-PWA-Onsen-UI/Documents/onsen-vue-lighthouse-report.pdf "Onsen Vue Lighthouse Report"){: .align-center}

[News Polymer Lighthouse Report]({{site.url}}/resources/2018-02-28-Porting-Cordova-to-PWA-Onsen-UI/Documents/news-polymer-project-lighthouse-report.pdf "News Polymer Lighthouse Report"){: .align-center}

# Minor Bugs

## Grid

Sometimes, the auto generated code by Onsen UI simply does not match. For instance, given the following

``` html
<ons-row>
    <ons-col width="40px">
    ...
    </ons-col>
</ons-row>
<ons-row>
    <ons-col width="40px">
    ...
    </ons-col>
</ons-row>
<ons-row>
    <ons-col width="40px">
    ...
    </ons-col>
</ons-row>
```

For some reason, the last row included only ```-webkit-box-flex: 0;``` in the CSS whereas the other rows included ```-webkit-box-flex: 0; flex: 0 0 40px; max-width: 40px;```. In this case, I had to switch to a normal HTML table as workaround.

## List Items

Similarly, list item CSS would make things difficult to customize. For instance, the final rendering for list is as follow

``` html
<ons-list-item class="list-item">
  <div class="left list-item__left">Left content</div>
  <div class="center list-item__center">Center content</div>
  <div class="right list-item__right">Right content</div>
</ons-list-item>
```

One thing I noticed is that even if I include ```<p>``` in the content, they would still be generated side by side. As such, I had to manually include ```<br/>``` tag when needed.