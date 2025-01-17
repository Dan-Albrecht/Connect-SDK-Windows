# Connect-SDK-Windows
Connect SDK is an open source framework that connects your mobile apps with multiple TV platforms. Because most TV platforms support a variety of protocols, Connect SDK integrates and abstracts the discovery and connectivity between all supported protocols.

For more information, visit our [website](http://www.connectsdk.com/).

* [General information about Connect SDK](http://www.connectsdk.com/discover/)

## Fork Info
This fork targets .NET Standard 2.0.

## Including Connect SDK in your app
* Clone this repository
* Include the ConnectSDK.Windows project into your solution by right clicking your solution name, select add existing project and selecting the ConnectSdk.Windows.csproj file
* Reference ConnectSDK.Windows from the project you want to use it by right clicking the project name, choose add refference and select the ConnectSDK.Windows project)

-- or --

Install the [nuget package](https://www.nuget.org/packages/ConnectSDK.Windows/) by running the command: Install-Package ConnectSDK.Windows

Look at the [Connect-SDK-Windows-Sampler](https://github.com/ConnectSDK/Connect-SDK-Windows-Sampler) for an example on how to use the framework.

The basics are:
* implement the IDiscoveryManagerListener (like [here](https://github.com/ConnectSDK/Connect-SDK-Windows-Sampler/blob/master/ConnectSdk.Demo/ConnectSdk.Demo.Shared/DiscoveryManagerListener.cs)) to be able to be notified when a device was discoverred, connected or disconnected
* call to connect to the device (like [here](https://github.com/ConnectSDK/Connect-SDK-Windows-Sampler/blob/master/ConnectSdk.Demo/ConnectSdk.Demo.WindowsPhone/Search.xaml.cs))
* use the device object to interact with it

## Contact
* Add any issues [here](https://github.com/ConnectSDK/Connect-SDK-Windows/issues)

## License
Copyright (c) 2015, [SDaemon](https://github.com/sdaemon).

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

> http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
