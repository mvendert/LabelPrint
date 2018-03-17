@ECHO OFF
@ECHO This Service Requires .NET Framework 4 full 4 installed on your system.
@Echo Installing ACA LabelX Service
C:\windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil /ShowCallStack ACALabelXServerService.exe
@Echo Starting ACA LabelX Service
NET START "ACALabelXServerService"
GOTO End

:End
Pause