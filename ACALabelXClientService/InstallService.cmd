@ECHO OFF
@ECHO This Service Requires .NET Framework 4 to be installed on your system.
@Echo Installing ACA LabelX Service
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil /ShowCallStack /user=.\ACALabelPrint /password=lp131-Password ACALabelXClientService.exe                                                          
@Echo To start the service manually type the command below or use ACALP_Config.exe
@Echo NET START "ACALabelXClientService"
GOTO End

:End
Pause