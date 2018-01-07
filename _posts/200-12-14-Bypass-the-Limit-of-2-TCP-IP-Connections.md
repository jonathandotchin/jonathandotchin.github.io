---
tags:
  - best practices
  - snippet
  - asp.net
---

Make sure you are not limited by 2 tcp ip connections when you don't need to
The default setting for the maximum amount of connection is two per connection group. This can be changed with a simple modification on the client web.config/app.config

``` xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <system.net>
    <connectionManagement>
      <add address="*" maxconnection="256" />
    </connectionManagement>
  </system.net>
</configuration>
```