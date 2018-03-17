@ECHO OFF
@ECHO This Service Requires .NET Framework 4 to be installed on your system.
:Uninstall
@Echo Trying to stop a service using net stop command.
NET STOP "ACALabelXClientService"

@Echo Uninstalling service.
C:\Windows\Microsoft.NET\Framework\v4.0.30319\\InstallUtil /u ACALabelXClientService.exe

:End
Pause