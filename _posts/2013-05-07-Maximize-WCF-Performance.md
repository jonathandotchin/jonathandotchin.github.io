---
tags:
  - best practices
  - tips
  - wcf
  - performance
---

# Service Definition
For the given service definition below,

``` xml
<system.serviceModel>
    <services>
      <service name="SomeService.SomeService">
        <endpoint address="soap" binding="basicHttpBinding"
          bindingConfiguration="" name="basicHttpBinding" contract="SomeService.ServiceContract.ISomeService" />
        <endpoint address="rest" binding="webHttpBinding"
          bindingConfiguration="UltraWebHttpBinding"
name="webHttpBinding" contract="SomeService.ServiceContract.ISomeService" behaviorConfiguration="RESTfulBehavior" />
      </service>
    </services>
  </system.serviceModel>
```

# Binding Configuration

``` xml
<system.serviceModel>
    <bindings>
      <webHttpBinding>
        <binding name="UltraWebHttpBinding"
               maxBufferPoolSize="2147483647"
               maxBufferSize="2147483647"
               maxReceivedMessageSize="2147483647">
          <security mode="None">
            <transport clientCredentialType="None"/>
          </security>
        </binding>
      </webHttpBinding>
    </bindings>
  </system.serviceModel>
```
http://msdn.microsoft.com/en-us/library/bb412176.aspx

# Service Behaviors
``` xml
<system.serviceModel>
    <behaviors>
      <serviceBehaviors>
        <behavior>
          <serviceThrottling maxConcurrentCalls="2147483647" maxConcurrentSessions="2147483647" maxConcurrentInstances="2147483647" />
        </behavior>
      </serviceBehaviors>
    </behaviors>
  </system.serviceModel>
```
http://msdn.microsoft.com/en-us/library/ms731379.aspx

# Endpoint Behaviors
``` xml
<system.serviceModel>
    <behaviors>
      <endpointBehaviors>
        <behavior name="RESTfulBehavior">
          <dataContractSerializer maxItemsInObjectGraph="2147483647" />
          <webHttp helpEnabled="true"/>
        </behavior>
      </endpointBehaviors>
    </behaviors>
  </system.serviceModel>
```
http://msdn.microsoft.com/en-us/library/aa749852.aspx