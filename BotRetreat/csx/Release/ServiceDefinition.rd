<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="BotRetreat" generation="1" functional="0" release="0" Id="397a010c-a933-4932-9560-755195694616" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="BotRetreatGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="BotRetreat.Web.ScriptValidation:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/BotRetreat/BotRetreatGroup/LB:BotRetreat.Web.ScriptValidation:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="BotRetreat.Web:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/BotRetreat/BotRetreatGroup/LB:BotRetreat.Web:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="BotRetreat.Web.ScriptValidation:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/BotRetreat/BotRetreatGroup/MapBotRetreat.Web.ScriptValidation:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="BotRetreat.Web.ScriptValidationInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/BotRetreat/BotRetreatGroup/MapBotRetreat.Web.ScriptValidationInstances" />
          </maps>
        </aCS>
        <aCS name="BotRetreat.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/BotRetreat/BotRetreatGroup/MapBotRetreat.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="BotRetreat.WebInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/BotRetreat/BotRetreatGroup/MapBotRetreat.WebInstances" />
          </maps>
        </aCS>
        <aCS name="BotRetreat.Worker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/BotRetreat/BotRetreatGroup/MapBotRetreat.Worker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="BotRetreat.WorkerInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/BotRetreat/BotRetreatGroup/MapBotRetreat.WorkerInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:BotRetreat.Web.ScriptValidation:Endpoint1">
          <toPorts>
            <inPortMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.Web.ScriptValidation/Endpoint1" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:BotRetreat.Web:Endpoint1">
          <toPorts>
            <inPortMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.Web/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapBotRetreat.Web.ScriptValidation:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.Web.ScriptValidation/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapBotRetreat.Web.ScriptValidationInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.Web.ScriptValidationInstances" />
          </setting>
        </map>
        <map name="MapBotRetreat.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.Web/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapBotRetreat.WebInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.WebInstances" />
          </setting>
        </map>
        <map name="MapBotRetreat.Worker:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.Worker/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapBotRetreat.WorkerInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.WorkerInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="BotRetreat.Web" generation="1" functional="0" release="0" software="C:\TFS\BotRetreat\BotRetreat\csx\Release\roles\BotRetreat.Web" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;BotRetreat.Web&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;BotRetreat.Web&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;BotRetreat.Web.ScriptValidation&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;BotRetreat.Worker&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="NETFXInstall" defaultAmount="[1024,1024,1024]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.WebInstances" />
            <sCSPolicyUpdateDomainMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.WebUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.WebFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="BotRetreat.Web.ScriptValidation" generation="1" functional="0" release="0" software="C:\TFS\BotRetreat\BotRetreat\csx\Release\roles\BotRetreat.Web.ScriptValidation" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="8080" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;BotRetreat.Web.ScriptValidation&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;BotRetreat.Web&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;BotRetreat.Web.ScriptValidation&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;BotRetreat.Worker&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="NETFXInstall" defaultAmount="[1024,1024,1024]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.Web.ScriptValidationInstances" />
            <sCSPolicyUpdateDomainMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.Web.ScriptValidationUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.Web.ScriptValidationFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="BotRetreat.Worker" generation="1" functional="0" release="0" software="C:\TFS\BotRetreat\BotRetreat\csx\Release\roles\BotRetreat.Worker" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;BotRetreat.Worker&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;BotRetreat.Web&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;BotRetreat.Web.ScriptValidation&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;BotRetreat.Worker&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="NETFXInstall" defaultAmount="[1024,1024,1024]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.WorkerInstances" />
            <sCSPolicyUpdateDomainMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.WorkerUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.WorkerFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="BotRetreat.WebUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="BotRetreat.Web.ScriptValidationUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="BotRetreat.WorkerUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="BotRetreat.WebFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="BotRetreat.Web.ScriptValidationFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="BotRetreat.WorkerFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="BotRetreat.Web.ScriptValidationInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="BotRetreat.WebInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="BotRetreat.WorkerInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="1ec0e88d-58ea-431d-89ab-88b39a594521" ref="Microsoft.RedDog.Contract\ServiceContract\BotRetreatContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="92146005-8328-4d87-827c-4d0a8a8a44f5" ref="Microsoft.RedDog.Contract\Interface\BotRetreat.Web.ScriptValidation:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.Web.ScriptValidation:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="58b81447-7c9c-483f-b83e-c0f95d87736f" ref="Microsoft.RedDog.Contract\Interface\BotRetreat.Web:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/BotRetreat/BotRetreatGroup/BotRetreat.Web:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>