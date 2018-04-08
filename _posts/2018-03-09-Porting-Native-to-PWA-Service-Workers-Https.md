---
tags:
  - pwa
  - mobile
  - app
---

> Using service worker you can hijack connections, fabricate, and filter responses. Powerful stuff. While you would use these powers for good, a man-in-the-middle might not. To avoid this, you can only register service workers on pages served over HTTPS, so we know the service worker the browser receives hasn't been tampered with during its journey through the network.

This means that not only the service worker must be delivered via HTTPS but anything that it touches. For example, if the service worker is attempting to cache a HTML page or an image, it must be delivered via HTTPS.