@ECHO OFF
@ECHO This Service Requires .NET Framework 4 to be installed on your system.
:Uninstall
@Echo Trying to stop a service using net stop command.
NET STOP "ACALabelXServerService"

@Echo Uninstalling service.
c:\windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil /u ACALabelXServerService.exe

:End
Pause