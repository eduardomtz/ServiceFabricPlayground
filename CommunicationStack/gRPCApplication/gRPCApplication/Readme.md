Execute this to compile Calculator.proto in Package Manager Console window.
Change the version of the nuget package accordingly.

`packages\Grpc.Tools.<version>\tools\windows_x86\protoc.exe --csharp_out Calculator --grpc_out Calculator ./Calculator.proto --plugin=protoc-gen-grpc=packages\Grpc.Tools.<version>\tools\windows_x86\grpc_csharp_plugin.exe`


I hit DNS resolution failed, because my computer was actively rejecting the calls.

Enabling the logs helped figure out and the workaround was:
1) Corroborate that my computer was rejecting the calls with PowerShell:

`Test-NetConnection 127.0.0.1 -Port 8189`

2) Validate if I had the ports open, also PowerShell:

`netstat -na | Select-String <port>`

3) At this point I noticed that for some reason my computer had a local IP instead of 127.0.0.1 or localhost, and other IPv6:

> TCP    192.168.1.114:<port>     0.0.0.0:0              LISTENING
  
> ...

4) Added the hostname to resolve to 192.168.1.114 in drivers\etc\hosts allowed communication.
