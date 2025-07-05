# FortiScheduler
# =================
FortiScheduler is a program written so a non-administrator is able to use the API provided by a FoirtiGate firewall to change the start and end times of a schedule.
The tool allows an IT Administrator to configure it using startup arguments.

The following arguments are available:
-l 10.0.0.1 443 16g8zy5hcy9cgdcr8wdxnk6b0tyG9f -a

-l : Lock the connection settings module, so the user cannot change the connection settings.
-host : The IP address of the FortiGate firewall.
-port : The port of the FortiGate firewall. Default is 443.
-api_key : The API key to authenticate with the FortiGate firewall.
-a : Automatically connect to the FortiGate firewall using the provided settings.
-s : Hide the eventlog module

Example usage:
```
FortiScheduler.exe -host 10.0.0.1 -port 443 -s -l -apikey 16g8zy5hcy9cgdcr8wdxnk6b0tyG9f -a
```

# Requirements
# =================
FortiScheduler requires a windows operating system with .NET 8.0 or higher installed.

*The program is written in C# and uses the FortiOS API to interact with the FortiGate firewall.*
*The program is designed to be easy to use and requires no prior knowledge of the FortiOS API.*
*The program is open source and can be found on GitHub at https://github.com/NE137/FortiScheduler
*The program is licensed under the MIT License, which allows for free use, modification, and distribution of the code.*
*The program is designed to be used by IT administrators to manage schedules on FortiGate firewalls.*
*The program is provided as is, without any warranty or guarantee of functionality.*