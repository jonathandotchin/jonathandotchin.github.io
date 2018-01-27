---
tags:
  - best practices
  - tips
  - wcf
---

Windows Communication Foundation (WCF) provides the capability to log incoming and outgoing messages for offline consumption. Message logging enables you to see what the message and message body looks like. This type of logging is particularly helpful in letting you know what arguments were passed in and how the receiving endpoint saw the arguments expressed as XML. In addition, logging the message as it was received allows you to diagnose malformed messages as well as to see how the message arrived. You can also examine the security tokens used, parts encrypted and signed, and parts left intact.

http://msdn.microsoft.com/en-us/library/ms731859(v=vs.110).aspx

For instance, the following configuration is good to get a log of all the methods being called from a WCF service. It is something that typical IIS is unable to get considering they are POST versus GET.

``` xml
...
<system.diagnostics>
  <sources>
   <source name="System.ServiceModel.MessageLogging" switchValue="Information,ActivityTracing">
    <listeners>
     <add type="System.Diagnostics.DefaultTraceListener" name="Default">
      <filter type="" />
     </add>
     <add name="ServiceModelMessageLoggingListener">
      <filter type="" />
     </add>
    </listeners>
   </source>
  </sources>
  <sharedListeners>
   <add initializeData="E:\Logs\Messages.svclog"
    type="System.Diagnostics.XmlWriterTraceListener, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    name="ServiceModelMessageLoggingListener" traceOutputOptions="Timestamp">
    <filter type="" />
   </add>
  </sharedListeners>
  <trace autoflush="true" />
 </system.diagnostics>
...
<diagnostics wmiProviderEnabled="true">
   <messageLogging logEntireMessage="false" logMalformedMessages="false"
    logMessagesAtServiceLevel="true" logMessagesAtTransportLevel="false"
    maxMessagesToLog="3000" />
  </diagnostics>
```